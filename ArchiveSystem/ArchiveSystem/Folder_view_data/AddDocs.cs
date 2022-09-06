using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Management;
using System.Data.SqlClient;
using ArchiveSystem.Folder_view_data;
using System.Text.RegularExpressions;

namespace ArchiveSystem.EditeDocs
{
    public partial class AddDocs : MetroFramework.Forms.MetroForm
    {
        public static string _bc;
        public AddDocs(string _BookCode)
        {
            _bc = _BookCode;

            InitializeComponent();
        }
        string FTP_ip = ConfigurationSettings.AppSettings["FTP_Server_Ip"];
        string FTP_user = ConfigurationSettings.AppSettings["FTP_Server_user"];
        string FTP_pass = ConfigurationSettings.AppSettings["FTP_Server_pass"];
        string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

        public static string photo_con_ = ConfigurationManager.ConnectionStrings["photo_con"].ConnectionString;
        SqlConnection photo_con = new SqlConnection(photo_con_);

        //variables 
        public string selectedFolder = "";
        public string picture_path = "";
        public string Doc_source = "";
        public int DepID = 0;

        string BookNumber = Form_show_edit_docs._BookNumber;
        string subject = Form_show_edit_docs._subject;
        string Filtered_BookNumber = Regex.Replace(Form_show_edit_docs._BookNumber, @"[^0-9a-zA-Zء-ي]", " ");
        string Filtered_subject = Regex.Replace(Form_show_edit_docs._subject, @"[^0-9a-zA-Zء-ي]", " ");


        string Typee = Form_show_edit_docs._BookType;
        //string book_code = Form_show_edit_docs._BookCode;

        private void AddDocs_Load(object sender, EventArgs e)
        {
            Doc_source = Properties.Settings.Default.DOC_Source.ToString(); // doc source
            Refresh_Folders();
        }

        private void BTN_Scan_Click(object sender, EventArgs e)
        {
            ScanDialog sd = new ScanDialog();
            sd.Show();
        }

        private void Scanning_Folder_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog1.ShowDialog();
                string Doc_source = folderBrowserDialog1.SelectedPath;

                Properties.Settings.Default.DOC_Source = Doc_source;

                Properties.Settings.Default.Save();

            }
            catch
            {
                MessageBox.Show("الرجاء اختيار مصدر الملفات");
            }

        }
        public void folder_update()

        {
            string[] Files = Directory.GetFiles(Doc_source + @"\" + selectedFolder + "", "*.*");//put variable name instade of path
            DataTable table = new DataTable();

            table.Columns.Add("check", typeof(bool));
            table.Columns.Add("File Name");

            for (int i = 0; i < Files.Length; i++)
            {
                FileInfo file = new FileInfo(Files[i]);

                table.Rows.Add(false, file.Name);

            }

            DGV_Files.DataSource = table;

            DGV_Files.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CHK_selectall.Visible = true;
        }
        //get folders(scanned books) of speacific folders
        public void Refresh_Folders()
        {
            try
            {
                Doc_source = Properties.Settings.Default.DOC_Source.ToString(); // doc source

                string[] folders = Directory.GetDirectories(Doc_source);
                DataTable folderDT = new DataTable();

                folderDT.Columns.Add("اسم الملف");

                for (int i = 0; i < folders.Length; i++)
                {
                    FileInfo folder = new FileInfo(folders[i]);
                    folderDT.Rows.Add(folder.Name);
                }
                DGV_Folders.DataSource = null;
                DGV_Folders.DataSource = folderDT;

            }
            catch
            {
                MessageBox.Show("الرجاء اختيار مصدر الملفات");
            }
        }

        private void DGV_Folders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            String folderName = DGV_Folders.Rows[e.RowIndex].Cells[0].Value.ToString();
            selectedFolder = folderName;

            string[] Files = Directory.GetFiles(Doc_source + @"\" + folderName + "", "*.*");//put variable name instade of path
            DataTable table = new DataTable();

            table.Columns.Add("check", typeof(bool));
            table.Columns.Add("File Name");

            for (int i = 0; i < Files.Length; i++)
            {
                FileInfo file = new FileInfo(Files[i]);

                table.Rows.Add(false, file.Name);


            }

            DGV_Files.DataSource = table;

            DGV_Files.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CHK_selectall.Visible = true;

            int row = DGV_Folders.CurrentCell.RowIndex;
            TXT_addFolder.Text = DGV_Folders.Rows[row].Cells[0].Value.ToString();
        }

        private void CHK_selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_selectall.Checked == true)
            {
                foreach (DataGridViewRow row in DGV_Files.Rows)
                {
                    row.Cells[0].Value = row.Cells[0].Value = true;

                }
            }
            else if (CHK_selectall.Checked == false)
            {
                foreach (DataGridViewRow row in DGV_Files.Rows)
                {
                    row.Cells[0].Value = row.Cells[0].Value = false;

                }
            }
        }

        private void DGV_Files_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in DGV_Files.Rows)
            {
                try
                {
                String file_name = DGV_Files.Rows[e.RowIndex].Cells[1].Value.ToString();
                Image image2 = Image.FromFile(Doc_source + @"\" + selectedFolder + @"\" + file_name + "");//put var here

                pictureBox1.Image = new Bitmap(image2);

                image2.Dispose();
                //get pah to use it to display img in wndows exploror
                picture_path = (Doc_source + @"\" + selectedFolder + @"\" + file_name + "");
                }
               catch
                {

                }
            }
        }

        private void BTN_AddDocs_Click(object sender, EventArgs e)
        {
            List<string> files_checked = new List<string>();
            for (int i = 0; i < DGV_Files.Rows.Count; i++)
            {
                bool is_checked = (bool)DGV_Files.Rows[i].Cells[0].Value;
                {
                    if (is_checked == true)
                    {

                        files_checked.Add(DGV_Files.Rows[i].Cells[1].Value.ToString());
                    }
                }
            }



            string[] Files = Directory.GetFiles(Doc_source + @"\" + selectedFolder + "");//put variable here 

            int ii = 0;
            //foreach on checked files
            foreach (var item in files_checked)
            {

                string filenamechecked = item.ToString();
                foreach (string file in Files)
                {

                    string file_name = Path.GetFileName(file);

                    if (file_name == filenamechecked)
                    {
                        //upload selected files only 




                        string fn = Filtered_BookNumber + Filtered_subject + DateTime.Now.ToString("yyyyMMddhhmmss") + ii + Path.GetExtension(file);

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip + Typee + "/" + _bc + "/" + fn);
                        request.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        using (Stream fileStream = File.OpenRead(file))

                        using (Stream ftpStream = request.GetRequestStream())
                        {
                            fileStream.CopyTo(ftpStream);

                        }
                        //insert image file to db
                        photo_con.Open();
                        byte[] fileBytes = System.IO.File.ReadAllBytes(file);

                        string photoquery = @"INSERT INTO [dbo].[ArchiveImagesTBL]([bookCode],[filename],[image])
                                                                      VALUES  (@bookcode,@filename,@img)";

                        SqlCommand photo_cmd = new SqlCommand(photoquery, photo_con);

                        photo_cmd.Parameters.AddWithValue("bookcode", _bc);
                        photo_cmd.Parameters.AddWithValue("filename", fn);

                        photo_cmd.Parameters.AddWithValue("img", fileBytes);

                        photo_cmd.ExecuteNonQuery();
                        photo_con.Close();
                        if (File.Exists(file))
                        {
                            File.Delete(file);



                        }

                    }

                    ii++;

                }
            }
            folder_update();

           

                var path = string.Format(path_folder_client_temp);

                //BookCode = advanc_dgv_view_data_doc.CurrentRow.Cells[0].Value.ToString();


                //refresh view list 
                this.Hide();
                Form_show_edit_docs fshDocs = new Form_show_edit_docs(_bc);
                fshDocs.Show();
                //fshDocs.PrefermCall();


                MessageBox.Show("تم ارفاق المستندات");
               

        
        }
     

        private void BTN_GobackToDetails_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_show_edit_docs fshdocs = new Form_show_edit_docs(_bc);
            fshdocs.Show();
        }

        private void BTN_addfolder_Click(object sender, EventArgs e)
        {
            string root = Doc_source + @"\" + TXT_addFolder.Text + "";

            Directory.CreateDirectory(root);
            Refresh_Folders();
        }

        private void BTN_DELFolder_Click(object sender, EventArgs e)
        {
            string root = Doc_source + @"\" + TXT_addFolder.Text + "";

            Directory.Delete(root, true);

            Refresh_Folders();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            BTN_GobackToDetails_Click(null, null);
        }
    }
}

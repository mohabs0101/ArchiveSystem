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

namespace ArchiveSystem.EditeDocs
{
    public partial class EditeDocs : MetroFramework.Forms.MetroForm
    {
        public EditeDocs()
        {
            InitializeComponent();
        }
        string FTP_ip = ConfigurationSettings.AppSettings["FTP_Server_Ip"];
        string FTP_user = ConfigurationSettings.AppSettings["FTP_Server_user"];
        string FTP_pass = ConfigurationSettings.AppSettings["FTP_Server_pass"];
        string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

        //variables 
        public string selectedFolder = "";
        public string picture_path = "";
        public string Doc_source = "";
        public int DepID = 0;

        string subject = Form_show_docs._subject;
        string Typee = Form_show_docs._BookType;
        string book_code = Form_show_docs._bookCode;

        private void EditeDocs_Load(object sender, EventArgs e)
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
                String file_name = DGV_Files.Rows[e.RowIndex].Cells[1].Value.ToString();
                Image image2 = Image.FromFile(Doc_source + @"\" + selectedFolder + @"\" + file_name + "");//put var here

                pictureBox1.Image = new Bitmap(image2);

                image2.Dispose();
                //get pah to use it to display img in wndows exploror
                picture_path = (Doc_source + @"\" + selectedFolder + @"\" + file_name + "");
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
                       


                        string fn = subject + file_name;

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip + Typee + "/" + book_code + "/" + fn);
                        request.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        using (Stream fileStream = File.OpenRead(file))

                        using (Stream ftpStream = request.GetRequestStream())
                        {
                            fileStream.CopyTo(ftpStream);

                        }

                        //delete imges after copy it 
                        if (File.Exists(file))
                        {
                            File.Delete(file);



                        }


                    }



                }
            }
            folder_update();

            string[] files = GetFileList();

            System.IO.DirectoryInfo di = new DirectoryInfo(path_folder_client_temp);

            foreach (FileInfo file in di.GetFiles())
            {
                ////////////important code/////////////
                //It allows us to delete when the file is used by the processor
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                //----------end-------------

                file.Delete();

            }

            foreach (string file in files)
            {

                Download(file);
            }


            var path = string.Format(path_folder_client_temp);

            //BookCode = advanc_dgv_view_data_doc.CurrentRow.Cells[0].Value.ToString();


            //refresh view list 
            Form_show_docs fshDocs = new Form_show_docs();
            fshDocs.Show();
            //fshDocs.PrefermCall();


            MessageBox.Show("تم ارفاق المستندات");
            this.Hide();
         
        }
        private string[] GetFileList()
        {


            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                //                                          Here we put the path IP and of the FTP file server
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"wared\cjs2\"));
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTP_ip + "/" + Typee + "/" + book_code+ "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();

                return result.ToString().Split('\n');

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }
        private void Download(string fileName)
        {

            FtpWebRequest reqFTP;
            try
            {

                //filePath = <<The full path where the file is to be created. the>>,
                //fileName = <<Name of the file to be createdNeed not name on FTP server. name name()>>
                FileStream outputStream = new FileStream(path_folder_client_temp + "\\" + fileName, FileMode.Create);
                //                                           Here we put the path IP, and file name of the FTP file server
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"wared\cjs2\" + fileName)); 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTP_ip + "/" + Typee+ "/" + book_code + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

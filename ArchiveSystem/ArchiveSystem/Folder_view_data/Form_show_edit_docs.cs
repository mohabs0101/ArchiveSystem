using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Data.SqlClient;

 
using System.Net;

namespace ArchiveSystem.Folder_view_data
{
    public partial class Form_show_edit_docs : MetroFramework.Forms.MetroForm
    {
        public static string _BookNumber;
        public static string _subject;
        public static string _BookType;
        public static string _BookCode;
        public static string photo_con_ = ConfigurationManager.ConnectionStrings["photo_con"].ConnectionString;
        SqlConnection photo_con = new SqlConnection(photo_con_);
        public static string Archived_by_department_name;//bring this when page load(source fromread_details_doc)

        public Form_show_edit_docs(string BookCode)
        {
            _BookCode = BookCode;
            InitializeComponent();
        }
        public static implicit operator Form_show_edit_docs(ScanDialog v)
        {
            throw new NotImplementedException();
        }
        public static string _con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con = new SqlConnection(_con);

        DataTable dt = new DataTable();
        SqlDataAdapter adapter;







        void Fill_bookType()
        {
            try
            {
                string query = string.Format(@" SELECT   [BooksTypeID]
      ,[BookTypeName]
  FROM [ArchiveSystem].[dbo].[BooksType_TBL]", con);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable booktypes = new DataTable();

                adp.Fill(booktypes);
                COM_bookType.DataSource = booktypes;
                COM_bookType.DisplayMember = "BookTypeName";
                COM_bookType.ValueMember = "BooksTypeID";

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        String fn_g;

        void check_Extension_file()
        {

            var Extension_file = Path.GetExtension(fn_g);


            if (Extension_file == ".docx")
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[0]); }
            else if (Extension_file == ".pptx") //بور بوينت"
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[1]); }
            else if (Extension_file == ".xlsx") //ملف اكسل
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[2]); }
            else if (Extension_file == ".accdb") //'ملف اكسس
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[3]); }
            else if (Extension_file == ".txt") //'ملف نصي
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[4]); }
            else if (Extension_file == ".pdf") //'ملف pdf
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[5]); }
            else if (Extension_file == ".mp4") //'فيديو
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[6]); }
            else if (Extension_file == ".rar") //'ملف مضغوط
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[8]); }
            else if (Extension_file == ".bnp" || Extension_file == ".bmp" || Extension_file == ".gif" || Extension_file == ".tif" || Extension_file == ".exe" || Extension_file == ".dll" || Extension_file == ".ico" || Extension_file == ".glp" || Extension_file == ".psd" || Extension_file == "." || Extension_file == ".xml" || Extension_file == ".html" || Extension_file == ".js" || Extension_file == ".css" || Extension_file == ".rar") //اخرى
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[7]); }
            else
            { ImageList_add_viwe.Images.Add(fn_g, Bitmap.FromFile(fn_g)); }
            //ImageList_add_viwe.Dispose();
        }


        public static string book_ID;
        string BookStatus;
        void read_details_doc()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  
dbo.ArchiveBooks_TBL.ArchiveBookID,
dbo.ArchiveBooks_TBL.BookCode,
dbo.ArchiveBooks_TBL.BookNumber,
dbo.ArchiveBooks_TBL.BookDate,
dbo.ArchiveBooks_TBL.InboundNumber,
dbo.ArchiveBooks_TBL.InboundDate,
dbo.ArchiveBooks_TBL.Subject,
dbo.BooksType_TBL.BookTypeName,
dbo.ArchiveBooks_TBL.[From],
dbo.ArchiveBooks_TBL.[To],
dbo.ArchiveBooks_TBL.SearchKeys,
dbo.ArchiveBooks_TBL.BookPriority,
dbo.ArchiveBooks_TBL.ArchivedDate,
dbo.ArchiveBooks_TBL.BookPaperType,
dbo.ArchiveBooks_TBL.Notes,
dbo.Departments_TBL.DepartmentName,
 
dbo.Users_TBL.Username,
dbo.ArchiveBooks_TBL.BookStatus,
dbo.ArchiveBooks_TBL.Privacy
  FROM ArchiveBooks_TBL INNER JOIN
                  dbo.Departments_TBL ON dbo.ArchiveBooks_TBL.DepartmentID_archivedBy = dbo.Departments_TBL.DepartmentID INNER JOIN
                  dbo.Users_TBL ON dbo.ArchiveBooks_TBL.UserID_archivedBy = dbo.Users_TBL.UserID INNER JOIN
                  dbo.BooksType_TBL ON dbo.ArchiveBooks_TBL.BooksTypeID = dbo.BooksType_TBL.BooksTypeID
WHERE  (dbo.ArchiveBooks_TBL.BookCode) = @Param1 ", con);

            cmd.Parameters.AddWithValue("@Param1", _BookCode);

            SqlDataReader dr1 = cmd.ExecuteReader();


            dr1.Read();
            if (dr1.HasRows)
            {
                book_ID = dr1["ArchiveBookID"].ToString();
                txt_book_code.Text = dr1["BookCode"].ToString();
                TXT_bookNumber.Text = dr1["BookNumber"].ToString();
                DT_bookDate.Text = dr1["BookDate"].ToString();
                TXT_Book_recive_number.Text = dr1["InboundNumber"].ToString();
                DT_bookRecive_date.Text = dr1["InboundDate"].ToString();
                TXT_Subject.Text = dr1["Subject"].ToString();
                COM_bookType.Text = dr1["BookTypeName"].ToString();
                TXT_From.Text = dr1["From"].ToString();
                TXT_To.Text = dr1[@"To"].ToString();
                TXT_SearchKEys.Text = dr1["SearchKeys"].ToString();
                COM_priority.Text = dr1["BookPriority"].ToString();
                txt_ArchivedDate.Text = dr1["ArchivedDate"].ToString();
                COM_PaperType.Text = dr1["BookPaperType"].ToString();
                TXT_notes.Text = dr1["Notes"].ToString();
                txt_DepartmentName.Text = dr1["DepartmentName"].ToString();
                txt_Username.Text = dr1["Username"].ToString();
                TXT_bookStatus.Text = dr1["BookStatus"].ToString();
                BookStatus = dr1["BookStatus"].ToString();
                COM_privicy.Text = dr1["Privacy"].ToString();

                _BookNumber = dr1["BookNumber"].ToString();
                _subject = TXT_Subject.Text;
                _BookType = COM_bookType.Text;

                Archived_by_department_name = txt_DepartmentName.Text;

            }
            dr1.Close();
            con.Close();
        }





        //-------------------code Ftp reade files-------------------------
        /////////Code to read and download FTP files from the server/////////////
        //You must first create a file on the server and set the FTP for it.To save files with

        //1-Read FTP File

        //This variables is Existing in a file App.config in the program
        string ftp_server_Ip = ConfigurationManager.AppSettings["FTP_Server_Ip"];
        string ftp_server_username = ConfigurationManager.AppSettings["FTP_Server_user"];
        string ftp_server_password = ConfigurationManager.AppSettings["FTP_Server_pass"];

        //The path of the file on the client computer with which we will download the files from the FTP file temporarily, and then we delete the downloaded files
        public static string path_folder_client_temp = "";//نسند لة قيمة في حدث 




        public string[] GetFileList()
        {


            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                //                                          Here we put the path IP and of the FTP file server
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"wared\cjs2\"));
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"\" + _BookType + @"\" + _BookCode + @"\"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftp_server_username, ftp_server_password);
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
                //if (result.Length==0)
                //{
                //    MessageBox.Show("هذا الكتاب لا يحتوي على مستندات");
                //}
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();

                return result.ToString().Split('\n');

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        //2-Download FTP Files
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
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"\" + _BookType + @"\" + _BookCode + @"\" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftp_server_username, ftp_server_password);
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

        void Read_FTP()
        {
            try
            {
                //اولا نحذف جميع الفولدرات المنزلة سابقا

                System.IO.DirectoryInfo di = new DirectoryInfo(ConfigurationManager.AppSettings["Path_Folder_Client_Temp"]);

                foreach (DirectoryInfo file in di.GetDirectories())
                {
                    ////////////important code/////////////
                    //It allows us to delete when the file is used by the processor
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    //----------end-------------

                    file.Delete(true);

                }

                //ثانيا في كل مرة نشاء فولدر باسم جديد لتفادي بقاء استخدام الملف من قبل المعالج
                string temp = "";
                temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"] + @"\Temp" + DateTime.Now.ToString("yyyyMMddhhmmss");
                Directory.CreateDirectory(temp);

                path_folder_client_temp = temp;

                //ثالثا نجل الصور
                string[] files = GetFileList();


                //ننزل الصور من السيرفر ftp
                if (files != null)
                {
                    foreach (string file in files)
                    {

                        Download(file);

                    }
                }



                var path = string.Format(path_folder_client_temp);
            }

            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //----------------end----------------------

        public void show_files_doc()
        {

            ListView_show_doc.Items.Clear();
            ImageList_add_viwe.Images.Clear();


            foreach (string file in System.IO.Directory.GetFiles(path_folder_client_temp))
            {

                fn_g = file;

                check_Extension_file();


                FileInfo fi = new FileInfo(fn_g);

                //var files_n = new List<String>();
                //files_n.Add(fi.FullName);

                ListView_show_doc.Items.Add(fi.Name, ImageList_add_viwe.Images.Count - 1);



            }

        }




        void fill_FollowUp()
        {

            SqlDataAdapter adp1 = new SqlDataAdapter(@"SELECT

             
        	 [dbo].[Departments_TBL]. DepartmentName as [ارسال الى قسم],
             [dbo].[ArchiveFollowUp].Task as [عنوان الطلب],
             [dbo].[ArchiveFollowUp]. Action as [الاجراء المتخذ],
             [dbo].[ArchiveFollowUp]. Note as [الملاحظات],
             [dbo].[Users_TBL].Username as [موظف الاضافة],
             [dbo].[ArchiveFollowUp]. DateAdded as [تاريخ اضافة],
             [dbo].[ArchiveFollowUp].FollowStatus as [الحالة النهائية]
             


                    FROM[ArchiveSystem].[dbo].[ArchiveFollowUp]
               inner JOIN[Departments_TBL] ON[ArchiveFollowUp].[Department_To_you_ID] = [Departments_TBL].[DepartmentID]

               inner JOIN[Users_TBL] ON[ArchiveFollowUp].[User_Add] = [Users_TBL].[UserID]
WHERE([ArchiveFollowUp].Department_From_me_ID = @Param1)
AND([ArchiveFollowUp].BookCode = @Param2)

          ", con);
            adp1.SelectCommand.Parameters.AddWithValue("@Param1", Login._depID);
            adp1.SelectCommand.Parameters.AddWithValue("@Param2", _BookCode);

            DataTable FollUp_dt = new DataTable();
            FollUp_dt.Clear();

            adp1.Fill(FollUp_dt);
            advanc_dgv_FollowUp.DataSource = FollUp_dt;
            label_count_send.Text = Convert.ToString(FollUp_dt.Rows.Count);
        }
        private void TabControlBookdetails_Selected(object sender, TabControlEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            fill_FollowUp();
            advanc_dgv_FollowUp.Columns[0].Width = 180;
            advanc_dgv_FollowUp.Columns[1].Width = 200;
            advanc_dgv_FollowUp.Columns[2].Width = 100;
            advanc_dgv_FollowUp.Columns[3].Width = 200;
            advanc_dgv_FollowUp.Columns[4].Width = 200;
            advanc_dgv_FollowUp.Columns[5].Width = 160;
            advanc_dgv_FollowUp.Columns[6].Width = 80;

            Cursor = Cursors.Default;

        }
        void Auto_complet_subject()
        //نظع هذا السب في حدث الفورم لود
        {
            try
            {
                string query = string.Format(@" SELECT [Subject] FROM [ArchiveSystem].[dbo].[ArchiveBooks_TBL]", con);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                AutoCompleteStringCollection ds = new AutoCompleteStringCollection();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ds.Add(dt.Rows[i][0].ToString());
                }
                this.TXT_Subject.AutoCompleteCustomSource = ds;
                this.TXT_Subject.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.TXT_Subject.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        void Auto_complet_from()
        //نظع هذا السب في حدث الفورم لود
        {
            try
            {
                string query = string.Format(@" SELECT [From] FROM [ArchiveSystem].[dbo].[ArchiveBooks_TBL]", con);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                AutoCompleteStringCollection ds = new AutoCompleteStringCollection();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ds.Add(dt.Rows[i][0].ToString());
                }
                this.TXT_From.AutoCompleteCustomSource = ds;
                this.TXT_From.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.TXT_From.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        void Auto_complet_to()


        //نظع هذا السب في حدث الفورم لود
        {
            try
            {
                string query = string.Format(@" SELECT [To] FROM [ArchiveSystem].[dbo].[ArchiveBooks_TBL]", con);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                AutoCompleteStringCollection ds = new AutoCompleteStringCollection();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ds.Add(dt.Rows[i][0].ToString());
                }
                this.TXT_To.AutoCompleteCustomSource = ds;
                this.TXT_To.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.TXT_To.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        private void Form_show_edit_docs_Load_1(object sender, EventArgs e)
        {
            //permition code
            if (Login._permitionTYpeID == 1)//admin
            {
                BTN_deletebook.Enabled = false;
            }
            else if (Login._permitionTYpeID == 8)//admin
            {
                BTN_deletebook.Enabled = true;
            }
            else
            {
                BTN_deletebook.Enabled = false;
                BTN_SAVE.Enabled = false;
                BTN_addMoreDcos.Enabled = false;
                TSM_delete.Enabled = false;
            }


            TabControlBookdetails.SelectTab(0);




            TabControlBookdetails.RightToLeft = RightToLeft.Yes;
            TabControlBookdetails.RightToLeftLayout = true;
            Fill_bookType();

            ListView_show_doc.Columns.Add("الملف", 300);
            cm_type_show.SelectedIndex = 1; //or ListView_show_doc.View = View.LargeIcon;
            ImageList_add_viwe.ImageSize = new Size(100, 100);
            ListView_show_doc.Columns[0].Width = 250;



            read_details_doc();
            Read_FTP();


            show_files_doc();
            Auto_complet_subject();

            Auto_complet_from();
            Auto_complet_to();

        }

        private void cm_type_show_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cm_type_show.SelectedIndex == 0)
            {
                ListView_show_doc.View = View.Details;
                ListView_show_doc.GridLines = true;
            }
            else if (cm_type_show.SelectedIndex == 1)
            { ListView_show_doc.View = View.LargeIcon; }
            else if (cm_type_show.SelectedIndex == 2)
            { ListView_show_doc.View = View.List; }
            else if (cm_type_show.SelectedIndex == 3)
            { ListView_show_doc.View = View.SmallIcon; }
            else if (cm_type_show.SelectedIndex == 4)
            { //ListView_show_doc.View = View.Tile
            }
        }


         

        string path_file_name;
        private void ListView_show_doc_SelectedIndexChanged_1(object sender, EventArgs e)
        {


            ListView.SelectedListViewItemCollection breakfast = this.ListView_show_doc.SelectedItems;

            foreach (ListViewItem Item in breakfast)
            {
                //or
                //for (int i = 0; i < ListView_show_doc.Items.Count; i++)
                //{
                try
                {
                    path_file_name = path_folder_client_temp + @"\" + ListView_show_doc.Items[Item.Index].SubItems[0].Text;

                    //pictureBox_show_doc.Load(pic);
                    //or


                    Image image2 = Image.FromFile(path_file_name);//put var here

                    pictureBox_show_doc.Image = new Bitmap(image2);

                    image2.Dispose();
                    //get pah to use it to display img in wndows exploror
                    //picture_path = (Doc_source + @"\" + selectedFolder + @"\" + file_name + "");



                }
                catch (Exception ex)
                {
                    System.Diagnostics.Process.Start(path_file_name);
                }
                //}
            }

        }

        private void TSM_open_file_Click_1(object sender, EventArgs e)
        {
            if (path_file_name == null)
            {
                MessageBox.Show("لاتوجد صوره لفتحها");
                return;
            }
            System.Diagnostics.Process.Start(path_file_name);
        }

        private void TSM_open_all_file_Click_1(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(path_folder_client_temp);
        }

        private void BTN_addMoreDcos_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditeDocs.AddDocs ed = new EditeDocs.AddDocs(_BookCode);
            ed.Show();

        }

        private void TSM_delete_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("هل انت متاكد بانك تريد حذف هذة المرفق", "تاكيد", buttons);
            if (result == DialogResult.No)
            {
                return;
            }
            ListView.SelectedListViewItemCollection breakfast = this.ListView_show_doc.SelectedItems;

            foreach (ListViewItem Item in breakfast)
            {
                path_file_name = path_folder_client_temp + @"\" + ListView_show_doc.Items[Item.Index].SubItems[0].Text;

                string fn = ListView_show_doc.Items[Item.Index].SubItems[0].Text;


                //------------

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp_server_Ip + _BookType + "/" + _BookCode + "/" + fn);
                request.Credentials = new NetworkCredential(ftp_server_username, ftp_server_password);
                //delete it from server
                request.Method = WebRequestMethods.Ftp.DeleteFile;



                FtpWebResponse response = (FtpWebResponse)request.GetResponse();


                photo_con.Open();


                string photoquery = String.Format(@"delete from [dbo].[ArchiveImagesTBL]
                                                                      where [bookCode]='{0}' and [filename]='{1}' ", _BookCode,fn);

                SqlCommand photo_cmd = new SqlCommand(photoquery, photo_con);

                //photo_cmd.Parameters.AddWithValue("bookcode", _BookCode);
                //photo_cmd.Parameters.AddWithValue("filename", fn);

 
                photo_cmd.ExecuteNonQuery();
                photo_con.Close();





                MessageBox.Show("تم حذف المستند");
                response.Close();

                //delete it localy
                if (File.Exists(path_file_name))
                {
                    //ListView_show_doc.Dispose();
                    //ImageList_add_viwe.Dispose();
                    //Image img = new Bitmap(pic);
                    //pictureBox_show_doc.Image = img.GetThumbnailImage(350, 350, null, new IntPtr());
                    //pictureBox_show_doc.Dispose();

                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    //pictureBox_show_doc.Dispose();

                    File.Delete(path_file_name);
                    //refresh listview
                    show_files_doc();
                    // Form_show_docs s_doc1 = new Form_show_docs();
                    //s_doc1.Show();

                }




            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            ListView_show_doc.Items.Clear();
            ImageList_add_viwe.Images.Clear();



            foreach (string file in System.IO.Directory.GetFiles(path_folder_client_temp))
            {

                fn_g = file;

                check_Extension_file();


                FileInfo fi = new FileInfo(fn_g);

                //var files_n = new List<String>();
                //files_n.Add(fi.FullName);

                ListView_show_doc.Items.Add(fi.Name, ImageList_add_viwe.Images.Count - 1);



            }
        }

        private void BTN_EnableEdite_Click(object sender, EventArgs e)
        {

            TXT_bookNumber.Enabled = true;
            //COM_bookType.Enabled = true;
            TXT_Subject.Enabled = true;
            DT_bookDate.Enabled = true;
            COM_privicy.Enabled = true;
            TXT_From.Enabled = true;
            TXT_To.Enabled = true;
            TXT_Book_recive_number.Enabled = true;
            DT_bookRecive_date.Enabled = true;
            COM_priority.Enabled = true;
            COM_PaperType.Enabled = true;
            //COM_bookStatus.Enabled = true;
            TXT_notes.Enabled = true;
            TXT_SearchKEys.Enabled = true;

            BTN_SAVE.Visible = true;
            BTN_deletebook.Visible = true;
            BTN_StopEditing.Visible = true;
            this.BTN_EnableEdite.Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            TXT_bookNumber.Enabled = false;
            //COM_bookType.Enabled = false;
            TXT_Subject.Enabled = false;
            DT_bookDate.Enabled = false;
            COM_privicy.Enabled = false;
            TXT_From.Enabled = false;
            TXT_To.Enabled = false;
            TXT_Book_recive_number.Enabled = false;
            DT_bookRecive_date.Enabled = false;
            COM_priority.Enabled = false;
            COM_PaperType.Enabled = false;
            TXT_bookStatus.Enabled = false;
            TXT_notes.Enabled = false;
            TXT_SearchKEys.Enabled = false;
            BTN_SAVE.Visible = false;
            BTN_deletebook.Visible = false;
            this.BTN_EnableEdite.Visible = true;
            this.BTN_StopEditing.Visible = false;
        }



        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string BookNumber = TXT_bookNumber.Text;

            string Subject = TXT_Subject.Text;
            string From = TXT_From.Text;
            string To = TXT_To.Text;
            string SearchKeys = TXT_SearchKEys.Text;




            if (string.IsNullOrWhiteSpace(BookNumber) || string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(From) || string.IsNullOrWhiteSpace(To) || string.IsNullOrWhiteSpace(SearchKeys))
            {
                List<TextBox> boxes = new List<TextBox>();
                if (string.IsNullOrWhiteSpace(TXT_bookNumber.Text))
                {

                    boxes.Add(TXT_bookNumber);
                }
                else { TXT_bookNumber.BackColor = Color.White; }


                if (string.IsNullOrWhiteSpace(TXT_Subject.Text))
                {

                    boxes.Add(TXT_Subject);
                }
                else { TXT_Subject.BackColor = Color.White; }
                if (string.IsNullOrWhiteSpace(TXT_From.Text))
                {

                    boxes.Add(TXT_From);
                }
                else { TXT_From.BackColor = Color.White; }

                if (string.IsNullOrWhiteSpace(TXT_To.Text))
                {

                    boxes.Add(TXT_To);
                }
                else { TXT_To.BackColor = Color.White; }
                if (string.IsNullOrWhiteSpace(TXT_SearchKEys.Text))
                {

                    boxes.Add(TXT_SearchKEys);
                }
                else { TXT_SearchKEys.BackColor = Color.White; }

                foreach (var item in boxes)
                {
                    if (string.IsNullOrWhiteSpace(item.Text))
                    {
                        item.BackColor = Color.LightSalmon;
                    }
                }

                MessageBox.Show("الرجاء مليء جميع الحقول");



            }

            else
            {

                String strQuery;
                con.Open();

                strQuery = @"Update ArchiveBooks_TBL set BookNumber = @BookNumber, BookDate = @BookDate, " +
                    "InboundNumber = @InboundNumber, InboundDate = @InboundDate, Subject = @Subject, [From] = @From, [To] = @To," +

                "SearchKeys = @SearchKeys, BookPriority = @BookPriority, BookPaperType = @BookPaperType, Notes = @Notes, Privacy = @Privacy" +
                    " Where ArchiveBookID = " + book_ID;

                SqlCommand cmd = new SqlCommand(strQuery, con);


                string DT_bookDate_ = DT_bookDate.Text;
                DateTime DT_bookDatee = Convert.ToDateTime(DT_bookDate_);
                string _DT_bookDatee = DT_bookDatee.ToString("yyyy/MM/dd");

                //اذا كان رقم واردنا فارغ نقوم بادخال تاريخ واردنا
                string InboundDate = "";
                if (TXT_Book_recive_number.Text == "")
                {
                    InboundDate = "";
                }
                else
                {
                    string InboundDate_ = DT_bookRecive_date.Text;
                    DateTime oDate = Convert.ToDateTime(InboundDate_);
                    InboundDate = oDate.ToString("yyyy/MM/dd");
                }




                cmd.Parameters.Add(new SqlParameter("@BookNumber", SqlDbType.NVarChar)).Value = TXT_bookNumber.Text;
                cmd.Parameters.Add(new SqlParameter("@BookDate", SqlDbType.NVarChar)).Value = _DT_bookDatee;
                cmd.Parameters.Add(new SqlParameter("@InboundNumber", SqlDbType.NVarChar)).Value = TXT_Book_recive_number.Text;
                cmd.Parameters.Add(new SqlParameter("@InboundDate", SqlDbType.NVarChar)).Value = InboundDate;
                cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar)).Value = TXT_Subject.Text;
                cmd.Parameters.Add(new SqlParameter("@From", SqlDbType.NVarChar)).Value = TXT_From.Text;
                cmd.Parameters.Add(new SqlParameter("@To", SqlDbType.NVarChar)).Value = TXT_To.Text;
                cmd.Parameters.Add(new SqlParameter("@SearchKeys", SqlDbType.NVarChar)).Value = TXT_SearchKEys.Text;
                cmd.Parameters.Add(new SqlParameter("@BookPriority", SqlDbType.NVarChar)).Value = COM_priority.Text;
                cmd.Parameters.Add(new SqlParameter("@BookPaperType", SqlDbType.NVarChar)).Value = COM_PaperType.Text;
                cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar)).Value = TXT_notes.Text;
                cmd.Parameters.Add(new SqlParameter("@Privacy", SqlDbType.NVarChar)).Value = COM_privicy.Text;

                cmd.ExecuteNonQuery();




                con.Close();
                button5_Click(null, null);

                Cursor = Cursors.WaitCursor;
                Form_view_data_dqv FVD = new Form_view_data_dqv();

                FVD.fill_dgv_view_data_doc();

            }


            Cursor = Cursors.Default;

        }




        private void TXT_Book_recive_number_TextChanged(object sender, EventArgs e)
        {
            {
                if (TXT_Book_recive_number.Text == "")
                {
                    panel_viwe_DT_bookRecive_date.Visible = true;
                    label_check_input_DT_bookRecive_date.Visible = false;
                }
                else
                {
                    panel_viwe_DT_bookRecive_date.Visible = false;
                    label_check_input_DT_bookRecive_date.Visible = true;
                }
            }
        }







        //---- code Zoom and Rotate------

        private void ZoomInOut(bool zoom)
        {
            try
            {
                //Zoom ratio by which the images will be zoomed by default
                int zoomRatio = 10;
                //Set the zoomed width and height
                int widthZoom = pictureBox_show_doc.Width * zoomRatio / 100;
                int heightZoom = pictureBox_show_doc.Height * zoomRatio / 100;
                //zoom = true --> zoom in
                //zoom = false --> zoom out
                if (!zoom)
                {
                    widthZoom *= -1;
                    heightZoom *= -1;
                }
                //Add the width and height to the picture box dimensions
                pictureBox_show_doc.Width += widthZoom;
                pictureBox_show_doc.Height += heightZoom;
            }
            catch (Exception ex)
            {

            }

        }

        private void btn_zoom_out_Click_1(object sender, EventArgs e)
        {
            ZoomInOut(false);
        }

        private void btn_zoom_in_Click_1(object sender, EventArgs e)
        {
            ZoomInOut(true);
        }
        void Rotat_img(RotateFlipType r90)
        {
            try
            {
                Image img = pictureBox_show_doc.Image;
                img.RotateFlip(r90);
                pictureBox_show_doc.Image = img;
            }
            catch (Exception ex)
            {

            }
        }
        private void btn_Rotate_180_Click(object sender, EventArgs e)
        {
            Rotat_img(RotateFlipType.Rotate180FlipNone);
        }

        private void btn_Rotate_90_Click(object sender, EventArgs e)
        {
            Rotat_img(RotateFlipType.Rotate90FlipNone);

        }

        // -- Zoom Mouse
        protected override void OnMouseWheel(MouseEventArgs ea)
        {
            //  flag = 1;
            // Override OnMouseWheel event, for zooming in/out with the scroll wheel
            if (pictureBox_show_doc.Image != null)
            {
                // If the mouse wheel is moved forward (Zoom in)
                if (ea.Delta > 0)
                {
                    // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                    if ((pictureBox_show_doc.Width < (15 * this.Width)) && (pictureBox_show_doc.Height < (15 * this.Height)))
                    {
                        // Change the size of the picturebox, multiply it by the ZOOMFACTOR
                        pictureBox_show_doc.Width = (int)(pictureBox_show_doc.Width * 1.25);
                        pictureBox_show_doc.Height = (int)(pictureBox_show_doc.Height * 1.25);

                        // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                        pictureBox_show_doc.Top = (int)(ea.Y - 1.25 * (ea.Y - pictureBox_show_doc.Top));
                        pictureBox_show_doc.Left = (int)(ea.X - 1.25 * (ea.X - pictureBox_show_doc.Left));
                    }
                }
                else
                {
                    // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                    if ((pictureBox_show_doc.Width > (panel16.Width)) && (pictureBox_show_doc.Height > (panel16.Height)))
                    {
                        // Change the size of the picturebox, divide it by the ZOOMFACTOR
                        pictureBox_show_doc.Width = (int)(pictureBox_show_doc.Width / 1.25);
                        pictureBox_show_doc.Height = (int)(pictureBox_show_doc.Height / 1.25);

                        // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                        pictureBox_show_doc.Top = (int)(ea.Y - 0.80 * (ea.Y - pictureBox_show_doc.Top));
                        pictureBox_show_doc.Left = (int)(ea.X - 0.80 * (ea.X - pictureBox_show_doc.Left));
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void BTN_deletebook_Click(object sender, EventArgs e)
        {
          
            

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("هل انت متاكد بانك تريد حذف هذة الكتاب", "تاكيد", buttons);
            if (result == DialogResult.No)
            {
                return;
            }
            //check if there is followup before deleting book
            string followup_query = string.Format(@" SELECT   [BookCode]  
        
         FROM [ArchiveSystem].[dbo].[ArchiveFollowUp] where [BookCode]='{0}'", _BookCode, con);

            con.Open();
            SqlCommand cmdf = new SqlCommand(followup_query, con);

            SqlDataAdapter adpf = new SqlDataAdapter(cmdf);

            DataTable dtf = new DataTable();

            adpf.Fill(dtf);
            con.Close();
            if (dtf.Rows.Count > 0)
            {
                MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;
                DialogResult result1 = MessageBox.Show("سيتم حذف جميع متابعات الكتاب", "تاكيد", buttons1);
                if (result1 == DialogResult.No)
                {
                    return;
                }
               var targetBookcode=dtf.Rows[0][0].ToString();

                string queryd = string.Format(@"delete FROM [ArchiveSystem].[dbo].[ArchiveFollowUp] where [BookCode]=N'{0}'", targetBookcode);

                con.Open();
                SqlCommand cmdd = new SqlCommand(queryd, con);
                
                int excuteDel_result = (int)cmdd.ExecuteNonQuery();
                
                if (excuteDel_result == 0)
                {
                    MessageBox.Show("حدث خطاء اثناء حذف متابعات الكتاب  ");
                    return;
                }
                else if (excuteDel_result !=0)
                {
                    MessageBox.Show("تم حذف متابعات الكتاب بنجاح ");

                  



                }
                con.Close();




            }

            if (ListView_show_doc.Items.Count != 0)//if the book exist on ftp and sql 
            {
                //delete files (imgs) from server ftp
                for (int i = 0; i < ListView_show_doc.Items.Count; i++)
                {

                    string fn = ListView_show_doc.Items[i].SubItems[0].Text;
                    // then you can access properties of item.PropertyName
                    FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create(ftp_server_Ip + _BookType + "/" + _BookCode + "/" + fn);
                    request1.Credentials = new NetworkCredential(ftp_server_username, ftp_server_password);
                    request1.Method = WebRequestMethods.Ftp.DeleteFile;
                    FtpWebResponse response1 = (FtpWebResponse)request1.GetResponse();
                    response1.Close();


                



                }
                MessageBox.Show("تم حذف مرفقات الكتاب");

                //delete the directry of files from ftp server 
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp_server_Ip + _BookType + "/" + _BookCode);
                request.Credentials = new NetworkCredential(ftp_server_username, ftp_server_password);
                request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                MessageBox.Show("تم حذف المجلد الرئيسي");
                //delete files images from local path
                for (int i = 0; i < ListView_show_doc.Items.Count; i++)
                {
                    path_file_name = path_folder_client_temp + @"\" + ListView_show_doc.Items[i].SubItems[0].Text;

                    if (File.Exists(path_file_name))
                    {
                        //ListView_show_doc.Dispose();
                        //ImageList_add_viwe.Dispose();
                        //Image img = new Bitmap(pic);
                        //pictureBox_show_doc.Image = img.GetThumbnailImage(350, 350, null, new IntPtr());
                        //pictureBox_show_doc.Dispose();

                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        //pictureBox_show_doc.Dispose();

                        File.Delete(path_file_name);
                        //refresh listview

                        // Form_show_docs s_doc1 = new Form_show_docs();
                        //s_doc1.Show();

                    }
                }
                //refresh viewlist 
                show_files_doc();
                //delete the book from sql 
                string bookCode = txt_book_code.Text;

                string query = string.Format(@"delete FROM [ArchiveSystem].[dbo].[ArchiveBooks_TBL] where [BookCode]=N'{0}'", bookCode);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                //int excute_result = 0;
               int excute_result = (int)cmd.ExecuteNonQuery();
                if (excute_result == 0)
                {
                    MessageBox.Show("حدث خطاء,لم يتم حذف الكتاب من قاعدة البيانات");
                    return;

                }
                else if (excute_result != 0)
                {
                    MessageBox.Show("تم حذف الكتاب من قاعدة البيانات ");

                    //deleting photos from db 
                    photo_con.Open();

                    string photoquery = String.Format(@"delete from [dbo].[ArchiveImagesTBL]  where bookCode='{0}'  ", bookCode);


                    SqlCommand photo_cmd = new SqlCommand(photoquery, photo_con);

                    photo_cmd.ExecuteNonQuery();
                    photo_con.Close();

                }
                con.Close();
                this.Hide();
            }
            else//if the book exis only on sql server 
            {   //delete the directry of files from ftp server 
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp_server_Ip + _BookType + "/" + _BookCode);
                request.Credentials = new NetworkCredential(ftp_server_username, ftp_server_password);
                request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                MessageBox.Show("تم حذف المجلد الرئيسي");
                //delete book from sql server 
                string bookCode = txt_book_code.Text;

                string query = string.Format(@"delete FROM [ArchiveSystem].[dbo].[ArchiveBooks_TBL] where [BookCode]=N'{0}'", bookCode);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int excute_result = 0;
                excute_result = cmd.ExecuteNonQuery();
                if (excute_result == 0)
                {
                    MessageBox.Show("حدث خطيء,لم يتم حذف الكتاب من قاعدة البيانات");

                }
                else if (excute_result == 1)
                {
                    MessageBox.Show("تم حذف الكتاب من قاعدة البيانات ");
                }
                con.Close();

                this.Hide();

            }
        }



    }
}



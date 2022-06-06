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
    public partial class Form_show_docs : MetroFramework.Forms.MetroForm
    {
        public static string _BookNumber;
        public static string _subject;
        public static string _BookType;
        public static string _BookCode;

        public static string Archived_by_department_name;//bring this when page load(source fromread_details_doc)

        public Form_show_docs(string BookCode)
        {
            _BookCode = BookCode;
            InitializeComponent();
        }
        public static implicit operator Form_show_docs(ScanDialog v)
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
            Cursor = Cursors.WaitCursor;
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
            Cursor = Cursors.Default;
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


        private void Form_show_docs_Load_1(object sender, EventArgs e)
        {
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
            fill_FollowUp();

            advanc_dgv_FollowUp.Columns[0].Width = 180;
            advanc_dgv_FollowUp.Columns[1].Width = 200;
            advanc_dgv_FollowUp.Columns[2].Width = 100;
            advanc_dgv_FollowUp.Columns[3].Width = 200;
            advanc_dgv_FollowUp.Columns[4].Width = 200;
            advanc_dgv_FollowUp.Columns[5].Width = 160;
            advanc_dgv_FollowUp.Columns[6].Width = 80;
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

        private void btn_add_Tracker_Click_1(object sender, EventArgs e)
        {
           
        }

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
            EditeDocs.EditeDocs ed = new EditeDocs.EditeDocs(_BookCode);
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
             
                String strQuery ;
            con.Open();

            strQuery = @"Update ArchiveBooks_TBL set BookNumber = @BookNumber, BookDate = @BookDate, " +
                "InboundNumber = @InboundNumber, InboundDate = @InboundDate, Subject = @Subject, [From] = @From, [To] = @To," +
                
            "SearchKeys = @SearchKeys, BookPriority = @BookPriority, BookPaperType = @BookPaperType, Notes = @Notes, Privacy = @Privacy" +
                " Where ArchiveBookID = " + book_ID;
                
            SqlCommand cmd = new SqlCommand(strQuery, con);


                string DT_bookDate_= DT_bookDate.Text;
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


        //-///////////////start code FollowUp//////////////////
       
        //i have login dep id and i have selected book archived dep id 
        // if they matched so it mean the book i opened is created by same dep so bring full assignation table 
        // else if not matchet ether the book i opened assing to me or not 
        //so i will bring the rows that have [Department_AssignTO_ID] as my login dep id 
        // if it return empty it means there is no assign to me in it
        void BringFolloWUp_TBL()
        {
            int bookID = Convert.ToInt32(book_ID); //book id of selected book

            int departmentID = Login._depID;//login dep id

            //bring archiveByDepID by its name 
            string query2 = string.Format(@"SELECT   [DepartmentID]  ,[DepartmentName]
    
  FROM [ArchiveSystem].[dbo].[Departments_TBL] where [DepartmentName]=N'{0}'", Archived_by_department_name, con);

            con.Open();
            SqlCommand cmd2 = new SqlCommand(query2, con);

            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);

            DataTable dt2 = new DataTable();
            con.Close();
            adp2.Fill(dt2);
            int DepArch_by_ID_ = Convert.ToInt32(dt2.Rows[0][0].ToString());

            int archived_by_bookID = DepArch_by_ID_; //book archived by id 

            if (departmentID == archived_by_bookID)
            {

                string query3 = string.Format(@"SELECT  [ArchiveFollowUpID]
      ,[ArchiveBookID]
      ,[Department_To_you_ID]
      ,[Task] as 'المهمة'
      ,[DepartmentName] as 'القسم'
      ,[Action] as 'الاجراء المتخذم'
      ,[Note] as 'ملاحضة'
      ,[DateAdded] as 'تاريخ الاضافة'
  FROM [ArchiveSystem].[dbo].[ArchiveFollowUp] INNER JOIN
                  dbo.Departments_TBL ON dbo.ArchiveFollowUp.Department_To_you_ID = dbo.Departments_TBL.DepartmentID 
where ArchiveBookID={0}", bookID, con);

                con.Open();
                SqlCommand cmd3 = new SqlCommand(query3, con);

                SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);

                DataTable dt3 = new DataTable();
                con.Close();
                adp3.Fill(dt3);
                advanc_dgv_FollowUp.DataSource = dt3;

                advanc_dgv_FollowUp.Columns[0].Visible = false;
                advanc_dgv_FollowUp.Columns[1].Visible = false;
                advanc_dgv_FollowUp.Columns[2].Visible = false;
            }
            else if (departmentID != archived_by_bookID) // it means the book i opened not created by my department
            {
                string query = string.Format(@"SELECT 
       [ArchiveFollowUpID]
      ,[ArchiveBookID]
      ,[Department_AssignTO_ID]
      ,[Task] as 'المهمة'
      ,[DepartmentName] as 'القسم'
      ,[Action] as 'الاجراء المتخذم'
      ,[Note] as 'ملاحضة'
      ,[DateAdded] as 'تاريخ الاضافة'
  FROM [ArchiveSystem].[dbo].[ArchiveFollowUp] INNER JOIN
                  dbo.Departments_TBL ON dbo.ArchiveFollowUp.Department_AssignTO_ID = dbo.Departments_TBL.DepartmentID 
   where [Department_AssignTO_ID]={0} and [ArchiveBookID]={1} ", departmentID, bookID, con);




                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                con.Close();
                adp.Fill(dt);
                advanc_dgv_FollowUp.DataSource = dt;

                advanc_dgv_FollowUp.Columns[0].Visible = false;
                advanc_dgv_FollowUp.Columns[1].Visible = false;
                advanc_dgv_FollowUp.Columns[2].Visible = false;
                
            }


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

        
    }
}

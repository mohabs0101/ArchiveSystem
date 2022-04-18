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
        public static string _subject;
        public static string _BookType;
        public static string _bookCode;
        public static string Archived_by_department_name;//bring this when page load(source fromread_details_doc)

        public Form_show_docs()
        {
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

        void Select_Departments()
        {
            try
            {
                string query = string.Format(@"  
SELECT  [DepartmentID]
      ,[DepartmentName]
  FROM [ArchiveSystem].[dbo].[Departments_TBL]", con);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dep = new DataTable();

                adp.Fill(dep);
                COMLIST_assination.DataSource = dep;
                COMLIST_assination.DisplayMember = "DepartmentName";
                COMLIST_assination.ValueMember = "DepartmentID";

                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       

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
            else if (Extension_file == ".bnp" || Extension_file == ".bmp" || Extension_file == ".gif" || Extension_file == ".tif" || Extension_file == ".exe" || Extension_file == ".dll" || Extension_file == ".ico" || Extension_file == ".glp" || Extension_file == ".psd" || Extension_file == "." || Extension_file == ".xml" || Extension_file == ".html" || Extension_file == ".js" || Extension_file == ".css") //اخرى
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

            cmd.Parameters.AddWithValue("@Param1", Form_view_data_dqv.BookCode);

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

 
                _subject = TXT_Subject.Text;
                _BookType = COM_bookType.Text;
                _bookCode = txt_book_code.Text;
                Archived_by_department_name = txt_DepartmentName.Text;

            }
            dr1.Close();
            con.Close();
        }

  //      void Bring_assignTable()
  //      {
  //          try
  //          {
               
  //              int Login_dep =Convert.ToInt32( Login._depID.ToString());
  //              int book_archivedBy_depID = Convert.ToInt32(book_ID.ToString());
  //              string query = string.Format(@" SELECT   [BooksTypeID]
  //    ,[BookTypeName]
  //FROM [ArchiveSystem].[dbo].[BooksType_TBL]", con);

  //              con.Open();
  //              SqlCommand cmd = new SqlCommand(query, con);

  //              SqlDataAdapter adp = new SqlDataAdapter(cmd);

  //              DataTable booktypes = new DataTable();

  //              adp.Fill(booktypes);
  //              COM_bookType.DataSource = booktypes;
  //              COM_bookType.DisplayMember = "BookTypeName";
  //              COM_bookType.ValueMember = "BooksTypeID";

  //              con.Close();

  //          }
  //          catch (Exception ex)
  //          {
  //              MessageBox.Show(ex.ToString());
  //          }

  //      }
        //public void PrefermCall()
        //{
        //    show_files_doc();

        //}
        public void show_files_doc()
        {
            ListView_show_doc.Items.Clear();
            ImageList_add_viwe.Images.Clear();

            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

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
            show_files_doc();
            Select_Departments();

            BringFolloWUp_TBL();

            //Bring_assignTable();


            advanc_dgv_Assign_Comment.Columns[3].Width = 200;
            advanc_dgv_Assign_Comment.Columns[4].Width = 150;
            advanc_dgv_Assign_Comment.Columns[5].Width = 90;
            advanc_dgv_Assign_Comment.Columns[6].Width = 200;


            tSComBox_FollowUp_type.SelectedIndex = 0;



            if (BookStatus == "مكتمل")
            {
                btn_edit_status_FollowUp.Text = "اضافة الى المتابعه"; 
            }
            else
            {
                btn_edit_status_FollowUp.Text = "ازالة من المتابعه";
            }
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
            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

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
            System.Diagnostics.Process.Start(path_file_name);
        }

        private void TSM_open_all_file_Click_1(object sender, EventArgs e)
        {
            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];
            System.Diagnostics.Process.Start(path_folder_client_temp);
        }

        private void BTN_addMoreDcos_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditeDocs.EditeDocs ed = new EditeDocs.EditeDocs();
            ed.Show();

        }

        private void TSM_delete_Click(object sender, EventArgs e)
        {
            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];
            ListView.SelectedListViewItemCollection breakfast = this.ListView_show_doc.SelectedItems;

            foreach (ListViewItem Item in breakfast)
            {
                path_file_name = path_folder_client_temp + @"\" + ListView_show_doc.Items[Item.Index].SubItems[0].Text;

                string fn = ListView_show_doc.Items[Item.Index].SubItems[0].Text;


                //------------
                string FTP_ip = ConfigurationSettings.AppSettings["FTP_Server_Ip"];
                string FTP_user = ConfigurationSettings.AppSettings["FTP_Server_user"];
                string FTP_pass = ConfigurationSettings.AppSettings["FTP_Server_pass"];
                string type = COM_bookType.Text;
                string bookcode = txt_book_code.Text;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip + type + "/" + bookcode + "/" + fn);
                request.Credentials = new NetworkCredential(FTP_user, FTP_pass);
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

            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

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
            string BookNumber = TXT_bookNumber.Text;
            string InboundNumber = TXT_Book_recive_number.Text;

            string Subject = TXT_Subject.Text;
            string From = TXT_From.Text;
            string To = TXT_To.Text;
            string SearchKeys = TXT_SearchKEys.Text;
            if (string.IsNullOrWhiteSpace(BookNumber) || string.IsNullOrWhiteSpace(InboundNumber) || string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(From) || string.IsNullOrWhiteSpace(To) || string.IsNullOrWhiteSpace(SearchKeys))
            {
                List<TextBox> boxes = new List<TextBox>();
                if (string.IsNullOrWhiteSpace(TXT_bookNumber.Text))
                {

                    boxes.Add(TXT_bookNumber);
                }
                else { TXT_bookNumber.BackColor = Color.White; }
                if (string.IsNullOrWhiteSpace(TXT_Book_recive_number.Text))
                {
                    boxes.Add(TXT_Book_recive_number);
                }
                else { TXT_Book_recive_number.BackColor = Color.White; }

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
                
            "SearchKeys = @SearchKeys, BookPriority = @BookPriority, BookPaperType = @BookPaperType, Notes = @Notes, BookStatus = @BookStatus, Privacy = @Privacy" +
                " Where ArchiveBookID = " + book_ID;
                
            SqlCommand cmd = new SqlCommand(strQuery, con);

            cmd.Parameters.Add(new SqlParameter("@BookNumber", SqlDbType.NVarChar)).Value = TXT_bookNumber.Text;
            cmd.Parameters.Add(new SqlParameter("@BookDate", SqlDbType.NVarChar)).Value = DT_bookDate.Text;
            cmd.Parameters.Add(new SqlParameter("@InboundNumber", SqlDbType.NVarChar)).Value = TXT_Book_recive_number.Text;
            cmd.Parameters.Add(new SqlParameter("@InboundDate", SqlDbType.NVarChar)).Value = DT_bookRecive_date.Text;
            cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar)).Value = TXT_Subject.Text;
            cmd.Parameters.Add(new SqlParameter("@From", SqlDbType.NVarChar)).Value = TXT_From.Text;
            cmd.Parameters.Add(new SqlParameter("@To", SqlDbType.NVarChar)).Value = TXT_To.Text;
            cmd.Parameters.Add(new SqlParameter("@SearchKeys", SqlDbType.NVarChar)).Value = TXT_SearchKEys.Text;
            cmd.Parameters.Add(new SqlParameter("@BookPriority", SqlDbType.NVarChar)).Value = COM_priority.Text;
            cmd.Parameters.Add(new SqlParameter("@BookPaperType", SqlDbType.NVarChar)).Value = COM_PaperType.Text;
            cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar)).Value = TXT_notes.Text;
            cmd.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = TXT_bookStatus.Text;
            cmd.Parameters.Add(new SqlParameter("@Privacy", SqlDbType.NVarChar)).Value = COM_privicy.Text;

            cmd.ExecuteNonQuery();




            con.Close();
            button5_Click(null, null);

                Form_view_data_dqv FVD = new Form_view_data_dqv();

                FVD.fill_dgv_view_data_doc();
              
            }




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
      ,[Department_AssignTO_ID]
      ,[Task]as 'المهمة'
      ,[DepartmentName]as 'القسم'
      ,[Action]as 'الاجراء المتخذ'
      ,[Note]as 'ملاحضة'
      ,[DateAdded]as 'تاريخ الاضافة'
  FROM [ArchiveSystem].[dbo].[ArchiveFollowUp] INNER JOIN
                  dbo.Departments_TBL ON dbo.ArchiveFollowUp.Department_AssignTO_ID = dbo.Departments_TBL.DepartmentID 
where ArchiveBookID={0}", bookID, con);

                con.Open();
                SqlCommand cmd3 = new SqlCommand(query3, con);

                SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);

                DataTable dt3 = new DataTable();
                con.Close();
                adp3.Fill(dt3);
                advanc_dgv_Assign_Comment.DataSource = dt3;

                advanc_dgv_Assign_Comment.Columns[0].Visible = false;
                advanc_dgv_Assign_Comment.Columns[1].Visible = false;
                advanc_dgv_Assign_Comment.Columns[2].Visible = false;
            }
            else if (departmentID != archived_by_bookID) // it means the book i opened not created by my department
            {
                string query = string.Format(@"SELECT 
       [ArchiveFollowUpID]
      ,[ArchiveBookID]
      ,[Department_AssignTO_ID]
      ,[Task] as 'المهمة'
      ,[DepartmentName]as 'القسم'
      ,[Action] as 'الاجراء المتخذ'
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
                advanc_dgv_Assign_Comment.DataSource = dt;

                advanc_dgv_Assign_Comment.Columns[0].Visible = false;
                advanc_dgv_Assign_Comment.Columns[1].Visible = false;
                advanc_dgv_Assign_Comment.Columns[2].Visible = false;

                TXT_assignTitle.Enabled = false;
                COMLIST_assination.Enabled = false;
                BTN_addTask.Enabled = false;



            }


        }

        private void BTN_addTask_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TXT_assignTitle.Text))
            {
                List<TextBox> boxes = new List<TextBox>();
                if (string.IsNullOrWhiteSpace(TXT_assignTitle.Text))
                {

                    boxes.Add(TXT_assignTitle);
                }
                else {TXT_assignTitle.BackColor = Color.White; }
                foreach (var item in boxes)
                {
                    if (string.IsNullOrWhiteSpace(item.Text))
                    {
                        item.BackColor = Color.LightSalmon;
                    }
                }
                MessageBox.Show("يجب ادخال نص", "لم يتم ");
                return;
            }

            if (COMLIST_assination.CheckedItems.Count != 0)
            { }

            else
            {
                MessageBox.Show("يجب الاشارة الى قسم واحد على الاقل وذالك من خلال وضع علامة صح", "لم يتم ");
                return;
            }

                try
            {
                int Archive_bookID = Convert.ToInt32(book_ID.ToString());
                int Department_assintToID = Convert.ToInt32(COMLIST_assination.SelectedValue.ToString());
                string task = TXT_assignTitle.Text;
                string currentDate = DateTime.Now.ToString("yyyy/MM/dd");

                string query = string.Format(@"INSERT INTO [dbo].[ArchiveFollowUp]
           ([ArchiveBookID]
           ,[Department_AssignTO_ID]
           ,[Task]
           ,[Action]
           ,[Note]
           ,[DateAdded]
          )
     VALUES
           ({0},{1},N'{2}','{3}','{4}','{5}')
", Archive_bookID, Department_assintToID, task, "انتظار الاجراء", "", currentDate, con);




                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int check = (int)cmd.ExecuteNonQuery();


                //final status to doc
                String strQuery = @"Update ArchiveBooks_TBL set BookStatus = @BookStatus Where ArchiveBookID = " + book_ID;

                SqlCommand cmd1 = new SqlCommand(strQuery, con);

                cmd1.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = "قيد المتابعة";
               
                cmd1.ExecuteNonQuery();


                con.Close();

                BringFolloWUp_TBL();
              

                if (check != 0)
                {
                    TXT_assignTitle.Text = "";
                   
                    MessageBox.Show("تم اضافة متابعة ");

                }

                else if (check == 0)
                {
                    MessageBox.Show("لم يتم ادخال المعلومات ");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void tSMenuItem_FollowUp_Save_Click(object sender, EventArgs e)
        {
            con.Open();
          
            String strQuery = @"Update ArchiveFollowUp set Action = @Action, Note = @Note Where ArchiveFollowUpID = " + advanc_dgv_Assign_Comment.CurrentRow.Cells[0].Value;

            SqlCommand cmd1 = new SqlCommand(strQuery, con);

            cmd1.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = tSComBox_FollowUp_type.SelectedItem;
            cmd1.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar)).Value = tSTXT_FollowUp_Not.Text;

            cmd1.ExecuteNonQuery();


            con.Close();

            BringFolloWUp_TBL();

            tSComBox_FollowUp_type.Text = "";
            tSTXT_FollowUp_Not.Text = "";
        }

        private void BTN_deleteTask_Click(object sender, EventArgs e)
        {
            if (advanc_dgv_Assign_Comment.RowCount == 0)
            {
                MessageBox.Show("لايمكن الحذف لاتوجد مهام لحذفها","لم يتم ");
               return ;
            }

          
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("هل انت متاكد بانك تريد حذف هذة المهمة", "تاكيد", buttons);
            if (result == DialogResult.No)
            {
              return;
            }
        
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from ArchiveFollowUp where ArchiveFollowUpID =" + advanc_dgv_Assign_Comment.CurrentRow.Cells[0].Value, con);
            cmd.ExecuteNonQuery();
            con.Close();
            BringFolloWUp_TBL();
        
        }

        private void BTN_editTask_Click(object sender, EventArgs e)
        {
            if (advanc_dgv_Assign_Comment.RowCount == 0)
            {
                MessageBox.Show("لاتوجد مهام لتعديلها", "لم يتم ");
                return;
            }

            TXT_assignTitle.Text = Convert.ToString(advanc_dgv_Assign_Comment.CurrentRow.Cells[3].Value);
            string deName = Convert.ToString(advanc_dgv_Assign_Comment.CurrentRow.Cells[4].Value);

            for (int x = 0; x < COMLIST_assination.Items.Count; x++)

            {
                if (COMLIST_assination.Items[x].ToString() == deName)
                {
                    COMLIST_assination.SetItemChecked(x, true);
                }
                else
                {
                    COMLIST_assination.SetItemChecked(x, false);
                }
            }
        }

        private void advanc_dgv_Assign_Comment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tSComBox_FollowUp_type.SelectedItem = Convert.ToString(advanc_dgv_Assign_Comment.CurrentRow.Cells[5].Value);
            tSTXT_FollowUp_Not.Text = Convert.ToString(advanc_dgv_Assign_Comment.CurrentRow.Cells[6].Value);
        }

        private void btn_edit_status_FollowUp_Click(object sender, EventArgs e)
        {

          if (btn_edit_status_FollowUp.Text == "اضافة الى المتابعه")
            {
            con.Open();
            //final status to doc
            String strQuery = @"Update ArchiveBooks_TBL set BookStatus = @BookStatus Where ArchiveBookID = " + book_ID;
            SqlCommand cmd1 = new SqlCommand(strQuery, con);
            cmd1.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = "قيد المتابعة";
            cmd1.ExecuteNonQuery();
            con.Close();
            btn_edit_status_FollowUp.Text = "ازالة من المتابعه";
            MessageBox.Show("تم اضافة الكتاب الى المتابعة", "تم ");
            }
          else
            {
                for (int x = 0; x < advanc_dgv_Assign_Comment.Rows.Count; x++)

                {
                    if (Convert.ToString(advanc_dgv_Assign_Comment.CurrentRow.Cells[5].Value) != "مكتمل")
                    {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                 DialogResult result = MessageBox.Show("توجد مهمة واحدة او اكثر قيد المتابعة الى الان, ولم يتم عمل لها اجراء مكتمل "+ System.Environment.NewLine + "هل انته متاكد من ازالة الكتاب من المتابعه", "تاكيد", buttons);
                if (result == DialogResult.No)
                {
                    return;
                }
                    break;
                }
                   
                }

                con.Open();
                //final status to doc
                String strQuery = @"Update ArchiveBooks_TBL set BookStatus = @BookStatus Where ArchiveBookID = " + book_ID;
                SqlCommand cmd1 = new SqlCommand(strQuery, con);
                cmd1.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = "مكتمل";
                cmd1.ExecuteNonQuery();
                con.Close();
                btn_edit_status_FollowUp.Text = "اضافة الى المتابعه";
                MessageBox.Show("تم ازالة الكتاب من المتابعة", "تم ");
            }
           
        }
    }
}

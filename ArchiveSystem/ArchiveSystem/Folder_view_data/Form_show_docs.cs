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


        void Assign_Comment_TBL()
        {

//            adapter = new SqlDataAdapter(@"SELECT 

//dbo.Departments_TBL.DepartmentName as [القسم],
//dbo.[Assign&Comment_TBL].Task as [المهمة],
//dbo.[Assign&Comment_TBL].Action as [الاجراء],
//dbo.[Assign&Comment_TBL].Note as [الملاحظات],
//dbo.[Assign&Comment_TBL].DateAdded as [تاريخ الاضافة]

//FROM     dbo.ArchiveBooks_TBL INNER JOIN
//                  dbo.[Assign&Comment_TBL] ON dbo.ArchiveBooks_TBL.ArchiveBookID = dbo.[Assign&Comment_TBL].ArchiveBookID INNER JOIN
//                  dbo.Departments_TBL ON dbo.[Assign&Comment_TBL].DepartmentID = dbo.Departments_TBL.DepartmentID
//WHERE  ([Assign&Comment_TBL].ArchiveBookID) = @Param1

//                ", con);
//            adapter.SelectCommand.Parameters.AddWithValue("@Param1", Form_show_doc.book_ID);

            //dt.Clear();

            //adapter.Fill(dt);
            //advanc_dgv_Assign_Comment.DataSource = dt;
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

        string pic;
        public static string book_ID;
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
                COM_bookStatus.Text = dr1["BookStatus"].ToString();
                COM_privicy.Text = dr1["Privacy"].ToString();


                _subject = TXT_Subject.Text;
                _BookType = COM_bookType.Text;
                _bookCode = txt_book_code.Text;
            }
            dr1.Close();
            con.Close();
        }


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


        private void Form_show_docs_Load_1(object sender, EventArgs e)
        {
            TabControlBookdetails.SelectTab(0);
            Select_Departments();

            Assign_Comment_TBL();


            TabControlBookdetails.RightToLeft = RightToLeft.Yes;
            TabControlBookdetails.RightToLeftLayout = true;
            Fill_bookType();

            ListView_show_doc.Columns.Add("الملف", 300);
            cm_type_show.SelectedIndex = 1; //or ListView_show_doc.View = View.LargeIcon;
            ImageList_add_viwe.ImageSize = new Size(100, 100);
            ListView_show_doc.Columns[0].Width = 250;


            read_details_doc();
            show_files_doc();
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

        private void btn_zoom_out_Click_1(object sender, EventArgs e)
        {
            ZoomInOut(false);
        }

        private void btn_zoom_in_Click_1(object sender, EventArgs e)
        {
            ZoomInOut(true);
        }
        private void btn_Rotate_180_Click(object sender, EventArgs e)
        {
            Rotat_img(RotateFlipType.Rotate180FlipNone);
        }

        private void btn_Rotate_90_Click(object sender, EventArgs e)
        {
            Rotat_img(RotateFlipType.Rotate90FlipNone);

        }


        private void ListView_show_doc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

            ListView.SelectedListViewItemCollection breakfast = this.ListView_show_doc.SelectedItems;

            foreach (ListViewItem Item in breakfast)
            {

                try
                {
                    //pic = path_folder_client_temp + @"\" + ListView_show_doc.Items[Item.Index].SubItems[0].Text;
                    //pictureBox_show_doc.Load(pic);

                    for (int i = 0; i < ListView_show_doc.Items.Count; i++)
                    {
                        String file_name = ListView_show_doc.Items[Item.Index].SubItems[0].Text;
                        Image image2 = Image.FromFile(path_folder_client_temp + @"\" + file_name + "");//put var here

                        pictureBox_show_doc.Image = new Bitmap(image2);

                        image2.Dispose();
                        //get pah to use it to display img in wndows exploror
                        //picture_path = (Doc_source + @"\" + selectedFolder + @"\" + file_name + "");
                    }
                    //System.Diagnostics.Process.Start(pic);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        private void TSM_open_file_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(pic);
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
                pic = path_folder_client_temp + @"\" + ListView_show_doc.Items[Item.Index].SubItems[0].Text;

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

                MessageBox.Show(response.StatusDescription);
                response.Close();
                //delete it localy
                if (File.Exists(pic))
                {
                    //ListView_show_doc.Dispose();
                    //ImageList_add_viwe.Dispose();
                    //Image img = new Bitmap(pic);
                    //pictureBox_show_doc.Image = img.GetThumbnailImage(350, 350, null, new IntPtr());
                    //pictureBox_show_doc.Dispose();

                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    //pictureBox_show_doc.Dispose();

                    File.Delete(pic);
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
    }
}

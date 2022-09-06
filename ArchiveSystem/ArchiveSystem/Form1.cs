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
using ArchiveSystem.FollowUp;
using ArchiveSystem.EditeDocs;

namespace ArchiveSystem
{
    // i used metro forms library
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        //default taps indexes 
        public static int books_list_index = 1;
        public static int followUp_index = 2;


        //get this values from app onfig
        string FTP_ip = ConfigurationSettings.AppSettings["FTP_Server_Ip"];
        string FTP_user = ConfigurationSettings.AppSettings["FTP_Server_user"];
        string FTP_pass = ConfigurationSettings.AppSettings["FTP_Server_pass"];


        public Form1()
        {
            InitializeComponent();
        }
        //variables 
        public string selectedFolder = "";
        public string picture_path = "";
        public string Doc_source = "";
        public int DepID = 0;



        //connection string from app config
        public static string _con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con = new SqlConnection(_con);

        public static string photo_con_ = ConfigurationManager.ConnectionStrings["photo_con"].ConnectionString;
        SqlConnection photo_con = new SqlConnection(photo_con_);

        DataTable DT_TEXTBOX = new DataTable();
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





        private void Form1_Load(object sender, EventArgs e)
        {
            //اذا كان البرمشن متابعه ومهام قم بعرض تاب المتابعه والمهام فقط
            //permition code
            if (Login._permitionTYpeID == 1 || Login._permitionTYpeID == 8)//admin
            {

            }
            else
            {
                //remove index 0 and set new indexes
                metroTabControl1.TabPages.Remove(metroTabPage1);
                books_list_index = 0;
                followUp_index = 1;
            }


            //--------------moh------------------

            //metroTabControl1.SelectTab(0);



            COM_PaperType.SelectedIndex = 0;
            COM_priority.SelectedIndex = 0;
            COM_privicy.SelectedIndex = 0;
            Doc_source = Properties.Settings.Default.DOC_Source.ToString(); // doc source
            metroTabControl1.RightToLeft = RightToLeft.Yes;
            metroTabControl1.RightToLeftLayout = true;



            Refresh_Folders();
            Fill_bookType();
            callLogin_info();
            Auto_complet_subject();
            Auto_complet_from();
            Auto_complet_to();


            //check ftp if connected or not 
            try
            {


                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(FTP_user, FTP_pass);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                Console.WriteLine(reader.ReadToEnd());


                reader.Close();
                response.Close();
                PICBOX_successICON.Visible = true;
                PICBOX_failICON.Visible = false;
                LBL_FTPconValue.Text = "متصل";
                LBL_FTPconValue.Visible = true;

            }
            catch
            {
                LBL_FTPconValue.Text = "غير متصل";
                LBL_FTPconValue.Visible = true;
                PICBOX_failICON.Visible = true;
                PICBOX_successICON.Visible = false;
                MessageBox.Show("لايوجد اتصال ب ملف FTP");
            }
            //--------------end------------------


            //--------------show tap Form_view_data_dqv-----------------------

            Form_view_data_dqv new_tab = new Form_view_data_dqv();
            TabPage t = new TabPage();
            new_tab.TopLevel = false;
            t.Controls.Add(new_tab);
            metroTabControl1.TabPages.Insert(books_list_index, t);//or  metroTabControl1.TabPages.Add(t);
            new_tab.Show();
            new_tab.Dock = DockStyle.Fill;
            int x = metroTabControl1.TabCount;
            t.Text = "الارشيف العام";// + x

            //---------------end-----------------

            //--------------show tap-----------------------
            Form_Manager_FollowUp new_tab2 = new Form_Manager_FollowUp();
            TabPage t2 = new TabPage();
            new_tab2.TopLevel = false;
            t2.Controls.Add(new_tab2);
            metroTabControl1.TabPages.Insert(followUp_index, t2);//or  metroTabControl1.TabPages.Add(t2);
            new_tab2.Show();
            new_tab2.Dock = DockStyle.Fill;
            int x2 = metroTabControl1.TabCount;
            t2.Text = "المتابعة والمهام";// + x

            //---------------end-----------------

            this.WindowState = FormWindowState.Maximized;




        }


        //read files of folder into dgv files by clicking on folder
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
        //select all(check) files of dgv files 
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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


        //archive both db and ftp files
        private void BTN_Archive_Click(object sender, EventArgs e)
        {

            Random rand = new Random();
            int ran = rand.Next(100000, 999999);

            string subject_ = TXT_Subject.Text;
            string subject = Regex.Replace(subject_, @"[^0-9a-zA-Zء-ي]", " ");


            string datenow = DateTime.Now.ToString("hhmmss");
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd");

            string book_code_ = subject + ran.ToString() + datenow;

            //allow only araic and english alphabets and numbers and remove all special charicters
            string book_code = Regex.Replace(book_code_, @"[^0-9a-zA-Zء-ي]", " ");

            string BookNumber_ = TXT_bookNumber.Text;
            string BookNumber = Regex.Replace(BookNumber_, @"[^0-9a-zA-Zء-ي]", " ");



            int departmentID = Login._depID;
            int userid = Login._userID;

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



            string DT_bookDate_ = DT_bookDate.Text;
            DateTime oDate_ = Convert.ToDateTime(DT_bookDate_);
            string DT_bookDateE = oDate_.ToString("yyyy/MM/dd");






            string InboundNumber = TXT_Book_recive_number.Text;

            //string Subject = TXT_Subject.Text;
            string From = TXT_From.Text;
            string To = TXT_To.Text;
            string SearchKeys = TXT_SearchKEys.Text;

            // check list 
            //get list of checked rows 
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
            if (string.IsNullOrWhiteSpace(BookNumber) || string.IsNullOrWhiteSpace(subject_) || string.IsNullOrWhiteSpace(From) || string.IsNullOrWhiteSpace(To) || string.IsNullOrWhiteSpace(SearchKeys))
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

            else if (files_checked.Count <= 0)
            {
                MessageBox.Show("الرجاء اختيار صورة المستند");
            }



            //insert fields into sql db
            else
            {
                //check ftp connection 
                try
                {


                    // Get the object used to communicate with the server.
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip);
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    // This example assumes the FTP site uses anonymous logon.
                    request.Credentials = new NetworkCredential(FTP_user, FTP_pass);

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    Console.WriteLine(reader.ReadToEnd());


                    reader.Close();
                    response.Close();


                }
                catch
                {

                    MessageBox.Show("لا يمكن الادخال لايوجد اتصال ب FTP");
                    return;
                }


                string bookStatus_FollowUp = "بدون متابعة";
                string query = string.Format(@"INSERT INTO [dbo].[ArchiveBooks_TBL]
           ([BookCode]
           ,[BookNumber]
           ,[BookDate]
           ,[InboundNumber]
           ,[InboundDate]
           ,[Subject]

           ,[BooksTypeID]
           ,[From]
           ,[To]
           
           ,[BookPriority]
          
           ,[BookPaperType]
           ,[Notes]
           ,[DepartmentID_archivedBy]
           ,[UserID_archivedBy]
           ,[BookStatus]
           ,[Privacy]
           ,[SearchKeys]
            ) output INSERTED.ArchiveBookID
     VALUES
           (N'{0}',N'{1}','{2}',N'{3}','{4}',N'{5}',{6},N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',{12},{13},N'{14}',N'{15}',N'{16}')
", book_code, BookNumber_, DT_bookDateE, InboundNumber, InboundDate, subject_, COM_bookType.SelectedValue, From, To, COM_priority.Text, COM_PaperType.Text, TXT_notes.Text, departmentID, userid, bookStatus_FollowUp, COM_privicy.Text, SearchKeys, con);




                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int Book_id = (int)cmd.ExecuteScalar();
                con.Close();

                if (Book_id != 0)
                {

                    //create folder with same bookCode
                    var Typee = COM_bookType.Text;

                    //wefinish inserted book in sql now lets insert it in docs
                    //if we create folder inside type folder with name of bookcode 
                    //if type folder not found then delete the sql book we inseted

                    WebRequest request_ = WebRequest.Create(FTP_ip + Typee + "/" + book_code + "/");
                    request_.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request_.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                    try
                    {
                        using (var resp = (FtpWebResponse)request_.GetResponse())
                        {
                            Console.WriteLine(resp.StatusCode);
                       
                        }

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("cant create folder with name bookcod because type folder not found ",ex.ToString() );
                        MessageBox.Show("the inserted book will removed automaticaly");
                        con.Open();
                       

                        string delete_book = String.Format(@"delete FROM [ArchiveSystem].[dbo].[ArchiveBooks_TBL] where BookCode='{0}'", book_code);
                                                    
                                            
                        SqlCommand cmddd = new SqlCommand(delete_book, con);


                      int x=  cmddd.ExecuteNonQuery();
                        con.Close();

                        if (x != 0)
                        {
                            MessageBox.Show("inserted book without docs is removed");
                            Environment.Exit(1);
                        }
                    }


                    //put all files of selected folder in array 

                    string[] Files = Directory.GetFiles(Doc_source + @"\" + selectedFolder + "");//put variable here 

                    int i = 0;
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
                                //string fn = subject + file_name;
                                string fn = BookNumber + subject + DateTime.Now.ToString("yyyyMMddhhmmss") + i + Path.GetExtension(file);

                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip + Typee + "/" + book_code + "/" + fn);
                                request.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                                request.Method = WebRequestMethods.Ftp.UploadFile;

                                using (Stream fileStream = File.OpenRead(file))

                                using (Stream ftpStream = request.GetRequestStream())
                                {
                                    fileStream.CopyTo(ftpStream);

                                }
                                //insert image file to db
                                if (photo_con != null)
                                {
                                    photo_con.Open();
                                    byte[] fileBytes = System.IO.File.ReadAllBytes(file);

                                    string photoquery = @"INSERT INTO [dbo].[ArchiveImagesTBL]([bookCode],[filename],[image])
                                                                      VALUES  (@bookcode,@filename,@img)";

                                    SqlCommand photo_cmd = new SqlCommand(photoquery, photo_con);

                                    photo_cmd.Parameters.AddWithValue("bookcode", book_code);
                                    photo_cmd.Parameters.AddWithValue("filename", fn);

                                    photo_cmd.Parameters.AddWithValue("img", fileBytes);

                                    photo_cmd.ExecuteNonQuery();
                                    photo_con.Close();


                                }

                                //FileStream FS = new FileStream(file, FileMode.Open, FileAccess.Read); //create a file stream object associate to user selected file 
                                //byte[] img = new byte[FS.Length]; //create a byte array with size of user select file stream length
                                //FS.Read(img, 0, Convert.ToInt32(FS.Length));//read user selected file stream in to byte array


                                //cmd = new SqlCommand("insert into student(sno,sname,course,fee,photo) values(" + textBox1.Text + ",'" +
                                //                       textBox2.TabIndex + "','" + textBox3.Text + "'," + textBox4.Text + ",@photo)", con);
                                //conv_photo();
                                //con.Open();
                                //int n = cmd.ExecuteNonQuery();
                                //con.Close();
                                //if (n > 0)
                                //{
                                //    MessageBox.Show("record inserted");
                                //    loaddata();
                                //}
                                //else
                                //    MessageBox.Show("insertion failed");


                                //delete imges after copy it 
                                if (File.Exists(file))
                                {
                                    File.Delete(file);



                                }


                            }

                            i++;

                        }
                    }

                    //Clear fields
                    TXT_bookNumber.Clear();
                    TXT_Book_recive_number.Clear();
                    TXT_Subject.Clear();
                    TXT_From.Clear();
                    TXT_To.Clear();
                    TXT_SearchKEys.Clear();
                    TXT_notes.Clear();
                    COM_PaperType.SelectedIndex = 0;
                    COM_priority.SelectedIndex = 0;
                    COM_privicy.SelectedIndex = 0;
                    COM_bookType.SelectedIndex = 0;
                    DT_bookDate.TabIndex = 0;
                    DT_bookDate.Value = DateTime.Now;
                    DT_bookRecive_date.Value = DateTime.Now;

                    if (PicB_displayBOOK.Image != null)
                    {
                        PicB_displayBOOK.Image.Dispose();
                        PicB_displayBOOK.Image = null;
                    }

                    //call folder update to remove selected files of folder its like we reclicked on the folder to see what files remain in it
                    folder_update();


                    MessageBox.Show("تم ارشفة الكتاب");

                    //Form_view_data_dqv fvd = new Form_view_data_dqv();

                    //fvd.fill_dgv_view_data_doc();
                    ////fvd.Form_view_data_dqv_Load();
                    //button2.PerformClick();

                }

                else if (Book_id == 0)
                {
                    MessageBox.Show("لم يتم ادخال المعلومات ");
                }




            }

        }

        //show picture in picturebox
        private void DGV_Files_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in DGV_Files.Rows)
            {
                try
                {
                    String file_name = DGV_Files.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Image image2 = Image.FromFile(Doc_source + @"\" + selectedFolder + @"\" + file_name + "");//put var here

                    PicB_displayBOOK.Image = new Bitmap(image2);

                    image2.Dispose();
                    //get pah to use it to display img in wndows exploror
                    picture_path = (Doc_source + @"\" + selectedFolder + @"\" + file_name + "");
                }
                catch
                {
                }


            }
        }

        //click scanbutton
        private void button1_Click(object sender, EventArgs e)
        {
            ScanDialog sd = new ScanDialog();
            sd.Show();

        }


        //display picbox in windows
        private void PicB_displayBOOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Use default image viewer  
                System.Diagnostics.Process.Start(picture_path);
            }
            catch
            {
                MessageBox.Show("لاتوجد صورة لعرضها");
            }



        }


        //add folder to dgv folders
        private void BTN_addfolder_Click(object sender, EventArgs e)
        {
            string root = Doc_source + @"\" + TXT_addFolder.Text + "";

            Directory.CreateDirectory(root);
            Refresh_Folders();
        }


        //delete folder from dgv folders
        private void BTN_DELFolder_Click(object sender, EventArgs e)
        {
            string root = Doc_source + @"\" + TXT_addFolder.Text + "";

            Directory.Delete(root, true);

            Refresh_Folders();
        }

        //select folder to scann into
        private void Scanning_Folder_Click(object sender, EventArgs e)
        {

            try
            {
                Folder_Brows_DOC_Source.ShowDialog();
                string Doc_source = Folder_Brows_DOC_Source.SelectedPath;

                Properties.Settings.Default.DOC_Source = Doc_source;

                Properties.Settings.Default.Save();

            }
            catch
            {
                MessageBox.Show("الرجاء اختيار مصدر الملفات");
            }


        }


        //refres folder to display its files 
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



        //get folders (scanned books) of speacific folders
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
        //get books type from db
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


        //get info from login  form
        void callLogin_info()
        {
            LBL_USERNAME.Text = Login._user;
            DepID = Login._depID;
            //bring dep name from id 
            int depid = Login._depID;
            int permitionid = Login._permitionTYpeID;
            string query = string.Format(@" SELECT  [DepartmentID]
      ,[DepartmentName]
  FROM [ArchiveSystem].[dbo].[Departments_TBL] where DepartmentID={0}", depid, con);

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string DepName = dt.Rows[0]["DepartmentName"].ToString();
                //put dep name in lable 
                LBL_department.Text = DepName;
            }
            con.Close();

            string query2 = string.Format(@" SELECT  
      [PermitionType]
  FROM [ArchiveSystem].[dbo].[PermitionType_TBL] where PermitionTypeID={0}", permitionid, con);

            con.Open();
            SqlCommand cmd2 = new SqlCommand(query2, con);

            SqlDataAdapter permition = new SqlDataAdapter(cmd2);

            DataTable permitionDT = new DataTable();

            permition.Fill(permitionDT);
            if (permitionDT.Rows.Count > 0)
            {
                string per_name = permitionDT.Rows[0]["PermitionType"].ToString();
                //put dep name in lable 
                LBL_permitionType.Text = per_name;
            }
            con.Close();
        }


        private void BTN_RefrshFolders_Click(object sender, EventArgs e)
        {
            Refresh_Folders();
        }

        private void TXT_Book_recive_number_TextChanged(object sender, EventArgs e)
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



        // --Code Zoom Mouse----
        protected override void OnMouseWheel(MouseEventArgs ea)
        {
            //  flag = 1;
            // Override OnMouseWheel event, for zooming in/out with the scroll wheel
            if (PicB_displayBOOK.Image != null)
            {
                // If the mouse wheel is moved forward (Zoom in)
                if (ea.Delta > 0)
                {
                    // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                    if ((PicB_displayBOOK.Width < (15 * this.Width)) && (PicB_displayBOOK.Height < (15 * this.Height)))
                    {
                        // Change the size of the picturebox, multiply it by the ZOOMFACTOR
                        PicB_displayBOOK.Width = (int)(PicB_displayBOOK.Width * 1.25);
                        PicB_displayBOOK.Height = (int)(PicB_displayBOOK.Height * 1.25);

                        // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                        PicB_displayBOOK.Top = (int)(ea.Y - 1.25 * (ea.Y - PicB_displayBOOK.Top));
                        PicB_displayBOOK.Left = (int)(ea.X - 1.25 * (ea.X - PicB_displayBOOK.Left));
                    }
                }
                else
                {
                    // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                    if ((PicB_displayBOOK.Width > (panel14.Width)) && (PicB_displayBOOK.Height > (panel14.Height)))
                    {
                        // Change the size of the picturebox, divide it by the ZOOMFACTOR
                        PicB_displayBOOK.Width = (int)(PicB_displayBOOK.Width / 1.25);
                        PicB_displayBOOK.Height = (int)(PicB_displayBOOK.Height / 1.25);

                        // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                        PicB_displayBOOK.Top = (int)(ea.Y - 0.80 * (ea.Y - PicB_displayBOOK.Top));
                        PicB_displayBOOK.Left = (int)(ea.X - 0.80 * (ea.X - PicB_displayBOOK.Left));
                    }
                }
            }
        }

        private void BTN_LogOut_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);

            //Form_show_edit_docs showdoc = new Form_show_edit_docs(BookCode);

            //Form_show_edit_docs obj1 = (Form_show_edit_docs)Application.OpenForms["Form_show_edit_docs"];
            //obj1.Close();

            //ScanDialog obj2 = (ScanDialog)Application.OpenForms["ScanDialog"];
            //obj2.Close();

            //SelectScanner obj3 = (SelectScanner)Application.OpenForms["SelectScanner"];
            //obj3.Close();

            //AddDocs obj4 = (AddDocs)Application.OpenForms["AddDocs"];
            //obj4.Hide();



            //    List<Form> formsToClose = new List<Form>();
            //    foreach (Form form in Application.OpenForms)
            //    {
            //        if (form != this)
            //        {
            //            formsToClose.Add(form);
            //        }
            //    }

            //    formsToClose.ForEach(f => f.Close());

            //Login log_in = new Login();
            //log_in.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_view_data_dqv vd = new Form_view_data_dqv();
            vd.fill_dgv_view_data_doc();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {


        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (metroTabControl1.SelectedTab.Text == "الارشيف العام")
            //{
            //    Form_view_data_dqv vd = new Form_view_data_dqv();
            //    vd.Form_view_data_dqv_Load(null, EventArgs.Empty);
            //}
        }

        private void BTN_generateReport_Click(object sender, EventArgs e)
        {
            ReportForm rf = new ReportForm();
            rf.Show();
        }
    }
}

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

namespace ArchiveSystem
{
    // i used metro forms library
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

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


      
        private void Form1_Load(object sender, EventArgs e)
        {

            //--------------moh------------------
            // set indexes of comboboxes
            metroTabControl1.SelectTab(0);
            
            COM_PaperType.SelectedIndex = 0;
            COM_priority.SelectedIndex = 0;
            COM_privicy.SelectedIndex = 0;
            Doc_source = Properties.Settings.Default.DOC_Source.ToString(); // doc source
            metroTabControl1.RightToLeft = RightToLeft.Yes;
            metroTabControl1.RightToLeftLayout = true;

            Refresh_Folders();
            Fill_bookType();
            callLogin_info();

            //DepartmentBooks();
            AssignmentBooks();

 //check ftp if connected or not 
            try
            {


                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(FTP_user,FTP_pass);

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

            }
            //--------------end------------------


            //--------------shukri-----------------------

            Form_view_data_dqv new_tab = new Form_view_data_dqv();
            TabPage t = new TabPage();
            new_tab.TopLevel = false;
            t.Controls.Add(new_tab);
            metroTabControl1.TabPages.Insert(1, t);//or  metroTabControl1.TabPages.Add(t);
            new_tab.Show();
            new_tab.Dock = DockStyle.Fill;
            int x = metroTabControl1.TabCount;
            t.Text = "الارشيف العام";// + x

            //---------------end-----------------



             

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


            string subject = TXT_Subject.Text;
            int ran = rand.Next(100000, 999999);

            string datenow = DateTime.Now.ToString("hhmmss");
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd");

            string book_code = TXT_Subject.Text + ran.ToString() + datenow;
            int departmentID = Login._depID;
            int userid = Login._userID;



            string InboundDate_ = DT_bookRecive_date.Text;
            DateTime oDate = Convert.ToDateTime(InboundDate_);
            string InboundDate = oDate.ToString("yyyy/MM/dd");


            string DT_bookDate_ = DT_bookDate.Text;
            DateTime oDate_ = Convert.ToDateTime(DT_bookDate_);
            string DT_bookDateE = oDate_.ToString("yyyy/MM/dd");




            string BookNumber = TXT_bookNumber.Text;
            string InboundNumber = TXT_Book_recive_number.Text;

            string Subject = TXT_Subject.Text;
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

                    MessageBox.Show("لا يمكن الادخال تأكد من الاتصال بالشبكة");
                    return;
                }


              string  bookStatus_FollowUp = "بدون متابعة";
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
           ,[ArchivedDate]
           ,[BookPaperType]
           ,[Notes]
           ,[DepartmentID_archivedBy]
           ,[UserID_archivedBy]
           ,[BookStatus]
           ,[Privacy]
           ,[SearchKeys]
            ) output INSERTED.ArchiveBookID
     VALUES
           (N'{0}','{1}','{2}','{3}','{4}',N'{5}',{6},N'{7}',N'{8}',N'{9}','{10}',N'{11}',N'{12}',{13},{14},N'{15}',N'{16}',N'{17}')
", book_code, BookNumber, DT_bookDateE, InboundNumber, InboundDate, Subject, COM_bookType.SelectedValue, From, To, COM_priority.Text, currentDate, COM_PaperType.Text, TXT_notes.Text, departmentID, userid, bookStatus_FollowUp, COM_privicy.Text, SearchKeys, con);




                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int Book_id = (int)cmd.ExecuteScalar();
                con.Close();

                if (Book_id != 0)
                {

                    //create folder with same bookCode
                    var Typee = COM_bookType.Text;



                    WebRequest request_ = WebRequest.Create(FTP_ip + Typee + "/" + book_code + "/");
                    request_.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request_.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                    using (var resp = (FtpWebResponse)request_.GetResponse())
                    {
                        Console.WriteLine(resp.StatusCode);
                    }



                    //put all files of selected folder in array 

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
                String file_name = DGV_Files.Rows[e.RowIndex].Cells[1].Value.ToString();
                Image image2 = Image.FromFile(Doc_source + @"\" + selectedFolder + @"\" + file_name + "");//put var here

                PicB_displayBOOK.Image = new Bitmap(image2);

                image2.Dispose();
                //get pah to use it to display img in wndows exploror
                picture_path = (Doc_source + @"\" + selectedFolder + @"\" + file_name + "");
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

         //this function will bring books that have assign to my department
        private void AssignmentBooks()
        {
            try
            {
                string query = string.Format(@"  SELECT
       [BookCode] as 'رمز الكتاب'
      ,[BookNumber] as 'رقم الكتاب'
      ,[BookDate]as 'تاريخ الكتاب'
      ,[InboundNumber]as 'رقم واردنا'
      ,[InboundDate]as 'تاريخ واردنا'
      ,[Subject]as 'الموضوع'
      ,[BooksType_TBL].[BookTypeName]as 'نوع الكتاب'
      ,[From]as 'من'
      ,[To]as 'الى'
      ,[BookPriority]as 'الاولوية'
      ,[ArchivedDate]as 'تاريخ الارشفة'
      ,[BookPaperType]as 'نوع النسخة'
      ,[Notes]as 'ملاحظات'
      ,[Departments_TBL].[DepartmentName]as 'ارشفة بواسطة-قسم'
      ,[Users_TBL].[Username]as 'ارشفة بواسطة-اسم'
      ,[BookStatus]as 'حالة الكتاب'
      ,[Privacy]as 'الخصوصية'
      ,[SearchKeys]as 'مفاتيح البحث'
	  
            FROM[ArchiveSystem].[dbo].[ArchiveFollowUp]
   inner JOIN[ArchiveBooks_TBL] ON[ArchiveFollowUp].ArchiveBookID = [ArchiveBooks_TBL].ArchiveBookID
      inner JOIN[BooksType_TBL] ON[ArchiveBooks_TBL].[BooksTypeID] = [BooksType_TBL].[BooksTypeID]
	     inner JOIN[Departments_TBL] ON[ArchiveBooks_TBL].[DepartmentID_archivedBy] = [Departments_TBL].[DepartmentID]
	     inner JOIN[Users_TBL] ON[ArchiveBooks_TBL].[UserID_archivedBy] = [Users_TBL].[UserID]
	    
   where[ArchiveFollowUp].[Department_AssignTO_ID] = {0}", DepID, con);




                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable AssignBooks = new DataTable();

                adp.Fill(AssignBooks);
                DGV_assignation.DataSource = AssignBooks;

                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



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
        string ftp_server_Ip = ConfigurationManager.AppSettings["FTP_Server_Ip"];
        string ftp_server_username = ConfigurationManager.AppSettings["FTP_Server_user"];
        string ftp_server_password = ConfigurationManager.AppSettings["FTP_Server_pass"];

        //The path of the file on the client computer with which we will download the files from the FTP file temporarily, and then we delete the downloaded files
        string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];
        public  string BookCode;
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
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"\" + DGV_assignation.CurrentRow.Cells[6].Value.ToString() + @"\" + DGV_assignation.CurrentRow.Cells[0].Value.ToString() + @"\" + fileName));
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

        public string[] GetFileList()
        {


            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                //                                          Here we put the path IP and of the FTP file server
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"wared\cjs2\"));
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"\" + DGV_assignation.CurrentRow.Cells[6].Value.ToString() + @"\" + DGV_assignation.CurrentRow.Cells[0].Value.ToString() + @"\"));
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
        private void DGV_assignation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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

                if (files != null)
                {
                    foreach (string file in files)
                    {

                        Download(file);

                    }
                }



                var path = string.Format(path_folder_client_temp);

                BookCode = DGV_assignation.CurrentRow.Cells[0].Value.ToString();

                Form_show_docs s_doc1 = new Form_show_docs(BookCode);
                s_doc1.Show();

                //System.Diagnostics.Process.Start(path);

            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BTN_RefrshFolders_Click(object sender, EventArgs e)
        {
            Refresh_Folders();
        }
    }
}

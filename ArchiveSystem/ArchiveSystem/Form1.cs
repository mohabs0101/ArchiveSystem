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

    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        public Form1()
        {
            InitializeComponent();
        }
        public string selectedFolder = "";
        public string picture_path = "";
        public string Doc_source = "";
        
         
        string FTP_ip = ConfigurationSettings.AppSettings["FTP_Path"];
        string FTP_user = ConfigurationSettings.AppSettings["FTP_user"];
        string FTP_pass = ConfigurationSettings.AppSettings["FTP_pass"];



        public static string _con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con = new SqlConnection(_con);



      public  void Refresh_Folders()
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
        void callLogin_info()
        {
            LBL_USERNAME.Text = Login._user;

            //bring dep name from id 
            int depid = Login._depID;

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


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            metroTabControl1.SelectTab(0);
            //--------------moh------------------
           
            COM_bookStatus.SelectedIndex = 0;
            COM_PaperType.SelectedIndex = 0;
            COM_priority.SelectedIndex = 0;
            COM_privicy.SelectedIndex = 0;
            Doc_source = Properties.Settings.Default.DOC_Source.ToString(); // doc source
            metroTabControl1.RightToLeft = RightToLeft.Yes;
            metroTabControl1.RightToLeftLayout = true;

            Refresh_Folders();
            Fill_bookType();
            callLogin_info();
            Select_Departments();
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



        private void BTN_Archive_Click(object sender, EventArgs e)
        {
            Random rand = new Random();


            string subject = TXT_Subject.Text;
            int ran = rand.Next(100000, 999999);

            string datenow = DateTime.Now.ToString("hhmmss");
            string currentDate = DateTime.Now.ToString("yyyy:MM:dd");

            string book_code = TXT_Subject.Text + ran.ToString() + datenow;
            int departmentID = Login._depID;
            int userid = Login._userID;
            string InboundDate = DT_bookRecive_date.Text;

            //check if fields are empty


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
                        //files_checked.Add(dataGridView1.Rows[i].Cells[1].ToString());
                        files_checked.Add(DGV_Files.Rows[i].Cells[1].Value.ToString());
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(BookNumber) || string.IsNullOrWhiteSpace(InboundNumber) || string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(From) || string.IsNullOrWhiteSpace(To) || string.IsNullOrWhiteSpace(SearchKeys))
            {
                List<TextBox> boxes = new List<TextBox>();
                if (string.IsNullOrWhiteSpace(TXT_bookNumber.Text))
                {
                    //highlightTextBox= txtFname;
                    boxes.Add(TXT_bookNumber);
                }
                else { TXT_bookNumber.BackColor = Color.White; }
                if (string.IsNullOrWhiteSpace(TXT_Book_recive_number.Text))
                {
                    //highlightTextBox = txtLname;
                    boxes.Add(TXT_Book_recive_number);
                }
                else { TXT_Book_recive_number.BackColor = Color.White; }

                if (string.IsNullOrWhiteSpace(TXT_Subject.Text))
                {
                    //highlightTextBox = txtTown;
                    boxes.Add(TXT_Subject);
                }
                else { TXT_Subject.BackColor = Color.White; }
                if (string.IsNullOrWhiteSpace(TXT_From.Text))
                {
                    //highlightTextBox = txtPostCode;
                    boxes.Add(TXT_From);
                }
                else { TXT_From.BackColor = Color.White; }

                if (string.IsNullOrWhiteSpace(TXT_To.Text))
                {
                    //highlightTextBox = txtPostCode;
                    boxes.Add(TXT_To);
                }
                else { TXT_To.BackColor = Color.White; }
                if (string.IsNullOrWhiteSpace(TXT_SearchKEys.Text))
                {
                    //highlightTextBox = txtPostCode;
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




            else
            {

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
", book_code, BookNumber, DT_bookDate.Text, InboundNumber, InboundDate, Subject, COM_bookType.SelectedValue, From, To, COM_priority.Text, currentDate, COM_PaperType.Text, TXT_notes.Text, departmentID, userid, COM_bookStatus.Text, COM_privicy.Text, SearchKeys, con);




                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int Book_id = (int)cmd.ExecuteScalar();
                con.Close();

                if (Book_id != 0)
                {
                    // for loop on list of dep and make query to insert in assign table 
                    foreach (Object item in COMLIST_assination.CheckedItems)
                    {
                        DataRowView drv = item as DataRowView;
                        int id = Convert.ToInt16(drv["DepartmentID"]);
                        string asssignQuery = string.Format(@" INSERT INTO [dbo].[Assign&Comment_TBL]
           ([ArchiveBookID]
           ,[DepartmentID]
           ,[Comment])
     VALUES
           ( {0},{1},N'{2}')", Book_id, id, "", con);

                        con.Open();
                        SqlCommand cmd2 = new SqlCommand(asssignQuery, con);

                        cmd2.ExecuteNonQuery();

                        con.Close();
                    }

                }




                //create folder with same db index id
                var Typee = COM_bookType.Text;// bring it from dropdown user chose
                                              //var BookCat = "كتاب عادي";// bring it from dropdown user chose



                WebRequest request_ = WebRequest.Create(FTP_ip + Typee + "/" + book_code + "/");
                request_.Method = WebRequestMethods.Ftp.MakeDirectory;
                request_.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                using (var resp = (FtpWebResponse)request_.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }



                //create array of string with all local dir files names

                string[] Files = Directory.GetFiles(Doc_source + @"\" + selectedFolder + "");//put variable here 


                //get the record number (RecID)


                foreach (var item in files_checked)
                { 

                    string filenamechecked = item.ToString();
                    foreach (string file in Files)
                    {

                        string file_name = Path.GetFileName(file);
                        //if file == selected files from app
                        if (file_name == filenamechecked)
                        {
                            string fn = subject + file_name;
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_ip + Typee + "/" + book_code + "/" + fn);
                            request.Credentials = new NetworkCredential(FTP_user, FTP_pass);
                            request.Method = WebRequestMethods.Ftp.UploadFile;

                            using (Stream fileStream = File.OpenRead(file))

                            using (Stream ftpStream = request.GetRequestStream())
                            {
                                fileStream.CopyTo(ftpStream);

                            }

                            //delete imges after coopy
                            if (File.Exists(file))
                            {
                                File.Delete(file);



                            }
                            //int ColumnIndex = DGV_Files.CurrentCell.ColumnIndex;

                            ////remove item from gridview 
                            //DGV_Files.Rows.RemoveAt(ColumnIndex);
                            //DGV_Files.Update();
                            //DGV_Files.Parent.Refresh();
                            
                        }



                    }
                }
                //this.Form1_Load(null, null);

                TXT_bookNumber.Clear();
                TXT_Book_recive_number.Clear();
                TXT_Subject.Clear();
                TXT_From.Clear();
                TXT_To.Clear();
                TXT_SearchKEys.Clear();
                TXT_notes.Clear();
                COM_bookStatus.SelectedIndex = 0;
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

                //foreach (object o in COMLIST_assination.Items)
                //{
                //    COMLIST_assination.SetItemCheckState(0, CheckState.Unchecked);

                //}
                for (int i = 0; i < COMLIST_assination.Items.Count; i++)
                {
                    COMLIST_assination.SetItemCheckState(i, CheckState.Unchecked);

                }


                string[] Files2 = Directory.GetFiles(Doc_source + @"\" + selectedFolder + "", "*.*");//put variable name instade of path
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




                folder_update();


                MessageBox.Show("تم ارشفة الكتاب");



            }
























            //check if dgvfiles list  is empty








            //SqlDataAdapter adp = new SqlDataAdapter(cmd);

            //DataTable dt2 = new DataTable();





        }

        private void DGV_Files_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in DGV_Files.Rows)
            {
                String file_name = DGV_Files.Rows[e.RowIndex].Cells[1].Value.ToString();
                Image image2 = Image.FromFile(Doc_source + @"\" + selectedFolder + @"\" + file_name + "");//put var here
                //pictureBox1.Image=file
                // Get a PropertyItem from image1.
                //PropertyItem propItem = image1.GetPropertyItem(20624);

                //// Change the ID of the PropertyItem.
                //propItem.Id = 20625;

                //// Set the PropertyItem for image2.
                //image2.SetPropertyItem(propItem);

                //// Draw the image.
                //e.Graphics.DrawImage(image2, 20.0F, 20.0F);

                PicB_displayBOOK.Image = new Bitmap(image2);

                image2.Dispose();
                picture_path = (Doc_source + @"\" + selectedFolder + @"\" + file_name + "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScanDialog sd = new ScanDialog();

            
            sd.Show();
            
        }



        private void PicB_displayBOOK_Click(object sender, EventArgs e)
        {

            // Use default image viewer  
            System.Diagnostics.Process.Start(picture_path);

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

        private void Scanning_Folder_Click(object sender, EventArgs e)
        {
            //if (Folder_Brows_DOC_Source.ShowDialog() == true)
            //{
            //    string fileName = openFileDialog.FileName;
            //    if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            //    {
            //        spamText.Text = File.ReadAllText(fileName);
            //        Splitter(new[] { fileName });
            //    }
            //}
            try
            {
                Folder_Brows_DOC_Source.ShowDialog();
                string Doc_source = Folder_Brows_DOC_Source.SelectedPath;

                Properties.Settings.Default.DOC_Source = Doc_source;

                Properties.Settings.Default.Save();
                this.Form1_Load(null, null);
            }
            catch
            {
                MessageBox.Show("الرجاء اختيار مصدر الملفات");
            }


        }

      

        private void FOLDERS_prefermCLick_Click(object sender, EventArgs e)
        {
          
        }
        //refres folder to display its files 
      public  void folder_update ()

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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void BTN_RefrshFolders_Click(object sender, EventArgs e)
        {
            Refresh_Folders();
        }

        //private void panel3_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void TXT_bookNumber_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(TXT_bookNumber.Text))
        //    {
        //        e.Cancel = true;
        //        TXT_bookNumber.Focus();
        //        errorProvider1.SetError(TXT_bookNumber, "حقل فارغ");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(TXT_bookNumber, "");
        //    }
        //}

        //private void BTN_Archive_Enter(object sender, EventArgs e)
        //{
        //    if (ValidateChildren(ValidationConstraints.Enabled))
        //    {
        //        MessageBox.Show(TXT_bookNumber.Text, "Demo App - Message!");
        //    }
        //}

        //private void TXT_Book_recive_number_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(TXT_Book_recive_number.Text))
        //    {
        //        e.Cancel = true;
        //        TXT_Book_recive_number.Focus();
        //        errorProvider1.SetError(TXT_Book_recive_number, "Name should not be left blank!");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(TXT_Book_recive_number, "");
        //    }
        //}

        //private void TXT_SearchKEys_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(TXT_SearchKEys.Text))
        //    {
        //        e.Cancel = true;
        //        TXT_SearchKEys.Focus();
        //        errorProvider1.SetError(TXT_SearchKEys, "Name should not be left blank!");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(TXT_SearchKEys, "");
        //    }
        //}

        //private void TXT_Subject_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void TXT_Subject_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(TXT_Subject.Text))
        //    {
        //        e.Cancel = true;
        //        TXT_Subject.Focus();
        //        errorProvider1.SetError(TXT_Subject, "Name should not be left blank!");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(TXT_Subject, "");
        //    }
        //}

        //private void TXT_From_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(TXT_From.Text))
        //    {
        //        e.Cancel = true;
        //        TXT_From.Focus();
        //        errorProvider1.SetError(TXT_From, "Name should not be left blank!");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(TXT_From, "");
        //    }
        //}

        //private void TXT_To_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void TXT_To_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(TXT_From.Text))
        //    {
        //        e.Cancel = true;
        //        TXT_To.Focus();
        //        errorProvider1.SetError(TXT_To, "Name should not be left blank!");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(TXT_To, "");
        //    }
        //}

        //private void TXT_notes_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(TXT_notes.Text))
        //    {
        //        e.Cancel = true;
        //        TXT_notes.Focus();
        //        errorProvider1.SetError(TXT_notes, "Name should not be left blank!");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(TXT_notes, "");
        //    }
        //}


    }
}

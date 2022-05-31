using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections;

namespace ArchiveSystem.Folder_view_data
{
    public partial class Form_view_data_dqv : Form
    {
        public Form_view_data_dqv()
        {
            InitializeComponent();
        }

        public static string _con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con = new SqlConnection(_con);



        DataTable dt = new DataTable();
        SqlDataAdapter adapter;

       public void fill_dgv_view_data_doc()
        {


            adapter = new SqlDataAdapter(@"SELECT

dbo.ArchiveBooks_TBL.BookCode as [كود الكتاب],
dbo.ArchiveBooks_TBL.BookNumber as [رقم الكتاب],
dbo.ArchiveBooks_TBL.BookDate as [تاريخ الكتاب],
dbo.ArchiveBooks_TBL.InboundNumber as [رقم واردنا],
dbo.ArchiveBooks_TBL.InboundDate as [تاريخ واردنا],
dbo.ArchiveBooks_TBL.Subject as [موضوع الكتاب],
dbo.ArchiveBooks_TBL.SearchKeys as [مفاتيح البحث],
dbo.BooksType_TBL.BookTypeName as [النوع(الكابينة)],
dbo.ArchiveBooks_TBL.[From] as [من],
dbo.ArchiveBooks_TBL.[To] as [الى],
dbo.ArchiveBooks_TBL.SearchKeys as [مفاتيح البحث],
dbo.ArchiveBooks_TBL.BookPriority as [الاولوية],
dbo.ArchiveBooks_TBL.ArchivedDate as [تاريخ الارشفة],
dbo.ArchiveBooks_TBL.BookPaperType as [نوع النسخة],
dbo.ArchiveBooks_TBL.Notes as [الملاحظات],
dbo.Departments_TBL.DepartmentName as [القسم],
dbo.Users_TBL.Username as [المستخدم],
dbo.ArchiveBooks_TBL.BookStatus as [حالة الكتاب],
dbo.ArchiveBooks_TBL.Privacy as [الخصوصية]


FROM   dbo.ArchiveBooks_TBL INNER JOIN
                  dbo.Departments_TBL ON dbo.ArchiveBooks_TBL.DepartmentID_archivedBy = dbo.Departments_TBL.DepartmentID INNER JOIN
                  dbo.Users_TBL ON dbo.ArchiveBooks_TBL.UserID_archivedBy = dbo.Users_TBL.UserID INNER JOIN
                  dbo.BooksType_TBL ON dbo.ArchiveBooks_TBL.BooksTypeID = dbo.BooksType_TBL.BooksTypeID

           
            WHERE(dbo.ArchiveBooks_TBL.BookDate between @Param1 and @Param2)

                ", con);

//AND(dbo.ArchiveBooks_TBL.InboundDate between @Param3 and @Param4)

            adapter.SelectCommand.Parameters.AddWithValue("@Param1", DT_bookDate_from.Value.Date);
            adapter.SelectCommand.Parameters.AddWithValue("@Param2", DT_bookDate_to.Value.Date);
            //adapter.SelectCommand.Parameters.AddWithValue("@Param3", DT_bookRecive_date_from.Value.Date);
            //adapter.SelectCommand.Parameters.AddWithValue("@Param4", DT_bookRecive_date_to.Value.Date);

            dt.Clear();

            adapter.Fill(dt);
            advanc_dgv_view_data_doc.DataSource = dt;
            Label2_count_doc.Text = Convert.ToString(BindingContext[dt].Count);

        }


        public void Form_view_data_dqv_Load(object sender, EventArgs e)
        {
            DT_bookDate_to.Value = DateTime.Now;
            DT_bookRecive_date_to.Value = DateTime.Now;

            fill_dgv_view_data_doc();

            //for (int i = 0; i < advanc_dgv_view_data_doc.Columns.Count - 1; i++)
            //   {
            //    advanc_dgv_view_data_doc.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            //   }




            advanc_dgv_view_data_doc.RowTemplate.Height = 30;

            advanc_dgv_view_data_doc.Columns[0].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            advanc_dgv_view_data_doc.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11);


            advanc_dgv_view_data_doc.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;
            advanc_dgv_view_data_doc.RowsDefaultCellStyle.BackColor = Color.LightGray;

            advanc_dgv_view_data_doc.RowsDefaultCellStyle.SelectionBackColor = Color.Orange;
            advanc_dgv_view_data_doc.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            advanc_dgv_view_data_doc.Columns[5].Width = 170;
            advanc_dgv_view_data_doc.Columns[6].Width = 350;
        }

        private void btn_search_claer_Click(object sender, EventArgs e)
        {
            txt_seach.Clear();
        }

        private void txt_seach_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (checkBox_search_all.Checked == true)
                {
                    DataView dv = dt.DefaultView;

                    dv.RowFilter = "[رقم الكتاب]+[تاريخ الكتاب]+[رقم واردنا]+[تاريخ واردنا]+[موضوع الكتاب]+[مفاتيح البحث]+[من]+[الى]+[الملاحظات]  Like '%" + txt_seach.Text + "%'";
                    this.advanc_dgv_view_data_doc.DataSource = dv;
                }
                else
                { 
                DataView dv = dt.DefaultView;

                dv.RowFilter = "[" + advanc_dgv_view_data_doc.Columns[col_index_select].Name + "]+[رقم الكتاب]  Like '%" + txt_seach.Text + "%'";
                this.advanc_dgv_view_data_doc.DataSource = dv;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "يجب اختيار عمود لبحث بة");
            }


        }

        int col_index_select = 1;
        private void advanc_dgv_view_data_doc_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_seach.Clear();

            col_index_select = e.ColumnIndex;

            for (int i = 0; i < advanc_dgv_view_data_doc.Columns.Count - 1; i++)
            {

                advanc_dgv_view_data_doc.Columns[i].HeaderCell.Style.BackColor = Color.LightGray;

                advanc_dgv_view_data_doc.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.DeepSkyBlue;

            }
            txt_seach.Select();
            
        }


        private void NumericUpDown_font_size_ValueChanged(object sender, EventArgs e)
        {

            this.advanc_dgv_view_data_doc.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", Convert.ToInt32(NumericUpDown_font_size.Value) + 1);
            this.advanc_dgv_view_data_doc.DefaultCellStyle.Font = new Font("Tahoma", Convert.ToInt32(NumericUpDown_font_size.Value));

        }

        private void advanc_dgv_view_data_doc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Label2_count_doc_search.Text = Convert.ToString(advanc_dgv_view_data_doc.RowCount);
        }


        /////////Code to read and download FTP files from the server/////////////
        //You must first create a file on the server and set the FTP for it.To save files with

        //1-Read FTP File

        //This variables is Existing in a file App.config in the program
        string ftp_server_Ip = ConfigurationManager.AppSettings["FTP_Server_Ip"];
        string ftp_server_username = ConfigurationManager.AppSettings["FTP_Server_user"];
        string ftp_server_password = ConfigurationManager.AppSettings["FTP_Server_pass"];

        //The path of the file on the client computer with which we will download the files from the FTP file temporarily, and then we delete the downloaded files
        public static string path_folder_client_temp  = "";//نسند لة قيمة في حدث الدبل كلك

        public string[] GetFileList()
        {


            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                //                                          Here we put the path IP and of the FTP file server
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"wared\cjs2\"));
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"\" + advanc_dgv_view_data_doc.CurrentRow.Cells[7].Value.ToString() + @"\" + advanc_dgv_view_data_doc.CurrentRow.Cells[0].Value.ToString() + @"\"));
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
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_server_Ip + @"\" + advanc_dgv_view_data_doc.CurrentRow.Cells[7].Value.ToString() + @"\" + advanc_dgv_view_data_doc.CurrentRow.Cells[0].Value.ToString() + @"\" + fileName));
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

        public static string BookCode;
        private void advanc_dgv_view_data_doc_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
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

                BookCode = advanc_dgv_view_data_doc.CurrentRow.Cells[0].Value.ToString();

                Form_show_docs s_doc1 = new Form_show_docs(BookCode);
                s_doc1.Show();

                //System.Diagnostics.Process.Start(path);

            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //-----------------END------------------------



        //////////////code filter bettween date////////////////////
        private void btn_show_filter_Click(object sender, EventArgs e)
        {
            if (panel_filter.Visible == false)
            {
                panel_filter.Visible = true;
            }
            else
            {
                panel_filter.Visible = false;
            }
        }
        private void btn_close_show_fliter_Click(object sender, EventArgs e)
        {
            panel_filter.Visible = false;
        }
        private void btn_filter_Click(object sender, EventArgs e)
        {

            fill_dgv_view_data_doc();
            panel_filter.Visible = false;
            btn_show_filter.BackColor = Color.Salmon;

            if (com_mode_filter_bookDate.SelectedIndex > 0)
            {
                advanc_dgv_view_data_doc.Columns[2].HeaderCell.Style.BackColor = Color.Salmon;
            }

            if (com_mode_filter_bookRecive.SelectedIndex > 0)
            {
                advanc_dgv_view_data_doc.Columns[4].HeaderCell.Style.BackColor = Color.Salmon;
            }
        }

        private void btn_clear_fliter_Click(object sender, EventArgs e)
        {
            DT_bookDate_from.Value = Convert.ToDateTime("1990/01/01");
            DT_bookDate_to.Value = DateTime.Now;
            DT_bookRecive_date_from.Value = Convert.ToDateTime("1990/01/01");
            DT_bookRecive_date_to.Value = DateTime.Now;

            btn_filter_Click(null, null);
            btn_show_filter.BackColor = Color.DeepSkyBlue;

            if (com_mode_filter_bookDate.SelectedIndex > 0)
            {
                advanc_dgv_view_data_doc.Columns[2].HeaderCell.Style.BackColor = Color.LightGray;
            }
            if (com_mode_filter_bookRecive.SelectedIndex > 0)
            {
                advanc_dgv_view_data_doc.Columns[4].HeaderCell.Style.BackColor = Color.LightGray;
            }
        }

        private void com_mode_filter_bookDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_mode_filter_bookDate.SelectedIndex == 0)//Day now
            {
                DT_bookDate_from.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day);// Convert.ToDateTime(DateTime.Now.Year + DateTime.Now.Month);
                DT_bookDate_to.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day);
                panel_DT_bookDate.Visible = false;
            }
            else if (com_mode_filter_bookDate.SelectedIndex == 1)// month now
            {
                DT_bookDate_from.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/1");
                DT_bookDate_to.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/30");
                panel_DT_bookDate.Visible = false;
            }
            else if (com_mode_filter_bookDate.SelectedIndex == 2)// year now
            {
                DT_bookDate_from.Value = Convert.ToDateTime(DateTime.Now.Year + "/1" + "/1");
                DT_bookDate_to.Value = Convert.ToDateTime(DateTime.Now.Year + "/12" + "/30");
                panel_DT_bookDate.Visible = false;
            }
            else if (com_mode_filter_bookDate.SelectedIndex == 3)// custome
            {
                panel_DT_bookDate.Visible = true;

            }
        }

        private void com_mode_filter_bookRecive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_mode_filter_bookRecive.SelectedIndex == 0)
            {
                DT_bookRecive_date_from.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day);// Convert.ToDateTime(DateTime.Now.Year + DateTime.Now.Month);
                DT_bookRecive_date_to.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day);
                panel_DT_bookRecive_date.Visible = false;
            }
            else if (com_mode_filter_bookRecive.SelectedIndex == 1)
            {
                DT_bookRecive_date_from.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/1");
                DT_bookRecive_date_to.Value = Convert.ToDateTime(DateTime.Now.Year + "/" + DateTime.Now.Month + "/31");
                panel_DT_bookRecive_date.Visible = false;
            }
            else if (com_mode_filter_bookRecive.SelectedIndex == 2)
            {
                DT_bookRecive_date_from.Value = Convert.ToDateTime(DateTime.Now.Year + "/1" + "/1");
                DT_bookRecive_date_to.Value = Convert.ToDateTime(DateTime.Now.Year + "/12" + "/31");
                panel_DT_bookRecive_date.Visible = false;
            }
            else if (com_mode_filter_bookRecive.SelectedIndex == 3)
            {
                panel_DT_bookRecive_date.Visible = true;

            }
        }

        private void btn_fill_doc_Click(object sender, EventArgs e)
        {
            fill_dgv_view_data_doc();
        }

        private void checkBox_search_all_CheckedChanged(object sender, EventArgs e)
        {
            txt_seach.Select();
        }
    }
}

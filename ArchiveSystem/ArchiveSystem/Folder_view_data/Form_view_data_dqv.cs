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
using ArchiveSystem.FollowUp;

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
            Cursor = Cursors.WaitCursor;

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
            Cursor = Cursors.Default;
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




            advanc_dgv_view_data_doc.RowTemplate.Height = 40;

            advanc_dgv_view_data_doc.Columns[0].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
            advanc_dgv_view_data_doc.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 12);


            advanc_dgv_view_data_doc.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;
            advanc_dgv_view_data_doc.RowsDefaultCellStyle.BackColor = Color.LightGray;

            advanc_dgv_view_data_doc.RowsDefaultCellStyle.SelectionBackColor = Color.Orange;
            advanc_dgv_view_data_doc.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            advanc_dgv_view_data_doc.Columns[0].Width = 0;
            advanc_dgv_view_data_doc.Columns[5].Width = 170;
            advanc_dgv_view_data_doc.Columns[6].Width = 350;
            advanc_dgv_view_data_doc.Columns[8].Width = 180;
            advanc_dgv_view_data_doc.Columns[9].Width = 180;
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

        private void numericUpDown_Width_columns_ValueChanged(object sender, EventArgs e)
        {
            this.advanc_dgv_view_data_doc.RowTemplate.Height = Convert.ToInt32(numericUpDown_Width_columns.Value);
            fill_dgv_view_data_doc();
        }

        private void advanc_dgv_view_data_doc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Label2_count_doc_search.Text = Convert.ToString(advanc_dgv_view_data_doc.RowCount);
        }


       
        public static string BookCode;
        private void advanc_dgv_view_data_doc_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
         

                BookCode = advanc_dgv_view_data_doc.CurrentRow.Cells[0].Value.ToString();

                Form_show_docs s_doc1 = new Form_show_docs(BookCode);
                s_doc1.Show();

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

        private void TSM_show_doc_Click(object sender, EventArgs e)
        {
            advanc_dgv_view_data_doc_CellDoubleClick_1(null, null);
        }
       
        public static string BookID;
        public static string date_book;
        public static string sbj_book;
        private void TSM_Add_FollowUp_Click(object sender, EventArgs e)
        {

            BookCode = advanc_dgv_view_data_doc.CurrentRow.Cells[0].Value.ToString();
            BookID = advanc_dgv_view_data_doc.CurrentRow.Cells[1].Value.ToString();
            date_book = advanc_dgv_view_data_doc.CurrentRow.Cells[2].Value.ToString();
            sbj_book = advanc_dgv_view_data_doc.CurrentRow.Cells[5].Value.ToString();
            Form_Manager_FollowUp f_doc1 = new Form_Manager_FollowUp(BookCode, BookID , date_book, sbj_book);
          
            f_doc1.FormBorderStyle = FormBorderStyle.Sizable;
            //f_doc1.WindowState= FormWindowState.Minimized;
            f_doc1.panel_details_doc.Visible = true;

                f_doc1.Show();
           
        }
    }
}

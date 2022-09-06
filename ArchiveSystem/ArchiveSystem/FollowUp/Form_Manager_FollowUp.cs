using ArchiveSystem.Folder_view_data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveSystem.FollowUp
{
    public partial class Form_Manager_FollowUp : Form
    {
        public Form_Manager_FollowUp()
        {
            InitializeComponent();
        }

        string _BookCode = "";
        string _bookID = "";
        string _date_book;
        string _sbj_book;




        public Form_Manager_FollowUp(string BookCode, string bookID, string date_book, string sbj_book)
        {
            _BookCode = BookCode;
            _bookID = bookID;
            _date_book = date_book;
            _sbj_book = sbj_book;


            InitializeComponent();
        }

        //connection string from app config
        public static string _con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con = new SqlConnection(_con);


        void fill_Departments()
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

                listView_Departments.View = View.Details;
                listView_Departments.GridLines = true;
                listView_Departments.Columns.Add("معرف", 25);
                listView_Departments.Columns.Add("القسم", 300);

                foreach (DataRow r in dep.Rows)
                {

                    ListViewItem item = new ListViewItem(r["DepartmentID"].ToString());
                    item.SubItems.Add(r["DepartmentName"].ToString());

                    listView_Departments.Items.AddRange(new ListViewItem[] { item });

                }
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }








        //this function will bring books that have assign to my department
        string FollowStatus = "نشط";

        void fill_FollowUp_send()
        {

            SqlDataAdapter adp1 = new SqlDataAdapter(@"SELECT

             [dbo].[ArchiveFollowUp]. type_follow_task as [النوع],
             [dbo].[ArchiveFollowUp]. BookCode as [كود الكتاب],
             [dbo].[ArchiveFollowUp]. ArchiveBookID as [رقم الكتاب],
             [dbo].[ArchiveFollowUp]. Department_From_me_ID as [من قسم],
        	 [dbo].[Departments_TBL]. DepartmentName as [ارسال الى قسم],
             [dbo].[ArchiveFollowUp].Task as [عنوان الطلب],
             [dbo].[ArchiveFollowUp]. Action as [الاجراء المتخذ],
             [dbo].[ArchiveFollowUp]. Note as [الملاحظات],
             [dbo].[Users_TBL].Username as [موظف الاضافة],
             [dbo].[ArchiveFollowUp]. DateAdded as [تاريخ اضافة],
             [dbo].[ArchiveFollowUp].FollowStatus as [الحالة النهائية],
             [dbo].[ArchiveFollowUp].ArchiveFollowUpID as [المعرف]


                    FROM[ArchiveSystem].[dbo].[ArchiveFollowUp]
               inner JOIN[Departments_TBL] ON[ArchiveFollowUp].[Department_To_you_ID] = [Departments_TBL].[DepartmentID]

               inner JOIN[Users_TBL] ON[ArchiveFollowUp].[User_Add] = [Users_TBL].[UserID]
WHERE([ArchiveFollowUp].Department_From_me_ID = @Param1)

AND ([ArchiveFollowUp].FollowStatus = @Param2)
          ", con);
            adp1.SelectCommand.Parameters.AddWithValue("@Param1", Login._depID);
            adp1.SelectCommand.Parameters.AddWithValue("@Param2", FollowStatus);

            DataTable FollUp_dt = new DataTable();
            FollUp_dt.Clear();

            adp1.Fill(FollUp_dt);
            advanc_dgv_FollowUp.DataSource = FollUp_dt;
            label_count_send.Text = Convert.ToString(FollUp_dt.Rows.Count);
        }

        void fill_FollowUp_recive()
        {

            SqlDataAdapter adp1 = new SqlDataAdapter(@"SELECT

             [dbo].[ArchiveFollowUp]. type_follow_task as [النوع],
             [dbo].[ArchiveFollowUp]. BookCode as [كود الكتاب],
             [dbo].[ArchiveFollowUp]. ArchiveBookID as [رقم الكتاب],
             [dbo].[ArchiveFollowUp]. Department_From_me_ID as [من قسم],
        	 [dbo].[Departments_TBL]. DepartmentName as [مستلم من قسم],
             [dbo].[ArchiveFollowUp].Task as [عنوان الطلب],
             [dbo].[ArchiveFollowUp]. Action as [الاجراء المتخذ],
             [dbo].[ArchiveFollowUp]. Note as [الملاحظات],
             [dbo].[Users_TBL].Username as [موظف الاضافة],
             [dbo].[ArchiveFollowUp]. DateAdded as [تاريخ اضافة],
             [dbo].[ArchiveFollowUp].FollowStatus as [الحالة النهائية],
             [dbo].[ArchiveFollowUp].ArchiveFollowUpID as [المعرف]


                    FROM[ArchiveSystem].[dbo].[ArchiveFollowUp]
               inner JOIN[Departments_TBL] ON[ArchiveFollowUp].[Department_From_me_ID] = [Departments_TBL].[DepartmentID]

               inner JOIN[Users_TBL] ON[ArchiveFollowUp].[User_Add] = [Users_TBL].[UserID]
WHERE([ArchiveFollowUp].Department_To_you_ID = @Param1)

AND ([ArchiveFollowUp].FollowStatus = @Param2)
          ", con);
            adp1.SelectCommand.Parameters.AddWithValue("@Param1", Login._depID);
            adp1.SelectCommand.Parameters.AddWithValue("@Param2", FollowStatus);
            DataTable FollUp_dt = new DataTable();
            FollUp_dt.Clear();

            adp1.Fill(FollUp_dt);
            advanc_dgv_FollowUp.DataSource = FollUp_dt;
            label_count_recive.Text = Convert.ToString(FollUp_dt.Rows.Count);
        }


        private void Form_Manager_FollowUp_Load(object sender, EventArgs e)

        {
            comboB_FollowUp_Title.SelectedIndex = 1;
            contextMenuStrip_FollowUp.Enabled = false;

            txt_book_id.Text = _bookID;
            txt_Date_doc.Text = _date_book;
            txt_sbj_doc.Text = _sbj_book;

            fill_Departments();

            fill_FollowUp_recive();
            fill_FollowUp_send();

            advanc_dgv_FollowUp.Columns[1].Visible = false;
            advanc_dgv_FollowUp.Columns[3].Visible = false;
            advanc_dgv_FollowUp.Columns[11].Visible = false;

            advanc_dgv_FollowUp.Columns[4].Width = 150;
            advanc_dgv_FollowUp.Columns[5].Width = 150;
            advanc_dgv_FollowUp.Columns[7].Width = 200;
        }

        private void BTN_addTask_Click(object sender, EventArgs e)
        {
            if (comboB_FollowUp_Title.Text == "")
            {
                comboB_FollowUp_Title.BackColor = Color.LightSalmon;
                MessageBox.Show("يجب اضافة عنوان", "لم يتم ");
                return;
            }
            else { comboB_FollowUp_Title.BackColor = Color.White; }



            if (listView_Departments.CheckedItems.Count != 0)
            { }

            else
            {
                MessageBox.Show("يجب الاشارة الى قسم واحد على الاقل وذالك من خلال وضع علامة صح", "لم يتم ");
                return;
            }

            //try
            //{


            string task = comboB_FollowUp_Title.Text;
            string DateAdded = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

            ListView.CheckedListViewItemCollection breakfast = this.listView_Departments.CheckedItems;
            con.Open();
            foreach (ListViewItem Item in breakfast)
            {
                int Department_To_you_ID = Convert.ToInt32(listView_Departments.Items[Item.Index].SubItems[0].Text);

                string query = string.Format(@"INSERT INTO [dbo].[ArchiveFollowUp](type_follow_task,BookCode,ArchiveBookID,Department_From_me_ID,Department_To_you_ID,Task,Action,Note,User_Add,DateAdded,FollowStatus)
                          
                         VALUES(@type_follow_task,@BookCode,@ArchiveBookID,@Department_From_me_ID,@Department_To_you_ID,@Task,@Action,@Note,@User_Add,@DateAdded,@FollowStatus)", con);



                SqlCommand cmd = new SqlCommand(query, con);

                //حسب القيمة التي تاتي من فورم عرض جميع الكتب اذا كود الكتاب  يمتلك على قيمة  اذا تسجل ك متابعة كتاب
                //اما اذا لاتوجد قيمة كود الكتاب وكان فارغ اذا تسجل ك مهمة عامة لاتحتوي على كتاب
                string type_follow_task;
                if (_BookCode == "")
                {
                    type_follow_task = "مهمة عامة";
                    _BookCode = "0";
                    _bookID = "بدون مرفق";
                }
                else
                {
                    type_follow_task = "متابعة كتاب";
                }



                cmd.Parameters.Add(new SqlParameter("@type_follow_task", SqlDbType.NVarChar)).Value = type_follow_task;
                cmd.Parameters.Add(new SqlParameter("@BookCode", SqlDbType.NVarChar)).Value = _BookCode;
                cmd.Parameters.Add(new SqlParameter("@ArchiveBookID", SqlDbType.NVarChar)).Value = _bookID;
                cmd.Parameters.Add(new SqlParameter("@Department_From_me_ID", SqlDbType.Int)).Value = Login._depID;
                cmd.Parameters.Add(new SqlParameter("@Department_To_you_ID", SqlDbType.Int)).Value = Department_To_you_ID;
                cmd.Parameters.Add(new SqlParameter("@Task", SqlDbType.NVarChar)).Value = task;
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = "انتظار الاجراء";
                cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar)).Value = "";
                cmd.Parameters.Add(new SqlParameter("@User_Add", SqlDbType.Int)).Value = Login._userID;
                cmd.Parameters.Add(new SqlParameter("@DateAdded", SqlDbType.NVarChar)).Value = DateAdded;
                cmd.Parameters.Add(new SqlParameter("@FollowStatus", SqlDbType.NVarChar)).Value = "نشط";

                int check = (int)cmd.ExecuteNonQuery();

                if (check != 0)
                {
                    if (_BookCode != "")
                    {
                        //con.Open();
                        //final status to doc
                        String strQuery = @"Update ArchiveBooks_TBL set BookStatus = @BookStatus Where BookCode = @BookCode ";
                        SqlCommand cmd1 = new SqlCommand(strQuery, con);
                        cmd1.Parameters.Add(new SqlParameter("@BookCode", SqlDbType.NVarChar)).Value = _BookCode;


                        cmd1.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = "قيد المتابعة";
                        cmd1.ExecuteNonQuery();
                        //con.Close();


                    }

                }

                else if (check == 0)
                {
                    MessageBox.Show("لم يتم ادخال المعلومات ");
                }
            }
            con.Close();

            fill_FollowUp_send();


            MessageBox.Show("تم اضافة متابعة جديدة ");

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

        }



        public static string BookCode;
        private void advanc_dgv_FollowUp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            BookCode = advanc_dgv_FollowUp.CurrentRow.Cells[1].Value.ToString();
            if (BookCode != "0")
            {
                Cursor = Cursors.WaitCursor;
                Form_show_edit_docs s_doc1 = new Form_show_edit_docs(BookCode);
                s_doc1.Show();
                Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("(لايوجد مرفق لان النوع (مهمة عامة) وليس (متابعة كتاب");
            }


        }

        private void TSM_show_doc_Click(object sender, EventArgs e)
        {
            advanc_dgv_FollowUp_CellDoubleClick(null, null);
        }

        private void BTN_editTask_Click(object sender, EventArgs e)
        {
            if (advanc_dgv_FollowUp.RowCount == 0)
            {
                MessageBox.Show("لاتوجد مهام لتعديلها", "لم يتم ");
                return;
            }

            comboB_FollowUp_Title.Text = Convert.ToString(advanc_dgv_FollowUp.CurrentRow.Cells[5].Value);

            string deName = Convert.ToString(advanc_dgv_FollowUp.CurrentRow.Cells[4].Value);
            string item = "";
            for (int x = 0; x < listView_Departments.Items.Count; x++)

            {
                item = Convert.ToString(listView_Departments.Items[x].SubItems[1].Text);
                if (item == deName)
                {


                    listView_Departments.CheckedItems[x].Checked = true;
                }
                else
                {
                    listView_Departments.CheckedItems[x].Checked = false;

                }
            }
        }

        private void BTN_deleteTask_Click(object sender, EventArgs e)
        {
            if (advanc_dgv_FollowUp.RowCount == 0)
            {
                MessageBox.Show("لايمكن الحذف لاتوجد مهام لحذفها", "لم يتم ");
                return;
            }


            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("هل انت متاكد بانك تريد حذف الطلب", "تاكيد", buttons);
            if (result == DialogResult.No)
            {
                return;
            }

            var dgv_CurrentRow_BC = Convert.ToString(advanc_dgv_FollowUp.CurrentRow.Cells[1].Value);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from ArchiveFollowUp where ArchiveFollowUpID = @ArchiveFollowUpID", con);
            cmd.Parameters.Add(new SqlParameter("@ArchiveFollowUpID", SqlDbType.NVarChar)).Value = advanc_dgv_FollowUp.CurrentRow.Cells[11].Value;
            int check = (int)cmd.ExecuteNonQuery();

            if (check != 0)
            {

                con.Close();

                fill_FollowUp_recive();
                fill_FollowUp_send();


                if (FollowStatus != "منتهي")
                {
                    string state = "";
                    for (int x = 0; x < advanc_dgv_FollowUp.Rows.Count; x++)

                    {
                        var CurrentRow_loop = Convert.ToString(advanc_dgv_FollowUp.Rows[x].Cells[1].Value);
                        if (CurrentRow_loop == dgv_CurrentRow_BC)
                        {
                            state = "found";
                            break;
                        }
                        else
                        {
                            state = "no_found";
                        }

                    }

                    if (state == "no_found")
                    {
                        con.Open();
                        //final status to doc
                        String strQuery = @"Update ArchiveBooks_TBL set BookStatus = @BookStatus Where BookCode = @BookCode";
                        SqlCommand cmd1 = new SqlCommand(strQuery, con);
                        cmd1.Parameters.Add(new SqlParameter("@BookCode", SqlDbType.NVarChar)).Value = dgv_CurrentRow_BC;

                        cmd1.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = "بدون متابعة";
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                MessageBox.Show("تم حذف الطب نهائياً");
            }
            else if (check == 0)
            {
                MessageBox.Show("لم تتم العملية ");
            }




        }

        private void btn_end_status_FollowUp_Click(object sender, EventArgs e)
        {
            if (advanc_dgv_FollowUp.RowCount == 0)
            {
                MessageBox.Show("لايمكن الانهاء لاتوجد مهام لانهائها", "لم يتم ");
                return;
            }


            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("هل انت متاكد بانك تريد انهاء الطلب", "تاكيد", buttons);
            if (result == DialogResult.No)
            {
                return;
            }

            var dgv_CurrentRow_BC = Convert.ToString(advanc_dgv_FollowUp.CurrentRow.Cells[1].Value);
            con.Open();

            //final status to doc
            String strQuery = @"Update ArchiveFollowUp set FollowStatus = @FollowStatus Where ArchiveFollowUpID = @ArchiveFollowUpID";
            SqlCommand cmd = new SqlCommand(strQuery, con);
            cmd.Parameters.Add(new SqlParameter("@ArchiveFollowUpID", SqlDbType.NVarChar)).Value = advanc_dgv_FollowUp.CurrentRow.Cells[11].Value;

            cmd.Parameters.Add(new SqlParameter("@FollowStatus", SqlDbType.NVarChar)).Value = "منتهي";

            int check = (int)cmd.ExecuteNonQuery();

            if (check != 0)
            {

                con.Close();

                fill_FollowUp_recive();
                fill_FollowUp_send();

                string state = "";
                for (int x = 0; x < advanc_dgv_FollowUp.Rows.Count; x++)

                {
                    var CurrentRow_loop = Convert.ToString(advanc_dgv_FollowUp.Rows[x].Cells[1].Value);
                    if (CurrentRow_loop == dgv_CurrentRow_BC)
                    {
                        state = "found";
                        break;
                    }
                    else
                    {
                        state = "no_found";
                    }

                }

                if (state == "no_found")
                {
                    con.Open();
                    //final status to doc
                    String strQuery1 = @"Update ArchiveBooks_TBL set BookStatus = @BookStatus Where BookCode = @BookCode";
                    SqlCommand cmd1 = new SqlCommand(strQuery1, con);
                    cmd1.Parameters.Add(new SqlParameter("@BookCode", SqlDbType.NVarChar)).Value = dgv_CurrentRow_BC;

                    cmd1.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = "بدون متابعة";
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تم انهاء الطب ولايمكن مشاهدة من باقي الاقسام و يمكن مشاهدة الطلب في خانة الطلبات المنتهية لديك");
                }


            }
            else if (check == 0)
            {
                MessageBox.Show("لم تتم العملية ");
            }





        }

        private void btn_active_status_FollowUp_Click(object sender, EventArgs e)
        {
            if (advanc_dgv_FollowUp.RowCount == 0)
            {
                MessageBox.Show("لايمكن التنشيط لاتوجد مهام لتنشيطها", "لم يتم ");
                return;
            }


            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("هل انت متاكد بانك تريد اعادة تنشيط هذا الطلب", "تاكيد", buttons);
            if (result == DialogResult.No)
            {
                return;
            }

            var dgv_CurrentRow_BC = Convert.ToString(advanc_dgv_FollowUp.CurrentRow.Cells[1].Value);
            con.Open();

            //final status to doc
            String strQuery = @"Update ArchiveFollowUp set FollowStatus = @FollowStatus Where ArchiveFollowUpID = @ArchiveFollowUpID";
            SqlCommand cmd = new SqlCommand(strQuery, con);
            cmd.Parameters.Add(new SqlParameter("@ArchiveFollowUpID", SqlDbType.NVarChar)).Value = advanc_dgv_FollowUp.CurrentRow.Cells[11].Value;

            cmd.Parameters.Add(new SqlParameter("@FollowStatus", SqlDbType.NVarChar)).Value = "نشط";

            int check = (int)cmd.ExecuteNonQuery();

            if (check != 0)
            {

                con.Close();


                fill_FollowUp_recive();
                fill_FollowUp_send();



                con.Open();
                //final status to doc
                String strQuery1 = @"Update ArchiveBooks_TBL set BookStatus = @BookStatus Where BookCode = @BookCode";
                SqlCommand cmd1 = new SqlCommand(strQuery1, con);
                cmd1.Parameters.Add(new SqlParameter("@BookCode", SqlDbType.NVarChar)).Value = dgv_CurrentRow_BC;

                cmd1.Parameters.Add(new SqlParameter("@BookStatus", SqlDbType.NVarChar)).Value = "قيد المتابعة";
                cmd1.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("تم اعادة تنشيط الطب يمكن مشاهدة الطلب في خانة الطلبات النشطة");

            }
            else if (check == 0)
            {
                MessageBox.Show("لم تتم العملية ");
            }

        }



        private void tSMenuItem_FollowUp_Save_Click(object sender, EventArgs e)
        {
            if (tSComBox_FollowUp_type.Text == "")
            {
                tSComBox_FollowUp_type.BackColor = Color.LightSalmon;
                MessageBox.Show("يجب اختيار اجراء", "لم يتم ");
                return;
            }
            else { tSComBox_FollowUp_type.BackColor = Color.White; }

            con.Open();

            String strQuery = @"Update ArchiveFollowUp set Action = @Action, Note = @Note Where ArchiveFollowUpID = " + advanc_dgv_FollowUp.CurrentRow.Cells[11].Value;

            SqlCommand cmd1 = new SqlCommand(strQuery, con);

            string not = "* التاريخ:- " + DateTime.Now + "   " + "*المستخدم:- " + Login._userID + "-" + Login._user + "   " + "*الاجراء المتخذ:- " + tSComBox_FollowUp_type.SelectedItem + "   " + "*الملاحظلات المضافة:- " + tSTXT_FollowUp_Not.Text;
            not = advanc_dgv_FollowUp.CurrentRow.Cells[7].Value + "      ـــــــــــــــــــــــــــــ     " + not;
            cmd1.Parameters.Add(new SqlParameter("@Action", SqlDbType.NVarChar)).Value = tSComBox_FollowUp_type.SelectedItem;
            cmd1.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar)).Value = not;

            cmd1.ExecuteNonQuery();


            con.Close();

            fill_FollowUp_recive();

            tSComBox_FollowUp_type.Text = "";
            tSTXT_FollowUp_Not.Text = "";
        }

        private void btn_task_send_Click(object sender, EventArgs e)
        {
            btn_end_all_followup_comleted.Visible = true;

            fill_FollowUp_send();
            panel_state_send_recive.BackColor = Color.Teal;

            contextMenuStrip_FollowUp.Enabled = false;
            BTN_addTask.Enabled = true;
            btn_Active_FollowStatus.Visible = true;
            btn_end_FollowStatus.Visible = true;
            txt_not.Visible = true;
            panel_contener_buttons.Visible = true;
        }

        private void btn_task_recive_Click(object sender, EventArgs e)
        {

            FollowStatus = "نشط";
            fill_FollowUp_recive();
            panel_state_send_recive.BackColor = Color.Firebrick;
            contextMenuStrip_FollowUp.Enabled = true;
            BTN_addTask.Enabled = false;
            btn_Active_FollowStatus.Visible = false;
            btn_end_FollowStatus.Visible = false;
            txt_not.Visible = false;
            panel_contener_buttons.Visible = false;

        }

        private void btn_Active_FollowStatus_Click(object sender, EventArgs e)
        {
            btn_end_all_followup_comleted.Visible = true;

            FollowStatus = "نشط";
            btn_end_status_FollowUp.Visible = true;
            btn_active_status_FollowUp.Visible = false;
            fill_FollowUp_send();
            txt_not.Text = "الطلبات التي قمت بارسالها وبانتظار اتخاذ الاجراء المناسب لها من الاقسام التي تم الاشارة لها";
        }

        private void btn_end_FollowStatus_Click(object sender, EventArgs e)
        {
            btn_end_all_followup_comleted.Visible = false;

            FollowStatus = "منتهي";
            btn_end_status_FollowUp.Visible = false;
            btn_active_status_FollowUp.Visible = true;
            fill_FollowUp_send();
            txt_not.Text = "الطلبات التي تم اتخاذ الاجراءالمناسب لها وقمت بانهائها علماً هذة الطلبات لاتظهر بعد الانهاء عند باقي الاقسام التي تم الاشارة لها ";
        }

        private void label_count_recive_Click(object sender, EventArgs e)
        {

        }

        private void btn_end_all_followup_comleted_Click(object sender, EventArgs e)
        {
            if (advanc_dgv_FollowUp.RowCount == 0)
            {
                MessageBox.Show("لايمكن الانهاء لاتوجد مهام او متابعات لانهائها", "لم يتم ");
                return;
            }


            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("هل انت متاكد بانك تريد انهاء جميع المتابعات او المهام المكتملة", "تاكيد", buttons);
            if (result == DialogResult.No)
            {
                return;
            }
            int[] followupid_array = new int[1000];
            //int[] intgerArray = new int[] { };

            int i = 0;
            foreach (DataGridViewRow row in advanc_dgv_FollowUp.Rows)
            {

                if (row.Cells[6].Value.ToString() == "مكتمل")
                {

                    int folw_id = Convert.ToInt32(row.Cells[11].Value.ToString());
                    //followupid_array = followupid_array.Concat(new[] { folw_id }).ToArray(); // another way to add item to array 

                    followupid_array[i] = folw_id;
                    i++;
                    //int[] terms = new int[400];
                    //for (int runs = 0; runs < 400; runs++)
                    //{
                    //    terms[runs] = value;
                    //}
                }



            }
            if (followupid_array[0] == 0)
            {
                MessageBox.Show("لا توجد متابعات مكتملة او مهام لانهائها");
                
            }
            else
            {
                for (int x = 0; x < followupid_array.Length; x++)
                {


                    string id = followupid_array[x].ToString();
                    if (id =="0")
                    { break; }
                     
                    con.Open();
                    String strQuery = @"Update ArchiveFollowUp set FollowStatus = @FollowStatus Where ArchiveFollowUpID = @ArchiveFollowUpID";
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@ArchiveFollowUpID", SqlDbType.NVarChar)).Value = id;

                    cmd.Parameters.Add(new SqlParameter("@FollowStatus", SqlDbType.NVarChar)).Value = "منتهي";

                    cmd.ExecuteNonQuery();
                    con.Close();



                }
                MessageBox.Show("تم انهاء المتابعات المكتملة او المهام ولايمكن مشاهدتها من باقي الاقسام و يمكن مشاهدة الطلب في خانة الطلبات المنتهية لديك");

            }




            fill_FollowUp_recive();
            fill_FollowUp_send();


         


        }
        //else if (followupid_array.Length <= 0)
        //{

        //    MessageBox.Show("لا توجد متابعات مكتملة او مهام لانهائها");



        //}




    }
}
 

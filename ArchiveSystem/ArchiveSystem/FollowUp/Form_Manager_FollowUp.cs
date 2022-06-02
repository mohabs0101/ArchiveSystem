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

namespace ArchiveSystem.FollowUp
{
    public partial class Form_Manager_FollowUp : MetroFramework.Forms.MetroForm
    {
        public Form_Manager_FollowUp()
        {
            InitializeComponent();
        }

        //connection string from app config
        public static string _con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con = new SqlConnection(_con);

        //this function will bring books that have assign to my department
         void fill_FollowUp()
        {

            string query = string.Format(@"  SELECT
      ArchiveFollowUpID as [معرف المهمة],
      ArchiveBookID as [رقم الكتاب],
	  Department_AssignTO_ID as [الى القسم],
Task as [المهمة],
Action as [الاجراء],
Note as [الملاحظات],
DateAdded as [تاريخ اضافة المهة],

            FROM[ArchiveSystem].[dbo].[ArchiveFollowUp]
  ", con);
//DepID
 //inner JOIN[ArchiveBooks_TBL] ON[ArchiveFollowUp].ArchiveBookID = [ArchiveBooks_TBL].ArchiveBookID
 //     inner JOIN[BooksType_TBL] ON[ArchiveBooks_TBL].[BooksTypeID] = [BooksType_TBL].[BooksTypeID]
	//     inner JOIN[Departments_TBL] ON[ArchiveBooks_TBL].[DepartmentID_archivedBy] = [Departments_TBL].[DepartmentID]
	//     inner JOIN[Users_TBL] ON[ArchiveBooks_TBL].[UserID_archivedBy] = [Users_TBL].[UserID]
	    
 //  where[ArchiveFollowUp].[Department_AssignTO_ID] = {0}


            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable AssignBooks = new DataTable();

            adp.Fill(AssignBooks);
            advanc_dgv_FollowUp.DataSource = AssignBooks;

            con.Close();



        }


        private void Form_Manager_FollowUp_Load(object sender, EventArgs e)
        {
            fill_FollowUp();
        }
    }
}

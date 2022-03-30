using ArchiveSystem.Folder_view_data;
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

namespace ArchiveSystem.Folder_Tracker
{
    public partial class Form_Tracker_Procedure : MetroFramework.Forms.MetroForm
    {
        public Form_Tracker_Procedure()
        {
            InitializeComponent();
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

            adapter = new SqlDataAdapter(@"SELECT 

dbo.Departments_TBL.DepartmentName as [القسم],
dbo.[Assign&Comment_TBL].Question as [المهمة],
dbo.[Assign&Comment_TBL].Comment as [الاجراء],
dbo.[Assign&Comment_TBL].note_me as [الملاحظات],
dbo.[Assign&Comment_TBL].date_add as [تاريخ الاضافة]

FROM     dbo.ArchiveBooks_TBL INNER JOIN
                  dbo.[Assign&Comment_TBL] ON dbo.ArchiveBooks_TBL.ArchiveBookID = dbo.[Assign&Comment_TBL].ArchiveBookID INNER JOIN
                  dbo.Departments_TBL ON dbo.[Assign&Comment_TBL].DepartmentID = dbo.Departments_TBL.DepartmentID
WHERE  ([Assign&Comment_TBL].ArchiveBookID) = @Param1

                ", con);
            adapter.SelectCommand.Parameters.AddWithValue("@Param1", Form_show_doc.book_ID);

            dt.Clear();

            adapter.Fill(dt);
            advanc_dgv_Assign_Comment.DataSource = dt;
        }

        private void Form_Tracker_Procedure_Load(object sender, EventArgs e)
        {
            Select_Departments();

            Assign_Comment_TBL();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

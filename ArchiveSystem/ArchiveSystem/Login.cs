using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveSystem
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string _user;
        public static int _userID;
        public static int _depID;
        public static int _permitionTYpeID;


        public static string _con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlConnection con = new SqlConnection(_con);

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            try
            {
                string username = TXT_User.Text;
                string password = TXT_pass.Text;

                string query = string.Format(@"SELECT   [UserID]
      ,[Username]
      ,[Password]
      ,[FullName]
      ,[DepartmentID]
      ,[PermitionTypeID]
      ,[PhoneNumber]
  FROM [ArchiveSystem].[dbo].[Users_TBL] where username=N'{0}' and password='{1}'", username, password, con);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                con.Close();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    int userID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    int depID = Convert.ToInt32(dt.Rows[0]["DepartmentID"]);

                    string user = dt.Rows[0]["Username"].ToString();
                    int permitiontypeID = Convert.ToInt32(dt.Rows[0]["PermitionTypeID"].ToString());

                    _user = user.ToString();
                    _userID = userID;
                    _depID = depID;
                    _permitionTYpeID = permitiontypeID;

                    Form1 f1 = new Form1();
                    f1.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("اسم المستخدم او الرمز  غير صحيح");
                }

        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

}
        //login with enter key button
        private void TXT_pass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BTN_Login.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "حدث خطأ يرجى المحاولة مجددا", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, true);
            }

        }
        void Auto_complet_username()
        //نظع هذا السب في حدث الفورم لود
        {
            try
            {
                string query = string.Format(@"SELECT [Username]FROM [ArchiveSystem].[dbo].[Users_TBL]", con);

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
                this.TXT_User.AutoCompleteCustomSource = ds;
                this.TXT_User.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.TXT_User.AutoCompleteMode = AutoCompleteMode.Append;
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void Login_Load(object sender, EventArgs e)
        {


            //اولا نحذف جميع الفولدرات المنزلة سابقا تحسبا لعدم حذفها بسبب استخدامه من قبل المعالج
            try
            {
                Auto_complet_username();


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
            }
            catch (Exception ex)
            {

            }
            this.TopMost = false;
        }
    }
}

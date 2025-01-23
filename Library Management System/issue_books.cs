using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class issue_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=library_managemant_system;Integrated Security=True;");

        public issue_books()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  from student_info where student_enrollment_no '" + tet_enrollment.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if(i==0 )
            {
                MessageBox.Show("This.enrollment no not found");
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tet_studentname.Text = dr["student_name"].ToString();
                    tet_studentdept.Text = dr["student_department"].ToString();
                    tet_studentsem.Text = dr["student_sem"].ToString();
                    tet_studentcontact.Text = dr["student_contact"].ToString();
                    tet_studentemail.Text = dr["student_email"].ToString();
                }
            }
        }

        private void issue_books_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

        }
    }
}

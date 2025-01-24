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
            try
            {
                // Initialize a parameterized query to prevent SQL Injection
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM student_info WHERE student_enrollment_no = @enrollment_no";
                cmd.Parameters.AddWithValue("@enrollment_no", tet_enrollment.Text);

                // Use SqlDataAdapter to fill a DataTable
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("This enrollment number was not found.");
                }
                else
                {
                    // Populate the text boxes with retrieved data
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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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

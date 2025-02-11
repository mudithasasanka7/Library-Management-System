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
                int i = 0;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM student_info WHERE student_enrollment_no = '" + tet_enrollment.Text +"'";
                cmd.ExecuteNonQuery();
                //cmd.Parameters.AddWithValue("@enrollment_no", tet_enrollment.Text);
                
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

        private void txt_boksname_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;
            if (e.KeyCode != Keys.Enter)
            {
                listBox1.Items.Clear();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + txt_boksname.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                if (count > 0)
                {
                    listBox1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["books_name"].ToString());

                    }

                }
            }
        }

        private void txt_boksname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;

            }

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_boksname.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txt_boksname.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible=false; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int books_qty = 0;
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from books_info where books_name = '" + txt_boksname.Text+ "'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach(DataRow dr2 in dt2.Rows)
            {
                books_qty = Convert.ToInt32(dr2["available_qty"].ToString());
            }

            if (books_qty > 0)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into issue_books values('" + tet_enrollment.Text + "','" + tet_studentname.Text + "','" + tet_studentdept.Text + "','" + tet_studentsem.Text + "','" + tet_studentcontact.Text + "','" + tet_studentemail.Text + "','" + txt_boksname.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "','')";
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update books_info set available_qty = available_qty - 1 where books_name = '" + txt_boksname.Text + "'";
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Books issue Successfully.");
            }
            else
            {
                MessageBox.Show("Books Not available.");
            }
        }
    }
}

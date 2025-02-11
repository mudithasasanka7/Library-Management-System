﻿using System;
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
    public partial class books_stock : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=library_managemant_system;Integrated Security=True;");

        public books_stock()
        {
            InitializeComponent();
        }

        private void books_stock_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            fill_books_info();
        }
        public void fill_books_info()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT books_name,books_author_name,books_quantity,available_qty from books_info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string i;
            i = dataGridView1.SelectedCells[0].Value.ToString();
            
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM issue_books WHERE books_name='" + i.ToString()+"' and book_return_date=''";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT books_name,books_author_name,books_quantity,available_qty from books_info WHERE books_name link('" + textBox1.Text+"')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}

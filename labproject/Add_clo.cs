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

namespace labproject
{
    public partial class Add_clo : Form
    {
        public Add_clo()
        {
            InitializeComponent();
        }
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

            }
            else
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                string check = "Select * from Clo where Name='" + textBox1.Text + "' ";
                SqlCommand comcheck = new SqlCommand(check, con);
                SqlDataAdapter adapt = new SqlDataAdapter();
                adapt.SelectCommand = new SqlCommand(check, con);
                DataTable ds = new DataTable();
                adapt.Fill(ds);
                int i = ds.Rows.Count;
                if (i > 0)
                {
                    MessageBox.Show("Clo with this name Already Exists");
                    ds.Clear();
                }
                else
                {
                    DateTime? d = null;
                    DateTime nowdate = DateTime.Now;
                    if (con.State == ConnectionState.Open)
                    {
                        string query = "INSERT INTO  Clo (Name,DateCreated,DateUpdated)VALUES ('" + textBox1.Text + "','" + nowdate + "','" + d + "')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Inserted");
                        textBox1.Text = "";

                    }

                    else
                    {
                        MessageBox.Show("Erro occure while inserting");
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLO obj = new CLO();
           this.Hide();
            obj.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Show_student obj = new Show_student();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attendance obj = new Attendance();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void Add_clo_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Student_result obj = new Student_result();
            this.Hide();
            obj.Show();

        }
    }
}

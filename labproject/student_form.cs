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
    public partial class student_form : Form
    {
        public student_form()
        {
            InitializeComponent();
        }

        //public string constr = "server=DESKTOP-G0K5DQK;database=ProjectB;";

        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            String cmds = "SELECT * FROM Lookup" ;
            SqlCommand command = new SqlCommand(cmds, con);
          
            SqlDataReader reader = command.ExecuteReader();

            int columnValue;
            string m;
            if (con.State == ConnectionState.Open)
            {
                while (reader.Read())
                {
                    if (reader[1].ToString() == comboBox1.Text)
                    {
                        m = reader[1].ToString();
                        columnValue = Convert.ToInt32(reader[0]);
                        string query = "INSERT INTO  Student (FirstName, LastName, Contact,Email,RegistrationNumber,Status)VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + columnValue + "')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("successful");
                    }
                }
                }
                   
            else
            {
                MessageBox.Show("erro");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attendance obj = new Attendance();
            this.Hide();
            obj.Show();
        }
    }
}

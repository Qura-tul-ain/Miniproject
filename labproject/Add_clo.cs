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
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            DateTime? d = null;
            DateTime nowdate = DateTime.Now;
            if (con.State == ConnectionState.Open)
            {
                string query = "INSERT INTO  Clo (Name,DateCreated,DateUpdated)VALUES ('" + textBox1.Text + "','" + nowdate + "','" + d + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful");
             
            }

            else
            {
                MessageBox.Show("erro");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLO obj = new CLO();
           this.Hide();
            obj.Show();
        }
    }
}

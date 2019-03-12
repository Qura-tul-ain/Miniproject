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
    public partial class CLO : Form
    {
        public CLO()
        {
            InitializeComponent();
        }
        public static string publicCloId;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            DateTime? d = null;
            DateTime nowdate = DateTime.Now;
            if (con.State == ConnectionState.Open)
            {
                string query = "INSERT INTO  Clo (Name,DateCreated,DateUpdated)VALUES ('" + textBox1.Text + "','"+ nowdate+ "','"+ d +"')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successful");
                show();
            }

            else
            {
                MessageBox.Show("erro");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Level_display obj = new Level_display();
            this.Hide();
            obj.Show();
        }

        public void show()
        {
            
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;


            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = "SELECT * FROM Clo" ;

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);

        }

        private void CLO_Load(object sender, EventArgs e)
        {
          
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;


            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = "SELECT * FROM Clo";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /* if (e.RowIndex != -1)
           {
               DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
               publicCloId = rows.Cells[2].Value.ToString();
               Add_rubrics form_rublevel = new Add_rubrics();
               form_rublevel.Show();

           }*/
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    publicCloId = rows.Cells[2].Value.ToString();
                    Add_rubrics form_rublevel = new Add_rubrics();
                    form_rublevel.Show();
                }
            }
            else if (e.ColumnIndex == 1)
            {
                MessageBox.Show("edit");
            }

        }
    }
    
    
}

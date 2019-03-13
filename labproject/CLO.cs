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
        public string globalid;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                //Open the connection to db
                conn.Open();
                DateTime nowdate = DateTime.Now;
                string query = "UPDATE Clo set Name='" + textBox1.Text + "' ,DateUpdated='" +nowdate+ "' where Id='" + globalid+"'";
                SqlCommand cmd = new SqlCommand(query, conn);
                //  SqlDataAdapter adapter = new SqlDataAdapter();
                //  adapter.SelectCommand = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Updated");
             
                while(reader.Read())
                {

                }
                show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;
            dataGridView1.Columns["Delete"].DisplayIndex = 6;

        }

        private void CLO_Load(object sender, EventArgs e)
        {
          
            SqlConnection conn = new SqlConnection(constr);
            //Open the connection to db
            conn.Open();
            string query;
            SqlCommand SqlCommand;
            SqlDataAdapter adapter = new SqlDataAdapter();
            

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
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;
            dataGridView1.Columns["Delete"].DisplayIndex = 6;
            /*  DataGridViewComboBoxColumn col1 = new DataGridViewComboBoxColumn();
              col1.HeaderText = "Edit";
              col1.ToolTipText = "Edit";
              col1.Name = "Edit";

              dataGridView1.Columns[4].add;
              dataGridView1.Columns[1].Visible = false;*/


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
            if (e.ColumnIndex == 0)//rubrics
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    publicCloId = rows.Cells[3].Value.ToString();//column3 is containing id
                    Add_rubrics form_rublevel = new Add_rubrics();
                    form_rublevel.Show();
                }
            }
            else if (e.ColumnIndex == 1)//edit
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                   globalid = rows.Cells[3].Value.ToString();
                  textBox1.Text= rows.Cells[4].Value.ToString();
                }
            }
            else if (e.ColumnIndex == 2)// delete
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    globalid = rows.Cells[3].Value.ToString();

                    try
                    {
                        SqlConnection conn = new SqlConnection(constr);
                        //Open the connection to db
                        conn.Open();

                        //  string q="DELETE * FROM Clo inner JOIN Rubric ON Clo.Id = Rubric.CloId LEFT JOIN RubricLevel ON Rubric.Id=RbricLevel.RubricId WHERE Clo.Id = '" + globalid+ " '";
                        //   string q ="DELETE FROM Clo, Rubric , RubricLevel USING Clo, Rubric , RubricLevel WHERE Clo.Id=Rubric.CloId and Rubric.Id=RubricLevel.RubricId and Clo.Id = '" + globalid + " '";
                        string q1 = "Select count(Id) from Rubric where CloId='" + globalid + "' GROUP BY CloId having count(Id)>1";
                        SqlCommand cmd1 = new SqlCommand(q1, conn);//no of same id
                        int jj = Convert.ToInt32(cmd1.ExecuteScalar());
                        string q2 = "Select * from Rubric where CloId='" + globalid + "'";
                        SqlCommand cmd11 = new SqlCommand(q2, conn);//id of first rubric having sme cloId
                        int ii = Convert.ToInt32(cmd11.ExecuteScalar());
                        int k;
                        for (int j = 0; j < jj; j++)
                        {
                            k = j + ii;
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                string delete_rubric_level = "DELETE RubricLevel WHERE EXISTS ( SELECT * FROM Rubric WHERE Rubric.Id= RubricLevel.RubricId and Rubric.Id ='" + k + "')";
                                SqlCommand cmd2 = new SqlCommand(delete_rubric_level, conn);
                                SqlDataReader reader1 = cmd2.ExecuteReader();
                            }
                        }
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            string delete_rubric = "DELETE Rubric WHERE EXISTS ( SELECT * FROM Clo WHERE Rubric.CloId= Clo.Id and Clo.Id ='" + globalid + "')";
                            SqlCommand cmd3 = new SqlCommand(delete_rubric, conn);
                            SqlDataReader reader2 = cmd3.ExecuteReader();
                        }

                        string delete_clo = "DELETE from Clo WHERE Clo.Id ='" + globalid + "'";
                        SqlCommand cmd4 = new SqlCommand(delete_clo, conn);
                        SqlDataReader reader3 = cmd4.ExecuteReader();

                        MessageBox.Show("Deleted");

                       
                        show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            else
            {
                MessageBox.Show("Click rightly box");
            }

        }
    }
    
    
}

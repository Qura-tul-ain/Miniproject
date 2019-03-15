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
        private void button1_Click(object sender, EventArgs e)//edit 
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Select data to Edit");
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection(constr);
                    //Open the connection to db
                    conn.Open();
                    DateTime nowdate = DateTime.Now;
                    string query = "UPDATE Clo set Name='" + textBox1.Text + "' ,DateUpdated='" + nowdate + "' where Id='" + globalid + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Successfully Updated");

                    while (reader.Read())
                    {

                    }
                    show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_display_levelcs obj = new Add_display_levelcs();
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
        


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.ColumnIndex == 0)//rubrics
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    publicCloId = rows.Cells[3].Value.ToString();//column3 is containing id
                    if (publicCloId == "")
                    {
                        MessageBox.Show("First enter Clo");
                    }
                    else
                    {
                        Add_display_rubrics form_rub = new Add_display_rubrics();
                        this.Hide();
                        form_rub.Show();
                    }
                }
            }//end of rubric

            else if (e.ColumnIndex == 1)//edit
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                   globalid = rows.Cells[3].Value.ToString();
                  textBox1.Text= rows.Cells[4].Value.ToString();
                }
            }//end of edit

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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_clo obj = new Add_clo();
            this.Hide();
            obj.Show();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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
    }
    
    
}

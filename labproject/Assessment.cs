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
    public partial class Assessment : Form
    {
        public Assessment()
        {
            InitializeComponent();
        }
        public static int assessment_id;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query;
            DateTime date = DateTime.Now;
            query= "INSERT INTO Assessment(Title,DateCreated,TotalMarks,TotalWeightage)VALUES('" + textBox1.Text + "', '" + date + "', '" + textBox2.Text + "', '" + textBox3.Text + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            show();
         
           
        }
        public void show()
        {
            // load data in grid view 
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string showdata = "Select  Assessment.Id, Assessment.Title, Assessment.TotalMarks,Assessment.TotalWeightage from Assessment";
            SqlCommand SqlCommand = new SqlCommand(showdata, conn);
            adapter.SelectCommand = new SqlCommand(showdata, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns["Delete"].DisplayIndex = 6;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;
        }

        private void Assessment_Load(object sender, EventArgs e)
        {
            // load data in grid view 
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string showdata = "Select  Assessment.Id, Assessment.Title, Assessment.TotalMarks,Assessment.TotalWeightage from Assessment";
            SqlCommand SqlCommand = new SqlCommand(showdata, conn);
            adapter.SelectCommand = new SqlCommand(showdata, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            dataGridView1.Columns[3].Visible = false;
          
            dataGridView1.Columns["Delete"].DisplayIndex = 6;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            //Open the connection to db
            conn.Open();
            if (e.ColumnIndex == 0)//assessment id
            {

                if (e.RowIndex != -1)//delete
                {
                   
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    assessment_id = Convert.ToInt32(rows.Cells[3].Value);//column3 is containing id

                 
                    string q1 = "Select count(Id) from AssessmentComponent where AssessmentId='" + assessment_id + "' GROUP BY AssessmentId having count(Id)>=1";
                    SqlCommand cmd1 = new SqlCommand(q1, conn);//no of same id
                    int jj = Convert.ToInt32(cmd1.ExecuteScalar());
                    string q2 = "Select * from AssessmentComponent where AssessmentId='" + assessment_id + "' ";
                    SqlCommand cmd11 = new SqlCommand(q2, conn);//id of first comp having sme assessment_id
                    int ii = Convert.ToInt32(cmd11.ExecuteScalar());
                    int k;
                    for (int j = 0; j < jj; j++)
                    {
                        k = j + ii;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {

                            string delete_result = "DELETE  StudentResult WHERE EXISTS( SELECT * FROM AssessmentComponent WHERE AssessmentComponent.Id=StudentResult.AssessmentComponentId and AssessmentComponent.Id ='" + k + "')";
                            SqlCommand cmd = new SqlCommand(delete_result, conn);
                            SqlDataReader reader1 = cmd.ExecuteReader();
                        }
                    }

                    

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        
                        string delete_component = "DELETE AssessmentComponent WHERE EXISTS ( SELECT * FROM Assessment WHERE Assessment.Id= AssessmentComponent.AssessmentId and Assessment.Id ='" + assessment_id + "')";
                        SqlCommand cmd3 = new SqlCommand(delete_component, conn);
                        SqlDataReader reader2 = cmd3.ExecuteReader();
                    }
             
                    string delete = "DELETE from Assessment WHERE Id ='" + assessment_id + "'";
                    SqlCommand cmd2 = new SqlCommand(delete, conn);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    MessageBox.Show("Successfully deleted");
                    show();

                }
            }//end of delete

            if (e.ColumnIndex == 1)//edit
            {
                if (e.RowIndex != -1)
                {
                   
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    assessment_id =Convert.ToInt32( rows.Cells[3].Value);
                
                    textBox1.Text = rows.Cells[4].Value.ToString();
                    textBox2.Text = rows.Cells[5].Value.ToString();
                    textBox3.Text = rows.Cells[6].Value.ToString();
                   


                }
            }//end of edit
            if (e.ColumnIndex == 2)//manage component
            {
                Assess_component obj = new Assess_component();
                this.Hide();
                obj.Show();

            }
            else
            {
                MessageBox.Show("Click on right button");
            }


            }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Kindly First Select data");
            }
            else
            {

                SqlConnection conn = new SqlConnection(constr);
                //Open the connection to db
                conn.Open();
                string sql = "UPDATE Assessment set Title='" + textBox1.Text + "'  ,TotalMarks='" + textBox2.Text + "',TotalWeightage='" + textBox3.Text + "'  where Assessment.Id='" + assessment_id + "'";
                SqlCommand insert = new SqlCommand(sql, conn);
                insert.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated Component");
                show();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }
    }
}

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
    public partial class Show_student : Form
    {
        public Show_student()
        {
            InitializeComponent();
        }
        public string studentid;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_clo obj = new Add_clo();
            this.Hide();
            obj.Show();
        }

        private void Show_student_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            //Open the connection to db
            conn.Open();
            string query;
            SqlCommand SqlCommand;
            SqlDataAdapter adapter = new SqlDataAdapter();


            //Generating the query to fetch the contact details
            query = "SELECT * FROM Student";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);
            //  dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[9].Visible = false;//hide actual status containing integer value,i.e 5 or 6
            dataGridView1.Columns["St"].DisplayIndex = 9;
            dataGridView1.Columns["Edit"].DisplayIndex = 9;// thses three columns are at stating indexes
            dataGridView1.Columns["Delete"].DisplayIndex = 9;

            int value = 5;
            int value2 = 6;
            for (int i=0 ; i<dataGridView1.RowCount; i++)
            {
                 if(Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) == value)
                {
                  
                    dataGridView1.Rows[i].Cells["St"].Value = "Active";
                }
                else if(Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) == value2)
                {
                    dataGridView1.Rows[i].Cells["St"].Value = "InActive";
                }
                else
                {

                }
            }




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
            query = "SELECT * FROM Student";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);
           dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns["St"].DisplayIndex = 9;
            dataGridView1.Columns["Edit"].DisplayIndex = 9;
            dataGridView1.Columns["Delete"].DisplayIndex = 9;
            int value = 5;
            int value2 = 6;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) == value)
                {

                    dataGridView1.Rows[i].Cells["St"].Value = "Active";
                }
                else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value) == value2)
                {
                    dataGridView1.Rows[i].Cells["St"].Value = "InActive";
                }
                else
                {

                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string student_id;
            if (e.ColumnIndex == 0)//student id
            {

                if (e.RowIndex != -1)//delete
                {
                    SqlConnection conn = new SqlConnection(constr);
                    //Open the connection to db
                    conn.Open();
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                     student_id= rows.Cells[3].Value.ToString();//column3 is containing id
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        string delete_StudentAttendance = "DELETE StudentAttendance WHERE EXISTS ( SELECT * FROM Student WHERE Student.Id = StudentAttendance.StudentId and Student.Id ='" + student_id + "')";
                        SqlCommand cmd1 = new SqlCommand(delete_StudentAttendance, conn);
                        SqlDataReader reader1 = cmd1.ExecuteReader();
                    }
                    int id = Convert.ToInt32(student_id);
                    string delete_clo = "DELETE from Student WHERE Id ='" + id + "'";
                    SqlCommand cmd = new SqlCommand(delete_clo, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Successfully deleted");
                    show();

                }
            }//end of delete
            else if (e.ColumnIndex == 1)//edit
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    studentid = rows.Cells[3].Value.ToString();
                    textBox1.Text = rows.Cells[4].Value.ToString();
                    textBox2.Text = rows.Cells[5].Value.ToString();
                    textBox3.Text = rows.Cells[6].Value.ToString();
                    textBox4.Text = rows.Cells[7].Value.ToString();
                    textBox5.Text = rows.Cells[8].Value.ToString();
                    if(rows.Cells[9].Value.ToString()== "5")
                    {
                        comboBox1.Text = "Active";
                    }
                    else if (rows.Cells[9].Value.ToString() == "6")
                    {
                        comboBox1.Text = "InActive";
                    }
                    else
                    {

                    }

                }
            }
            else if (e.ColumnIndex == 2)
            {
                if (e.RowIndex != -1)
                {
                   

                
                }
            }

            else
            {
                MessageBox.Show("Click rightly box");
            }

        
    }

        private void button1_Click(object sender, EventArgs e)//edit button
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                //Open the connection to db
                con.Open();
                String cmds = "SELECT * FROM Lookup";
                SqlCommand command = new SqlCommand(cmds, con);
                SqlDataReader reader = command.ExecuteReader();
                int columnValue;
                if (con.State == ConnectionState.Open)
                {
                    while (reader.Read())
                    {
                        if (reader[1].ToString() == comboBox1.Text)
                        {
                          //  m = reader[1].ToString();
                            columnValue = Convert.ToInt32(reader[0]);
                            string query = "UPDATE Student set FirstName='" + textBox1.Text + "' , LastName='" + textBox2.Text + "' , Contact='" + textBox3.Text + "' ,Email='" + textBox4.Text + "' ,RegistrationNumber='" + textBox5.Text + "' ,Status='" + columnValue + "'  where Id='" + studentid + "'";
                            SqlCommand com = new SqlCommand(query, con);
                            com.ExecuteNonQuery();
                            MessageBox.Show("successful Updated");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            comboBox1.Text = "";
                            show();
                        }
                    }
                }// end of outer if

                else
                {
                    MessageBox.Show("erro");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attendance obj = new Attendance();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assessment obj = new Assessment();
            this.Hide();
            obj.Show();
        }
    }
}

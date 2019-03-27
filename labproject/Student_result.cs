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
    public partial class Student_result : Form
    {
        public Student_result()
        {
            InitializeComponent();
        }
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        public static int rubric_id;
        public static int student_id;
        public static int assess_comp_id;
        public static int measurementLevel_id;
        public static int componentid;
        public static string componentName;
        public static int RubricMeasu_id;
        public static int R_id;
        public static int maxfind;
        private void Student_result_Load(object sender, EventArgs e)
        {
            //binding of combobox1
            using (SqlConnection con = new SqlConnection(constr))
            {//bind combox with all students
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT Id,RegistrationNumber FROM Student where Student.Status='"+ 5+"'", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox1.DisplayMember = "RegistrationNumber";
                    comboBox1.DataSource = dt;

                }
            }//end of combox binding

            //binding of combobox4
            using (SqlConnection con = new SqlConnection(constr))
            {//bind combox with all students
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT Id,RegistrationNumber FROM Student where Student.Status='" + 5 + "'", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox4.DisplayMember = "RegistrationNumber";
                    comboBox4.DataSource = dt;

                }
            }//end of combox binding
            //binding combbox2
            using (SqlConnection con = new SqlConnection(constr))
            {//bind combox with all Assessment Component
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT Id,Name FROM AssessmentComponent ", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox2.DisplayMember = "Name";
                    comboBox2.DataSource = dt;

                }
            }//end of combox binding

          
        }

     

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //ComboBox senderComboBox = (ComboBox)sender;
            if (comboBox2.SelectedIndex > -1)
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string txt = comboBox2.Text;
                string query = "SELECT * FROM AssessmentComponent where AssessmentComponent.Name ='" + txt + "' ";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //binding of comboBox3
                    using (SqlConnection con = new SqlConnection(constr))
                    {//bind combox rubric level
                        using (SqlDataAdapter da = new SqlDataAdapter("SELECT MeasurementLevel FROM RubricLevel where RubricLevel.RubricId='" + reader[2].ToString() + "'", con))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comboBox3.DisplayMember = "MeasurementLevel";
                            comboBox3.DataSource = dt;

                        }
                    }//end of combox binding

                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLO obj = new CLO();
            this.Hide();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
        
            //get student id
            string regNo = comboBox1.Text;
            string StudentIdQuery = "SELECT * FROM Student where Student.RegistrationNumber='" + comboBox1.Text + "' ";
            SqlCommand student_cmd = new SqlCommand(StudentIdQuery, conn);
            SqlDataReader reader = student_cmd.ExecuteReader();
            while(reader.Read())
            {
                 student_id = Convert.ToInt32(reader[0]);
            }
            // get assessment component id
          
            string Assess_comp_Query = "SELECT * FROM Assessment where AssessmentComponent.Name='" + comboBox2.Text + "' ";
            SqlCommand Assess_cmd = new SqlCommand(Assess_comp_Query, conn);
            SqlDataReader reader2 = Assess_cmd.ExecuteReader();
            while (reader2.Read())
            {
                 assess_comp_id = Convert.ToInt32(reader2[0]);
                rubric_id = Convert.ToInt32(reader2[2]);
            }

            // get Rubric measurementlevel id
            int measurementLevel = Convert.ToInt32(comboBox3.Text);
            string measurementLevel_Query = "SELECT * FROM RubricLevel where RubricLevel.MeasurementLevel='" + measurementLevel + "' and RubricLevel.RubricId='"+ rubric_id+"' ";
            SqlCommand measurementLevel_cmd = new SqlCommand(measurementLevel_Query, conn);
            SqlDataReader reader3 = measurementLevel_cmd.ExecuteReader();
            while (reader3.Read())
            {
                measurementLevel_id = Convert.ToInt32(reader3[0]);
            }

            // now insert data in StudentResult table
            string query = "INSERT INTO StudentResult(StudentId,AssessmentComponentId,RubricMeasurementId,EvaluationDate)VALUES('" + student_id + "', '" + assess_comp_id + "', '" + measurementLevel_id + "', '" + dateTimePicker1.Value.Date + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // load data in datagridview
            SqlConnection conn = new SqlConnection(constr);
            //Open the connection to db
            conn.Open();
            //get student id
            string regNo = comboBox4.Text;
            string StudentIdQuery = "SELECT * FROM Student where Student.RegistrationNumber='" + comboBox4.Text + "' ";
            SqlCommand student_cmd = new SqlCommand(StudentIdQuery, conn);
            SqlDataReader reader = student_cmd.ExecuteReader();
            while (reader.Read())
            {
                student_id = Convert.ToInt32(reader[0]);
            }
            // get student related data from studentResult
            string componentidQuery = "SELECT * FROM StudentResult where Studentresult.StudentId='" + student_id + "' ";
            SqlCommand componentid_cmd = new SqlCommand(componentidQuery, conn);
            SqlDataReader reader2 = componentid_cmd.ExecuteReader();
            while (reader2.Read())
            {
                componentid = Convert.ToInt32(reader2[1]);
                RubricMeasu_id = Convert.ToInt32(reader2[2]);
            }

            //  show in data gridview
            SqlCommand SqlCommand;
            SqlDataAdapter adapter = new SqlDataAdapter();
            int finalamrks;

            // string showdata = "SELECT Student.Id,AssessmentComponent.Name,Rubric.Details,StudentResult.RubricMeasurementId from StudentResult,student,AssessmentComponent,Rubric where StudentResult.StudentId=Student.Id and StudentResult.StudentId='" + student_id + "' ";
            string showdata = "Select AssessmentComponent.Name as Component,Rubric.Details as Rubric,AssessmentComponent.TotalMarks, RubricLevel.MeasurementLevel as RubricLevel from StudentResult inner join Student on Student.Id=StudentResult.StudentId and Student.Id='" + student_id + "' left join AssessmentComponent on StudentResult.AssessmentComponentId=AssessmentComponent.Id and AssessmentComponent.Id= '" + componentid + "' left join Rubric on Rubric.Id=AssessmentComponent.RubricId left join RubricLevel on RubricLevel.Id=StudentResult.RubricMeasurementId and StudentResult.RubricMeasurementId='" + RubricMeasu_id + "' ";
            SqlCommand = new SqlCommand(showdata, conn);
            adapter.SelectCommand = new SqlCommand(showdata, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            dataGridView1.Columns["ObtainedMarks"].DisplayIndex = 4;
            // caculate result
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
            
                    DataGridViewRow rows = dataGridView1.Rows[i];
                    string c = rows.Cells[1].Value.ToString();
                    string q = "Select * from AssessmentComponent where AssessmentComponent.Name='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' ";
                    SqlCommand cmdd = new SqlCommand(q, conn);
                    SqlDataReader read = cmdd.ExecuteReader();
                    while (read.Read())
                    {
                        R_id = Convert.ToInt32(read[2]);
                    }
                    //
                    string max = "SELECT MAX(MeasurementLevel) FROM RubricLevel where RubricLevel.RubricId='" + R_id + "'";
                    SqlCommand cmd_max = new SqlCommand(max, conn);
                    int maxfind = Convert.ToInt32(cmd_max.ExecuteScalar());
                    int f_step = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                    int s_step = f_step % maxfind;
                    int t_step = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    finalamrks = s_step * t_step;
                    dataGridView1.Rows[i].Cells[0].Value = finalamrks;

                }
              

            


        }
        private void button3_Click(object sender, EventArgs e)
        {
          
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            //Open the connection to db
            conn.Open();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    DataGridViewRow rows = dataGridView1.Rows[i];
                    string c = rows.Cells[1].Value.ToString();
                    string q = "Select * from AssessmentComponent where AssessmentComponent.Name='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' ";
                    SqlCommand cmdd = new SqlCommand(q, conn);
                    SqlDataReader read = cmdd.ExecuteReader();
                    while (read.Read())
                    {
                        R_id = Convert.ToInt32(read[2]);
                    }
                    //
                    string max = "SELECT MAX(MeasurementLevel) FROM RubricLevel where RubricLevel.RubricId='" + R_id + "'";
                    SqlCommand cmd_max = new SqlCommand(max, conn);
                    int maxfind = Convert.ToInt32(cmd_max.ExecuteScalar());
                    int f_step = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                    int s_step = f_step % maxfind;
                    int t_step = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                   int finalamrks = s_step * t_step;
                    dataGridView1.Rows[i].Cells[0].Value = finalamrks;

                }
                catch (Exception c)
                {
                    MessageBox.Show("hgff");
                }

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

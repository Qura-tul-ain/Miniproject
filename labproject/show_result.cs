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
    public partial class show_result : Form
    {
        public show_result()
        {
            InitializeComponent();
        }
        public static int getclo_id;
        public static int R_id;
        public static int maxfind;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void show_result_Load(object sender, EventArgs e)
        {
            //binding of combobox1
            using (SqlConnection con = new SqlConnection(constr))
            {//bind combox with all clo having rubrics assign to assessmentComponent
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT Clo.Name as Name,Rubric.Id,Rubric.CloId,AssessmentComponent.RubricId FROM Clo,Rubric,AssessmentComponent where Clo.Id=Rubric.CloId and Rubric.Id=AssessmentComponent.RubricId", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox1.DisplayMember = "Name";
                    comboBox1.DataSource = dt;

                }
            }//end of combox binding


            //SqlConnection conn = new SqlConnection(constr);
            //conn.Open();
            //getclo_id = Student_assessment.clo_id;// get clo id from previous form
            //SqlCommand SqlCommand;
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //string showdata = "SELECT Student.RegistrationNumber,Rubric.Details,RubricLevel.MeasurementLevel,AssessmentComponent.TotalMarks FROM Clo inner join Rubric on Clo.Id=Rubric.CloId  inner join AssessmentComponent on Rubric.Id=AssessmentComponent.RubricId inner join StudentResult  on AssessmentComponent.Id=StudentResult.AssessmentComponentId inner join RubricLevel on StudentResult.RubricMeasurementId=RubricLevel.Id inner join Student on Student.Id=StudentResult.StudentId and Clo.Id='" + getclo_id + "'";
            ////  string showdata = "SELECT StudentResult.StudentId,StudentResult.AssessmentComponentId FROM Clo,Rubric,AssessmentComponent,StudentResult,Student where Clo.Id=Rubric.CloId and Rubric.Id=AssessmentComponent.RubricId and AssessmentComponent.Id=StudentResult.AssessmentComponentId and Clo.Id='" + clo_id+"'";
            //SqlCommand = new SqlCommand(showdata, conn);
            //adapter.SelectCommand = new SqlCommand(showdata, conn);
            //DataTable dbdataset = new DataTable();
            //adapter.Fill(dbdataset);
            //BindingSource bsource = new BindingSource();
            //bsource.DataSource = dbdataset;
            //dataGridView1.DataSource = bsource;
            //dataGridView1.Columns["ObtainedMarks"].DisplayIndex = 4;

        }

        private void button1_Click(object sender, EventArgs e)//calculate result
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            float finalmarks;
            int j = 0;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
              
                DataGridViewRow rows = dataGridView1.Rows[i];
            
                string q = "Select * from Rubric where Rubric.Details='" + dataGridView1.Rows[j].Cells[2].Value.ToString() + "' ";
                SqlCommand cmdd = new SqlCommand(q, conn);
                SqlDataReader read = cmdd.ExecuteReader();
                read.Read();
                
                    R_id = Convert.ToInt32(read[0]);
                
                //
                string max = "SELECT MAX(MeasurementLevel) FROM RubricLevel where RubricLevel.RubricId='" + R_id + "'";
                SqlCommand cmd_max = new SqlCommand(max, conn);
                int maxfind = Convert.ToInt32(cmd_max.ExecuteScalar());
                float f_step = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);//measurement level
                float s_step = f_step / maxfind;
                float t_step = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);// total marks
                finalmarks = s_step * t_step;
                dataGridView1.Rows[i].Cells[0].Value = finalmarks;// final obtained marks
                j = i+1;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Student_assessment obj = new Student_assessment();
            this.Hide();
            obj.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assess_component obj = new Assess_component();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLO obj = new CLO();
            this.Hide();
            obj.Show();

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            string cloquery = "Select Clo.Id from Clo where Clo.Name='" + comboBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(cloquery, conn);
            getclo_id = Convert.ToInt32(cmd.ExecuteScalar());
      
            SqlCommand SqlCommand;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string showdata = "SELECT Student.RegistrationNumber,Rubric.Details,RubricLevel.MeasurementLevel,AssessmentComponent.TotalMarks FROM Clo inner join Rubric on Clo.Id=Rubric.CloId  inner join AssessmentComponent on Rubric.Id=AssessmentComponent.RubricId inner join StudentResult  on AssessmentComponent.Id=StudentResult.AssessmentComponentId inner join RubricLevel on StudentResult.RubricMeasurementId=RubricLevel.Id inner join Student on Student.Id=StudentResult.StudentId and Clo.Id='" + getclo_id + "'";
            //  string showdata = "SELECT StudentResult.StudentId,StudentResult.AssessmentComponentId FROM Clo,Rubric,AssessmentComponent,StudentResult,Student where Clo.Id=Rubric.CloId and Rubric.Id=AssessmentComponent.RubricId and AssessmentComponent.Id=StudentResult.AssessmentComponentId and Clo.Id='" + clo_id+"'";
            SqlCommand = new SqlCommand(showdata, conn);
            adapter.SelectCommand = new SqlCommand(showdata, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            dataGridView1.Columns["ObtainedMarks"].DisplayIndex = 4;
        }
    }
}

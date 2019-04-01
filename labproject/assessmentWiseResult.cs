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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
namespace labproject
{
    public partial class assessmentWiseResult : Form
    {
        public assessmentWiseResult()
        {
            InitializeComponent();
        }
        public static int assess_id;
        public static int R_id;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            show_result obj = new show_result();
            this.Hide();
            obj.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assessment obj = new Assessment();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assess_component obj = new Assess_component();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_clo obj = new Add_clo();
            this.Hide();
            obj.Show();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }


        private void assessmentWiseResult_Load(object sender, EventArgs e)
        {
            //binding of combobox1
            //using (SqlConnection con = new SqlConnection(constr))
            //{//bind combox with all students
            //    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Title FROM Assessment", con))
            //    {
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        comboBox1.DisplayMember = "Title";
            //        comboBox1.DataSource = dt;

            //    }
            //}//end of combox binding

            // load data in datagridview
            SqlConnection conn = new SqlConnection(constr);
            //Open the connection to db
            conn.Open();

            SqlCommand SqlCommand;
            SqlDataAdapter adapter = new SqlDataAdapter();

            // string showdata = "SELECT Student.Id,AssessmentComponent.Name,Rubric.Details,StudentResult.RubricMeasurementId from StudentResult,student,AssessmentComponent,Rubric where StudentResult.StudentId=Student.Id and StudentResult.StudentId='" + student_id + "' ";
            string showdata = "select Assessment.Title,Student.RegistrationNumber as RegNo,AssessmentComponent.Name as Component,Rubric.Details as Rubric,AssessmentComponent.TotalMarks ,RubricLevel.MeasurementLevel from Assessment inner join AssessmentComponent on Assessment.Id=AssessmentComponent.AssessmentId inner join Rubric on AssessmentComponent.RubricId=Rubric.Id inner join StudentResult on AssessmentComponent.Id=StudentResult.AssessmentComponentId inner join RubricLevel on StudentResult.RubricMeasurementId=RubricLevel.Id  inner join Student on Student.Id=StudentResult.StudentId  ";
            SqlCommand = new SqlCommand(showdata, conn);
            adapter.SelectCommand = new SqlCommand(showdata, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            // add cloumns to gridview
            DataGridViewTextBoxColumn ObtainedMarks = new DataGridViewTextBoxColumn();
            ObtainedMarks.HeaderText = "ObtainedMarks";

            dataGridView1.Columns.Add(ObtainedMarks);
            // dataGridView1.Columns["Marks"].DisplayIndex = 6;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
       
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // // load data in datagridview
           // SqlConnection conn = new SqlConnection(constr);
           // //Open the connection to db
           // conn.Open();

           // SqlCommand SqlCommand;
           // SqlDataAdapter adapter = new SqlDataAdapter();

           // // string showdata = "SELECT Student.Id,AssessmentComponent.Name,Rubric.Details,StudentResult.RubricMeasurementId from StudentResult,student,AssessmentComponent,Rubric where StudentResult.StudentId=Student.Id and StudentResult.StudentId='" + student_id + "' ";
           // string showdata = "select Assessment.Title, Student.RegistrationNumber as RegNo,AssessmentComponent.Name as Component,Rubric.Details as Rubric ,AssessmentComponent.TotalMarks ,RubricLevel.MeasurementLevel from Assessment inner join AssessmentComponent on Assessment.Id=AssessmentComponent.AssessmentId and Assessment.Title='" + comboBox1.Text + "' inner join StudentResult on AssessmentComponent.Id=StudentResult.AssessmentComponentId inner join Rubric on AssessmentComponent.RubricId=Rubric.Id  inner join RubricLevel on StudentResult.RubricMeasurementId=RubricLevel.Id  inner join Student on Student.Id=StudentResult.StudentId  ";
           // SqlCommand = new SqlCommand(showdata, conn);
           // adapter.SelectCommand = new SqlCommand(showdata, conn);
           // DataTable dbdataset = new DataTable();
           // adapter.Fill(dbdataset);
           // BindingSource bsource = new BindingSource();
           // bsource.DataSource = dbdataset;
           // dataGridView1.DataSource = bsource;
           //// dataGridView1.Columns["Marks"].DisplayIndex = 6;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            float finalmarks;
            int j = 0;
          
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    DataGridViewRow rows = dataGridView1.Rows[i];
                // get id of rubric
                   
                    string q = "Select * from Rubric where Rubric.Details='" + dataGridView1.Rows[j].Cells[3].Value.ToString() + "' ";
                    SqlCommand cmdd = new SqlCommand(q, conn);
                    SqlDataReader read = cmdd.ExecuteReader();
                    read.Read();
                    R_id = Convert.ToInt32(read[0]);

                    // find max measurement level
                    string max = "SELECT MAX(MeasurementLevel) FROM RubricLevel where RubricLevel.RubricId='" + R_id + "'";
                    SqlCommand cmd_max = new SqlCommand(max, conn);
                    int maxfind = Convert.ToInt32(cmd_max.ExecuteScalar());
                    float f_step = Convert.ToInt32(dataGridView1.Rows[j].Cells[5].Value);//measurement level
                    float s_step = f_step / maxfind;
                    float t_step = Convert.ToInt32(dataGridView1.Rows[j].Cells[4].Value);// total marks
                    finalmarks = s_step * t_step;
                    dataGridView1.Rows[j].Cells[6].Value = finalmarks;// final obtained marks
                j = j + 1;
               
                }
       
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Student_assessment obj = new Student_assessment();
            this.Hide();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportpdf(dataGridView1, "AssessmentReport");
        }

        public void exportpdf(DataGridView data, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(data.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            //add header
            foreach (DataGridViewColumn columns in data.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(columns.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);
            }

            // add datagridview data


            foreach (DataGridViewRow row in data.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));

                }
            }

            var savefiledialouge = new SaveFileDialog();
            savefiledialouge.FileName = filename;
            savefiledialouge.DefaultExt = ".pdf";
            if (savefiledialouge.ShowDialog() == DialogResult.OK)

            {
                using (FileStream stream = new FileStream(savefiledialouge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
            // dataGridView1.Columns["ObtainedMarks"].DisplayIndex = 5;
        }
    }
}

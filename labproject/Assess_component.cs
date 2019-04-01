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
    public partial class Assess_component : Form
    {
        public Assess_component()
        {
            InitializeComponent();
        }
        public static string name;
        public static int totalMark;
        public static int marks;
        public static int compid;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assessment obj = new Assessment();
            this.Hide();
            obj.Show();
        }

        private void Assess_component_Load(object sender, EventArgs e)
        {
           
            using (SqlConnection con = new SqlConnection(constr))
            {//bind combox with all Rubric.details
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT Details,CloId FROM Rubric", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox1.DisplayMember = "Details";
                    comboBox1.DataSource = dt;

                }
            }//end of combox binding

            // load data in grid view 
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string showdata = "Select Assessment.Title, Assessment.TotalMarks, AssessmentComponent.Name as Component,AssessmentComponent.DateCreated,AssessmentComponent.Id,AssessmentComponent.TotalMarks as Marks,AssessmentComponent.RubricId from Assessment inner join AssessmentComponent on Assessment.Id=AssessmentComponent.AssessmentId";
            SqlCommand SqlCommand = new SqlCommand(showdata, conn);
            adapter.SelectCommand = new SqlCommand(showdata, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns["Edit"].DisplayIndex = 8;
            dataGridView1.Columns["Delete"].DisplayIndex = 8;
        }

        private void button1_Click(object sender, EventArgs e)// add button
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            // get added id of  assessment ,which we need in AssessmentComponent
            int id = Assessment.assessment_id;
            DateTime dateCreate = DateTime.Now;
            DateTime dateUpdate = DateTime.Now;
            string c = comboBox1.Text;
            string querymarks = "Select TotalMarks from Assessment where Assessment.Id='" + id + "'";
            SqlCommand cd = new SqlCommand(querymarks, conn);
            marks = Convert.ToInt32(cd.ExecuteScalar());

            int componentMark = Convert.ToInt32(textBox2.Text);
            string marksCal = "Select AssessmentComponent.TotalMarks,Assessment.TotalMarks from AssessmentComponent,Assessment where Assessment.Id=AssessmentComponent.AssessmentId and Assessment.Id='" + id + "'";
            SqlCommand mark= new SqlCommand(marksCal, conn);
            SqlDataReader reader4 = mark.ExecuteReader();
            while(reader4.Read())
                {
                totalMark = componentMark + Convert.ToInt32(reader4[0]);
            }

            if(totalMark <marks)
            {
              
                //serach for RubricId(id of rubric seleted in combobox)
                string search = "SELECT * FROM Rubric where Rubric.Details='" + comboBox1.Text + "' ";
                SqlCommand cmnd = new SqlCommand(search, conn);
                SqlDataReader reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    int rubricId = Convert.ToInt32(reader[0]);
                    //insert component in table
                    string sql = "INSERT INTO AssessmentComponent(Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId) VALUES ('" + textBox1.Text + "','" + rubricId + "','" + textBox2.Text + "','" + dateCreate + "','" + dateUpdate + "','" + id + "')";
                    SqlCommand insert = new SqlCommand(sql, conn);
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Successfully Inserted Component,Add new Component");
                    show();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    comboBox1.Text = "";
                
                }
            }
            else if (totalMark >marks)
                    {
                MessageBox.Show("Components Marks greater than Total mark");
            }
            else{
                //serach for RubricId(id of rubric seleted in combobox)
                string search = "SELECT * FROM Rubric where Rubric.Details='" + comboBox1.Text + "' ";
                SqlCommand cmnd = new SqlCommand(search, conn);
                SqlDataReader reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    int rubricId = Convert.ToInt32(reader[0]);
                    //insert component in table
                    string sql = "INSERT INTO AssessmentComponent(Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId) VALUES ('" + textBox1.Text + "','" + rubricId + "','" + textBox2.Text + "','" + dateCreate + "','" + dateUpdate + "','" + id + "')";
                    SqlCommand insert = new SqlCommand(sql, conn);
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Successfully Inserted Component");
                    show();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    comboBox1.Text = "";
                }
            }
        }


        public void show()
        {
            // load data in grid view 
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string showdata = "Select Assessment.Title, Assessment.TotalMarks, AssessmentComponent.Name as Component,AssessmentComponent.DateCreated,AssessmentComponent.Id,AssessmentComponent.TotalMarks as Marks,AssessmentComponent.RubricId  from Assessment inner join AssessmentComponent on Assessment.Id=AssessmentComponent.AssessmentId";
            SqlCommand SqlCommand = new SqlCommand(showdata, conn);
            adapter.SelectCommand = new SqlCommand(showdata, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
           dataGridView1.Columns[6].Visible = false;// containing comp id
            dataGridView1.Columns[8].Visible = false;// containing rubricid
            dataGridView1.Columns["Edit"].DisplayIndex = 8;
            dataGridView1.Columns["Delete"].DisplayIndex = 8;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)//component id
            {

                if (e.RowIndex != -1)//delete
                {
                    SqlConnection conn = new SqlConnection(constr);
                    //Open the connection to db
                    conn.Open();
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    string comp_id = rows.Cells[6].Value.ToString();//column6 is containing id
                    int C_id = Convert.ToInt32(comp_id);
                
                    for (int i = 0; i < 3; i++)
                    {
                      
                        string delete_result = "DELETE StudentResult WHERE EXISTS ( SELECT * FROM AssessmentComponent WHERE AssessmentComponent.Id= Studentresult.AssessmentComponentId and AssessmentComponent.Id ='" + C_id + "')";
                        SqlCommand cmd3 = new SqlCommand(delete_result, conn);
                        SqlDataReader reader3 = cmd3.ExecuteReader();
                    }


                    string delete_comp = "DELETE from AssessmentComponent WHERE AssessmentComponent.Id ='" + C_id + "'";
                    SqlCommand cmd2 = new SqlCommand(delete_comp, conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    MessageBox.Show("Deleted Successfully");
                    show();

                }
            }//end of delete


            else if (e.ColumnIndex == 0)//edit
            {
                if (e.RowIndex != -1)
                {
                    
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter("SELECT Details FROM Rubric ORDER BY Details", conn))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comboBox1.DisplayMember = "Details";
                            comboBox1.DataSource = dt;

                        }
                    }
                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                   string compId = rows.Cells[6].Value.ToString();//column6 is containing id
                    string rubId = rows.Cells[8].Value.ToString();//column8 is containing id
                    string q = "SELECT * from Rubric where Id='" + rubId + "'";
                    SqlCommand cm = new SqlCommand(q, con);
                    SqlDataReader reader = cm.ExecuteReader();
                    while(reader.Read())
                    {
                        name = reader[1].ToString();
                    }
                    compid = Convert.ToInt32(rows.Cells[6].Value);
                    textBox1.Text = rows.Cells[4].Value.ToString();//comp name
                    comboBox1.Text = name;//rubric 
                    textBox2.Text = rows.Cells[7].Value.ToString();//comp marks
                   
                    
                   

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//edit button
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Kindly select data");
            }
            else
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                DateTime dateUpdate = DateTime.Now;
                string c = comboBox1.Text;
                //serach for RubricId(id of rubric seleted in combobox)
                string search = "SELECT * FROM Rubric where Rubric.Details='" + comboBox1.Text + "' ";
                SqlCommand cmnd = new SqlCommand(search, conn);
                SqlDataReader reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    int rubricId = Convert.ToInt32(reader[0]);
                    //insert component in table
                    string sql = "UPDATE AssessmentComponent set Name='" + textBox1.Text + "' , RubricId='" + rubricId + "' ,TotalMarks='" + textBox2.Text + "',DateUpdated='" + dateUpdate + "'  where AssessmentComponent.Id='" + compid + "'";
                    SqlCommand insert = new SqlCommand(sql, conn);
                    insert.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated Component");
                    show();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    comboBox1.Text = "";
                }
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_clo obj = new Add_clo();
            this.Hide();
            obj.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Student_assessment obj = new Student_assessment();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            show_result obj = new show_result();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();

        }
    }
}

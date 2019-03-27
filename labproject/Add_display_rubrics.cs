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
    public partial class Add_display_rubrics : Form
    {
        public Add_display_rubrics()
        {
            InitializeComponent();
        }
        public static string rubric_id;
        public static string rub_name;
        public string clo_id;//for editing of rubrics
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void Add_rubrics_Load(object sender, EventArgs e)
        {
            string id = CLO.publicCloId;
            
            int clo_id = Convert.ToInt32(id);
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = "select Clo.Name,Clo.Id, Rubric.Id,Rubric.Details as Rubric from Clo,Rubric where Clo.Id=Rubric.CloId and Clo.Id='" + clo_id+"'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);
            dataGridView1.Columns[4].Visible = false;//hiding clo.id
            dataGridView1.Columns[5].Visible = false;//hiding rubric.id
            dataGridView1.Columns["ManageLevel"].DisplayIndex = 6;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;
            dataGridView1.Columns["Delete"].DisplayIndex = 6;



        }
        public void show()
        {
            string id = CLO.publicCloId;

            int clo_id = Convert.ToInt32(id);
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;


            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = "select Clo.Name,Clo.Id ,Rubric.Id, Rubric.Details as Rubric from Clo,Rubric where Clo.Id=Rubric.CloId and Clo.Id='" + clo_id + "'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);
            dataGridView1.Columns[4].Visible = false;//hiding clo.id
            dataGridView1.Columns[5].Visible = false;//hiding rubric.id
            dataGridView1.Columns["ManageLevel"].DisplayIndex = 6;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;
            dataGridView1.Columns["Delete"].DisplayIndex = 6;

        }
        private void button1_Click(object sender, EventArgs e)//insert data
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("please insert data");
            }
            else
            {
                string id = CLO.publicCloId;
                int cloid = Convert.ToInt32(id);
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Please Enter Details");
                    }
                    else
                    {
                        // string rubriclevel= "INSERT INTO RubricLevel(Details, CloId)VALUES('" + textBox1.Text + "', '" + cloid + "')";"
                        string query = "INSERT INTO Rubric (Details,CloId)VALUES ('" + textBox1.Text + "','" + cloid + "')";
                        SqlCommand cmdrubric = new SqlCommand(query, con);
                        cmdrubric.ExecuteNonQuery();
                        MessageBox.Show("Successful Inserted");
                        textBox1.Text = "";
                        show();
                    }


                }

                else
                {
                    MessageBox.Show("Erro while inserting data");
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.ColumnIndex == 0)//by default datagridview gives added column starting indexes.
            {

                if (e.RowIndex != -1)//managelevel
                {
                  
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    rubric_id = rows.Cells[5].Value.ToString();
                    rub_name = rows.Cells[6].Value.ToString();
                    Add_display_levelcs form_rub = new Add_display_levelcs();
                    this.Hide();
                    form_rub.Show();

                }
            }//end of managelevel

            if (e.ColumnIndex == 1)//edit
            {
                if (e.RowIndex != -1)
                {// binding of comobox with clo's names.
                    using (SqlConnection conn = new SqlConnection(constr))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter("SELECT Name FROM Clo ORDER BY Name", conn))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comboBox1.DisplayMember = "Name";
                            comboBox1.DataSource = dt;
                          
                        }
                    }
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    rubric_id = rows.Cells[5].Value.ToString();
                   clo_id = rows.Cells[4].Value.ToString();
                    textBox2.Text = rows.Cells[6].Value.ToString();
                    comboBox1.Text = rows.Cells[3].Value.ToString();
                    
                }
            }//end of edit

            if(e.ColumnIndex == 2)//delete 
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();//open connection
                DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
               rubric_id = rows.Cells[5].Value.ToString();
                for (int i = 0; i < 3; i++)
                {
                    // RubricId=rubric which user wants to delete.
                    //delete rubriclevel where Rubric.Id equals to RubricLevel.rubricId 
                    string delete_rubric_level = "DELETE RubricLevel WHERE EXISTS ( SELECT * FROM Rubric WHERE Rubric.Id= RubricLevel.RubricId and Rubric.Id ='" + rubric_id + "')";
                    SqlCommand cmd3 = new SqlCommand(delete_rubric_level, conn);
                    SqlDataReader reader3 = cmd3.ExecuteReader();
                }
               

                string delete_rub = "DELETE from Rubric WHERE Rubric.Id ='" + rubric_id + "'";
                SqlCommand cmd2 = new SqlCommand(delete_rub, conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                MessageBox.Show("Deleted Successfully");
                show();

            }

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || comboBox1.Text=="")
            {
                MessageBox.Show("Select data to edit");
            }
            else
            {
                //edit the rubric...if user edit the clo of a rubric then map this rubric
                // to new clo
                SqlConnection con = new SqlConnection(constr);
                //Open the connection to db
                con.Open();
                string check = "Select Id from Clo where Clo.Name='" + comboBox1.Text + "'";//find id of clo selected
                                                                                            // by user in checkbox
                SqlCommand command = new SqlCommand(check, con);
                int required_cloId = Convert.ToInt32(command.ExecuteScalar());//get CloId
                string query = "UPDATE Rubric set Details='" + textBox2.Text + "' , CloId='" + required_cloId + "'  where Rubric.Id='" + rubric_id + "'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                MessageBox.Show("successful Updated");
                textBox2.Text = "";
               comboBox1.Text = "";
                show();
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLO obj = new CLO();
            this.Hide();
            obj.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_clo obj = new Add_clo();
            this.Hide();
            obj.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Show_student obj = new Show_student();
            this.Hide();
            obj.Show();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attendance obj = new Attendance();
            this.Hide();
            obj.Show();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Student_assessment obj = new Student_assessment();
            this.Hide();
            obj.Show();
        }
    }
}

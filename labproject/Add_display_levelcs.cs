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
    public partial class Add_display_levelcs : Form
    {
        public Add_display_levelcs()
        {
            InitializeComponent();
        }
        public string levelId;
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("please enter data");
            }
            else
            {
                string id = Add_display_rubrics.rubric_id;
                int rubid = Convert.ToInt32(id);
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    // string rubriclevel= "INSERT INTO RubricLevel(Details, CloId)VALUES('" + textBox1.Text + "', '" + cloid + "')";"
                    string query = "INSERT INTO RubricLevel (RubricId,Details,MeasurementLevel)VALUES ('" + rubid + "','" + textBox1.Text + "','" + textBox2.Text + "')";
                    SqlCommand cmdrubric = new SqlCommand(query, con);
                    cmdrubric.ExecuteNonQuery();
                    MessageBox.Show("Successful Inserted");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    show();//reload new data in gridview


                }

                else
                {
                    MessageBox.Show("Erro while inserting data");
                }
            }
        }

        public void show()
        {

            string rub = Add_display_rubrics.rub_name;
            label1.Text = "ADD Level For " + rub + " Rubric";

            string id = Add_display_rubrics.rubric_id;

            int rub_id = Convert.ToInt32(id);
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;


            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = " select Rubric.Details as Rubric,RubricLevel.Id,RubricLevel.Details as LevelDetails,RubricLevel.MeasurementLevel,RubricLevel.RubricId from Rubric,RubricLevel where Rubric.Id=RubricLevel.RubricId and RubricId='" + rub_id + "'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns["Delete"].DisplayIndex = 5;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void Add_levelcs_Load(object sender, EventArgs e)
        {
            string rub = Add_display_rubrics.rub_name;
            label1.Text = "ADD Level For " + rub+ " Rubric";

            string id = Add_display_rubrics.rubric_id;

            int rub_id = Convert.ToInt32(id);
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;


            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = " select Rubric.Details as Rubric,RubricLevel.Id,RubricLevel.Details as LevelDetails,RubricLevel.MeasurementLevel,RubricLevel.RubricId from Rubric,RubricLevel where Rubric.Id=RubricLevel.RubricId and RubricId='" + rub_id + "'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);
           dataGridView1.Columns[3].Visible = false;//hide rubricId
            dataGridView1.Columns[6].Visible = false;//hide rubriclevel Id
            dataGridView1.Columns["Delete"].DisplayIndex = 5;
            dataGridView1.Columns["Edit"].DisplayIndex = 6;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//student id
            {

                if (e.RowIndex != -1)//delete
                {
                    SqlConnection conn = new SqlConnection(constr);
                    //Open the connection to db
                    conn.Open();
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    string level_id = rows.Cells[3].Value.ToString();//column3 is containing id
                    int L_id = Convert.ToInt32(level_id);
                    string delete_level = "DELETE from RubricLevel WHERE Id ='" + L_id + "'";
                    SqlCommand cmd = new SqlCommand(delete_level, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Successfully deleted");
                    show();

                }
            }//end of delete


            else if (e.ColumnIndex == 1)//edit
            {
                if (e.RowIndex != -1)
                {
                    string id = CLO.publicCloId;
                    int cloid = Convert.ToInt32(id);
                    using (SqlConnection conn = new SqlConnection(constr))
                    {//bind combox with Rubric.details for required clo.
                        using (SqlDataAdapter da = new SqlDataAdapter("SELECT Details,CloId FROM Rubric where Rubric.CloId='" + cloid + "' ORDER BY Details" , conn))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            comboBox1.DisplayMember = "Details";
                            comboBox1.DataSource = dt;

                        }
                    }
                    DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                    levelId = rows.Cells[3].Value.ToString();//column3 is containing id
                    textBox3.Text = rows.Cells[4].Value.ToString();//level detals
                    textBox4.Text = rows.Cells[5].Value.ToString();//measurement level
                    comboBox1.Text = rows.Cells[2].Value.ToString();

                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Please Select data");
            }
            else
            {
                //edit the rubric level...if user edit the rubric of a rubriclevel then map this rubriclevel
                // to new rubric
                SqlConnection con = new SqlConnection(constr);
                //Open the connection to db
                con.Open();
                string check = "Select Id from Rubric where Rubric.Details='" + comboBox1.Text + "'";//find id of clo selected
                                                                                                     // by user in checkbox
                SqlCommand command = new SqlCommand(check, con);
                int required_rubId = Convert.ToInt32(command.ExecuteScalar());//get CloId
                string query = "UPDATE RubricLevel set Details='" + textBox3.Text + "' , MeasurementLevel='" + textBox4.Text + "' ,RubricId='" + required_rubId + "'  where RubricLevel.Id='" + levelId + "'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                MessageBox.Show("Successful Updated");
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox1.Text = "";
                show();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_display_rubrics obj = new Add_display_rubrics();
            this.Hide();
            obj.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_clo obj = new Add_clo();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLO obj = new CLO();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Student_assessment obj = new Student_assessment();
            this.Hide();
            obj.Show();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assessment obj = new Assessment();
            this.Hide();
            obj.Show();
        }
    }
}

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
    public partial class Clo_rubric_level : Form
    {
        public Clo_rubric_level()
        {
            InitializeComponent();
        }
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void Clo_rubric_level_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Select Clo.Name,Rubric.Details as Rubric ,RubricLevel.Details,RubricLevel.MeasurementLevel from Clo left join Rubric on  Clo.Id = Rubric.CloId left join RubricLevel on Rubric.Id = RubricLevel.RubricId";
         //   string query="
            SqlCommand comand = new SqlCommand(query,con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, con);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Show_student obj = new Show_student();
            this.Hide();
            obj.Show();

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attendance obj = new Attendance();
            this.Hide();
            obj.Show();
        }
    }
}

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
    public partial class show_attendance : Form
    {
        public show_attendance()
        {
            InitializeComponent();
        }
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Show_student obj = new Show_student();
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

        private void show_attendance_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;


            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = "select Student.FirstName,Student.LastName,Student.RegistrationNumber, StudentAttendance.AttendanceStatus from Student,StudentAttendance where Student.Status='"+ 5+"'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;
            dataGridView1.DataSource = bsource;
            dataGridView1.Columns["AttendanceStatus"].Visible = false;
            dataGridView1.Columns["Att_Status"].DisplayIndex = 4;
            // for displaying attendance in the from of present,absent..
            int valuepresent = 1;
            int valueabsent =2;
            int valueleave = 3;
            int valuelate = 4;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) == valuepresent)
                {

                    dataGridView1.Rows[i].Cells["Att_status"].Value = "Present";
                }
                else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) == valueabsent)
                {
                    dataGridView1.Rows[i].Cells["Att_status"].Value = "Absent";
                }
                else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) == valueleave)
                {
                    dataGridView1.Rows[i].Cells["Att_status"].Value = "OnLeave";
                }
                else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) == valuelate)
                {
                    dataGridView1.Rows[i].Cells["Att_status"].Value = "Late";
                }
                else
                {

                }
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            start obj = new start();
            this.Hide();
            obj.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

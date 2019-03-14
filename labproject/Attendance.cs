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
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
        }
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void Attendance_Load(object sender, EventArgs e)
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
            DataGridViewComboBoxColumn col1 = new DataGridViewComboBoxColumn();
            col1.HeaderText = "Mark";
            col1.Items.Add("Present");
            col1.Items.Add("Absent");
            col1.Items.Add("Leave");
            col1.Items.Add("Late");
            dataGridView1.Columns.Add(col1);

             dataGridView1.Columns[0].Visible = false;
             dataGridView1.Columns[1].Visible = false;
             dataGridView1.Columns[2].Visible = false;
             dataGridView1.Columns[3].Visible = false;
             dataGridView1.Columns[4].Visible = false;
           
             dataGridView1.Columns[6].Visible = false;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
            this.Hide();
            obj.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(constr);
            //Open the connection to db
            conn.Open();

            string query = "insert into ClassAttendance(AttendanceDate) Values('" + dateTimePicker1.Value.Date + "')";
            SqlCommand cmmd = new SqlCommand(query, conn);
            cmmd.ExecuteNonQuery();

            string q1 = "SELECT IDENT_CURRENT('ClassAttendance')";
            SqlCommand q = new SqlCommand(q1, conn);
            int id = Convert.ToInt32(q.ExecuteScalar());
            int columnValue;
            string m;
            String cmds = "SELECT * FROM Lookup";
            SqlCommand command = new SqlCommand(cmds, conn);

            SqlDataReader reader = command.ExecuteReader();
            int mm = dataGridView1.Rows.Count;
            for (int i = 1; i < dataGridView1.RowCount; i++)
            {
                while (reader.Read())
                {
                    if (reader[1].ToString() == dataGridView1.Rows[i].Cells[7].Value.ToString())
                    {
                        m = reader[1].ToString();
                        columnValue = Convert.ToInt32(reader[0]);
                        string sql = "INSERT INTO StudentAttendance(AttendanceId,StudentId,AttendanceStatus) VALUES ('" + id + "','"
                      + dataGridView1.Rows[i].Cells[0].Value + "', '"
                     + columnValue + "' )";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        continue;
                       // MessageBox.Show("successful");
                    }
                }
               
            }
        }
    }
}

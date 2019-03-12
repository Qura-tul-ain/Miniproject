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
    public partial class Add_levelcs : Form
    {
        public Add_levelcs()
        {
            InitializeComponent();
        }
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void button1_Click(object sender, EventArgs e)
        {
            string id = Add_rubrics.rubric_id;
            int rubid = Convert.ToInt32(id);
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                // string rubriclevel= "INSERT INTO RubricLevel(Details, CloId)VALUES('" + textBox1.Text + "', '" + cloid + "')";"
                string query = "INSERT INTO RubricLevel (RubricId,Details,MeasurementLevel)VALUES ('"+rubid+"','" + textBox1.Text + "','" + textBox2.Text + "')";
                SqlCommand cmdrubric = new SqlCommand(query, con);
                cmdrubric.ExecuteNonQuery();
                MessageBox.Show("Successful");


            }

            else
            {
                MessageBox.Show("erro");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Level_display obj = new Level_display();
            this.Hide();
            obj.Show();
        }

        private void Add_levelcs_Load(object sender, EventArgs e)
        {
            string rub = Add_rubrics.rub_name;
            label1.Text = "ADD Level For " + rub;
        }
    }
}

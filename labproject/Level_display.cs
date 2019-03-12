using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace labproject
{
    public partial class Level_display : Form
    {
        public Level_display()
        {
            InitializeComponent();
        }
        public static string publicCloId = " ";
        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void Level_display_Load(object sender, EventArgs e)
        {
            string id = Add_rubrics.rubric_id;

            int rub_id = Convert.ToInt32(id);
            SqlConnection conn = new SqlConnection(constr);

            string query;
            SqlCommand SqlCommand;


            SqlDataAdapter adapter = new SqlDataAdapter();
            //Open the connection to db
            conn.Open();

            //Generating the query to fetch the contact details
            query = "SELECT * FROM RubricLevel where RubricId='"+rub_id+"'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

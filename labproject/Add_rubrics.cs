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
    public partial class Add_rubrics : Form
    {
        public Add_rubrics()
        {
            InitializeComponent();
        }
        public static string rubric_id;
        public static string rub_name;
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
            query = "SELECT * FROM Rubric where CloId='"+clo_id+"'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);


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
            query = "SELECT * FROM Rubric where CloId='" + clo_id + "'";

            SqlCommand = new SqlCommand(query, conn);
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable dbdataset = new DataTable();
            adapter.Fill(dbdataset);
            BindingSource bsource = new BindingSource();
            bsource.DataSource = dbdataset;

            dataGridView1.DataSource = bsource;
            adapter.Update(dbdataset);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string id = CLO.publicCloId;
            int cloid = Convert.ToInt32(id);
            SqlConnection con = new SqlConnection(constr);
            con.Open();
          
            if (con.State == ConnectionState.Open)
            {
               // string rubriclevel= "INSERT INTO RubricLevel(Details, CloId)VALUES('" + textBox1.Text + "', '" + cloid + "')";"
                string query = "INSERT INTO Rubric (Details,CloId)VALUES ('" + textBox1.Text + "','" + cloid + "')";
                SqlCommand cmdrubric = new SqlCommand(query, con);
                cmdrubric.ExecuteNonQuery();
                MessageBox.Show("Successful");
                show();

               
            }

            else
            {
                MessageBox.Show("erro");
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
            if (e.RowIndex != -1)
            {
                DataGridViewRow rows = dataGridView1.Rows[e.RowIndex];
                rubric_id = rows.Cells[1].Value.ToString();
                rub_name = rows.Cells[2].Value.ToString();
                Add_levelcs form_rub = new Add_levelcs();
                form_rub.Show();




            }
        }
    }
}

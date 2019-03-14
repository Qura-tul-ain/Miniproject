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
using System.ComponentModel.Design;
namespace labproject
{
    public partial class student_form : Form
    {
        public student_form()
        {
            InitializeComponent();
        }

        //public string constr = "server=DESKTOP-G0K5DQK;database=ProjectB;";

        public string constr = "Data Source = DESKTOP-G0K5DQK; Initial Catalog = ProjectB; Integrated Security = True;MultipleActiveResultSets=true;";
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == " " || textBox4.Text == " " || textBox5.Text == " " || comboBox1.Text == "")
            {
                MessageBox.Show("Give complete info");
            }
            else
            {
                var txt = textBox5.Text;
                if (textBox3.Text.Length > 11 || textBox3.Text.Length < 11)
                {
                    MessageBox.Show("Contact length should be equal to 11");
                }
               

                else if (txt[3].ToString() != "-" || txt[6].ToString() != "-" || txt.Length > 9 || txt[3].ToString()=="" || txt[6].ToString() == "" || txt.Length < 8)
                {
                    MessageBox.Show("Enter Registration no in correct format,Correct format is 2016-cs-258");
                }
                else
                {
                    
                    
                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    // for checking if reg no already exists.
                    string check = "Select * from Student where RegistrationNumber='" + textBox5.Text + "' ";
                    SqlCommand comcheck = new SqlCommand(check, con);
                    SqlDataAdapter adapt = new SqlDataAdapter();
                    adapt.SelectCommand = new SqlCommand(check, con);
                    DataTable ds = new DataTable();
                    adapt.Fill(ds);
                    int i = ds.Rows.Count;
                    if (i > 0)
                    {
                        MessageBox.Show(" Student Already Exists");
                        ds.Clear();
                    }
                    else
                    {

                        String cmds = "SELECT * FROM Lookup";
                        SqlCommand command = new SqlCommand(cmds, con);

                        SqlDataReader reader = command.ExecuteReader();

                        int columnValue;
                        string m;
                        if (con.State == ConnectionState.Open)
                        {
                            while (reader.Read())
                            {
                                if (reader[1].ToString() == comboBox1.Text)
                                {
                                    m = reader[1].ToString();
                                    columnValue = Convert.ToInt32(reader[0]);
                                    string query = "INSERT INTO  Student (FirstName, LastName, Contact,Email,RegistrationNumber,Status)VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + columnValue + "')";
                                    SqlCommand cmd = new SqlCommand(query, con);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Successfully Inserted");
                                }
                            }
                        }

                        else
                        {
                            MessageBox.Show("erro");
                        }
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attendance obj = new Attendance();
            this.Hide();
            obj.Show();
        }

      
            string oldText = string.Empty;
    private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^a-zA-Z]"))
            {
                MessageBox.Show("Please enter only Alphabatic char.");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^a-zA-Z]"))
            {
                MessageBox.Show("Please enter only Alphabatic char.");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]") )
            {
                MessageBox.Show("Please enter only numbers.");
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            }
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Show_student obj = new Show_student();
            this.Hide();
            obj.Show();
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (textBox4.Text.Length > 0 && textBox4.Text.Trim().Length != 0)
            {
                if (!rEmail.IsMatch(textBox4.Text.Trim()))
                {
                    MessageBox.Show("Please check your email id");
                    textBox4.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

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
    }
}

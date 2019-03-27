using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labproject
{
    public partial class start : Form
    {
        public start()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            student_form obj = new student_form();
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
            Attendance obj = new Attendance();
            this.Hide();
            obj.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Show_student obj = new Show_student();
            this.Hide();
            obj.Show();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLO obj = new CLO();
            this.Hide();
            obj.Show();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clo_rubric_level obj = new Clo_rubric_level();
            this.Hide();
            obj.Show();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assessment obj = new Assessment();
            this.Hide();
            obj.Show();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assess_component obj = new Assess_component();
            this.Hide();
            obj.Show();
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Student_assessment obj = new Student_assessment();
            this.Hide();
            obj.Show();
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            show_result obj = new show_result();
            this.Hide();
            obj.Show();
        }
    }
}

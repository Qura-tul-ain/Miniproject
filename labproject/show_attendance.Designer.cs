﻿namespace labproject
{
    partial class show_attendance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Att_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student Attendance";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Att_Status});
            this.dataGridView1.Location = new System.Drawing.Point(121, 150);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(650, 407);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Att_Status
            // 
            this.Att_Status.HeaderText = "AttendanceStatus";
            this.Att_Status.Name = "Att_Status";
            this.Att_Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Att_Status.ToolTipText = "AttendanceStatus";
            this.Att_Status.Width = 150;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(931, 335);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(132, 25);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Add Student";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(917, 387);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(158, 25);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Show Students";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.Location = new System.Drawing.Point(931, 186);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(102, 25);
            this.linkLabel3.TabIndex = 4;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Add CLO";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.Location = new System.Drawing.Point(931, 227);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(117, 25);
            this.linkLabel4.TabIndex = 5;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Show CLO";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel5.Location = new System.Drawing.Point(931, 280);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(124, 25);
            this.linkLabel5.TabIndex = 6;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Home Page";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel6.Location = new System.Drawing.Point(917, 437);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(175, 25);
            this.linkLabel6.TabIndex = 7;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Add Assessment";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // linkLabel7
            // 
            this.linkLabel7.AutoSize = true;
            this.linkLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel7.Location = new System.Drawing.Point(917, 484);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(176, 25);
            this.linkLabel7.TabIndex = 8;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "Mark Attendence";
            this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel7_LinkClicked);
            // 
            // show_attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 651);
            this.Controls.Add(this.linkLabel7);
            this.Controls.Add(this.linkLabel6);
            this.Controls.Add(this.linkLabel5);
            this.Controls.Add(this.linkLabel4);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "show_attendance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "show_attendance";
            this.Load += new System.EventHandler(this.show_attendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Att_Status;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel7;
    }
}
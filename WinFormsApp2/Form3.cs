using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsApp2
{
    public partial class Form3 : Form
    {
        private bool is_aut = false;
        private Form1 parent;
        private DB_work dbwork;
        public Form3(Form1 parent, DB_work dbwork)
        {
            InitializeComponent();
            this.parent = parent;
            this.dbwork = dbwork;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!is_aut)
            {
                dbwork.disconnect();
                Application.Exit();
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            

            string log_text = textBox1.Text;
            string pas_text = textBox2.Text;


            if (string.IsNullOrEmpty(log_text))
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            if (dbwork.auth_user(log_text, pas_text))
            {
                parent.UserAuthed();
                is_aut = true;
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form4 regis = new Form4(dbwork);
            regis.ShowDialog();
        }
    }
}

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
    public partial class Form4 : Form
    {
        private DB_work dbwork;


        public Form4(DB_work dbwork)
        {
            InitializeComponent();
            this.dbwork = dbwork;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password1 = textBox2.Text;
            string password2 = textBox3.Text;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин!");
            }
            else if (password1 !=  password2)
            {
                MessageBox.Show("Пароли не совпадают! Проверьте правильность написания и попробуйте ещё раз.");
            }
            else
            {
                if (dbwork.add_new_user(login, password1) == true)
                {
                    MessageBox.Show("Ваша учётная запись зарегистрирована.");
                    this.Close();
                }
            }
            
        }
    }
}

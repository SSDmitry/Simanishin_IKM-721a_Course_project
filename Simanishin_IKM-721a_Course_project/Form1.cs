using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simanishin_IKM_721a_Course_project
{
    public partial class Form1 : Form
    {
        private bool Mode = false;
        private MajorWork MajorObject;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            About A = new About();
            A.tAbout.Start();
            A.ShowDialog();
            MajorObject = new MajorWork();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tClock_Tick(object sender, EventArgs e)
        {
            tClock.Stop();
            MessageBox.Show("the time has passed!");
            tClock.Start();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (Mode)
            {
                tbInput.Enabled = false;
                Mode = false;
                bStart.Text = "Пуск";
                tClock.Stop();
                MajorObject.Write(tbInput.Text);
                MajorObject.Task();
                label1.Text = MajorObject.Read();
            }
            else
            {
                tbInput.Enabled = true;
                Mode = true;
                bStart.Text = "Стоп";
                tClock.Start();
                tbInput.Focus();
            }
        }

        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            tClock.Stop();
            tClock.Start();

            if ((e.KeyChar >= '0' && e.KeyChar <= '9') | (e.KeyChar == (char)8) | (e.KeyChar == '.') | (e.KeyChar == ' '))
            {
                return;

            }
            else
            {
                MessageBox.Show("Неправильний символ", "Помилка");
                e.KeyChar = (char)0;
            }
        }
    }
}
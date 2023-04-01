﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

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
            MajorObject.SetTime();

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
                пускToolStripMenuItem.Text = "Старт";
            }
            else
            {
                tbInput.Enabled = true;
                Mode = true;
                bStart.Text = "Стоп";
                tClock.Start();
                tbInput.Focus();
                пускToolStripMenuItem.Text = "Стоп";
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string s;
            s = (System.DateTime.Now - MajorObject.GetTime()).ToString();
            MessageBox.Show(s, "Час роботи програми");
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About A = new About();
            A.ShowDialog();
        }

        private void зберегтиЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(sfdSave.FileName);
            }
        }

        private void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdOpen.ShowDialog() == DialogResult.OK)

            {
                MessageBox.Show(ofdOpen.FileName);
            }
        }

        private void проНакопичувачіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] disks = System.IO.Directory.GetLogicalDrives();
            string disk = "";
            for (int i = 0; i < disks.Length; i++)
            {
                try
                {
                    System.IO.DriveInfo D = new System.IO.DriveInfo(disks[i]);
                    disk += D.Name + "-" + D.TotalSize.ToString() + "-" + D.TotalFreeSpace.ToString() + (char)13;
                }
                catch
                {
                    disk += disks[i] + "- не готовий" + (char)13;
}
            }
            MessageBox.Show(disk, "Накопичувачі");
        }

        private void роботаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
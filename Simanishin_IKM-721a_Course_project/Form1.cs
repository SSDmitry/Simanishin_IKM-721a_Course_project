using System;
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
        private bool Mode = false; //Режим дозволу / заборони введення даних
        private MajorWork MajorObject; // Створення об'єкта класу MajorWork
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
        public Form1()
        {
            InitializeComponent();
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Поточні дата і час:";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();
            statusStrip1.Items.Add(infoLabel);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);
            timer = new Timer() { Interval = 1000 };
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();

            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MajorObject = new MajorWork();
            MajorObject.SetTime();
            MajorObject.Modify = false; //заборона запису
            About A = new About(); //сворення форми About
            A.tAbout.Start(); //відображення діалогового вікна About
            A.ShowDialog();
            this.Mode = true;

            toolTip1.SetToolTip(bSearch, "Натисніть на кнопку для пошуку"); 
            toolTip1.IsBalloon = true;
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
            MessageBox.Show("the time has passed!"); // Виведення повідомлення "the time has passed!" на екран
            tClock.Start();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (Mode)
            {
                tbInput.Enabled = false; //Режим заборони введення
                Mode = false;
                bStart.Text = "Пуск"; //зміна тексту на кнопці на "Пуск"
                tClock.Stop();
                MajorObject.Write(tbInput.Text); //запис даних у об'єкт
                MajorObject.Task(); //Обробка даних
                label1.Text = MajorObject.Read(); //Відображення результату
                пускToolStripMenuItem.Text = "Старт";
            }
            else
            {
                tbInput.Enabled = true; //Режим дозволу введення
                Mode = true;
                bStart.Text = "Стоп"; //зміна тексту на кнопці на "Стоп"
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
                tClock.Stop();
                MessageBox.Show("Неправильний символ", "Помилка");
                tClock.Start();
                e.KeyChar = (char)0;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string s;
            s = (System.DateTime.Now - MajorObject.GetTime()).ToString();
            MessageBox.Show(s, "Час роботи програми"); //Виведення часу роботи програми і повідомлення "Час роботи програми" на екран
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About A = new About();
            A.ShowDialog();
            A.progressBar1.Hide();
        }

        private void зберегтиЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK) //Виклик діалогу збереження файлу
            {
                MajorObject.WriteSaveFileName(sfdSave.FileName); // запис імені файла для збереження
                MajorObject.Generator();
                MajorObject.SaveToFile(); //метод збереження в файл
            }
        }

        private void відкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdOpen.ShowDialog() == DialogResult.OK) //Виклик діалогу відкриття файлу
            {
                MajorObject.WriteOpenFileName(ofdOpen.FileName); // відкриття файлу
                MajorObject.ReadFromFile(dgwOpen); // читання даних з файлу
            }
        }

        private void проНакопичувачіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] disks = System.IO.Directory.GetLogicalDrives(); // строковий масив з логічних дисків
            string disk = "";
            for (int i = 0; i < disks.Length; i++)
            {
                try
                {
                    System.IO.DriveInfo D = new System.IO.DriveInfo(disks[i]);
                    disk += D.Name + "-" + D.TotalSize.ToString() + "-" + D.TotalFreeSpace.ToString() + (char)13; // змінній присвоюється ім'я диска, загальна кількість місця і вільне місце на диску
                }
                catch
                {
                    disk += disks[i] + "- не готовий" + (char)13; //якщо пристрій не готовий виведення на екран ім'я пристрою і повідомлення "не готовий"
}
            }
            MessageBox.Show(disk, "Накопичувачі");
        }

        private void роботаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MajorObject.SaveFileNameExists()) // задане ім'я файлу існує?
                MajorObject.SaveToFile(); // зберегти в дані в файл
            else
                зберегтиЯкToolStripMenuItem_Click(sender, e); // зберегти дані в "ім'я файлу", яке буде введене у форму для збереження
        }
        private void новийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MajorObject.NewRec();
            tbInput.Clear(); //очистити вміст текст бокса
            label1.Text = "";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MajorObject.Modify)
            if (MessageBox.Show("Дані не були збережені. Продовжити вихід?", "УВАГА",
            MessageBoxButtons.YesNo) == DialogResult.No)
            e.Cancel = true; // припинити закриття
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            MajorObject.Find(tbSearch.Text); //пошук
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
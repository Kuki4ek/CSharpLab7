using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7
{
    public partial class Form1 : Form
    {
        public List<Time> times = new List<Time>();
        private Random random = new Random();
        public IEnumerable<Time> saved;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                times.Add(new Time(random.Next(24), random.Next(60), random.Next(60)));
            }
            updateListBox();
        }
        public void updateRequestBox(IEnumerable<Time> request)
        {
            string listbox_text = "";
            foreach (Time time in request)
            {
                listbox_text += time.ToString() + "\r\n";
            }
            textBox2.Text = listbox_text;
        }
        public void updateListBox()
        {
            string listbox_text = "";
            foreach (Time time in times)
            {
                listbox_text += time.ToString() + "\r\n";
            }
            textBox1.Text = listbox_text;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = radioButton1.Checked;
            groupBox3.Enabled = !radioButton1.Checked;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = radioButton2.Checked;
            groupBox2.Enabled = !radioButton2.Checked;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Enabled = radioButton3.Checked;
            groupBox5.Enabled = !radioButton3.Checked;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox5.Enabled = radioButton4.Checked;
            groupBox4.Enabled = !radioButton4.Checked;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.times.Add(new Time(dateTimePicker1.Value));
            updateListBox();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int number_remove = (int) numericUpDown1.Value;
            if (number_remove >= 0 && number_remove < times.Count)
            {
                this.times.RemoveAt(number_remove);
                updateListBox();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Time timePickerValue = new Time(dateTimePicker2.Value);
            for(int i = 0; i < times.Count; i++)
            {
                if (times[i].GetFullSecond() == timePickerValue.GetFullSecond())
                {
                    this.times.RemoveAt(i);
                    updateListBox();
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var request = from t in this.times 
                          where t.hour < 12 
                          orderby t.hour, t.minute, t.second 
                          select t;

            updateRequestBox(request);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var request = from t in this.times 
                          where t.hour >= 12 
                          orderby t.hour, t.minute, t.second 
                          select t;

            updateRequestBox(request);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            var request = from t in this.times 
                          orderby t.hour, t.minute, t.second 
                          select t;

            updateRequestBox(request);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            var request = from t in this.times 
                          where t.hour >= 8 && t.hour <= 17 
                          orderby t.hour, t.minute, t.second 
                          select t;

            updateRequestBox(request);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            saved = (from t in this.times 
                orderby t.hour, t.minute, t.second 
                select t).ToList();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            updateRequestBox(this.saved);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string listbox_text = "";
            var groups = from t in this.times 
                         group t by t.hour
                         into g
                         orderby g.Key
                         select g;
            foreach (var group in groups)
            {
                listbox_text += $"Группа {group.Key}" + "\r\n";
                foreach (var time in group)
                {
                    listbox_text+= time.ToString() + "\r\n";
                }
            }
            textBox2.Text = listbox_text;
        }
    }
}

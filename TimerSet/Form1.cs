using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Config;

namespace TimerSet
{
    public partial class Form1 : Form
    {
        Config.Data Data;
        
        public Form1()
        {
            try
            {
                Data = DataSerializor.GetData();
            }
            catch (Exception )
            {
                MessageBox.Show("配置文件损坏，将自动生成新文件");
                
                    
            }

            if (Data == null)
            {
                Data = new Data
                {
                    StartTime = DateTime.Today,
                    Type = "天"
                };
            }

            InitializeComponent();

            dateTimePicker1.Value = Data.StartTime;
            comboBox1.SelectedItem = Data.Type;
            if (Data.Type == "周")
                comboBox2.SelectedItem = Data.Span;
            else if (Data.Type == "月")
                comboBox3.SelectedItem = Data.Span;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.StartTime = dateTimePicker1.Value;
            Data.Type = Convert.ToString(comboBox1.SelectedItem);
            if (Data.Type == "天")
                Data.Span = "";
            else if (Data.Type == "周")
                Data.Span = Convert.ToString(comboBox2.SelectedItem);
            else if (Data.Type == "月")
                Data.Span = Convert.ToString(comboBox3.SelectedItem);

            try
            {
                DataSerializor.SetData(Data);
                Application.Exit();
            }
            catch (Exception )
            {
                MessageBox.Show("设置保存失败");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = Convert.ToString(comboBox1.SelectedItem);
            if (type == "天")
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
            else if (type == "周")
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;
            }
            else if (type == "月")
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = true;
            }
        }
    }
}

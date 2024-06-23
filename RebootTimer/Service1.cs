using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Config;

namespace RebootTimer
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private Data data;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(60000);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();
            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                data = DataSerializor.GetData();
            }
            catch
            {
                data = null;
            }

            if (data == null)
            {
                return;
            }

            if (CheckDate())
            {
                if( data.StartTime.Hour == DateTime.Now.Hour && data.StartTime.Minute == DateTime.Now.Minute) 
                {
                    System.Diagnostics.Process.Start("shutdown", "/r /f /t 0");
                }
            }

        }

        private bool CheckDate()
        {
            var date = DateTime.Now;
            if (data.Type == "天")
                return true;
            else if (data.Type == "周")
            {
                if ((int)date.DayOfWeek == (Convert.ToInt32(data.Span) % 7))
                    return true;
                else return false;
            }
            else if (data.Type == "月")
            {
                if (date.Day == Convert.ToInt32(data.Span))
                    return true;
                else return false;
            }
            else return false;
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void contextMenuStrip1_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            
        }
    }
}

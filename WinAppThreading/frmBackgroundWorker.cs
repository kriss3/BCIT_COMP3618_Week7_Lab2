using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Logic;

namespace WinAppThreading
{
    public partial class frmBackgroundWorker : Form
    {
        public AppLogic Helper { get; private set; }
        DateTime startDateTime = DateTime.Now;

        public frmBackgroundWorker()
        {
            InitializeComponent();
            Helper = new AppLogic();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"{Helper.GetTester("Kris")}", "Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmBackgroundWorker_Activated(object sender, EventArgs e)
        {
            bgw.RunWorkerAsync();
            timer.Start();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt;
            toolStripLabel.Text = "Loading ... " + "Thanks for your patience";
            dt = GetDataTable(1000000);
            e.Result = dt;
            toolStripLabel.Text = "Please, wait ...";
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar.Value = 100;
            dataGridViewCities.DataSource = e.Result;
            toolStripLabel.Text = "";
            toolStripProgressBar.Value = 0;
            timer.Stop();
            toolStripTime.Text = "";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now.Subtract(startDateTime);
            string sTime = $"  ... {ts.Minutes.ToString("00")} : {ts.Seconds.ToString("00")} : {ts.Milliseconds.ToString("000")}";

            toolStripTime.Text = sTime;
            if (toolStripProgressBar.Value == toolStripProgressBar.Maximum)
            {
                toolStripProgressBar.Value = 0;
            }
            toolStripProgressBar.PerformStep();
        }

        #region Private methods

        private DataTable GetDataTable(int Rows)
        {
            GetDataHelper getData = new GetDataHelper();
            return (getData.GetDataSetCities(Rows).Tables[0]);
        }

        #endregion

    }
}

using BLL.Logic;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace WinAppThreading
{
    public partial class frmBackgroundWorker : Form
    {
        #region Fields

        DateTime startDateTime = DateTime.Now;

        #endregion

        #region Operataions

        public frmBackgroundWorker()
        {
            InitializeComponent();
        }

        private void frmBackgroundWorker_Activated(object sender, EventArgs e)
        {
            bgw.RunWorkerAsync();
            timer.Start();
        }

        //The DoWork event occurs when the RunWorkersAsync method is called.  
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt;
            toolStripLabel.Text = "Loading ... " + "Thanks for your patience";
            dt = GetDataTable(1000000);
            e.Result = dt;
            toolStripLabel.Text = "Please, wait ...";
        }

        //the RunWorkerCompleted event occurs when the background operation has completed
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

        #endregion Operations
        
        #region Private methods

        private DataTable GetDataTable(int Rows)
        {
            GetDataHelper getData = new GetDataHelper();
            return (getData.GetDataSetCities(Rows).Tables[0]);
        }

        #endregion Private methods
    }
}

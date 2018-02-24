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
    public partial class frmMain : Form
    {
        public AppLogic Helper { get; private set; }
        public frmMain()
        {
            InitializeComponent();
            Helper = new AppLogic();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show($"{Helper.GetTester("Kris")}", "Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

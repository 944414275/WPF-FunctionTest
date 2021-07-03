using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsBarCodeScanner
{
    public partial class Form1 : Form
    {
        private ScanerHook listener = new ScanerHook();
        public Form1()
        {
            InitializeComponent();
            listener.ScanerEvent += Listener_ScanerEvent;
        }

        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {
            textBox3.Text = codes.Result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listener.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            listener.Stop();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Mogre;

namespace Wingitor
{
    public partial class MainWindow  : Form
    {
        private Thread renderThread;
        public MainWindow()
        {
            InitializeComponent();

            if(!editorRenderPanel.Setup())
            {
                return;
            }
            renderThread = new Thread(editorRenderPanel.Go);
            renderThread.Start();
           

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                editorRenderPanel.Running = false;
                renderThread.Join();

            }
            catch (Exception)
            {
            }
           
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void quit(object sender, EventArgs e)
        {
            Close();
        }

        private void load(object sender, EventArgs e)
        {
           DialogResult result=  openFileDialog.ShowDialog();

          
        }
        private void save(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog.ShowDialog();
        }
     

       
    }
}
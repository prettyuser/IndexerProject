using IndexerProject.Common;
using IndexerProject.Util;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndexerProject
{
    public partial class Form1 : Form
    {
        #region Fields

        Watcher watcher = null;

        int counter = 0;

        string path = null;

        IKernel kernel = null;

        #endregion Fields

        #region Constructor

        public Form1()
        {
            InitializeComponent();
            kernel = new StandardKernel(new NinjectRegistrations());
            watcher = kernel.Get<Watcher>();
        }

        #endregion Constructor

        #region UIHandlers

        private void btnDeleteIndexes_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtBoxPath.Text))
            {
                path = txtBoxPath.Text;
                kernel.Get<RecursivelyIndexing>(new ConstructorArgument("kernel", kernel)).DeleteIndexing(path, true);
                MessageBox.Show("Delete Completed!");
            }
            else
            {
                MessageBox.Show("Can't delete! Wrong path!");
            }
        }

        private void btnCreateIndexes_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtBoxPath.Text))
            {
                path = txtBoxPath.Text;
                kernel.Get<RecursivelyIndexing>(new ConstructorArgument("kernel", kernel)).DirectoryIndexing(path, true);
                MessageBox.Show("Create Completed!");
            }
            else
            {
                MessageBox.Show("Can't create! Wrong path!");
            }
        }

        private void btnStartTracing_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtBoxPath.Text))
            {
                btnStartTracing.Enabled = false;
                btnStopTracing.Enabled = true;
                btnStopTracing.BackColor = Control.DefaultBackColor;
                btnStartTracing.BackColor = Color.DarkGray;

                watcher.WatchDirectoryName = txtBoxPath.Text;
                watcher.PropertyChanged += DetectedNewFiles;
                watcher.RunReactive(kernel);
            }
            else
            {
                MessageBox.Show("Wrong path!");
                txtBoxPath.Text = "Type a folder's path...";
            }            
        }

        private void btnStopTracing_Click(object sender, EventArgs e)
        {
            btnStopTracing.Enabled = false;
            btnStartTracing.Enabled = true;

            btnStartTracing.BackColor = Control.DefaultBackColor;
            btnStopTracing.BackColor = Color.DarkGray;

            watcher.StopReactive();
            lstBoxTraceInfo.Items.Clear();
        }

        #region PureUI

        private void txtBoxPath_Leave(object sender, EventArgs e)
        {
            if (txtBoxPath.Text == "Type a folder's path..." || String.IsNullOrWhiteSpace(txtBoxPath.Text))
            {
                txtBoxPath.Text = "Type a folder's path...";
                txtBoxPath.ForeColor = Color.Gray;
            }
            else
            {
                txtBoxPath.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtBoxPath_Changed(object sender, EventArgs e)
        {
            if (txtBoxPath.Text == "Type a folder's path...")
            {
                txtBoxPath.ForeColor = Color.Gray;
            }
            else
            {
                txtBoxPath.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtBoxPath_Click(object sender, EventArgs e)
        {
            if (txtBoxPath.Text == "Type a folder's path..." || String.IsNullOrWhiteSpace(txtBoxPath.Text) || txtBoxPath.ForeColor == Color.Gray)
            {
                txtBoxPath.Text = "";
                txtBoxPath.ForeColor = SystemColors.WindowText;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtBoxPath.Text = "Type a folder's path...";
            txtBoxPath.ForeColor = Color.Gray;
        }

        private void btnChoosePath_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtBoxPath.Text) || String.IsNullOrWhiteSpace(txtBoxPath.Text) || txtBoxPath.Text == "Type a folder's path...")
            {
                using (var fbd = new FolderBrowserDialog() { SelectedPath = Directory.Exists(txtBoxPath.Text) ? txtBoxPath.Text : "" })
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        txtBoxPath.Text = fbd.SelectedPath;
                    }
                }
            }
            else
            {
                MessageBox.Show("Path doesn't exist!");
                txtBoxPath.Text = "Type a folder's path...";
            }
        }

        private void btnClearTextField_Click(object sender, EventArgs e)
        {
            lstBoxTraceInfo.Items.Clear();
        }

        #endregion PureUI

        #endregion UIHandlers

        #region Handlers

        private void DetectedNewFiles(object sender, EventArgs e)
        {
            if(watcher.synchro == null)
            {
                while (watcher.ListOfFiles.Any())
                {
                    lock (watcher.synchro)
                    {
                        lstBoxTraceInfo.BeginInvoke(new Action(() =>
                        {
                            foreach (var item in watcher.ListOfFiles)
                            {
                                lstBoxTraceInfo.Items.Add(++counter + ": " + item);
                            }

                            watcher.ListOfFiles.Clear();

                            lstBoxTraceInfo.Refresh();
                        }));
                    }

                }
            }
        }

        #endregion Handlers

        
    }
}

namespace IndexerProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lstBoxTraceInfo = new System.Windows.Forms.ListBox();
            this.btnDeleteIndexes = new System.Windows.Forms.Button();
            this.btnCreateIndexes = new System.Windows.Forms.Button();
            this.btnStartTracing = new System.Windows.Forms.Button();
            this.btnStopTracing = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxPath = new System.Windows.Forms.TextBox();
            this.btnChoosePath = new System.Windows.Forms.Button();
            this.btnClearTextField = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstBoxTraceInfo
            // 
            this.lstBoxTraceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBoxTraceInfo.FormattingEnabled = true;
            this.lstBoxTraceInfo.Location = new System.Drawing.Point(13, 39);
            this.lstBoxTraceInfo.Name = "lstBoxTraceInfo";
            this.lstBoxTraceInfo.Size = new System.Drawing.Size(679, 264);
            this.lstBoxTraceInfo.TabIndex = 0;
            // 
            // btnDeleteIndexes
            // 
            this.btnDeleteIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteIndexes.Location = new System.Drawing.Point(559, 378);
            this.btnDeleteIndexes.Name = "btnDeleteIndexes";
            this.btnDeleteIndexes.Size = new System.Drawing.Size(133, 23);
            this.btnDeleteIndexes.TabIndex = 1;
            this.btnDeleteIndexes.Text = "Delete Indexes";
            this.btnDeleteIndexes.UseVisualStyleBackColor = true;
            this.btnDeleteIndexes.Click += new System.EventHandler(this.btnDeleteIndexes_Click);
            // 
            // btnCreateIndexes
            // 
            this.btnCreateIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateIndexes.Location = new System.Drawing.Point(559, 353);
            this.btnCreateIndexes.Name = "btnCreateIndexes";
            this.btnCreateIndexes.Size = new System.Drawing.Size(133, 23);
            this.btnCreateIndexes.TabIndex = 2;
            this.btnCreateIndexes.Text = "Create Indexes";
            this.btnCreateIndexes.UseVisualStyleBackColor = true;
            this.btnCreateIndexes.Click += new System.EventHandler(this.btnCreateIndexes_Click);
            // 
            // btnStartTracing
            // 
            this.btnStartTracing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartTracing.Location = new System.Drawing.Point(12, 353);
            this.btnStartTracing.Name = "btnStartTracing";
            this.btnStartTracing.Size = new System.Drawing.Size(75, 23);
            this.btnStartTracing.TabIndex = 3;
            this.btnStartTracing.Text = "Start";
            this.btnStartTracing.UseVisualStyleBackColor = true;
            this.btnStartTracing.Click += new System.EventHandler(this.btnStartTracing_Click);
            // 
            // btnStopTracing
            // 
            this.btnStopTracing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStopTracing.Location = new System.Drawing.Point(12, 378);
            this.btnStopTracing.Name = "btnStopTracing";
            this.btnStopTracing.Size = new System.Drawing.Size(75, 23);
            this.btnStopTracing.TabIndex = 3;
            this.btnStopTracing.Text = "Stop";
            this.btnStopTracing.UseVisualStyleBackColor = true;
            this.btnStopTracing.Click += new System.EventHandler(this.btnStopTracing_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 332);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tracing changes:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(556, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Manage processing:";
            // 
            // txtBoxPath
            // 
            this.txtBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxPath.Location = new System.Drawing.Point(13, 13);
            this.txtBoxPath.Name = "txtBoxPath";
            this.txtBoxPath.Size = new System.Drawing.Size(637, 20);
            this.txtBoxPath.TabIndex = 6;
            this.txtBoxPath.Click += new System.EventHandler(this.txtBoxPath_Click);
            this.txtBoxPath.TextChanged += new System.EventHandler(this.txtBoxPath_Changed);
            this.txtBoxPath.Leave += new System.EventHandler(this.txtBoxPath_Leave);
            // 
            // btnChoosePath
            // 
            this.btnChoosePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoosePath.Location = new System.Drawing.Point(656, 13);
            this.btnChoosePath.Name = "btnChoosePath";
            this.btnChoosePath.Size = new System.Drawing.Size(35, 19);
            this.btnChoosePath.TabIndex = 7;
            this.btnChoosePath.Text = "...";
            this.btnChoosePath.UseVisualStyleBackColor = true;
            this.btnChoosePath.Click += new System.EventHandler(this.btnChoosePath_Click);
            // 
            // btnClearTextField
            // 
            this.btnClearTextField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearTextField.BackColor = System.Drawing.Color.White;
            this.btnClearTextField.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClearTextField.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClearTextField.FlatAppearance.BorderSize = 0;
            this.btnClearTextField.Location = new System.Drawing.Point(648, 40);
            this.btnClearTextField.Name = "btnClearTextField";
            this.btnClearTextField.Size = new System.Drawing.Size(42, 23);
            this.btnClearTextField.TabIndex = 8;
            this.btnClearTextField.Text = "clear";
            this.btnClearTextField.UseVisualStyleBackColor = false;
            this.btnClearTextField.Click += new System.EventHandler(this.btnClearTextField_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 411);
            this.Controls.Add(this.btnClearTextField);
            this.Controls.Add(this.btnChoosePath);
            this.Controls.Add(this.txtBoxPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStopTracing);
            this.Controls.Add(this.btnStartTracing);
            this.Controls.Add(this.btnCreateIndexes);
            this.Controls.Add(this.btnDeleteIndexes);
            this.Controls.Add(this.lstBoxTraceInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(720, 450);
            this.Name = "Form1";
            this.Text = "Indexer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstBoxTraceInfo;
        private System.Windows.Forms.Button btnDeleteIndexes;
        private System.Windows.Forms.Button btnCreateIndexes;
        private System.Windows.Forms.Button btnStartTracing;
        private System.Windows.Forms.Button btnStopTracing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxPath;
        private System.Windows.Forms.Button btnChoosePath;
        private System.Windows.Forms.Button btnClearTextField;
    }
}


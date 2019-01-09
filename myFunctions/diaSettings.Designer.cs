namespace CAS.myFunctions
{
    partial class DiaSettings
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelExport = new System.Windows.Forms.Panel();
            this.tb_Header = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tB_Separator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Decimal = new System.Windows.Forms.ComboBox();
            this.cB_Header = new System.Windows.Forms.CheckBox();
            this.cB_OutputFile = new System.Windows.Forms.CheckBox();
            this.btExportFile = new System.Windows.Forms.Button();
            this.tb_PunktExport = new System.Windows.Forms.TextBox();
            this.panelImport = new System.Windows.Forms.Panel();
            this.cbBlock = new System.Windows.Forms.ComboBox();
            this.labelBlock = new System.Windows.Forms.Label();
            this.cbBasislayer = new System.Windows.Forms.ComboBox();
            this.labelBasislayer = new System.Windows.Forms.Label();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelExport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelImport.SuspendLayout();
            this.panelGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(634, 461);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.SplitterIncrement = 1000;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(165, 461);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.splitContainer2.Panel1.Controls.Add(this.panelExport);
            this.splitContainer2.Panel1.Controls.Add(this.panelImport);
            this.splitContainer2.Panel1.Controls.Add(this.panelGeneral);
            this.splitContainer2.Panel1MinSize = 400;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btOK);
            this.splitContainer2.Size = new System.Drawing.Size(465, 461);
            this.splitContainer2.SplitterDistance = 400;
            this.splitContainer2.TabIndex = 1;
            // 
            // panelExport
            // 
            this.panelExport.BackColor = System.Drawing.SystemColors.Control;
            this.panelExport.Controls.Add(this.tb_Header);
            this.panelExport.Controls.Add(this.groupBox1);
            this.panelExport.Controls.Add(this.cB_Header);
            this.panelExport.Controls.Add(this.cB_OutputFile);
            this.panelExport.Controls.Add(this.btExportFile);
            this.panelExport.Controls.Add(this.tb_PunktExport);
            this.panelExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExport.Location = new System.Drawing.Point(0, 0);
            this.panelExport.Name = "panelExport";
            this.panelExport.Size = new System.Drawing.Size(465, 400);
            this.panelExport.TabIndex = 1;
            // 
            // tb_Header
            // 
            this.tb_Header.Location = new System.Drawing.Point(39, 34);
            this.tb_Header.Name = "tb_Header";
            this.tb_Header.Size = new System.Drawing.Size(317, 20);
            this.tb_Header.TabIndex = 10;
            this.tb_Header.TextChanged += new System.EventHandler(this.tb_Header_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tB_Separator);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cb_Decimal);
            this.groupBox1.Location = new System.Drawing.Point(39, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 113);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Formatierung";
            // 
            // tB_Separator
            // 
            this.tB_Separator.Location = new System.Drawing.Point(93, 48);
            this.tB_Separator.Name = "tB_Separator";
            this.tB_Separator.Size = new System.Drawing.Size(31, 20);
            this.tB_Separator.TabIndex = 10;
            this.tB_Separator.TextChanged += new System.EventHandler(this.tB_Separator_TextChanged);
            this.tB_Separator.Validating += new System.ComponentModel.CancelEventHandler(this.tB_Separator_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Trennzeichen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dezimalzeichen";
            // 
            // cb_Decimal
            // 
            this.cb_Decimal.FormattingEnabled = true;
            this.cb_Decimal.Items.AddRange(new object[] {
            ".",
            ","});
            this.cb_Decimal.Location = new System.Drawing.Point(93, 84);
            this.cb_Decimal.Name = "cb_Decimal";
            this.cb_Decimal.Size = new System.Drawing.Size(31, 21);
            this.cb_Decimal.TabIndex = 7;
            this.cb_Decimal.TextChanged += new System.EventHandler(this.cb_Decimal_TextChanged);
            // 
            // cB_Header
            // 
            this.cB_Header.AutoSize = true;
            this.cB_Header.Location = new System.Drawing.Point(21, 9);
            this.cB_Header.Name = "cB_Header";
            this.cB_Header.Size = new System.Drawing.Size(61, 17);
            this.cB_Header.TabIndex = 5;
            this.cB_Header.Text = "Header";
            this.cB_Header.UseVisualStyleBackColor = true;
            this.cB_Header.CheckedChanged += new System.EventHandler(this.cB_Header_CheckedChanged);
            // 
            // cB_OutputFile
            // 
            this.cB_OutputFile.AutoSize = true;
            this.cB_OutputFile.Location = new System.Drawing.Point(21, 77);
            this.cB_OutputFile.Name = "cB_OutputFile";
            this.cB_OutputFile.Size = new System.Drawing.Size(137, 17);
            this.cB_OutputFile.TabIndex = 4;
            this.cB_OutputFile.Text = "Ausgabefile verwenden";
            this.cB_OutputFile.UseVisualStyleBackColor = true;
            this.cB_OutputFile.CheckedChanged += new System.EventHandler(this.cB_OutputFile_CheckedChanged);
            // 
            // btExportFile
            // 
            this.btExportFile.Location = new System.Drawing.Point(362, 100);
            this.btExportFile.Name = "btExportFile";
            this.btExportFile.Size = new System.Drawing.Size(27, 23);
            this.btExportFile.TabIndex = 2;
            this.btExportFile.UseVisualStyleBackColor = true;
            this.btExportFile.Click += new System.EventHandler(this.btExportFile_Click);
            // 
            // tb_PunktExport
            // 
            this.tb_PunktExport.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tb_PunktExport.Location = new System.Drawing.Point(39, 101);
            this.tb_PunktExport.Name = "tb_PunktExport";
            this.tb_PunktExport.ReadOnly = true;
            this.tb_PunktExport.Size = new System.Drawing.Size(317, 20);
            this.tb_PunktExport.TabIndex = 1;
            this.tb_PunktExport.TextChanged += new System.EventHandler(this.tb_PunktExport_TextChanged);
            // 
            // panelImport
            // 
            this.panelImport.BackColor = System.Drawing.SystemColors.Control;
            this.panelImport.Controls.Add(this.cbBlock);
            this.panelImport.Controls.Add(this.labelBlock);
            this.panelImport.Controls.Add(this.cbBasislayer);
            this.panelImport.Controls.Add(this.labelBasislayer);
            this.panelImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImport.Location = new System.Drawing.Point(0, 0);
            this.panelImport.Name = "panelImport";
            this.panelImport.Size = new System.Drawing.Size(465, 400);
            this.panelImport.TabIndex = 0;
            // 
            // cbBlock
            // 
            this.cbBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlock.FormattingEnabled = true;
            this.cbBlock.Location = new System.Drawing.Point(78, 49);
            this.cbBlock.Name = "cbBlock";
            this.cbBlock.Size = new System.Drawing.Size(121, 21);
            this.cbBlock.TabIndex = 3;
            this.cbBlock.SelectedIndexChanged += new System.EventHandler(this.cbBlock_SelectedIndexChanged);
            // 
            // labelBlock
            // 
            this.labelBlock.AutoSize = true;
            this.labelBlock.Location = new System.Drawing.Point(14, 52);
            this.labelBlock.Name = "labelBlock";
            this.labelBlock.Size = new System.Drawing.Size(37, 13);
            this.labelBlock.TabIndex = 2;
            this.labelBlock.Text = "Block:";
            // 
            // cbBasislayer
            // 
            this.cbBasislayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBasislayer.FormattingEnabled = true;
            this.cbBasislayer.Location = new System.Drawing.Point(78, 13);
            this.cbBasislayer.Name = "cbBasislayer";
            this.cbBasislayer.Size = new System.Drawing.Size(329, 21);
            this.cbBasislayer.TabIndex = 1;
            this.cbBasislayer.SelectedIndexChanged += new System.EventHandler(this.cbBasislayer_SelectedIndexChanged);
            // 
            // labelBasislayer
            // 
            this.labelBasislayer.AutoSize = true;
            this.labelBasislayer.Location = new System.Drawing.Point(14, 17);
            this.labelBasislayer.Name = "labelBasislayer";
            this.labelBasislayer.Size = new System.Drawing.Size(57, 13);
            this.labelBasislayer.TabIndex = 0;
            this.labelBasislayer.Text = "Basislayer:";
            // 
            // panelGeneral
            // 
            this.panelGeneral.BackColor = System.Drawing.SystemColors.Info;
            this.panelGeneral.Controls.Add(this.label1);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGeneral.Location = new System.Drawing.Point(0, 0);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(465, 400);
            this.panelGeneral.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "allgemein";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(400, 22);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(47, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // DiaSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(650, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 500);
            this.Name = "DiaSettings";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelExport.ResumeLayout(false);
            this.panelExport.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelImport.ResumeLayout(false);
            this.panelImport.PerformLayout();
            this.panelGeneral.ResumeLayout(false);
            this.panelGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelImport;
        private System.Windows.Forms.Label labelBasislayer;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Panel panelExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBasislayer;
        private System.Windows.Forms.ComboBox cbBlock;
        private System.Windows.Forms.Label labelBlock;
        private System.Windows.Forms.Button btExportFile;
        private System.Windows.Forms.TextBox tb_PunktExport;
        private System.Windows.Forms.CheckBox cB_OutputFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tB_Separator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_Decimal;
        private System.Windows.Forms.CheckBox cB_Header;
        private System.Windows.Forms.TextBox tb_Header;
        private System.Windows.Forms.Label label4;
    }
}
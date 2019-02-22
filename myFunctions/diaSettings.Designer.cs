﻿namespace CAS.myFunctions
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
            this.panelImport = new System.Windows.Forms.Panel();
            this.nuUD_Überhöhung = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_AttH = new System.Windows.Forms.ComboBox();
            this.cb_Z = new System.Windows.Forms.ComboBox();
            this.cb_Y = new System.Windows.Forms.ComboBox();
            this.cb_X = new System.Windows.Forms.ComboBox();
            this.lb_Z = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_3dImport = new System.Windows.Forms.CheckBox();
            this.cB_ExportFile = new System.Windows.Forms.CheckBox();
            this.lb_Prototypzeichnung = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbBlock = new System.Windows.Forms.ComboBox();
            this.labelBlock = new System.Windows.Forms.Label();
            this.cbBasislayer = new System.Windows.Forms.ComboBox();
            this.labelBasislayer = new System.Windows.Forms.Label();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelExport = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numUD_Kommastellen = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tB_Separator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Decimal = new System.Windows.Forms.ComboBox();
            this.tb_Header = new System.Windows.Forms.TextBox();
            this.cB_Header = new System.Windows.Forms.CheckBox();
            this.cB_OutputFile = new System.Windows.Forms.CheckBox();
            this.btExportFile = new System.Windows.Forms.Button();
            this.tb_PunktExport = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuUD_Überhöhung)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelGeneral.SuspendLayout();
            this.panelExport.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Kommastellen)).BeginInit();
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
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1_BeforeCollapse);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
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
            this.splitContainer2.Panel1.Controls.Add(this.panelImport);
            this.splitContainer2.Panel1.Controls.Add(this.panelGeneral);
            this.splitContainer2.Panel1.Controls.Add(this.panelExport);
            this.splitContainer2.Panel1MinSize = 400;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btOK);
            this.splitContainer2.Size = new System.Drawing.Size(465, 461);
            this.splitContainer2.SplitterDistance = 400;
            this.splitContainer2.TabIndex = 1;
            // 
            // panelImport
            // 
            this.panelImport.BackColor = System.Drawing.SystemColors.Control;
            this.panelImport.Controls.Add(this.nuUD_Überhöhung);
            this.panelImport.Controls.Add(this.label9);
            this.panelImport.Controls.Add(this.panel1);
            this.panelImport.Controls.Add(this.cb_3dImport);
            this.panelImport.Controls.Add(this.cB_ExportFile);
            this.panelImport.Controls.Add(this.lb_Prototypzeichnung);
            this.panelImport.Controls.Add(this.label6);
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
            // nuUD_Überhöhung
            // 
            this.nuUD_Überhöhung.DecimalPlaces = 2;
            this.nuUD_Überhöhung.Location = new System.Drawing.Point(280, 212);
            this.nuUD_Überhöhung.Name = "nuUD_Überhöhung";
            this.nuUD_Überhöhung.Size = new System.Drawing.Size(66, 20);
            this.nuUD_Überhöhung.TabIndex = 12;
            this.nuUD_Überhöhung.ValueChanged += new System.EventHandler(this.nuUD_Überhöhung_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(277, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Überhöhung (Y)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_AttH);
            this.panel1.Controls.Add(this.cb_Z);
            this.panel1.Controls.Add(this.cb_Y);
            this.panel1.Controls.Add(this.cb_X);
            this.panel1.Controls.Add(this.lb_Z);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(84, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 80);
            this.panel1.TabIndex = 10;
            // 
            // cb_AttH
            // 
            this.cb_AttH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AttH.FormattingEnabled = true;
            this.cb_AttH.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.cb_AttH.Location = new System.Drawing.Point(125, 31);
            this.cb_AttH.Name = "cb_AttH";
            this.cb_AttH.Size = new System.Drawing.Size(31, 21);
            this.cb_AttH.TabIndex = 16;
            this.cb_AttH.SelectedIndexChanged += new System.EventHandler(this.Cb_AttH_SelectedIndexChanged);
            // 
            // cb_Z
            // 
            this.cb_Z.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Z.FormattingEnabled = true;
            this.cb_Z.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.cb_Z.Location = new System.Drawing.Point(80, 31);
            this.cb_Z.Name = "cb_Z";
            this.cb_Z.Size = new System.Drawing.Size(31, 21);
            this.cb_Z.TabIndex = 15;
            this.cb_Z.SelectedIndexChanged += new System.EventHandler(this.Cb_Z_SelectedIndexChanged);
            // 
            // cb_Y
            // 
            this.cb_Y.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Y.FormattingEnabled = true;
            this.cb_Y.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.cb_Y.Location = new System.Drawing.Point(43, 31);
            this.cb_Y.Name = "cb_Y";
            this.cb_Y.Size = new System.Drawing.Size(31, 21);
            this.cb_Y.TabIndex = 14;
            this.cb_Y.SelectedIndexChanged += new System.EventHandler(this.Cb_Y_SelectedIndexChanged);
            // 
            // cb_X
            // 
            this.cb_X.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_X.FormattingEnabled = true;
            this.cb_X.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.cb_X.Location = new System.Drawing.Point(6, 31);
            this.cb_X.Name = "cb_X";
            this.cb_X.Size = new System.Drawing.Size(31, 21);
            this.cb_X.TabIndex = 13;
            this.cb_X.SelectedIndexChanged += new System.EventHandler(this.Cb_X_SelectedIndexChanged);
            // 
            // lb_Z
            // 
            this.lb_Z.AutoSize = true;
            this.lb_Z.Location = new System.Drawing.Point(90, 15);
            this.lb_Z.Name = "lb_Z";
            this.lb_Z.Size = new System.Drawing.Size(14, 13);
            this.lb_Z.TabIndex = 12;
            this.lb_Z.Text = "Z";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(55, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Att Höhe";
            // 
            // cb_3dImport
            // 
            this.cb_3dImport.AutoSize = true;
            this.cb_3dImport.Location = new System.Drawing.Point(84, 139);
            this.cb_3dImport.Name = "cb_3dImport";
            this.cb_3dImport.Size = new System.Drawing.Size(70, 17);
            this.cb_3dImport.TabIndex = 7;
            this.cb_3dImport.Text = "3d Import";
            this.cb_3dImport.UseVisualStyleBackColor = true;
            this.cb_3dImport.CheckedChanged += new System.EventHandler(this.Cb_3dImport_CheckedChanged);
            // 
            // cB_ExportFile
            // 
            this.cB_ExportFile.AutoSize = true;
            this.cB_ExportFile.Enabled = false;
            this.cB_ExportFile.Location = new System.Drawing.Point(84, 115);
            this.cB_ExportFile.Name = "cB_ExportFile";
            this.cB_ExportFile.Size = new System.Drawing.Size(182, 17);
            this.cB_ExportFile.TabIndex = 6;
            this.cB_ExportFile.Text = "Exportdatei für Import verwenden";
            this.cB_ExportFile.UseVisualStyleBackColor = true;
            this.cB_ExportFile.CheckedChanged += new System.EventHandler(this.ChkB_ExportFile_CheckedChanged);
            // 
            // lb_Prototypzeichnung
            // 
            this.lb_Prototypzeichnung.AutoSize = true;
            this.lb_Prototypzeichnung.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Prototypzeichnung.Location = new System.Drawing.Point(119, 13);
            this.lb_Prototypzeichnung.Name = "lb_Prototypzeichnung";
            this.lb_Prototypzeichnung.Size = new System.Drawing.Size(69, 13);
            this.lb_Prototypzeichnung.TabIndex = 5;
            this.lb_Prototypzeichnung.Text = "Prototyp.dwg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Prototypzeichnung:";
            // 
            // cbBlock
            // 
            this.cbBlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlock.FormattingEnabled = true;
            this.cbBlock.Location = new System.Drawing.Point(84, 50);
            this.cbBlock.Name = "cbBlock";
            this.cbBlock.Size = new System.Drawing.Size(121, 21);
            this.cbBlock.TabIndex = 3;
            this.cbBlock.SelectedIndexChanged += new System.EventHandler(this.CbBlock_SelectedIndexChanged);
            // 
            // labelBlock
            // 
            this.labelBlock.AutoSize = true;
            this.labelBlock.Location = new System.Drawing.Point(41, 52);
            this.labelBlock.Name = "labelBlock";
            this.labelBlock.Size = new System.Drawing.Size(37, 13);
            this.labelBlock.TabIndex = 2;
            this.labelBlock.Text = "Block:";
            // 
            // cbBasislayer
            // 
            this.cbBasislayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBasislayer.FormattingEnabled = true;
            this.cbBasislayer.Location = new System.Drawing.Point(84, 77);
            this.cbBasislayer.Name = "cbBasislayer";
            this.cbBasislayer.Size = new System.Drawing.Size(329, 21);
            this.cbBasislayer.TabIndex = 1;
            this.cbBasislayer.SelectedIndexChanged += new System.EventHandler(this.CbBasislayer_SelectedIndexChanged);
            // 
            // labelBasislayer
            // 
            this.labelBasislayer.AutoSize = true;
            this.labelBasislayer.Location = new System.Drawing.Point(21, 80);
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
            // panelExport
            // 
            this.panelExport.BackColor = System.Drawing.SystemColors.Control;
            this.panelExport.Controls.Add(this.panel2);
            this.panelExport.Controls.Add(this.tb_Header);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.numUD_Kommastellen);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tB_Separator);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cb_Decimal);
            this.panel2.Location = new System.Drawing.Point(44, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 119);
            this.panel2.TabIndex = 12;
            // 
            // numUD_Kommastellen
            // 
            this.numUD_Kommastellen.Location = new System.Drawing.Point(98, 84);
            this.numUD_Kommastellen.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUD_Kommastellen.Name = "numUD_Kommastellen";
            this.numUD_Kommastellen.Size = new System.Drawing.Size(46, 20);
            this.numUD_Kommastellen.TabIndex = 12;
            this.numUD_Kommastellen.ValueChanged += new System.EventHandler(this.NumUD_Kommastellen_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Kommastellen";
            // 
            // tB_Separator
            // 
            this.tB_Separator.BackColor = System.Drawing.SystemColors.Window;
            this.tB_Separator.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_Separator.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tB_Separator.Location = new System.Drawing.Point(98, 16);
            this.tB_Separator.Name = "tB_Separator";
            this.tB_Separator.Size = new System.Drawing.Size(31, 23);
            this.tB_Separator.TabIndex = 10;
            this.tB_Separator.TextChanged += new System.EventHandler(this.Tb_Separator_TextChanged);
            this.tB_Separator.Validating += new System.ComponentModel.CancelEventHandler(this.Tb_Separator_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Trennzeichen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dezimalzeichen";
            // 
            // cb_Decimal
            // 
            this.cb_Decimal.BackColor = System.Drawing.SystemColors.Window;
            this.cb_Decimal.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Decimal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cb_Decimal.FormattingEnabled = true;
            this.cb_Decimal.Items.AddRange(new object[] {
            ".",
            ","});
            this.cb_Decimal.Location = new System.Drawing.Point(98, 52);
            this.cb_Decimal.Name = "cb_Decimal";
            this.cb_Decimal.Size = new System.Drawing.Size(31, 23);
            this.cb_Decimal.TabIndex = 7;
            this.cb_Decimal.TextChanged += new System.EventHandler(this.Cb_Decimal_TextChanged);
            // 
            // tb_Header
            // 
            this.tb_Header.Location = new System.Drawing.Point(39, 34);
            this.tb_Header.Name = "tb_Header";
            this.tb_Header.Size = new System.Drawing.Size(317, 20);
            this.tb_Header.TabIndex = 10;
            this.tb_Header.TextChanged += new System.EventHandler(this.Tb_Header_TextChanged);
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
            this.cB_Header.CheckedChanged += new System.EventHandler(this.Cb_Header_CheckedChanged);
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
            this.cB_OutputFile.CheckedChanged += new System.EventHandler(this.Cb_OutputFile_CheckedChanged);
            // 
            // btExportFile
            // 
            this.btExportFile.Location = new System.Drawing.Point(362, 100);
            this.btExportFile.Name = "btExportFile";
            this.btExportFile.Size = new System.Drawing.Size(27, 23);
            this.btExportFile.TabIndex = 2;
            this.btExportFile.UseVisualStyleBackColor = true;
            this.btExportFile.Click += new System.EventHandler(this.BtExportFile_Click);
            // 
            // tb_PunktExport
            // 
            this.tb_PunktExport.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tb_PunktExport.Location = new System.Drawing.Point(39, 101);
            this.tb_PunktExport.Name = "tb_PunktExport";
            this.tb_PunktExport.ReadOnly = true;
            this.tb_PunktExport.Size = new System.Drawing.Size(317, 20);
            this.tb_PunktExport.TabIndex = 1;
            this.tb_PunktExport.TextChanged += new System.EventHandler(this.Tb_PunktExport_TextChanged);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(400, 22);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(47, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.BtOK_Click);
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
            this.panelImport.ResumeLayout(false);
            this.panelImport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuUD_Überhöhung)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelGeneral.ResumeLayout(false);
            this.panelGeneral.PerformLayout();
            this.panelExport.ResumeLayout(false);
            this.panelExport.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_Kommastellen)).EndInit();
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
        private System.Windows.Forms.TextBox tB_Separator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_Decimal;
        private System.Windows.Forms.CheckBox cB_Header;
        private System.Windows.Forms.TextBox tb_Header;
        private System.Windows.Forms.Label lb_Prototypzeichnung;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cB_ExportFile;
        private System.Windows.Forms.CheckBox cb_3dImport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown numUD_Kommastellen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cb_AttH;
        private System.Windows.Forms.ComboBox cb_Z;
        private System.Windows.Forms.ComboBox cb_Y;
        private System.Windows.Forms.ComboBox cb_X;
        private System.Windows.Forms.Label lb_Z;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nuUD_Überhöhung;
        private System.Windows.Forms.Label label9;
    }
}
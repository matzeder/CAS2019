﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using _AcAp = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

using CAS.myUtilities;

namespace CAS.myFunctions
{
    public partial class DiaSettings : Form
    {
        CAS.myUtilities.MyConfig _config = new myUtilities.MyConfig();
        string _Basislayer = MyConfig.Instance.GetAppSettingString("Basislayer");
        string value;

        public DiaSettings()
        {
            InitializeComponent();

            //Init Configuration
            //Basislayer
            value = _config.GetAppSettingString("Basislayer");
            if (value == String.Empty)
                _config.SetAppSetting("Basislayer", "MP-P");
            
            //Header
            value = _config.GetAppSettingString("useHeader");
            if (value == String.Empty)
                _config.SetAppSetting("useHeader", "False");
            cB_Header.Checked = Convert.ToBoolean(value);

            value = _config.GetAppSettingString("Header");
            if (value == String.Empty)
                _config.SetAppSetting("Header", "PNr, X,Y,Z");
            tb_Header.Text = value;

            //3dImport
            value = _config.GetAppSettingString("3dImport");
            if (value == String.Empty)
                _config.SetAppSetting("3dImport", "False");
            cb_3dImport.Checked = Convert.ToBoolean(value);
            lb_Z.Visible = cb_3dImport.Checked;
            cb_Z.Visible = cb_3dImport.Checked;

            //import Coords
            value = _config.GetAppSettingString("colX");
            if (value == String.Empty)
            {
                value = "X";
                _config.SetAppSetting("colX", value);
            }
            cb_X.SelectedIndex = cb_X.Items.IndexOf(value);

            value = _config.GetAppSettingString("colY");
            if (value == String.Empty)
            {
                value = "Y";
                _config.SetAppSetting("colY", value);
            }
            cb_Y.SelectedIndex = cb_Y.Items.IndexOf(value);

            value = _config.GetAppSettingString("colZ");
            if (value == String.Empty)
            {
                value = "Z";
                _config.SetAppSetting("colZ", value);
            }
            cb_Z.SelectedIndex = cb_Z.Items.IndexOf(value);

            value = _config.GetAppSettingString("colAttH");
            if (value == String.Empty)
            {
                value = "Z";
                _config.SetAppSetting("colAttH", value);
            }
            cb_AttH.SelectedIndex = cb_AttH.Items.IndexOf(value);

            value = _config.GetAppSettingString("Yscale");
            if (value == String.Empty)
            {
                value = "1.00";
                _config.SetAppSetting("Yscale", value);
            }
            nuUD_Überhöhung.Value = Convert.ToDecimal(value);


            //Output File
            value = _config.GetAppSettingString("OutputFile");
            if (File.Exists(value))
            {
                tb_PunktExport.Text = value;
                cB_ExportFile.Enabled = File.Exists(value);

                if(!_config.GetAppSettingBool("useOutputFile"))
                    _config.SetAppSetting("useOutputFile", "False");
                else
                    cB_OutputFile.Checked = true;
            }
            else
                tb_PunktExport.Text = " ";

            //Separator & Decimal
            value = _config.GetAppSettingString("Separator");
            if (value == String.Empty)
                _config.SetAppSetting("Separator", ";");
            tB_Separator.Text = value;

            value = _config.GetAppSettingString("Decimal");
            if (value == String.Empty)
                _config.SetAppSetting("Decimal", ".");
            cb_Decimal.SelectedItem = value;
            cb_Decimal.Refresh();

            value = _config.GetAppSettingString("decimals");
            if (value == String.Empty)
            {
                value = "3";
                _config.SetAppSetting("decimals", value);
            }
            numUD_Kommastellen.Value = Convert.ToInt32(value);

            //import Exportfile
            value = _config.GetAppSettingString("importExportfile");
            if (value == String.Empty)
               cB_ExportFile.Checked = false;
            else
                cB_ExportFile.Checked = Convert.ToBoolean(value);

            //Treenode
            TreeNode root = treeView1.Nodes.Add("CAS 2019");
            //root.Nodes.Add("general");
            root.Nodes.Add("Import");
            root.Nodes.Add("Export");
            root.ExpandAll();
            treeView1.SelectedNode = root.FirstNode;

            //Basislayer
            myCAD.MyLayer objLayer = myCAD.MyLayer.Instance;
            objLayer.Refresh();

            foreach(LayerTableRecord ltr in objLayer.LsLayerTableRecord)
            {
                string layName = ltr.Name;
                if (layName.Length > 2)
                {
                    if (layName.Substring(layName.Length - 2, 2) == "-P")
                        cbBasislayer.Items.Add(layName);
                }
            }

            cbBasislayer.SelectedIndex = cbBasislayer.FindStringExact(_config.GetAppSettingString("Basislayer"));

            if (cbBasislayer.SelectedIndex == -1)
            {
                if (cbBasislayer.Items.Count > 0)
                {
                    DialogResult res = MessageBox.Show(_config.GetAppSettingString("Basislayer") 
                                                       + " nicht gefunden! Soll dieser Layer erstellt werden?", "", 
                                                       MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        objLayer.Add(_config.GetAppSettingString("Basislayer"));
                }
                else
                {
                    string Basislayer = _config.GetAppSettingString("Basislayer");
                    CheckBasislayer();
                    
                    cbBasislayer.Items.Add(Basislayer);
                    cbBasislayer.SelectedItem = cbBasislayer.Items[0];
                }
            }

            //Blöcke aus Protoypzeichnung lesen
            _AcAp.Document myDWG;
            _AcAp.DocumentLock myDWGlock;
            Database db = HostApplicationServices.WorkingDatabase;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager myTm = null;
            myTm = db.TransactionManager;
            Transaction myT = db.TransactionManager.StartTransaction();

            string ProtoDWG = CAS.myUtilities.Global.Instance.PrototypFullPath;
            lb_Prototypzeichnung.Text = ProtoDWG;

            if (File.Exists(ProtoDWG))
            {
                try
                {
                    myDWG = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                    myDWGlock = myDWG.LockDocument();

                    using (Database srcDb = new Database(false, false))
                    {
                        srcDb.ReadDwgFile(ProtoDWG, FileShare.Read, true, "");
                        ObjectIdCollection blockIds = new ObjectIdCollection();

                        Autodesk.AutoCAD.DatabaseServices.TransactionManager srcT = srcDb.TransactionManager;
                        try
                        {
                            using (Transaction protoT = srcT.StartTransaction())
                            {
                                BlockTable bt = (BlockTable)protoT.GetObject(srcDb.BlockTableId, OpenMode.ForRead, false);

                                foreach (ObjectId btrid in bt)
                                {
                                    BlockTableRecord btr = (BlockTableRecord)protoT.GetObject(btrid, OpenMode.ForRead, false);
                                    if (!btr.IsAnonymous && !btr.IsLayout)
                                    {
                                        blockIds.Add(btrid);
                                        cbBlock.Items.Add(btr.Name);
                                    }
                                    btr.Dispose();
                                }
                            }
                        }
                        catch { }

                        finally
                        {
                            myT.Commit();
                            myT.Dispose();
                        }

                        //Blöcke in aktuelle Zeichnung einfügen
                        IdMapping mapping = new IdMapping();
                        srcDb.WblockCloneObjects(blockIds, db.BlockTableId, mapping, DuplicateRecordCloning.Replace, false);

                        srcDb.Dispose();
                        myDWGlock.Dispose();
                    }
                }
                catch { }
            }
            else
                MessageBox.Show("Achtung!!! Prototypzeichnung nicht gefunden!");

             if (cbBlock.Items.Count > 0)
            {
                try
                {
                    cbBlock.SelectedIndex = cbBlock.FindStringExact(_config.GetAppSettingString("Block"));   //Block aus settings.xml suchen
                }
                catch
                {
                    cbBlock.SelectedItem = cbBlock.Items[0];  //sonst ersten Block wählen
                }
            }

            else
                MessageBox.Show("Keine Blöcke gefunden!");

            //Header
            cB_Header.Checked = _config.GetAppSettingBool("useHeader");
            tb_Header.Text = _config.GetAppSettingString("Header");

            //Output File
            cB_OutputFile.Checked = _config.GetAppSettingBool("useOutputFile");
            tb_PunktExport.Text = _config.GetAppSettingString("OutputFile");
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch(treeView1.SelectedNode.Text)
            {
                case "general":
                    panelGeneral.BringToFront();

                    break;

                case "Import":
                    panelImport.BringToFront();

                    break;

                case "Export":
                    panelExport.BringToFront();

                    break;
            }
        }

        //Layer für MP generieren
        private void CheckBasislayer()
        {
            int cnt = 0;
            myCAD.MyLayer objLayer = myCAD.MyLayer.Instance;

            string Basislayer = _config.GetAppSettingString("Basislayer");
            Basislayer = Basislayer.Substring(0, Basislayer.Length - 2);
            cnt += objLayer.Add(Basislayer + Global.Instance.LayNummer);
            cnt += objLayer.Add(Basislayer + Global.Instance.LayHöhe);
            cnt +=  objLayer.Add(Basislayer + Global.Instance.LayDatum);
            cnt += objLayer.Add(Basislayer + Global.Instance.LayCode);

            if (cnt > 0)
                MessageBox.Show("Layer für " + Basislayer + " wurden erstellt!");
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void CbBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cbBlock.SelectedItem.ToString();
            if (item != null && item != _config.GetAppSettingString("Block"))
                _config.SetAppSetting("Block", item);
           
        }

        private void CbBasislayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cbBasislayer.SelectedItem.ToString();
            if (item != null && item != _config.GetAppSettingString("Basislayer"))
            {
                _config.SetAppSetting("Basislayer", item);
                CheckBasislayer();
            }
        }

        private void BtExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog diaOpenFile = new OpenFileDialog()
            { Filter = "Punktdatei|*.csv" };

            if (diaOpenFile.ShowDialog() == DialogResult.OK)
                tb_PunktExport.Text = diaOpenFile.FileName;
        }

        private void Tb_PunktExport_TextChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("OutputFile", tb_PunktExport.Text);
        }

        private void Cb_OutputFile_CheckedChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("useOutputFile", cB_OutputFile.Checked.ToString());
        }

        private void Cb_Header_CheckedChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("useHeader", cB_Header.Checked.ToString());
        }

        private void Tb_Header_TextChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("Header", tb_Header.Text);
        }

        private void Tb_Separator_TextChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("Separator", tB_Separator.Text);
        }

        private void Cb_Decimal_TextChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("Decimal", cb_Decimal.Text);
        }

        //max Länge 1
        private void Tb_Separator_Validating(object sender, CancelEventArgs e)
        {
            string val = tB_Separator.Text;

            if (val.Length != 1)
            {
                e.Cancel = true;
            }
            else
                e.Cancel = false;
        }

        private void ChkB_ExportFile_CheckedChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("importExportfile", cB_ExportFile.Checked.ToString());
        }

        private void Cb_3dImport_CheckedChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("3dImport", cb_3dImport.Checked.ToString());

            //set coord z visible
            lb_Z.Visible = cb_3dImport.Checked;
            cb_Z.Visible = cb_3dImport.Checked;
        }

        private void NumUD_Kommastellen_ValueChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("decimals", numUD_Kommastellen.Value.ToString());
        }

        private void Cb_X_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("colX", cb_X.SelectedItem.ToString());
        }

        private void Cb_Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("colY", cb_Y.SelectedItem.ToString());
        }

        private void Cb_Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("colZ", cb_Z.SelectedItem.ToString());
        }

        private void Cb_AttH_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("colAttH", cb_AttH.SelectedItem.ToString());
        }

        private void nuUD_Überhöhung_ValueChanged(object sender, EventArgs e)
        {
            _config.SetAppSetting("Yscale", nuUD_Überhöhung.Value.ToString());
        }
    }
}

using System;
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

using Autodesk.AutoCAD.DatabaseServices;

namespace CAS.myFunctions
{
    public partial class DiaSettings : Form
    {
        CAS.myUtilities.myConfig _config = new myUtilities.myConfig();
        string value;

        public DiaSettings()
        {
            InitializeComponent();

            //Init Configuration
            //Basislayer
            value = _config.getAppSetting("Basislayer");
            if (value == String.Empty)
                _config.setAppSetting("Basislayer", "MP-P");
            
            //Header
            value = _config.getAppSetting("useHeader");
            if (value == String.Empty)
                _config.setAppSetting("useHeader", "False");
            cB_Header.Checked = Convert.ToBoolean(value);

            value = _config.getAppSetting("Header");
            if (value == String.Empty)
                _config.setAppSetting("Header", "PNr, X,Y,Z");
            tb_Header.Text = value;

            //Output File
            value = _config.getAppSetting("useOutputFile");
            if (value == String.Empty)
                _config.setAppSetting("useOutputFile", "False");
            cB_OutputFile.Checked = Convert.ToBoolean(value);

            value = _config.getAppSetting("OutputFile");
            tb_PunktExport.Text = value;

            //Separator & Decimal
            value = _config.getAppSetting("Separator");
            if (value == String.Empty)
                _config.setAppSetting("Separator", ";");
            tB_Separator.Text = value;

            value = _config.getAppSetting("Decimal");
            if (value == String.Empty)
                _config.setAppSetting("Decimal", ".");
            cb_Decimal.SelectedItem = value;
            cb_Decimal.Refresh();

            //Treenode
            TreeNode root = treeView1.Nodes.Add("CAS 2019");
            //root.Nodes.Add("general");
            root.Nodes.Add("Import");
            root.Nodes.Add("Export");
            root.ExpandAll();
            treeView1.SelectedNode = root.FirstNode;

            //Basislayer
            myAutoCAD.MyLayer objLayer = myAutoCAD.MyLayer.Instance;
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

            cbBasislayer.SelectedIndex = cbBasislayer.FindStringExact(_config.getAppSetting("Basislayer"));

            if (cbBasislayer.SelectedIndex == -1)
            {
                if (cbBasislayer.Items.Count > 0)
                {
                    DialogResult res = MessageBox.Show(_config.getAppSetting("Basislayer") + " nicht gefunden! Soll dieser Layer erstellt werden?", "", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        objLayer.Add(_config.getAppSetting("Basislayer"));
                }
                else
                {
                    objLayer.Add(_config.getAppSetting("Basislayer"));
                    MessageBox.Show(_config.getAppSetting("Basislayer") + " wurde erstellt!");
                    cbBasislayer.Items.Add(_config.getAppSetting("Basislayer"));
                    cbBasislayer.SelectedItem = cbBasislayer.Items[0];
                }
            }

            //Blöcke
            Assembly assem = typeof(CAS2019).Assembly;
            string blockPath = Path.Combine(Path.GetDirectoryName(assem.Location), "blocks");
            System.IO.DirectoryInfo ParentDirectory = new DirectoryInfo(blockPath);

            foreach(System.IO.FileInfo fi in ParentDirectory.GetFiles())
            {
                if (Path.GetExtension(fi.Name) == ".dwg")
                {
                    string _block = Path.GetFileNameWithoutExtension(fi.Name);
                    cbBlock.Items.Add(Path.GetFileName(_block));
                }
            }

            if (cbBlock.Items.Count > 0)
            {
                try
                {
                    cbBlock.SelectedIndex = cbBlock.FindStringExact(_config.getAppSetting("Block"));
                }
                catch
                {
                    cbBlock.SelectedItem = cbBlock.Items[0];
                }
            }

            else
                MessageBox.Show("Keine Blöcke gefunden!");

            //Header
            cB_Header.Checked = Convert.ToBoolean(_config.getAppSetting("useHeader"));
            tb_Header.Text = _config.getAppSetting("Header");

            //Output File
            cB_OutputFile.Checked = Convert.ToBoolean(_config.getAppSetting("useOutputFile"));
            tb_PunktExport.Text = _config.getAppSetting("OutputFile");
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
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

        private void btOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void cbBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cbBlock.SelectedItem.ToString();
            if (item != null && item != _config.getAppSetting("Block"))
                _config.setAppSetting("Block", item);
           
        }

        private void cbBasislayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cbBasislayer.SelectedItem.ToString();
            if (item != null && item != _config.getAppSetting("Basislayer"))
                _config.setAppSetting("Basislayer", item);
        }

        private void btExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog diaOpenFile = new OpenFileDialog();
            diaOpenFile.Filter = "Punktdatei|*.csv";

            if (diaOpenFile.ShowDialog() == DialogResult.OK)
                tb_PunktExport.Text = diaOpenFile.FileName;
        }

        private void tb_PunktExport_TextChanged(object sender, EventArgs e)
        {
            _config.setAppSetting("OutputFile", tb_PunktExport.Text);
        }

        private void cB_OutputFile_CheckedChanged(object sender, EventArgs e)
        {
            _config.setAppSetting("useOutputFile", cB_OutputFile.Checked.ToString());
        }

        private void cB_Header_CheckedChanged(object sender, EventArgs e)
        {
            _config.setAppSetting("useHeader", cB_Header.Checked.ToString());
        }

        private void tb_Header_TextChanged(object sender, EventArgs e)
        {
            _config.setAppSetting("Header", tb_Header.Text);
        }

        private void tB_Separator_TextChanged(object sender, EventArgs e)
        {
            _config.setAppSetting("Separator", tB_Separator.Text);
        }

        private void cb_Decimal_TextChanged(object sender, EventArgs e)
        {
            _config.setAppSetting("Decimal", cb_Decimal.Text);
        }

        private void tB_Separator_Validating(object sender, CancelEventArgs e)
        {
            string val = tB_Separator.Text;

            if (val.Length != 1)
            {
                e.Cancel = true;
            }
            else
                e.Cancel = false;
        }
    }
}

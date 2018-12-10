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
        CAS.myUtilities.MySettings objSettings = new myUtilities.MySettings();

        public DiaSettings()
        {
            InitializeComponent();

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

            cbBasislayer.SelectedIndex = cbBasislayer.FindStringExact(objSettings.Basislayer);

            if (cbBasislayer.SelectedIndex == -1)
            {
                if (cbBasislayer.Items.Count > 0)
                {
                    DialogResult res = MessageBox.Show(objSettings.Basislayer + " nicht gefunden! Soll dieser Layer erstellt werden?", "", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        objLayer.Add(objSettings.Basislayer);
                }
                else
                {
                    objLayer.Add(objSettings.Basislayer);
                    MessageBox.Show(objSettings.Basislayer + " wurde erstellt!");
                    cbBasislayer.Items.Add(objSettings.Basislayer);
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
                    cbBlock.Items.Add(Path.GetFileName(fi.Name));
            }

            if (cbBlock.Items.Count > 0)
            {
                try
                {
                    cbBlock.SelectedIndex = cbBlock.FindStringExact(objSettings.Block);
                }
                catch
                {
                    cbBlock.SelectedItem = cbBlock.Items[0];
                }
            }

            else
                MessageBox.Show("Keine Blöcke gefunden!");

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
            if (item != null && item != objSettings.Block)
                objSettings.Block = item;
           
        }

        private void cbBasislayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = cbBasislayer.SelectedItem.ToString();
            if (item != null && item != objSettings.Basislayer)
                objSettings.Basislayer = item;
        }

        private void btExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog diaOpenFile = new OpenFileDialog();
            diaOpenFile.Filter = "Punktdatei|*.csv";

            if (diaOpenFile.ShowDialog() == DialogResult.OK)
                tb_PunktExport.Text = diaOpenFile.FileName;
        }
    }
}

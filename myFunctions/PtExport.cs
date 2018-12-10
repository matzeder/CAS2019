using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

using CAS.myAutoCAD;
using CAS.myUtilities;

namespace CAS.myFunctions
{
    class PtExport
    {
        private string m_Ausgabe = String.Empty;
        private int m_Zähler;
        private Editor m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        private MySettings _settings = new MySettings();
        
        myAutoCAD.myUtilities m_objUtil = new myAutoCAD.myUtilities();

        public void run()
        {
            SaveFileDialog ddSaveFile = new SaveFileDialog();
            //ddSaveFile.InitialDirectory = 
            ddSaveFile.DefaultExt = ".csv";
            ddSaveFile.Filter = "Punktwolke|*.csv";

            DialogResult diagRes = DialogResult.None;

        //    if (!m_Settings.isExportFile)
              diagRes = ddSaveFile.ShowDialog();
        //    else
        //    {
        //        ddSaveFile.FileName = m_Settings.ExportFile;
        //        diagRes = DialogResult.OK;
        //    }

            if (diagRes == DialogResult.OK)
            {
                myAutoCAD.Blöcke.Instance.init();
                myAutoCAD.Blöcke.Instance.selectWindow();

        //        if (Blöcke.Instance.count > 0)
        //        {
        //            try
        //            {
        //                StreamWriter sw = new StreamWriter(ddSaveFile.FileName, false, Encoding.Default);
        //                sw.NewLine = "\n";

        //                //Header
        //                if (m_Settings.isHeader)
        //                    sw.WriteLine(m_Settings.Header);

        //                Messpunkt[] vMP = Blöcke.Instance.getMP;
        //                foreach (Messpunkt MP in vMP)
        //                {
        //                    //Punktnummer
        //                    string Zeile = MP.PNum + ";";
        //                    Point3d pt3d = new Point3d();

        //                    if (m_Settings.UCScoords)
        //                    {
        //                        //aktuelles BKS setzen
        //                        Matrix3d ucs = m_ed.CurrentUserCoordinateSystem;
        //                        pt3d = MP.Position.TransformBy(ucs.Inverse());
        //                    }
        //                    else
        //                        pt3d = MP.Position;

        //                    if (m_Settings.UCScoords)
        //                    {
        //                        switch (m_Settings.AusgabeFormat)
        //                        {
        //                            //x,y,z
        //                            case 0:
        //                                Zeile += pt3d.X.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                Zeile += pt3d.Y.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                Zeile += pt3d.Z.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                break;

        //                            //x,z,y
        //                            case 1:
        //                                Zeile += pt3d.X.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                Zeile += pt3d.Z.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                Zeile += pt3d.Y.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                break;

        //                            //x,z,z
        //                            case 2:
        //                                Zeile += pt3d.X.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                Zeile += pt3d.Z.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                Zeile += pt3d.Z.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        switch (m_Settings.AusgabeFormat)
        //                        {
        //                            //x,y,z
        //                            case 0:
        //                                //Rechtswert
        //                                Zeile += MP.Position.X.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";

        //                                //Hochwert
        //                                Zeile += MP.Position.Y.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";

        //                                //Höhe
        //                                if (MP.CASHöhe.HasValue && MP.HeigthPrecision != null)
        //                                    Zeile += MP.CASHöhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
        //                                else
        //                                    if (MP.Höhe.HasValue)
        //                                    Zeile += MP.Höhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
        //                                else
        //                                    Zeile += ";";
        //                                break;

        //                            //x,z,y
        //                            case 1:
        //                                //Rechtswert
        //                                Zeile += MP.Position.X.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";

        //                                //Höhe
        //                                if (MP.CASHöhe.HasValue && MP.HeigthPrecision != null)
        //                                    Zeile += MP.CASHöhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
        //                                else
        //                                    if (MP.Höhe.HasValue)
        //                                    Zeile += MP.Höhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
        //                                else
        //                                    Zeile += ";";

        //                                //Hochwert
        //                                Zeile += MP.Position.Y.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                break;

        //                            //y,z,x
        //                            case 2:
        //                                //Hochwert
        //                                Zeile += MP.Position.Y.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";

        //                                //Höhe
        //                                if (MP.CASHöhe.HasValue && MP.HeigthPrecision != null)
        //                                    Zeile += MP.CASHöhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
        //                                else
        //                                    if (MP.Höhe.HasValue)
        //                                    Zeile += MP.Höhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
        //                                else
        //                                    Zeile += ";";

        //                                //Rechtswert
        //                                Zeile += MP.Position.X.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + ";";
        //                                break;
        //                        }
        //                    }

        //                    //Blockname
        //                    //Zeile += MP.BlockReferenz.Name + ";";

        //                    //Att3
        //                    if (MP.Att3_Wert != null)
        //                        Zeile += MP.Att3_Wert + ";";

        //                    //Att4
        //                    if (MP.Att4_Wert != null)
        //                        Zeile += MP.Att4_Wert.Replace("\r", "") + ";";

        //                    //Att5
        //                    Zeile += MP.Att5_Wert + ";";

        //                    //Att6
        //                    Zeile += MP.Att6_Wert + ";";

        //                    //Att7
        //                    Zeile += MP.Att7_Wert + ";";

        //                    //Att8
        //                    Zeile += MP.Att8_Wert + ";";

        //                    //Att9
        //                    Zeile += MP.Att9_Wert + ";";

        //                    //Att10
        //                    Zeile += MP.Att10_Wert + ";";

        //                    m_Zähler += 1;
        //                    sw.WriteLine(Zeile);
        //                }

        //                MessageBox.Show(m_Zähler + " Punkte exportiert!");
        //                sw.Close();
        //            }
        //            catch
        //            {
        //                MessageBox.Show("Ausgabedateiname ungültig!");
        //            }
        //        }
            }        
        }
    }
}

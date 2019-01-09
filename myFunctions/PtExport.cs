using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
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
        private myConfig _config = new myConfig();
        
        myAutoCAD.myUtilities m_objUtil = new myAutoCAD.myUtilities();

        public void run()
        {
            int _digits = 2;
            NumberFormatInfo _numberFormatInfo = new NumberFormatInfo();
            _numberFormatInfo.NumberDecimalSeparator = _config.getAppSetting("Decimal");
            _numberFormatInfo.NumberDecimalDigits = _digits;

            SaveFileDialog ddSaveFile = new SaveFileDialog();
            //ddSaveFile.InitialDirectory = 
            ddSaveFile.DefaultExt = ".csv";
            ddSaveFile.Filter = "Punktwolke|*.csv";

            DialogResult diagRes = DialogResult.None;

            if (!Convert.ToBoolean(_config.getAppSetting("useOutputfile")))
                diagRes = ddSaveFile.ShowDialog();
            else
            {
                ddSaveFile.FileName = _config.getAppSetting("OutputFile");
                diagRes = DialogResult.OK;
            }

            if (diagRes == DialogResult.OK)
            {
                myAutoCAD.Blöcke.Instance.init();
                myAutoCAD.Blöcke.Instance.selectWindow();

                if (Blöcke.Instance.count > 0)
                {
                    string _Separator = _config.getAppSetting("Separator");
                    char _Decimal = Convert.ToChar(_config.getAppSetting("Decimal"));
                    try
                    {
                        StreamWriter sw = new StreamWriter(ddSaveFile.FileName, false, Encoding.Default);
                        sw.NewLine = "\n";

                        //Header
                        if (Convert.ToBoolean( _config.getAppSetting("useHeader")))
                            sw.WriteLine(_config.getAppSetting("Header"));

                        foreach (Messpunkt MP in Blöcke.Instance.lsMP)
                        {
                            //Punktnummer
                            string Zeile = MP.getAttribute(0).Value + _Separator;
                            Point3d pt3d = new Point3d();

        //                    if (m_Settings.UCScoords)
        //                    {
        //                        //aktuelles BKS setzen
        //                        Matrix3d ucs = m_ed.CurrentUserCoordinateSystem;
        //                        pt3d = MP.Position.TransformBy(ucs.Inverse());
        //                    }
        //                    else
                                pt3d = MP.Pos;

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
                            //x,y,z
                            //                            case 0:
                            //Rechtswert
                            //Zeile += MP.Pos.X.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + _Separator;
                            Zeile += MP.Pos.X.ToString(_numberFormatInfo) + _Separator;

                            //Hochwert
                            //Zeile += MP.Pos.Y.ToString(m_objUtil.Formatstring(4)).Replace('.', ',') + _Separator;
                            Zeile += MP.Pos.Y.ToString(_numberFormatInfo) + _Separator;

                            //Höhe
                            if (MP.CASHöhe.HasValue && MP.HeigthPrecision != null)
                            //  Zeile += MP.CASHöhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
                            Zeile += MP.CASHöhe.Value.ToString(_numberFormatInfo) + _Separator;
                                        //else
                                        //    if (MP.Höhe.HasValue)
                                        //    Zeile += MP.Höhe.Value.ToString(m_objUtil.Formatstring(MP.HeigthPrecision.Value)).Replace('.', ',') + ";";
                                        //else
                                        //    Zeile += ";";
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

                            //Attribute
                            for(int i = 1; i < MP.AttCount;i++)
                            {
                                Zeile += MP.Attribute[i].Value + _Separator; 
                            }

                            m_Zähler += 1;
                            sw.WriteLine(Zeile);
                        }

                        MessageBox.Show(m_Zähler + " Punkte exportiert!");
                        sw.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Ausgabedateiname ungültig!");
                    }
                }
            }        
        }
    }
}

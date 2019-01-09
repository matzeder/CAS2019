using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using CAS.myAutoCAD;
using CAS.myUtilities;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

namespace CAS.myFunctions
{
    class PtImport
    {
        private List<Messpunkt> _lsMP = new List<Messpunkt>();
        private myConfig _config = new myConfig();
        private string _Filename;
        private string _Text;

        //Methoden
        public void run()
        {
            OpenFileDialog ddOpenFile = new OpenFileDialog();
            ddOpenFile.Title = "Vermessungspunkte importieren";
            ddOpenFile.Filter = "Punktdatei|*.csv";
            Editor m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            DialogResult diagRes = DialogResult.None;

            diagRes = ddOpenFile.ShowDialog();

            if(diagRes == DialogResult.OK)
            {
                _Filename = ddOpenFile.FileName;
                bool fileOK = true;
                BlockTableRecord _btRec = null;

                Prototyp.Instance.Blockname = _config.getAppSetting("Block");
                _btRec = Prototyp.Instance.btRec;
                if (_btRec == null)
                    fileOK = false;

                //Punktdatei einlesen
                try
                {
                    StreamReader sr = new StreamReader(_Filename, Encoding.Default);
                    _Text = sr.ReadToEnd();
                    sr.Close();
                }
                catch { fileOK = false; }

                if (fileOK)
                {
                    bool bHeader = false;
                    bool bError = false;
                    string[] arZeile = _Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    List<Messpunkt> _lsMP = new List<Messpunkt>();

                    for (int i = 0; i < arZeile.Length; i++)
                    {
                        try
                        {
                            string Zeile = arZeile[i];
                            string[] arElement = Zeile.Split(new char[] { ';' }, StringSplitOptions.None);

                            string PNum = arElement[0];
                            double X = Convert.ToDouble(arElement[1]);
                            double Y = Convert.ToDouble(arElement[2]);
                            double Z = Convert.ToDouble(arElement[3]);

                            Point3d Pos = new Point3d(X, Y, Z);
                            _lsMP.Add(new Messpunkt(PNum, Pos));
                        }
                        catch
                        {
                            if (i == 0)
                                bHeader = true;
                            else
                                bError = true;
                        }
                    }

                        //Punkte in Autocad einfügen
                        if (!bError)
                        {
                            foreach (Messpunkt _MP in _lsMP)
                            {
                                _MP.draw(_btRec, _config.getAppSetting("Basislayer"));
                            }
                        }
                        else
                            MessageBox.Show("Fehler in Eingabefile!");
                }
            }
        }
    }
}

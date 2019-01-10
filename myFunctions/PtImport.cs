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
        private string _block, _basislayer;

        //Methoden
        public void run()
        {
            _block = _config.getAppSetting("Block");
            _basislayer = _config.getAppSetting("Basislayer");

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
                    bool bHeader = true;
                    bool bError = false;
                    int firstRow = 0;       //erste einzulesende Zeile

                    string[] arZeile = _Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    List<Messpunkt> _lsMP = new List<Messpunkt>();

                    //Header?
                    if (bHeader)
                        firstRow = 1;

                    for (int i = firstRow; i < arZeile.Length; i++)
                    {
                        try
                        {
                            string Zeile = arZeile[i];
                            string[] arElement = Zeile.Split(new char[] { ';' }, StringSplitOptions.None);

                            string PNum = arElement[0];
                            double X = Convert.ToDouble(arElement[1].Replace(',','.'));
                            double Y = Convert.ToDouble(arElement[2].Replace(',', '.'));

                            //Höhe
                            string z = arElement[3];
                            double? Z = null;
                            if (z != String.Empty)
                                Z = Convert.ToDouble(z.Replace(',', '.'));

                            if(Z.HasValue)
                            {
                                Point3d pos = new Point3d(X, Y, Z.Value);
                                _lsMP.Add(new Messpunkt(PNum, pos, Z.Value));
                            }
                            else
                            {
                                Point2d pos = new Point2d(X, Y);
                                _lsMP.Add(new Messpunkt(PNum, pos));
                            }
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
                                _MP.draw(_block, _basislayer);
                            }
                        }
                        else
                            MessageBox.Show("Fehler in Eingabefile!");
                }
            }
        }
    }
}

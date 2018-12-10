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

namespace CAS.myFunctions
{
    class PtImport
    {
        private List<Messpunkt> _lsMP = new List<Messpunkt>();
        private MySettings _settings = new MySettings();
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

                try
                {
                    StreamReader sr = new StreamReader(_Filename, Encoding.Default);
                    _Text = sr.ReadToEnd();
                    sr.Close();
                }
                catch { fileOK = false; }

                if (fileOK)
                {
                    string[] arZeile = _Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    foreach(string Zeile in arZeile)
                    {
                        string[] arElement = Zeile.Split(new char[] { ';' }, StringSplitOptions.None);

                        string PNum = arElement[0];
                        double X = Convert.ToDouble(arElement[1]);
                        double Y = Convert.ToDouble(arElement[2]);
                        double Z = Convert.ToDouble(arElement[3]);

                        Point3d Pos = new Point3d(X, Y, Z);
                    }
                }
            }
        }
    }
}

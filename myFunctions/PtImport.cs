using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using CAS.myAutoCAD;
using CAS.myUtilities;
using ShowProgressBar;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using CadApp = Autodesk.AutoCAD.ApplicationServices.Application;

//[assembly: CommandClass(typeof(ShowProgressBar.MyCadCommands))]

namespace CAS.myFunctions
{
    public class PtImport : ILongProcessingObject
    {
        private myConfig _config = new myConfig();
        private string _Filename;
        private string _Text;
        private string _block, _basislayer;
        private List<int> _lsErrorLine = new List<int>();

        //Methoden
        public void start()
        {
            _block = _config.getAppSetting("Block");
            _basislayer = _config.getAppSetting("Basislayer");

            OpenFileDialog ddOpenFile = new OpenFileDialog();
            ddOpenFile.Title = "Vermessungspunkte importieren";
            ddOpenFile.Filter = "Punktdatei|*.csv";
            Editor m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            DialogResult diagRes = DialogResult.None;
            bool importExportfile = Convert.ToBoolean(_config.getAppSetting("importExportfile"));
            if (!importExportfile)
                diagRes = ddOpenFile.ShowDialog();
            else
            {
                ddOpenFile.FileName = _config.getAppSetting("Outputfile");
                diagRes = DialogResult.OK;
            }

            if (diagRes == DialogResult.OK)
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
                    //Punkte in Autocad einfügen
                    using (var progress = new ProcessingProgressBar(this))
                    {
                        progress.Start();
                    }
                }
            }
        }

        //Messpunkte einfügen
        private void insertMP()
        {
            bool bHeader = false;
            bool bErrorIO = false;
            int firstRow = 0;       //erste einzulesende Zeile

            string[] arZeile = _Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<Messpunkt> lsMP = new List<Messpunkt>();

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
                    double X = Convert.ToDouble(arElement[1].Replace(',', '.'));
                    double Y = Convert.ToDouble(arElement[2].Replace(',', '.'));

                    //Höhe
                    double? Z = null;
                    int? HPrec = null;
                    bool import3d = Convert.ToBoolean(_config.getAppSetting("3dImport"));

                    try
                    {
                        string z = arElement[3].Replace(',', '.');

                        if (z != String.Empty)
                        {
                            Z = Convert.ToDouble(z);

                            //Höhe Nachkommastellen
                            string[] arZ = z.Split('.');
                            if (arZ.Length == 1)
                                HPrec = 0;
                            else
                                HPrec = arZ[arZ.Length - 1].Length;
                        }
                    }
                    catch { }

                    Messpunkt MP = null;
                    if (import3d)
                    {
                        if (Z.HasValue)
                        {
                            Point3d pos = new Point3d(X, Y, Z.Value);
                            MP = new Messpunkt(PNum, pos, Z.Value);
                        }
                        else
                        {
                            Point2d pos = new Point2d(X, Y);
                            MP = new Messpunkt(PNum, pos);
                        }                        
                    }
                    else
                    {
                        Point2d pos = new Point2d(X, Y);

                        if (Z.HasValue)
                            MP = new Messpunkt(PNum, pos, Z.Value);
                        else
                            MP = new Messpunkt(PNum, pos);
                    }

                    MP.HeigthPrecision = HPrec;
                    lsMP.Add(MP);
                }
                catch
                {
                    if (i == 0)
                        bHeader = true;
                    else
                    {
                        _lsErrorLine.Add(i);
                        bErrorIO = true;
                    }
                }
            }

            try
            {
                if (!bErrorIO)
                {
                    int Anzahl = lsMP.Count;

                    if (ProcessingStarted != null)
                    {
                        string process = "Punkte importieren";
                        LongProcessStartedEventArgs e = new LongProcessStartedEventArgs(
                                                            process, lsMP.Count, true);

                        ProcessingStarted(this, e);
                    }

                    for (int i = 0; i < lsMP.Count; i++)
                    {
                        Messpunkt MP = lsMP[i];

                        if (ProcessingProgressed != null)
                        {
                            string progMsg = string.Format(
                                "{0} out of {1}. {2} remaining...\n" +
                                "Processing entity: {3}",
                                i,
                                lsMP.Count,
                                lsMP.Count - i,
                                MP.PNum);

                            LongProcessingProgressEventArgs e1 =
                                new LongProcessingProgressEventArgs(progMsg);
                            ProcessingProgressed(this, e1);

                            //Since this processing is cancellable, we
                            //test if user clicked the "Stop" button in the 
                            //progressing dialog box
                            if (e1.Cancel)
                            {
                                Anzahl = i;
                                break;
                            }
                        }

                        MP.draw(_block, _basislayer);
                    }

                    if (ProcessingEnded != null)
                    {
                        ProcessingEnded(this, EventArgs.Empty);
                    }

                    MessageBox.Show(Anzahl.ToString() + " Messpunkte eingefügt!");
                }
                else
                {
                    dlgPtImport diaPtImport = new dlgPtImport();

                    diaPtImport.Text = _Text;
                    diaPtImport.ShowDialog();
                }
                    
            }
            finally
            {
                //Make sure the CloseProgressUIRequested event always fires, so
                //that the progress dialog box gets closed because of this event
                if (CloseProgressUIRequested != null)
                {
                    CloseProgressUIRequested(this, EventArgs.Empty);
                }
            }
        }
        #region Implementing ILongProcessingObject interface

        public event LongProcessStarted ProcessingStarted;

        public event LongProcessingProgressed ProcessingProgressed;

        public event EventHandler ProcessingEnded;

        public event EventHandler CloseProgressUIRequested;

        public void DoLongProcessingWork()
        {
            insertMP();
        }

        #endregion
    }
}
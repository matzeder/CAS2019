using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using _AcAp = Autodesk.AutoCAD.ApplicationServices;
using _AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace CAS.myCAD
{
    public partial class MyLayer
    {
        private List<Autodesk.AutoCAD.DatabaseServices.LayerTableRecord> m_lsLayerTableRecord = new List<LayerTableRecord>();

        //Konstruktor (damit kein Default Konstruktor generiert wird!)
        protected MyLayer()
        {
            //Datenbank
            try {
                Database db = HostApplicationServices.WorkingDatabase;
                Transaction myT = db.TransactionManager.StartTransaction();

                using (DocumentLock dl = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    LayerTable layT = (LayerTable)myT.GetObject(db.LayerTableId, OpenMode.ForRead);

                    //Layernamen in Liste schreiben
                    foreach (ObjectId id in layT)
                    {
                        LayerTableRecord ltr = (LayerTableRecord)(myT.GetObject(id, OpenMode.ForRead));

                        m_lsLayerTableRecord.Add(ltr);
                    }
                }

                myT.Commit();
                myT.Dispose();
            }
            catch { }
        }
        //Properties
        public List<_AcDb.LayerTableRecord> LsLayerTableRecord
        {
            get { return m_lsLayerTableRecord; }
        }

        //Methoden
        //neuen Layer anlegen
        public int Add(string Name)
        {
            //Datenbank
            int isLayerCreated = 0;
            Database db = HostApplicationServices.WorkingDatabase;
            Transaction myT = db.TransactionManager.StartTransaction();

            using (DocumentLock dl = _AcAp.Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                LayerTable layT = (LayerTable)myT.GetObject(db.LayerTableId, OpenMode.ForWrite);

                if (!layT.Has(Name))
                {
                    LayerTableRecord layTR = new LayerTableRecord()
                        { Name = Name };

                    layT.Add(layTR);
                    m_lsLayerTableRecord.Add(layTR);
                    myT.AddNewlyCreatedDBObject(layTR, true);
                    isLayerCreated = 1;
                }
            }
            myT.Commit();
            myT.Dispose();

            return isLayerCreated;
        }

        public void Refresh()
        {
            //Datenbank
            Database db = HostApplicationServices.WorkingDatabase;

            if (db != null)
            {
                Transaction myT = db.TransactionManager.StartTransaction();

                using (DocumentLock dl = _AcAp.Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    LayerTable layT = (LayerTable)myT.GetObject(db.LayerTableId, OpenMode.ForRead);
                    m_lsLayerTableRecord.Clear();

                    //Layernamen in Liste schreiben
                    foreach (ObjectId id in layT)
                    {
                        LayerTableRecord ltr = (LayerTableRecord)(myT.GetObject(id, OpenMode.ForRead));

                        m_lsLayerTableRecord.Add(ltr);
                    }
                }

                myT.Commit();
                myT.Dispose();
            }
        }

        //Layerliste in csv exportieren
        public void Export()
        {
            myCAD.MyLayer objLayer = myCAD.MyLayer.Instance;

            string curDwg = HostApplicationServices.WorkingDatabase.Filename;

            SaveFileDialog ddSaveFile = new SaveFileDialog()
            {
                DefaultExt = "csv",
                Filter = "Layerliste|*.csv",
                InitialDirectory = curDwg.Substring(0, curDwg.LastIndexOf('\\')),
                FileName = curDwg.Substring(curDwg.LastIndexOf('\\') + 1, curDwg.LastIndexOf('.'))
            };

            if (ddSaveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(ddSaveFile.FileName, false, Encoding.Default);

                foreach (_AcDb.LayerTableRecord objLTR in objLayer.LsLayerTableRecord)
                {
                    string Zeile = objLTR.Name + ";";
                    Zeile += objLTR.Color.ToString() + ";";

                    sw.WriteLine(Zeile);
                }
                sw.Close();
            }
        }

        /// <summary>
        /// Prüft, ob Layer vorhanden. Legt diesen an, falls nicht vorhanden.
        /// </summary>
        /// <param name="Layer"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public bool CheckLayer(string Layer, bool create)
        {
            bool LayerExists = false;

            if (Layer != "")
            {
                List<string> lsLayer = new List<string>();

                try
                {
                    foreach (LayerTableRecord ltr in m_lsLayerTableRecord)
                        lsLayer.Add(ltr.Name);
                }
                catch { }

                if (lsLayer.Contains(Layer))
                    LayerExists = true;
                else
                {
                    if (create)
                        Add(Layer);
                }
            }
            return LayerExists;
        }

        public bool IsBlockLayer(string Layer)
        {
            bool blockLayer = false;

            if (Layer.Length > 2)
            {
                if (Layer.Substring(Layer.Length - 2, 2) == "-P")
                    blockLayer = true;
            }

            return blockLayer;
        }

        //Instanz (Singleton)
        public static MyLayer Instance
        {
            get { return MyLayerCreator.CreateInstance; }
        }

        private sealed class MyLayerCreator
        {
            private static readonly MyLayer _Instance = new MyLayer();

            public static MyLayer CreateInstance
            {
                get { return _Instance; }
            }
        }
    }
}

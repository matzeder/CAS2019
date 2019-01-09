using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;

namespace CAS.myAutoCAD
{
    public partial class Messpunkt
    {
        private string _Name;
        private string _PNum;
        private Point3d _Pos;
        private string _Layer;
        private double[] _scaleFactor;
        private double? _Höhe;
        private double? _CASHöhe;
        private int? m_HeigthPrecision;
        private BlockReference _blkDef = null;
        private BlockReference _blkRef = null;
        private List<Att> _Attributes = new List<Att>();

        //Constructor
        public Messpunkt(string PNum, Point3d Pos, double? Höhe)
        {
            _PNum = PNum;
            _Pos = Pos;
            _Höhe = Höhe;
        }

        public Messpunkt(BlockReference blkRef)
        {
            _blkRef = blkRef;
            _Name = blkRef.Name;
            _Pos = blkRef.Position;
            _Layer = blkRef.Layer;
            _scaleFactor = new double[] {blkRef.ScaleFactors.X,
                                         blkRef.ScaleFactors.Y,
                                         blkRef.ScaleFactors.Z};
        }

        public Messpunkt(string PNum, Point3d Pos3d)
        {
            _PNum = PNum;
            _Pos = Pos3d;
        }

        //Properties
        /// <summary>
        /// Blocknamen festlegen bzw. abfragen
        /// </summary>
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public Point3d Pos
        {
            get { return _Pos; }
        }

        public double? CASHöhe
        {
            get { return this._CASHöhe; }
            set { this._CASHöhe = value; }
        }

        /// <summary>
        /// Höhengenauigkeit
        /// </summary>
        public int? HeigthPrecision
        {
            get { return this.m_HeigthPrecision; }
            set { this.m_HeigthPrecision = value; }
        }

        //Attribute
        public List<Att> Attribute
        {
            get { return _Attributes; }
        }

        public int AttCount
        {
            get { return _Attributes.Count; }
        }

        //Methods
        public void addAttribute(Att att)
        {
            _Attributes.Add(att);
        }

        public Att getAttribute(int nr)
        {
            Att att = null;
            if (nr <= _Attributes.Count)
                att = _Attributes[nr];

            return att;
        }

        public void draw(BlockTableRecord btr, string Basislayer)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager myTm = db.TransactionManager;
            Transaction myT = db.TransactionManager.StartTransaction();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            BlockTableRecord _btRec = null;

            MyLayer objLayer = MyLayer.Instance;

            //Block in dwg suchen
            try
            {
                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    //BlockTable bt = (BlockTable)myT.GetObject(db.BlockTableId, OpenMode.ForRead);
                    //BlockTableRecord btr = (BlockTableRecord)myT.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    
                    _btRec = Prototyp.Instance.btRec;
                    //btrDef = (BlockTableRecord)myT.GetObject(bt[blockName], OpenMode.ForRead);
                    
                }
            }  
            //Block aus Prototypzeichnung holen
            catch { }

            //Block in Zeichnung einfügen
            if (_btRec != null)
            {
                ;
            }

        }

        public class Att
        {
            private string _Value;
            private Point3d _Pos;
            private string _Layer;
            private bool _visible;
            private string _Textstyle;
            private double _Height;
            private double _Oblique;
            private double _Width;

            //Constructor
            public Att() {}

            //Properties
            public string Value
            {
                set { _Value = value; }
                get { return _Value; }
            }

            public Point3d Pos
            {
                set { _Pos = value; }
                get { return _Pos; }
            }

            public string Layer
            {
                set { _Layer = value; }
                get { return Layer; }
            }
        }
    }

    public partial class Blöcke
    {
        private Database m_db = null;
        private Transaction m_myT = null;
        private Editor m_ed = null;

        private List<Messpunkt> ls_MP = new List<Messpunkt>();

        //Constructor (damit kein Default Konstruktor generiert wird!)
        protected Blöcke() { }

        //Properties
        public List<Messpunkt> lsMP
        {
            get { return ls_MP; }
        }

        public int count
        {
            get { return ls_MP.Count; }
        }

        //Methods
        public void init()
        {
            //Datenbank initialisieren
            m_db = HostApplicationServices.WorkingDatabase;
            m_myT = m_db.TransactionManager.StartTransaction();
            m_ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //Tabelle wieder löschen
            ls_MP.Clear();
        }

        //Instanz (Singleton)
        public static Blöcke Instance
        {
            get { return BlöckeCreator.createInstance; }
        }

        private sealed class BlöckeCreator
        {
            private static readonly Blöcke _Instance = new Blöcke();

            public static Blöcke createInstance
            {
                get { return _Instance; }
            }
        }

        public void close()
        {
            try
            {
                m_myT.Commit();
                m_myT.Dispose();
            }
            catch { }
        }

        private void fillTable(SelectionSet ssRes)
        {
            ObjectId[] objID = ssRes.GetObjectIds();
            int i = 0;

            //SelectionSet iterieren
            foreach (ObjectId blkID in objID)
            {
                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    try
                    {
                        m_myT.GetObject(blkID, OpenMode.ForRead);
                        BlockReference blkRef = (BlockReference)m_myT.GetObject(blkID, OpenMode.ForRead);

                        //nur Blöcke mit Attributen berücksichtigen
                        AttributeCollection colAtt = blkRef.AttributeCollection;

                        if (colAtt.Count >= 1)
                        {
                            Messpunkt MP = new Messpunkt(blkRef);

                            //XData lesen
                            ResultBuffer rb = blkRef.XData;

                            if (rb != null)
                            {
                                int n = 0;
                                foreach (TypedValue tv in rb)
                                {
                                    switch (tv.TypeCode)
                                    {
                                        case 1000:
                                            try
                                            {
                                                MP.CASHöhe = Convert.ToDouble(tv.Value);
                                            }
                                            catch { }

                                            break;

                                        case 1001:

                                            break;

                                        case 1071:
                                            try
                                            {
                                                MP.HeigthPrecision = Convert.ToInt32(tv.Value);
                                            }
                                            catch { }

                                            break;
                                    }
                                    n++;
                                }
                            }
                            //Attribute iterieren
                            foreach (ObjectId attID in colAtt)
                            {
                                AttributeReference attRef = (AttributeReference)m_myT.GetObject(attID, OpenMode.ForRead);

                                Messpunkt.Att att = new Messpunkt.Att();
                                att.Value = attRef.TextString;
                                att.Pos = attRef.Position;
                                att.Layer = attRef.Layer;

                                MP.addAttribute(att);


                                attRef.Dispose();
                            }

                            lsMP.Add(MP);
                        }
                    }
                    catch { }
                }
            }

            m_myT.Commit();

        }   //fillTable
    }

    public sealed class Prototyp
    {
        private string _blkName;
        private BlockTableRecord _btRec = null;

        Database db = HostApplicationServices.WorkingDatabase;
        Autodesk.AutoCAD.DatabaseServices.TransactionManager myTm = null;

        private static readonly Lazy<Prototyp> lazy =
            new Lazy<Prototyp>(() => new Prototyp());

        //Properties
        public string Blockname
        { set { _blkName = value; } }

        public BlockTableRecord btRec
        {
            get
            {
                myTm = db.TransactionManager;
                Transaction myT = db.TransactionManager.StartTransaction();
                try
                {
                    using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                    {
                        BlockTable bt = (BlockTable)myT.GetObject(db.BlockTableId, OpenMode.ForRead);
                        _btRec = (BlockTableRecord)myT.GetObject(bt[_blkName], OpenMode.ForRead);
                    }
                }
                catch { }
                return _btRec;
            }
        }

        public static Prototyp Instance
            { get { return lazy.Value; } }

        private Prototyp() { }
    }
}

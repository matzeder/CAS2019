using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

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
        private Point3d _Pos3d;
        private string _Layer;
        private double[] _scaleFactor;
        private double? _Höhe;
        private double? _CASHöhe;
        private int? _HeigthPrec;
        private BlockReference _blkRef = null;
        private List<Att> _Attributes = new List<Att>();

        //Constructor
        public Messpunkt(string PNum, Point3d Pos, double Höhe)
        {
            _PNum = PNum;
            _Pos3d = new Point3d(Pos.X, Pos.Y, Höhe);
            _Höhe = Höhe;
        }

        public Messpunkt(string PNum, Point2d Pos)
        {
            _PNum = PNum;
            _Pos3d = new Point3d(Pos.X, Pos.Y, 0);
        }

        public Messpunkt(string PNum, Point2d Pos, double Höhe)
        {
            _PNum = PNum;
            _Pos3d = new Point3d(Pos.X, Pos.Y, 0);
            _Höhe = Höhe;
        }

        public Messpunkt(BlockReference blkRef)
        {
            _blkRef = blkRef;
            _Name = blkRef.Name;
            _Pos3d = blkRef.Position;
            _Layer = blkRef.Layer;
            _scaleFactor = new double[] {blkRef.ScaleFactors.X,
                                         blkRef.ScaleFactors.Y,
                                         blkRef.ScaleFactors.Z};
        }

        public Messpunkt(string PNum, Point3d Pos3d)
        {
            _PNum = PNum;
            _Pos3d = Pos3d;
        }

        //Properties
        /// <summary>
        /// Blocknamen festlegen bzw. abfragen
        /// </summary>
        public string PNum
        {
            get { return this._PNum; }

        }

        public Point3d Pos
        {
            get { return _Pos3d; }
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
            get { return this._HeigthPrec; }
            set { this._HeigthPrec = value; }
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

        public void HeightVisible(bool visible)
         {
            Database db = HostApplicationServices.WorkingDatabase;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager myTm = db.TransactionManager;
            Transaction myT = db.TransactionManager.StartTransaction();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectContextManager conTextManager = db.ObjectContextManager;

            try
            {
                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    BlockTable bt = (BlockTable)myT.GetObject(db.BlockTableId, OpenMode.ForRead);
                    ObjectId id = bt[BlockTableRecord.ModelSpace];

                    AttributeCollection col = _blkRef.AttributeCollection;

                    foreach(ObjectId attId in col)
                    {
                        AttributeReference attRef = (AttributeReference)myT.GetObject(attId, OpenMode.ForWrite);

                        switch(attRef.Tag)
                        {
                            case "height":
                                attRef.Visible = visible;
                                attRef.Dispose();
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) { }

            finally
            {
                myT.Commit();
                myT.Dispose();
            }
        }

        public void draw(string block, string Basislayer)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager myTm = db.TransactionManager;
            Transaction myT = db.TransactionManager.StartTransaction();
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectContextManager conTextManager = db.ObjectContextManager;

            Dictionary<string, Point3d> _attPos = new Dictionary<string, Point3d>();
            List<AttributeDefinition> _attDef = new List<AttributeDefinition>();

            MyLayer objLayer = MyLayer.Instance;
            objLayer.CheckLayer(Basislayer, true);

            try
            {
                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    //Block in Zeichnung einfügen
                    BlockTable bt = (BlockTable)myT.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)myT.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    BlockTableRecord btrDef = (BlockTableRecord)myT.GetObject(bt[block], OpenMode.ForRead);

                    //Attribute aus Blockdefinition übernehmen      
                    if (btrDef.HasAttributeDefinitions)
                    {
                        foreach (ObjectId id in btrDef)
                        {
                            DBObject obj = myT.GetObject(id, OpenMode.ForRead);
                            try
                            {
                                AttributeDefinition ad = (AttributeDefinition)obj;

                                if (ad != null)
                                {
                                    _attPos.Add(ad.Tag, ad.Position);
                                    _attDef.Add(ad);
                                }
                            }
                            catch
                            {
                                try
                                {
                                    Entity ent = (Entity)obj;
                                    //Layer = ent.Layer;
                                }
                                catch { }
                            }
                        }
                    }

                    BlockReference blkRef = new BlockReference(_Pos3d, bt[block]);
                    blkRef.ScaleFactors = new Scale3d(db.Cannoscale.Scale);
                    blkRef.Layer = Basislayer;
                    btr.AppendEntity(blkRef);

                    myT.AddNewlyCreatedDBObject(blkRef, true); ;

                    //Attribute befüllen
                    if (_attPos != null)
                    {
                        string[] lsBasislayer = Basislayer.Split('-');
                        string Stammlayer = lsBasislayer[lsBasislayer.Length - 2];

                        for (int i = 0; i < _attPos.Count; i++)
                        {
                            AttributeReference _attRef = new AttributeReference();
                            _attRef.SetDatabaseDefaults();
                            _attRef.SetAttributeFromBlock(_attDef[i], Matrix3d.Identity);
                            _attRef.SetPropertiesFrom(_attDef[i]);

                            Point3d ptBase = new Point3d(blkRef.Position.X + _attRef.Position.X,
                                                         blkRef.Position.Y + _attRef.Position.Y,
                                                         blkRef.Position.Z + _attRef.Position.Z);


                            _attRef.Position = ptBase;

                            string attLayer = String.Empty;

                            KeyValuePair<string, Point3d> keyValuePair = _attPos.ElementAt(i);
                            switch (keyValuePair.Key)
                            {
                                case "number":
                                    _attRef.TextString = _PNum;
                                    _attRef.Layer = Stammlayer + "-P";
                                    break;

                                case "height":
                                    if (_Höhe.HasValue)
                                        _attRef.TextString = _Höhe.Value.ToString("F" + _HeigthPrec.ToString());

                                    _attRef.Layer = Stammlayer + "-P";
                                    break;

                                case "date":
                                    _attRef.Layer = Stammlayer + "-Datum";
                                    break;

                                case "code":
                                    _attRef.Layer = Stammlayer + "-C";
                                    break;

                                case "owner":
                                    _attRef.Layer = Stammlayer + "-Hersteller";
                                    break;

                                default:
                                    break;
                            }

                            blkRef.AttributeCollection.AppendAttribute(_attRef);
                            myT.AddNewlyCreatedDBObject(_attRef, true);
                        }
                    }
                }
            }
            //Block aus Prototypzeichnung holen
            catch (Autodesk.AutoCAD.Runtime.Exception e) { }

            finally
            {
                myT.Commit();
                
                myT.Dispose();
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

    //public sealed class Prototyp
    //{
    //    private bool _BlockFound = false;
    //    private string _blkName;
    //    private BlockTableRecord _btRec = null;

    //    private static readonly Lazy<Prototyp> lazy =
    //        new Lazy<Prototyp>(() => new Prototyp());

    //    //Properties
    //    public string Blockname
    //    { set {
    //            _blkName = value;
    //            refresh();
    //        } }

    //    public bool OK
    //    {
    //        get { return _BlockFound; }
    //    }

    //    public BlockTableRecord btRec
    //    {
    //        get { return _btRec; }
    //    }

    //    private void refresh()
    //    {
    //        Document myDWG;
    //        DocumentLock myDWGlock;
    //        Database db = HostApplicationServices.WorkingDatabase;
    //        Autodesk.AutoCAD.DatabaseServices.TransactionManager myTm = null;
    //        myTm = db.TransactionManager;
    //        Transaction myT = db.TransactionManager.StartTransaction();

    //        try
    //        {
    //            using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
    //            {
    //                BlockTable bt = (BlockTable)myT.GetObject(db.BlockTableId, OpenMode.ForRead);
    //                _btRec = (BlockTableRecord)myT.GetObject(bt[_blkName], OpenMode.ForRead);
    //                _BlockFound = true;
    //            }
    //        }
    //        catch { _BlockFound = false; }

    //        finally
    //        {
    //            myT.Commit();
    //            myT.Dispose();
    //        }

    //        //if not found in dwg get it from Folder .\Blocks
    //        if (!_BlockFound)
    //        {
    //            string ProtoDWG = CAS.myUtilities.Global.Instance.PrototypFullPath;

    //            if (File.Exists(ProtoDWG))
    //            {
    //                try
    //                {
    //                    myDWG = Application.DocumentManager.MdiActiveDocument;
    //                    myDWGlock = myDWG.LockDocument();
    //                    Database srcDb = new Database();
    //                    srcDb.ReadDwgFile(ProtoDWG, FileShare.Read, true, "");
    //                    ObjectIdCollection blockIds = new ObjectIdCollection();

    //                    Autodesk.AutoCAD.DatabaseServices.TransactionManager srcT = srcDb.TransactionManager;
    //                    using (Transaction protoT = srcT.StartTransaction())
    //                    {
    //                        BlockTable bt = (BlockTable)protoT.GetObject(srcDb.BlockTableId, OpenMode.ForRead, false);

    //                        foreach(ObjectId btrid in bt)
    //                        {
    //                            BlockTableRecord btr = (BlockTableRecord)protoT.GetObject(btrid, OpenMode.ForRead, false);
    //                            if(!btr.IsAnonymous && !btr.IsLayout)
    //                            {
    //                                blockIds.Add(btrid);
    //                            }
    //                            btr.Dispose();
    //                        }
    //                    }

    //                    //Block in aktuelle Zeichnung einfügen
    //                    IdMapping mapping = new IdMapping();
    //                    srcDb.WblockCloneObjects(blockIds, db.BlockTableId, mapping, DuplicateRecordCloning.Replace, false);

    //                    srcDb.Dispose();
    //                    myDWGlock.Dispose();
    //                }
    //                catch(Autodesk.AutoCAD.Runtime.Exception e) { }
    //            }
    //            else
    //                System.Windows.Forms.MessageBox.Show("Block " + _blkName + " nicht gefunden!");
    //        }
    //    }

    //    public static Prototyp Instance
    //        { get { return lazy.Value; } }

    //    private Prototyp() { }
    //}
}

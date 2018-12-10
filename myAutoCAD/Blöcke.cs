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
    public class Messpunkt
    {
        private string _Name;
        private int? _AttCount = null;
        private string _PNum;
        private Point3d _Pos;
        private double? _Höhe;
        private BlockReference _blkRef = null;
        private List<Attributes> _Attributes = new List<Attributes>();

        //Constructor
        public Messpunkt(string PNum, Point3d Pos, double? Höhe)
        {
            _PNum = PNum;
            _Pos = Pos;
            _Höhe = Höhe;
        }

        private class Attributes
        {
            private Point3d _Pos;
            private string _Layer;
            private bool _visible;
            private string _Textstyle;
            private double _Height;
            private double _Oblique;
            private double _Width;

            //Constructor
            Attributes(Point3d Pos, string Layer, bool Visible, string Textstyle, double Height, double Oblique, double Width)
            {
                _Pos = Pos;
                _Layer = Layer;
                _visible = Visible;
                _Textstyle = Textstyle;
                _Height = Height;
                _Oblique = Oblique;
                _Width = Width;
            }

            //Properties
            public Point3d Pos
            {
                get { return _Pos; }
                set { _Pos = value; }
            }
        }
    }

    public class Blöcke
    {
        private Database m_db = null;
        private Transaction m_myT = null;
        private Editor m_ed = null;

        private Messpunkt[] m_vMP = null;           //alle Messpunkte
        private Messpunkt[] m_vMPheigth = null;     //Messpunkte mit korrektem Höhenattribut
        private Messpunkt[] m_vMPbereich = null;    //Messpunkte im gewählten Höhenbereich
        private int[] m_vMPempty = null;
        private int m_vMPEmptycount = 0;

        private double? m_Hmin = null;              //minimale Höhe
        private double? m_Hmax = null;              //maximale Höhe

        private lsHöhen m_lsHöhen = new lsHöhen();
        private List<string> mls_textStyles = new List<string>();
        private string m_Textstyle = null;


        //Konstruktor (damit kein Default Konstruktor generiert wird!)
        protected Blöcke() { }

        public void init()
        {
            //Datenbank initialisieren
            m_db = HostApplicationServices.WorkingDatabase;
            m_myT = m_db.TransactionManager.StartTransaction();
            m_ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //Tabelle neu initialisieren
            m_vMP = null;
            m_vMPempty = null;
            m_vMPEmptycount = 0;

            m_Hmin = null;
            m_Hmax = null;
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

        //Properties
        /// <summary>
        /// Anzahl der gewählten Blöcke
        /// </summary>
        public int count
        {
            get
            {
                try
                {
                    return this.m_vMP.Length;
                }
                catch { return 0; }
            }
        }

        /// <summary>
        /// Id's von Blöcken mit leerem Höhenstring
        /// </summary>
        public int[] idBlockEmpty
        {
            get { return this.m_vMPempty; }
        }

        /// <summary>
        /// Anzahl der Blöcke mit gültiger Höhe
        /// </summary>
        public int countHeigths
        {
            get { return this.m_vMPheigth.Length; }
        }

        /// <summary>
        /// Anzahl der Blöcke mit leerem Höhenstring
        /// </summary>
        public int countEmptyHeigths
        {
            get { return this.m_vMPEmptycount; }
        }

        /// <summary>
        /// kleinste Höhe
        /// </summary>
        public double getHmin
        {
            get { return this.m_Hmin.Value; }
        }

        /// <summary>
        /// größte Höhe
        /// /// </summary>
        public double getHmax
        {
            get { return this.m_Hmax.Value; }
        }

        /// <summary>
        /// alle Messpunkte
        /// </summary>
        public Messpunkt[] getMP
        {
            get { return this.m_vMP; }
        }

        /// <summary>
        /// Messpunkte im gewählten Höhenbereich
        /// </summary>
        public Messpunkt[] getMPsel
        {
            get { return this.m_vMPbereich; }
        }

        /// <summary>
        /// löscht Blöcke mit übergebenen Namen aus Blockliste
        /// </summary>
        /// <param name="Blocknamen"></param>
        //public void delNames(string[] Blocknamen)
        //{
        //    Messpunkt[] vBlöckeNeu = new Messpunkt[m_vMP.Length];
        //    List<Messpunkt> lsMPNeu = new List<Messpunkt>();

        //    foreach (Messpunkt objMP in m_vMP)
        //    {
        //        if (!Blocknamen.Contains(objMP.Name))
        //            lsMPNeu.Add(objMP);
        //    }

        //    m_vMP = lsMPNeu.ToArray();
        //}

        /// <summary>
        /// TextStile in Attributen abfragen
        /// </summary>
        //public string[] getTextstyles
        //{
        //    get
        //    {
        //        string[] lsTextstyles = new String[this.mls_textStyles.Count];

        //        for (int i = 0; i < this.mls_textStyles.Count; i++)
        //        {
        //            lsTextstyles[i] = this.mls_textStyles[i];
        //        }
        //        return lsTextstyles;
        //    }
        //}

        /// <summary>
        /// Textstil für Blockattribute setzen
        /// </summary>
        //public string setTextstyle
        //{
        //    set { this.m_Textstyle = value; }
        //}

        //public int? setHeightPrecision
        //{
        //    set { this.m_HeightPrecision = value; }
        //}

        //public List<string> getNames
        //{
        //    get
        //    {
        //        List<string> lsNamen = new List<string>();

        //        foreach (Messpunkt objMP in m_vMP)
        //        {
        //            if (!lsNamen.Contains(objMP.Name))
        //                lsNamen.Add(objMP.Name);
        //        }

        //        return lsNamen;
        //    }
        //}

        //Methoden

        /// <summary>
        /// alle Blöcke aus Zeichnung lesen
        /// </summary>
        /// <returns></returns>
        public ErrorStatus selectAll()
        {
            ErrorStatus eStatus = ErrorStatus.KeyNotFound;
            SelectionSet ssRes = null;

            //Blöcke auswählen
            //Filter
            TypedValue[] values = new TypedValue[1] {
                new TypedValue((int)DxfCode.Start, "INSERT")};
            SelectionFilter selFilter = new SelectionFilter(values);

            PromptSelectionResult resSel = m_ed.SelectAll(selFilter);
            ssRes = resSel.Value;

            if (ssRes.Count > 0)
            {
                fillTable(ssRes);
                eStatus = ErrorStatus.OK;
            }

            return eStatus;
        }   //selectAll

        /// <summary>
        /// Blöcke mit Fenster wählen
        /// </summary>
        /// <returns></returns>
        public ErrorStatus selectWindow()
        {
            ErrorStatus eStatus = ErrorStatus.KeyNotFound;
            SelectionSet ssRes = null;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            //Filter
            TypedValue[] values = new TypedValue[1] {
                new TypedValue((int)DxfCode.Start, "INSERT")};
            SelectionFilter selFilter = new SelectionFilter(values);

            PromptSelectionResult resSel = ed.GetSelection(selFilter);

            if (resSel.Status == PromptStatus.OK)
            {
                ssRes = resSel.Value;

                fillTable(ssRes);
                eStatus = ErrorStatus.OK;
            }

            return eStatus;
        }   //selectWindow

        /// <summary>
        /// Blöcke mit eigenem Filter auswählen
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public ErrorStatus selectWindow(SelectionFilter selFilter)
        {
            ErrorStatus eStatus = ErrorStatus.KeyNotFound;
            SelectionSet ssRes = null;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionResult resSel = ed.GetSelection(selFilter);

            if (resSel.Status == PromptStatus.OK)
            {
                ssRes = resSel.Value;

                fillTable(ssRes);
                eStatus = ErrorStatus.OK;
            }

            return eStatus;
        }   //selectWindow

        /// <summary>
        /// Wert zu Attributen addieren
        /// </summary>
        /// <param name="dH"></param>
        public void addHeigth(double dH)
        {
            for (int i = 0; i < m_vMP.Length; i++)
            {
                Messpunkt MP = m_vMP[i];
                if (MP.Höhe != null)
                    MP.Höhe += dH;

            }   //for

            //Attribute aktualisieren
            refreshAttributes();
        }   //addHeigth

        /// <summary>
        /// Sichtbarkeit von Blockattributen schalten
        /// </summary>
        /// <param name="Switch"></param>
        public void attSwitch(string Switch)
        {
            switch (Switch)
            {
                case "AttPon":
                    foreach (Messpunkt MP in m_vMP)
                        MP.Att1_Visible = true;
                    break;

                case "AttPoff":
                    foreach (Messpunkt MP in m_vMP)
                        MP.Att1_Visible = false;
                    break;

                case "AttHon":
                    foreach (Messpunkt MP in m_vMP)
                        MP.Att2_Visible = true;
                    break;

                case "AttHoff":
                    foreach (Messpunkt MP in m_vMP)
                        MP.Att2_Visible = false;
                    break;
            }
            refreshAttributes();
        }

        //Kommastellen anpassen
        public void Kommastellen(int decimals)
        {
            foreach (Messpunkt MP in m_vMP)
                MP.Kommastellen(decimals);

            refreshAttributes();
        }

        //Messpunkt an bestimmter Position
        public ErrorStatus findPos(ref Messpunkt MP, Point2d Position, double Tolerance)
        {
            ErrorStatus eStatus = ErrorStatus.KeyNotFound;

            for (int i = 0; i < m_vMP.Length; i++)
            {
                Point3d blockPos = m_vMP[i].Position;
                Point2d blockPos2d = new Point2d(blockPos.X, blockPos.Y);

                if ((Position.GetDistanceTo(blockPos2d) < Tolerance)    //nur Blöcke mit mind. 1 Attribut
                    && m_vMP[i].AttCount > 0 && !m_vMP[i].isErased)
                {
                    MP = m_vMP[i];
                    eStatus = ErrorStatus.OK;
                    break;
                }
            }

            return eStatus;
        }

        //Messpunkt auf Bogen
        public ErrorStatus findArc(ref Messpunkt MP, CircularArc2d objArc, double Tolerance)
        {
            ErrorStatus eStatus = ErrorStatus.KeyNotFound;

            for (int i = 0; i < m_vMP.Length; i++)
            {
                Point3d blockPos = m_vMP[i].Position;
                Point2d blockPos2d = new Point2d(blockPos.X, blockPos.Y);

                if ((objArc.GetDistanceTo(blockPos2d) < Tolerance)    //nur Blöcke mit mind. 1 Attribut
                    && m_vMP[i].AttCount > 0)
                {
                    //Anfangs- und Endpunkt des Bogens auschliessen
                    Point2d ptMP = new Point2d(m_vMP[i].Position.X, m_vMP[i].Position.Y);
                    if ((objArc.StartPoint.GetDistanceTo(ptMP) > Tolerance)
                        && (objArc.EndPoint.GetDistanceTo(ptMP) > Tolerance))
                    {
                        MP = m_vMP[i];
                        eStatus = ErrorStatus.OK;
                        break;
                    }
                }
            }

            return eStatus;
        }

        public ErrorStatus findID(ref Messpunkt MP, int id)
        {
            ErrorStatus eStatus = ErrorStatus.NotInBlock;

            try
            {
                MP = m_vMP[id];
            }
            finally
            {
                eStatus = ErrorStatus.OK;
            }

            return eStatus;
        }


        /// <summary>
        /// MP anhand von Höhenbereich finden
        /// </summary>
        /// <param name="?"></param>
        /// <param name="Hstart"></param>
        /// <param name="Hend"></param>
        /// <returns></returns>
        public ErrorStatus createMPBereich(double Hstart, double Hend)
        {
            ErrorStatus eStatus = ErrorStatus.NotApplicable;

            //Anzahl der Punkte im Höhenbereich bestimmen
            int iMPBereich = 0;

            foreach (Messpunkt mp in m_vMP)
            {
                if ((mp.Höhe >= Hstart) && (mp.Höhe <= Hend))
                    iMPBereich++;
            }

            //Array befüllen
            Messpunkt[] vMPBereich = new Messpunkt[iMPBereich];
            m_vMPbereich = vMPBereich;
            int iZähler = 0;

            foreach (Messpunkt mp in m_vMP)
            {
                if ((mp.Höhe >= Hstart) && (mp.Höhe <= Hend))
                    vMPBereich[iZähler++] = mp;
            }

            if (m_vMPbereich != null)
                eStatus = ErrorStatus.OK;

            return eStatus;
        }

        private void fillTable(SelectionSet ssRes)
        {
            Messpunkt[] vMP = new Messpunkt[ssRes.Count];
            m_vMP = vMP;

            int[] vMPempty = new int[ssRes.Count];
            m_vMPempty = vMPempty;

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

                            m_vMP[i] = new Messpunkt();

                            m_vMP[i].BlockReferenz = blkRef;
                            m_vMP[i].Name = blkRef.Name;
                            m_vMP[i].Position = blkRef.Position;
                            m_vMP[i].Layer = blkRef.Layer;
                            m_vMP[i].FaktorX = blkRef.ScaleFactors.X;
                            m_vMP[i].FaktorY = blkRef.ScaleFactors.Y;
                            m_vMP[i].FaktorZ = blkRef.ScaleFactors.Z;

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
                                                m_vMP[i].CASHöhe = Convert.ToDouble(tv.Value);
                                            }
                                            catch { }

                                            break;

                                        case 1001:

                                            break;

                                        case 1071:
                                            try
                                            {
                                                m_vMP[i].HeigthPrecision = Convert.ToInt32(tv.Value);
                                            }
                                            catch { }

                                            break;
                                    }
                                    n++;
                                }
                            }
                            //Attribute iterieren
                            int attCount = 0;

                            foreach (ObjectId attID in colAtt)
                            {
                                AttributeReference attRef = (AttributeReference)m_myT.GetObject(attID, OpenMode.ForRead);

                                switch (attCount)
                                {
                                    //Punktnummer
                                    case 0:
                                        m_vMP[i].PNum = attRef.TextString;

                                        m_vMP[i].Att1_Pos = attRef.Position;
                                        m_vMP[i].Att1_Layer = attRef.Layer;
                                        m_vMP[i].Att1_Height = attRef.Height;
                                        m_vMP[i].Att1_Neigung = attRef.Oblique;
                                        m_vMP[i].Att1_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att1_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att1_Visible = attRef.Visible;

                                        if (!this.mls_textStyles.Contains(attRef.TextStyleName))
                                            this.mls_textStyles.Add(attRef.TextStyleName);
                                        break;

                                    //Punkthöhe 
                                    case 1:
                                        string sHöhe = attRef.TextString;

                                        m_vMP[i].Att2_Pos = attRef.Position;
                                        m_vMP[i].Att2_Layer = attRef.Layer;
                                        m_vMP[i].Att2_Height = attRef.Height;
                                        m_vMP[i].Att2_Neigung = attRef.Oblique;
                                        m_vMP[i].Att2_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att2_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att2_Visible = attRef.Visible;

                                        //leeren Textstring überspringen
                                        if (sHöhe.Trim() == "")
                                        {
                                            m_vMP[i].Höhe = null;
                                            m_vMP[i].HeigthPrecision = null;
                                            m_vMPempty[m_vMPEmptycount++] = i;
                                            break;
                                        }

                                        myAutoCAD.myUtilities objUtil = new myUtilities();
                                        double dHöhe = 0;
                                        if (objUtil.convertToDouble(sHöhe, ref dHöhe, null) == ErrorStatus.OK)
                                        {
                                            //double dHöhe = Convert.ToDouble(sHöhe);
                                            m_vMP[i].Höhe = dHöhe;

                                            if ((dHöhe < m_Hmin) || (m_Hmin == null))
                                            {
                                                m_Hmin = dHöhe;
                                            }

                                            if ((dHöhe > m_Hmax) || (m_Hmax == null))
                                                m_Hmax = dHöhe;

                                            try
                                            {
                                                sHöhe = sHöhe.Substring(sHöhe.LastIndexOf('.'));
                                            }
                                            catch { }

                                            if (sHöhe[0] == '.')
                                                sHöhe = sHöhe.Substring(1);

                                            if (m_vMP[i].HeigthPrecision == null)
                                                m_vMP[i].HeigthPrecision = sHöhe.Length;
                                        }
                                        break;

                                    //Att3
                                    case 2:
                                        string sAtt3Wert = attRef.TextString;

                                        m_vMP[i].Att3_Pos = attRef.Position;
                                        m_vMP[i].Att3_Layer = attRef.Layer;
                                        m_vMP[i].Att3_Height = attRef.Height;
                                        m_vMP[i].Att3_Neigung = attRef.Oblique;
                                        m_vMP[i].Att3_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att3_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att3_Wert = attRef.TextString;

                                        break;

                                    //Att4
                                    case 3:
                                        string sAtt4Wert = attRef.TextString;

                                        m_vMP[i].Att4_Pos = attRef.Position;
                                        m_vMP[i].Att4_Layer = attRef.Layer;
                                        m_vMP[i].Att4_Height = attRef.Height;
                                        m_vMP[i].Att4_Neigung = attRef.Oblique;
                                        m_vMP[i].Att4_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att4_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att4_Wert = attRef.TextString;

                                        break;

                                    //Att5
                                    case 4:
                                        string sAtt5Wert = attRef.TextString;

                                        m_vMP[i].Att5_Pos = attRef.Position;
                                        m_vMP[i].Att5_Layer = attRef.Layer;
                                        m_vMP[i].Att5_Height = attRef.Height;
                                        m_vMP[i].Att5_Neigung = attRef.Oblique;
                                        m_vMP[i].Att5_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att5_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att5_Wert = attRef.TextString;

                                        break;

                                    //Att6
                                    case 5:
                                        string sAtt6Wert = attRef.TextString;

                                        m_vMP[i].Att6_Pos = attRef.Position;
                                        m_vMP[i].Att6_Layer = attRef.Layer;
                                        m_vMP[i].Att6_Height = attRef.Height;
                                        m_vMP[i].Att6_Neigung = attRef.Oblique;
                                        m_vMP[i].Att6_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att6_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att6_Wert = attRef.TextString;

                                        break;

                                    //Att7
                                    case 6:
                                        string sAtt7Wert = attRef.TextString;

                                        m_vMP[i].Att7_Pos = attRef.Position;
                                        m_vMP[i].Att7_Layer = attRef.Layer;
                                        m_vMP[i].Att7_Height = attRef.Height;
                                        m_vMP[i].Att7_Neigung = attRef.Oblique;
                                        m_vMP[i].Att7_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att7_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att7_Wert = attRef.TextString;

                                        break;

                                    //Att8
                                    case 7:
                                        string sAtt8Wert = attRef.TextString;

                                        m_vMP[i].Att8_Pos = attRef.Position;
                                        m_vMP[i].Att8_Layer = attRef.Layer;
                                        m_vMP[i].Att8_Height = attRef.Height;
                                        m_vMP[i].Att8_Neigung = attRef.Oblique;
                                        m_vMP[i].Att8_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att8_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att8_Wert = attRef.TextString;

                                        break;

                                    //Att9
                                    case 8:
                                        string sAtt9Wert = attRef.TextString;

                                        m_vMP[i].Att9_Pos = attRef.Position;
                                        m_vMP[i].Att9_Layer = attRef.Layer;
                                        m_vMP[i].Att9_Height = attRef.Height;
                                        m_vMP[i].Att9_Neigung = attRef.Oblique;
                                        m_vMP[i].Att9_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att9_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att9_Wert = attRef.TextString;

                                        break;

                                    //Att10
                                    case 9:
                                        string sAtt10Wert = attRef.TextString;

                                        m_vMP[i].Att10_Pos = attRef.Position;
                                        m_vMP[i].Att10_Layer = attRef.Layer;
                                        m_vMP[i].Att10_Height = attRef.Height;
                                        m_vMP[i].Att10_Neigung = attRef.Oblique;
                                        m_vMP[i].Att10_Textstil = attRef.TextStyleName;
                                        m_vMP[i].Att10_Breitenfaktor = attRef.WidthFactor;
                                        m_vMP[i].Att10_Wert = attRef.TextString;

                                        break;

                                    default:
                                        break;
                                }
                                attCount++;
                                attRef.Dispose();
                            }
                            m_vMP[i].AttCount = attCount;
                            i++;

                        }
                    }
                    catch { }
                }
            }

            //Array auf tatsächliche Größe reduzieren
            Messpunkt[] vMPneu = new Messpunkt[i];
            for (int j = 0; j < i; j++)
            {
                vMPneu[j] = m_vMP[j];
            }
            m_vMP = vMPneu;
            //Vektor mit MP mit gültiger Höhe befüllen
            createValidHeigths();

            m_myT.Commit();

        }   //fillTable

        /// <summary>
        /// Attribute aktualisieren
        /// </summary>
        private void refreshAttributes()
        {
            IFormatProvider iFormatDE = CultureInfo.GetCultureInfo("de-DE").NumberFormat;
            Transaction tr = m_db.TransactionManager.StartTransaction();

            myFunctions.Settings objSettings = new myFunctions.Settings();

            using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {


                for (int i = 0; i < m_vMP.Length; i++)
                {
                    Messpunkt MP = m_vMP[i];
                    BlockTable bt = (BlockTable)tr.GetObject(m_db.BlockTableId, OpenMode.ForRead);
                    ObjectId id = bt[BlockTableRecord.ModelSpace];

                    //Attribute iterieren
                    AttributeCollection colAtt = MP.BlockReferenz.AttributeCollection;
                    int attCount = 0;

                    foreach (ObjectId attID in colAtt)
                    {
                        AttributeReference attRef = (AttributeReference)tr.GetObject(attID, OpenMode.ForRead);

                        switch (attCount)
                        {
                            //Punktnummer
                            case 0:
                                attRef.UpgradeOpen();
                                attRef.Visible = MP.Att1_Visible;
                                attRef.DowngradeOpen();
                                break;

                            //Punkthöhe 
                            case 1:
                                string sFormat = "{0:f" + objSettings.HeightPrecision.ToString() + "}";

                                attRef.UpgradeOpen();
                                attRef.TextString = string.Format(sFormat, m_vMP[i].Höhe);
                                attRef.Visible = MP.Att2_Visible;
                                attRef.DowngradeOpen();

                                attRef.Dispose();
                                break;

                            default:
                                break;
                        }

                        attCount++;
                    }
                }
            }
            tr.Commit();
        }   //refreshAttributes

        /// <summary>
        /// Eigenschaften der Blockattribute aktualisieren
        /// </summary>
        public void refreshAttProperties(string A1Textstil, double? A1Texthöhe, double? A1Neigung, double? A1Breitenfaktor,
                                         string A2Textstil, double? A2Texthöhe, double? A2Neigung, double? A2Breitenfaktor, bool A2SwitchVis, int? precision)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Transaction myT = db.TransactionManager.StartTransaction();

            for (int i = 0; i < m_vMP.Length; i++)
            {
                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    Messpunkt MP = m_vMP[i];

                    //Attribute iterieren
                    AttributeCollection colAtt = MP.BlockReferenz.AttributeCollection;
                    int attCount = 0;

                    foreach (ObjectId attID in colAtt)
                    {
                        AttributeReference attRef = (AttributeReference)myT.GetObject(attID, OpenMode.ForWrite);
                        switch (attCount)
                        {
                            //Punktnummer 
                            case 0:
                                if (A1Textstil != null)
                                {
                                    myAutoCAD.myUtilities myUtil = new myUtilities();

                                    ObjectId A1TextstilId = new ObjectId();
                                    if (myUtil.getTextStyleId(A1Textstil, ref A1TextstilId) == ErrorStatus.OK)
                                        attRef.TextStyleId = A1TextstilId;
                                }

                                if (A1Texthöhe.HasValue)
                                    attRef.Height = A1Texthöhe.Value;

                                if (A1Neigung.HasValue)
                                    attRef.Oblique = A1Neigung.Value;

                                if (A1Breitenfaktor.HasValue)
                                    attRef.WidthFactor = A1Breitenfaktor.Value;

                                break;

                            // Punkthöhe
                            case 1:
                                if (A2Textstil != null)
                                {
                                    myAutoCAD.myUtilities myUtil = new myUtilities();

                                    ObjectId A2TextstilId = new ObjectId();
                                    if (myUtil.getTextStyleId(A2Textstil, ref A2TextstilId) == ErrorStatus.OK)
                                        attRef.TextStyleId = A2TextstilId;

                                }

                                if (A2Texthöhe.HasValue)
                                    attRef.Height = A2Texthöhe.Value;

                                if (A2Neigung.HasValue)
                                    attRef.Oblique = A2Neigung.Value;

                                if (A2Breitenfaktor.HasValue)
                                    attRef.WidthFactor = A2Breitenfaktor.Value;

                                if (A2SwitchVis)
                                {
                                    bool bVis = attRef.Invisible;
                                    if (bVis)
                                    {
                                        attRef.Invisible = false;
                                        attRef.Visible = true;
                                    }
                                    else
                                    {
                                        attRef.Invisible = true;
                                        attRef.Visible = false;
                                    }
                                }

                                if (precision.HasValue)
                                {
                                    string sFormat = "{0:f" + precision.Value.ToString() + "}";
                                    if ((precision < this.m_vMP[i].HeigthPrecision) && precision.HasValue && m_vMP[i].Höhe.HasValue)
                                        attRef.TextString = string.Format(sFormat, m_vMP[i].Höhe);
                                }

                                break;

                            default:
                                break;
                        }

                        attCount++; ;
                    }
                }
            }
            myT.Commit();
            Application.UpdateScreen();
            m_ed.Regen();
        }

        /// <summary>
        /// Blöcke aus Prototypzeichnung übernehmen
        /// </summary>
        /// <param name="objProto"></param>
        /// <returns></returns>
        public ErrorStatus updateBlocks(Prototyp objProto, List<string> lsNamen)
        {
            Transaction tr = m_db.TransactionManager.StartTransaction();

            ErrorStatus es = ErrorStatus.KeyNotFound;
            List<BlockReference> lsBlkRef = objProto.getBlockRefList;

            //zu ersetzende Blöcke umbenennen
            //Blocktable
            try
            {
                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    BlockTable bt = (BlockTable)(tr.GetObject(m_db.BlockTableId, OpenMode.ForWrite));
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    ObjectIdCollection blockIDs = new ObjectIdCollection();

                    //Blockdefinition aus Prototypzeichnung einfügen
                    objProto.importBlock(lsNamen);


                    foreach (ObjectId btrId in btr)
                    {
                        BlockReference br = (BlockReference)tr.GetObject(btrId, OpenMode.ForWrite);

                        if (lsNamen.Contains(br.Name))
                        {
                            //zu ersetzende Blöcke umbenennen




                            //neuen Block anlegen
                        }
                    }

                }
            }
            catch { }

            tr.Commit();
            tr.Dispose();

            return es;
        }

        public ErrorStatus refreshAttributes(int index)
        {
            IFormatProvider iFormatDE = CultureInfo.GetCultureInfo("de-DE").NumberFormat;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            DocumentLock dl = doc.LockDocument(DocumentLockMode.ProtectedAutoWrite, null, null, true);
            Database db = HostApplicationServices.WorkingDatabase;

            ErrorStatus eS = ErrorStatus.KeyNotFound;

            Transaction tr = db.TransactionManager.StartTransaction();

            Messpunkt MP = m_vMP[index];
            //Attribute iterieren
            try
            {

                AttributeCollection colAtt = MP.BlockReferenz.AttributeCollection;
                int attCount = 0;

                foreach (ObjectId attID in colAtt)
                {
                    AttributeReference attRef = (AttributeReference)tr.GetObject(attID, OpenMode.ForRead);
                    switch (attCount)
                    {
                        //Punkthöhe 
                        case 1:

                            string sFormat = "{0:f" + m_vMP[index].HeigthPrecision.ToString() + "}";
                            attRef.UpgradeOpen();
                            attRef.TextString = string.Format(sFormat, m_vMP[index].Höhe);
                            attRef.DowngradeOpen();

                            //attRef.Dispose();
                            break;

                        default:
                            break;
                    }

                    attCount++;
                }
            }
            catch { }
            tr.Commit();

            return eS;
        }

        public void HeigthsOffset(double Hmin, double Hmax, double Offset)
        {
            createMPBereich(Hmin, Hmax);
            foreach (Messpunkt mp in m_vMPbereich)
            {
                mp.Höhe += Offset;
            }

            //Attribute aktualisieren
            refreshAttributes();
        }

        public void Dispose()
        {
            m_myT.Dispose();
        }


        private void createValidHeigths()
        {
            Messpunkt[] vMPmitHöhe = new Messpunkt[countValidHeigths()];
            m_vMPheigth = vMPmitHöhe;
            int i = 0;

            foreach (Messpunkt mp in m_vMP)
            {
                if (mp.Höhe != null)
                    m_vMPheigth[i++] = mp;
            }
        }

        //MP mit gültiger Höhe zählen
        private int countValidHeigths()
        {
            int i = 0;
            foreach (Messpunkt mp in m_vMP)
            {
                if (mp.Höhe != null)
                    i++;
            }

            return i;
        }

        public void getClosestHeigths(double dHöhe, ref double? min, ref double? max)
        {
            //Liste mit Höhen auffüllen
            List<double> lsHöhen = new List<double>();

            foreach (Messpunkt mp in m_vMPheigth)
            {
                if (!lsHöhen.Contains(mp.Höhe.Value))
                    lsHöhen.Add(mp.Höhe.Value);

            }

            //Testen, ob gesuchte Höhe vorhanden ist
            if (lsHöhen.Contains(dHöhe))
            {
                min = dHöhe;
                max = dHöhe;
            }
            else
            {
                lsHöhen.Sort();

                for (int i = 0; i < lsHöhen.Count; i++)
                {
                    if (lsHöhen[i] > dHöhe)
                    {
                        if (i > 0)
                            min = lsHöhen[i - 1];
                        else
                            min = lsHöhen[i];

                        max = lsHöhen[i];

                        break;
                    }
                }

                //wenn max immer noch NULL -> max = größte Höhe
                if (max == null)
                {
                    min = lsHöhen[lsHöhen.Count - 1];
                    max = min;
                }
            }
        }


        private class lsHöhen
        {
            private List<idHöhe> m_liste = new List<idHöhe>();
            private Dictionary<int, double> m_dict = new Dictionary<int, double>();

            public void insert(int id, double dHöhe)
            {
                idHöhe IDHöhe = new idHöhe(id, dHöhe);
                m_liste.Add(IDHöhe);
            }

            public void sort()
            {
                int n = m_liste.Count;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (m_liste[j].Höhe > m_liste[j + 1].Höhe)
                        {
                            idHöhe temp = new idHöhe(m_liste[j].ID, m_liste[j].Höhe);
                            m_liste[j] = m_liste[j + 1];
                            m_liste[j + 1] = temp;
                        }
                    }
                }

                //sortierte Liste in Dictionary schreiben
                for (int i = 0; i < m_liste.Count; i++)
                {
                    m_dict.Add(m_liste[i].ID, m_liste[i].Höhe);
                }

            }

            //Hmin
            public double getHmin()
            {
                Dictionary<int, double>.Enumerator it = m_dict.GetEnumerator();
                it.MoveNext();
                return it.Current.Value;
            }

            //Hmax
            public double getHmax()
            {
                Dictionary<int, double>.Enumerator it = m_dict.GetEnumerator();
                for (int i = 0; i < m_dict.Count; i++)
                    it.MoveNext();
                return it.Current.Value;
            }

            //Dict
            public Dictionary<int, double> getDictionary
            {
                get { return this.m_dict; }
            }

            private class idHöhe
            {
                private int m_id;
                private double m_dHöhe;

                public idHöhe(int id, double Höhe)
                {
                    m_id = id;
                    m_dHöhe = Höhe;
                }

                public int ID
                {
                    get { return this.m_id; }
                }


                public double Höhe
                {
                    get { return this.m_dHöhe; }
                }

            }
        }

        private ObjectId createMarker()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Transaction tr = db.TransactionManager.StartTransaction();

            ObjectId btrId = new ObjectId();
            string blkName = "Marker";

            // Get the block table from the drawing
            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

            // Check the block name, to see whether it's
            // already in use     
            if (bt.Has(blkName))
                btrId = bt[blkName];
            else
            {
                // Kreis als Geometrie hinzufügen
                Point3d center = new Point3d(0, 0, 0);
                Circle ent = new Circle(center, Vector3d.ZAxis, 0.5);

                // Attribute Definition
                Point3d attPos = new Point3d(0.6, 0, 0);
                AttributeDefinition att1 = new AttributeDefinition(attPos, "", "dH", "", db.Textstyle);
                att1.ColorIndex = 1;
                att1.Height = 1.0;

                // Create our new block table record...
                BlockTableRecord btr = new BlockTableRecord();
                btr.Name = blkName;

                // Add the new block to the block table
                bt.UpgradeOpen();
                btrId = bt.Add(btr);
                tr.AddNewlyCreatedDBObject(btr, true);

                btr.AppendEntity(ent);
                btr.AppendEntity(att1);

                tr.AddNewlyCreatedDBObject(ent, true);
                tr.AddNewlyCreatedDBObject(att1, true);
            }
            tr.Commit();
            return btrId;
        }

        //Marker setzen
        public ErrorStatus setMarker(Point3d pos, string Wert)
        {
            ErrorStatus eS = ErrorStatus.KeyNotFound;

            Database db = HostApplicationServices.WorkingDatabase;
            Transaction tr = db.TransactionManager.StartTransaction();

            using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                try
                {
                    BlockTable bt = (BlockTable)(tr.GetObject(db.BlockTableId, OpenMode.ForWrite));
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    // Create the block reference
                    BlockReference br = new BlockReference(pos, createMarker());
                    Scale3d Skalierung = new Scale3d(0.3);
                    br.ScaleFactors = Skalierung;

                    AttributeReference attRef = new AttributeReference();

                    // Iterate the Marker block and find the attribute definition
                    BlockTableRecord btrMarker = (BlockTableRecord)tr.GetObject(bt["Marker"], OpenMode.ForRead);
                    foreach (ObjectId id in btrMarker)
                    {
                        Entity ent = (Entity)tr.GetObject(id, OpenMode.ForRead, false);
                        // Use it to open the current object! 
                        if (ent is AttributeDefinition)
                        {
                            // Set the properties from the attribute definition on our attribute reference
                            AttributeDefinition attDef = ((AttributeDefinition)(ent));
                            attRef.SetAttributeFromBlock(attDef, br.BlockTransform);
                            attRef.TextString = Wert;
                        }
                    }

                    // Add the reference to ModelSpace
                    btr.AppendEntity(br);
                    // Add the attribute reference to the block reference
                    br.AttributeCollection.AppendAttribute(attRef);
                    // let the transaction know
                    tr.AddNewlyCreatedDBObject(attRef, true);
                    tr.AddNewlyCreatedDBObject(br, true);

                    tr.Commit();
                }
                catch { }
            }

            return eS;
        }
    }   //Blöcke

    /// <summary>
    /// Prototypzeichnung
    /// </summary>
    public partial class Prototyp
    {
        private DocumentCollection m_dm = Application.DocumentManager;
        private Editor m_ed = Application.DocumentManager.MdiActiveDocument.Editor;
        private Database m_destDB = null;
        private Database m_sourceDB = new Database(false, true);
        private Autodesk.AutoCAD.DatabaseServices.TransactionManager m_myTM = null;
        private Transaction m_myT = null;

        private BlockTable m_bt = null;
        private List<BlockReference> m_lsBlockRef = new List<BlockReference>();

        //Konstruktor (damit kein Default Konstruktor generiert wird!)
        protected Prototyp() { }

        //Properties
        public int AnzahlBlöcke
        {
            get { return m_lsBlockRef.Count; }
        }

        public List<BlockReference> getBlockRefList
        {
            get { return m_lsBlockRef; }
        }

        /// <summary>
        /// Blocknamen in Prototypzeichnung abrufen
        /// </summary>
        public List<string> getNames
        {
            get
            {
                List<string> lsNamen = new List<string>();

                foreach (BlockReference blkRef in m_lsBlockRef)
                {
                    lsNamen.Add(blkRef.Name);
                }

                return lsNamen;
            }
        }

        //Methoden
        public ErrorStatus init(string Zeichnungsname)
        {
            ErrorStatus es = ErrorStatus.OK;

            //Datenbank initialisieren
            m_destDB = m_dm.MdiActiveDocument.Database;
            m_myTM = m_sourceDB.TransactionManager;
            m_myT = m_myTM.StartTransaction();

            try
            {
                if (m_sourceDB.Filename != "")
                    m_sourceDB.Dispose();

                using (DocumentLock dl = Application.DocumentManager.MdiActiveDocument.LockDocument())
                {
                    m_sourceDB.ReadDwgFile(Zeichnungsname, FileOpenMode.OpenTryForReadShare, true, "");
                }

                //BlockTable öffnen
                m_bt = (BlockTable)m_myTM.GetObject(m_sourceDB.BlockTableId, OpenMode.ForRead, false);
                List<string> blkName = new List<string>();

                foreach (ObjectId btrId in m_bt)
                {
                    BlockTableRecord btr = (BlockTableRecord)m_myTM.GetObject(btrId, OpenMode.ForRead, false);

                    //nur benannte Blöcke aus dem Modellbereich übernehmen
                    if (!btr.IsAnonymous && !btr.IsLayout)
                    {
                        ObjectIdCollection blockReferences = btr.GetBlockReferenceIds(true, true);

                        foreach (ObjectId blkRefObjId in blockReferences)
                        {
                            DBObject blockRefDbObj = (DBObject)m_myTM.GetObject(blkRefObjId, OpenMode.ForRead);
                            BlockReference blkRef = (BlockReference)blockRefDbObj;

                            if (!blkName.Contains(blkRef.Name))
                            {
                                blkName.Add(blkRef.Name);
                                m_lsBlockRef.Add(blkRef);
                            }
                        }
                    }

                    btr.Dispose();
                }
            }
            catch { es = ErrorStatus.KeyNotFound; }

            return es;
        }

        public BlockReference findBlockReference(string Name)
        {
            foreach (BlockReference br in m_lsBlockRef)
            {
                if (br.Name == Name)
                    return br;
            }

            return null;
        }

        public ObjectId findObjectId(string Name)
        {
            ObjectId blockId = new ObjectId();

            foreach (BlockReference br in m_lsBlockRef)
            {
                if (br.Name == Name)
                    return br.ObjectId;
            }
            return blockId;
        }

        public ErrorStatus importBlock(List<string> lsBlöcke)
        {
            ErrorStatus es = ErrorStatus.KeyNotFound;


            return es;
        }

        public void close()
        {
            try
            {
                m_sourceDB.CloseInput(true);
                m_sourceDB.Dispose();
                m_destDB.CloseInput(false);
                m_myT.Commit();
                m_myT.Dispose();
            }
            catch { }
        }

        //Instanz
        public static Prototyp Instance
        {
            get { return PrototypCreator.createInstance; }
        }

        private sealed class PrototypCreator
        {
            private static readonly Prototyp _Instance = new Prototyp();

            public static Prototyp createInstance
            {
                get { return _Instance; }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

namespace CAS.myAutoCAD
{
    public partial class myUtilities
    {    
        public ErrorStatus convertToDouble(string String, ref double dZahl, int? Zähler)
        {
            ErrorStatus eStatus = ErrorStatus.OutOfRange;

            if (!(String == String.Empty || String == "\r"))
            {
                string sZahl = "";

                //',' gegen '.' tauschen
                String = String.Replace(',', '.');

                //Vorzeichen berücksichtigen
                try
                {
                    if (String[0] == '-')
                        sZahl = "-";
                }

                catch { }

                //alphazeichen aus String entfernen
                for (int i = 0; i < String.Length; i++)
                {
                    if ((String[i] >= '0') && (String[i] <= '9')
                          || String[i] == '.'
                          || String[i] == ',')

                        sZahl += String[i];
                }

                int test = String.Length - sZahl.Length;

                try
                {
                    if (sZahl != String.Empty)
                    {
                        dZahl = Convert.ToDouble(sZahl);
                        eStatus = ErrorStatus.OK;
                    }
                }

                catch (System.FormatException e)
                {
                    System.Windows.Forms.MessageBox.Show(sZahl + " konnte nicht konvertiert werden. (Zeile " + Zähler.ToString()+ ")");
                }
            }

            //MP ohne Höhe
            else
            {
                eStatus = ErrorStatus.NullExtents;
            }

            return eStatus;
        }

        //Zahlenstring
        public int Precision(string String)
        {
            //',' gegen '.' tauschen
            String = String.Replace(',', '.');

           if (!String.Contains("."))
               return 0;    

            String = String.Substring(String.IndexOf('.') + 1);
            string Nachkomma = String.Empty; 

            foreach (char ch in String)
            {
                if ( ch >= 48 && (int) ch <= 57)
                    Nachkomma += ch;
            }

            if (Nachkomma == "0")
                Nachkomma = String.Empty;

            return Nachkomma.Length;
        } 

        //Formatstring
        public string Formatstring(int Precision)
        {
            string Format = "0.";

            for (int i = 0; i < Precision; i++)
                Format += "0";

            return Format;
        }
        
        /// <summary>
        /// Anzahl der gültigen Nachkommastellen ermitteln
        /// </summary>
        /// <param name="Wert"></param>
        /// <returns></returns>
        //public int Precision(string Wert)
        //{
        //    try
        //    {
        //        Wert = Wert.Substring(Wert.LastIndexOf('.'));
        //    }
        //    catch { }

        //    if (Wert[0] == '.')
        //        Wert = Wert.Substring(1);
            
        //    return Wert.Length;
        //}

        ///<summary>
        ///Richtungswinkel
        ///</summary>
        public double RiWi(Point2d pt1, Point2d pt2)
        {
            double dRiWi = 0;
            double dx = pt2.X - pt1.X;
            double dy = pt2.Y - pt1.Y;
            double dPhi = 0;

            if (dx != 0)
            {
                dPhi = Math.Abs(Math.Atan(dx / dy));

                // 1. Quadrant
                if ((dx >= 0) && (dy >= 0))
                    dRiWi = dPhi;

                //2. Quadrant
                if ((dx >= 0) && (dy <= 0))
                    dRiWi = Math.PI - dPhi;

                //3. Quadrant
                if ((dx <= 0) && (dy <= 0))
                    dRiWi = dPhi + Math.PI;


                //4. Quadrant
                if ((dx <= 0) && (dy >= 0))
                    dRiWi = -dPhi;

            }
            else
                if (dy >= 0)
                    dPhi = 0;
                else
                    dPhi = Math.PI;



            return dRiWi;
        }

        ///<summary>
        ///Berechnung der Sehnenlänge
        ///</summary>
        public double calcSehne(double dRadius, double dStich)
        {
            double dSehnenlänge = 2 * Math.Sqrt((dRadius * dRadius) - (dRadius - dStich) * (dRadius - dStich));

            return dSehnenlänge;
        }

        ///<summary>
        ///Bogenkleinpunkte
        ///</summary>
        public List<Point3d> calcBogenKleinpunkte3d(Point2d ptZentrum, double dRadius, Point3d ptAnfang, Point3d ptEnde, double dStich)
        {
            List<Point3d> lsPunkte = new List<Point3d>();
            Point2d ptAnfang2d = new Point2d(ptAnfang.X, ptAnfang.Y);
            Point2d ptEnde2d = new Point2d(ptEnde.X, ptEnde.Y);

            //erforderliche Sehnenlänge berechnen
            myAutoCAD.myUtilities objUtil = new myAutoCAD.myUtilities();
            double dSehne = objUtil.calcSehne(dRadius, dStich);

            //Berechnung Bogenlänge
            double dAbstandSE = ptAnfang2d.GetDistanceTo(ptEnde2d);
            double dPhi = 2 * Math.Asin(dAbstandSE / (2 * dRadius));
            double dBL = dRadius * dPhi;

            //Berechnung der Bogenlänge für Kleinpunkte
            double dPhi1 = 0;

            dPhi1 = 2 * Math.Asin(dSehne / (2 * dRadius));
            double dBL1 = dRadius * dPhi1;

            //Vorzeichen für Phi1 festlegen (je nach Drehsinn)
            double dAlphaStart = objUtil.RiWi(ptZentrum, ptAnfang2d);
            double dAlphaEnde = objUtil.RiWi(ptZentrum, ptEnde2d);

            if (dAlphaStart > dAlphaEnde)
                dPhi1 = -dPhi1;

            //Richtungswinkel
            Vector2d v2dRiWi = ptZentrum.GetVectorTo(ptAnfang2d);
            double dRiWi = v2dRiWi.Angle;
            dRiWi = objUtil.RiWi(ptZentrum, ptAnfang2d);

            //solange Endpunkt nicht erreicht ist, Kleinpunkte berechnen
            double dStation = dBL1;
            double dWinkel = dRiWi;

            //dH für Höheninterpolation
            double dH = (ptEnde.Z - ptAnfang.Z) / dBL * dBL1;
            double dz = 0;

            while (dStation < dBL)
            {
                dWinkel += dPhi1;

                //dx, dy und dz berechnen
                double dx = dRadius * Math.Sin(dWinkel);
                double dy = dRadius * Math.Cos(dWinkel);
                dz += dH;

                //Kleinpunkt berechnen
                Point3d ptPunkt = new Point3d(ptZentrum.X + dx, ptZentrum.Y + dy, ptAnfang.Z + dz);

                //Punkt zu Liste hinzufügen
                lsPunkte.Add(ptPunkt);

                dStation += dBL1;
            }

            return lsPunkte;
        }

        public ErrorStatus getTextStyleId(string Textstil, ref ObjectId TextstilId)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Transaction myT = db.TransactionManager.StartTransaction();

            ErrorStatus es = ErrorStatus.KeyNotFound;

            try
            {
                TextStyleTable tsTbl = (TextStyleTable)myT.GetObject(db.TextStyleTableId, OpenMode.ForRead, true, true);

                foreach (ObjectId objId in tsTbl)
                {
                    TextStyleTableRecord tsTblRec = new TextStyleTableRecord();
                    tsTblRec = (TextStyleTableRecord)myT.GetObject(objId, OpenMode.ForWrite);

                    if (Textstil == tsTblRec.Name)
                    {
                        TextstilId = objId;
                        //m_myT.Commit();
                        break;
                    }
                }

                es = ErrorStatus.OK;
            }

            finally
            {
                myT.Commit();
            }

            return es;
        }

        public string incString(string Wert)
        {
           
            return Wert;
        }
    }

    //icon to imagesource
  //  public static class IconUtilities
    //{
    //    [DllImport("gdi32.dll", SetLastError = true)]

    //    private static extern bool DeleteObject(IntPtr hObject);
    //    public static System.Windows.Media.ImageSource ToImageSource(this System.Drawing.Icon icon)
    //    {
    //        System.Drawing.Bitmap bitmap = icon.ToBitmap();
    //        IntPtr hBitmap = bitmap.GetHbitmap();
    //        ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap,
    //                                             IntPtr.Zero, System.Windows.Int32Rect.Empty,
    //                                             BitmapSizeOptions.FromEmptyOptions());
    //        if (!DeleteObject(hBitmap))
    //        { throw new System.ComponentModel.Win32Exception(); }
    //        return wpfBitmap;
    //    }
    //}
}

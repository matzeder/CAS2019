using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using CAS.myUtilities;
using CAS.myUtilities.myString;

namespace CAS.myFunctions
{
    public class setPoint
    {
        Editor m_ed = null;
        myAutoCAD.myUtilities objUtil = new myAutoCAD.myUtilities();
        myConfig _config = new myConfig();

        private static string PNrZähler = "0";

        //Methods
        public void start()
        {
            string _block = _config.getAppSetting("Block");
            string _Basislayer = _config.getAppSetting("Basislayer");
            m_ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointOptions prPtOpt = new PromptPointOptions("Bitte Position wählen:");
            bool cont = true;

            while(cont)
            {
                PromptPointResult prPtRes = m_ed.GetPoint("Position wählen:");

                if (prPtRes.Status == PromptStatus.OK)
                {
                    PNrZähler = myString.Increment(PNrZähler, mode.AlphaNumeric);
                    PromptStringOptions prStringOpt = new PromptStringOptions("Nr: " + PNrZähler);
                    prStringOpt.AllowSpaces = false;
                    PromptResult prRes = m_ed.GetString(prStringOpt);

                    if (prRes.StringResult != "")
                        PNrZähler = prRes.StringResult;

                    Point2d pt2d = new Point2d(prPtRes.Value.X, prPtRes.Value.Y);
                    CAS.myAutoCAD.Messpunkt MP = new myAutoCAD.Messpunkt(PNrZähler, pt2d);
                    MP.draw(_block, _Basislayer);
                }
                else
                    cont = false;
            }
        }
    }
}

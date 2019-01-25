﻿using System;
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
    public partial class Blöcke
    {
        //Methods


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
        }  

        /// <summary>
        /// Attribute schalten
        /// </summary>
        /// 
        public enum mode
        {
            HeightOff = 1,
            HeightOn = 2,
        }

        public void switchAtt(mode mode)
        {
            switch(mode)
            {
                case mode.HeightOn:
                    foreach(Messpunkt MP in ls_MP)
                    {
                        MP.HeightVisible(true);
                    }

                    break;

                case mode.HeightOff:
                    foreach (Messpunkt MP in ls_MP)
                    {
                        MP.HeightVisible(false);
                    }

                    break;

                default:
                    break;
            }
        }
    }
}
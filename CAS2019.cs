using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;

[assembly: ExtensionApplication(typeof(CAS.CAS2019))]
[assembly: CommandClass(typeof(CAS.CAS2019))]

namespace CAS
{
    public class CAS2019:Autodesk.AutoCAD.Runtime.IExtensionApplication 
    {
        //initialization 
        public void Initialize()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            CAS.myFunctions.DiaSettings objSettings = new myFunctions.DiaSettings();

            /// <summary>
            /// look for updates
            /// </summary>
            /// 
            CAS.myUtilities.update objUpdate = new myUtilities.update();

            ed.WriteMessage("CAS installed");
        }

        public void Terminate() { }

        //pt import
        [CommandMethod("cas_ptImport")]
        public void CasPtImport()
        {
            myFunctions.PtImport objPtImport = new myFunctions.PtImport();
            objPtImport.start();
        }

        //pt export
        [CommandMethod("cas_ptExport")]
        public void CasPtExport()
        {
            myFunctions.PtExport objPtExport = new myFunctions.PtExport();
            objPtExport.run();
        }

        //att Höhe unsichtbar schalten
        [CommandMethod("cas_Hoff")]
        public void CasAtInvisible()
        {
            myAutoCAD.Blöcke.Instance.init();
            myAutoCAD.Blöcke.Instance.selectWindow();
            myAutoCAD.Blöcke.Instance.switchAtt(myAutoCAD.Blöcke.mode.HeightOff);
        }

        //att Höhe sichtbar schalten
        [CommandMethod("cas_Hon")]
        public void CasAtVisible()
        {
            myAutoCAD.Blöcke.Instance.init();
            myAutoCAD.Blöcke.Instance.selectWindow();
            myAutoCAD.Blöcke.Instance.switchAtt(myAutoCAD.Blöcke.mode.HeightOn);
        }

        //Punkte setzen
        [CommandMethod("cas_setPoint")]
        public void CasSetPoint()
        {
            myFunctions.setPoint objSetPoint = new myFunctions.setPoint();
            objSetPoint.start();
        }

        //show Settings
        [CommandMethod("cas_Settings")]
        public void casSettings()
        {
            CAS.myFunctions.DiaSettings objSettings = new myFunctions.DiaSettings();
            objSettings.ShowDialog();
        }

        //shows about box
        [CommandMethod("cas_About")]
        public void CasAbout()
        {
            CASAboutBox objAboutBox = new CASAboutBox();
            objAboutBox.ShowDialog();
        }

        //register CAS
        [CommandMethod("regcas")]
        public void RegisterCas()
        {
            myRegistry.regApp.register();
        }

        //unregister CAS
        [CommandMethod("unregcas")]
        public void UnregisterCas()
        {
            myRegistry.regApp.unregister();
        }
    }
}

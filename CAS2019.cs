using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

[assembly: ExtensionApplication(null)]
[assembly: CommandClass(typeof(CAS.CAS2019))]

namespace CAS
{
    public class CAS2019
    {
        //pt import
        [CommandMethod("cas_ptImport")]
        public void CasPtImport()
        {
            myFunctions.PtImport objPtImport = new myFunctions.PtImport();
            objPtImport.run();
        }

        //pt export
        [CommandMethod("cas_ptExport")]
        public void CasPtExport()
        {
            myFunctions.PtExport objPtExport = new myFunctions.PtExport();
            objPtExport.run();
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
        [CommandMethod("cas_register")]
        public void CasRegister()
        {
            myRegistry.regApp.register();
        }

        //unregister CAS
        [CommandMethod("cas_unregister")]
        public void CasUnregister()
        {
            myRegistry.regApp.unregister();
        }
    }
}

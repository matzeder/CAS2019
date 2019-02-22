using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using _Ac = Autodesk.AutoCAD;
using CadApp = Autodesk.AutoCAD.ApplicationServices.Application;

[assembly: CommandClass(typeof(ShowProgressBar.MyCadCommands))]

namespace ShowProgressBar
{
    public class MyCadCommands
    {
        [CommandMethod("LongWork1")]
        public static void RunLongWork_1()
        {
            Document dwg = CadApp.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            try
            {
                MyCadDataHandler dataHandler = new MyCadDataHandler(dwg);
                dataHandler.DoWorkWithKnownLoopCount();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(
                    "\nError: {0}\n{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                _Ac.Internal.Utils.PostCommandPrompt();
            }
        }

        [CommandMethod("LongWork2")]
        public static void RunLongWork_2()
        {
            Document dwg = CadApp.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            try
            {
                MyCadDataHandler dataHandler = new MyCadDataHandler(dwg);
                dataHandler.DoWorkWithUnknownLoopCount();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(
                    "\nError: {0}\n{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                _Ac.Internal.Utils.PostCommandPrompt();
            }
        }
    }
}

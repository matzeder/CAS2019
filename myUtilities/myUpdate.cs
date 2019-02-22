using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace CAS.myUtilities
{
    public class MyUpdate
    {
        readonly Version _curVersion = Assembly.GetExecutingAssembly().GetName().Version;
        Assembly _assUpd = null;
        Version _updVersion = null;

        public MyUpdate(string updFile)
        {
            try
            {
                _assUpd = Assembly.LoadFile(updFile);
                _updVersion = _assUpd.GetName().Version;

                if (_updVersion.CompareTo(_curVersion) > 0)
                    MessageBox.Show("Update v" + _updVersion.ToString() + " verfügbar!");
            }
            catch { MessageBox.Show("Fehler beim Lesen der update Version: " + updFile); }
        }
    }
}

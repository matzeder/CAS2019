using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace CAS.myUtilities
{
    public sealed class Global
    {
        Assembly _assembly = typeof(CAS2019).Assembly;
        private string _Prototypzeichnung = "CASProto.dwg";

        private static readonly Lazy<Global> lazy =
        new Lazy<Global>(() => new Global());

        //Constructor
        private Global() { }

        //Properties
        public string PrototypFullPath
        {
            get { return Path.GetDirectoryName(_assembly.Location) + "\\"+_Prototypzeichnung; }
        }

        public static Global Instance
        { get { return lazy.Value; } }

        
    }
}

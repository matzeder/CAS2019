using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace CAS.myUtilities
{
    public sealed class MySettings
    {
        private static volatile MySettings instance;
        private static object syncRoot = new object();

        private XmlDocument doc = new XmlDocument();
        private XmlNode myRoot, myNode;
        private XmlNodeList myNodeList;
        private string m_xmlName;

        private string m_Block;
        private string m_Basislayer;
        private int m_Nachkommastellen;

        //Konstruktor
        public MySettings()
        {
            Assembly assem = typeof(CAS2019).Assembly;
            m_xmlName = Path.Combine(Path.GetDirectoryName(assem.Location), "settings.xml");

            try { doc.Load(m_xmlName); }
            catch (System.IO.FileNotFoundException)
            {
                ////Defaulteinstellungen
                //Knoten
                myRoot = doc.CreateElement(assem.GetName().Name.ToString() + "_" + assem.GetName().Version.ToString());
                doc.AppendChild(myRoot);

                //Basislayer
                myNode = doc.CreateElement("Basislayer");
                myNode.InnerText = "MP-P";
                myRoot.AppendChild(myNode);

                //Block
                myNode = doc.CreateElement("Block");
                myNode.InnerText = "MP.dwg";
                myRoot.AppendChild(myNode);

                //Nachkommastellen
                myNode = doc.CreateElement("Nachkommastellen");
                myNode.InnerText = "4";
                myRoot.AppendChild(myNode);

                doc.Save(m_xmlName);
            }

            //Werte auslesen
            //Basislayer
            myNodeList = doc.GetElementsByTagName("Basislayer");
            myNode = myNodeList[0];
            m_Basislayer = myNode.InnerText;

            //Block
            myNodeList = doc.GetElementsByTagName("Block");
            myNode = myNodeList[0];
            m_Block = myNode.InnerText;

            myNodeList = doc.GetElementsByTagName("Nachkommastellen");
            myNode = myNodeList[0];
            m_Nachkommastellen = Convert.ToInt32(myNode.InnerText.ToString());
        }

        //Properties
        public string Basislayer
        {
            get { return m_Basislayer; }
            set
            {
                OnSettingChanged("Basislayer", value);
            }
        }

        public string Block
        {
            get { return m_Block; }
            set
            {
                OnSettingChanged("Block", value);
            }
        }

        public int Nachkommastellen
        {
            get { return m_Nachkommastellen; }
        }

        //Methoden
        public static MySettings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MySettings();
                    }
                }
                return instance;
            }
        }
        public void chgNachkommatellen(int digits)
        {
            myNodeList = doc.GetElementsByTagName("Nachkommastellen");
            myNode = myNodeList[0];
            myNode.InnerText = digits.ToString();
            m_Nachkommastellen = digits;

            doc.Save(m_xmlName);
        }

        //veränderte Konfiguration in xml schreiben
        private void OnSettingChanged(string item, string value)
        {
            doc.Load(m_xmlName);

            myNodeList = doc.GetElementsByTagName(item);
            myNode = myNodeList[0];
            myNode.InnerText = value;

            doc.Save(m_xmlName);
        }
    }
}

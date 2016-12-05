using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SoloProjects.Dudhit.SpaceEngineers.CircleBluePrint
{

    public class MyFileHandler : StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return null;
            }
        }


    }

    [Serializable]
    class BluePrintXml : XDocument, IDisposable
    {
        private string XSD = "xsd";
        private string XSI = "xsi";
        private XNamespace xmlSchema = "http://www.w3.org/2001/XMLSchema";
        private XNamespace xmlSchemaI = "http://www.w3.org/2001/XMLSchema-instance";
        public string Path { get; set; }
        public string SteamUserName { get; set; }
        public string SteamUserId { get; set; }
        public string BluePrintName { get; set; }

        public BluePrintXml()
        {

        }
        
        public void BluePrintToFile()
        {
            using (StreamWriter sw = new StreamWriter(Path))
                {
                    using (StringWriter msw = new MyFileHandler())
                    {
                        this.Save(msw);
                        sw.WriteLine(msw);
                    }
                }
        }
    }
    /*
       
    
       /*
      
           XNamespace ns1 = "http://example.com/ns1";
              string prefix = "pf1";
              string localName = "foo";
              XElement el = new XElement(ns1 + localName, new XAttribute(XNamespace.Xmlns + prefix, ns1));
              Console.WriteLine(el);
         
        ouput:
<pf1:foo xmlns:pf1="http://example.com/ns1" />
        */

    /*
        public BluePrintXml()
        {   
            
            XElement root = new XElement("Definitions");
            root.Add(new XAttribute(XNamespace.Xmlns + XSD, xmlSchema));
            root.Add(new XAttribute(XNamespace.Xmlns + XSI, xmlSchemaI));
            this.Add(root);

        //    root.AddBeforeSelf(xmlDecl());
    
            this.Save(Path);
        } 
            void IDisposable.Dispose()
        {
            this.close();//.Dispose();
        }*/
}

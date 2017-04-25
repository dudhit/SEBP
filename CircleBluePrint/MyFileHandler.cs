using SoloProjects.Dudhit.SpaceEngineers.SEBP.Collection;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using System.Collections.Generic;
using SoloProjects.Dudhit.Utilites;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP
{
    //used to prevent encoding attributes being generated in XML file
    public class MyFileHandler : StringWriter, IDisposable
    {
        bool disposed = false;
        public override Encoding Encoding
        {
            get
            {
                return null;
            }
        }
        #region disposal

        ~MyFileHandler()
        {

            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                disposed = true;

      // Call the base class implementation.
      base.Dispose(disposing);
                }
            

        }
        #endregion

    }

    [Serializable]
  public  class BluePrintXml : XDocument, IDisposable
    {
        //public Delegate Point3D GetPointAtIndex(int i);
        //public Delegate int GetTotalPoints();
      private BlueprintModel basicData;
      private HashSet<Point3D> allPoints;
        private string XSD = "xsd";
        private string XSI = "xsi";

        [NonSerialized]
        private XNamespace xmlSchema = "http://www.w3.org/2001/XMLSchema";
        [NonSerialized]
        private XNamespace xmlSchemaI = "http://www.w3.org/2001/XMLSchema-instance";
        private BlueprintHSV compressedValues;
        private long entityTracked;
        //    private List<Point3D> GridData { get; set; }
        //public Point3D PopulateGrid { set { GridData.Add(value); } }
        public BluePrintXml(BlueprintModel bp, HashSet<Point3D> data)
        {
          this.basicData=bp;
          this.allPoints=data;
            entityTracked = EntityGenerator(18);

            //      GridData = new List<Point3D>();
        }

        private void ConvertModelColoursForThis() 
        
        {
          compressedValues=ColourConverters.ConvertSEFormatHSVtoBluePrintFormat(basicData.BlockColour.H, basicData.BlockColour.S, basicData.BlockColour.V);
        }

        public void BluePrintFileHandling()
        {
            using (StreamWriter sw = new StreamWriter(basicData.BlueprintFilePath))
            {
                using (StringWriter msw = new MyFileHandler())
                {
                    this.Save(msw);
                    sw.WriteLine(msw);
                }
            }
        }

        public void MakeBaseStructure()
        {
            //todo put all attributes into a dictionary

            this.Declaration = new XDeclaration("1.0", null, null);

            /* var result = from el in this.Root.Elements("Definitions")
                          where el.Attribute("id").Value == yourId.ToString()
                          select el;*/
            //    XElement query = (XElement)from c in this.Descendants("Definitions") select c;
            //   List<XElement> result = this.Descendants("Definitions").ToList();
            XElement root = new XElement("Definitions", new XAttribute(XNamespace.Xmlns + XSD, xmlSchema), new XAttribute(XNamespace.Xmlns + XSI, xmlSchemaI));
            this.Add(root);

            XElement lv1 = new XElement("ShipBlueprints");
            XElement lv2 = new XElement("ShipBlueprint", new XAttribute(xmlSchemaI + "type", "MyObjectBuilder_ShipBlueprintDefinition"));
            XElement lv3a = new XElement("Id", new XAttribute("Type", "MyObjectBuilder_ShipBlueprintDefinition"), new XAttribute("Subtype", basicData.BlueprintName));
            XElement lv3b = new XElement("DisplayName",basicData.SteamName);
            XElement lv3c = new XElement("CubeGrids", makeCubeGrid());
            XElement lv3d = new XElement("WorkshopId", 0);
            XElement lv3e = new XElement("OwnerSteamId", basicData.SteamId);
            XElement lv3f = new XElement("Points", 0);
            root.Add(lv1);
            lv1.Add(lv2);
            lv2.Add(lv3a); lv2.Add(lv3b); lv2.Add(lv3c); lv2.Add(lv3d); lv2.Add(lv3e); lv2.Add(lv3f);
            //        foreach (XElement e in result)
            //  { e.Add(new XElement("ShipBlueprints"));}

            //         result = this.Descendants("ShipBlueprints").ToList();


            // foreach (XElement e in result)
            // { e.Add ( new XElement("ShipBlueprint",new XAttribute("xms:type","MyObjectBuilder_ShipBlueprintDefinition"))); }
          //  BluePrintFileHandling();
        }

        private XElement makeCubeGrid()
        {
            XElement setGrid = new XElement("CubeGrid");
            XElement sType = new XElement("SubtypeName");
            XElement entity = new XElement("EntityId", entityTracked);
            XElement pFlags = new XElement("PersistentFlags", "CastShadows InScene");
            XElement gridSize = new XElement("GridSizeEnum", basicData.BlockSize);
            XElement displayName = new XElement("DisplayName", basicData.BlueprintName);
            XElement destructable = new XElement("DestructibleBlocks", "true");
            XElement physics = new XElement("CreatePhysics", "false");
            XElement connections = new XElement("EnableSmallToLargeConnections", "false");
            XElement respwan = new XElement("IsRespawnGrid", "false");
            XElement localPos = new XElement("LocalCoordSys", 0);
            setGrid.Add(sType);
            setGrid.Add(entity);
            setGrid.Add(pFlags);
            setGrid.Add(Positioning());
            setGrid.Add(ComponentContainer());
            setGrid.Add(gridSize);
            setGrid.Add(CubeBlocks());
            setGrid.Add(displayName);
            setGrid.Add(destructable);
            setGrid.Add(physics);
            setGrid.Add(connections);
            setGrid.Add(respwan);
            setGrid.Add(localPos);
            return setGrid;
        }

        private XElement Positioning()
        {
            XElement locations = new XElement("PositionAndOrientation");
            XElement pos = new XElement("Position", new XAttribute("x", "0"), new XAttribute("y", "0"), new XAttribute("z", "0"));
            XElement fwd = new XElement("Forward", new XAttribute("x", "0"), new XAttribute("y", "0"), new XAttribute("z", "-1"));
            XElement up = new XElement("Up", new XAttribute("x", "0"), new XAttribute("y", "1"), new XAttribute("z", "0"));
            XElement ori = new XElement("Orientation");
            locations.Add(pos);
            locations.Add(fwd);
            locations.Add(up);
            locations.Add(ori);
            XElement X = new XElement("X", "0");
            XElement Y = new XElement("Y", "0");
            XElement Z = new XElement("Z", "0");
            XElement W = new XElement("W", "1");
            ori.Add(X); ori.Add(Y); ori.Add(Z); ori.Add(W);
            return locations;
        }

        private XElement ComponentContainer()
        {

            XElement setContainer = new XElement("ComponentContainer");
            XElement components = new XElement("Components");
            XElement componentData = new XElement("ComponentData");
            XElement typeId = new XElement("TypeId", "MyHierarchyComponentBase");
            XElement component = new XElement("Component", new XAttribute(xmlSchemaI + "type", "MyObjectBuilder_HierarchyComponentBase"));
            XElement children = new XElement("Children");
            setContainer.Add(components);
            components.Add(componentData);
            componentData.Add(typeId);
            componentData.Add(component);
            component.Add(children);
            return setContainer;
        }

        private XElement CubeBlocks()
        {
          ConvertModelColoursForThis();
         //   Object writeLock = new Object();
            XElement setCubeBlocks = new XElement("CubeBlocks");
           Parallel.ForEach(allPoints, p =>
            //     Parallel.For(0, PointContainer.Count() + 1, i =>
       //  for (int i = 0; i < PointContainer.Count(); i++)
            {
       
                XElement builder = new XElement("MyObjectBuilder_CubeBlock", new XAttribute(xmlSchemaI + "type", "MyObjectBuilder_CubeBlock"));
                XElement sub = new XElement("SubtypeName", basicData.BlockArmour);
                XElement min = new XElement("Min", new XAttribute("x", p.X), new XAttribute("y", p.Y), new XAttribute("z", p.Z));
                XElement colour = new XElement("ColorMaskHSV", new XAttribute("x", compressedValues.H), new XAttribute("y", compressedValues.S), new XAttribute("z", compressedValues.V));
                builder.Add(sub);
                builder.Add(min);
                builder.Add(colour);
                //     lock (writeLock)
                //    {
                setCubeBlocks.Add(builder);
                //     }
            });
            return setCubeBlocks;
        }

        private long EntityGenerator(int length)
        {
            Random rnd = new Random();
            string joinArray = "";
            for (int i = 0; i < length; i++)
            {
                joinArray += (int)rnd.Next(0, 10);
            }
            return long.Parse(joinArray);
        }

        #region disposal

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BluePrintXml()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources  
                //if (Encoding != null)
                //{
                //    Encoding.Dispose();
                //    Encoding = null;
                //}
            }

        }
        #endregion

    }

    /*
      
        XNamespace ns1 = "http://example.com/ns1";
           string prefix = "pf1";
           string localName = "foo";
           XElement el = new XElement(ns1 + localName, new XAttribute(XNamespace.Xmlns + prefix, ns1));
           Console.WriteLine(el);
         
     ouput:
<pf1:foo xmlns:pf1="http://example.com/ns1" />
     */


}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using System.Globalization;

namespace GISManager
{
    public class GISManager
    {
        ArcGISTiledMapServiceLayer Maplayer;
        GraphicsLayer m_sitelayer, m_realnodelayer, m_reallinklayer;
        Grid m_GISGrid;
        private Draw MyDrawObject;
        private Map m_map;
        private NOCMap m_nocmap;
        private static ESRI.ArcGIS.Client.Projection.WebMercator mercator = new ESRI.ArcGIS.Client.Projection.WebMercator();

        public void StartDrawSiteOnGIS()
        {
            MyDrawObject.DrawMode = DrawMode.Point; 
            MyDrawObject.IsEnabled = true;
        }

        public void StopDrawing()
        {
            MyDrawObject.DrawMode = DrawMode.None;

        }
        public void LoadIn(Grid GISGrid)
        {
            m_GISGrid = GISGrid;
        }

        public void Start()
        {
            m_nocmap = new NOCMap();
            m_map = m_nocmap.m_map;
            m_map.UseAcceleratedDisplay = true;
            m_map.WrapAround = true;

            Maplayer = new ArcGISTiledMapServiceLayer();
            Maplayer.ID = "myfirstlayer";
            Maplayer.Url = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";

            m_map.Layers.Add(Maplayer);

            m_sitelayer = new GraphicsLayer();
            m_map.Layers.Add(m_sitelayer);

            m_realnodelayer = new GraphicsLayer();
            m_map.Layers.Add(m_realnodelayer);


            m_reallinklayer = new GraphicsLayer();
            m_map.Layers.Add(m_reallinklayer);
            m_GISGrid.Children.Add(m_nocmap);

            MyDrawObject = new Draw(m_map);
            MyDrawObject.DrawComplete += MyDrawObject_DrawComplete;

        }
        private void MyDrawObject_DrawComplete(object sender, ESRI.ArcGIS.Client.DrawEventArgs args)
        {
            SimpleMarkerSymbol symbol = new SimpleMarkerSymbol();
            symbol.Color = Brushes.Red;
            symbol.Style = SimpleMarkerSymbol.SimpleMarkerStyle.Circle;
            symbol.Size = 12;

            GraphicsLayer graphicsLayer = m_sitelayer;
            ESRI.ArcGIS.Client.Graphic graphic = new ESRI.ArcGIS.Client.Graphic()
            {
                Geometry = args.Geometry,
                Symbol = symbol
            };
            graphicsLayer.Graphics.Add(graphic);
        }

        public void Stop()
        {
            m_GISGrid.Children.Clear();
            m_map = null;
        }
        public void CreateSite(string site_name, string coorx, string coory)
        {
            SimpleMarkerSymbol symbol = new SimpleMarkerSymbol();
            symbol.Color = Brushes.Red;
            symbol.Style = SimpleMarkerSymbol.SimpleMarkerStyle.Circle;
            symbol.Size = 12;

            string jsonCoordinateString = "{\"Coordinates\":[{\"X\":" + coorx + ",\"Y\":" + coory + "}]}";
            
            CustomCoordinateList coordinateList = DeserializeJson<CustomCoordinateList>(jsonCoordinateString);

            for (int i = 0; i < coordinateList.Coordinates.Count; i++)
            {
                Graphic graphic = new Graphic()
                {
                    Geometry = mercator.FromGeographic(new MapPoint(coordinateList.Coordinates[i].X, coordinateList.Coordinates[i].Y)),
                    Symbol= symbol
                };
                m_sitelayer.Graphics.Add(graphic);
            }

            
        }
        public void CreateRealNode(string node_name, string coorx, string coory)
        {
            SimpleMarkerSymbol symbol = new SimpleMarkerSymbol();
            symbol.Color = Brushes.Blue;
            symbol.Style = SimpleMarkerSymbol.SimpleMarkerStyle.Square;
            symbol.Size = 12;

            string jsonCoordinateString = "{\"Coordinates\":[{\"X\":" + coorx + ",\"Y\":" + coory + "}]}";
            CustomCoordinateList coordinateList = DeserializeJson<CustomCoordinateList>(jsonCoordinateString);
            for (int i = 0; i < coordinateList.Coordinates.Count; i++)
            {
                Graphic graphic = new Graphic()
                {
                    Geometry = mercator.FromGeographic(new MapPoint(coordinateList.Coordinates[i].X, coordinateList.Coordinates[i].Y)),
                    Symbol = symbol
                };
                m_realnodelayer.Graphics.Add(graphic);
            }
        }
        public void CreateRealLinks(string name, string x1, string y1, string x2, string y2)
        {
            SimpleLineSymbol symbol = new SimpleLineSymbol();
            symbol.Color = Brushes.Green;
            symbol.Style = SimpleLineSymbol.LineStyle.Solid;
            symbol.Width = 1;

            string geoRSSLine = @"<?xml version='1.0' encoding='utf-8'?>
                                    <feed xmlns='http://www.w3.org/2005/Atom' xmlns:georss='http://www.georss.org/georss'>
                                    <georss:line>"+x1+", "+y1+","+x2+","+y2+@"</georss:line>
                                </feed>";

            List<ESRI.ArcGIS.Client.Geometry.Polyline> polylineList = new List<ESRI.ArcGIS.Client.Geometry.Polyline>();

            using (System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(new System.IO.StringReader(geoRSSLine)))
            {
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case System.Xml.XmlNodeType.Element:
                            string nodeName = xmlReader.Name;
                            if (nodeName == "georss:line")
                            {
                                string lineString = xmlReader.ReadElementContentAsString();

                                string[] lineCoords = lineString.Split(',');

                                ESRI.ArcGIS.Client.Geometry.PointCollection pointCollection = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                                for (int i = 0; i < lineCoords.Length; i += 2)
                                {
                                    pointCollection.Add(new MapPoint(
                                        Convert.ToDouble(lineCoords[i], CultureInfo.InvariantCulture),
                                        Convert.ToDouble(lineCoords[i + 1], CultureInfo.InvariantCulture)));
                                }

                                ESRI.ArcGIS.Client.Geometry.Polyline polyline = new ESRI.ArcGIS.Client.Geometry.Polyline();
                                polyline.Paths.Add(pointCollection);

                                polylineList.Add(polyline);

                            }
                            break;
                    }
                }
            }


            foreach (ESRI.ArcGIS.Client.Geometry.Polyline polyline in polylineList)
            {

                Graphic graphic = new Graphic()
                {
                     Geometry = mercator.FromGeographic(polyline),
                    Symbol = symbol
                };
                m_reallinklayer.Graphics.Add(graphic);
            }

        }
        internal static T DeserializeJson<T>(string json)
        {
            T objectInstance = Activator.CreateInstance<T>();
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(System.Text.Encoding.Unicode.GetBytes(json));
            System.Runtime.Serialization.Json.DataContractJsonSerializer jsonSerializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(objectInstance.GetType());
            objectInstance = (T)jsonSerializer.ReadObject(memoryStream);
            memoryStream.Close();
            return objectInstance;
        }


       
    }

    [DataContract]
    public class CustomCoordinateList
    {
        [DataMember]
        public List<CustomCoordinate> Coordinates = new List<CustomCoordinate>();
    }

    [DataContract]
    public class CustomCoordinate
    {
        public CustomCoordinate() { }
        public CustomCoordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        [DataMember]
        public double X { get; set; }
        [DataMember]
        public double Y { get; set; }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GISManager
{
    /// <summary>
    /// Interaction logic for NOCMap.xaml
    /// </summary>
    public partial class NOCMap : UserControl
    {
        public NOCMap()
        {
            InitializeComponent();
        }
        private void MyMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs args)
        {
            if (m_map.Extent != null)
            {
                System.Windows.Point screenPoint = args.GetPosition(m_map);
                ScreenCoordsTextBlock.Text = string.Format("Screen Coords: X = {0}, Y = {1}",
                    screenPoint.X, screenPoint.Y);

                ESRI.ArcGIS.Client.Geometry.MapPoint mapPoint = m_map.ScreenToMap(screenPoint);
                if (mapPoint != null)
                {
                    if (m_map.WrapAroundIsActive)
                        mapPoint = ESRI.ArcGIS.Client.Geometry.Geometry.NormalizeCentralMeridian(mapPoint) as ESRI.ArcGIS.Client.Geometry.MapPoint;
                    MapCoordsTextBlock.Text = string.Format("Map Coords: X = {0}, Y = {1}",
                            Math.Round(mapPoint.X, 4), Math.Round(mapPoint.Y, 4));
                }
                else
                    return;

            }
        }

    }
}

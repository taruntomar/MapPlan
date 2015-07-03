using MapsManager.TreeViewControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapsManager
{
    /// <summary>
    /// Interaction logic for MapControlTree.xaml
    /// </summary>
    public partial class MapControlTree : UserControl
    {
        private object m_project =null;//NOCApplication.NOCApp.Project;
        List<HierarchicalObjectViewModel> list;
        public MapControlTree()
        {
            InitializeComponent();
            LoadTree();
        }
        
        private void LoadTree()
        {
            list = new List<HierarchicalObjectViewModel>();
            AddServiceAreaList(list);
            AddSitesList(list);
            tree.ItemsSource = list;
        }

        private void AddServiceAreaList(List<HierarchicalObjectViewModel> list)
        {
            HierarchicalObjectViewModel tmp = new HierarchicalObjectViewModel();
            tmp.Name = "Service Areas";
            tmp.Image = @"..\Images\service_area.png";
            //foreach (projectdataDataSet.SitesRow row in m_project.Sites)
            //{
            //    ccvm = new DataObjectViewModel();
            //    ccvm.Name = row.name;
            //    ccvm.Image = @"..\Images\service_area_obj.png";
            //    tmp.Children.Add(ccvm);
            //}
            list.Add(tmp);
        }

        private void AddSitesList(List<HierarchicalObjectViewModel> list)
        {
            HierarchicalObjectViewModel tmp = new HierarchicalObjectViewModel();
            tmp.Name = "Sites";
            tmp.Image = @"..\Images\sites.png";
            HierarchicalObjectViewModel svm;

            // Create ContextMenu of site
            System.Windows.Controls.ContextMenu cm = new ContextMenu();
            MenuItem itm = new MenuItem() { Header = "Edit" };
            itm.Click += new RoutedEventHandler(Site_EditMenu_Click);
            cm.Items.Add(itm);


            itm = new MenuItem() { Header = "Delete" };
            itm.Click += new RoutedEventHandler(Site_DeleteMenu_Click);
            cm.Items.Add(itm);

            //foreach (projectdataDataSet.SiteTypesRow row in m_project.SiteTypes)
            //{
            //    svm = new HierarchicalObjectViewModel();
            //    svm.Name = row.Name;
            //    //svm.Image = @"..\Images\sites.png";
            //    svm.Image = NOCApplication.NOCApp.CurrentProjectTMPPath + "\\Images\\DataMapping\\" + row.Name + "-Site.png";
            //    svm.ContextMenuObj = cm;
            //    tmp.HierarchicalObjects.Add(svm);

            //    var sites = from site in m_project.Sites where site.type.Equals(Convert.ToString(row.Id)) select site;
            //    HierarchicalObjectViewModel siteviewmodel;
            //    foreach (var site in sites)
            //    {
            //        siteviewmodel = new HierarchicalObjectViewModel();
            //        siteviewmodel.Name = site.name;
            //        siteviewmodel.Image = @"..\Images\sites.png";
            //        siteviewmodel.ContextMenuObj = cm;
            //        svm.HierarchicalObjects.Add(siteviewmodel);
            //    }
            //}
            list.Add(tmp);
        }

        void Site_EditMenu_Click(object sender, RoutedEventArgs e)
        {
        //{
        //   HierarchicalObjectViewModel siteobj = ((HierarchicalObjectViewModel)tree.SelectedItem);
        //   SitePropertyDialog dialog = new SitePropertyDialog((HierarchicalObjectViewModel)tree.SelectedItem);
        //   dialog.ShowDialog();

        //   System.Drawing.Image img =  dialog.CurrentImage;
        //   tree.ItemsSource = null;
        //   img.Save(NOCApplication.NOCApp.CurrentProjectTMPPath + "\\Images\\DataMapping\\" + siteobj.Name + "-Site_tmp.png");
        //   siteobj.Image = NOCApplication.NOCApp.CurrentProjectTMPPath + "\\Images\\DataMapping\\" + siteobj.Name + "-Site_tmp.png";
        //   tree.ItemsSource = list;
        //   //((TreeViewItem) tree.Items[1]).IsExpanded=true;
        }
        void Site_DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
    }
}

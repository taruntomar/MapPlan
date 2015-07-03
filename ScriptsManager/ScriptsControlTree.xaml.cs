using DataModel;
using ScriptsManager.TreeViewControl;
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
using Xceed.Wpf.AvalonDock.Layout;

namespace ScriptsManager
{
    /// <summary>
    /// Interaction logic for ScriptsControlTree.xaml
    /// </summary>
    public partial class ScriptsControlTree : UserControl
    {
        List<HierarchicalObjectViewModel> list;
        
        List<string> m_scriptnames = new List<string>();
        private ProjectManager m_projectManager = null;
        private HierarchicalObjectViewModel m_selectednode;
        private ContextMenu contextmenu;
        private LayoutPanel m_layoutpane;
        private LayoutAnchorablePane m_contentpane=null;
        private LayoutAnchorablePaneGroup m_anchorablepanegroup = null; // this is the pane group in which this script control reside
        Dictionary<string, LayoutDocument> m_openedDocuments = new Dictionary<string, LayoutDocument>();
        public ScriptsControlTree(LayoutPanel layoutpane,ProjectManager projectmanager )
        {
            m_layoutpane = layoutpane;
            m_projectManager = projectmanager;
            contextmenu = CreateContextMenu();
            InitializeComponent();
            //LoadTree();
        }

        private ContextMenu CreateContextMenu()
        {
            ContextMenu cm = new System.Windows.Controls.ContextMenu();
            MenuItem itm = new MenuItem() { Header = "Run" };
            //itm.Click += new RoutedEventHandler(Site_EditMenu_Click);
            cm.Items.Add(itm);
            itm = new MenuItem() { Header = "Open" };
            //itm.Click += new RoutedEventHandler(Site_EditMenu_Click);
            cm.Items.Add(itm);
            
            itm = new MenuItem() { Header = "Delete" };
            //itm.Click += new RoutedEventHandler(Site_EditMenu_Click);
            cm.Items.Add(itm);
            itm = new MenuItem() { Header = "Rename" };
            //itm.Click += new RoutedEventHandler(Site_EditMenu_Click);
            cm.Items.Add(itm);

            return cm;
        }
        private void LoadTree()
        {
            list = new List<HierarchicalObjectViewModel>();
            AddScriptsList(list);
            if(list.Count>0)
            tree.ItemsSource = list;
        }
        

        private void AddScriptsList(List<HierarchicalObjectViewModel> list)
        {
            foreach (Script script in m_projectManager.CurrentProject.Scripts){
                HierarchicalObjectViewModel tmp = new HierarchicalObjectViewModel();
                tmp.Name = script.Name;
                tmp.Image = @"\Icons\DocumentHS.png";
                tmp.TextBlockVisible = Visibility.Visible;
                tmp.TextBoxVisible = Visibility.Collapsed;
                tmp.ContextMenuObj = contextmenu;
                list.Add(tmp);
            }
        }
      

        private string GetNewScriptDefaultName()
        {
            int count = 1;
            StringBuilder defaultName = new StringBuilder("NewScript");
            m_scriptnames.Sort();
            foreach (string scriptname in m_scriptnames)
            {
                if (scriptname == defaultName.ToString())
                {
                    defaultName.Length = 9;
                    defaultName = defaultName.Append(count);
                    count++;
                }
            }
            return defaultName.ToString();
        }

        private void NewScriptCommand(object sender, RoutedEventArgs e)
        {
            if (list == null)
                return;

            HierarchicalObjectViewModel tmp = new HierarchicalObjectViewModel();
            Script tmpscript = new Script();
            tmpscript.Name = GetNewScriptDefaultName();
            tmp.Name = tmpscript.Name;
            tmp.TextBlockVisible = Visibility.Visible;
            tmp.TextBoxVisible = Visibility.Collapsed;
            tmp.Image = @"\Icons\DocumentHS.png";
            tmp.ContextMenuObj = contextmenu;
            list.Add(tmp);
            m_projectManager.CurrentProject.Scripts.InsertOnSubmit(tmpscript);
            m_scriptnames.Add(tmpscript.Name);
            tree.ItemsSource = null; 
            tree.ItemsSource = list;

        }
        private void DeleteScriptCommand(object sender, RoutedEventArgs e)
        {
            if (list == null)
                return;
            m_selectednode = (HierarchicalObjectViewModel)tree.SelectedItem;
            if (m_selectednode == null)
                return;
            list.Remove(m_selectednode);
            m_scriptnames.Remove(m_selectednode.Name);
            //m_scriptnames.BinarySearch(tmp.Name);
            foreach (Script script in m_projectManager.CurrentProject.Scripts)
            {
                if (script.Name == m_selectednode.Name)
                {
                    m_projectManager.CurrentProject.Scripts.DeleteOnSubmit(script);
                    break;
                }
            }
            tree.ItemsSource = null;
            tree.ItemsSource = list;

        }
        private void RunScriptScriptCommand(object sender, RoutedEventArgs e)
        {

        }
        

        
        private void RenameScriptCommand(object sender, RoutedEventArgs e)
        {
           
            m_selectednode = (HierarchicalObjectViewModel)tree.SelectedItem;
            if (list == null || m_selectednode==null)
                return;
            m_selectednode.TextBlockVisible = Visibility.Collapsed;
            m_selectednode.TextBoxVisible = Visibility.Visible;
            tree.ItemsSource = null;
            tree.ItemsSource = list;
            
        }

        private void RenameStart(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.SelectAll();
            tb.Focus();
        }

        private void RenameComplete(object sender, RoutedEventArgs e)
        {

            m_selectednode.TextBlockVisible = Visibility.Visible;
            m_selectednode.TextBoxVisible = Visibility.Collapsed;
            tree.ItemsSource = null;
            tree.ItemsSource = list;
        }

        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            m_selectednode = (HierarchicalObjectViewModel)tree.SelectedItem;
            if (m_selectednode != null)
            {
                DeleteScript_Button.IsEnabled = true;
                RunScript_Button.IsEnabled = true;
                RenameScript_Button.IsEnabled = true;
            }
            else
            {
                DeleteScript_Button.IsEnabled = false;
                RunScript_Button.IsEnabled = false;
                RenameScript_Button.IsEnabled = false;
            }
        }

        private void tree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            m_selectednode = (HierarchicalObjectViewModel)tree.SelectedItem;
            AddPageInDocumentPane(m_selectednode.Name);
        }

        private bool s_isScriptPaneVisible;
        public bool IsScriptPaneVisible
        {
            set
            {
                if(s_isScriptPaneVisible==true && value)
                    return;
                if (value)
                {
                    
                    if (m_layoutpane.Children.Count == 0)
                    {
                        m_anchorablepanegroup = new LayoutAnchorablePaneGroup();
                        m_anchorablepanegroup.Orientation = Orientation.Vertical;
                        m_anchorablepanegroup.DockWidth = new GridLength(150);
                        m_layoutpane.Children.Add(m_anchorablepanegroup);
                    }
                    else 
                    {
                        foreach (ILayoutPanelElement pane in m_layoutpane.Children)
                        {
                            if (pane is LayoutAnchorablePaneGroup)
                            {
                                m_anchorablepanegroup = (LayoutAnchorablePaneGroup)pane;
                                break;
                            }
                        }
                    }
                    if (m_anchorablepanegroup == null)
                    {
                        m_anchorablepanegroup = new LayoutAnchorablePaneGroup();
                        m_anchorablepanegroup.Orientation = Orientation.Vertical;
                        m_anchorablepanegroup.DockWidth = new GridLength(150);
                        m_layoutpane.Children.Add(m_anchorablepanegroup);
                    }

                    m_contentpane = new LayoutAnchorablePane();
                    m_contentpane.Children.Add(new LayoutAnchorable() { Title = "Scripts", Content = this });
                    m_anchorablepanegroup.Children.Add(m_contentpane);
                    s_isScriptPaneVisible = value;

                    LoadTree();
                }
                else
                {
                    // disable script pane
                    m_contentpane.Children.Clear();
                    s_isScriptPaneVisible = false;
                }
            }
            get
            {
                return s_isScriptPaneVisible;
            }
        }

        public void AddPageInDocumentPane(string title)
        {
            if(m_openedDocuments.ContainsKey(title))
            {
                LayoutDocument tmpdoc = m_openedDocuments[title];
                tmpdoc.IsSelected = true;
                return;
            }
            LayoutDocumentPaneGroup m_LayoutDocumentPaneGroup=null;
            foreach (ILayoutElement pane in m_layoutpane.Children)
            {
                if (pane is LayoutDocumentPaneGroup)
                {
                    m_LayoutDocumentPaneGroup =(LayoutDocumentPaneGroup) pane;
                    break;
                }
            }
            if (m_LayoutDocumentPaneGroup == null)
            {
                m_LayoutDocumentPaneGroup = new LayoutDocumentPaneGroup();
                m_layoutpane.Children.Add(m_anchorablepanegroup);
            }
            LayoutDocumentPane layoutpane = null;
            if (m_LayoutDocumentPaneGroup.Children.Count == 0)
            {
                layoutpane = new LayoutDocumentPane();
                m_LayoutDocumentPaneGroup.Children.Add(layoutpane);
            }
            else
            {
                layoutpane = (LayoutDocumentPane)m_LayoutDocumentPaneGroup.Children[0];
            }
            LayoutDocument document = new LayoutDocument() { Content = new TextBox() { AcceptsReturn = true, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, VerticalScrollBarVisibility = ScrollBarVisibility.Auto}, Title = title };
            document.Closed += document_Closed;
            layoutpane.Children.Add(document);
            m_openedDocuments.Add(title,document);
            document.IsSelected = true;

        }

        void document_Closed(object sender, EventArgs e)
        {
            LayoutDocument doc = ( LayoutDocument)sender;
            m_openedDocuments.Remove(doc.Title);
        }

        private void SaveScript_click(object sender, RoutedEventArgs e)
        {
            if(m_openedDocuments.ContainsKey(m_selectednode.Name))
            {
                LayoutDocument tmpdoc = m_openedDocuments[m_selectednode.Name];
                m_projectManager.SaveScript(m_selectednode.Name, ((TextBox)(tmpdoc.Content)).Text);  
            }
            
        }
    }
}

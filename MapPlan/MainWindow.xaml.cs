using ScriptsManager;
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
using DataModel;
using Microsoft.Win32;

namespace MapPlan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GISManager.GISManager m_gismanager;
        private ScriptsControlTree m_ScriptsControlTree = null;
        

        private ProjectManager m_projectmanager;
        private LayoutDocumentPaneGroup m_LayoutDocumentPaneGroup = null;
        private LayoutDocumentPane layoutDocumentpane = null;
        private LayoutDocument m_Layoutdocument = null;

        public MainWindow()
        {
            InitializeComponent();
            m_projectmanager = new ProjectManager();
            m_ScriptsControlTree = new ScriptsControlTree(m_LayoutPanel, m_projectmanager);
            m_gismanager = new GISManager.GISManager();
            ToggleStartPage(null, null);
            //m_gismanager.LoadIn(GISGrid);
        }

        private void NewCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            //NewProject_Click(null, null);
        }
        private void OpenCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SaveCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SaveAllCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenProject()
        {
            m_projectmanager.OpenProject("c:\asdasd.mpn");
            // enable all controls
        }
        private void SaveProject()
        {
            m_projectmanager.SaveProject();

        }
        private void SaveAsProject()
        {
            m_projectmanager.SaveProject("c:\asdasd.mpn");

        }

        private void ToggleScripts(object sender, RoutedEventArgs e)
        {
            if (m_ScriptsControlTree.IsScriptPaneVisible)
                m_ScriptsControlTree.IsScriptPaneVisible = false;
            else
                m_ScriptsControlTree.IsScriptPaneVisible = true;
        }
        private void ToggleMaps(object sender, RoutedEventArgs e)
        {
            if (m_ScriptsControlTree.IsScriptPaneVisible)
                m_ScriptsControlTree.IsScriptPaneVisible = false;
            else
                m_ScriptsControlTree.IsScriptPaneVisible = true;
        }
        private void ToggleCustomers(object sender, RoutedEventArgs e)
        {

        }
        private void ToggleNodes(object sender, RoutedEventArgs e)
        {

        }
        private void ToggleLinks(object sender, RoutedEventArgs e)
        {

        }
        private void ToggleServices(object sender, RoutedEventArgs e)
        {

        }
        private void ToggleConnections(object sender, RoutedEventArgs e)
        {

        }
        private void ToggleStartPage(object sender, RoutedEventArgs e)
        {
            if (IsStartPageVisible)
                IsStartPageVisible = false;
            else
                IsStartPageVisible = true;
        }

        private bool s_isStartPageVisible;
        public bool IsStartPageVisible
        {
            set
            {
                if (s_isStartPageVisible == true && value)
                    return;
                if (value)
                {
                    foreach (ILayoutElement pane in m_LayoutPanel.Children)
                    {
                        if (pane is LayoutDocumentPaneGroup)
                        {
                            m_LayoutDocumentPaneGroup = (LayoutDocumentPaneGroup)pane;
                            break;
                        }
                    }
                    if (m_LayoutDocumentPaneGroup == null)
                    {
                        m_LayoutDocumentPaneGroup = new LayoutDocumentPaneGroup();
                        m_LayoutPanel.Children.Add(m_LayoutDocumentPaneGroup);
                    }

                    if (m_LayoutDocumentPaneGroup.Children.Count == 0)
                    {
                        layoutDocumentpane = new LayoutDocumentPane();
                        m_LayoutDocumentPaneGroup.Children.Add(layoutDocumentpane);
                    }
                    else
                    {
                        layoutDocumentpane = (LayoutDocumentPane)m_LayoutDocumentPaneGroup.Children[0];
                    }
                    m_Layoutdocument = new LayoutDocument() { Content = new StartPage(this), Title = "Start Page" };
                    //document.Closed += document_Closed;
                    layoutDocumentpane.Children.Add(m_Layoutdocument);
                    m_Layoutdocument.IsSelected = true;
                    s_isStartPageVisible = value;
                }
                else
                {
                    // disable StartPage
                    layoutDocumentpane.Children.Clear();
                    s_isStartPageVisible = false;
                }
            }
            get
            {
                return s_isStartPageVisible;
            }
        }

        public void NewProject_Click(object sender, RoutedEventArgs e)
        {
            // invoke new project dialog
            NewProjectDialog dlg = new NewProjectDialog();
            dlg.ShowDialog();
            if (!dlg.m_createproject)
                return;
            m_projectmanager.NewProject(dlg.ProjectName, dlg.ProjectLocation);

            // enable scriptpane
            m_ScriptsControlTree.IsScriptPaneVisible = true;
        }
        public void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MPN Project File|*.mpn";
            bool? x = dlg.ShowDialog();
            if (x.HasValue && x.Value)
            {
                if(!m_projectmanager.OpenProject(dlg.FileName))
                {
                    global::System.Windows.Forms.MessageBox.Show("Project file is not valid.");
                    return;
                }
                m_ScriptsControlTree.IsScriptPaneVisible = true;
            }
        }
    }
}

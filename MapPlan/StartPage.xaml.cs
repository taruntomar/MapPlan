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

namespace MapPlan
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Grid
    {
        private MainWindow m_mainwindow=null;
        
        public StartPage(MainWindow mainwindow)
        {
            InitializeComponent();
            m_mainwindow = mainwindow;
        }

     
        private void Link_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb =  (TextBlock)sender;
            tb.TextDecorations = TextDecorations.Underline;
            tb.Cursor = Cursors.Hand;
        }

        private void Link_MouseLeave(object sender, MouseEventArgs e)
        {
            
            TextBlock tb = (TextBlock)sender;
            tb.TextDecorations = null;
            tb.Cursor = Cursors.Arrow;
        }

   
        private void NewProjectLinkMouseClicked(object sender, MouseButtonEventArgs e)
        {
            m_mainwindow.NewProject_Click(null, null);
        }

        private void OpenProjectLinkMouseClicked(object sender, MouseButtonEventArgs e)
        {
            m_mainwindow.OpenProject_Click(null, null);
        }
    
    }
}

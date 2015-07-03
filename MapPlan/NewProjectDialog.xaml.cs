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

namespace MapPlan
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : Window
    {
        public bool m_createproject = false;
        public string ProjectLocation { get; set; }
        public string ProjectName { get; set; }
        public NewProjectDialog()
        {
            InitializeComponent();

            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            m_projectlocationtextbox.Text= path+@"\MapPlan Projects\";

            m_projectnametextbox.Focus();
            m_projectnametextbox.SelectionStart = 0;
            m_projectnametextbox.SelectionLength = m_projectnametextbox.Text.Length;
            m_projectnametextbox.SelectAll();

        }

     
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    

        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectLocation = m_projectlocationtextbox.Text;
            ProjectName = m_projectnametextbox.Text;
            m_createproject = true;
            CancelButton_Click(null, null);
        }
    }
}

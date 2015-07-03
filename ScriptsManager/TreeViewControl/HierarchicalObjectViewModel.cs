using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ScriptsManager.TreeViewControl
{
    public class HierarchicalObjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }

        public Visibility TextBlockVisible { get; set; }
        public Visibility TextBoxVisible { get; set; }
        public ContextMenu ContextMenuObj { get; set; }
        public ObservableCollection<HierarchicalObjectViewModel> HierarchicalObjects { get; set; }
        public ObservableCollection<DataObjectViewModel> DataObjects { get; set; }

        public HierarchicalObjectViewModel()
        {
            HierarchicalObjects = new ObservableCollection<HierarchicalObjectViewModel>();
            DataObjects = new ObservableCollection<DataObjectViewModel>();
        }
        public IEnumerable Items
        {
            get
            {
                var items = new CompositeCollection();
                items.Add(new CollectionContainer { Collection = HierarchicalObjects });
                items.Add(new CollectionContainer { Collection = DataObjects });
                return items;
            }
        }
    }
}

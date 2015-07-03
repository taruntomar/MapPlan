using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MapsManager.TreeViewControl
{
    public class DataObjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ContextMenu ContextMenuObj { get; set; }
    }
}

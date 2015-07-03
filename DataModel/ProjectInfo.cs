using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Serializable()]
    public class ProjectInfo
    {
        public string ProjectName { get; set; }
        public string ProjectFileVersion { get; set; }
        public string Owner { get; set; }

        public string DatabaseFilePath { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
     
        public string MapsPath { get; set; }

        public List<string> Maps { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLibrary
{
    public class DBManager
    {
        private string m_templocation = null;

        public void DBManager()
        {
            m_templocation = Path.GetTempPath();

        }

        public void CreateDatabase(string p)
        {

        }
    }
}

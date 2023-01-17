using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAddon.Models
{
  public  class ParentTable
    {
        public string TableName { get; set; }

        public List<InitialValue> Values { get; set; }

        public List<ChildTable> ChildrenTables { get; set; }
        //public Table HeaderTable { get; set; } 

        //public List<Table> RowsTables { get; set; }
    }
}

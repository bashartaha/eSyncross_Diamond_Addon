using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAddon.Models
{
  public  class Table
    {
        public Table(string tableName, string tableDescription )
        {
            TableName = tableName;
            TableDescription = tableDescription; 
        }


        public string TableName { get; set; }

        public string TableDescription { get; set; }
        public SAPbobsCOM.BoUTBTableType TableType { get; set; }


        public List<UserDefinedField> FieldsList { get; set; }
 
    }
}

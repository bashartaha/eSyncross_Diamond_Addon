using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace DiamondAddon.Models
{
   public class UserDefinedField
    {
        public UserDefinedField(string table, string fieldCode, string fieldName, BoFieldTypes types, BoFldSubTypes subType, int size, string[,] validValues, string defaultValue, string linkedTable)
        {
            Table = table;
            FieldCode = fieldCode;
            FieldName = fieldName;
            Types = types;
            SubType = subType;
            Size = size;
            ValidValues = validValues;
            DefaultValue = defaultValue;
            LinkedTable = linkedTable;
            
        }


     

        public string Table { get; set; }
        public string FieldCode { get; set; }
        public string FieldName { get; set; }
        public SAPbobsCOM.BoFieldTypes Types { get; set; }
        public SAPbobsCOM.BoFldSubTypes SubType { get; set; }
        public int Size { get; set; }
        public string[,] ValidValues { get; set; }
        public string DefaultValue { get; set; }

        public string LinkedTable { get; set; }
    

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace DiamondAddon.Models
{
    public class UserDefinedObject
    { 
        public Table Table { get; set; }
        public BoUDOObjType ObjType { get; set; }
        public BoYesNoEnum AllowCancel { get; set; } = BoYesNoEnum.tNO;
        public BoYesNoEnum AllowClose { get; set; } = BoYesNoEnum.tNO;
        public BoYesNoEnum AllowCreateDafultForm { get; set; } = BoYesNoEnum.tNO;
        public BoYesNoEnum EnableEnhancedForm { get; set; } = BoYesNoEnum.tNO;
        public string[] FormsColumns { get; set; }

        public BoYesNoEnum AllowDelete { get; set; } = BoYesNoEnum.tNO;
        public BoYesNoEnum AllowFind { get; set; } = BoYesNoEnum.tNO;
        public string[] FindColumns { get; set; }

        public BoYesNoEnum MenuItem { get; set; } = BoYesNoEnum.tNO; 
        public string MenuCaption { get; set; }
        public int FatherMenuID { get; set; }
        public int Position { get; set; }
        public string MenuUID { get; set; }
        public BoYesNoEnum CanLog { get; set; } = BoYesNoEnum.tNO;
        public List<Table> Childs;




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAddon.Models
{
  public  class ChildTable
    {
 

        public string TableName { get; set; }         

        public List<InitialValue> ChildValues { get; set; }
    }
}

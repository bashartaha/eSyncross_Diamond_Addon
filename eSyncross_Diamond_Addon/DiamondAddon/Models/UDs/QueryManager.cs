using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondAddon.Models
{
  public  class QueryManager
    {

        public Category Category { get; set; }

        public List<Query> Queries { get; set; }

    }

    public class Category
    {
        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

    }

    public class Query
    {
        public Query(string queryName, string queryString, string formID, string itemID, string colID,string refreshBy)
        {
            QueryName = queryName;
            QueryString = queryString;
            FormID = formID;
            ItemID = itemID;
            ColID = colID;
            RefreshBy = refreshBy;
           
        }

        public string QueryName { get; set; }
        public int QueryId { get; set; }
        public string QueryString { get; set; }


        public string FormID { get; set; }
        public string ItemID { get; set; }
        public string ColID { get; set; }
        public string RefreshBy { get; set; }
    }

  
}

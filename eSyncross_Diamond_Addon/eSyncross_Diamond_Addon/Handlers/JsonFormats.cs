using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond_Addon.Handlers
{
    class JsonFormats
    {
        public static string getJsonString(object obj )
        {
            return  JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
        }
    }
}

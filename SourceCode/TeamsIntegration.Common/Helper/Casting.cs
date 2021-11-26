using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TeamsIntegration.Common.Helper
{
    public static class Casting
    {
        //casting object to another object using Newtonsoft.Json
        public static T Cast<T>(this Object OriginalObject)
        {
            //serialize original object           
            string SerializedOriginalObject = JsonConvert.SerializeObject(OriginalObject, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            //deserialize the original object string to the target object 
            T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(SerializedOriginalObject);
            return result;
        }
    }
}

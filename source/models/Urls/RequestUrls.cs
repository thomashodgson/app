using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace models.Urls
{
    public class RequestUrls
    {
        public const string Hello = "api/queue/Hello";
        public const string Bye = "api/queue/Bye";
        public const string LogIn = "api/account/LogIn";
        public const string LogOut = "api/account/LogOut";

        public static IEnumerable<string> AllUrls
        {
            get
            {
                return typeof (RequestUrls).GetFields(BindingFlags.Public | BindingFlags.Static |
                                                      BindingFlags.FlattenHierarchy)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(x => x.GetRawConstantValue() as string);
            } 
        }
    }
}

using Microsoft.Analytics.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Extensions
{
    public static string ToEdmString<T>(this T t) where T : Type
    {
        if (t == typeof(System.String)) return "Edm.String";
        if (t == typeof(System.Int32)) return "Edm.Int32";
        if (t == typeof(System.Int16)) return "Edm.Int16";
        if (t == typeof(System.Int64)) return "Edm.Int64";
        if (t == typeof(System.DateTime)) return "Edm.DateTime";
        if (t == typeof(System.Guid)) return "Edm.Guid";
        if (t == typeof(System.Boolean)) return "Edm.Boolean";
        return "Edm.FixMe";
    }

    public static dynamic ToExpando(this IRow row)
    {
        var obj = new ExpandoObject();
        ((IDictionary<string, object>)obj)["searchaction"] = "upload";
        row.Schema.ToList().ForEach(x =>
        {
            ((IDictionary<string, object>)obj)[x.Name] = row.Get<object>(x.Name);
        });
        
        
        return obj;
    }
}



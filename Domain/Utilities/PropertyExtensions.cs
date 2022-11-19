using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Utilities;

public static class PropertyExtensions
{
    public static string ToDisplay(this object obj,string propName)
    {
        MemberInfo property = obj.GetType().GetProperty(propName);
        var atts = property.GetCustomAttributes(typeof(DisplayAttribute), true);
        if (atts.Length == 0)
            return null;
        return (atts[0] as DisplayAttribute).Name;
    }
}

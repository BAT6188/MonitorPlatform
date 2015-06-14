using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Utility
{
    public static class ExceptionSafe
    {
        public static string GetText(XmlNode node)
        {
            if (node != null)
            {
                return node.InnerText;
            }
            return "";
        }

        public static string SafeInnerText(this XmlNode node)
        {
            return GetText(node);
        }
        public static int SafeInnerInt(this XmlNode node)
        {
            return GetInt(node);
        }

        public static int GetInt(XmlNode node)
        {
            int ret = 0;
            if (node != null)
            {
                int.TryParse(node.InnerText, out ret);
            }
            return ret;
        }
    }
}

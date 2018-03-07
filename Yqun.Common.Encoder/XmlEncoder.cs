using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace Yqun.Common.Encoder
{
    public class XmlEncoder
    {
        public static string XMLPattern(string tag)
        {
            return ("<" + tag + @">.*</" + tag + ">");
        }

        public static ArrayList GetElementsByTag(string xml, string tag)
        {
            ArrayList al = new ArrayList();
            Regex reg = new Regex(XMLPattern(tag), RegexOptions.Compiled | RegexOptions.IgnoreCase |
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);
            Regex tagreg = new Regex("</?" + tag + ">");
            MatchCollection matches = reg.Matches(xml);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    al.Add(tagreg.Replace(match.Value, ""));
                }
            }
            return al;
        }
    }
}

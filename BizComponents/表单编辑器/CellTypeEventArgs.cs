using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread;
using System.Drawing;

namespace BizComponents
{
    public class CellTypeEventArgs : EventArgs
    {
        BaseCellType _cellType;
        public BaseCellType CellType
        {
            get
            {
                return _cellType;
            }
            set
            {
                _cellType = value;
            }
        }

        Cell _cell;
        public Cell ActiveCell
        {
            get
            {
                return _cell;
            }
            set
            {
                _cell = value;
            }
        }
    }

    public class ExcelsEventArgs : EventArgs
    {
        List<SheetView> _Sheets = new List<SheetView>();
        public List<SheetView> Sheets
        {
            get
            {
                return _Sheets;
            }
        }
    }

    public class FontFactory
    {
        readonly public static FontFactory Instance = new FontFactory();
        readonly static Dictionary<string, float> FontSize = new Dictionary<string, float>();
        readonly static List<string> FontName = new List<string>();

        static FontFactory()
        {
            FontFamily[] families = FontFamily.Families;
            Font familiesFont;
            foreach (FontFamily family in families)
            {
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    familiesFont = new Font(family, 10F);
                    FontName.Add(familiesFont.Name);
                    familiesFont.Dispose();
                }
            }

            FontSize.Add("初号", 42);
            FontSize.Add("小初", 36);
            FontSize.Add("一号", 26.25f);
            FontSize.Add("小一", 24);
            FontSize.Add("二号", 21.75f);
            FontSize.Add("小二", 18);
            FontSize.Add("三号", 15.75f);
            FontSize.Add("小三", 15);
            FontSize.Add("四号", 14.25f);
            FontSize.Add("小四", 12);
            FontSize.Add("五号", 10.5f);
            FontSize.Add("小五", 9);
            FontSize.Add("六号", 7.5f);
            FontSize.Add("小六", 6.75f);
            FontSize.Add("七号", 5.25f);
            FontSize.Add("八号", 5.20f);
            FontSize.Add("8", 8.25f);
            FontSize.Add("9", 9);
            FontSize.Add("10", 9.75f);
            FontSize.Add("11", 11.25f);
            FontSize.Add("12", 12);
            FontSize.Add("14", 14.25f);
            FontSize.Add("16", 15.75f);
            FontSize.Add("18", 18);
            FontSize.Add("20", 20.25f);
            FontSize.Add("22", 21.75f);
            FontSize.Add("24", 24);
            FontSize.Add("26", 26.25f);
            FontSize.Add("28", 27.75f);
            FontSize.Add("36", 36);
            FontSize.Add("48", 48);
            FontSize.Add("72", 72);
        }

        private FontFactory()
        {
        }

        public string[] FontNames()
        {
            return FontName.ToArray();
        }

        public float GetSize(string Name)
        {
            if (FontSize.ContainsKey(Name))
            {
                return FontSize[Name];
            }

            return FontSize["小五"];
        }

        public string[] Sizes()
        {
            List<string> Keys = new List<string>(FontSize.Keys);
            return Keys.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;

namespace BizCommon
{
    /// <summary>
    /// 由多个SheetConfiguration和一个TableDefineInfo组成
    /// </summary>
    [Serializable]
    public class ModuleConfiguration
    {
        public static ModuleConfiguration Empty = new ModuleConfiguration();

        public ModuleConfiguration()
        {
            m_Sheets = new List<SheetConfiguration>();
            Index = Guid.NewGuid().ToString();
        }

        string m_Index;
        public string Index
        {
            get
            {
                return m_Index;
            }
            set
            {
                m_Index = value;
                m_ExtentDataSchema = new TableDefineInfo();
                m_ExtentDataSchema.Name = "biz_norm_extent_" + value;
                m_ExtentDataSchema.Description = "Extent_" + value;
            }
        }

        string m_Code;
        public string Code
        {
            get
            {
                return m_Code;
            }
            set
            {
                m_Code = value;
            }
        }

        string m_Description;
        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        List<SheetConfiguration> m_Sheets;
        public List<SheetConfiguration> Sheets
        {
            get
            {
                return m_Sheets;
            }
        }

        TableDefineInfo m_ExtentDataSchema;
        public TableDefineInfo ExtentDataSchema
        {
            get
            {
                return m_ExtentDataSchema;
            }
            set
            {
                m_ExtentDataSchema = value;
            }
        }

        public String GetUniqueDescription()
        {
            string ModuleName = "新建表单";
            string tempModuleName = ModuleName;
            Boolean Have = true;
            int Index = 1;

            while (Have)
            {
                Have = false;
                foreach (SheetConfiguration Sheet in Sheets)
                {
                    if (tempModuleName == Sheet.Description)
                    {
                        Have = true;
                        break;
                    }
                }

                if (Have)
                {
                    tempModuleName = ModuleName + "_" + (Index++).ToString();
                }
            }

            return tempModuleName;
        }

        public Boolean ContainSheet(String Index)
        {
            Boolean Have = false;
            foreach (SheetConfiguration Sheet in Sheets)
            {
                if (Index == Sheet.Index)
                {
                    Have = true;
                    break;
                }
            }

            return Have;
        }

        public Boolean ContainSheet(SheetConfiguration Sheet)
        {
            return ContainSheet(Sheet.Index);
        }
    }
}

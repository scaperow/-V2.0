using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BizCommon;

namespace BizComponents
{
    public class DataNULL
    {
        public static Boolean IsNULL(DataSet Data, List<ModuleField> modelFields)
        {
            Boolean r = true;
            String[] Items = new string[]{"自检","平行","见证"};

            foreach(ModuleField modelField in modelFields)
            {
                if (modelField.IsSystem)
                {
                    String Type = modelField.ContentFieldType;
                    String Table = string.Format("[{0}]", modelField.TableName);
                    if (Data.Tables[Table].Rows.Count > 0)
                    {
                        String Column = modelField.Contents;
                        object value = Data.Tables[Table].Rows[0][Column];
                        Type DataType = value.GetType();
                        if (Type.Equals("下拉框"))
                        {
                            int Index = Array.IndexOf(Items, value.ToString());
                            r = r && (Index >= 0);
                        }
                        else if (Type.Equals("文本"))
                        {
                            r = r && (value != DBNull.Value && value.ToString().Trim() != "");
                        }
                    }
                }
            }

            return !r;
        }
    }
}

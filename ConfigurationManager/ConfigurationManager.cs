using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;

namespace ConfigurationManager
{
    public class ConfigurationManager
    {
        private static Hashtable _AppSettings;
        public static Hashtable AppSettings
        {
            get
            {
                if (_AppSettings == null)
                {
                    _AppSettings = new Hashtable();

                    try
                    {
                        String FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration.xml");
                        if (File.Exists(FileName))
                        {
                            DataSet dataset = new DataSet("ItemCollection");
                            dataset.ReadXml(FileName);
                            if (dataset.Tables.Count > 0)
                            {
                                DataTable datatable = dataset.Tables[0];
                                foreach (DataRow row in datatable.Rows)
                                {
                                    if (!_AppSettings.ContainsKey(row["Name"].ToString()))
                                    {
                                        _AppSettings.Add(row["Name"].ToString(), row["Value"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    catch
                    { }
                }

                return _AppSettings;
            }
        }
    }
}

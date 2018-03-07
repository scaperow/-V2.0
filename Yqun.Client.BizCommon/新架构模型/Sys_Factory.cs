using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BizCommon
{
    [Serializable]
    public class Sys_Factory
    {
        public Guid FactoryID { set; get; }
        public string FactoryName { set; get; }
        public string Address { set; get; }
        public string LinkMan { set; get; }
        public string Telephone { set; get; }
        public decimal Longitude { set; get; }
        public decimal Latitude { set; get; }
        public int Status { set; get; }
        public string Remark { set; get; }
        public DateTime CreateTime { set; get; }

        public static Sys_Factory Parse(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            var factory = new Sys_Factory()
            {
                FactoryID = new Guid(Convert.ToString(row["FactoryID"])),
                Address = Convert.ToString(row["Address"]),
                CreateTime = Convert.ToDateTime(row["CreateTime"]),
                FactoryName = Convert.ToString(row["FactoryName"]),
                Latitude = Convert.ToDecimal(row["Latitude"]),
                LinkMan = Convert.ToString(row["LinkMan"]),
                Longitude = Convert.ToDecimal(row["Longitude"]),
                Remark = Convert.ToString(row["Remark"]),
                Status = Convert.ToInt32(row["Status"]),
                Telephone = Convert.ToString(row["Telephone"])
            };

            return factory;
        }
    }
}

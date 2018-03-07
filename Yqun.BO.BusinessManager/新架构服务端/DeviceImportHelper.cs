using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using Newtonsoft.Json;
using System.IO;
using System.ServiceModel;
using TransferServiceCommon;
using System.Data;
using System.Collections;

namespace Yqun.BO.BusinessManager
{
    public class DeviceImportHelper : BOBase
    {

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int Offset = 6;
        private const string Template = "{\"ID\":\"{#id}\",\"Sheets\":[{\"ID\":\"050afdcc-8438-4e63-a55b-85c60001950c\",\"Name\":\"试验室仪器设备登记表\",\"Cells\":[{\"Name\":\"B5\",\"Value\":\"{#b5}\"},{\"Name\":\"B6\",\"Value\":\"{#b6}\"},{\"Name\":\"B7\",\"Value\":\"{#b7}\"},{\"Name\":\"B8\",\"Value\":null},{\"Name\":\"B9\",\"Value\":\"{#b9}\"},{\"Name\":\"C11\",\"Value\":\"{#c11}\"},{\"Name\":\"G5\",\"Value\":\"{#g5}\"},{\"Name\":\"G6\",\"Value\":\"{#g6}\"},{\"Name\":\"G7\",\"Value\":\"{#g7}\"},{\"Name\":\"G8\",\"Value\":null},{\"Name\":\"G9\",\"Value\":\"{#g9}\"},{\"Name\":\"H11\",\"Value\":\"{#h11}\"},{\"Name\":\"K10\",\"Value\":null},{\"Name\":\"K11\",\"Value\":\"{#k11}\"},{\"Name\":\"K5\",\"Value\":\"{#k5}\"},{\"Name\":\"K6\",\"Value\":\"{#k6}\"},{\"Name\":\"K7\",\"Value\":\"{#k7}\"},{\"Name\":\"K8\",\"Value\":null},{\"Name\":\"K9\",\"Value\":\"{#k9}\"}]}]}";
        private const string SummarySheetID = "C472C1DB-0066-4054-96E9-51EA2BF67C75";
        private const string DeviceSummaryID = "BA23C25D-7C79-4CB3-A0DC-ACFA6C285295";
        private const string DeviceRegistrationID = "A0C51954-302D-43C6-931E-0BAE2B8B10DB";
        private const string Mapping = "b,b5,c,g5,d,k5,e,b6,f,g6,g,k6,h,b7,i,g7,j,k7,k,b9,l,g9,m,k9,n,c11,o,h11,p,k11";
        private OpenUpHelper Openup = new OpenUpHelper();

        public void ConvertDevice()
        {
            var map = Mapping.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var sql = "SELECT ID, ModuleSetting FROM sys_module WHERE ID = '" + DeviceRegistrationID + "'";
            var table = GetDataTable(sql);
            if (table == null || table.Rows.Count == 0)
            {
                Logger.Error("没有获取台账设置的数据");
                return;
            }

            var setting = Convert.ToString(table.Rows[0]["ModuleSetting"]);
            var ms = JsonConvert.DeserializeObject<List<ModuleSetting>>(setting);
            if (ms == null)
            {
                Logger.Error("从台账设置转换出来的对象为空");
                return;
            }

            sql = "SELECT * FROM sys_document WHERE ModuleID = '" + DeviceSummaryID + "' AND Status > 0";
            table = GetDataTable(sql);
            if (table == null || table.Rows.Count == 0)
            {
                Logger.Error("没有获取到设备汇总表数据");
                return;
            }

            foreach (DataRow row in table.Rows)
            {
                var data = Convert.ToString(row["Data"]);
                if (string.IsNullOrEmpty(data))
                {
                    continue;
                }

                var document = JsonConvert.DeserializeObject<JZDocument>(data);
                if (document == null)
                {
                    continue;
                }

                var sheet = document.Sheets.Find(m => m.ID.ToString().ToLower() == SummarySheetID.ToLower());
                if (sheet == null)
                {
                    continue;
                }

                try
                {
                    NewRegisterion(row, sheet, ms, map);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
            //
        }

        public Dictionary<string, List<JZCell>> GroupCells(List<JZCell> cells)
        {
            var result = new Dictionary<string, List<JZCell>>();
            foreach (var cell in cells)
            {
                var name = cell.Name.Substring(1);
                if (result.ContainsKey(name) == false)
                {
                    result[name] = new List<JZCell>();
                }

                result[name].Add(cell);
            }

            return result;
        }

        public void NewRegisterion(DataRow row, JZSheet sheet, List<ModuleSetting> setting, string[] maps)
        {
            var offset = Offset;
            var cells = GroupCells(sheet.Cells);
            foreach (string key in cells.Keys)
            {

                if (Convert.ToInt32(key) < Offset)
                {
                    continue;
                }

                var datas = Template;
                var id = Guid.NewGuid();
                var values = ConvertRow(row);
                var counter = 0;
                foreach (var c in cells[key])
                {
                    for (var i = 0; i < maps.Length; i += 2)
                    {
                        if (c.Name[0].ToString().ToLower() == maps[i].ToLower())
                        {
                            var value = c.Value;
                            var newc = maps[i + 1];
                            var location = "{#" + newc + "}";


                            if (value == null)
                            {
                                datas = datas.Replace(location, "");
                            }
                            else
                            {
                                datas = datas.Replace(location, Convert.ToString(value));

                                var set = setting.Find(m => m.CellName.ToLower() == newc);
                                if (set != null)
                                {
                                    values[set.DocColumn] = value;
                                }

                                counter++;
                            }
                        }
                    }
                }

                if (counter > 0)
                {
                    datas = datas.Replace("{#id}", id.ToString());
                    //datas = datas.Replace("{#line}", Line.LineName);
                    values["Data"] = datas;
                    values["ID"] = id;
                    values["ModuleID"] = DeviceRegistrationID;
                    values["SegmentCode"] = Convert.ToString(values["TestRoomCode"]).Substring(0, 8);
                    values["CompanyCode"] = Convert.ToString(values["TestRoomCode"]).Substring(0, 12);

                    Openup.Insert("sys_document", values);
                    offset++;
                }
            }

        }

        public Dictionary<string, object> ConvertRow(DataRow row)
        {
            var result = new Dictionary<string, object>();

            foreach (DataColumn column in row.Table.Columns)
            {
                if (column.ColumnName.StartsWith("Ext"))
                {
                    continue;
                }

                result[column.ColumnName] = row[column.ColumnName];
            }

            return result;
        }

        public object GetCellValue(JZSheet sheet, string name)
        {
            if (string.IsNullOrEmpty(name) || sheet == null)
            {
                return null;
            }

            foreach (var cell in sheet.Cells)
            {
                if (cell.Name.ToLower() == name.ToLower())
                {
                    return cell.Value;
                }
            }

            return null;
        }
    }
}

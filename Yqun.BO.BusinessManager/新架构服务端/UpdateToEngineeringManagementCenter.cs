using System;
using System.Collections.Generic;
using System.Text;
using BizCommon;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    /// <summary>
    ///  数据上传工管中心
    /// </summary>
    public class UpdateToEngineeringManagementCenter:BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// 实时数据保存
        /// </summary>
        StringBuilder _StringBuilderRealTimeData = new StringBuilder();

        /// <summary>
        /// 获取龄期帮助实例
        /// </summary>
        StadiumHelper _StadiumHelper = new StadiumHelper();

        /// <summary>
        /// 上传数据到工管中心
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="moduleID"></param>
        /// <param name="stadiumID"></param>
        /// <param name="wtbh"></param>
        /// <param name="testRoomCode"></param>
        /// <param name="seriaNumber"></param>
        /// <param name="userName"></param>
        /// <param name="cells"></param>
        /// <param name="realTimeData"></param>
        /// <param name="machineBH"></param>
        /// <returns></returns>
        public bool UpdateToEMC(JZDocument doc, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, List<JZTestCell> cells, String realTimeData, string machineBH)
        {
            string tempMachineType = GetModuleTypeByID(moduleID);
            string _ErrorMsg = string.Empty;
            EMCServiceReference.DataInterfaceClient _DataInterfaceClient = new Yqun.BO.BusinessManager.EMCServiceReference.DataInterfaceClient();
            if (tempMachineType == "1")
            {

                if (_DataInterfaceClient.UploadPressureData(out _ErrorMsg, GetPressureDataModel(doc, moduleID, stadiumID, wtbh, testRoomCode, seriaNumber, userName, cells, realTimeData, machineBH)) == -1)
                { 
                    logger.Error("上传工管中心压力机数据错误：" + _ErrorMsg);
                }
            }
            if (tempMachineType == "2")
            {
                if (_DataInterfaceClient.UploadUniversalData(out _ErrorMsg, GetUniversalDataModel(doc, moduleID, stadiumID, wtbh, testRoomCode, seriaNumber, userName, cells, realTimeData, machineBH)) == -1)
                {
                    logger.Error("上传工管中心万能机数据错误：" + _ErrorMsg);
                }
            }
            return false;
        }

        /// <summary>
        /// 获取压力数据实例对象
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="moduleID"></param>
        /// <param name="stadiumID"></param>
        /// <param name="wtbh"></param>
        /// <param name="testRoomCode"></param>
        /// <param name="seriaNumber"></param>
        /// <param name="userName"></param>
        /// <param name="cells"></param>
        /// <param name="realTimeData"></param>
        /// <param name="machineBH"></param>
        /// <returns></returns>
        private EMCServiceReference.PressureData GetPressureDataModel(JZDocument doc, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, List<JZTestCell> cells, String realTimeData, string machineBH)
        {
            EMCServiceReference.PressureData _PressureDataModel = new EMCServiceReference.PressureData();
            try
            {
                _PressureDataModel.FGuid = System.Guid.NewGuid().ToString();
                _PressureDataModel.FIswjj = "0";
                _PressureDataModel.FKylz = Convert.ToDouble(cells[0].Value.ToString());
                DataTable _dt = _StadiumHelper.GetStadiumByID(stadiumID);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    string tempRealTimeData = GetYSLZ(realTimeData);
                    _PressureDataModel.FLq = int.Parse(_dt.Rows[0]["DateSpan"].ToString());
                    _PressureDataModel.FOperator = userName;
                    _PressureDataModel.FQddj = _dt.Rows[0]["F_Added"].ToString();
                    _PressureDataModel.FRtcode = GetModuleEMCRTCode(moduleID);
                    _PressureDataModel.FSbbh = machineBH;
                    _PressureDataModel.FSjbh = _dt.Rows[0]["F_SJBH"].ToString();
                    _PressureDataModel.FSjcc = GetSJCCMethod(_dt.Rows[0]["F_SJBH"].ToString());
                    _PressureDataModel.FSoftcom = "北京金舟神创科技发展有限公司";
                    _PressureDataModel.FSysj = tempRealTimeData.Split('|')[0];
                    _PressureDataModel.FVender = "北京金舟神创科技发展有限公司";
                    _PressureDataModel.FWtbh = wtbh;
                    _PressureDataModel.FYskylz = tempRealTimeData.Split('|')[1];
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return _PressureDataModel;
        }

        /// <summary>
        /// 获取万能机数据实例对象
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="moduleID"></param>
        /// <param name="stadiumID"></param>
        /// <param name="wtbh"></param>
        /// <param name="testRoomCode"></param>
        /// <param name="seriaNumber"></param>
        /// <param name="userName"></param>
        /// <param name="cells"></param>
        /// <param name="realTimeData"></param>
        /// <param name="machineBH"></param>
        /// <returns></returns>
        private EMCServiceReference.UniversalData GetUniversalDataModel(JZDocument doc, Guid moduleID, Guid stadiumID, String wtbh, String testRoomCode,
                Int32 seriaNumber, String userName, List<JZTestCell> cells, String realTimeData, string machineBH)
        {
            EMCServiceReference.UniversalData _UniversalDataModel = new EMCServiceReference.UniversalData();
            try
            {
                DataTable _dt = _StadiumHelper.GetStadiumByID(stadiumID);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    string tempRealTimeData = GetYSLZ(realTimeData);
                    _UniversalDataModel.FArea = ((Math.Pow(Convert.ToDouble(_dt.Rows[0]["F_SJSize"].ToString()), 2.0)) / 4 * 3.1415926);
                    _UniversalDataModel.FGczj = int.Parse(_dt.Rows[0]["F_SJSize"].ToString());
                    _UniversalDataModel.FGuid = System.Guid.NewGuid().ToString();
                    for (int i = 0; i < cells.Count; i++)
                    {
                        if (cells[i].Name == JZTestEnum.LDZDL)
                        {
                            _UniversalDataModel.FLz = Convert.ToDouble(cells[i].Value.ToString());
                        }
                        if (cells[i].Name == JZTestEnum.QFL)
                        {
                            _UniversalDataModel.FQflz = Convert.ToDouble(cells[i].Value.ToString());
                        }
                        if (cells[i].Name == JZTestEnum.DHBJ)
                        {
                            _UniversalDataModel.FScl = (Convert.ToDouble(cells[i].Value.ToString()) - Convert.ToDouble(_dt.Rows[0]["F_SJSize"].ToString()) * 5) / Convert.ToDouble(_dt.Rows[0]["F_SJSize"].ToString()) * 5;
                        }
                    }
                    _UniversalDataModel.FOperator = userName;
                    _UniversalDataModel.FPzcode = "";
                    _UniversalDataModel.FRtcode = GetModuleEMCRTCode(moduleID);
                    _UniversalDataModel.FSbbh = machineBH;
                    _UniversalDataModel.FSjbh = _dt.Rows[0]["F_SJBH"].ToString();
                    _UniversalDataModel.FSoftcom = "北京金舟神创科技发展有限公司";
                    _UniversalDataModel.FSysj = tempRealTimeData.Split('|')[0];
                    _UniversalDataModel.FVender = "北京金舟神创科技发展有限公司";
                    _UniversalDataModel.FWtbh = wtbh;
                    _UniversalDataModel.FWy = "0";
                    _UniversalDataModel.FYskllz = tempRealTimeData.Split('|')[1];
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return _UniversalDataModel;
        }


        /// <summary>
        /// 根据模板ID获取实验类型
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        private string GetModuleTypeByID(Guid moduleID)
        {
            try
            {
                DataTable dt = GetDataTable("select DeviceType from sys_module where ID='" + moduleID.ToString() + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取铁道部模板Code
        /// </summary>
        /// <param name="ModuleID">模板ID</param>
        /// <returns>工管中心模板对应ID</returns>
        public string GetModuleEMCRTCode(Guid ModuleID)
        {
            try
            {
                DataTable dt = GetDataTable("select ModuleALT from sys_module where ID='" + ModuleID.ToString() + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取试件尺寸编码
        /// </summary>
        /// <returns></returns>
        private int GetSJCCMethod(string TSize)
        {
            switch (TSize)
            {
                case "100×100×100":
                    return 2;
                case "150×150×150":
                    return 1;
                case "200×200×200":
                   return 3;
                case "70.7×70.7×70.7":
                    return 4;
            }
            return 0;
        }


        /// <summary>
        /// 获取实验的实时力值和试验时间
        /// </summary>
        /// <param name="realTimeData">实验的实时力值JOSN</param>
        /// <returns>试验时间|实时力值</returns>
        public string GetYSLZ(string realTimeData)
        {
            try
            {
                if (_StringBuilderRealTimeData.Length > 0)
                {
                    _StringBuilderRealTimeData.Remove(0, _StringBuilderRealTimeData.Length);
                }
                List<JZRealTimeData> _JZRealTimeData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JZRealTimeData>>(realTimeData);
                for (int i = 0; i < _JZRealTimeData.Count; i++)
                {
                    _StringBuilderRealTimeData.Append(_JZRealTimeData[i].Value);
                    if (i < _JZRealTimeData.Count - 1)
                    {
                        _StringBuilderRealTimeData.Append(",");
                    }
                }
                return _JZRealTimeData[_JZRealTimeData.Count - 1].Time.ToString() + "|" + _StringBuilderRealTimeData.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return string.Empty;
            }
        }
    }
}

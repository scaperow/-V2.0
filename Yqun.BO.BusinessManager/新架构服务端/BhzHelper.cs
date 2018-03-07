using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using TransferServiceCommon;
using System.Collections;
using System.IO;
using System.Data;
using BizCommon;

namespace Yqun.BO.BusinessManager
{
    /// <summary>
    /// 拌合站操作类
    /// </summary>
    public class BhzHelper : BOBase
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
        public DataTable GetBhzIng(string UserName, string TestCode)
        {
            if (TestCode == "-1")
            {
                TestCode = "";
            }
            DataTable dt = GetDataTable(string.Format("EXEC bhz_spweb_dtzs  '', '', '{0}', 1", TestCode)); //dal.GetListByProc("bhz_spweb_dtzs", "", "", testCode, 1);
            return dt;
        }
        /// <summary>
        /// 线路概况
        /// </summary>
        /// <returns></returns>
        public DataTable GetBhzLineStatics(DateTime StartDate, DateTime EndDate, string MachineCode)
        {
            string strSQL = string.Format(@"SELECT a.MachineName,B.MachineCode,SUM(ShuLiang) AS ZongFangLiang,COUNT(b.ChuLiaoShiJian) AS ZongPanLiang
,MAX(b.ChuLiaoShiJian)AS ZuiHouTime,
(SELECT COUNT(c.ChuLiaoShiJian) FROM dbo.bhz_PanDetail c WHERE 
  ChuLiaoShiJian>'{0}' AND ChuLiaoShiJian<'{1}'
AND c.MachineCode=b.MachineCode AND ChaoBiaoDengJi=1
)AS ChaoBiaoDengJi1,
(SELECT COUNT(c.ChuLiaoShiJian) FROM dbo.bhz_PanDetail c WHERE 
  ChuLiaoShiJian>'{0}' AND ChuLiaoShiJian<'{1}'
AND c.MachineCode=b.MachineCode AND ChaoBiaoDengJi=2
)AS ChaoBiaoDengJi2,
(SELECT COUNT(c.ChuLiaoShiJian) FROM dbo.bhz_PanDetail c WHERE 
  ChuLiaoShiJian>'{0}' AND ChuLiaoShiJian<'{1}'
AND c.MachineCode=b.MachineCode AND ChaoBiaoDengJi=3
)AS ChaoBiaoDengJi3
 FROM dbo.bhz_index a
INNER JOIN dbo.bhz_PanDetail b ON a.MachineCode=b.MachineCode
WHERE b.ChuLiaoShiJian>'{0}' AND b.ChuLiaoShiJian<'{1}' ", StartDate, EndDate);
            if (!string.IsNullOrEmpty(MachineCode))
            {
                strSQL = strSQL + string.Format(" AND a.MachineCode in({0}) ", MachineCode);
            }
            strSQL = strSQL + " GROUP BY b.MachineCode,MachineName ORDER BY b.MachineCode";

            DataTable dt = GetDataTable(strSQL);
            return dt;
        }
        /// <summary>
        /// 获取监理单位列表
        /// </summary>
        /// <param name="IsJL">是否监理：0否，1是</param>
        /// <returns></returns>
        public DataTable GetBhzJLCompanyList()
        {
            DataTable dt = GetDataTable(string.Format("SELECT * FROM dbo.bhz_Company WHERE IsJL=1"));
            return dt;
        }
        /// <summary>
        /// 获取施工单位列表
        /// </summary>
        /// <param name="IsJL">是否监理：0否，1是</param>
        /// <returns></returns>
        public DataTable GetBhzSGCompanyList(string JLID)
        {
            DataTable dt = GetDataTable(string.Format(@"SELECT * FROM dbo.bhz_Company WHERE ID IN(
          SELECT SGID FROM dbo.bhz_JL_SG_Relation WHERE JLID='{0}')", JLID));
            return dt;
        }
        /// <summary>
        /// 获取拌合站列表
        /// </summary>
        /// <param name="CompanyID">施工单位ID</param>
        /// <returns></returns>
        public DataTable GetBhzStationList(string CompanyID)
        {
            DataTable dt = GetDataTable(string.Format(@"SELECT * FROM dbo.bhz_Station WHERE CompanyID ='{0}' ", CompanyID));
            return dt;
        }
        /// <summary>
        /// 获取拌合站机器列表
        /// </summary>
        /// <param name="StationID">拌合站ID</param>
        /// <returns></returns>
        public DataTable GetBhzMachineList(string StationID)
        {
            DataTable dt = GetDataTable(string.Format(@"SELECT * FROM dbo.bhz_Machine WHERE StationID='{0}'", StationID));
            return dt;
        }
        /// <summary>
        /// 获取拌合站机器列表
        /// </summary>
        /// <param name="JLID">监理ID</param>
        /// <param name="TestCode">试验室编码</param>
        /// <returns></returns>
        public DataTable GetBhzMachineListWithFullName(string JLID, string TestCode)
        {
            string strWhere = string.Empty;
            if (TestCode != "-1" && TestCode != "")
            {
                if (!String.IsNullOrEmpty(JLID))
                {
                    //ds1 = dalTree.GetList(" JLID='" + JLID + "' and MachineCode in (" + TestCode + ") ");
                    strWhere = string.Format(" JLID='{0}' and MachineCode in ({1}) ", JLID, TestCode);
                }
                else
                {
                    strWhere = string.Format(" 1=0 ");
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(JLID))
                {
                    strWhere = string.Format(" JLID='{0}' ", JLID);
                }
                else
                {
                    strWhere = string.Format(" 1=0 ");
                }
            }
            string strSQL = string.Format(@"select JLID,JLName,SGID,SGName,StationID,StationName,MachineID,MachineName,MachineCode,FullName 
    FROM bhz_v_Tree  where {0}", strWhere);
            //logger.Error("GetBhzMachineListWithFullName strSQL" + strSQL);
            DataTable dt = GetDataTable(strSQL);
            return dt;
        }

        /// <summary>
        /// 超标查询
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable SuperscalarSearch(int PageSize, int LastRowNum, string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                strWhere = "1=1";
            }
            String sql = string.Format(@"SELECT TOP {0} * FROM (
SELECT row_number() over(order by ChuLiaoShiJian desc) as rownum, ID ,
          JiaoBanShiJian ,
          ShuLiang ,
          SheJiFangLiang ,
          ChuLiaoShiJian ,
          Shui ,
          Shui_SG ,
          ShuiWuCha ,
          Shui2 ,
          Shui2_SG ,
          Shui2WuCha ,
          Sha1 ,
          Sha1_SG ,
          Sha1WuCha ,
          Sha2 ,
          Sha2_SG ,
          Sha2WuCha ,
          ShuiNi1 ,
          ShuiNi1_SG ,
          ShuiNi1WuCha ,
          ShuiNi2 ,
          ShuiNi2_SG ,
          ShuiNi2WuCha ,
          ShuiNi3 ,
          ShuiNi3_SG ,
          ShuiNi3WuCha ,
          ShuiNi4 ,
          ShuiNi4_SG ,
          ShuiNi4WuCha ,
          GuLiao1 ,
          GuLiao1_SG ,
          GuLiao1WuCha ,
          GuLiao2 ,
          GuLiao2_SG ,
          GuLiao2WuCha ,
          GuLiao3 ,
          GuLiao3_SG ,
          GuLiao3WuCha ,
          GuLiao4 ,
          GuLiao4_SG ,
          GuLiao4WuCha ,
          XiGuLiao ,
          XiGuLiao_SG ,
          XiGuLiaoWuCha ,
          DaShiZi ,
          DaShiZi_SG ,
          DaShiZiWuCha ,
          XiaoShiZi ,
          XiaoShiZi_SG ,
          XiaoShiZiWuCha ,
          MeiHui ,
          MeiHui_SG ,
          MeiHuiWuCha ,
          FenMeiHui1 ,
          FenMeiHui1_SG ,
          FenMeiHui1WuCha ,
          KuangFen1 ,
          KuangFen1_SG ,
          KuangFen1WuCha ,
          KuangFen2 ,
          KuangFen2_SG ,
          KuangFen2WuCha ,
          KuangFen3 ,
          KuangFen3_SG ,
          KuangFen3WuCha ,
          KuangFen4 ,
          KuangFen4_SG ,
          KuangFen4WuCha ,
          FenMeiHui2 ,
          FenMeiHui2_SG ,
          FenMeiHui2WuCha ,
          FenMeiHui3 ,
          FenMeiHui3_SG ,
          FenMeiHui3WuCha ,
          FenMeiHui4 ,
          FenMeiHui4_SG ,
          FenMeiHui4WuCha ,
          WaiJiaJi1 ,
          WaiJiaJi1_SG ,
          WaiJiaJi1WuCha ,
          WaiJiaJi2 ,
          WaiJiaJi2_SG ,
          WaiJiaJi2WuCha ,
          WaiJiaJi3 ,
          WaiJiaJi3_SG ,
          WaiJiaJi3WuCha ,
          WaiJiaJi4 ,
          WaiJiaJi4_SG ,
          WaiJiaJi4WuCha ,
          Bz1 ,
          Bz1_SG ,
          Bz1WuCha ,
          Bz2 ,
          Bz2_SG ,
          Bz2WuCha ,
          Bz3 ,
          Bz3_SG ,
          Bz3WuCha ,
          Bz4 ,
          Bz4_SG ,
          Bz4WuCha ,
          Bz5 ,
          Bz5_SG ,
          Bz5WuCha ,
          GongDanHao ,
          CaoZuoZhe ,
          ProjectName ,
          DiDian_LiCheng ,
          JiaoZhuBuWei ,
          WaiJiaJiPinZhong ,
          ShuiNiPinZhong ,
          ShiGongPeiHeBiBianHao ,
          QiangDuDengJi ,
          MachineCode ,
          SavedTime ,
          CollectedTime ,
          KeHuDuanBianHao ,
          TongYongLiang ,
          JiaoZhuFangShi ,
          TaLuoDu ,
          CheCi ,
          CheShu ,
          CheFangLiang ,
          ChaoBiaoDengJi ,
          DuanXin ,
          DuanXinTime ,
          ChuLiYiJian1 ,
          ChuLiYiJian2 ,
          FullName FROM bhz_PanDetail where {2})T WHERE rownum>{1}  ", PageSize, LastRowNum, strWhere);
            return GetDataTable(sql);
        }

        /// <summary>
        /// 超标查询总数
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int SuperscalarSearchCount(string strWhere)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                strWhere = "1=1";
            }
            String sql = string.Format(@"SELECT count(0) FROM bhz_PanDetail where {0} ", strWhere);
            int iCount = 0;
            iCount = int.Parse(ExcuteScalar(sql).ToString());
            return iCount;
        }

        /// <summary>
        /// 查询最新超标数量
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int GetSuperscalarCount(string LastDate)
        {
            string strWhere = " ChaoBiaoDengJi>0  and ChaoBiaoDengJi<4  ";
            DateTime dtLastDate;
            bool bLastDate = DateTime.TryParse(LastDate, out dtLastDate);
            if (bLastDate)
            {
                strWhere += " AND ChuLiaoShiJian>'" + dtLastDate.ToString("yyyy-MM-dd") + "' ";
            }

            String sql = string.Format(@"SELECT COUNT(*) FROM bhz_PanDetail WHERE {0}", strWhere);
            int iCount = 0;
            iCount = int.Parse(ExcuteScalar(sql).ToString());
            return iCount;
        }

        /// <summary>
        /// 产能分析
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataSet CapacityAnalysis(string MachineCodes, DateTime dtStart, int FType, DateTime dtEnd, int PageSize, int PageIndex)
        {
            MachineCodes = MachineCodes.Replace("'", "''");
            String sql = string.Format(@"
DECLARE	@pageCount int,
		@Counts int

EXEC	[dbo].[bhz_spweb_cnfx]
		@testcode = N'{0}',
		@startdate = N'{1}',
		@ftype = {2},
		@enddate = N'{3}',
		@pageSize = {4},
		@page = {5},
		@Sort = 0,
		@pageCount = @pageCount OUTPUT,
		@Counts = @Counts OUTPUT
SELECT @Counts
", MachineCodes, dtStart, FType, dtEnd, PageSize, PageIndex);

            //logger.Error("CapacityAnalysis SQL:" + sql);
            return GetDataSet(sql);
        }

        /// <summary>
        /// 获取超标详细
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetBhzPanDetail(string PanID)
        {
            String sql = string.Format(@"SELECT ID ,
          JiaoBanShiJian ,
          ShuLiang ,
          SheJiFangLiang ,
          ChuLiaoShiJian ,
          Shui ,
          Shui_SG ,
          ShuiWuCha ,
          Shui2 ,
          Shui2_SG ,
          Shui2WuCha ,
          Sha1 ,
          Sha1_SG ,
          Sha1WuCha ,
          Sha2 ,
          Sha2_SG ,
          Sha2WuCha ,
          ShuiNi1 ,
          ShuiNi1_SG ,
          ShuiNi1WuCha ,
          ShuiNi2 ,
          ShuiNi2_SG ,
          ShuiNi2WuCha ,
          ShuiNi3 ,
          ShuiNi3_SG ,
          ShuiNi3WuCha ,
          ShuiNi4 ,
          ShuiNi4_SG ,
          ShuiNi4WuCha ,
          GuLiao1 ,
          GuLiao1_SG ,
          GuLiao1WuCha ,
          GuLiao2 ,
          GuLiao2_SG ,
          GuLiao2WuCha ,
          GuLiao3 ,
          GuLiao3_SG ,
          GuLiao3WuCha ,
          GuLiao4 ,
          GuLiao4_SG ,
          GuLiao4WuCha ,
          XiGuLiao ,
          XiGuLiao_SG ,
          XiGuLiaoWuCha ,
          DaShiZi ,
          DaShiZi_SG ,
          DaShiZiWuCha ,
          XiaoShiZi ,
          XiaoShiZi_SG ,
          XiaoShiZiWuCha ,
          MeiHui ,
          MeiHui_SG ,
          MeiHuiWuCha ,
          FenMeiHui1 ,
          FenMeiHui1_SG ,
          FenMeiHui1WuCha ,
          KuangFen1 ,
          KuangFen1_SG ,
          KuangFen1WuCha ,
          KuangFen2 ,
          KuangFen2_SG ,
          KuangFen2WuCha ,
          KuangFen3 ,
          KuangFen3_SG ,
          KuangFen3WuCha ,
          KuangFen4 ,
          KuangFen4_SG ,
          KuangFen4WuCha ,
          FenMeiHui2 ,
          FenMeiHui2_SG ,
          FenMeiHui2WuCha ,
          FenMeiHui3 ,
          FenMeiHui3_SG ,
          FenMeiHui3WuCha ,
          FenMeiHui4 ,
          FenMeiHui4_SG ,
          FenMeiHui4WuCha ,
          WaiJiaJi1 ,
          WaiJiaJi1_SG ,
          WaiJiaJi1WuCha ,
          WaiJiaJi2 ,
          WaiJiaJi2_SG ,
          WaiJiaJi2WuCha ,
          WaiJiaJi3 ,
          WaiJiaJi3_SG ,
          WaiJiaJi3WuCha ,
          WaiJiaJi4 ,
          WaiJiaJi4_SG ,
          WaiJiaJi4WuCha ,
          Bz1 ,
          Bz1_SG ,
          Bz1WuCha ,
          Bz2 ,
          Bz2_SG ,
          Bz2WuCha ,
          Bz3 ,
          Bz3_SG ,
          Bz3WuCha ,
          Bz4 ,
          Bz4_SG ,
          Bz4WuCha ,
          Bz5 ,
          Bz5_SG ,
          Bz5WuCha ,
          GongDanHao ,
          CaoZuoZhe ,
          ProjectName ,
          DiDian_LiCheng ,
          JiaoZhuBuWei ,
          WaiJiaJiPinZhong ,
          ShuiNiPinZhong ,
          ShiGongPeiHeBiBianHao ,
          QiangDuDengJi ,
          MachineCode ,
          SavedTime ,
          CollectedTime ,
          KeHuDuanBianHao ,
          TongYongLiang ,
          JiaoZhuFangShi ,
          TaLuoDu ,
          CheCi ,
          CheShu ,
          CheFangLiang ,
          ChaoBiaoDengJi ,
          DuanXin ,
          DuanXinTime ,
          ChuLiYiJian1 ,
          ChuLiYiJian2 ,
          FullName FROM bhz_PanDetail where ID='{0}' ", PanID);
            return GetDataTable(sql);
        }

        /// <summary>
        /// 更新拌合站超标详细的处理结果
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool UpdateBhzPanDetailChuLiYiJian(string PanID, string ChuLiYiJian)
        {
            String sql = string.Format(@"UPDATE dbo.bhz_PanDetail SET ChuLiYiJian1='{1}' WHERE ID='{0}'", PanID, ChuLiYiJian);
            int row = ExcuteCommand(sql);
            bool bSuccess = row > 0 ? true : false;
            //logger.Error("UpdateBhzPanDetailChuLiYiJian bSuccess:" + bSuccess);
            return bSuccess;
        }
        #region 通用方法
        #endregion
    }
}

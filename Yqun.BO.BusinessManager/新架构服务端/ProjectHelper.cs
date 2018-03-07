using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Yqun.BO.BusinessManager
{
    public class ProjectHelper : BOBase
    {
        public DataTable GetTestRoomCodeView()
        {
            String sql = @"SELECT * FROM dbo.v_bs_codeName ORDER BY 单位名称, 试验室编码";

            return GetDataTable(sql);

        }
        public DataTable GetTestRoomInfoByCode(string TestRoomCode)
        {
            String sql = @"SELECT * FROM dbo.v_bs_codeName where 试验室编码='"+TestRoomCode+"'";

            return GetDataTable(sql);
        }
    }
}

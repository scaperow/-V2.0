using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Yqun.Services;
using BizCommon;
using Yqun.Permissions.Runtime;
using Yqun.Permissions.Common;

namespace BizComponents
{
    public class DepositoryProjectInfo
    {
        /// <summary>
        /// 查询所有工程信息
        /// </summary>
        /// <returns></returns>
        public static List<Project> QueryProjects()
        {
            List<Project> ProjectsLists = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryProjects", new object[] { }) as List<Project>;
            return ProjectsLists;
        }

        public static Project Query(String PrjCode)
        {
            Project ProjectInfo = Agent.CallService("Yqun.BO.BusinessManager.dll", "QueryProject", new object[] { PrjCode }) as Project;
            return ProjectInfo;
      
        }
        /// <summary>
        /// 判断是否存在工程信息
        /// </summary>
        /// <param name="PrjCode"></param>
        /// <returns></returns>
        public static Boolean HaveProjectInfo(String PrjCode)
        {
            Boolean IsHave = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "HaveProjectInfo", new object[] { PrjCode }));
            return IsHave;
        }

        /// <summary>
        /// 新建工程信息
        /// </summary>
        /// <param name="ProjectInfo"></param>
        /// <returns></returns>
        public static Boolean New(Project ProjectInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "NewProject", new object[] { ProjectInfo }));
            return Result;       
        }

        /// <summary>
        /// 删除工程信息
        /// </summary>
        /// <param name="PrjCode"></param>
        /// <returns></returns>
        public static Boolean Delete(string ProjCode,string ProjId)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "DeleteProject", new object[] { ProjCode, ProjId }));
            return Result;
        }

        /// <summary>
        /// 更新工程信息
        /// </summary>
        /// <param name="ProjectInfo"></param>
        /// <returns></returns>
        public static Boolean Update(Project ProjectInfo)
        {
            Boolean Result = Convert.ToBoolean(Agent.CallService("Yqun.BO.BusinessManager.dll", "UpdateProjectInfo", new object[] { ProjectInfo }));
            return Result;           
        }
    }
}

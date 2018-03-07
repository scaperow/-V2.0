/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2014-10-13 16:30:56                          */
/*==============================================================*/


if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ExecSql')
          and type in ('P','PC'))
   drop procedure dbo.ExecSql
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fsys_DayTemperatureSum')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fsys_DayTemperatureSum
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fsys_EarlyDate')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fsys_EarlyDate
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fsys_StadiumStartDate')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fsys_StadiumStartDate
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fsys_SumTemperature')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fsys_SumTemperature
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fweb_FailReport')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fweb_FailReport
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fweb_ReturnCount')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fweb_ReturnCount
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fweb_ReturnPXCount')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fweb_ReturnPXCount
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fweb_ReturnPXQualityCount')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fweb_ReturnPXQualityCount
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fweb_ReturnPXQualityCount_New')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fweb_ReturnPXQualityCount_New
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.Fweb_SplitExpression')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.Fweb_SplitExpression
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.HasRefer')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.HasRefer
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.ReferTo')
          and type in ('IF', 'FN', 'TF'))
   drop function dbo.ReferTo
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_Line_Company_Station_Machine')
          and type in ('P','PC'))
   drop procedure dbo.bhz_Line_Company_Station_Machine
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_cbcx_chart')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_cbcx_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_cbhs')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_cbhs
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_clhs')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_clhs
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_cltj')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_cltj
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_cnfx')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_cnfx
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_dtzs')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_dtzs
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_dxbj')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_dxbj
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_login_charts')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_login_charts
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_login_charts_pop')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_login_charts_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_login_one')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_login_one
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_pager')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_pager
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.bhz_spweb_wcfx')
          and type in ('P','PC'))
   drop procedure dbo.bhz_spweb_wcfx
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_GetSGTestCode')
          and type in ('P','PC'))
   drop procedure dbo.sp_GetSGTestCode
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_document_list')
          and type in ('P','PC'))
   drop procedure dbo.sp_document_list
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_getFormulas')
          and type in ('P','PC'))
   drop procedure dbo.sp_getFormulas
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_getValidTestRoomCode')
          and type in ('P','PC'))
   drop procedure dbo.sp_getValidTestRoomCode
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_newPXJZ_ByNewModel')
          and type in ('P','PC'))
   drop procedure dbo.sp_newPXJZ_ByNewModel
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_newPX_Chart')
          and type in ('P','PC'))
   drop procedure dbo.sp_newPX_Chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_pager')
          and type in ('P','PC'))
   drop procedure dbo.sp_pager
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_px_chart')
          and type in ('P','PC'))
   drop procedure dbo.sp_px_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_pxjzReport')
          and type in ('P','PC'))
   drop procedure dbo.sp_pxjzReport
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_pxjz_report')
          and type in ('P','PC'))
   drop procedure dbo.sp_pxjz_report
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_report')
          and type in ('P','PC'))
   drop procedure dbo.sp_report
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_update')
          and type in ('P','PC'))
   drop procedure dbo.sp_update
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_update_module')
          and type in ('P','PC'))
   drop procedure dbo.sp_update_module
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_update_module_sheet')
          and type in ('P','PC'))
   drop procedure dbo.sp_update_module_sheet
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.sp_update_sheet')
          and type in ('P','PC'))
   drop procedure dbo.sp_update_sheet
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_CreateMatchine')
          and type in ('P','PC'))
   drop procedure dbo.spweb_CreateMatchine
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_Createtree')
          and type in ('P','PC'))
   drop procedure dbo.spweb_Createtree
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_PX_Count_Summary')
          and type in ('P','PC'))
   drop procedure dbo.spweb_PX_Count_Summary
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_PX_ZJ_Summary')
          and type in ('P','PC'))
   drop procedure dbo.spweb_PX_ZJ_Summary
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_ageremind')
          and type in ('P','PC'))
   drop procedure dbo.spweb_ageremind
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_check_table')
          and type in ('P','PC'))
   drop procedure dbo.spweb_check_table
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_evaluatedata')
          and type in ('P','PC'))
   drop procedure dbo.spweb_evaluatedata
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_evaluatedata_chart')
          and type in ('P','PC'))
   drop procedure dbo.spweb_evaluatedata_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_evaluatedata_chart_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_evaluatedata_chart_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_exec_qxzlhzb_charts')
          and type in ('P','PC'))
   drop procedure dbo.spweb_exec_qxzlhzb_charts
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_failreport')
          and type in ('P','PC'))
   drop procedure dbo.spweb_failreport
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_failreport_chart')
          and type in ('P','PC'))
   drop procedure dbo.spweb_failreport_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_failreport_chart_order')
          and type in ('P','PC'))
   drop procedure dbo.spweb_failreport_chart_order
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_failreport_chart_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_failreport_chart_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_jzReport')
          and type in ('P','PC'))
   drop procedure dbo.spweb_jzReport
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_login_charts')
          and type in ('P','PC'))
   drop procedure dbo.spweb_login_charts
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_login_charts_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_login_charts_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_login_one')
          and type in ('P','PC'))
   drop procedure dbo.spweb_login_one
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_loginlogpop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_loginlogpop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_machineSummary_chart')
          and type in ('P','PC'))
   drop procedure dbo.spweb_machineSummary_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_machineSummary_chart_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_machineSummary_chart_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pager')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pager
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pxjzReport')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pxjzReport
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pxjzReport_ByCode')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pxjzReport_ByCode
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pxjzReport_ByCode_fail')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pxjzReport_ByCode_fail
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pxjzReport_Chart')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pxjzReport_Chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pxjzReport_fail_chart')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pxjzReport_fail_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pxjzReport_line_chart')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pxjzReport_line_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_pxreport_Chart_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_pxreport_Chart_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_qxzlhzb')
          and type in ('P','PC'))
   drop procedure dbo.spweb_qxzlhzb
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_qxzlhzb_charts')
          and type in ('P','PC'))
   drop procedure dbo.spweb_qxzlhzb_charts
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_qxzlhzb_charts_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_qxzlhzb_charts_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_qxzlhzb_charts_pop_grid')
          and type in ('P','PC'))
   drop procedure dbo.spweb_qxzlhzb_charts_pop_grid
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_qxzlhzb_fail_charts')
          and type in ('P','PC'))
   drop procedure dbo.spweb_qxzlhzb_fail_charts
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_qxzlhzb_jqgrid_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_qxzlhzb_jqgrid_pop
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_tqdreport')
          and type in ('P','PC'))
   drop procedure dbo.spweb_tqdreport
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_userSummary')
          and type in ('P','PC'))
   drop procedure dbo.spweb_userSummary
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_userSummary_chart')
          and type in ('P','PC'))
   drop procedure dbo.spweb_userSummary_chart
go

if exists (select 1
          from sysobjects
          where  id = object_id('dbo.spweb_userSummary_chart_pop')
          and type in ('P','PC'))
   drop procedure dbo.spweb_userSummary_chart_pop
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.sys_formulas') and o.name = 'FK_sys_formulas_sys_module')
alter table dbo.sys_formulas
   drop constraint FK_sys_formulas_sys_module
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.sys_module_sheet') and o.name = 'FK_sys_module_sheet_sys_module')
alter table dbo.sys_module_sheet
   drop constraint FK_sys_module_sheet_sys_module
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.sys_stadium') and o.name = 'FK_sys_stadium_sys_document')
alter table dbo.sys_stadium
   drop constraint FK_sys_stadium_sys_document
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_bs_codeName')
            and   type = 'V')
   drop view dbo.v_bs_codeName
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_bs_evaluateData')
            and   type = 'V')
   drop view dbo.v_bs_evaluateData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_bs_machineSummary')
            and   type = 'V')
   drop view dbo.v_bs_machineSummary
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_bs_reminder_stadiumData')
            and   type = 'V')
   drop view dbo.v_bs_reminder_stadiumData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_bs_sms')
            and   type = 'V')
   drop view dbo.v_bs_sms
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_bs_userSummary')
            and   type = 'V')
   drop view dbo.v_bs_userSummary
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_codeName')
            and   type = 'V')
   drop view dbo.v_codeName
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_invalid_document')
            and   type = 'V')
   drop view dbo.v_invalid_document
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.v_operate_log')
            and   type = 'V')
   drop view dbo.v_operate_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Sys_Tree')
            and   type = 'U')
   drop table dbo.Sys_Tree
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Sys_Users')
            and   type = 'U')
   drop table dbo.Sys_Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.bhz_Mix')
            and   type = 'U')
   drop table dbo.bhz_Mix
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.bhz_sys_BaseLine_Users')
            and   type = 'U')
   drop table dbo.bhz_sys_BaseLine_Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.bhz_sys_BaseUsers')
            and   type = 'U')
   drop table dbo.bhz_sys_BaseUsers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.biz_ZJ_JZ_Summary')
            and   type = 'U')
   drop table dbo.biz_ZJ_JZ_Summary
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.biz_fail')
            and   type = 'U')
   drop table dbo.biz_fail
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.biz_machinelist')
            and   type = 'U')
   drop table dbo.biz_machinelist
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.biz_px_relation')
            and   type = 'U')
   drop table dbo.biz_px_relation
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.biz_px_relation_web')
            and   type = 'U')
   drop table dbo.biz_px_relation_web
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.biz_qxzl')
            and   type = 'U')
   drop table dbo.biz_qxzl
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.biz_tongqiangdu')
            and   type = 'U')
   drop table dbo.biz_tongqiangdu
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.check_table')
            and   type = 'U')
   drop table dbo.check_table
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_BaseLine_Users')
            and   type = 'U')
   drop table dbo.sys_BaseLine_Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_BaseLine_UsersLoginLog')
            and   type = 'U')
   drop table dbo.sys_BaseLine_UsersLoginLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_BaseUsers')
            and   type = 'U')
   drop table dbo.sys_BaseUsers
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_KeyModify')
            and   type = 'U')
   drop table dbo.sys_KeyModify
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_ReditRating')
            and   type = 'U')
   drop table dbo.sys_ReditRating
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_ReditRatingBHZ')
            and   type = 'U')
   drop table dbo.sys_ReditRatingBHZ
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_TJ_Factory')
            and   type = 'U')
   drop table dbo.sys_TJ_Factory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_TJ_Item')
            and   type = 'U')
   drop table dbo.sys_TJ_Item
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_TJ_Item_Module')
            and   type = 'U')
   drop table dbo.sys_TJ_Item_Module
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_TJ_MainData')
            and   type = 'U')
   drop table dbo.sys_TJ_MainData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_TJ_StandardValue')
            and   type = 'U')
   drop table dbo.sys_TJ_StandardValue
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_CustomerService_UpdateInfo')
            and   type = 'U')
   drop table dbo.sys_auth_CustomerService_UpdateInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_Customer_Service_Users')
            and   type = 'U')
   drop table dbo.sys_auth_Customer_Service_Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_DataPermission')
            and   type = 'U')
   drop table dbo.sys_auth_DataPermission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_FieldPermission')
            and   type = 'U')
   drop table dbo.sys_auth_FieldPermission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_FunctionPermission')
            and   type = 'U')
   drop table dbo.sys_auth_FunctionPermission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_Permissions')
            and   type = 'U')
   drop table dbo.sys_auth_Permissions
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_RecordPermission')
            and   type = 'U')
   drop table dbo.sys_auth_RecordPermission
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_Roles')
            and   type = 'U')
   drop table dbo.sys_auth_Roles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_Users')
            and   type = 'U')
   drop table dbo.sys_auth_Users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_auth_UsersDevices')
            and   type = 'U')
   drop table dbo.sys_auth_UsersDevices
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_Cache')
            and   type = 'U')
   drop table dbo.sys_biz_Cache
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_Cache_DeletedData')
            and   type = 'U')
   drop table dbo.sys_biz_Cache_DeletedData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_CrossSheetFormulas')
            and   type = 'U')
   drop table dbo.sys_biz_CrossSheetFormulas
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_DataArea')
            and   type = 'U')
   drop table dbo.sys_biz_DataArea
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_DataModification')
            and   type = 'U')
   drop table dbo.sys_biz_DataModification
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_DataUpload')
            and   type = 'U')
   drop table dbo.sys_biz_DataUpload
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_FunctionInfos')
            and   type = 'U')
   drop table dbo.sys_biz_FunctionInfos
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_Image')
            and   type = 'U')
   drop table dbo.sys_biz_Image
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_Module')
            and   type = 'U')
   drop table dbo.sys_biz_Module
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ModuleCatlog')
            and   type = 'U')
   drop table dbo.sys_biz_ModuleCatlog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ModuleCatlog_update')
            and   type = 'U')
   drop table dbo.sys_biz_ModuleCatlog_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ModuleFormatStrings')
            and   type = 'U')
   drop table dbo.sys_biz_ModuleFormatStrings
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ReportCatlog')
            and   type = 'U')
   drop table dbo.sys_biz_ReportCatlog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ReportFormatStrings')
            and   type = 'U')
   drop table dbo.sys_biz_ReportFormatStrings
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ReportParameters')
            and   type = 'U')
   drop table dbo.sys_biz_ReportParameters
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ReportRTConfig')
            and   type = 'U')
   drop table dbo.sys_biz_ReportRTConfig
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_ReportSheet')
            and   type = 'U')
   drop table dbo.sys_biz_ReportSheet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_Reporttables')
            and   type = 'U')
   drop table dbo.sys_biz_Reporttables
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_Sheet')
            and   type = 'U')
   drop table dbo.sys_biz_Sheet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_SheetCatlog')
            and   type = 'U')
   drop table dbo.sys_biz_SheetCatlog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_SheetCatlog_update')
            and   type = 'U')
   drop table dbo.sys_biz_SheetCatlog_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_moduleview')
            and   type = 'U')
   drop table dbo.sys_biz_moduleview
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_Itemcondition')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_Itemcondition
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_Itemfrequency')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_Itemfrequency
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_evaluateData')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_evaluateData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_evaluatecondition')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_evaluatecondition
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_samplingFrequency')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_samplingFrequency
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_stadiumData')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_stadiumData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_stadiumData_UpdateInfo')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_stadiumData_UpdateInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_stadiumInfo')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_stadiumInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_stadiummodel')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_stadiummodel
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_supervisionReport')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_supervisionReport
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_biz_reminder_testitem')
            and   type = 'U')
   drop table dbo.sys_biz_reminder_testitem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_bs_users')
            and   type = 'U')
   drop table dbo.sys_bs_users
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_caiji_log')
            and   type = 'U')
   drop table dbo.sys_caiji_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_columns')
            and   type = 'U')
   drop table dbo.sys_columns
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_custom_view')
            and   name  = 'idx_testRoomCode'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_custom_view.idx_testRoomCode
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_custom_view')
            and   name  = 'idx_moduleID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_custom_view.idx_moduleID
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_custom_view')
            and   type = 'U')
   drop table dbo.sys_custom_view
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_day')
            and   type = 'U')
   drop table dbo.sys_day
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_devices')
            and   type = 'U')
   drop table dbo.sys_devices
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_dictionary')
            and   name  = 'idx_codeClass'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_dictionary.idx_codeClass
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_dictionary')
            and   type = 'U')
   drop table dbo.sys_dictionary
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_dictionary_update')
            and   type = 'U')
   drop table dbo.sys_dictionary_update
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_document')
            and   name  = 'idx_wtbh'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_document.idx_wtbh
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_document')
            and   name  = 'idx_trytype'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_document.idx_trytype
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_document')
            and   name  = 'idx_testRoomCode'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_document.idx_testRoomCode
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_document')
            and   name  = 'idx_moduleID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_document.idx_moduleID
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_document')
            and   name  = 'idx_list'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_document.idx_list
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_document')
            and   name  = 'idx_index'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_document.idx_index
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_document')
            and   name  = 'idx_bgbh'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_document.idx_bgbh
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_document')
            and   type = 'U')
   drop table dbo.sys_document
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_document_ext')
            and   type = 'U')
   drop table dbo.sys_document_ext
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_engs_CompanyInfo')
            and   type = 'U')
   drop table dbo.sys_engs_CompanyInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_engs_ItemInfo')
            and   type = 'U')
   drop table dbo.sys_engs_ItemInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_engs_ProjectInfo')
            and   type = 'U')
   drop table dbo.sys_engs_ProjectInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_engs_SectionInfo')
            and   type = 'U')
   drop table dbo.sys_engs_SectionInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_engs_Tree')
            and   type = 'U')
   drop table dbo.sys_engs_Tree
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_engs_Tree_Chart')
            and   type = 'U')
   drop table dbo.sys_engs_Tree_Chart
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_equipment_code')
            and   type = 'U')
   drop table dbo.sys_equipment_code
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_formulas')
            and   name  = 'idx_sheetID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_formulas.idx_sheetID
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_formulas')
            and   name  = 'idx_moduleID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_formulas.idx_moduleID
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_formulas')
            and   type = 'U')
   drop table dbo.sys_formulas
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_formulas_update')
            and   type = 'U')
   drop table dbo.sys_formulas_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_ggc_LabTypeCode')
            and   type = 'U')
   drop table dbo.sys_ggc_LabTypeCode
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_ggc_UserAuth')
            and   type = 'U')
   drop table dbo.sys_ggc_UserAuth
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_invalid_document')
            and   type = 'U')
   drop table dbo.sys_invalid_document
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_invalid_document_update')
            and   type = 'U')
   drop table dbo.sys_invalid_document_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_line')
            and   type = 'U')
   drop table dbo.sys_line
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_line_cellstyle')
            and   type = 'U')
   drop table dbo.sys_line_cellstyle
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_line_formulas')
            and   type = 'U')
   drop table dbo.sys_line_formulas
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_line_formulas_update')
            and   type = 'U')
   drop table dbo.sys_line_formulas_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_line_module')
            and   type = 'U')
   drop table dbo.sys_line_module
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_log')
            and   type = 'U')
   drop table dbo.sys_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_loginlog')
            and   type = 'U')
   drop table dbo.sys_loginlog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_module')
            and   type = 'U')
   drop table dbo.sys_module
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_module_config')
            and   name  = 'idx_moduleID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_module_config.idx_moduleID
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_module_config')
            and   type = 'U')
   drop table dbo.sys_module_config
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_module_config_update')
            and   type = 'U')
   drop table dbo.sys_module_config_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_module_sheet')
            and   type = 'U')
   drop table dbo.sys_module_sheet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_module_sheet_update')
            and   type = 'U')
   drop table dbo.sys_module_sheet_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_module_update')
            and   type = 'U')
   drop table dbo.sys_module_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_module_watermark')
            and   type = 'U')
   drop table dbo.sys_module_watermark
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_moduleview')
            and   type = 'U')
   drop table dbo.sys_moduleview
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_operate_log')
            and   name  = 'idx_testRoomCode'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_operate_log.idx_testRoomCode
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_operate_log')
            and   name  = 'idx_requestID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_operate_log.idx_requestID
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_operate_log')
            and   name  = 'idx_moduleID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_operate_log.idx_moduleID
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_operate_log')
            and   name  = 'idx_modifiedDate'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_operate_log.idx_modifiedDate
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_operate_log')
            and   name  = 'idx_dataID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_operate_log.idx_dataID
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_operate_log')
            and   type = 'U')
   drop table dbo.sys_operate_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_px_relation')
            and   type = 'U')
   drop table dbo.sys_px_relation
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_pxjz_frequency')
            and   type = 'U')
   drop table dbo.sys_pxjz_frequency
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_qualificationauth')
            and   type = 'U')
   drop table dbo.sys_qualificationauth
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_readfunction')
            and   type = 'U')
   drop table dbo.sys_readfunction
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_readfunctioncondition')
            and   type = 'U')
   drop table dbo.sys_readfunctioncondition
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_readfunctionmodify')
            and   type = 'U')
   drop table dbo.sys_readfunctionmodify
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_report')
            and   type = 'U')
   drop table dbo.sys_report
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_report_config')
            and   type = 'U')
   drop table dbo.sys_report_config
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_report_config_module')
            and   type = 'U')
   drop table dbo.sys_report_config_module
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_report_parameters')
            and   type = 'U')
   drop table dbo.sys_report_parameters
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_request_change')
            and   name  = 'idx_dataID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_request_change.idx_dataID
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_request_change')
            and   type = 'U')
   drop table dbo.sys_request_change
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_rt_hntyljDataTable')
            and   type = 'U')
   drop table dbo.sys_rt_hntyljDataTable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_sheet')
            and   type = 'U')
   drop table dbo.sys_sheet
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_sheet_update')
            and   type = 'U')
   drop table dbo.sys_sheet_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_sms_log')
            and   type = 'U')
   drop table dbo.sys_sms_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_sms_receiver')
            and   type = 'U')
   drop table dbo.sys_sms_receiver
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_sms_stadium_log')
            and   type = 'U')
   drop table dbo.sys_sms_stadium_log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_solcontent')
            and   type = 'U')
   drop table dbo.sys_solcontent
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_solfordertype')
            and   name  = 'IDX_SYS_SOLFORDERTYPE_TYPEFLAG'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_solfordertype.IDX_SYS_SOLFORDERTYPE_TYPEFLAG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_solfordertype')
            and   type = 'U')
   drop table dbo.sys_solfordertype
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_solforldr')
            and   type = 'U')
   drop table dbo.sys_solforldr
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_stadium')
            and   name  = 'idx_testroomcode_moduleID_fisdone_fwtbh'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_stadium.idx_testroomcode_moduleID_fisdone_fwtbh
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_stadium')
            and   name  = 'idx_dataID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_stadium.idx_dataID
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_stadium')
            and   type = 'U')
   drop table dbo.sys_stadium
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_stadium_config')
            and   type = 'U')
   drop table dbo.sys_stadium_config
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_stadium_config_days')
            and   type = 'U')
   drop table dbo.sys_stadium_config_days
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_stadium_config_days_update')
            and   type = 'U')
   drop table dbo.sys_stadium_config_days_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_stadium_config_update')
            and   type = 'U')
   drop table dbo.sys_stadium_config_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_stadium_temperature')
            and   type = 'U')
   drop table dbo.sys_stadium_temperature
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_tables')
            and   type = 'U')
   drop table dbo.sys_tables
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_temperature_types')
            and   type = 'U')
   drop table dbo.sys_temperature_types
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_test_data')
            and   name  = 'idx_wtbh'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_test_data.idx_wtbh
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_test_data')
            and   name  = 'idx_sync'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_test_data.idx_sync
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_test_data')
            and   name  = 'idx_stadiumID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_test_data.idx_stadiumID
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_test_data')
            and   name  = 'idx_moduleID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_test_data.idx_moduleID
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.sys_test_data')
            and   name  = 'idx_dataID'
            and   indid > 0
            and   indid < 255)
   drop index dbo.sys_test_data.idx_dataID
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_test_data')
            and   type = 'U')
   drop table dbo.sys_test_data
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_test_overtime')
            and   type = 'U')
   drop table dbo.sys_test_overtime
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_update')
            and   type = 'U')
   drop table dbo.sys_update
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_update_catch')
            and   type = 'U')
   drop table dbo.sys_update_catch
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_updaterfiletable')
            and   type = 'U')
   drop table dbo.sys_updaterfiletable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_users_testroom')
            and   type = 'U')
   drop table dbo.sys_users_testroom
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sysdiagrams')
            and   type = 'U')
   drop table dbo.sysdiagrams
go

execute sp_revokedbaccess dbo
go

/*==============================================================*/
/* User: dbo                                                    */
/*==============================================================*/
execute sp_grantdbaccess dbo
go

/*==============================================================*/
/* Table: Sys_Tree                                              */
/*==============================================================*/
create table dbo.Sys_Tree (
   NodeCode             varchar(50)          collate Chinese_PRC_CI_AS not null,
   DESCRIPTION          varchar(50)          collate Chinese_PRC_CI_AS null,
   DepType              varchar(50)          collate Chinese_PRC_CI_AS null,
   OrderID              varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Sys_Users                                             */
/*==============================================================*/
create table dbo.Sys_Users (
   ID                   uniqueidentifier     not null,
   Account              varchar(32)          collate Chinese_PRC_CI_AS not null,
   Password             varchar(128)         collate Chinese_PRC_CI_AS not null,
   Code                 varchar(32)          collate Chinese_PRC_CI_AS not null constraint DF_Sys_Users_Code default '-1',
   LastEditedTime       datetime             not null constraint DF_Sys_Users_LastEditedTime default getdate(),
   State                int                  not null,
   constraint PK_Sys_Users primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: bhz_Mix                                               */
/*==============================================================*/
create table dbo.bhz_Mix (
   ID                   int                  identity(1, 1),
   PeiBiCode            nvarchar(64)         collate Chinese_PRC_CI_AS null,
   ShuiNi1              decimal(18,4)        null,
   ShuiNi2              decimal(18,4)        null,
   ChanHeLiao1          decimal(18,4)        null,
   ChanHeLiao2          decimal(18,4)        null,
   XiGuLiao             decimal(18,4)        null,
   CuGuLiao1            decimal(18,4)        null,
   CuGuLiao2            decimal(18,4)        null,
   CuGuLiao3            decimal(18,4)        null,
   CuGuLiao4            decimal(18,4)        null,
   WaiJiaJi1            decimal(18,4)        null,
   WaiJiaJi2            decimal(18,4)        null,
   Shui                 decimal(18,4)        null,
   TestCode             nvarchar(64)         collate Chinese_PRC_CI_AS null,
   QiangDuDengJi        nvarchar(64)         collate Chinese_PRC_CI_AS null,
   TanLuoDu             nvarchar(64)         collate Chinese_PRC_CI_AS null,
   ProjectName          sysname              collate Chinese_PRC_CI_AS null,
   ShiGongBuWei         nvarchar(256)        collate Chinese_PRC_CI_AS null,
   ShengChanDate        datetime             null,
   FangLiang            decimal(18,2)        null,
   constraint PK_bhz_Mix primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '主键，自增',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '配合比编号',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'PeiBiCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '水泥',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'ShuiNi1'
go

execute sp_addextendedproperty 'MS_Description', 
   '掺合料1（粉煤灰/矿粉）',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'ChanHeLiao1'
go

execute sp_addextendedproperty 'MS_Description', 
   '掺合料2（粉煤灰/矿粉）',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'ChanHeLiao2'
go

execute sp_addextendedproperty 'MS_Description', 
   '细骨料',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'XiGuLiao'
go

execute sp_addextendedproperty 'MS_Description', 
   '粗骨料1',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'CuGuLiao1'
go

execute sp_addextendedproperty 'MS_Description', 
   '粗骨料2',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'CuGuLiao2'
go

execute sp_addextendedproperty 'MS_Description', 
   '粗骨料3',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'CuGuLiao3'
go

execute sp_addextendedproperty 'MS_Description', 
   '外加剂1',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'WaiJiaJi1'
go

execute sp_addextendedproperty 'MS_Description', 
   '外加剂2',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'WaiJiaJi2'
go

execute sp_addextendedproperty 'MS_Description', 
   '水',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'Shui'
go

execute sp_addextendedproperty 'MS_Description', 
   '实验室编号',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'TestCode'
go

execute sp_addextendedproperty 'MS_Description', 
   '设计强度等级',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'QiangDuDengJi'
go

execute sp_addextendedproperty 'MS_Description', 
   '允许坍落度',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'TanLuoDu'
go

execute sp_addextendedproperty 'MS_Description', 
   '工程名称',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'ProjectName'
go

execute sp_addextendedproperty 'MS_Description', 
   '施工部位',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'ShiGongBuWei'
go

execute sp_addextendedproperty 'MS_Description', 
   '生产日期',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'ShengChanDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '每盘方量',
   'user', 'dbo', 'table', 'bhz_Mix', 'column', 'FangLiang'
go

/*==============================================================*/
/* Table: bhz_sys_BaseLine_Users                                */
/*==============================================================*/
create table dbo.bhz_sys_BaseLine_Users (
   ID                   int                  identity(1, 1),
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LineID               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_bhz_sys_BaseLine_Users primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: bhz_sys_BaseUsers                                     */
/*==============================================================*/
create table dbo.bhz_sys_BaseUsers (
   id                   int                  identity(1, 1),
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Password             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  null,
   TrueName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LineID               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Descrption           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RoleName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_bhz_sys_BaseUsers primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: biz_ZJ_JZ_Summary                                     */
/*==============================================================*/
create table dbo.biz_ZJ_JZ_Summary (
   ID                   bigint               identity(1, 1),
   BGRQ                 datetime             not null,
   ModelIndex           varchar(50)          collate Chinese_PRC_CI_AS not null,
   ModelName            varchar(50)          collate Chinese_PRC_CI_AS null,
   TestRoomCode         varchar(50)          collate Chinese_PRC_CI_AS not null,
   ZjCount              int                  not null constraint DF_biz_ZJ_JZ_Summary_ZjCount default (0),
   JzCount              int                  not null constraint DF_biz_ZJ_JZ_Summary_JzCount default (0),
   JLCompanyName        varchar(50)          collate Chinese_PRC_CI_AS null,
   JLCompanyCode        varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK_biz_ZJ_JZ_Summary primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: biz_fail                                              */
/*==============================================================*/
create table dbo.biz_fail (
   failtestcode         varchar(50)          collate Chinese_PRC_CI_AS null,
   failmodelindex       varchar(50)          collate Chinese_PRC_CI_AS null,
   FaliSCTS             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   FaliBGRQ             datetime             null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: biz_machinelist                                       */
/*==============================================================*/
create table dbo.biz_machinelist (
   ID                   bigint               identity(1, 1),
   SCTS                 datetime             null,
   SCPT                 varchar(50)          collate Chinese_PRC_CI_AS null,
   SCCT                 varchar(50)          collate Chinese_PRC_CI_AS null,
   SCCS                 varchar(50)          collate Chinese_PRC_CI_AS null,
   col_norm_A6          int                  null,
   col_norm_B6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_C6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_D6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_E6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_F6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_G6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_H6          datetime             null,
   col_norm_I6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_J6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_K6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_L6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_M6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_N6          datetime             null,
   col_norm_O6          datetime             null,
   col_norm_P6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   col_norm_Q6          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   constraint PK__biz_machinelist__3AB982EA primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: biz_px_relation                                       */
/*==============================================================*/
create table dbo.biz_px_relation (
   ID                   bigint               identity(1, 1),
   SGDataID             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PXDataID             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SGTestRoomCode       nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PXTestRoomCode       nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PXTime               datetime             not null,
   constraint PK_biz_px_relation primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: biz_px_relation_web                                   */
/*==============================================================*/
create table dbo.biz_px_relation_web (
   ID                   bigint               identity(1, 1),
   ModelIndex           varchar(50)          collate Chinese_PRC_CI_AS null,
   SGDataID             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PXDataID             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SGTestRoomCode       nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PXTestRoomCode       nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PXTime               datetime             not null,
   PXBGRQ               datetime             null,
   SGBGRQ               datetime             null,
   constraint PK_biz_px_relation_web primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: biz_qxzl                                              */
/*==============================================================*/
create table dbo.biz_qxzl (
   testcode             varchar(50)          collate Chinese_PRC_CI_AS null,
   modelindex           varchar(50)          collate Chinese_PRC_CI_AS null,
   ToatlSCTS            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ToatlBaRQ            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   company              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   companycode          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   segment              nvarchar(50)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: biz_tongqiangdu                                       */
/*==============================================================*/
create table dbo.biz_tongqiangdu (
   ModelID              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ModelName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   OrderID              int                  null,
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS null,
   cType                nvarchar(80)         collate Chinese_PRC_CI_AS null,
   sDate                datetime             null,
   sZJRQ                datetime             null,
   sPlace               nvarchar(4000)       collate Chinese_PRC_CI_AS null,
   sValue1              nvarchar(80)         collate Chinese_PRC_CI_AS null,
   sValue2              nvarchar(80)         collate Chinese_PRC_CI_AS null,
   sValue3              nvarchar(80)         collate Chinese_PRC_CI_AS null,
   sValue4              nvarchar(80)         collate Chinese_PRC_CI_AS null,
   sAge                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   sTestCode            varchar(50)          collate Chinese_PRC_CI_AS null,
   segment              varchar(50)          collate Chinese_PRC_CI_AS null,
   company              varchar(50)          collate Chinese_PRC_CI_AS null,
   testroom             varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: check_table                                           */
/*==============================================================*/
create table dbo.check_table (
   modexindex           varchar(50)          collate Chinese_PRC_CI_AS null,
   modelname            varchar(50)          collate Chinese_PRC_CI_AS null,
   errornumber          varchar(50)          collate Chinese_PRC_CI_AS null,
   errorseverity        varchar(50)          collate Chinese_PRC_CI_AS null,
   errorstate           varchar(50)          collate Chinese_PRC_CI_AS null,
   errormessage         varchar(5000)        collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_BaseLine_Users                                    */
/*==============================================================*/
create table dbo.sys_BaseLine_Users (
   ID                   int                  identity(1, 1),
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LineID               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_BaseLine_Users primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '用户名',
   'user', 'dbo', 'table', 'sys_BaseLine_Users', 'column', 'UserName'
go

execute sp_addextendedproperty 'MS_Description', 
   'Key值',
   'user', 'dbo', 'table', 'sys_BaseLine_Users', 'column', 'LineID'
go

/*==============================================================*/
/* Table: sys_BaseLine_UsersLoginLog                            */
/*==============================================================*/
create table dbo.sys_BaseLine_UsersLoginLog (
   ID                   bigint               identity(1, 1),
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IpAddress            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LoginTime            datetime             null,
   LineName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Desic                nvarchar(50)         collate Chinese_PRC_CI_AS null,
   biz1                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   biz2                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_BaseLine_UsersLoginLog primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_BaseUsers                                         */
/*==============================================================*/
create table dbo.sys_BaseUsers (
   id                   int                  identity(1, 1),
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Password             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  null,
   TrueName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LineID               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Descrption           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RoleName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_BaseUsers primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_KeyModify                                         */
/*==============================================================*/
create table dbo.sys_KeyModify (
   KMID                 int                  identity(1, 1),
   DataID               uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   RequestID            uniqueidentifier     not null,
   TestRoomCode         varchar(32)          collate Chinese_PRC_CI_AS not null,
   BGBH                 varchar(256)         collate Chinese_PRC_CI_AS not null,
   DataName             varchar(256)         collate Chinese_PRC_CI_AS not null,
   ModifyItem           varchar(4000)        collate Chinese_PRC_CI_AS null,
   ModifyBy             varchar(64)          collate Chinese_PRC_CI_AS not null,
   ModifyTime           datetime             not null constraint DF__sys_KeyMo__Modif__1BFD2C07 default getdate(),
   Status               smallint             not null constraint DF__sys_KeyMo__Statu__1CF15040 default (0),
   YZUserName           varchar(64)          collate Chinese_PRC_CI_AS null,
   YZOPTime             datetime             not null constraint DF__sys_KeyMo__YZOPT__1DE57479 default getdate(),
   YZContent            varchar(4000)        collate Chinese_PRC_CI_AS null,
   JLUserName           varchar(64)          collate Chinese_PRC_CI_AS null,
   JLOPTime             datetime             not null constraint DF__sys_KeyMo__JLOPT__1ED998B2 default getdate(),
   JLContent            varchar(4000)        collate Chinese_PRC_CI_AS null,
   SGUserName           varchar(64)          collate Chinese_PRC_CI_AS null,
   SGOPTime             datetime             not null constraint DF__sys_KeyMo__SGOPT__1FCDBCEB default getdate(),
   SGContent            varchar(4000)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_KeyModify primary key (KMID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_ReditRating                                       */
/*==============================================================*/
create table dbo.sys_ReditRating (
   ID                   int                  identity(1, 1),
   SysID                varchar(50)          collate Chinese_PRC_CI_AS not null,
   SegmentCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   CompanyCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   TestRoomCode         varchar(50)          collate Chinese_PRC_CI_AS null,
   CompanyType          varchar(50)          collate Chinese_PRC_CI_AS null,
   Name                 varchar(50)          collate Chinese_PRC_CI_AS null,
   IDCard               varchar(50)          collate Chinese_PRC_CI_AS null,
   Job                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Deduct               int                  null,
   Remark               varchar(200)         collate Chinese_PRC_CI_AS null,
   CreateOn             datetime             null,
   IsDeleted            bit                  null,
   RType                varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK_sys_ReditRating primary key (SysID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_ReditRatingBHZ                                    */
/*==============================================================*/
create table dbo.sys_ReditRatingBHZ (
   ID                   int                  identity(1, 1),
   SysID                varchar(50)          collate Chinese_PRC_CI_AS not null,
   Name                 varchar(50)          collate Chinese_PRC_CI_AS null,
   IDCard               varchar(50)          collate Chinese_PRC_CI_AS null,
   Job                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Deduct               int                  null,
   Remark               ntext                collate Chinese_PRC_CI_AS null,
   CreateOn             datetime             null,
   IsDeleted            bit                  null,
   RType                varchar(50)          collate Chinese_PRC_CI_AS null,
   JLName               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGName               varchar(50)          collate Chinese_PRC_CI_AS null,
   StationName          varchar(50)          collate Chinese_PRC_CI_AS null,
   MachineCode          varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK_sys_ReditRatingBHZ primary key (SysID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_TJ_Factory                                        */
/*==============================================================*/
create table dbo.sys_TJ_Factory (
   FactoryID            uniqueidentifier     not null,
   FactoryName          varchar(100)         collate Chinese_PRC_CI_AS null,
   Address              varchar(120)         collate Chinese_PRC_CI_AS null,
   LinkMan              varchar(50)          collate Chinese_PRC_CI_AS null,
   Telephone            varchar(50)          collate Chinese_PRC_CI_AS null,
   Longitude            numeric(10,6)        not null constraint DF__sys_TJ_Fa__Longi__42865892 default (0),
   Latitude             numeric(10,6)        not null constraint DF__sys_TJ_Fa__Latit__437A7CCB default (0),
   Remark               varchar(500)         collate Chinese_PRC_CI_AS null,
   Status               tinyint              not null constraint DF__sys_TJ_Fa__Statu__446EA104 default (0),
   CreateTime           datetime             not null constraint DF__sys_TJ_Fa__Creat__4562C53D default getdate(),
   constraint PK_sys_TJ_Factory primary key (FactoryID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_TJ_Item                                           */
/*==============================================================*/
create table dbo.sys_TJ_Item (
   ItemID               uniqueidentifier     not null,
   ItemName             varchar(50)          collate Chinese_PRC_CI_AS null,
   Columns              varchar(4000)        collate Chinese_PRC_CI_AS null,
   ItemType             tinyint              not null constraint DF__sys_TJ_It__ItemT__483F31E8 default (0),
   Weight               smallint             not null constraint DF__sys_TJ_It__Weigh__49335621 default (10),
   Status               tinyint              not null constraint DF__sys_TJ_It__Statu__4A277A5A default (1),
   constraint PK_sys_TJ_Item primary key (ItemID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_TJ_Item_Module                                    */
/*==============================================================*/
create table dbo.sys_TJ_Item_Module (
   ID                   int                  identity(1, 1),
   ItemID               uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   constraint PK_sys_TJ_Item_Module primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_TJ_MainData                                       */
/*==============================================================*/
create table dbo.sys_TJ_MainData (
   DataID               uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   FactoryID            varchar(100)         collate Chinese_PRC_CI_AS null,
   SegmentCode          varchar(32)          collate Chinese_PRC_CI_AS null,
   CompanyCode          varchar(32)          collate Chinese_PRC_CI_AS null,
   TestRoomCode         varchar(32)          collate Chinese_PRC_CI_AS null,
   ItemID               uniqueidentifier     not null,
   ItemType             tinyint              not null constraint DF__sys_TJ_Ma__ItemT__610ADFB2 default (0),
   FactoryName          varchar(100)         collate Chinese_PRC_CI_AS null,
   BGBH                 varchar(256)         collate Chinese_PRC_CI_AS null,
   BGRQ                 datetime             null,
   QDDJ                 varchar(64)          collate Chinese_PRC_CI_AS null,
   PZDJ                 varchar(50)          collate Chinese_PRC_CI_AS null,
   SGBW                 varchar(500)         collate Chinese_PRC_CI_AS null,
   ShuLiang             decimal(18,6)        null constraint DF__sys_TJ_Ma__ShuLi__61FF03EB default (0),
   ZuZhi                float                null constraint DF__sys_TJ_Ma__ZuZhi__62F32824 default (0),
   f1                   float                null,
   f2                   float                null,
   f3                   float                null,
   f4                   float                null,
   f5                   float                null,
   f6                   float                null,
   f7                   float                null,
   f8                   float                null,
   f9                   float                null,
   f10                  float                null,
   f11                  float                null,
   f12                  float                null,
   f13                  float                null,
   f14                  float                null,
   f15                  float                null,
   f16                  float                null,
   f17                  float                null,
   f18                  float                null,
   f19                  float                null,
   d1                   datetime             null,
   d2                   datetime             null,
   d3                   datetime             null,
   d4                   datetime             null,
   d5                   datetime             null,
   t1                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t2                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t3                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t4                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t5                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t6                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t7                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t8                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t9                   varchar(200)         collate Chinese_PRC_CI_AS null,
   t10                  varchar(200)         collate Chinese_PRC_CI_AS null,
   t11                  varchar(200)         collate Chinese_PRC_CI_AS null,
   t12                  varchar(200)         collate Chinese_PRC_CI_AS null,
   t13                  varchar(200)         collate Chinese_PRC_CI_AS null,
   t14                  varchar(200)         collate Chinese_PRC_CI_AS null,
   t15                  varchar(200)         collate Chinese_PRC_CI_AS null,
   t16                  varchar(200)         collate Chinese_PRC_CI_AS null,
   LasteditedTime       datetime             null,
   constraint PK_sys_TJ_MainData primary key (DataID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_TJ_StandardValue                                  */
/*==============================================================*/
create table dbo.sys_TJ_StandardValue (
   ItemID               uniqueidentifier     not null,
   ItemName             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Model                nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   StandardValue        nvarchar(100)        collate Chinese_PRC_CI_AS null,
   IndexID              uniqueidentifier     not null,
   ModuleID             uniqueidentifier     null,
   constraint PK__sys_TJ_S__40BC8AA153B0E494 primary key (IndexID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_CustomerService_UpdateInfo                   */
/*==============================================================*/
create table dbo.sys_auth_CustomerService_UpdateInfo (
   ReasonID             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   WTBH                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   UpdateUser           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SQUser               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SPUser               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ReasonContent        nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ReasonIamge          image                null,
   ReasonRemark         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RecoderTime          datetime             null,
   OptionType           int                  null,
   constraint PK_sys_auth_CustomerService_UpdateInfo primary key (ReasonID)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '操作类型：1为修改龄期；2为平行修复；3为用户同步；4：用户登录',
   'user', 'dbo', 'table', 'sys_auth_CustomerService_UpdateInfo', 'column', 'OptionType'
go

/*==============================================================*/
/* Table: sys_auth_Customer_Service_Users                       */
/*==============================================================*/
create table dbo.sys_auth_Customer_Service_Users (
   ID                   bigint               identity(1, 1),
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   UserPwd              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_auth_Customer_Service_Users primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_DataPermission                               */
/*==============================================================*/
create table dbo.sys_auth_DataPermission (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   TableID              nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   FieldName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   FieldValues          nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   constraint PK_sys_auth_DataPermission primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_FieldPermission                              */
/*==============================================================*/
create table dbo.sys_auth_FieldPermission (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   FieldsID             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Indentity            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(200)        collate Chinese_PRC_CI_AS null,
   Editable             bit                  null,
   Viewable             bit                  null,
   constraint PK_sys_auth_FieldPermission primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_FunctionPermission                           */
/*==============================================================*/
create table dbo.sys_auth_FunctionPermission (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   FunctionsID          nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Indentity            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(200)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_auth___Scdel__2FA7C2F1 default (0),
   constraint PK_sys_auth_FunctionPermission primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_Permissions                                  */
/*==============================================================*/
create table dbo.sys_auth_Permissions (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   ModuleID             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   ClsInfo              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_auth___Scdel__2EB39EB8 default (0),
   constraint PK_sys_auth_Permissions primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_RecordPermission                             */
/*==============================================================*/
create table dbo.sys_auth_RecordPermission (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   RecordsID            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Indentity            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(200)        collate Chinese_PRC_CI_AS null,
   RecordCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_auth___Scdel__309BE72A default (0),
   constraint PK_sys_auth_RecordPermission primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_Roles                                        */
/*==============================================================*/
create table dbo.sys_auth_Roles (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   Name                 nvarchar(200)        collate Chinese_PRC_CI_AS null,
   Code                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Permissions          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   IsAdministrator      bit                  null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_auth___Scdel__2CCB5646 default (0),
   constraint PK_sys_auth_Roles primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_Users                                        */
/*==============================================================*/
create table dbo.sys_auth_Users (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   Code                 nvarchar(300)        collate Chinese_PRC_CI_AS null,
   UserName             nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Password             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Roles                nvarchar(200)        collate Chinese_PRC_CI_AS null,
   IsSys                bit                  null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_auth___Scdel__2DBF7A7F default (0),
   Devices              nvarchar(500)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_auth_Users primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_auth_UsersDevices                                 */
/*==============================================================*/
create table dbo.sys_auth_UsersDevices (
   ID                   uniqueidentifier     not null,
   UserName             nvarchar(100)        collate Chinese_PRC_CI_AS null,
   DeviceESN            varchar(100)         collate Chinese_PRC_CI_AS null,
   TSClientID           varchar(50)          collate Chinese_PRC_CI_AS null,
   LastLoginTime        datetime             not null constraint DF__sys_auth___LastL__4F7CD00D default getdate(),
   CreateTime           datetime             not null constraint DF__sys_auth___Creat__5070F446 default getdate(),
   constraint PK_SYS_AUTH_USERSDEVICES primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_Cache                                         */
/*==============================================================*/
create table dbo.sys_biz_Cache (
   ID                   varchar(50)          collate Chinese_PRC_CI_AS not null constraint DF_sys_biz_Cache_ID default newid(),
   SCTS                 datetime             null constraint DF_sys_biz_Cache_SCTS default getdate(),
   TableName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IsDel                bit                  null constraint DF_sys_biz_Cache_IsDel default (0),
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_C__Scdel__3654C080 default (0),
   constraint PK_sys_biz_Cache primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_Cache_DeletedData                             */
/*==============================================================*/
create table dbo.sys_biz_Cache_DeletedData (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null constraint DF_sys_biz_Cache_DeletedData_ID default newid(),
   Scts                 datetime             null constraint DF_sys_biz_Cache_DeletedData_Scts default getdate(),
   BizObject            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Operation            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Commands             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_biz_Cache_DeletedData primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_CrossSheetFormulas                            */
/*==============================================================*/
create table dbo.sys_biz_CrossSheetFormulas (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DF_sys_biz_CrossSheetFormulas_SCTS default getdate(),
   ModelIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SheetIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RowIndex             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ColumnIndex          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Formula              nvarchar(2000)       collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_C__Scdel__2341EC0C default (0),
   constraint PK_sys_biz_CrossSheetFormulas primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_DataArea                                      */
/*==============================================================*/
create table dbo.sys_biz_DataArea (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DF_sys_biz_DataArea_SCTS default getdate(),
   SheetID              nvarchar(36)         collate Chinese_PRC_CI_AS null,
   TableName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ColumnName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Range                nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_D__Scdel__20657F61 default (0),
   constraint PK_Sys_DataRange primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_DataModification                              */
/*==============================================================*/
create table dbo.sys_biz_DataModification (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null constraint DF_sys_biz_DataModification_ID default newid(),
   Scts                 datetime             null,
   DataID               uniqueidentifier     not null,
   Segment              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CompanyName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   TestRoomName         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SponsorPerson        nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SponsorDate          datetime             null,
   Caption              nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Reason               nvarchar(150)        collate Chinese_PRC_CI_AS null,
   ApprovePerson        nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ApproveDate          datetime             null,
   ProcessReason        nvarchar(450)        collate Chinese_PRC_CI_AS null,
   State                nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ModifyItem           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ModuleID             uniqueidentifier     null,
   ModuleName           nvarchar(256)        collate Chinese_PRC_CI_AS null,
   TestRoomCode         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_biz_DataModification primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_DataUpload                                    */
/*==============================================================*/
create table dbo.sys_biz_DataUpload (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   SCPT                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SCLU                 bit                  null,
   SCRU                 bit                  null,
   SCRC                 int                  null,
   PDFFileName          nvarchar(500)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_biz_DataUpload primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_FunctionInfos                                 */
/*==============================================================*/
create table dbo.sys_biz_FunctionInfos (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null constraint DF_sys_biz_FunctionInfos_ID default newid(),
   scts                 datetime             null constraint DF_sys_biz_FunctionInfos_scts default getdate(),
   scpt                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   AssemblyName         nvarchar(150)        collate Chinese_PRC_CI_AS null,
   FullClassName        nvarchar(150)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_F__Scdel__224DC7D3 default (0),
   constraint PK_sys_biz_FunctionInfos primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_Image                                         */
/*==============================================================*/
create table dbo.sys_biz_Image (
   ImgID                nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ImgDataID            nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ImgName              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ImgContent           image                null,
   ImgRemark            nvarchar(200)        collate Chinese_PRC_CI_AS null,
   constraint "PK_sys.biz.Image" primary key (ImgID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_Module                                        */
/*==============================================================*/
create table dbo.sys_biz_Module (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Sheets               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ExtentSheet          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_M__Scdel__1C94EE7D default (0),
   constraint PK_sys_biz_Module primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ModuleCatlog                                  */
/*==============================================================*/
create table dbo.sys_biz_ModuleCatlog (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_biz_ModuleCatlog_Scts default getdate(),
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CatlogName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_M__Scdel__1AACA60B default (0),
   constraint PK_sys_biz_PrimaryDetailsCatlog primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ModuleCatlog_update                           */
/*==============================================================*/
create table dbo.sys_biz_ModuleCatlog_update (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null,
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CatlogName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_M__Scdel__7C530774 default (0),
   constraint PK_sys_biz_PrimaryDetailsCatlog_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ModuleFormatStrings                           */
/*==============================================================*/
create table dbo.sys_biz_ModuleFormatStrings (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   FormatStyle          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   FormatString         nvarchar(200)        collate Chinese_PRC_CI_AS null,
   constraint PK__sys_biz_ReportFo__5CA84D83 primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ReportCatlog                                  */
/*==============================================================*/
create table dbo.sys_biz_ReportCatlog (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null,
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CatlogName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_biz_ReportCatlog primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ReportFormatStrings                           */
/*==============================================================*/
create table dbo.sys_biz_ReportFormatStrings (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_biz_ReportFormatStrings_Scts default getdate(),
   FormatStyle          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   FormatString         nvarchar(200)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_R__Scdel__29EEE99B default (0),
   constraint PK_sys_biz_ReportFormatStrings primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ReportParameters                              */
/*==============================================================*/
create table dbo.sys_biz_ReportParameters (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DF_sys_biz_ReportParameters_SCTS default getdate(),
   ReportID             nvarchar(36)         collate Chinese_PRC_CI_AS null,
   Name                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   DisplayName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_R__Scdel__2AE30DD4 default (0),
   constraint PK_sys_biz_ReportParameters primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ReportRTConfig                                */
/*==============================================================*/
create table dbo.sys_biz_ReportRTConfig (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null constraint DF_sys_biz_ReportRTConfig_ID default newid(),
   Scts                 datetime             null constraint DF_sys_biz_ReportRTConfig_Scts default getdate(),
   GroupName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ReportName           nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_R__Scdel__28FAC562 default (0),
   constraint PK_sys_biz_ReportRTConfig primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_ReportSheet                                   */
/*==============================================================*/
create table dbo.sys_biz_ReportSheet (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DF_sys_biz_ReportSheet_SCTS default getdate(),
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null,
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SheetStyle           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   DataSources          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_R__Scdel__2BD7320D default (0),
   constraint PK_sys_biz_ReportSheet primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_Reporttables                                  */
/*==============================================================*/
create table dbo.sys_biz_Reporttables (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null constraint DF_sys_biz_Reporttables_ID default newid(),
   Scts                 datetime             null constraint DF_sys_biz_Reporttables_Scts default getdate(),
   TableName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_R__Scdel__2806A129 default (0),
   constraint PK_sys_biz_Reporttables primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_Sheet                                         */
/*==============================================================*/
create table dbo.sys_biz_Sheet (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DF_sys_biz_Sheet_SCTS default getdate(),
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   DataTable            nvarchar(200)        collate Chinese_PRC_CI_AS null,
   SheetStyle           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_S__Scdel__1D8912B6 default (0),
   constraint PK_sys_biz_PrimaryDetailsSheet primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_SheetCatlog                                   */
/*==============================================================*/
create table dbo.sys_biz_SheetCatlog (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_biz_SheetCatlog_Scts default getdate(),
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CatlogName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_S__Scdel__1BA0CA44 default (0),
   constraint PK_sys_biz_SheetCatlog primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_SheetCatlog_update                            */
/*==============================================================*/
create table dbo.sys_biz_SheetCatlog_update (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null,
   CatlogCode           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CatlogName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null,
   constraint PK_sys_biz_SheetCatlog_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_moduleview                                    */
/*==============================================================*/
create table dbo.sys_biz_moduleview (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   ModuleCode           nvarchar(150)        collate Chinese_PRC_CI_AS null,
   ModuleID             nvarchar(36)         collate Chinese_PRC_CI_AS null,
   TableName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   TableText            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(200)        collate Chinese_PRC_CI_AS null,
   ContentType          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ContentFieldType     nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ContentText          nvarchar(200)        collate Chinese_PRC_CI_AS null,
   Contents             nvarchar(200)        collate Chinese_PRC_CI_AS null,
   ForeColor            int                  null,
   BgColor              int                  null,
   DisplayStyle         nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ColumnWidth          real                 null,
   IsVisible            bit                  null,
   IsEdit               bit                  null,
   IsNull               bit                  null,
   OrderIndex           smallint             null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_m__Scdel__19B881D2 default (0),
   constraint PK_sys_biz_moduleview primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_Itemcondition                        */
/*==============================================================*/
create table dbo.sys_biz_reminder_Itemcondition (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   EvaluateIndex        nvarchar(80)         collate Chinese_PRC_CI_AS null,
   Text                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Specifiedvalue       nvarchar(2500)       collate Chinese_PRC_CI_AS null,
   Truevalue            nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Expression           nvarchar(3000)       collate Chinese_PRC_CI_AS null,
   OrderIndex           tinyint              null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_r__Scdel__27127CF0 default (0),
   constraint PK_sys_biz_reminder_Itemcondition primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_Itemfrequency                        */
/*==============================================================*/
create table dbo.sys_biz_reminder_Itemfrequency (
   ID                   int                  identity(1, 1),
   ModelIndex           nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ModelCode            nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   TestRoomID           nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   TestRoomCode         nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ModelName            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   Frequency            float                not null,
   FrequencyType        tinyint              not null constraint DF_sys_biz_reminder_Itemfrequency_FrequencyType default (1),
   modifyTime           datetime             null constraint DF_sys_biz_reminder_Itemfrequency_SCTS default getdate(),
   modifyBy             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IsActive             tinyint              not null constraint DF_sys_biz_reminder_Itemfrequency_flag default (1),
   constraint PK_sys_biz_reminder_Itemfrequency_1 primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '1=平行，2=见证',
   'user', 'dbo', 'table', 'sys_biz_reminder_Itemfrequency', 'column', 'FrequencyType'
go

execute sp_addextendedproperty 'MS_Description', 
   '1=正在使用，0=不使用',
   'user', 'dbo', 'table', 'sys_biz_reminder_Itemfrequency', 'column', 'IsActive'
go

/*==============================================================*/
/* Table: sys_biz_reminder_evaluateData                         */
/*==============================================================*/
create table dbo.sys_biz_reminder_evaluateData (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null,
   ReportName           nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ReportNumber         nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ModelCode            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ModelIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_InvalidItem        nvarchar(1500)       collate Chinese_PRC_CI_AS null,
   ReportDate           datetime             null,
   SentCount            int                  not null constraint DF_sys_biz_reminder_evaluateData_SentCount default (0),
   LastSentStatus       nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   LastSentTime         datetime             null,
   AdditionalQualified  smallint             not null constraint DF_sys_biz_reminder_evaluateData_AdditionalQualified default (0),
   QualifiedTime        datetime             null,
   SGComment            nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   LastSGUser           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LastSGTime           datetime             null,
   JLComment            nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   LastJLUser           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LastJLTime           datetime             null,
   constraint PK_sys_biz_reminder_evaluateData primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_evaluatecondition                    */
/*==============================================================*/
create table dbo.sys_biz_reminder_evaluatecondition (
   ID                   varchar(80)          collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DF_sys_biz_reminder_evaluatecondition_SCTS default getdate(),
   ModelIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SheetIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ReportNumber         nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ReportDate           nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_biz_r__Scdel__261E58B7 default (0),
   constraint PK_sys_biz_reminder_evaluatecondition primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_samplingFrequency                    */
/*==============================================================*/
create table dbo.sys_biz_reminder_samplingFrequency (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   constraint PK_sys_biz_reminder_samplingFrequency primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_stadiumData                          */
/*==============================================================*/
create table dbo.sys_biz_reminder_stadiumData (
   ID                   bigint               identity(1, 1),
   DataID               nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_biz_reminder_stadiumData_Scts default getdate(),
   ModelCode            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ModelIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   DateSpan             int                  null,
   Date                 nvarchar(10)         collate Chinese_PRC_CI_AS null,
   F_Name               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_ItemId             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_PH                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_ZJRQ               datetime             null,
   F_SJBH               nvarchar(100)        collate Chinese_PRC_CI_AS null,
   F_SJSize             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_SYXM               nvarchar(80)         collate Chinese_PRC_CI_AS null,
   F_BGBH               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_WTBH               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_Added              nvarchar(100)        collate Chinese_PRC_CI_AS null,
   F_IsDone             bit                  null,
   F_ItemIndex          int                  null,
   SGComment            nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   LastSGUser           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LastSGTime           datetime             null,
   JLComment            nvarchar(1000)       collate Chinese_PRC_CI_AS null,
   LastJLUser           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LastJLTime           datetime             null,
   constraint PK_sys_biz_reminder_stadiumData primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_stadiumData_UpdateInfo               */
/*==============================================================*/
create table dbo.sys_biz_reminder_stadiumData_UpdateInfo (
   ReasonID             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   WTBH                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   UpdateUser           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SQUser               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SPUser               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ReasonContent        nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ReasonIamge          image                null,
   ReasonRemark         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RecoderTime          datetime             null,
   constraint PK_sys_biz_reminder_stadiumData_UpdateInfo primary key (ReasonID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_stadiumInfo                          */
/*==============================================================*/
create table dbo.sys_biz_reminder_stadiumInfo (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_biz_reminder_stadiumInfo_Scts default getdate(),
   F_ZJRQ               nvarchar(100)        collate Chinese_PRC_CI_AS null,
   F_Days               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   F_Columns            nvarchar(1500)       collate Chinese_PRC_CI_AS null,
   SearchRange          nvarchar(50)         collate Chinese_PRC_CI_AS not null constraint DF_sys_biz_reminder_stadiumInfo_SearchRange default '0',
   StadiumConfig        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   IsActive             smallint             not null constraint DF_sys_biz_reminder_stadiumInfo_IsActive default (0),
   constraint PK_sys_biz_reminder_StadiumInfo primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_stadiummodel                         */
/*==============================================================*/
create table dbo.sys_biz_reminder_stadiummodel (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Days                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   OrderIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_biz_reminder_stadiummodel primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_supervisionReport                    */
/*==============================================================*/
create table dbo.sys_biz_reminder_supervisionReport (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   CompanyCode          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SupervisionName      nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SheetStyle           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Scts                 datetime             null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_biz_reminder_testitem                             */
/*==============================================================*/
create table dbo.sys_biz_reminder_testitem (
   ID                   int                  not null,
   ItemName             nvarchar(255)        collate Chinese_PRC_CI_AS null,
   TestCount            tinyint              null,
   Type                 tinyint              null,
   constraint PK_sys_testitem primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_bs_users                                          */
/*==============================================================*/
create table dbo.sys_bs_users (
   ID                   uniqueidentifier     null constraint DF_sys_bs_users_ID default newid(),
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Password             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IsActive             bit                  null,
   TrueName             nvarchar(50)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_caiji_log                                         */
/*==============================================================*/
create table dbo.sys_caiji_log (
   ID                   bigint               identity(1, 1),
   TestRoomCode         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   MachineCode          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ErrMsg               nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   TimeStamp            datetime             not null constraint DF_sys_caiji_log_TimeStamp default getdate(),
   constraint PK_sys_caiji_log primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_columns                                           */
/*==============================================================*/
create table dbo.sys_columns (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DFT_SYS_COLUMNS_SCTS default getdate(),
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null constraint DFT_SYS_COLUMNS_SCPT default (0),
   DESCRIPTION          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   COLNAME              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   TABLENAME            nvarchar(100)        collate Chinese_PRC_CI_AS null,
   COLTYPE              nvarchar(20)         collate Chinese_PRC_CI_AS null,
   IsKeyField           bit                  null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_colum__Scdel__1F715B28 default (0),
   constraint PMK_SYS_000005_COLUMNS_ID primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_custom_view                                       */
/*==============================================================*/
create table dbo.sys_custom_view (
   ID                   bigint               identity(1, 1),
   ModuleID             uniqueidentifier     not null,
   TestRoomCode         nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   CustomView           nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LastEditedTime       datetime             not null,
   constraint PK_sys_custom_view primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_moduleID                                          */
/*==============================================================*/
create index idx_moduleID on dbo.sys_custom_view (
ModuleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_testRoomCode                                      */
/*==============================================================*/
create index idx_testRoomCode on dbo.sys_custom_view (
TestRoomCode ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_day                                               */
/*==============================================================*/
create table dbo.sys_day (
   DayID                datetime             not null,
   constraint PK_sys_day primary key (DayID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_devices                                           */
/*==============================================================*/
create table dbo.sys_devices (
   ID                   uniqueidentifier     not null,
   TestRoomCode         nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   MachineCode          nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   DeviceType           smallint             not null constraint DF__sys_devic__Devic__33F4B129 default (0),
   IsDYSF               smallint             not null constraint DF__sys_devic__IsDYS__34E8D562 default (0),
   RemoteCode1          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RemoteCode2          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   DeviceCompany        nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Comment              nvarchar(1024)       collate Chinese_PRC_CI_AS null,
   ClientConfig         nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ConfigUpdateTime     datetime             null,
   ConfigStatus         smallint             null constraint DF_sys_devices_ConfigStatus default (1),
   IsActive             smallint             not null constraint DF__sys_devic__IsAct__36D11DD4 default (0),
   CreateTime           datetime             not null constraint DF__sys_devic__Creat__37C5420D default getdate(),
   CreateBy             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   LastEditTime         datetime             not null constraint DF__sys_devic__LastE__38B96646 default getdate(),
   LastEditBy           nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Quantum              int                  not null constraint DF__sys_devic__Quant__39AD8A7F default (0),
   constraint PK_sys_Devices primary key (ID)
         on "PRIMARY",
   constraint UQ__sys_devi__DB84B5B8352C5D74 unique (MachineCode)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_dictionary                                        */
/*==============================================================*/
create table dbo.sys_dictionary (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DF_sys_dictionary_SCTS default getdate(),
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null constraint DF_sys_dictionary_SCPT default (0),
   CODECLASS            nvarchar(8)          collate Chinese_PRC_CI_AS null,
   DESCRIPTION          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CODE                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   CANANYLEVEL          smallint             null constraint DF_sys_dictionary_CANANYLEVEL default (0),
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_dicti__Scdel__24361045 default (0),
   constraint PK_sys_dictionary primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_codeClass                                         */
/*==============================================================*/
create index idx_codeClass on dbo.sys_dictionary (
CODECLASS ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_dictionary_update                                 */
/*==============================================================*/
create table dbo.sys_dictionary_update (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null,
   CODECLASS            nvarchar(8)          collate Chinese_PRC_CI_AS null,
   DESCRIPTION          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CODE                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   CANANYLEVEL          smallint             null,
   Scts_1               datetime             null,
   Scdel                smallint             not null,
   constraint PK_sys_dictionary_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_document                                          */
/*==============================================================*/
create table dbo.sys_document (
   ID                   uniqueidentifier     not null,
   SegmentCode          nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   CompanyCode          nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   TestRoomCode         nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   ModuleID             uniqueidentifier     not null,
   Data                 nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   DataName             nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   SetDataNameUser      nvarchar(64)         collate Chinese_PRC_CI_AS null,
   SetDataNameTime      datetime             null,
   TryType              nvarchar(16)         collate Chinese_PRC_CI_AS not null constraint DF_Table_1_TestType default '自检',
   TryPerson            nvarchar(64)         collate Chinese_PRC_CI_AS null,
   TryPersonTestRoomCode nvarchar(32)         collate Chinese_PRC_CI_AS null,
   TryTime              datetime             null,
   Status               smallint             not null constraint DF_sys_data_IsActive default (1),
   NeedUpload           bit                  not null constraint DF_sys_document_NeedUpload default (1),
   WillUploadCount      int                  not null constraint DF_sys_document_WillUploadedCount default (1),
   ShuLiang             decimal(18,6)        null constraint DF_sys_data_ShuLiang default (0),
   QDDJ                 nvarchar(64)         collate Chinese_PRC_CI_AS null,
   BGRQ                 datetime             null,
   WTBH                 nvarchar(256)        collate Chinese_PRC_CI_AS null,
   BGBH                 nvarchar(256)        collate Chinese_PRC_CI_AS null,
   Ext1                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext2                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext3                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext4                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext5                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext6                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext7                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext8                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext9                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext10                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext11                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext12                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext13                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext14                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext15                nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Ext16                decimal(18,6)        null,
   Ext17                decimal(18,6)        null,
   Ext18                decimal(18,6)        null,
   Ext19                decimal(18,6)        null,
   Ext20                decimal(18,6)        null,
   Ext21                datetime             null,
   Ext22                datetime             null,
   Ext23                datetime             null,
   MachineCode          sysname              collate Chinese_PRC_CI_AS null,
   LastEditedTime       datetime             null,
   IsLock               bit                  not null constraint DF__sys_docum__IsLoc__0A41FF6F default (0),
   GGCNeedUpload        smallint             not null constraint DF__sys_docum__GGCNe__2E7F5FE5 default (0),
   constraint PK_sys_document primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '0表示删除；1正式提交；',
   'user', 'dbo', 'table', 'sys_document', 'column', 'Status'
go

/*==============================================================*/
/* Index: idx_bgbh                                              */
/*==============================================================*/
create index idx_bgbh on dbo.sys_document (
TestRoomCode ASC,
BGBH ASC,
Status ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_index                                             */
/*==============================================================*/
create index idx_index on dbo.sys_document (
TestRoomCode ASC,
ModuleID ASC,
Status ASC,
CreatedTime DESC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_list                                              */
/*==============================================================*/
create index idx_list on dbo.sys_document (
TestRoomCode ASC,
CreatedTime DESC,
ModuleID ASC,
Status ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_moduleID                                          */
/*==============================================================*/
create index idx_moduleID on dbo.sys_document (
ModuleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_testRoomCode                                      */
/*==============================================================*/
create index idx_testRoomCode on dbo.sys_document (
TestRoomCode ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_trytype                                           */
/*==============================================================*/
create index idx_trytype on dbo.sys_document (
TryType ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_wtbh                                              */
/*==============================================================*/
create index idx_wtbh on dbo.sys_document (
TestRoomCode ASC,
WTBH ASC,
Status ASC,
CreatedTime DESC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_document_ext                                      */
/*==============================================================*/
create table dbo.sys_document_ext (
   ID                   uniqueidentifier     not null,
   TemperatureType      smallint             not null constraint DF__sys_docum__Tempe__179BFA8D default (0),
   constraint PK_sys_document_ext primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_engs_CompanyInfo                                  */
/*==============================================================*/
create table dbo.sys_engs_CompanyInfo (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_engs_CompanyInfo_Scts default getdate(),
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   DepType              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   DepAbbrev            nvarchar(20)         collate Chinese_PRC_CI_AS null,
   ConstructionCompany  nvarchar(500)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_engs___Scdel__346C780E default (0),
   constraint PK_sys_engs_orginfo primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_engs_ItemInfo                                     */
/*==============================================================*/
create table dbo.sys_engs_ItemInfo (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_engs_ItemInfo_Scts default getdate(),
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ItemType             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_engs___Scdel__35609C47 default (0),
   X                    nvarchar(20)         collate Chinese_PRC_CI_AS null,
   Y                    nvarchar(20)         collate Chinese_PRC_CI_AS null,
   ZoomLevel            int                  null,
   constraint PK_sys_engs_prjmachine_1 primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_engs_ProjectInfo                                  */
/*==============================================================*/
create table dbo.sys_engs_ProjectInfo (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_engs_ProjectInfo_Scts default getdate(),
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ShortName            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   PegFrom              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   PegTo                nvarchar(50)         collate Chinese_PRC_CI_AS null,
   LineName             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   HigWayClassification nvarchar(50)         collate Chinese_PRC_CI_AS null,
   TotalDistance        decimal(18,2)        null,
   ToltalPrice          decimal(18,2)        null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_engs___Scdel__32842F9C default (0),
   constraint PK_sys_engs_PrjItem primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_engs_SectionInfo                                  */
/*==============================================================*/
create table dbo.sys_engs_SectionInfo (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_engs_SectionInfo_Scts default getdate(),
   Description          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   PegFrom              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   PegTo                nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Price                decimal(18,2)        null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_engs___Scdel__337853D5 default (0),
   constraint PK_sys_engs_Prjsct primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_engs_Tree                                         */
/*==============================================================*/
create table dbo.sys_engs_Tree (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_engs_Tree_Scts default getdate(),
   NodeCode             nvarchar(300)        collate Chinese_PRC_CI_AS null,
   NodeType             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RalationID           nvarchar(36)         null,
   Scts_1               datetime             null,
   Scdel                smallint             not null default (0),
   constraint PK_sys_engs_PrjItemTree primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_engs_Tree_Chart                                   */
/*==============================================================*/
create table dbo.sys_engs_Tree_Chart (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null constraint DF_sys_engs_Tree_Chart_ID default newid(),
   scts                 datetime             null constraint DF_sys_engs_Tree_Chart_scts default getdate(),
   NodeFlag             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   NodeCode             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ReportID             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ReportName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_engs_Tree_Chart primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_equipment_code                                    */
/*==============================================================*/
create table dbo.sys_equipment_code (
   DataID               nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   SysCode              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   InfoCenterCode       varbinary(50)        null,
   ETCCode              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_equipment_code primary key (DataID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_formulas                                          */
/*==============================================================*/
create table dbo.sys_formulas (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   SheetID              uniqueidentifier     not null,
   RowIndex             int                  not null,
   ColumnIndex          int                  not null,
   Formula              nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LasteditedTime       datetime             not null,
   IsActive             smallint             not null constraint DF_sys_formulas_IsActive default (1),
   constraint PK_sys_formulas primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_moduleID                                          */
/*==============================================================*/
create index idx_moduleID on dbo.sys_formulas (
ModuleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_sheetID                                           */
/*==============================================================*/
create index idx_sheetID on dbo.sys_formulas (
SheetID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_formulas_update                                   */
/*==============================================================*/
create table dbo.sys_formulas_update (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   SheetID              uniqueidentifier     not null,
   RowIndex             int                  not null,
   ColumnIndex          int                  not null,
   Formula              nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LasteditedTime       datetime             not null,
   IsActive             smallint             not null,
   constraint PK_sys_formulas_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_ggc_LabTypeCode                                   */
/*==============================================================*/
create table dbo.sys_ggc_LabTypeCode (
   Code                 varchar(10)          collate Chinese_PRC_CI_AS not null,
   CodeName             varchar(100)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_ggc_LabTypeCode primary key (Code)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_ggc_UserAuth                                      */
/*==============================================================*/
create table dbo.sys_ggc_UserAuth (
   IDCARDNUM            varchar(30)          collate Chinese_PRC_CI_AS not null,
   YHMC                 varchar(20)          collate Chinese_PRC_CI_AS null,
   LABKEY               varchar(50)          collate Chinese_PRC_CI_AS null,
   LABIV                varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK_sys_ggc_UserAuth primary key (IDCARDNUM)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_invalid_document                                  */
/*==============================================================*/
create table dbo.sys_invalid_document (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   TestRoomCode         nvarchar(32)         not null,
   BGBH                 nvarchar(256)        null,
   BGRQ                 datetime             null,
   Status               smallint             not null default (0),
   LastEditedTime       datetime             not null default getdate(),
   F_InvalidItem        nvarchar(2048)       null,
   SentCount            int                  not null default (0),
   LastSentStatus       nvarchar(Max)        collate Chinese_PRC_CI_AS not null default '',
   LastSentTime         datetime             null,
   AdditionalQualified  smallint             not null default (0),
   QualifiedTime        datetime             null,
   SGComment            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LastSGUser           nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastSGTime           datetime             null,
   JLComment            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LastJLUser           nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastJLTime           datetime             null,
   DealResult           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   DealTime             datetime             null,
   DealUser             nvarchar(64)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_invalid_document primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_invalid_document_update                           */
/*==============================================================*/
create table dbo.sys_invalid_document_update (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   TestRoomCode         nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   BGBH                 nvarchar(256)        collate Chinese_PRC_CI_AS null,
   BGRQ                 datetime             null,
   Status               smallint             not null constraint DF__sys_inval__Statu__7917736D default (0),
   LastEditedTime       datetime             not null constraint DF__sys_inval__LastE__7A0B97A6 default getdate(),
   F_InvalidItem        nvarchar(2048)       collate Chinese_PRC_CI_AS null,
   SentCount            int                  not null constraint DF__sys_inval__SentC__7AFFBBDF default (0),
   LastSentStatus       nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   LastSentTime         datetime             null,
   AdditionalQualified  smallint             not null constraint DF__sys_inval__Addit__7CE80451 default (0),
   QualifiedTime        datetime             null,
   SGComment            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LastSGUser           nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastSGTime           datetime             null,
   JLComment            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LastJLUser           nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastJLTime           datetime             null,
   DealResult           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   DealTime             datetime             null,
   DealUser             nvarchar(64)         collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_line                                              */
/*==============================================================*/
create table dbo.sys_line (
   ID                   uniqueidentifier     not null,
   LineName             nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   Description          nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   IPAddress            nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   Port                 nvarchar(8)          collate Chinese_PRC_CI_AS not null,
   DataSourceAddress    nvarchar(32)         collate Chinese_PRC_CI_AS null,
   UserName             nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   PassWord             nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   DataBaseName         sysname              collate Chinese_PRC_CI_AS not null,
   StartUpload          smallint             not null constraint DF_sys_line_StartUpload default (0),
   UploadAddress        nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   TestRoomCodeMap      nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   ModuleCodeMap        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   JSDWCode             sysname              collate Chinese_PRC_CI_AS not null,
   IsActive             smallint             not null constraint DF_sys_line_IsActive default (1),
   Domain               nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IsBHZ                int                  null,
   MapJson              nvarchar(2000)       collate Chinese_PRC_CI_AS null,
   TestMapJson          nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   LinesJson            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   GGCStartUpload       bit                  not null constraint DF__sys_line__GGCSta__2C971773 default (0),
   MQWSStart            tinyint              not null constraint DF__sys_line__MQWSSt__3808CA1F default (0),
   "Statistics"         smallint             not null constraint DF__sys_line__Statis__39F11291 default (1),
   constraint PK_sys_line primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_line_cellstyle                                    */
/*==============================================================*/
create table dbo.sys_line_cellstyle (
   CSID                 int                  identity(1, 1),
   SheetID              uniqueidentifier     not null,
   CellName             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   CellStyle            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_line_cellstyle primary key (CSID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_line_formulas                                     */
/*==============================================================*/
create table dbo.sys_line_formulas (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   SheetID              uniqueidentifier     not null,
   RowIndex             int                  not null,
   ColumnIndex          int                  not null,
   Formula              nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LasteditedTime       datetime             not null,
   IsActive             smallint             not null constraint DF_sys_line_formulas_IsActive default (1),
   constraint PK_sys_line_formulas primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_line_formulas_update                              */
/*==============================================================*/
create table dbo.sys_line_formulas_update (
   ID                   uniqueidentifier     not null,
   LineID               uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   SheetID              uniqueidentifier     not null,
   RowIndex             int                  not null,
   ColumnIndex          int                  not null,
   Formula              nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LasteditedTime       datetime             not null,
   IsActive             smallint             not null,
   constraint PK_sys_line_formulas_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_line_module                                       */
/*==============================================================*/
create table dbo.sys_line_module (
   ID                   int                  identity(1, 1),
   LineID               uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   IsModule             tinyint              not null constraint DF_sys_line_module_IsModule default (0),
   constraint PK_sys_line_module primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_log                                               */
/*==============================================================*/
create table dbo.sys_log (
   Date                 datetime             null,
   Thread               nvarchar(255)        collate Chinese_PRC_CI_AS null,
   Level                nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Logger               nvarchar(255)        collate Chinese_PRC_CI_AS null,
   Message              nvarchar(4000)       collate Chinese_PRC_CI_AS null,
   Exception            nvarchar(2000)       collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_loginlog                                          */
/*==============================================================*/
create table dbo.sys_loginlog (
   ID                   bigint               identity(1, 1),
   loginDay             nvarchar(10)         collate Chinese_PRC_CI_AS not null,
   ipAddress            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   macAddress           nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   machineName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   osVersion            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   osUserName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   UserName             nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ProjectName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SegmentName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CompanyName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   TestRoomName         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   TestRoomCode         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   FirstAccessTime      datetime             null,
   LastAccessTime       datetime             null,
   constraint PK_sys_loginlog_1 primary key (loginDay, macAddress, UserName)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_module                                            */
/*==============================================================*/
create table dbo.sys_module (
   ID                   uniqueidentifier     not null,
   Name                 nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   Description          nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   CatlogCode           nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   ModuleSetting        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   QualifySetting       nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   UploadSetting        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   ModuleType           smallint             not null constraint DF_sys_module_ModuleType default (1),
   DeviceType           smallint             not null constraint DF_sys_module_DeviceType default (1),
   ModuleALT            nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LastEditedTime       datetime             not null,
   IsActive             smallint             not null constraint DF_sys_module_IsActive default (1),
   ReportIndex          int                  not null constraint DF__sys_modul__Repor__3284FAF3 default (0),
   ModuleALTGG          varchar(10)          collate Chinese_PRC_CI_AS null,
   GGCUploadSetting     varchar(4000)        collate Chinese_PRC_CI_AS null,
   GGCDocUploadSetting  varchar(4000)        collate Chinese_PRC_CI_AS null,
   StatisticsMap        nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   StatisticsCatlog     nvarchar(20)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_module primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_module_config                                     */
/*==============================================================*/
create table dbo.sys_module_config (
   ID                   uniqueidentifier     not null constraint DF_sys_module_config_ID default newid(),
   ModuleID             uniqueidentifier     not null,
   SerialNumber         int                  not null,
   Config               nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   IsActive             smallint             not null constraint DF_sys_module_config_IsActive default (1),
   constraint PK_sys_module_config primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_moduleID                                          */
/*==============================================================*/
create index idx_moduleID on dbo.sys_module_config (
ModuleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_module_config_update                              */
/*==============================================================*/
create table dbo.sys_module_config_update (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   SerialNumber         int                  not null,
   Config               nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   IsActive             smallint             not null,
   constraint PK_sys_module_config_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_module_sheet                                      */
/*==============================================================*/
create table dbo.sys_module_sheet (
   ID                   uniqueidentifier     not null constraint DF_sys_module_sheet_ID default newid(),
   ModuleID             uniqueidentifier     not null,
   SheetID              uniqueidentifier     not null,
   SortIndex            int                  not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LastEditedTime       datetime             not null,
   constraint PK_sys_module_sheet primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_module_sheet_update                               */
/*==============================================================*/
create table dbo.sys_module_sheet_update (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   SheetID              uniqueidentifier     not null,
   SortIndex            int                  not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LastEditedTime       datetime             not null,
   constraint PK_sys_module_sheet_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_module_update                                     */
/*==============================================================*/
create table dbo.sys_module_update (
   ID                   uniqueidentifier     not null,
   Name                 nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   Description          nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   CatlogCode           nvarchar(32)         collate Chinese_PRC_CI_AS null,
   ModuleSetting        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   QualifySetting       nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   UploadSetting        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   ModuleType           smallint             not null,
   DeviceType           smallint             not null,
   ModuleALT            nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LastEditedTime       datetime             not null,
   IsActive             smallint             not null,
   ReportIndex          int                  null,
   ModuleALTGG          varchar(10)          collate Chinese_PRC_CI_AS null,
   GGCUploadSetting     varchar(4000)        collate Chinese_PRC_CI_AS null,
   GGCDocUploadSetting  varchar(4000)        collate Chinese_PRC_CI_AS null,
   StatisticsMap        nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   StatisticsCatlog     nvarchar(20)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_module_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_module_watermark                                  */
/*==============================================================*/
create table dbo.sys_module_watermark (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   SheetID              uniqueidentifier     not null,
   FileName             nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   WLeft                int                  not null,
   WTop                 int                  not null,
   Width                int                  not null,
   Height               int                  not null,
   WOpacity             int                  not null,
   IsActive             smallint             not null constraint DF_sys_module_watermark_IsActive default (1),
   constraint PK_sys_module_watermark primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_moduleview                                        */
/*==============================================================*/
create table dbo.sys_moduleview (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null,
   ModuleCode           nvarchar(150)        collate Chinese_PRC_CI_AS null,
   ModuleID             nvarchar(36)         collate Chinese_PRC_CI_AS null,
   TableName            nvarchar(100)        collate Chinese_PRC_CI_AS null,
   TableText            nvarchar(50)         collate Chinese_PRC_CI_AS null,
   Description          nvarchar(200)        collate Chinese_PRC_CI_AS null,
   ContentType          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ContentFieldType     nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ContentText          nvarchar(200)        collate Chinese_PRC_CI_AS null,
   Contents             nvarchar(200)        collate Chinese_PRC_CI_AS null,
   ForeColor            int                  null,
   BgColor              int                  null,
   DisplayStyle         nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ColumnWidth          real                 null,
   IsVisible            bit                  null,
   IsEdit               bit                  null,
   IsNull               bit                  null,
   OrderIndex           smallint             null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_modul__Scdel__2159A39A default (0),
   constraint PK_sys_moduleview primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_operate_log                                       */
/*==============================================================*/
create table dbo.sys_operate_log (
   ID                   bigint               identity(1, 1),
   dataID               uniqueidentifier     not null,
   moduleID             uniqueidentifier     not null,
   testRoomCode         nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   BGBH                 nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   DataName             nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   requestID            uniqueidentifier     not null,
   modifiedby           nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   modifiedDate         datetime             not null,
   optType              nvarchar(10)         collate Chinese_PRC_CI_AS not null,
   modifyItem           nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   constraint PK_sys_operate_log primary key nonclustered (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_dataID                                            */
/*==============================================================*/
create index idx_dataID on dbo.sys_operate_log (
dataID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_modifiedDate                                      */
/*==============================================================*/
create clustered index idx_modifiedDate on dbo.sys_operate_log (
modifiedDate ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_moduleID                                          */
/*==============================================================*/
create index idx_moduleID on dbo.sys_operate_log (
moduleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_requestID                                         */
/*==============================================================*/
create index idx_requestID on dbo.sys_operate_log (
requestID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_testRoomCode                                      */
/*==============================================================*/
create index idx_testRoomCode on dbo.sys_operate_log (
testRoomCode ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_px_relation                                       */
/*==============================================================*/
create table dbo.sys_px_relation (
   ID                   bigint               identity(1, 1),
   SGDataID             uniqueidentifier     not null,
   PXDataID             uniqueidentifier     not null,
   PXTime               datetime             not null,
   constraint PK_sys_px_relation primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_pxjz_frequency                                    */
/*==============================================================*/
create table dbo.sys_pxjz_frequency (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   Frequency            float                not null,
   FrequencyType        smallint             not null,
   IsActive             smallint             not null,
   constraint PK_sys_pxjz_frequency primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_qualificationauth                                 */
/*==============================================================*/
create table dbo.sys_qualificationauth (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   Scts                 datetime             null constraint DF_sys_QualificationAuth_Scts default getdate(),
   CODE                 nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   FOLDERCODE           nvarchar(100)        collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_quali__Scdel__252A347E default (0),
   constraint PK_sys_QualificationAuth primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_readfunction                                      */
/*==============================================================*/
create table dbo.sys_readfunction (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   FunctionName         nvarchar(200)        collate Chinese_PRC_CI_AS null,
   ModelIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SheetIndex           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   SheetName            nvarchar(200)        collate Chinese_PRC_CI_AS null,
   ConditionList        nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   ModifyList           nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_readfunction primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_readfunctioncondition                             */
/*==============================================================*/
create table dbo.sys_readfunctioncondition (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ColName              nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ColDescription       nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ConditionType        nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ConditionColName     nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ConditionColDescription nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ConditionTblName     nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ConditionTblDescription nvarchar(100)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_readfunctioncondition primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_readfunctionmodify                                */
/*==============================================================*/
create table dbo.sys_readfunctionmodify (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   ColName              nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ColDescription       nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ModifyType           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ModifyColName        nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ModifyColDescription nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ModifyTblName        nvarchar(100)        collate Chinese_PRC_CI_AS null,
   ModifyTblDescription nvarchar(100)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_readfunctionmodify primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_report                                            */
/*==============================================================*/
create table dbo.sys_report (
   ID                   uniqueidentifier     not null,
   CatlogCode           nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   Name                 sysname              collate Chinese_PRC_CI_AS not null,
   Description          nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   SheetStyle           nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   Config               nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   IsActive             smallint             not null constraint DF_sys_report_IsActive default (1),
   constraint PK_sys_report primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_report_config                                     */
/*==============================================================*/
create table dbo.sys_report_config (
   ID                   int                  not null,
   TestName             nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   UnitName             nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   Frequency            nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   IsActive             smallint             not null constraint DF_sys_report_module_config_IsActive default (1),
   constraint PK_sys_report_module_config primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_report_config_module                              */
/*==============================================================*/
create table dbo.sys_report_config_module (
   ReportConfigID       int                  not null,
   ModuleID             uniqueidentifier     not null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_report_parameters                                 */
/*==============================================================*/
create table dbo.sys_report_parameters (
   ID                   uniqueidentifier     not null,
   ReportID             uniqueidentifier     not null,
   Name                 nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   Description          nvarchar(1024)       collate Chinese_PRC_CI_AS not null,
   constraint PK_sys_report_parameters primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_request_change                                    */
/*==============================================================*/
create table dbo.sys_request_change (
   ID                   uniqueidentifier     not null,
   DataID               uniqueidentifier     not null,
   TestRoomCode         nvarchar(32)         collate Chinese_PRC_CI_AS null,
   ModuleID             uniqueidentifier     null,
   WTBH                 nvarchar(256)        collate Chinese_PRC_CI_AS null,
   BGBH                 nvarchar(256)        collate Chinese_PRC_CI_AS null,
   RequestBy            nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   RequestTime          datetime             not null,
   Caption              nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   Reason               nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   ApprovePerson        nvarchar(50)         collate Chinese_PRC_CI_AS null,
   ApproveTime          datetime             null,
   ProcessReason        nvarchar(1024)       collate Chinese_PRC_CI_AS null,
   State                nvarchar(16)         collate Chinese_PRC_CI_AS not null constraint DF_sys_request_change_State default '已提交',
   IsDelete             tinyint              not null constraint DF__sys_reque__IsDel__50FB042B default (0),
   constraint PK_sys_request_change primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_dataID                                            */
/*==============================================================*/
create index idx_dataID on dbo.sys_request_change (
DataID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_rt_hntyljDataTable                                */
/*==============================================================*/
create table dbo.sys_rt_hntyljDataTable (
   ID                   nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   RT_WTBH              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RT_FSSJ              datetime             null,
   RT_SYMC              nvarchar(100)        collate Chinese_PRC_CI_AS null,
   RT_SBBH              nvarchar(100)        collate Chinese_PRC_CI_AS null,
   RT_ZDLZ              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RT_SYSCODE           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RT_KYLZ              nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RT_ItemIndex         nvarchar(50)         collate Chinese_PRC_CI_AS null,
   RT_CREATTIME         datetime             null,
   RT_TESTTIME          datetime             null,
   SCTS                 datetime             null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_sheet                                             */
/*==============================================================*/
create table dbo.sys_sheet (
   ID                   uniqueidentifier     not null,
   Name                 nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   CatlogCode           nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   SheetXML             nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   SheetData            nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LasteditedTime       datetime             not null,
   IsActive             smallint             not null constraint DF_sys_sheet_IsActive default (1),
   CellLogic            nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   Formulas             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_sheet primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_sheet_update                                      */
/*==============================================================*/
create table dbo.sys_sheet_update (
   ID                   uniqueidentifier     not null,
   Name                 nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   CatlogCode           nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   SheetXML             nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   SheetData            nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   CreatedUser          nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LasteditedTime       datetime             not null,
   IsActive             smallint             not null,
   CellLogic            nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   Formulas             nvarchar(Max)        collate Chinese_PRC_CI_AS null,
   constraint PK_sys_sheet_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_sms_log                                           */
/*==============================================================*/
create table dbo.sys_sms_log (
   ID                   bigint               identity(1, 1),
   SMSPhone             sysname              collate Chinese_PRC_CI_AS not null,
   SMSContent           nvarchar(512)        collate Chinese_PRC_CI_AS not null,
   SentTime             datetime             not null constraint DF_sys_sms_log_SentTime default getdate(),
   SentError            nvarchar(2048)       collate Chinese_PRC_CI_AS not null,
   RelationID           nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   constraint PK_sys_sms_log primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_sms_receiver                                      */
/*==============================================================*/
create table dbo.sys_sms_receiver (
   ID                   int                  identity(1, 1),
   TestRoomCode         nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   PersonName           nvarchar(50)         collate Chinese_PRC_CI_AS null,
   TestRoom             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CompanyName          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CellPhone            nvarchar(20)         collate Chinese_PRC_CI_AS null,
   IsActive             smallint             not null constraint DF_sys_sms_receiver_IsActive default (1),
   constraint PK_sys_sms_receiver primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_sms_stadium_log                                   */
/*==============================================================*/
create table dbo.sys_sms_stadium_log (
   ID                   bigint               identity(1, 1),
   SMSPhone             nvarchar(200)        collate Chinese_PRC_CI_AS not null,
   SMSContent           nvarchar(2000)       collate Chinese_PRC_CI_AS not null,
   SentTime             datetime             not null constraint DF_sys_sms_log_stadium_SentTime default getdate(),
   SentError            nvarchar(100)        collate Chinese_PRC_CI_AS not null,
   RelationID           nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   constraint PK_sys_sms_stadium_log primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_solcontent                                        */
/*==============================================================*/
create table dbo.sys_solcontent (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   NAME                 nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLCONTENT_NAME default (0),
   FCONTENT             varbinary(Max)       null constraint DFT_SYS_SOLCONTENT_FCONTENT default (0),
   OFFOLDER             nvarchar(Max)        collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLCONTENT_OFFOLDER default (0),
   ORDERINDEX           int                  null constraint DFT_SYS_SOLCONTENT_ORDERINDEX default (0),
   TYPEFALG             nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLCONTENT_TYPEFALG default (0),
   OFSOLUTION           nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLCONTENT_OFSOLUTION default (0),
   DESCRIPTION          nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLCONTENT_DESCRIPTION default (0),
   SCTS                 datetime             null constraint DFT_SYS_SOLCONTENT_SCTS default getdate(),
   SCUS                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCDF                 smallint             null constraint DFT_SYS_SOLCONTENT_SCDF default (0),
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null,
   SCAP                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCST                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCWT                 smallint             null constraint DFT_SYS_SOLCONTENT_SCWT default (0),
   SCER                 smallint             null constraint DFT_SYS_SOLCONTENT_SCER default (0),
   SCDL                 smallint             null constraint DFT_SYS_SOLCONTENT_SCDL default (0),
   SCCT                 smallint             null constraint DFT_SYS_SOLCONTENT_SCCT default (0),
   SCSV                 smallint             null constraint DFT_SYS_SOLCONTENT_SCSV default (0),
   constraint PMK_SYS_SOLCONTENT_ID primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_solfordertype                                     */
/*==============================================================*/
create table dbo.sys_solfordertype (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   NAME                 nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORDERTYPE_NAME default (0),
   CODE                 varchar(Max)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORDERTYPE_CODE default (0),
   TYPEFALG             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CONTENTIMAGEINDEX    int                  null constraint DFT_SYS_SOLFORDERTYPE_CONTENTIMAGEINDEX default (0),
   FOLDERIMAGEINDEX     int                  null constraint DFT_SYS_SOLFORDERTYPE_FOLDERIMAGEINDEX default (0),
   IDEDLL               nvarchar(200)        collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORDERTYPE_IDEDLL default (0),
   IDECLASS             nvarchar(200)        collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORDERTYPE_IDECLASS default (0),
   RUNDLL               nvarchar(200)        collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORDERTYPE_RUNDLL default (0),
   RUNCLASS             nvarchar(200)        collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORDERTYPE_RUNCLASS default (0),
   CANNEWFOLDER         smallint             null constraint DFT_SYS_SOLFORDERTYPE_CANNEWFOLDER default (0),
   CANNEWCONTENT        smallint             null constraint DFT_SYS_SOLFORDERTYPE_CANNEWCONTENT default (0),
   OFTYPE               nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORDERTYPE_OFTYPE default (0),
   SCUS                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCDF                 smallint             null constraint DFT_SYS_SOLFORDERTYPE_SCDF default (0),
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null,
   SCAP                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCST                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCWT                 smallint             null constraint DFT_SYS_SOLFORDERTYPE_SCWT default (0),
   SCER                 smallint             null constraint DFT_SYS_SOLFORDERTYPE_SCER default (0),
   SCDL                 smallint             null constraint DFT_SYS_SOLFORDERTYPE_SCDL default (0),
   SCCT                 smallint             null constraint DFT_SYS_SOLFORDERTYPE_SCCT default (0),
   SCSV                 smallint             null constraint DFT_SYS_SOLFORDERTYPE_SCSV default (0),
   constraint PMK_SYS_SOLFORDERTYPE_ID primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: IDX_SYS_SOLFORDERTYPE_TYPEFLAG                        */
/*==============================================================*/
create unique index IDX_SYS_SOLFORDERTYPE_TYPEFLAG on dbo.sys_solfordertype (
TYPEFALG ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_solforldr                                         */
/*==============================================================*/
create table dbo.sys_solforldr (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   CODE                 varchar(Max)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORLDR_CODE default (0),
   NAME                 nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORLDR_NAME default (0),
   OFSOLUTION           nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORLDR_OFSOLUTION default (0),
   ISSYSFOLDER          smallint             null constraint DFT_SYS_SOLFORLDR_ISSYSFOLDER default (0),
   ORDERINDEX           int                  null constraint DFT_SYS_SOLFORLDR_ORDERINDEX default (0),
   TYPEFALG             nvarchar(50)         collate Chinese_PRC_CI_AS null constraint DFT_SYS_SOLFORLDR_TYPEFALG default (0),
   SCTS                 datetime             null constraint DFT_SYS_SOLFORLDR_SCTS default getdate(),
   SCUS                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCDF                 smallint             null constraint DFT_SYS_SOLFORLDR_SCDF default (0),
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null,
   SCAP                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCST                 nvarchar(100)        collate Chinese_PRC_CI_AS null,
   SCWT                 smallint             null constraint DFT_SYS_SOLFORLDR_SCWT default (0),
   SCER                 smallint             null constraint DFT_SYS_SOLFORLDR_SCER default (0),
   SCDL                 smallint             null constraint DFT_SYS_SOLFORLDR_SCDL default (0),
   SCCT                 smallint             null constraint DFT_SYS_SOLFORLDR_SCCT default (0),
   SCSV                 smallint             null constraint DFT_SYS_SOLFORLDR_SCSV default (0),
   constraint PMK_SYS_SOLFORLDR_ID primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_stadium                                           */
/*==============================================================*/
create table dbo.sys_stadium (
   ID                   uniqueidentifier     not null constraint DF__sys_stadium__ID__3561679E default newid(),
   DataID               uniqueidentifier     not null,
   LastUpdatedTime      datetime             not null constraint DF__sys_stadi__LastU__36558BD7 default getdate(),
   DateSpan             int                  not null,
   Temperatures         int                  not null constraint DF__sys_stadi__Tempe__3749B010 default (0),
   StadiumRange         int                  not null constraint DF__sys_stadi__Stadi__383DD449 default (24),
   TemperatureSum       float                not null constraint DF__sys_stadi__Tempe__3931F882 default (0),
   StartTime            datetime             not null constraint DF__sys_stadi__Start__3A261CBB default getdate(),
   EndTime              datetime             not null constraint DF__sys_stadi__EndTi__3B1A40F4 default getdate(),
   ModuleID             uniqueidentifier     not null constraint DF__sys_stadi__Modul__3C0E652D default newid(),
   TestRoomCode         nvarchar(48)         collate Chinese_PRC_CI_AS not null constraint DF__sys_stadi__TestR__3D028966 default N'',
   DataName             nvarchar(256)        collate Chinese_PRC_CI_AS not null constraint DF__sys_stadi__DataN__3DF6AD9F default N'',
   F_ItemId             nvarchar(16)         collate Chinese_PRC_CI_AS null,
   F_PH                 sysname              collate Chinese_PRC_CI_AS null,
   F_ZJRQ               datetime             null,
   F_SJBH               sysname              collate Chinese_PRC_CI_AS null,
   F_SJSize             sysname              collate Chinese_PRC_CI_AS null,
   F_SYXM               sysname              collate Chinese_PRC_CI_AS null,
   F_BGBH               sysname              collate Chinese_PRC_CI_AS null,
   F_WTBH               sysname              collate Chinese_PRC_CI_AS null,
   F_Added              sysname              collate Chinese_PRC_CI_AS null,
   F_IsDone             bit                  not null constraint DF__sys_stadi__F_IsD__3EEAD1D8 default (0),
   F_ItemIndex          int                  not null constraint DF__sys_stadi__F_Ite__3FDEF611 default (1),
   SGComment            nvarchar(1024)       collate Chinese_PRC_CI_AS null,
   LastSGUser           nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastSGTime           datetime             null,
   JLComment            nvarchar(1024)       collate Chinese_PRC_CI_AS null,
   LastJLUser           nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastJLTime           datetime             null,
   constraint PK_sys_stadium primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_dataID                                            */
/*==============================================================*/
create index idx_dataID on dbo.sys_stadium (
DataID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_testroomcode_moduleID_fisdone_fwtbh               */
/*==============================================================*/
create index idx_testroomcode_moduleID_fisdone_fwtbh on dbo.sys_stadium (
TestRoomCode ASC,
ModuleID ASC,
F_WTBH ASC,
F_IsDone ASC,
StartTime ASC,
EndTime ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_stadium_config                                    */
/*==============================================================*/
create table dbo.sys_stadium_config (
   ID                   uniqueidentifier     not null,
   StadiumConfig        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LastEditedTime       datetime             not null,
   IsActive             smallint             not null constraint DF_sys_stadium_config_IsActive default (1),
   constraint PK_sys_stadium_config primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_stadium_config_days                               */
/*==============================================================*/
create table dbo.sys_stadium_config_days (
   ID                   uniqueidentifier     not null constraint DF_sys_stadium_config_days_ID default newid(),
   ModuleID             uniqueidentifier     not null,
   Days                 int                  not null,
   constraint PK_sys_stadium_config_days_1 primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_stadium_config_days_update                        */
/*==============================================================*/
create table dbo.sys_stadium_config_days_update (
   ID                   uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   Days                 int                  not null,
   constraint PK_sys_stadium_config_days_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_stadium_config_update                             */
/*==============================================================*/
create table dbo.sys_stadium_config_update (
   ID                   uniqueidentifier     not null,
   StadiumConfig        nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   LastEditedUser       nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   LastEditedTime       datetime             not null,
   IsActive             smallint             not null,
   constraint PK_sys_stadium_config_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_stadium_temperature                               */
/*==============================================================*/
create table dbo.sys_stadium_temperature (
   ID                   uniqueidentifier     not null constraint DF__sys_stadium___ID__0B6B2DD2 default newid(),
   TestRoomCode         nvarchar(48)         collate Chinese_PRC_CI_AS not null constraint DF__sys_stadi__TestR__0C5F520B default N'',
   Temperature1         float                null,
   Temperature2         float                null,
   Temperature3         float                null,
   TemperatureAvg       float                not null constraint DF__sys_stadi__Tempe__0D537644 default (0),
   Comment              nvarchar(1024)       collate Chinese_PRC_CI_AS null,
   LastEditUser         nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastEditTime         datetime             not null constraint DF__sys_stadi__LastE__0E479A7D default getdate(),
   TestTime             datetime             not null constraint DF__sys_stadi__TestT__0F3BBEB6 default getdate(),
   TemperatureType      smallint             not null constraint DF__sys_stadi__Tempe__14BF8DE2 default (0),
   constraint PK_sys_stadium_temperature primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_tables                                            */
/*==============================================================*/
create table dbo.sys_tables (
   ID                   varchar(36)          collate Chinese_PRC_CI_AS not null,
   SCTS                 datetime             null constraint DFT_SYS_TABLES_SCTS default getdate(),
   SCPT                 varchar(36)          collate Chinese_PRC_CI_AS null constraint DFT_SYS_TABLES_SCPT default (0),
   DESCRIPTION          nvarchar(100)        collate Chinese_PRC_CI_AS null,
   TABLENAME            nvarchar(100)        collate Chinese_PRC_CI_AS null,
   TABLETYPE            nvarchar(3)          collate Chinese_PRC_CI_AS null,
   Scts_1               datetime             null,
   Scdel                smallint             not null constraint DF__sys_table__Scdel__1E7D36EF default (0),
   constraint PMK_SYS_000005_TABLES_ID primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_temperature_types                                 */
/*==============================================================*/
create table dbo.sys_temperature_types (
   ID                   int                  not null,
   TestRoomCode         nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   CreateBy             nvarchar(50)         collate Chinese_PRC_CI_AS null,
   CreateTime           datetime             not null constraint DF__sys_tempe__Creat__351DDF8C default getdate(),
   Name                 nvarchar(50)         collate Chinese_PRC_CI_AS null,
   IsSystem             smallint             not null constraint DF__sys_tempe__IsSys__361203C5 default (0),
   constraint PK_sys_temperature_types primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_test_data                                         */
/*==============================================================*/
create table dbo.sys_test_data (
   ID                   uniqueidentifier     not null,
   DataID               uniqueidentifier     not null,
   StadiumID            uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   WTBH                 nvarchar(256)        not null,
   TestRoomCode         nvarchar(32)         not null,
   SerialNumber         int                  not null,
   UserName             nvarchar(64)         not null,
   CreatedTime          datetime             not null default getdate(),
   TestData             nvarchar(Max)        not null,
   RealTimeData         nvarchar(Max)        not null,
   Status               smallint             not null constraint 0 default 0,
   MachineCode          sysname              not null default '',
   TotallNumber         int                  not null default 0,
   UploadInfo           nvarchar(Max)        null default '',
   UploadCode           nvarchar(50)         null default '',
   UploadTDB            smallint             null default 0,
   UploadEMC            smallint             null default 0,
   constraint PK_sys_test_data primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_dataID                                            */
/*==============================================================*/
create index idx_dataID on dbo.sys_test_data (
DataID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_moduleID                                          */
/*==============================================================*/
create index idx_moduleID on dbo.sys_test_data (
ModuleID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_stadiumID                                         */
/*==============================================================*/
create index idx_stadiumID on dbo.sys_test_data (
StadiumID ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_sync                                              */
/*==============================================================*/
create index idx_sync on dbo.sys_test_data (
TestRoomCode ASC,
Status ASC,
CreatedTime ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_wtbh                                              */
/*==============================================================*/
create index idx_wtbh on dbo.sys_test_data (
WTBH ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_test_overtime                                     */
/*==============================================================*/
create table dbo.sys_test_overtime (
   ID                   uniqueidentifier     not null,
   CaiJiID              uniqueidentifier     not null,
   DataID               uniqueidentifier     not null,
   StadiumID            uniqueidentifier     not null,
   ModuleID             uniqueidentifier     not null,
   WTBH                 nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   TestRoomCode         nvarchar(32)         collate Chinese_PRC_CI_AS not null,
   SerialNumber         int                  not null,
   UserName             nvarchar(64)         collate Chinese_PRC_CI_AS not null,
   CreatedTime          datetime             not null,
   TestData             nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   RealTimeData         nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   MachineCode          sysname              collate Chinese_PRC_CI_AS not null,
   UploadInfo           nvarchar(Max)        collate Chinese_PRC_CI_AS not null,
   UploadCode           nvarchar(50)         collate Chinese_PRC_CI_AS not null,
   UploadTDB            smallint             not null,
   UploadEMC            smallint             not null,
   TotallNumber         int                  not null,
   SGComment            nvarchar(300)        collate Chinese_PRC_CI_AS null,
   LastSGUser           nvarchar(64)         collate Chinese_PRC_CI_AS null,
   LastSGTime           datetime             null,
   JLComment            nvarchar(300)        collate Chinese_PRC_CI_AS null,
   ApprovedJLUser       nvarchar(64)         collate Chinese_PRC_CI_AS null,
   ApprovedTime         datetime             null,
   Status               smallint             not null constraint DF_sys_test_overtime_Status default (0),
   constraint PK_sys_test_overtime primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_update                                            */
/*==============================================================*/
create table dbo.sys_update (
   ID                   uniqueidentifier     not null,
   FileName             nvarchar(256)        collate Chinese_PRC_CI_AS not null,
   FileType             smallint             not null,
   CreatedServerTime    datetime             not null,
   FileState            smallint             not null,
   constraint PK_sys_update primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_update_catch                                      */
/*==============================================================*/
create table dbo.sys_update_catch (
   TableName            sysname              collate Chinese_PRC_CI_AS not null,
   LastTime             datetime             not null,
   constraint PK_sys_update_catch primary key (TableName)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_updaterfiletable                                  */
/*==============================================================*/
create table dbo.sys_updaterfiletable (
   ID                   nvarchar(36)         collate Chinese_PRC_CI_AS not null,
   FileName             nvarchar(200)        collate Chinese_PRC_CI_AS null,
   FileData             image                null,
   FileVersion          nvarchar(50)         collate Chinese_PRC_CI_AS null,
   constraint PK_sys_updaterfiletable primary key (ID)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sys_users_testroom                                    */
/*==============================================================*/
create table dbo.sys_users_testroom (
   id                   bigint               identity(1, 1),
   username             varchar(50)          collate Chinese_PRC_CI_AS null,
   testroomcode         varchar(50)          collate Chinese_PRC_CI_AS null,
   segment              varchar(50)          collate Chinese_PRC_CI_AS null,
   project              varchar(50)          collate Chinese_PRC_CI_AS null,
   testroom             varchar(50)          collate Chinese_PRC_CI_AS null,
   nodecode             varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK__sys_users_testro__62C77444 primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sysdiagrams                                           */
/*==============================================================*/
create table dbo.sysdiagrams (
   name                 sysname              collate Chinese_PRC_CI_AS not null,
   principal_id         int                  not null,
   diagram_id           int                  identity(1, 1),
   version              int                  null,
   definition           varbinary(Max)       null,
   constraint PK__sysdiagrams__7F60ED59 primary key (diagram_id)
         on "PRIMARY",
   constraint UK_principal_name unique (principal_id, name)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* View: v_bs_codeName                                          */
/*==============================================================*/
create view dbo.v_bs_codeName as
  select 
c.工程编码,
c.工程名称,
d.标段编码,
d.标段名称,
e.单位编码,
e.单位名称,
f.试验室编码,
f.试验室名称
from 
(select a.NodeCode as '工程编码',b.Description as '工程名称' from sys_engs_Tree as a,sys_engs_ProjectInfo as b where a.RalationID = b.ID  AND a.Scdel=0) as c,
(select a.NodeCode as '标段编码',b.Description as '标段名称' from sys_engs_Tree as a,sys_engs_SectionInfo as b where a.RalationID = b.ID AND a.Scdel=0) as d,
(select a.NodeCode as '单位编码',b.Description  as '单位名称' from sys_engs_Tree as a,sys_engs_CompanyInfo as b where a.RalationID = b.ID AND a.Scdel=0) as e,
(select a.NodeCode as '试验室编码',b.Description as '试验室名称' from sys_engs_Tree as a,sys_engs_ItemInfo as b where a.RalationID = b.ID AND a.Scdel=0) as f
where 
substring(f.试验室编码,1,4) = c.工程编码 and 
substring(f.试验室编码,1,8) = d.标段编码 and 
substring(f.试验室编码,1,12) = e.单位编码
go

/*==============================================================*/
/* View: v_bs_evaluateData                                      */
/*==============================================================*/
create view dbo.v_bs_evaluateData as
select 
        a.ID,
        a.ModelCode,
        a.ModelIndex,
        c.Description as SectionName,	
		c.NodeCode AS SectionCode,
        b.Description as CompanyName,	
		b.NodeCode AS CompanyCode,  
        d.Description as TestRoomName,	
		d.NodeCode AS TestRoomCode,
        a.ReportName,
        a.ReportNumber,
        a.SCTS AS ReportDate,
        a.F_InvalidItem, 
        a.SGComment, 
        a.JLComment 
        from 
        sys_biz_reminder_evaluateData as a,
		 ( select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_CompanyInfo as b where a.RalationID = b.ID) as b,
        (select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_SectionInfo as b where a.RalationID = b.ID) as c,
        (select a.NodeCode,b.Description from sys_engs_Tree as a,sys_engs_ItemInfo as b where a.RalationID = b.ID) as d  
        where b.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-7) and 
              c.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-11) and
              d.NodeCode = substring(a.ModelCode,0,Len(a.ModelCode)-3) AND a.AdditionalQualified=0
go

/*==============================================================*/
/* View: v_bs_machineSummary                                    */
/*==============================================================*/
create view dbo.v_bs_machineSummary as
SELECT   a.ID, b.试验室编码 AS 试验室编码, b.试验室名称, b.单位名称, b.单位编码, 
                a.col_norm_B6 AS 管理编号, b.标段名称, b.标段编码, a.col_norm_C6 AS 设备名称, a.col_norm_D6 AS 生产厂家, 
                a.col_norm_E6 AS 规格型号, a.col_norm_H6 AS 购置日期, a.col_norm_J6 AS 数量, a.col_norm_Q6 AS 备注, 
				a.col_norm_l6 as 检定情况,a.col_norm_m6 as 检定证书编号,
				a.col_norm_n6 as 上次校验日期,a.col_norm_o6 as 预计下次校验日期,
				a.col_norm_p6 as 检定周期,
                c.OrderID
FROM      dbo.biz_machinelist AS a INNER JOIN
                dbo.v_bs_codeName AS b ON LEFT(a.SCPT, 16) = b.试验室编码 INNER JOIN
                dbo.Sys_Tree AS c ON b.试验室编码 = c.NodeCode
go

/*==============================================================*/
/* View: v_bs_reminder_stadiumData                              */
/*==============================================================*/
create view dbo.v_bs_reminder_stadiumData as
SELECT f.id, m.标段名称 , m.单位名称 , m.试验室名称 ,m.试验室编码 AS testroomcode,f.F_Name as 名称,f.F_PH as 批号,f.F_ZJRQ as 制件日期,f.F_SJBH as 试件编号,f.F_SJSize as 试件尺寸,f.F_SYXM as 试验项目,f.F_BGBH as 报告编号,f.F_WTBH as 委托编号   FROM 
  dbo.sys_biz_reminder_stadiumInfo e JOIN   
  dbo.sys_biz_reminder_stadiumData f ON    f.ModelIndex=e.ID AND CAST( DATEDIFF(day, DATEADD(DAY, f.DateSpan, f.F_ZJRQ), GETDATE()) AS NVARCHAR(50)) IN (e.SearchRange) JOIN 
 dbo.v_bs_codeName m ON LEFT(f.ModelCode,16)=m.试验室编码
go

/*==============================================================*/
/* View: v_bs_sms                                               */
/*==============================================================*/
create view dbo.v_bs_sms as
SELECT  a.*,c.标段名称 AS segment,c.单位名称 AS company ,c.试验室名称 AS testroom,d.PersonName ,c.试验室编码 AS testroomcode FROM dbo.sys_sms_log a 
JOIN dbo.sys_document b ON a.RelationID=b.ID
JOIN dbo.v_bs_codeName c ON b.TestRoomCode=c.试验室编码   AND a.SentError=''
JOIN (SELECT CellPhone,MAX(PersonName) AS PersonName FROM dbo.sys_sms_receiver GROUP BY CellPhone) d ON a.SMSPhone=d.CellPhone
go

/*==============================================================*/
/* View: v_bs_userSummary                                       */
/*==============================================================*/
create view dbo.v_bs_userSummary as
SELECT ID,试验室编码,标段名称 ,
           单位名称 ,
           试验室名称 ,
           单位编码 ,Ext1 AS 姓名,Ext2 AS 性别,Ext3 AS 年龄,Ext4 AS 技术职称,Ext5 AS 职务,Ext6 AS 工作年限,Ext7 AS 联系电话,Ext8 AS 学历,Ext9 AS 毕业学校,Ext10 AS 专业  FROM  dbo.sys_document a JOIN dbo.v_bs_codeName b ON a.TestRoomCode=b.试验室编码 AND   ModuleID='08899BA2-CC88-403E-9182-3EF73F5FB0CE'   AND a.Status>0
go

/*==============================================================*/
/* View: v_codeName                                             */
/*==============================================================*/
create view dbo.v_codeName as
SELECT     a.RalationID, a.NodeCode, b.Description
FROM         dbo.sys_engs_Tree AS a INNER JOIN
                      dbo.sys_engs_ProjectInfo AS b ON a.RalationID = b.ID  AND a.Scdel=0
UNION
SELECT     a1.RalationID, a1.NodeCode, b.Description
FROM         dbo.sys_engs_Tree AS a1 INNER JOIN
                      dbo.sys_engs_SectionInfo AS b ON a1.RalationID = b.ID  AND a1.Scdel=0
UNION
SELECT     a2.RalationID, a2.NodeCode, b.Description
FROM         dbo.sys_engs_Tree AS a2 INNER JOIN
                      dbo.sys_engs_CompanyInfo AS b ON a2.RalationID = b.ID  AND a2.Scdel=0
UNION
SELECT     a3.RalationID, a3.NodeCode, b.Description
FROM         dbo.sys_engs_Tree AS a3 INNER JOIN
                      dbo.sys_engs_ItemInfo AS b ON a3.RalationID = b.ID AND a3.Scdel=0
go

/*==============================================================*/
/* View: v_invalid_document                                     */
/*==============================================================*/
create view dbo.v_invalid_document as
SELECT DISTINCT 
                b.ID AS IndexID, b.ModuleID AS ModelIndex, c.Name AS ReportName, b.BGBH AS ReportNumber, 
                b.BGRQ AS ReportDate, a.F_InvalidItem, a.SGComment, a.JLComment, sys_tree.OrderID AS OrderID, 
                d.标段名称 AS SectionName, d.单位名称 AS CompanyName, d.单位编码 AS CompanyCode, 
                d.试验室名称 AS TestRoomName, d.试验室编码 AS TestRoomCode, a.AdditionalQualified, a.DealResult, b.WTBH, 
                c.DeviceType
FROM      dbo.sys_invalid_document AS a INNER JOIN
                dbo.sys_document AS b ON a.ID = b.ID INNER JOIN
                dbo.sys_module AS c ON b.ModuleID = c.ID INNER JOIN
                dbo.v_bs_codeName AS d ON b.TestRoomCode = d.试验室编码 AND b.Status > 0
				left outer join sys_tree on d.标段编码 = sys_tree.nodecode
go

/*==============================================================*/
/* View: v_operate_log                                          */
/*==============================================================*/
create view dbo.v_operate_log as
SELECT modifiedby, 标段名称 AS segmentName , 单位名称 AS companyName, 试验室名称 AS testRoom ,modifiedDate,optType,Name AS modelName,testRoomCode AS testroomcode FROM dbo.sys_operate_log a JOIN dbo.v_bs_codeName b ON a.testRoomCode=b.试验室编码  JOIN dbo.sys_module c ON a.moduleID=c.ID
go

alter table dbo.sys_formulas
   add constraint FK_sys_formulas_sys_module foreign key (ModuleID)
      references dbo.sys_module (ID)
go

alter table dbo.sys_module_sheet
   add constraint FK_sys_module_sheet_sys_module foreign key (ModuleID)
      references dbo.sys_module (ID)
go

alter table dbo.sys_stadium
   add constraint FK_sys_stadium_sys_document foreign key (DataID)
      references dbo.sys_document (ID)
go


create procedure dbo.ExecSql (
 	@sql varChar(Max)
 ) as
BEGIN
	return
--	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hyt_Temp1]') AND type in (N'U'))
--				DROP TABLE [dbo].[hyt_Temp1]
--	Exec(@sql)
END
go


create function dbo.Fsys_DayTemperatureSum (@TestRoomCode VARCHAR(50), 
@ZJRQ DATETIME, 
@Temperatures INT)
RETURNS DATETIME 
BEGIN 

DECLARE @EarlistDate DATETIME 
IF @Temperatures>0
BEGIN
SELECT @EarlistDate=MIN( TestTime) FROM ( 
SELECT a.TestTime,a.TestRoomCode, 
( 
SELECT SUM(TemperatureAvg) FROM dbo.sys_stadium_temperature b WHERE 
a.TestTime>=b.TestTime AND a.TestRoomCode=b.TestRoomCode 
AND b.TestTime>=@ZJRQ AND b.TestRoomCode=@TestRoomCode 
)AS TRTSum 
FROM dbo.sys_stadium_temperature a 
where TestTime>=@ZJRQ AND TestRoomCode=@TestRoomCode 
)T WHERE TRTSum>=@Temperatures 
END

IF	@EarlistDate IS NULL
BEGIN
SET @EarlistDate=GETDATE()	
END

RETURN @EarlistDate 
END
go


create function dbo.Fsys_EarlyDate (@Date1 DATETIME, 
@Date2 DATETIME)
RETURNS DATETIME 
BEGIN 

DECLARE @EarlyDate DATETIME 

IF DATEDIFF(ss,@Date2,@Date1)>0
BEGIN
SET @EarlyDate=@Date2
END
ELSE
begin
SET @EarlyDate=@Date1
end
RETURN @EarlyDate 
END
go


create function dbo.Fsys_StadiumStartDate (@TestRoomCode VARCHAR(50), 
@ZJRQ DATETIME, 
@Temperatures INT , 
@DateSpan INT)
RETURNS DATETIME 
BEGIN 

DECLARE @EarlistDate DATETIME --记录温度提醒开始时间
DECLARE @LastDate DATETIME --记录龄期提醒开始时间
SET @LastDate=DATEADD(mi, @DateSpan*60, @ZJRQ) 
IF @Temperatures>0 
--计算温度开始时间
BEGIN 
SELECT @EarlistDate=MIN( TestTime) FROM ( 
SELECT a.TestTime,a.TestRoomCode, 
( 
SELECT SUM(TemperatureAvg) FROM dbo.sys_stadium_temperature b WHERE 
a.TestTime>=b.TestTime AND a.TestRoomCode=b.TestRoomCode 
AND b.TestTime>=@ZJRQ AND b.TestRoomCode=@TestRoomCode 
)AS TRTSum 
FROM dbo.sys_stadium_temperature a 
where TestTime>=@ZJRQ AND TestRoomCode=@TestRoomCode 
)T WHERE TRTSum>=@Temperatures 
END 

--温度为空
IF	@EarlistDate IS NULL 
BEGIN 
SET @EarlistDate=@LastDate 
END 
ELSE	
BEGIN 

--比较温度开始提醒时间和龄期提醒时间
IF DATEDIFF(ss,@LastDate,@EarlistDate)>0 
BEGIN 
SET @EarlistDate=@LastDate 
END 
END 

RETURN @EarlistDate 
END
go


create function dbo.Fsys_SumTemperature (@TestRoomCode VARCHAR(50), 
@ZJRQ DATETIME)
RETURNS FLOAT 
BEGIN 

DECLARE @count FLOAT 
SELECT @count= SUM(TemperatureAvg) 
FROM dbo.sys_stadium_temperature WHERE TestRoomCode=@TestRoomCode AND TestTime>=@ZJRQ AND TestTime<=GETDATE() 
RETURN @count 
END
go


create function dbo.Fweb_FailReport (@testcode VARCHAR(50) ,
      @modelindex VARCHAR(50),
	  @startdate VARCHAR(50),
	@enddate VARCHAR(50))
returns @TempTable table (testcode VARCHAR(50),modelindex VARCHAR(50),TotalCounts BIGINT,FailCounts BIGINT)    
AS 
    BEGIN
       
   
 --            insert into @TempTable
 --select ZjCount,JzCount from dbo.biz_ZJ_JZ_Summary

 DECLARE @n INT,
 @m INT,
 @sqls NVARCHAR(4000)

   SET @sqls='select @n=count(1) from [biz_norm_extent_'+@modelindex+'] a   where  LEFT(SCPT,16)='''+@testcode+''' and    a.SCTS>='''+@startdate+''' AND a.SCTS<'''+@enddate+''''  

		EXEC sp_executesql @sqls,N'@n int output',@n OUTPUT
		
		IF	@n>0
			BEGIN


			   SET @sqls='  SELECT @m=COUNT(1) FROM  dbo.sys_biz_reminder_evaluateData a JOIN dbo.[biz_norm_extent_'+@modelindex+'] b ON a.ID = b.ID'  

		EXEC sp_executesql @sqls,N'@m int output',@m OUTPUT
				          
		      INSERT @TempTable
		              ( testcode ,
					   modelindex ,		               
		                FailCounts ,
		                TotalCounts
		              )
		      VALUES  ( @testcode , -- modelindex - varchar(50)
		                @modelindex , -- testcode - varchar(50)
		                @n , -- FailCounts - bigint
		                @m  -- TotalCounts - bigint
		              )


			END     
        
        RETURN
    END
go


create function dbo.Fweb_ReturnCount (@modelID VARCHAR(50),
	 @testroomid VARCHAR(50),
	 @startdate VARCHAR(50),
	 @enddate VARCHAR(50),
	 @type INT)
RETURNS INT
	BEGIN
  
		DECLARE @count INT  ,
		 @sqls nvarchar(4000)	,
		 @n INT,
 @m INT

		IF @type=1
			BEGIN
					select @n=count(1) from [biz_norm_extent_@modelID] a   where  LEFT(SCPT,16)=@testroomid and    a.SCTS>=@startdate AND a.SCTS<@enddate 
								
		           
			 END
		IF @type=2
			BEGIN
						 SELECT @m=COUNT(a.ID) FROM  dbo.sys_biz_reminder_evaluateData a JOIN dbo.[biz_norm_extent_@modelID] b ON a.ID = b.ID and LEFT(b.SCPT,16)=@testroomid AND a.AdditionalQualified=0 and   a.F_InvalidItem NOT LIKE '%#%' and    a.SCTS>=@startdate AND a.SCTS<@enddate 
					
			END           

		RETURN @count
	END
go


create function dbo.Fweb_ReturnPXCount (@ModelIndex VARCHAR(50),
	 @SGRoomCode VARCHAR(50),
	 @StartDate DATETIME,
	 @EndDate DATETIME)
RETURNS INT
	BEGIN
  
		DECLARE @count INT  


 SELECT @count=COUNT(1) FROM dbo.biz_px_relation_web WHERE
  ModelIndex=@ModelIndex
   AND  LEFT(SGTestRoomCode,16)=@SGRoomCode
   AND   SGBGRQ>@StartDate 
   AND   SGBGRQ<@EndDate 
   AND  PXBGRQ>@StartDate 
   AND  PXBGRQ<@EndDate 
  

		RETURN @count
	END
go


create function dbo.Fweb_ReturnPXQualityCount (@ModelIndex VARCHAR(50),
	 @SGRoomCode VARCHAR(50),
	 @StartDate DATETIME,
	 @EndDate DATETIME)
RETURNS INT
	BEGIN
  
		DECLARE @count INT  

	DECLARE @tmp_px_0 TABLE
	(
	chartDate VARCHAR(20),
	zjCount INT,
	pxjzCount INT	
	)

	DECLARE @tmp_px_1 TABLE
	(
	chartDate VARCHAR(20),
	countnum INT	
	)

	DECLARE @tmp_px_2 TABLE
	(
	chartDate VARCHAR(20),
	countnum INT	
	)
	
	DECLARE @tmp_px_3 TABLE
	(
	chartDate1 VARCHAR(20),
	chartDate2 VARCHAR(20),
	zjCount INT,
	pxjzCount INT
	)

INSERT @tmp_px_1
        ( chartDate, countnum )
SELECT BGRQ,ZjCount FROM dbo.biz_ZJ_JZ_Summary WHERE ModelIndex=@ModelIndex AND TestRoomCode=@SGRoomCode AND BGRQ>@StartDate AND BGRQ<@EndDate

INSERT @tmp_px_2
        ( chartDate, countnum )
SELECT PXBGRQ,COUNT(1) FROM  dbo.biz_px_relation_web WHERE
  ModelIndex=@ModelIndex
   AND  LEFT(SGTestRoomCode,16)=@SGRoomCode
   AND   SGBGRQ>@StartDate 
   AND   SGBGRQ<@EndDate 
   AND  PXBGRQ>@StartDate 
   AND  PXBGRQ<@EndDate GROUP BY PXBGRQ



   INSERT @tmp_px_3
		        ( chartDate1 ,
		          chartDate2 ,
		          zjCount ,
		          pxjzCount
		        )
		SELECT a.chartDate,b.chartDate,a.countnum,b.countnum FROM @tmp_px_1 a FULL JOIN @tmp_px_2 b ON a.chartDate = b.chartDate
		
		UPDATE @tmp_px_3 SET chartDate1=chartDate2 WHERE chartDate1 IS NULL
		
		
		INSERT @tmp_px_0
		        ( chartDate, zjCount, pxjzCount )
		SELECT chartDate1,zjCount,pxjzCount FROM  @tmp_px_3 

		update @tmp_px_0 set zjCount=0 where zjCount is null
		update @tmp_px_0 set pxjzCount=0 where pxjzCount is null
  

  SELECT @count=COUNT(pxjzCount) FROM @tmp_px_0


	DECLARE @n INT--判断dt是否为空  
    SELECT @n=COUNT(1) FROM @tmp_px_0
	IF @n>0
		BEGIN
			  DECLARE @maxdate DATETIME--获取dt中最大的时间
			  DECLARE @mindate DATETIME--获取dt中最小的时间			  
			  SELECT TOP 1 @mindate=chartDate FROM @tmp_px_0  ORDER BY chartDate ASC    
			  SELECT TOP 1 @maxdate=chartDate FROM @tmp_px_0  ORDER BY chartDate DESC   
			  
			  DECLARE @CountList INT--计算间隔几天
			  SET @CountList=DATEDIFF(day,@mindate, @maxdate )

			  DECLARE @NAverage INT--最大时间减去最小时间除以3,得到等分
			  SET @NAverage=@CountList/3


			  DECLARE @px1 FLOAT
			  DECLARE @px2 FLOAT
			  DECLARE @px3 FLOAT 
			  
			  DECLARE @zj1 FLOAT
			  DECLARE @zj2 FLOAT
			  DECLARE @zj3 FLOAT            


			  SELECT @px1=SUM(pxjzCount),@zj1=SUM(zjCount) FROM @tmp_px_0 WHERE chartDate>=@mindate AND chartDate<DATEADD(dd,@NAverage,@mindate)

			   SELECT @px2=SUM(pxjzCount),@zj2=SUM(zjCount) FROM @tmp_px_0 WHERE chartDate>=DATEADD(dd,@NAverage,@mindate) AND chartDate<DATEADD(dd,@NAverage*2,@mindate)

			    SELECT @px3=SUM(pxjzCount),@zj3=SUM(zjCount) FROM @tmp_px_0 WHERE chartDate>=DATEADD(dd,@NAverage*2,@mindate) AND chartDate<@maxdate
			
			DECLARE @B1 FLOAT
			DECLARE @B2 FLOAT
			DECLARE @B3 FLOAT     
			
				set @B1=0.0
				set @B2=0.0
				set @B3=0.0

			IF @zj1!=0
				BEGIN
				SET	@B1=(@px1/@zj1)*10
				END
			IF @zj2!=0
				BEGIN              
				SET	@B2=(@px2/@zj2)*10
				END
			IF @zj3!=0
				BEGIN              
				SET	@B3=(@px3/@zj3)*10
				END

			DECLARE @C3 FLOAT
  
			SET @C3=(@B1+@B2+@B3)/3

			DECLARE @E1 FLOAT
			DECLARE @E2 FLOAT
			DECLARE @E3 FLOAT     

			SET	@E1=(@B1-@C3)*(@B1-@C3)
			SET	@E2=(@B2-@C3)*(@B2-@C3)
			SET	@E3=(@B3-@C3)*(@B3-@C3)
			  
			  DECLARE @G3 FLOAT
			  
			  SET @G3=(@E1+@E2+@E3)/3

				DECLARE @F4 FLOAT
				SET @F4=SQRT(@G3)              
			  
			  IF @C3=0  
				  BEGIN
					SET @count=0             
				 END   
			  ELSE
				  BEGIN
					  SET @count=100-10*@F4                
				  END         
		END 
	ELSE
		BEGIN
			
			SET @count=0
		END
	
		RETURN @count
	END
go


create function dbo.Fweb_ReturnPXQualityCount_New (@ModelIndex VARCHAR(50),
	 @SGRoomCode VARCHAR(50),
	 @StartDate DATETIME,
	 @EndDate DATETIME)
RETURNS INT
	BEGIN
  
		DECLARE @count INT  

	DECLARE @tmp_px_0 TABLE
	(
	chartDate VARCHAR(20),
	zjCount INT,
	pxjzCount INT	
	)

	DECLARE @tmp_px_1 TABLE
	(
	chartDate VARCHAR(20),
	countnum INT	
	)

	DECLARE @tmp_px_2 TABLE
	(
	chartDate VARCHAR(20),
	countnum INT	
	)
	
	DECLARE @tmp_px_3 TABLE
	(
	chartDate1 VARCHAR(20),
	chartDate2 VARCHAR(20),
	zjCount INT,
	pxjzCount INT
	)

INSERT @tmp_px_1
        ( chartDate, countnum )
SELECT BGRQ,COUNT(1) FROM dbo.sys_document WHERE ModuleID=@ModelIndex AND TestRoomCode=@SGRoomCode AND  BGRQ>=@StartDate AND BGRQ<@EndDate GROUP BY BGRQ

 


INSERT @tmp_px_2
        ( chartDate, countnum )
		   SELECT c.BGRQ,COUNT(1) FROM dbo.sys_document a JOIN dbo.sys_px_relation b ON a.ID = b.SGDataID JOIN dbo.sys_document c ON c.ID=b.PXDataID AND a.Status>0 AND c.Status>0 AND a.ModuleID=c.ModuleID AND a.ModuleID=@ModelIndex AND a.TestRoomCode=@SGRoomCode AND a.BGRQ>=@StartDate AND a.BGRQ<@EndDate AND c.BGRQ>=@StartDate AND c.BGRQ<@EndDate GROUP BY c.BGRQ


   INSERT @tmp_px_3
		        ( chartDate1 ,
		          chartDate2 ,
		          zjCount ,
		          pxjzCount
		        )
		SELECT a.chartDate,b.chartDate,a.countnum,b.countnum FROM @tmp_px_1 a FULL JOIN @tmp_px_2 b ON a.chartDate = b.chartDate
		
		UPDATE @tmp_px_3 SET chartDate1=chartDate2 WHERE chartDate1 IS NULL
		
		
		INSERT @tmp_px_0
		        ( chartDate, zjCount, pxjzCount )
		SELECT chartDate1,zjCount,pxjzCount FROM  @tmp_px_3 

		update @tmp_px_0 set zjCount=0 where zjCount is null
		update @tmp_px_0 set pxjzCount=0 where pxjzCount is null
  

  SELECT @count=COUNT(pxjzCount) FROM @tmp_px_0


	DECLARE @n INT--判断dt是否为空  
    SELECT @n=COUNT(1) FROM @tmp_px_0
	IF @n>0
		BEGIN
			  DECLARE @maxdate DATETIME--获取dt中最大的时间
			  DECLARE @mindate DATETIME--获取dt中最小的时间			  
			  SELECT TOP 1 @mindate=chartDate FROM @tmp_px_0  ORDER BY chartDate ASC    
			  SELECT TOP 1 @maxdate=chartDate FROM @tmp_px_0  ORDER BY chartDate DESC   
			  
			  DECLARE @CountList INT--计算间隔几天
			  SET @CountList=DATEDIFF(day,@mindate, @maxdate )

			  DECLARE @NAverage INT--最大时间减去最小时间除以3,得到等分
			  SET @NAverage=@CountList/3


			  DECLARE @px1 FLOAT
			  DECLARE @px2 FLOAT
			  DECLARE @px3 FLOAT 
			  
			  DECLARE @zj1 FLOAT
			  DECLARE @zj2 FLOAT
			  DECLARE @zj3 FLOAT            


			  SELECT @px1=SUM(pxjzCount),@zj1=SUM(zjCount) FROM @tmp_px_0 WHERE chartDate>=@mindate AND chartDate<DATEADD(dd,@NAverage,@mindate)

			   SELECT @px2=SUM(pxjzCount),@zj2=SUM(zjCount) FROM @tmp_px_0 WHERE chartDate>=DATEADD(dd,@NAverage,@mindate) AND chartDate<DATEADD(dd,@NAverage*2,@mindate)

			    SELECT @px3=SUM(pxjzCount),@zj3=SUM(zjCount) FROM @tmp_px_0 WHERE chartDate>=DATEADD(dd,@NAverage*2,@mindate) AND chartDate<@maxdate
			
			DECLARE @B1 FLOAT
			DECLARE @B2 FLOAT
			DECLARE @B3 FLOAT     
			
				set @B1=0.0
				set @B2=0.0
				set @B3=0.0

			IF @zj1!=0
				BEGIN
				SET	@B1=(@px1/@zj1)*10
				END
			IF @zj2!=0
				BEGIN              
				SET	@B2=(@px2/@zj2)*10
				END
			IF @zj3!=0
				BEGIN              
				SET	@B3=(@px3/@zj3)*10
				END

			DECLARE @C3 FLOAT
  
			SET @C3=(@B1+@B2+@B3)/3

			DECLARE @E1 FLOAT
			DECLARE @E2 FLOAT
			DECLARE @E3 FLOAT     

			SET	@E1=(@B1-@C3)*(@B1-@C3)
			SET	@E2=(@B2-@C3)*(@B2-@C3)
			SET	@E3=(@B3-@C3)*(@B3-@C3)
			  
			  DECLARE @G3 FLOAT
			  
			  SET @G3=(@E1+@E2+@E3)/3

				DECLARE @F4 FLOAT
				SET @F4=SQRT(@G3)              
			  
			  IF @C3=0  
				  BEGIN
					SET @count=0             
				 END   
			  ELSE
				  BEGIN
					  SET @count=100-10*@F4                
				  END         
		END 
	ELSE
		BEGIN
			
			SET @count=0
		END
	
		RETURN @count
	END
go


create function dbo.Fweb_SplitExpression (@ExpressionToBeSplited NVARCHAR(MAX) ,
      @SplitChar CHAR(1))
RETURNS @Result TABLE ( [Value] NVARCHAR(MAX) )
AS 
    BEGIN
        DECLARE @Temp NVARCHAR(MAX)
       
        DECLARE @Index INT
        SET @Index = CHARINDEX(@SplitChar, @ExpressionToBeSplited, 1)
        WHILE ( @Index > 0 ) 
            BEGIN
                SELECT  @Temp = SUBSTRING(@ExpressionToBeSplited, 1,
                                          @Index - 1) 
                INSERT  INTO @Result
                VALUES  ( @Temp )
                SET @ExpressionToBeSplited = SUBSTRING(@ExpressionToBeSplited,
                                                       @Index + 1,
                                                       LEN(@ExpressionToBeSplited)
                                                       - @Index)
                SET @Index = CHARINDEX(@SplitChar, @ExpressionToBeSplited, 1)
            END
        IF ( LEN(@ExpressionToBeSplited) > 0 ) 
            BEGIN
    
                INSERT  INTO @Result
                        ( VALUE )
                VALUES  ( @ExpressionToBeSplited )
            END
        
        RETURN
    END
go


create function dbo.HasRefer (@TableName varChar(50),@Field varChar(50))
RETURNS bit
AS
BEGIN
	Declare	@Expr varChar(Max),@result bit
	Select @Expr=''
	Select @Expr=Expression from SYS_COLREFS where RTrim(TableName)=RTrim(@TableName)
		and RTrim(ColName)=RTrim(@Field)
	if @Expr=null Set @Expr=''
	if @Expr<>'' set @result=1
	else Set @result=0
	return @result
END
go


create function dbo.ReferTo (@TableName varChar(50),@Field varChar(50),@Value varChar(500))
RETURNS varChar(500)
WITH EXECUTE AS CALLER
AS
BEGIN
	Declare @Return varChar(500),
			@Expr varChar(Max)
	Select @Return='',@Expr=''
	Select @Expr=Expression from SYS_COLREFS where RTrim(TableName)=RTrim(@TableName)
		and RTrim(ColName)=RTrim(@Field)
	Set @Return=Convert(varChar(500),@Value,21)  --没有参照
	if @Expr<>''
	begin
		Declare @DOCUMENTTYPE varChar(50),
				@Code varChar(50)
		select @DOCUMENTTYPE='',@Code=''
		Select @DOCUMENTTYPE=SubString(@Expr,CharIndex('<DOCUMENTTYPE>',@Expr)+14,
			CharIndex('</DOCUMENTTYPE>',@Expr)-CharIndex('<DOCUMENTTYPE>',@Expr)-14),
			@Code=SubString(@Expr,CharIndex('<CODE>',@Expr)+6,
			CharIndex('</CODE>',@Expr)-CharIndex('<CODE>',@Expr)-6) 
		if @DOCUMENTTYPE='REF_DICTIONARY' 
		begin
			Select @Return=Description from SYS_DICTIONARY
				where Code=@Return and CodeClass=@Code
		end 
--		else if @DOCUMENTTYPE='REF_COL'
--		begin
----			Declare @RefCol varChar(50),@RefDisp varChar(50),@sTableName varChar(50),@sql varChar(Max)
----			Select @RefCol=SubString(@Expr,CharIndex('<REFCOL>',@Expr)+8,CharIndex('</REFCOL>',@Expr)-CharIndex('<REFCOL>',@Expr)-8),
----					@RefDisp=SubString(@Expr,CharIndex('<REFDISP>',@Expr)+9,CharIndex('</REFDISP>',@Expr)-CharIndex('<REFDISP>',@Expr)-9)
----			Select @sTableName=SubString(@RefCol,0,CharIndex('.',@RefCol))
----			
----			Set @sql='Select '+@RefDisp+' as Field1 Into hyt_Temp1 from '+@sTableName+' where '+@RefCol+'='''+@Return+''''
----			Exec dbo.ExecSql @sql
----			select @Return='Field1 from hyt_Temp1'
--		end
	end
	Return(@Return)
END;
go


create procedure dbo.bhz_Line_Company_Station_Machine (
 	@PID varchar(100)
 ) as
begin
create table #temp
(
	TID int identity,
	id varchar(100),
	code varchar(100),
	elementName varchar(100),
	sort varchar(50),
	[type] varchar(50),
	[level] int, 
	parent varchar(50), 
	isLeaf bit,
	expanded bit,
	loaded bit	
)
declare @expanded bit,@loaded bit;
select @expanded='true',@loaded='false'
--线路
insert into #temp select ID,LineCode,LineName,'','线路',0,'','',@expanded,@loaded from bhz_Line;
--监理
insert into #temp select ID,CompanyCode,CompanyName,Sort,'监理',1,LineID,'',@expanded,@loaded from bhz_Company where IsJL='1';
--施工
insert into #temp select ID,CompanyCode,CompanyName,Sort,'施工',1,LineID,'',@expanded,@loaded from bhz_Company where IsJL='0';
--站点
insert into #temp select ID,StationCode,StationName,'','站点',2,CompanyID,'',@expanded,@loaded from bhz_Station;
--机器
insert into #temp select ID,MachineCode,MachineName,'','机器',3,StationID,'',@expanded,@loaded from bhz_Machine;
--处理isLeaf列
declare @rowCount int,@tempLeaf varchar(100),@LeafCount int;
select @rowCount=Count(1) from #temp;
while(@rowCount>0)
begin
	select @tempLeaf=ID from #temp where TID=@rowCount;
	select @LeafCount=COUNT(1) from #temp where parent=@tempLeaf;
	if(@LeafCount>0)
	begin
		update #temp set isLeaf='false' where TID=@rowCount;
	end
	else
	begin
		update #temp set isLeaf='true' where TID=@rowCount
	end
	set @rowCount=@rowCount-1;
	
end
select id,code,elementName,sort,[type],[level],parent,isLeaf,expanded,loaded from #temp where parent=@PID;
drop table #temp;
end
go


create procedure dbo.bhz_spweb_cbcx_chart @testcode  VARCHAR(5000), 
 	@startdate VARCHAR(50),
 	@ftype TINYINT,
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

CREATE TABLE #tmp2
(
id bigint identity(1,1)  primary key,
chanliang VARCHAR(50),
num VARCHAR(50),
nian VARCHAR(50),
counts VARCHAR(50),
djcounts VARCHAR(50),
zjcounts VARCHAR(50),
gjcounts VARCHAR(50),
dj VARCHAR(50),
zj VARCHAR(50),
gj VARCHAR(50)
)



 
	
 IF @ftype=1
	 BEGIN

			select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt1 from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate   group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian) ORDER BY DATENAME(week,ChuLiaoShiJian) 

 

 
 
 select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts  INTO #ttt1   from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate AND
ChaoBiaoDengJi=1
  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian) 


   select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts  INTO #tttt1   from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate AND
ChaoBiaoDengJi=1
  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian)  



     select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts  INTO #ttttt1    from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate AND
ChaoBiaoDengJi=1
  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian) 

 
SELECT a.chanliang,a.nian,a.num,a.counts,b.djcounts,c.zjcounts,d.gjcounts INTO #qq FROM #tt1 a LEFT JOIN #ttt1 b ON a.nian=b.nian AND a.num=b.num 
LEFT JOIN #tttt1 c ON a.nian=c.nian AND a.num=c.num
LEFT JOIN #ttttt1 d ON a.nian=d.nian AND a.num=d.num


UPDATE #qq SET djcounts=0 WHERE djcounts IS NULL
UPDATE #qq SET zjcounts=0 WHERE zjcounts IS NULL
UPDATE #qq SET gjcounts=0 WHERE gjcounts IS NULL
 
 

 INSERT #tmp2
        ( 
		 nian ,
		   num ,
          chanliang ,    
          counts ,
          djcounts ,
          zjcounts ,
          gjcounts,
		  dj,
zj,
gj
        )
 SELECT  nian,'第'+num+'周' AS num,CONVERT(DECIMAL(18,2),chanliang) AS chanliang,counts,djcounts,zjcounts,gjcounts,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,djcounts)/CONVERT(FLOAT,counts)*100)) as dj,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,zjcounts)/CONVERT(FLOAT,counts)*100))  AS zj,
CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,gjcounts)/CONVERT(FLOAT,counts)*100))   AS gj FROM #qq


	END
   IF @ftype=2
	 BEGIN


	 select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt3 from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate
			 group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian)


			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts INTO #ttt3 from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate
			
			 AND
 ChaoBiaoDengJi=2
			 group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by  month(ChuLiaoShiJian)

		

		select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts INTO #tttt3 from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate
 AND
 ChaoBiaoDengJi=2
			 group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian)


			 	select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts INTO #ttttt3 from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate
 AND
 ChaoBiaoDengJi=2
			 group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian)



			  
SELECT a.chanliang,a.nian,a.num,a.counts,b.djcounts,c.zjcounts,d.gjcounts INTO #qqq FROM #tt3 a LEFT JOIN #ttt3 b ON a.nian=b.nian AND a.num=b.num 
LEFT JOIN #tttt3 c ON a.nian=c.nian AND a.num=c.num
LEFT JOIN #ttttt3 d ON a.nian=d.nian AND a.num=d.num


UPDATE #qqq SET djcounts=0 WHERE djcounts IS NULL
UPDATE #qqq SET zjcounts=0 WHERE zjcounts IS NULL
UPDATE #qqq SET gjcounts=0 WHERE gjcounts IS NULL
 
 INSERT #tmp2
        ( 
          chanliang ,
          num ,
          nian ,
          counts ,
          djcounts ,
          zjcounts ,
          gjcounts,
		  dj,
zj,
gj
        )
 SELECT  nian,num+'月' AS num,CONVERT(DECIMAL(18,2),chanliang) AS chanliang,counts,djcounts,zjcounts,gjcounts,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,djcounts)/CONVERT(FLOAT,counts)*100)) as dj,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,zjcounts)/CONVERT(FLOAT,counts)*100))  AS zj,
CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,gjcounts)/CONVERT(FLOAT,counts)*100))   AS gj FROM #qqq


		 
	END
	
	 IF @ftype=3
	 BEGIN

			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt5 from bhz_PanDetail WHERE  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)


				select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts INTO #ttt5 from bhz_PanDetail WHERE  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate
				
					 AND
  ChaoBiaoDengJi=3
				 group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)


					select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts INTO #tttt5 from bhz_PanDetail WHERE  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate
					
					 AND
  ChaoBiaoDengJi=3
					 group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)


						select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts INTO #ttttt5 from bhz_PanDetail WHERE  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate
						
						 AND
  ChaoBiaoDengJi=3
						 group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)






			  
SELECT a.chanliang,a.nian,a.num,a.counts,b.djcounts,c.zjcounts,d.gjcounts INTO #qqqq FROM #tt5 a LEFT JOIN #ttt5 b ON a.nian=b.nian AND a.num=b.num 
LEFT JOIN #tttt5 c ON a.nian=c.nian AND a.num=c.num
LEFT JOIN #ttttt5 d ON a.nian=d.nian AND a.num=d.num


UPDATE #qqqq SET djcounts=0 WHERE djcounts IS NULL
UPDATE #qqqq SET zjcounts=0 WHERE zjcounts IS NULL
UPDATE #qqqq SET gjcounts=0 WHERE gjcounts IS NULL
 
 INSERT #tmp2
        (
          chanliang ,
          num ,
          nian ,
          counts ,
          djcounts ,
          zjcounts ,
          gjcounts,
		  dj,
zj,
gj
        )
 SELECT  nian,'第'+num+'季度' AS num,CONVERT(DECIMAL(18,2),chanliang) AS chanliang,counts,djcounts,zjcounts,gjcounts,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,djcounts)/CONVERT(FLOAT,counts)*100)) as dj,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,zjcounts)/CONVERT(FLOAT,counts)*100))  AS zj,
CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,gjcounts)/CONVERT(FLOAT,counts)*100))   AS gj FROM #qqqq





	END  
	

 

  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp2

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

	SET @Counts=@totalcounts

	select * from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  


END
go


create procedure dbo.bhz_spweb_cbhs @testcode  VARCHAR(5000), 
 	@startdate VARCHAR(50),
 	@ftype TINYINT,
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

CREATE TABLE #tmp2
(
id bigint identity(1,1)  primary key,
nian NVARCHAR(50),
num NVARCHAR(50),
ShuiNi DECIMAL(18,6),
ShuiNi_SG DECIMAL(18,6),
FenMeiHui DECIMAL(18,6),
FenMeiHui_SG DECIMAL(18,6),
KuangFen DECIMAL(18,6),
KuangFen_SG DECIMAL(18,6),
GuLiao DECIMAL(18,6),
GuLiao_SG DECIMAL(18,6),
WaiJiaJi DECIMAL(18,6),
WaiJiaJi_SG DECIMAL(18,6)
)



  DECLARE 

	@sqls nvarchar(4000)
 
	
 IF @ftype=1
	 BEGIN

	 IF @testcode!=''

	 BEGIN
							 SET @sqls=' 	select YEAR(ChuLiaoShiJian) AS nian,  ''第''+ DATENAME(week,ChuLiaoShiJian)+''周'',
					SUM(ShuiNi1)+SUM(ShuiNi2)+SUM(ShuiNi3)+SUM(ShuiNi4) AS ShuiNi,
					SUM(ShuiNi1_SG)+SUM(ShuiNi2_SG)+SUM(ShuiNi3_SG)+SUM(ShuiNi4_SG) AS ShuiNi_SG,

					SUM(FenMeiHui1)+SUM(FenMeiHui2)+SUM(FenMeiHui3)+SUM(FenMeiHui4) AS FenMeiHui,
					SUM(FenMeiHui1_SG)+SUM(FenMeiHui2_SG)+SUM(FenMeiHui3_SG)+SUM(FenMeiHui4_SG) AS FenMeiHui_SG,

					SUM(KuangFen1)+SUM(KuangFen2)+SUM(KuangFen3)+SUM(KuangFen4) AS KuangFen,
					SUM(KuangFen1_SG)+SUM(KuangFen2_SG)+SUM(KuangFen3_SG)+SUM(KuangFen4_SG) AS KuangFen_SG,

					SUM(GuLiao1)+SUM(GuLiao2)+SUM(GuLiao3)+SUM(GuLiao4)+SUM(XiGuLiao) AS GuLiao,
					SUM(GuLiao1_SG)+SUM(GuLiao2_SG)+SUM(GuLiao3_SG)+SUM(GuLiao4_SG)+SUM(XiGuLiao_SG) AS GuLiao_SG,

					SUM(WaiJiaJi1)+SUM(WaiJiaJi2)+SUM(WaiJiaJi3)+SUM(WaiJiaJi4)  AS WaiJiaJi,
					SUM(WaiJiaJi1_SG)+SUM(WaiJiaJi2_SG)+SUM(WaiJiaJi3_SG)+SUM(WaiJiaJi4_SG)  AS WaiJiaJi_SG
					  from bhz_PanDetail where   MachineCode IN ('+@testcode+') AND ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+'''   group BY YEAR(ChuLiaoShiJian), datename(week,ChuLiaoShiJian) ORDER BY  YEAR(ChuLiaoShiJian), DATENAME(week,ChuLiaoShiJian) '

 END
 ELSE
 
 BEGIN
		 SET @sqls=' select YEAR(ChuLiaoShiJian) AS nian,  ''第''+ DATENAME(week,ChuLiaoShiJian)+''周'' ,
					SUM(ShuiNi1)+SUM(ShuiNi2)+SUM(ShuiNi3)+SUM(ShuiNi4) AS ShuiNi,
					SUM(ShuiNi1_SG)+SUM(ShuiNi2_SG)+SUM(ShuiNi3_SG)+SUM(ShuiNi4_SG) AS ShuiNi_SG,

					SUM(FenMeiHui1)+SUM(FenMeiHui2)+SUM(FenMeiHui3)+SUM(FenMeiHui4) AS FenMeiHui,
					SUM(FenMeiHui1_SG)+SUM(FenMeiHui2_SG)+SUM(FenMeiHui3_SG)+SUM(FenMeiHui4_SG) AS FenMeiHui_SG,

					SUM(KuangFen1)+SUM(KuangFen2)+SUM(KuangFen3)+SUM(KuangFen4) AS KuangFen,
					SUM(KuangFen1_SG)+SUM(KuangFen2_SG)+SUM(KuangFen3_SG)+SUM(KuangFen4_SG) AS KuangFen_SG,

					SUM(GuLiao1)+SUM(GuLiao2)+SUM(GuLiao3)+SUM(GuLiao4)+SUM(XiGuLiao) AS GuLiao,
					SUM(GuLiao1_SG)+SUM(GuLiao2_SG)+SUM(GuLiao3_SG)+SUM(GuLiao4_SG)+SUM(XiGuLiao_SG) AS GuLiao_SG,

					SUM(WaiJiaJi1)+SUM(WaiJiaJi2)+SUM(WaiJiaJi3)+SUM(WaiJiaJi4)  AS WaiJiaJi,
					SUM(WaiJiaJi1_SG)+SUM(WaiJiaJi2_SG)+SUM(WaiJiaJi3_SG)+SUM(WaiJiaJi4_SG)  AS WaiJiaJi_SG
					  from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+'''  group BY YEAR(ChuLiaoShiJian), datename(week,ChuLiaoShiJian) ORDER BY  YEAR(ChuLiaoShiJian), DATENAME(week,ChuLiaoShiJian) '		
 END
	END
				   IF @ftype=2
					 BEGIN

				 

					 IF @testcode!=''
					 BEGIN 
							 SET @sqls='  select YEAR(ChuLiaoShiJian) AS nian,    ''第''+ CONVERT(NVARCHAR(10),month(ChuLiaoShiJian))+''月'',
				SUM(ShuiNi1)+SUM(ShuiNi2)+SUM(ShuiNi3)+SUM(ShuiNi4) AS ShuiNi,
				SUM(ShuiNi1_SG)+SUM(ShuiNi2_SG)+SUM(ShuiNi3_SG)+SUM(ShuiNi4_SG) AS ShuiNi_SG,

				SUM(FenMeiHui1)+SUM(FenMeiHui2)+SUM(FenMeiHui3)+SUM(FenMeiHui4) AS FenMeiHui,
				SUM(FenMeiHui1_SG)+SUM(FenMeiHui2_SG)+SUM(FenMeiHui3_SG)+SUM(FenMeiHui4_SG) AS FenMeiHui_SG,

				SUM(KuangFen1)+SUM(KuangFen2)+SUM(KuangFen3)+SUM(KuangFen4) AS KuangFen,
				SUM(KuangFen1_SG)+SUM(KuangFen2_SG)+SUM(KuangFen3_SG)+SUM(KuangFen4_SG) AS KuangFen_SG,

				SUM(GuLiao1)+SUM(GuLiao2)+SUM(GuLiao3)+SUM(GuLiao4)+SUM(XiGuLiao) AS GuLiao,
				SUM(GuLiao1_SG)+SUM(GuLiao2_SG)+SUM(GuLiao3_SG)+SUM(GuLiao4_SG)+SUM(XiGuLiao_SG) AS GuLiao_SG,

				SUM(WaiJiaJi1)+SUM(WaiJiaJi2)+SUM(WaiJiaJi3)+SUM(WaiJiaJi4)  AS WaiJiaJi,
				SUM(WaiJiaJi1_SG)+SUM(WaiJiaJi2_SG)+SUM(WaiJiaJi3_SG)+SUM(WaiJiaJi4_SG)  AS WaiJiaJi_SG
				  from bhz_PanDetail where   MachineCode IN ('+@testcode+') AND ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' 
							 group BY YEAR(ChuLiaoShiJian), month(ChuLiaoShiJian) order by  YEAR(ChuLiaoShiJian), month(ChuLiaoShiJian)'
   
				  END   
				  ELSE 
				  BEGIN
					 SET @sqls='  select YEAR(ChuLiaoShiJian) AS nian,    ''第''+ CONVERT(NVARCHAR(10),month(ChuLiaoShiJian))+''月'',
				SUM(ShuiNi1)+SUM(ShuiNi2)+SUM(ShuiNi3)+SUM(ShuiNi4) AS ShuiNi,
				SUM(ShuiNi1_SG)+SUM(ShuiNi2_SG)+SUM(ShuiNi3_SG)+SUM(ShuiNi4_SG) AS ShuiNi_SG,

				SUM(FenMeiHui1)+SUM(FenMeiHui2)+SUM(FenMeiHui3)+SUM(FenMeiHui4) AS FenMeiHui,
				SUM(FenMeiHui1_SG)+SUM(FenMeiHui2_SG)+SUM(FenMeiHui3_SG)+SUM(FenMeiHui4_SG) AS FenMeiHui_SG,

				SUM(KuangFen1)+SUM(KuangFen2)+SUM(KuangFen3)+SUM(KuangFen4) AS KuangFen,
				SUM(KuangFen1_SG)+SUM(KuangFen2_SG)+SUM(KuangFen3_SG)+SUM(KuangFen4_SG) AS KuangFen_SG,

				SUM(GuLiao1)+SUM(GuLiao2)+SUM(GuLiao3)+SUM(GuLiao4)+SUM(XiGuLiao) AS GuLiao,
				SUM(GuLiao1_SG)+SUM(GuLiao2_SG)+SUM(GuLiao3_SG)+SUM(GuLiao4_SG)+SUM(XiGuLiao_SG) AS GuLiao_SG,

				SUM(WaiJiaJi1)+SUM(WaiJiaJi2)+SUM(WaiJiaJi3)+SUM(WaiJiaJi4)  AS WaiJiaJi,
				SUM(WaiJiaJi1_SG)+SUM(WaiJiaJi2_SG)+SUM(WaiJiaJi3_SG)+SUM(WaiJiaJi4_SG)  AS WaiJiaJi_SG
				  from bhz_PanDetail where    ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' 
							 group BY YEAR(ChuLiaoShiJian), month(ChuLiaoShiJian) order by  YEAR(ChuLiaoShiJian), month(ChuLiaoShiJian)'


				  END	
		 
					END
	
	 IF @ftype=3
	 BEGIN
	  if @testcode!=''
	  begin 
			set @sqls='	 select YEAR(ChuLiaoShiJian) AS nian,  ''第''+ datename(quarter,ChuLiaoShiJian)+''季度'',
SUM(ShuiNi1)+SUM(ShuiNi2)+SUM(ShuiNi3)+SUM(ShuiNi4) AS ShuiNi,
SUM(ShuiNi1_SG)+SUM(ShuiNi2_SG)+SUM(ShuiNi3_SG)+SUM(ShuiNi4_SG) AS ShuiNi_SG,

SUM(FenMeiHui1)+SUM(FenMeiHui2)+SUM(FenMeiHui3)+SUM(FenMeiHui4) AS FenMeiHui,
SUM(FenMeiHui1_SG)+SUM(FenMeiHui2_SG)+SUM(FenMeiHui3_SG)+SUM(FenMeiHui4_SG) AS FenMeiHui_SG,

SUM(KuangFen1)+SUM(KuangFen2)+SUM(KuangFen3)+SUM(KuangFen4) AS KuangFen,
SUM(KuangFen1_SG)+SUM(KuangFen2_SG)+SUM(KuangFen3_SG)+SUM(KuangFen4_SG) AS KuangFen_SG,

SUM(GuLiao1)+SUM(GuLiao2)+SUM(GuLiao3)+SUM(GuLiao4)+SUM(XiGuLiao) AS GuLiao,
SUM(GuLiao1_SG)+SUM(GuLiao2_SG)+SUM(GuLiao3_SG)+SUM(GuLiao4_SG)+SUM(XiGuLiao_SG) AS GuLiao_SG,

SUM(WaiJiaJi1)+SUM(WaiJiaJi2)+SUM(WaiJiaJi3)+SUM(WaiJiaJi4)  AS WaiJiaJi,
SUM(WaiJiaJi1_SG)+SUM(WaiJiaJi2_SG)+SUM(WaiJiaJi3_SG)+SUM(WaiJiaJi4_SG)  AS WaiJiaJi_SG
  from bhz_PanDetail  WHERE   MachineCode IN ('+@testcode+') AND ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+'''  group BY YEAR(ChuLiaoShiJian), datename(quarter,ChuLiaoShiJian) ORDER BY YEAR(ChuLiaoShiJian), datename(quarter,ChuLiaoShiJian)'
	  END
  else
  begin
  
		set @sqls='	 select YEAR(ChuLiaoShiJian) AS nian,  ''第''+ datename(quarter,ChuLiaoShiJian)+''季度'',
SUM(ShuiNi1)+SUM(ShuiNi2)+SUM(ShuiNi3)+SUM(ShuiNi4) AS ShuiNi,
SUM(ShuiNi1_SG)+SUM(ShuiNi2_SG)+SUM(ShuiNi3_SG)+SUM(ShuiNi4_SG) AS ShuiNi_SG,

SUM(FenMeiHui1)+SUM(FenMeiHui2)+SUM(FenMeiHui3)+SUM(FenMeiHui4) AS FenMeiHui,
SUM(FenMeiHui1_SG)+SUM(FenMeiHui2_SG)+SUM(FenMeiHui3_SG)+SUM(FenMeiHui4_SG) AS FenMeiHui_SG,

SUM(KuangFen1)+SUM(KuangFen2)+SUM(KuangFen3)+SUM(KuangFen4) AS KuangFen,
SUM(KuangFen1_SG)+SUM(KuangFen2_SG)+SUM(KuangFen3_SG)+SUM(KuangFen4_SG) AS KuangFen_SG,

SUM(GuLiao1)+SUM(GuLiao2)+SUM(GuLiao3)+SUM(GuLiao4)+SUM(XiGuLiao) AS GuLiao,
SUM(GuLiao1_SG)+SUM(GuLiao2_SG)+SUM(GuLiao3_SG)+SUM(GuLiao4_SG)+SUM(XiGuLiao_SG) AS GuLiao_SG,

SUM(WaiJiaJi1)+SUM(WaiJiaJi2)+SUM(WaiJiaJi3)+SUM(WaiJiaJi4)  AS WaiJiaJi,
SUM(WaiJiaJi1_SG)+SUM(WaiJiaJi2_SG)+SUM(WaiJiaJi3_SG)+SUM(WaiJiaJi4_SG)  AS WaiJiaJi_SG
  from bhz_PanDetail  WHERE    ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+'''  group BY YEAR(ChuLiaoShiJian), datename(quarter,ChuLiaoShiJian) ORDER BY YEAR(ChuLiaoShiJian), datename(quarter,ChuLiaoShiJian)'
  end    
	 end

	INSERT #tmp2
        ( 
          nian ,
          num ,
          ShuiNi ,
          ShuiNi_SG ,
          FenMeiHui ,
          FenMeiHui_SG ,
          KuangFen ,
          KuangFen_SG ,
          GuLiao ,
          GuLiao_SG ,
          WaiJiaJi ,
          WaiJiaJi_SG
        )
	   EXEC	sp_executesql @sqls
 
 


  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp2

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 

	SET @Counts=@totalcounts

	select  nian ,
          num ,
		   CONVERT(DECIMAL(18,2),ShuiNi)
          ShuiNi ,
		  CONVERT(DECIMAL(18,2),ShuiNi_SG)
          ShuiNi_SG ,
		  (CASE WHEN ShuiNi=0.000000 THEN '0'
							  WHEN ShuiNi>0 THEN CONVERT(DECIMAL(18,2),((ShuiNi_SG-ShuiNi)/ShuiNi)*1000)  				 
							 	END			
							) as ShuiNiWuCha,
							  CONVERT(DECIMAL(18,2),FenMeiHui)
          FenMeiHui ,
		  CONVERT(DECIMAL(18,2),FenMeiHui_SG)
          FenMeiHui_SG ,
		   (CASE WHEN FenMeiHui=0.000000 THEN '0'
							  WHEN FenMeiHui>0 THEN CONVERT(DECIMAL(18,2),((FenMeiHui_SG-FenMeiHui)/FenMeiHui)*1000)  				 
							 	END			
							) as FenMeiHuiWuCha,
							  CONVERT(DECIMAL(18,2),KuangFen)
          KuangFen ,
		    CONVERT(DECIMAL(18,2),KuangFen_SG)
          KuangFen_SG ,
		   (CASE WHEN KuangFen=0.000000 THEN '0'
							  WHEN KuangFen>0 THEN CONVERT(DECIMAL(18,2),((KuangFen_SG-KuangFen)/KuangFen)*1000)  				 
							 	END			
							) as KuangFenWuCha,
							  CONVERT(DECIMAL(18,2),GuLiao)
          GuLiao ,
		    CONVERT(DECIMAL(18,2),GuLiao_SG)
          GuLiao_SG ,
		  (CASE WHEN GuLiao=0.000000 THEN '0'
							  WHEN GuLiao>0 THEN CONVERT(DECIMAL(18,2),((GuLiao_SG-GuLiao)/GuLiao)*1000)  				 
							 	END			
							) as GuLiaoWuCha,
							  CONVERT(DECIMAL(18,2),WaiJiaJi)
          WaiJiaJi ,
		   CONVERT(DECIMAL(18,2),WaiJiaJi_SG)
          WaiJiaJi_SG,
		   (CASE WHEN WaiJiaJi=0.000000 THEN '0'
							  WHEN WaiJiaJi>0 THEN CONVERT(DECIMAL(18,2),((WaiJiaJi_SG-WaiJiaJi)/WaiJiaJi)*1000)  				 
							 	END			
							) as WaiJiaJiWuCha
		   from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

 

END
go


create procedure dbo.bhz_spweb_clhs @testcode  VARCHAR(5000),
 	@ftype TINYINT,--
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN
 create table #tmp1
 (
ID bigint identity(1,1)  primary key,
ChuLiaoShiJian DATETIME,
ProjectName NVARCHAR(50),
 DiDian_LiCheng NVARCHAR(50),
 JiaoZhuBuWei NVARCHAR(50),
 QiangDuDengJi NVARCHAR(50),
 ShiGongPeiHeBiBianHao NVARCHAR(50),
  ShuLiang DECIMAL(18,2)
 )

  DECLARE 

	@sqls nvarchar(4000)
 
  --MAX(ChuLiaoShiJian) ChuLiaoShiJian,ProjectName,DiDian_LiCheng,JiaoZhuBuWei,QiangDuDengJi,ShiGongPeiHeBiBianHao,SUM(ShuLiang) ShuLiang 

  IF @testcode!=''
  BEGIN

		   SET @sqls=' INSERT #tmp1
				 ( ChuLiaoShiJian ,
				   ProjectName ,
				   DiDian_LiCheng ,
				   JiaoZhuBuWei ,
				   QiangDuDengJi ,
				   ShiGongPeiHeBiBianHao ,
				   ShuLiang
				 )SELECT   MAX(ChuLiaoShiJian) ChuLiaoShiJian,ProjectName,DiDian_LiCheng,JiaoZhuBuWei,QiangDuDengJi,ShiGongPeiHeBiBianHao,SUM(ShuLiang) ShuLiang FROM dbo.bhz_PanDetail WHERE MachineCode IN ('+@testcode+') AND ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' GROUP BY  ProjectName,DiDian_LiCheng,JiaoZhuBuWei,QiangDuDengJi,ShiGongPeiHeBiBianHao ORDER BY ChuLiaoShiJian DESC'
 
    EXEC	sp_executesql @sqls
 END
 ELSE
 BEGIN
		
		   SET @sqls=' INSERT #tmp1
				 ( ChuLiaoShiJian ,
				   ProjectName ,
				   DiDian_LiCheng ,
				   JiaoZhuBuWei ,
				   QiangDuDengJi ,
				   ShiGongPeiHeBiBianHao ,
				   ShuLiang
				 )SELECT   MAX(ChuLiaoShiJian) ChuLiaoShiJian,ProjectName,DiDian_LiCheng,JiaoZhuBuWei,QiangDuDengJi,ShiGongPeiHeBiBianHao,SUM(ShuLiang) ShuLiang FROM dbo.bhz_PanDetail WHERE   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' GROUP BY  ProjectName,DiDian_LiCheng,JiaoZhuBuWei,QiangDuDengJi,ShiGongPeiHeBiBianHao ORDER BY ChuLiaoShiJian DESC'

				   EXEC	sp_executesql @sqls
 END

  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

	SET @Counts=@totalcounts

	select * from #tmp1 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  
			
END
go


create procedure dbo.bhz_spweb_cltj @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

CREATE TABLE #tmp
(
zhou INT,
years INT,
zongchanliang FLOAT,
zongpanshu FLOAT,
chuji FLOAT,
chujipanshu FLOAT,
zhongji FLOAT,
zhongjipanshu FLOAT,
gaoji FLOAT,
gaojipanshu FLOAT
)
	
	
 

			select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt1   from bhz_PanDetail WHERE ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate   group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)
			 
			 ORDER BY DATENAME(week,ChuLiaoShiJian)

 
 
			 
 	select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt2  from bhz_PanDetail WHERE  (GuLiao1WuCha BETWEEN -0.02 AND 0.02 )	AND  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate  
	  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian) ORDER BY DATENAME(week,ChuLiaoShiJian)

	 
	
  	select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts  INTO #tt3  from bhz_PanDetail WHERE  (GuLiao1WuCha BETWEEN 0.02 AND 0.05 )	AND  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate  	 
	  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian) ORDER BY DATENAME(week,ChuLiaoShiJian)

  
    	select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt4  from bhz_PanDetail WHERE  (GuLiao1WuCha BETWEEN 0.05 AND 0.1 )	AND  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate  	 
	  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian) ORDER BY DATENAME(week,ChuLiaoShiJian)

 

	  SELECT a.nian,a.num,a.chanliang AS zongchanliang ,a.counts,b.chanliang AS chuji,b.counts chujishu INTO #tt5  FROM #tt1 a LEFT JOIN #tt2 b ON  a.num = b.num
	  
	  SELECT a.*,b.chanliang AS zhongji,b.counts AS zhongjishu  INTO #tt6 FROM #tt5 a LEFT JOIN #tt3 b ON a.num = b.num

	   SELECT a.*,b.chanliang as gaoji ,b.counts as gaojishu INTO #tt7 FROM #tt6 a LEFT JOIN #tt4 b ON a.num = b.num


UPDATE #tt7 SET chuji=0 WHERE chuji IS NULL
UPDATE #tt7 SET chujishu=0 WHERE chujishu IS NULL
UPDATE #tt7 SET zhongji=0 WHERE zhongji IS NULL
UPDATE #tt7 SET zhongjishu=0 WHERE zhongjishu IS NULL
UPDATE #tt7 SET gaoji=0 WHERE gaoji IS NULL
UPDATE #tt7 SET gaojishu=0 WHERE gaojishu IS NULL


 create table #tmp1
 (
ID bigint identity(1,1)  primary key,
nian NVARCHAR(50),
num NVARCHAR(50),
zongchanliang NVARCHAR(50),
counts NVARCHAR(50),
chuji NVARCHAR(50),
chujishu NVARCHAR(50),
zhongji NVARCHAR(50),
zhongjishu NVARCHAR(50),
gaoji NVARCHAR(50),
gaojishu NVARCHAR(50)
 )

 INSERT #tmp1
         ( 
           nian ,
           num ,
           zongchanliang ,
           counts ,
           chuji ,
           chujishu ,
           zhongji ,
           zhongjishu ,
           gaoji ,
           gaojishu
         )
SELECT nian,'第'+num+'周',CONVERT(DECIMAL(18,2),zongchanliang) zongchanliang ,
counts,CONVERT(DECIMAL(18,2),chuji) chuji,chujishu,CONVERT(DECIMAL(18,2),zhongji) zhongji,zhongjishu,CONVERT(DECIMAL(18,2),gaoji) gaoji,gaojishu
 FROM #tt7 ORDER BY num


 

  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

	SET @Counts=@totalcounts

	select * from #tmp1 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

END
go


create procedure dbo.bhz_spweb_cnfx @testcode  VARCHAR(5000), 
 	@startdate VARCHAR(50),
 	@ftype TINYINT,
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

CREATE TABLE #tmp2
(
id bigint identity(1,1)  primary key,
chanliang VARCHAR(50),
num VARCHAR(50),
nian VARCHAR(50),
counts VARCHAR(50),
djcounts VARCHAR(50),
zjcounts VARCHAR(50),
gjcounts VARCHAR(50),
dj VARCHAR(50),
zj VARCHAR(50),
gj VARCHAR(50)
)


 DECLARE @sqls nvarchar(4000)	
 
	
 IF @ftype=1
	 BEGIN


	 CREATE TABLE #tt1
	 (
	 chanliang NVARCHAR(50),
	 num NVARCHAR(50),
	 nian NVARCHAR(50),
	 counts NVARCHAR(50)
	 )
    
	IF @testcode!=''
		BEGIN
			   SET @sqls='  INSERT #tt1
						 ( chanliang, num, nian, counts )
			select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND MachineCode IN ('+@testcode+')   group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian) ORDER BY DATENAME(week,ChuLiaoShiJian) '
		END
	ELSE
		BEGIN
		   SET @sqls='  INSERT #tt1
					 ( chanliang, num, nian, counts )
		select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+'''  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian) ORDER BY DATENAME(week,ChuLiaoShiJian) '
		END
  EXEC sp_executesql @sqls 


 CREATE TABLE #ttt1
 (
 num NVARCHAR(50),
 nian NVARCHAR(50),
 djcounts NVARCHAR(50)
 )
 	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #ttt1
         ( num, nian, djcounts )
		 select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=1  AND MachineCode IN ('+@testcode+') 
  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian)  '
		END
	ELSE
		BEGIN
		   SET @sqls=' INSERT #ttt1
         ( num, nian, djcounts )
		 select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=1   
  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian)  '
		END
  EXEC sp_executesql @sqls 
 

 
  CREATE TABLE #tttt1
 (
 num NVARCHAR(50),
 nian NVARCHAR(50),
 zjcounts NVARCHAR(50)
 )
  	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #tttt1
         ( num, nian, zjcounts )
		   select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts    from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=2  AND MachineCode IN ('+@testcode+')   group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian)   '
		END
	ELSE
		BEGIN
		   SET @sqls=' INSERT #tttt1
         ( num, nian, zjcounts )
		   select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts    from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=2     group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian)    '
		END
  EXEC sp_executesql @sqls 


    CREATE TABLE #ttttt1
 (
 num NVARCHAR(50),
 nian NVARCHAR(50),
 gjcounts NVARCHAR(50)
 )
   	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #ttttt1
         ( num, nian, gjcounts )
		      select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts     from bhz_PanDetail where  ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=3  AND MachineCode IN ('+@testcode+')  
  group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian)   '
		END
	ELSE
		BEGIN
		   SET @sqls=' INSERT #ttttt1
         ( num, nian, gjcounts )
		      select DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts     from bhz_PanDetail where  ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=3   group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)  ORDER BY DATENAME(week,ChuLiaoShiJian)   '
		END
  EXEC sp_executesql @sqls 

 

 
SELECT a.chanliang,a.nian,a.num,a.counts,b.djcounts,c.zjcounts,d.gjcounts INTO #qq FROM #tt1 a LEFT JOIN #ttt1 b ON a.nian=b.nian AND a.num=b.num 
LEFT JOIN #tttt1 c ON a.nian=c.nian AND a.num=c.num
LEFT JOIN #ttttt1 d ON a.nian=d.nian AND a.num=d.num


UPDATE #qq SET djcounts=0 WHERE djcounts IS NULL
UPDATE #qq SET zjcounts=0 WHERE zjcounts IS NULL
UPDATE #qq SET gjcounts=0 WHERE gjcounts IS NULL
 
 

 INSERT #tmp2
        ( 
		 nian ,
		   num ,
          chanliang ,    
          counts ,
          djcounts ,
          zjcounts ,
          gjcounts,
		  dj,
zj,
gj
        )
 SELECT  nian,'第'+num+'周' AS num,CONVERT(DECIMAL(18,2),chanliang) AS chanliang,counts,djcounts,zjcounts,gjcounts,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,djcounts)/CONVERT(FLOAT,counts)*100)) as dj,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,zjcounts)/CONVERT(FLOAT,counts)*100))  AS zj,
CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,gjcounts)/CONVERT(FLOAT,counts)*100))   AS gj FROM #qq




 


	END
   IF @ftype=2
	 BEGIN


	 	 CREATE TABLE #tt3
	 (
	 chanliang NVARCHAR(50),
	 num NVARCHAR(50),
	 nian NVARCHAR(50),
	 counts NVARCHAR(50)
	 )
    
	IF @testcode!=''
		BEGIN
			   SET @sqls='  INSERT #tt3
						 ( chanliang, num, nian, counts )
			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND MachineCode IN ('+@testcode+')    group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian) '
		END
	ELSE
		BEGIN
		   SET @sqls='  INSERT #tt3
						 ( chanliang, num, nian, counts )
			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+'''    group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian) '
		END
  EXEC sp_executesql @sqls 

 


  CREATE TABLE #ttt3
 (
 chanliang  NVARCHAR(50),
 num NVARCHAR(50),
 nian NVARCHAR(50),
 djcounts NVARCHAR(50)
 )
 	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #ttt3
         (chanliang, num, nian, djcounts )
		select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts  from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=1  AND MachineCode IN ('+@testcode+') 
   group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by  month(ChuLiaoShiJian) '
		END
	ELSE
		BEGIN
		   SET @sqls=' INSERT #ttt3
         (chanliang, num, nian, djcounts )
		select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts  from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=1    group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by  month(ChuLiaoShiJian) '
		END
  EXEC sp_executesql @sqls 


 

		 CREATE TABLE #tttt3
 (
  chanliang  NVARCHAR(50),
 num NVARCHAR(50),
 nian NVARCHAR(50),
 zjcounts NVARCHAR(50)
 )
  	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #tttt3
         (chanliang, num, nian, zjcounts )
		  select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts     from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=2  AND MachineCode IN ('+@testcode+')    group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian)   '
		END
	ELSE
		BEGIN
		   SET @sqls=' INSERT #tttt3
         (chanliang, num, nian, zjcounts )
		  select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts     from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=2      group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian)   '
		END
  EXEC sp_executesql @sqls 

 
  CREATE TABLE #ttttt3
 (
 chanliang  NVARCHAR(50),
 num NVARCHAR(50),
 nian NVARCHAR(50),
 gjcounts NVARCHAR(50)
 )
   	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #ttttt3
         (chanliang, num, nian, gjcounts )
		       	select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts    from bhz_PanDetail where  ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=3  AND MachineCode IN ('+@testcode+')  
  group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian) '
		END
	ELSE
		BEGIN
		   SET @sqls=' INSERT #ttttt3
         (chanliang, num, nian, gjcounts )
		       	select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts    from bhz_PanDetail where  ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=3     group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by   month(ChuLiaoShiJian) '
		END
  EXEC sp_executesql @sqls 

 



			  
SELECT a.chanliang,a.nian,a.num,a.counts,b.djcounts,c.zjcounts,d.gjcounts INTO #qqq FROM #tt3 a LEFT JOIN #ttt3 b ON a.nian=b.nian AND a.num=b.num 
LEFT JOIN #tttt3 c ON a.nian=c.nian AND a.num=c.num
LEFT JOIN #ttttt3 d ON a.nian=d.nian AND a.num=d.num


UPDATE #qqq SET djcounts=0 WHERE djcounts IS NULL
UPDATE #qqq SET zjcounts=0 WHERE zjcounts IS NULL
UPDATE #qqq SET gjcounts=0 WHERE gjcounts IS NULL
 
 INSERT #tmp2
        ( 
          chanliang ,
          num ,
          nian ,
          counts ,
          djcounts ,
          zjcounts ,
          gjcounts,
		  dj,
zj,
gj
        )
 SELECT  nian,num+'月' AS num,CONVERT(DECIMAL(18,2),chanliang) AS chanliang,counts,djcounts,zjcounts,gjcounts,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,djcounts)/CONVERT(FLOAT,counts)*100)) as dj,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,zjcounts)/CONVERT(FLOAT,counts)*100))  AS zj,
CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,gjcounts)/CONVERT(FLOAT,counts)*100))   AS gj FROM #qqq


		 
	END
	
	 IF @ftype=3
	 BEGIN
  
  	 CREATE TABLE #tt5
	 (
	 chanliang NVARCHAR(50),
	 num NVARCHAR(50),
	 nian NVARCHAR(50),
	 counts NVARCHAR(50)
	 )
    
	IF @testcode!=''
		BEGIN
			   SET @sqls='  INSERT #tt5
						 ( chanliang, num, nian, counts )
			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND MachineCode IN ('+@testcode+')   group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian) '
		END
	ELSE
		BEGIN
		   SET @sqls='  INSERT #tt5
						 ( chanliang, num, nian, counts )
			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts   from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+'''   group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian) '
		END
  EXEC sp_executesql @sqls    

	 

	  CREATE TABLE #ttt5
 (
 chanliang  NVARCHAR(50),
 num NVARCHAR(50),
 nian NVARCHAR(50),
 djcounts NVARCHAR(50)
 )
 	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #ttt5
         (chanliang, num, nian, djcounts )
	select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts  from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=1  AND MachineCode IN ('+@testcode+') 
 group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian) '
		END
	ELSE
		BEGIN
		    SET @sqls=' INSERT #ttt5
         (chanliang, num, nian, djcounts )
	select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS djcounts  from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=1  group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian) '
		END
  EXEC sp_executesql @sqls 
		 

		  CREATE TABLE #tttt5
 (
 chanliang NVARCHAR(50),
 num NVARCHAR(50),
 nian NVARCHAR(50),
 zjcounts NVARCHAR(50)
 )
  	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #tttt5
         (chanliang, num, nian, zjcounts )
		  	select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts    from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=2  AND MachineCode IN ('+@testcode+')   group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)  '
		END
	ELSE
		BEGIN
		   SET @sqls=' INSERT #tttt5
         (chanliang, num, nian, zjcounts )
		  	select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS zjcounts    from bhz_PanDetail where   ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=2  AND MachineCode IN ('+@testcode+')   group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)  '
		END
  EXEC sp_executesql @sqls 

		 
		  CREATE TABLE #ttttt5
 (chanliang  NVARCHAR(50),
 num NVARCHAR(50),
 nian NVARCHAR(50),
 gjcounts NVARCHAR(50)
 )
   	IF @testcode!=''
		BEGIN
			   SET @sqls=' INSERT #ttttt5
         (chanliang, num, nian, gjcounts )
		      select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts    from bhz_PanDetail where  ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=3  AND MachineCode IN ('+@testcode+')  
 group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)  '
		END
	ELSE
		BEGIN
		    SET @sqls=' INSERT #ttttt5
         (chanliang, num, nian, gjcounts )
		      select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS gjcounts    from bhz_PanDetail where  ChuLiaoShiJian>='''+@startdate+''' AND ChuLiaoShiJian<'''+@enddate+''' AND ChaoBiaoDengJi=3  AND MachineCode IN ('+@testcode+')  
 group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian) ORDER BY  datename(quarter,ChuLiaoShiJian)  '
		END
  EXEC sp_executesql @sqls 

		






			  
SELECT a.chanliang,a.nian,a.num,a.counts,b.djcounts,c.zjcounts,d.gjcounts INTO #qqqq FROM #tt5 a LEFT JOIN #ttt5 b ON a.nian=b.nian AND a.num=b.num 
LEFT JOIN #tttt5 c ON a.nian=c.nian AND a.num=c.num
LEFT JOIN #ttttt5 d ON a.nian=d.nian AND a.num=d.num


UPDATE #qqqq SET djcounts=0 WHERE djcounts IS NULL
UPDATE #qqqq SET zjcounts=0 WHERE zjcounts IS NULL
UPDATE #qqqq SET gjcounts=0 WHERE gjcounts IS NULL
 
 INSERT #tmp2
        (
          chanliang ,
          num ,
          nian ,
          counts ,
          djcounts ,
          zjcounts ,
          gjcounts,
		  dj,
zj,
gj
        )
 SELECT  nian,'第'+num+'季度' AS num,CONVERT(DECIMAL(18,2),chanliang) AS chanliang,counts,djcounts,zjcounts,gjcounts,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,djcounts)/CONVERT(FLOAT,counts)*100)) as dj,CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,zjcounts)/CONVERT(FLOAT,counts)*100))  AS zj,
CONVERT(DECIMAL(18,2),(CONVERT(FLOAT,gjcounts)/CONVERT(FLOAT,counts)*100))   AS gj FROM #qqqq





	END  
	

 

  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp2

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

	SET @Counts=@totalcounts

	select * from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  


END
go


create procedure dbo.bhz_spweb_dtzs @testcode  VARCHAR(5000),
 	@ftype TINYINT, 
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN
 


 


	select  ProjectName, DiDian_LiCheng, JiaoZhuBuWei, ChuLiaoShiJian, CaoZuoZhe,MachineCode INTO #t1  from bhz_v_PanDetail as a inner join
(
select max(ChuLiaoShiJian) as shijian From bhz_v_PanDetail Group by MachineCode
) as b
on a.ChuLiaoShiJian=b.shijian  


  DECLARE 

	@sqls nvarchar(4000)
	IF @testcode!=''
	BEGIN
    
 SET @sqls='SELECT SGName+''-''+StationName+''-''+MachineName MachineName,b.* FROM dbo.bhz_v_Tree a LEFT JOIN #t1 b ON  a.MachineCode = b.MachineCode  WHERE a.MachineCode IN ('+@testcode+')'
 END
 ELSE
 BEGIN
  SET @sqls='SELECT SGName+''-''+StationName+''-''+MachineName MachineName,b.* FROM dbo.bhz_v_Tree a LEFT JOIN #t1 b ON  a.MachineCode = b.MachineCode '
 END
		   EXEC	sp_executesql @sqls


 
 


			
END
go


create procedure dbo.bhz_spweb_dxbj @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN
 create table #tmp1
 (
ID bigint identity(1,1)  primary key,
SavedTime DATETIME,
ProjectName varchar(50),
DiDian_LiCheng varchar(50),
JiaoZhuBuWei varchar(50),
SMSContent NVARCHAR(4000), 
Sender VARCHAR(500)
 )


 
INSERT #tmp1
        (  
          SavedTime ,
          ProjectName ,
          DiDian_LiCheng ,
          JiaoZhuBuWei ,
          SMSContent ,
          Sender
        ) 
 SELECT ChuLiaoShiJian,ProjectName,DiDian_LiCheng,JiaoZhuBuWei,( ProjectName+'-'+JiaoZhuBuWei+'<br/>水泥1实'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),ShuiNi1))+'配'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),ShuiNi1_SG))+'误'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),ShuiNi1WuCha))+'%;'+'<br/>粗骨料1实'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),GuLiao1))+'配'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),GuLiao1_SG))+'误'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),GuLiao1WuCha))+'%
;'+'<br/>矿粉实'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),KuangFen1))+'配'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),KuangFen1_SG))+'误'+CONVERT(NVARCHAR(50),CONVERT(DECIMAL(18,2),KuangFen1WuCha))+'%;') AS SMSContent,'134****8971,<br/>135****8463,<br/>189****7623' AS Sender FROM dbo.bhz_PanDetail WHERE ChuLiaoShiJian>=@startdate AND 
ChuLiaoShiJian<@enddate AND ShuiNi1WuCha>0.1 AND GuLiao1WuCha>0.1 AND KuangFen1WuCha>0.1 ORDER BY ChuLiaoShiJian DESC

 




 
 


  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

	SET @Counts=@totalcounts

	select * from #tmp1 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  
			
END
go


create procedure dbo.bhz_spweb_login_charts @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 DECLARE @sqls nvarchar(4000)	

 CREATE  TABLE #t1
 (
 TestRoomCode VARCHAR(50),
  nTotals INT,
 CompanyName VARCHAR(50),
 SegmentName VARCHAR(50),
 TestRoomName VARCHAR(50),
 CompanyCode VARCHAR(50)
 )



 IF @testcode!=''
   BEGIN
   SET @sqls=' INSERT #t1
         ( TestRoomCode ,
           nTotals ,
           CompanyName ,
           SegmentName ,
           TestRoomName ,
           CompanyCode
         )
 SELECT LEFT(MachineCode,12) TestRoomCode,SUM(Counts) nTotals, MAX(SGName) CompanyName,
MAX(StationName) SegmentName,MAX(StationName) TestRoomName,LEFT(MachineCode,12) CompanyCode
 FROM  dbo.bhz_v_user_log WHERE LEN(MachineCode)>8 AND TestCode IN ('+@testcode+') AND 
 LastDateTime>='''+@startdate+'''  AND LastDateTime<'''+@enddate+''' GROUP BY LEFT(MachineCode,12) '
 END
 ELSE
		 BEGIN
			   SET @sqls=' INSERT #t1
         ( TestRoomCode ,
           nTotals ,
           CompanyName ,
           SegmentName ,
           TestRoomName ,
           CompanyCode
         )
SELECT LEFT(MachineCode,12) TestRoomCode,SUM(Counts) nTotals, MAX(SGName) CompanyName,
MAX(StationName) SegmentName,MAX(StationName) TestRoomName,LEFT(MachineCode,12) CompanyCode
 FROM  dbo.bhz_v_user_log WHERE LEN(MachineCode)>8  AND 
 LastDateTime>='''+@startdate+'''  AND LastDateTime<'''+@enddate+''' GROUP BY LEFT(MachineCode,12) '       
		 END
 
 


  EXEC sp_executesql @sqls 

  CREATE TABLE #t2
  (
  UserName VARCHAR(50),
  TestRoomCode VARCHAR(50)
  )



  IF @testcode!=''

  BEGIN
SET @sqls='    INSERT #t2
          ( UserName, TestRoomCode )
SELECT UserName,MAX(TestCode) TestRoomCode  FROM dbo.bhz_user_log  WHERE LEN(TestCode)>8  AND TestCode IN ('+@testcode+')  AND LastDateTime>='''+@startdate+'''  AND LastDateTime<'''+@enddate+'''   GROUP BY UserName '
END

ELSE
	BEGIN
		SET @sqls='    INSERT #t2
          ( UserName, TestRoomCode )
SELECT UserName,MAX(TestCode) TestRoomCode  FROM dbo.bhz_user_log  WHERE LEN(TestCode)>8  AND   LastDateTime>='''+@startdate+'''  AND LastDateTime<'''+@enddate+'''   GROUP BY UserName '
  END  

 





 EXEC sp_executesql @sqls 




 SELECT LEFT(TestRoomCode,12) TestRoomCode,COUNT(1) nUserCounts INTO #t3 FROM #t2  GROUP BY LEFT(TestRoomCode,12)


 SELECT MAX(CompanyName) CompanyName,MAX(SegmentName) SegmentName,SUM(nTotals) nTotals,SUM(nUserCounts) nUserCounts,CompanyCode INTO #t4 FROM #t1 a JOIN #t3 b ON a.TestRoomCode = LEFT(b.TestRoomCode,12)   GROUP BY  a.CompanyCode
  

  --SELECT a.* FROM #t4 a JOIN dbo.Sys_Tree b ON a.CompanyCode=b.NodeCode ORDER BY OrderID

  SELECT * FROM #t4
			
END
go


create procedure dbo.bhz_spweb_login_charts_pop @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 

 

 SELECT UserName,MAX(StationName) SegmentName,MAX(SGName) CompanyName,MAX(MachineName) TestRoomName,SUM(Counts) nTotals FROM dbo.bhz_v_user_log WHERE LEFT(MachineCode,12)=@testcode AND LEN(MachineCode)>8   AND LastDateTime>=@startdate  AND LastDateTime<@enddate GROUP BY UserName ORDER BY COUNT(1) DESC
   
   
			
END
go


create procedure dbo.bhz_spweb_login_one @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 
 
 
SELECT CONVERT(NVARCHAR(50),LastDateTime,23) LastDateTime,MAX(UserName) UserName,COUNT(1) nTotals FROM dbo.bhz_user_log WHERE UserName=@testcode AND  LEN(TestCode)>8 AND LastDateTime>=@startdate AND LastDateTime<@enddate  GROUP BY CONVERT(NVARCHAR(50),LastDateTime,23)
			
END
go


create procedure dbo.bhz_spweb_pager (
 @tblName     nvarchar(200),        ----要显示的表或多个表的连接
 @fldName     nvarchar(500) = '*',    ----要显示的字段列表
 @pageSize    int = 1,        ----每页显示的记录个数
 @page        int = 10,        ----要显示那一页的记录
 @pageCount    int = 1 output,            ----查询结果分页后的总页数
 @Counts    int = 1 output,                ----查询到的记录数
 @fldSort    nvarchar(200) = null,    ----排序字段列表或条件
 @Sort        bit = 1,        ----排序方法，0为升序，1为降序(如果是多字段排列Sort指代最后一个排序字段的排列顺序(最后一个排序字段不加排序标记)--程序传参如：' SortA Asc,SortB Desc,SortC ')
 @strCondition    nvarchar(4000) = null,    ----查询条件,不需where
 @ID        nvarchar(150),        ----主表的主键
 @Dist                 bit = 0           ----是否添加查询字段的 DISTINCT 默认0不添加/1添加
 ) as
SET NOCOUNT ON
Declare @sqlTmp nvarchar(4000)        ----存放动态生成的SQL语句
Declare @strTmp nvarchar(4000)        ----存放取得查询结果总数的查询语句
Declare @strID     nvarchar(1000)        ----存放取得查询开头或结尾ID的查询语句

Declare @strSortType nvarchar(10)    ----数据排序规则A
Declare @strFSortType nvarchar(10)    ----数据排序规则B

Declare @SqlSelect nvarchar(50)         ----对含有DISTINCT的查询进行SQL构造
Declare @SqlCounts nvarchar(50)          ----对含有DISTINCT的总数查询进行SQL构造


if @Dist  = 0
begin
    set @SqlSelect = 'select '
    set @SqlCounts = 'Count(*)'
end
else
begin
    set @SqlSelect = 'select distinct '
    set @SqlCounts = 'Count(DISTINCT '+@ID+')'
end


if @Sort=0
begin
    set @strFSortType=' ASC '
    set @strSortType=' DESC '
end
else
begin
    set @strFSortType=' DESC '
    set @strSortType=' ASC '
end

 

--------生成查询语句--------
--此处@strTmp为取得查询结果数量的语句
if @strCondition is null or @strCondition=''     --没有设置显示条件
begin
    set @sqlTmp =  @fldName + ' From ' + @tblName
    set @strTmp = @SqlSelect+' @Counts='+@SqlCounts+' FROM '+@tblName
    set @strID = ' From ' + @tblName
end
else
begin
    set @sqlTmp = + @fldName + 'From ' + @tblName + ' where (1>0) ' + @strCondition
    set @strTmp = @SqlSelect+' @Counts='+@SqlCounts+' FROM '+@tblName + ' where (1>0) ' + @strCondition
    set @strID = ' From ' + @tblName + ' where (1>0) ' + @strCondition
end

----取得查询结果总数量-----
exec sp_executesql @strTmp,N'@Counts int out ',@Counts out
declare @tmpCounts int
if @Counts = 0
    set @tmpCounts = 1
else
    set @tmpCounts = @Counts

    --取得分页总数
    set @pageCount=(@tmpCounts+@pageSize-1)/@pageSize

    /**//**当前页大于总页数 取最后一页**/
    if @page>@pageCount
        set @page=@pageCount

    --/*-----数据分页2分处理-------*/
    declare @pageIndex int --总数/页大小
    declare @lastcount int --总数%页大小 

    set @pageIndex = @tmpCounts/@pageSize
    set @lastcount = @tmpCounts%@pageSize
    if @lastcount > 0
        set @pageIndex = @pageIndex + 1
    else
        set @lastcount = @pagesize
 --//***显示分页
    if @strCondition is null or @strCondition=''     --没有设置显示条件
    begin
        if @pageIndex<2 or @page<=@pageIndex / 2 + @pageIndex % 2   --前半部分数据处理
            begin 
                set @strTmp=@SqlSelect+' top '+ CAST(@pageSize as VARCHAR(4))+' '+ @fldName+' from '+@tblName
                        +' where '+@ID+' not in('+ @SqlSelect+' top '+ CAST(@pageSize*(@page-1) as Varchar(20)) +' '+ @ID +' from '+@tblName
                        +' order by '+ @fldSort +' '+ @strFSortType+')'
                        +' order by '+ @fldSort +' '+ @strFSortType 
            end
        else
            begin
            set @page = @pageIndex-@page+1 --后半部分数据处理
                if @page <= 1 --最后一页数据显示
                    set @strTmp=@SqlSelect+' * from ('+@SqlSelect+' top '+ CAST(@lastcount as VARCHAR(4))+' '+ @fldName+' from '+@tblName
                        +' order by '+ @fldSort +' '+ @strSortType+') AS TempTB'+' order by '+ @fldSort +' '+ @strFSortType 
                else                
                    set @strTmp=@SqlSelect+' * from ('+@SqlSelect+' top '+ CAST(@pageSize as VARCHAR(4))+' '+ @fldName+' from '+@tblName
                        +' where '+@ID+' not in('+ @SqlSelect+' top '+ CAST(@pageSize*(@page-2)+@lastcount as Varchar(20)) +' '+ @ID +' from '+@tblName
                        +' order by '+ @fldSort +' '+ @strSortType+')'

                        +' order by '+ @fldSort +' '+ @strSortType+') AS TempTB'+' order by '+ @fldSort +' '+ @strFSortType 
            end
    end

    else --有查询条件
    begin
        if @pageIndex<2 or @page<=@pageIndex / 2 + @pageIndex % 2   --前半部分数据处理
        begin 
                set @strTmp=@SqlSelect+' top '+ CAST(@pageSize as VARCHAR(4))+' '+ @fldName +' from  '+@tblName
                    +' where '+@ID+' not in('+ @SqlSelect+' top '+ CAST(@pageSize*(@page-1) as Varchar(20)) +' '+ @ID +' from '+@tblName
                    +' Where (1>0) ' + @strCondition + ' order by '+ @fldSort +' '+ @strFSortType+')'
                    +' ' + @strCondition + ' order by '+ @fldSort +' '+ @strFSortType                 
        end
        else
        begin 
            set @page = @pageIndex-@page+1 --后半部分数据处理
            if @page <= 1 --最后一页数据显示
                    set @strTmp=@SqlSelect+' * from ('+@SqlSelect+' top '+ CAST(@lastcount as VARCHAR(4))+' '+ @fldName+' from '+@tblName
                        +' where (1>0) '+ @strCondition +' order by '+ @fldSort +' '+ @strSortType+') AS TempTB'+' order by '+ @fldSort +' '+ @strFSortType
            else
                    set @strTmp=@SqlSelect+' * from ('+@SqlSelect+' top '+ CAST(@pageSize as VARCHAR(4))+' '+ @fldName+' from '+@tblName
                        +' where '+@ID+' not in('+ @SqlSelect+' top '+ CAST(@pageSize*(@page-2)+@lastcount as Varchar(20)) +' '+ @ID +' from '+@tblName
                        +' where (1>0) '+ @strCondition +' order by '+ @fldSort +' '+ @strSortType+')'
                        + @strCondition +' order by '+ @fldSort +' '+ @strSortType+') AS TempTB'+' order by '+ @fldSort +' '+ @strFSortType 
        end    
    end

------返回查询结果-----
exec sp_executesql @strTmp
--print @strTmp
SET NOCOUNT OFF
go


create procedure dbo.bhz_spweb_wcfx @testcode  VARCHAR(5000), 
 	@startdate VARCHAR(50),
 	@ftype TINYINT,
 	@enddate VARCHAR(50) as
BEGIN


	
 IF @ftype=1
	 BEGIN

			select sum(ShuLiang) AS chanliang,DATENAME(week,ChuLiaoShiJian) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt1 from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate   group by datename(week,ChuLiaoShiJian) ,YEAR(ChuLiaoShiJian)

 SELECT chanliang,
 '第'+num+'周' num,
 nian,
 counts FROM #tt1

 
			 

	END
   IF @ftype=2
	 BEGIN

			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),month(ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt3 from bhz_PanDetail where   ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate group by month(ChuLiaoShiJian),YEAR(ChuLiaoShiJian) order by num

			SELECT chanliang,
  '第'+num+'月' num,
 nian,
 counts FROM #tt3
		 
	END
	
	 IF @ftype=3
	 BEGIN

			select sum(ShuLiang) AS chanliang,CONVERT(NVARCHAR(50),datename(quarter,ChuLiaoShiJian)) AS num,YEAR(ChuLiaoShiJian) AS nian,COUNT(1) AS counts INTO #tt5 from bhz_PanDetail WHERE  ChuLiaoShiJian>=@startdate AND ChuLiaoShiJian<@enddate group by datename(quarter,ChuLiaoShiJian),YEAR(ChuLiaoShiJian)

 SELECT chanliang,
  '第'+num+'季度' num,
 nian,
 counts FROM #tt5
	END  
	

 

END
go


create procedure dbo.sp_GetSGTestCode as
BEGIN
	


 
	

 CREATE TABLE #temp
 (
 sgtestcode NVARCHAR(50),
 jltestcode NVARCHAR(50)
 )
	DECLARE @ConstructionCompany NVARCHAR(4000),
	 @id NVARCHAR(50)
 
 		 
		DECLARE cur CURSOR FOR
 

	 SELECT a.ConstructionCompany,b.NodeCode   FROM dbo.sys_engs_CompanyInfo a JOIN dbo.sys_engs_Tree b ON a.ID=b.RalationID WHERE DepType='@unit_监理单位'

		OPEN cur
		FETCH NEXT FROM cur INTO @ConstructionCompany,@id
		WHILE @@FETCH_STATUS = 0
	BEGIN


	INSERT #temp
	        ( sgtestcode,jltestcode )
SELECT *,@id FROM dbo.Fweb_SplitExpression(@ConstructionCompany,',')




					  	   FETCH NEXT FROM cur INTO  @ConstructionCompany,@id
	END

	CLOSE cur
	DEALLOCATE cur	  



INSERT dbo.sys_sg_jl_relation
        ( jltestcode ,
          jlname ,
          sgtestcode ,
          sgname
        )
 SELECT a.jltestcode,d.Description AS jlname,b.NodeCode AS sgtestcode ,c.Description AS sgname   FROM #temp a JOIN dbo.sys_engs_Tree b ON a.sgtestcode=b.RalationID JOIN  dbo.v_codeName c ON b.NodeCode=c.NodeCode JOIN dbo.v_codeName d ON a.jltestcode=d.NodeCode

 END
go


create procedure dbo.sp_document_list @strGetFields nvarchar(3000) , -- 需要返回的列
  	@fldName varchar(255), -- 排序的字段名
  	@PageSize int , -- 页尺寸
  	@PageIndex int , -- 页码
  	@OrderType bit , -- 设置排序类型, 非0 值则降序
  	@strWhere varchar(6000),  -- 查询条件(注意: 不要加where)
  	@totalCount int output as
BEGIN
	declare @strSQL nvarchar(4000) -- 主语句
	declare @strOrder varchar(400) -- 排序类型
	declare @ct int
	if @OrderType != 0--降序
	begin
		set @strOrder = ' order by ' + @fldName +' desc'--如果@OrderType不是0，就执行降序，这句很重要！
	end
	else
	begin
		set @strOrder = ' order by ' + @fldName +' asc'
	END
	set @strSQL = 'select @a=count(1) from sys_document where 1=1 '+ @strWhere
	
	EXEC sp_executesql @strSQL,N'@a int output',@ct OUTPUT
	set @totalCount=@ct
	set @strSQL = 'select '+@strGetFields+' from sys_document a join (SELECT ID as k FROM (select ID,ROW_NUMBER() over('+@strOrder+') as r from sys_document where 1=1 ' + @strWhere + ') as t where r between ' +str(@PageSize*(@PageIndex-1)+1) +' and '++str(@PageSize*@PageIndex) +') tt on a.ID=tt.k '+@strOrder
	PRINT @strSQL
	exec (@strSQL)
END
go


create procedure dbo.sp_getFormulas @moduleID UNIQUEIDENTIFIER as
BEGIN
	
	SELECT * INTO #tmp FROM (
	SELECT *, 1 AS fromLine FROM dbo.sys_line_formulas WHERE IsActive=1 and ModuleID=@moduleID
	UNION
	SELECT *,0 AS fromLine FROM dbo.sys_formulas WHERE IsActive=1 AND ModuleID=@moduleID ) a

	SELECT ModuleID,SheetID,RowIndex,ColumnIndex INTO #tmp2 FROM #tmp GROUP BY ModuleID,SheetID,RowIndex,ColumnIndex HAVING COUNT(1)>1

	DELETE FROM #tmp
	FROM #tmp a
	JOIN #tmp2 b ON a.fromLine=0 AND a.ColumnIndex = b.ColumnIndex 
	AND a.ModuleID = b.ModuleID AND a.RowIndex = b.RowIndex AND a.SheetID = b.SheetID

	SELECT ModuleID,SheetID,RowIndex,ColumnIndex,Formula,fromLine FROM #tmp

END
go


create procedure dbo.sp_getValidTestRoomCode @id varchar(50),
  	@isAdmin INT as
BEGIN
	DECLARE @ids VARCHAR(400)
	IF @isAdmin=1
	BEGIN
		SELECT a.NodeCode FROM sys_engs_Tree a JOIN dbo.sys_engs_ItemInfo b
		ON a.RalationID=b.ID
	END
	ELSE
	BEGIN
		
		SELECT @ids=''''+REPLACE(ConstructionCompany,',',''',''')+'''' FROM dbo.sys_engs_CompanyInfo
		where id=@id
		IF @ids<>''''''
		BEGIN
			exec('select a.NodeCode from sys_engs_Tree a 
			join (select NodeCode from sys_engs_Tree where RalationID in('+@ids+')) b on Left(a.NodeCode,12)=b.NodeCode
			where len(a.NodeCode)=16')
		END
		ELSE
		BEGIN
			SELECT @ids=NodeCode FROM sys_engs_Tree WHERE RalationID=@id
			SELECT NodeCode FROM dbo.sys_engs_Tree WHERE LEFT(NodeCode,12)=@ids AND LEN(NodeCode)=16
		END
			
	END
END
go


create procedure dbo.sp_newPXJZ_ByNewModel @modelID VARCHAR(50), 
 	@modelCode VARCHAR(50), 
 	@modelName VARCHAR(50) as
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @testRoomID varchar(50),@testRoomCode VARCHAR(50)

	DECLARE cur CURSOR FOR
	SELECT ID FROM dbo.sys_engs_ItemInfo

	OPEN cur
	FETCH NEXT FROM cur INTO @testRoomID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	   SELECT @testRoomCode=NodeCode FROM dbo.sys_engs_Tree WHERE RalationID=@testRoomID
	   
	   INSERT INTO dbo.sys_biz_reminder_Itemfrequency
        ( ModelIndex ,
          ModelCode ,
          TestRoomID ,
          TestRoomCode ,
          ModelName ,
          Frequency ,
          FrequencyType ,
          IsActive
        )
		VALUES  
		( @modelID , -- ModelIndex - nvarchar(50)
          @modelCode , -- ModelCode - nvarchar(50)
          @testRoomID , -- TestRoomID - nvarchar(50)
          @testRoomCode , -- TestRoomCode - nvarchar(50)
          @modelName , -- ModelName - nvarchar(100)
          0.0 , -- Frequency - float
          1 , -- FrequencyType - tinyint
          0  -- IsActive - tinyint
        )
		
		INSERT INTO dbo.sys_biz_reminder_Itemfrequency
			( ModelIndex ,
			  ModelCode,
			  TestRoomID ,
			  TestRoomCode,
			  ModelName ,
			  Frequency ,
			  FrequencyType ,
			  IsActive
			)
		VALUES  
		( @modelID , -- ModelIndex - nvarchar(50)
          @modelCode , -- ModelCode - nvarchar(50)
          @testRoomID , -- TestRoomID - nvarchar(50)
          @testRoomCode , -- TestRoomCode - nvarchar(50)
          @modelName , -- ModelName - nvarchar(100)
          0.0 , -- Frequency - float
          2 , -- FrequencyType - tinyint
          0  -- IsActive - tinyint
        )
		
	   FETCH NEXT FROM cur INTO @testRoomID
	END

	CLOSE cur
	DEALLOCATE cur

END
go


create procedure dbo.sp_newPX_Chart @testRoomID VARCHAR(50), --标准实验室id
 	@idList varchar(300), --有权限的实验室id集合
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@start varchar(30),
 	@end varchar(30),
 	@modelID varchar(50) as
BEGIN


	
		CREATE TABLE #tmp
	(
	chartDate VARCHAR(20),
	zjCount INT,
	pxjzCount INT,	
	)
	
		CREATE TABLE #tmp1
	(
	chartDate VARCHAR(20),
	countnum INT	
	)
	
		CREATE TABLE #tmp2
	(
	chartDate VARCHAR(20),
	countnum INT	
	)
	
	CREATE TABLE #tmp3
	(
	chartDate1 VARCHAR(20),
	chartDate2 VARCHAR(20),
	zjCount INT,
	pxjzCount INT,	
	)
 
 DECLARE 

	@sqls nvarchar(4000)

		
	   SET @sqls='INSERT #tmp1
				   ( chartDate, countnum ) select REPLACE(CONVERT(varchar(12) , a.BGRQ, 111 ) ,''/'',''-''),count(*) from [biz_norm_extent_'+@modelID+'] a 
	   where (a.trytype=''自检'' or a.trytype=''见证'') AND SUBSTRING(a.SCPT,0,17)='''+@testRoomID+''' AND a.BGRQ>='''+@start+''' AND a.BGRQ<'''+@end+'''
	   GROUP by REPLACE(CONVERT(varchar(12) , a.BGRQ, 111 )  ,''/'',''-'') '
	   
	  
	   EXEC(@sqls)
	   
	   IF(@ftype=1)
	   begin
			 
			  SET @sqls='INSERT #tmp2
				   ( chartDate, countnum ) select REPLACE(CONVERT(varchar(12) , c.BGRQ, 111 )  ,''/'',''-''),count(*) from [biz_norm_extent_'+@modelID+'] c where c.ID in (select b.PXDataID from [biz_norm_extent_'+@modelID+'] a 
		   join dbo.biz_px_relation b on a.ID=b.SGDataID
		   where (a.trytype=''自检'' or a.trytype=''见证'') AND SUBSTRING(a.SCPT,0,17)='''+@testRoomID+''' AND a.BGRQ>='''+@start+''' AND a.BGRQ<'''+@end+''')
		   and c.BGRQ>='''+@start+''' AND c.BGRQ<'''+@end+'''
		   GROUP by REPLACE(CONVERT(varchar(12) , c.BGRQ, 111 )  ,''/'',''-'') '
		   print @sqls
		   EXEC(@sqls)
		END
		
		 IF(@ftype=2)
	   begin
			 SET @sqls='  INSERT #tmp2
				   ( chartDate, countnum ) select REPLACE(CONVERT(varchar(12) , BGRQ, 111 )  ,''/'',''-''),count(*) from [biz_norm_extent_'+@modelID+'] a 
		   where trytype=''见证'' AND SUBSTRING(a.SCPT,0,17)='''+@testRoomID+''' AND a.BGRQ>='''+@start+''' AND a.BGRQ<'''+@end+'''
		   GROUP by REPLACE(CONVERT(varchar(12) , BGRQ, 111 )  ,''/'',''-'') '
		
			
		   EXEC(@sqls)
		END
		
   
		
		--SELECT a.chartDate,b.chartDate,a.countnum,b.countnum FROM #tmp1 a FULL JOIN #tmp2 b ON a.chartDate = b.chartDate
		
		
		INSERT #tmp3
		        ( chartDate1 ,
		          chartDate2 ,
		          zjCount ,
		          pxjzCount
		        )
		SELECT a.chartDate,b.chartDate,a.countnum,b.countnum FROM #tmp1 a FULL JOIN #tmp2 b ON a.chartDate = b.chartDate
		
		UPDATE #tmp3 SET chartDate1=chartDate2 WHERE chartDate1 IS NULL
		
		
		INSERT #tmp
		        ( chartDate, zjCount, pxjzCount )
		SELECT chartDate1,zjCount,pxjzCount FROM  #tmp3

		update #tmp set zjCount=0 where zjCount is null
		update #tmp set pxjzCount=0 where pxjzCount is null
		select * from #tmp order by chartDate
			
END
go


create procedure dbo.sp_pager @tblname VARCHAR(255), -- 表名
  @strGetFields nvarchar(3000) , -- 需要返回的列
  @fldName varchar(255), -- 排序的字段名
  @PageSize int , -- 页尺寸
  @PageIndex int , -- 页码
  @doCount bit , -- 返回, 非0 值则返回记录总数
  @OrderType bit , -- 设置排序类型, 非0 值则降序
  @strWhere varchar(6000)  -- 查询条件(注意: 不要加where) as
BEGIN	
declare @strSQL varchar(5000) -- 主语句
declare @strOrder varchar(400) -- 排序类型
if @doCount != 0
BEGIN
    set @strSQL = 'select count(*) as Total from ' + @tblName + ' where 1=1 '+ @strWhere
END --以上代码的意思是如果@doCount传递过来的不是，就执行总数统计。以下的所有代码都是@doCount为的情况：
ELSE
BEGIN
   if @OrderType != 0--降序
   begin
    set @strOrder = ' order by ' + @fldName +' desc'--如果@OrderType不是0，就执行降序，这句很重要！
   end
   else
   begin
    set @strOrder = ' order by ' + @fldName +' asc'
   end
   
   set @strSQL = 'select top ' + str(@PageSize*@PageIndex) +' IDENTITY(INT,1,1) AS rowNum,' + @strGetFields + 
   ' into #tmp from ' + @tblName + ' where 1=1 ' + @strWhere + ' ' + @strOrder
	+';select * from #tmp where rowNum>'+str(@PageSize*(@PageIndex-1))
END
PRINT @strSQL
exec (@strSQL)

END
go


create procedure dbo.sp_px_chart @testRoomID VARCHAR(50),
  	@start varchar(30),
  	@end varchar(30),
  	@modelID UNIQUEIDENTIFIER as
BEGIN
	CREATE TABLE #tmp
	(
		chartDate VARCHAR(20),
		zjCount INT,
		pxjzCount INT,	
	)
	
	CREATE TABLE #tmp1
	(
		chartDate VARCHAR(20),
		countnum INT	
	)
	
	CREATE TABLE #tmp2
	(
		chartDate VARCHAR(20),
		countnum INT	
	)
	
	CREATE TABLE #tmp3
	(
		chartDate1 VARCHAR(20),
		chartDate2 VARCHAR(20),
		zjCount INT,
		pxjzCount INT,	
	)
 
	INSERT INTO #tmp1
	( chartDate, countnum )
	SELECT REPLACE(CONVERT(varchar(12) , BGRQ, 111 ) ,'/','-'),COUNT(1) FROM dbo.sys_document 
	WHERE Status>0 AND ModuleID=@modelID AND TestRoomCode=@testRoomID
	AND BGRQ>=@start AND BGRQ<@end AND (TryType='自检' OR TryType='见证')
	GROUP BY REPLACE(CONVERT(varchar(12) , BGRQ, 111 )  ,'/','-')
	
	INSERT INTO #tmp2
	( chartDate, countnum )
	SELECT REPLACE(CONVERT(varchar(12) , c.BGRQ, 111 ) ,'/','-'),COUNT(1) FROM dbo.sys_document a
	JOIN dbo.sys_px_relation b ON a.ID = b.SGDataID
	JOIN dbo.sys_document c ON c.ID = b.PXDataID
	WHERE a.Status>0 AND a.ModuleID=@modelID AND a.TestRoomCode=@testRoomID
	AND a.BGRQ>=@start AND a.BGRQ<@end AND (a.TryType='自检' OR a.TryType='见证')
	AND c.BGRQ>=@start AND c.BGRQ<@end AND c.Status>0
	GROUP BY REPLACE(CONVERT(varchar(12) , c.BGRQ, 111 )  ,'/','-')
		
	INSERT #tmp3
	        ( chartDate1 ,
	          chartDate2 ,
	          zjCount ,
	          pxjzCount
	        )
	SELECT a.chartDate,b.chartDate,a.countnum,b.countnum 
	FROM #tmp1 a FULL JOIN #tmp2 b ON a.chartDate = b.chartDate
	
	UPDATE #tmp3 SET chartDate1=chartDate2 WHERE chartDate1 IS NULL
	
	
	INSERT #tmp
	        ( chartDate, zjCount, pxjzCount )
	SELECT chartDate1,zjCount,pxjzCount FROM  #tmp3

	update #tmp set zjCount=0 where zjCount is null
	update #tmp set pxjzCount=0 where pxjzCount is null
	select * from #tmp order by chartDate
			
END
go


create procedure dbo.sp_pxjzReport @testRoomID VARCHAR(50), --标准实验室id
 	@idList varchar(300), --有权限的实验室id集合
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@start varchar(30),
 	@end varchar(30) as
BEGIN

	DECLARE 
	@modelID varchar(50),--游标中使用
	@zjCount float,
	@pxCount float,
	@jzCount float,
	@sqls nvarchar(4000),
	@id INT,
	@frequency FLOAT,
	@segment VARCHAR(100),
	@company VARCHAR(100),
	@jlcompany VARCHAR(100),
	@testroom VARCHAR(100),
	@sgcompanyID VARCHAR(50)

	CREATE TABLE #tmp
	(
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount INT,
	pxCount INT,
	frequency FLOAT,
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50)
	)

	SELECT @segment=b.Description FROM dbo.sys_engs_Tree a JOIN dbo.sys_engs_SectionInfo b ON a.RalationID=b.ID 
	WHERE a.NodeCode = LEFT(@testRoomID,8)
	
	SELECT @company=b.Description FROM dbo.sys_engs_Tree a JOIN dbo.sys_engs_CompanyInfo b ON a.RalationID=b.ID 
	WHERE a.NodeCode = LEFT(@testRoomID,12)
	
	SELECT @sgcompanyID=b.ID FROM dbo.sys_engs_Tree a 
	JOIN dbo.sys_engs_CompanyInfo b ON a.RalationID=b.ID 
	WHERE a.NodeCode = LEFT(@testRoomID,12)
	
	
	SET @sqls='SELECT top 1 @a=Description FROM dbo.sys_engs_CompanyInfo WHERE ConstructionCompany LIKE ''%' + @sgcompanyID + '%'' '
	EXEC sp_executesql @sqls,N'@a varchar(100) output',@jlcompany OUTPUT
	   
	
	SELECT @testroom=b.Description FROM dbo.sys_engs_Tree a JOIN dbo.sys_engs_ItemInfo b ON a.RalationID=b.ID 
	WHERE a.NodeCode = LEFT(@testRoomID,16)
	
	DECLARE cur CURSOR FOR
	SELECT ModelIndex,ID FROM dbo.sys_biz_reminder_Itemfrequency 
	WHERE TestRoomCode = @testRoomID AND IsActive = 1 AND FrequencyType=@ftype

	OPEN cur
	FETCH NEXT FROM cur INTO @modelID,@id
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
	   SET @sqls='select @a=count(1) from [biz_norm_extent_'+@modelID+'] a 
	   where (a.trytype=''自检'' or a.trytype=''见证'') AND SUBSTRING(a.SCPT,0,17) IN ('+REPLACE(@idList,'|',',')+') AND a.BGRQ>='''+@start+''' AND a.BGRQ<'''+@end+''''
	   EXEC	sp_executesql @sqls,N'@a int output',@zjCount OUTPUT
  PRINT @sqls     
	   
	   SET @sqls='select @a=count(1) from [biz_norm_extent_'+@modelID+'] c where ID in (select b.PXDataID from [biz_norm_extent_'+@modelID+'] a 
	   join dbo.biz_px_relation b on a.ID=b.SGDataID
	   where (a.trytype=''自检'' or a.trytype=''见证'') AND SUBSTRING(a.SCPT,0,17) IN ('+REPLACE(@idList,'|',',')+') AND a.BGRQ>='''+@start+''' AND a.BGRQ<'''+@end+''')
	   and c.BGRQ>='''+@start+''' AND c.BGRQ<'''+@end+''''
	   EXEC	sp_executesql @sqls,N'@a int output',@pxCount OUTPUT
		print @sqls

	   SET @sqls='select @a=count(1) from [biz_norm_extent_'+@modelID+'] a 
	   where trytype=''见证'' AND SUBSTRING(a.SCPT,0,17) IN ('+REPLACE(@idList,'|',',')+') AND a.BGRQ>='''+@start+''' AND a.BGRQ<'''+@end+''''
		EXEC sp_executesql @sqls,N'@a int output',@jzCount OUTPUT
		
		IF @zjCount=0
			SET @frequency = 100
		ELSE
		BEGIN
			IF @ftype=1
			begin
				SET @frequency = ROUND((@pxCount/@zjCount),4)*100
				INSERT INTO #tmp
			   ( zjCount ,
				 condition ,
				 modelName ,
				 result ,
				 frequency ,
				 pxCount,
				 segment,
				jl,
				sg,
				testroom,
				modelID,
				testroomID
			   )
			   SELECT @zjCount,
			   Frequency*100,
			   ModelName,
			   (CASE WHEN @frequency>=Frequency*100 THEN '合格' ELSE '不合格' END ),
			   @frequency,
			   @pxCount,
			   @segment,
			   @jlcompany,
			   @company,
			   @testroom,
			   @modelID,
			   @testRoomID
			   FROM dbo.sys_biz_reminder_Itemfrequency WHERE ID=@id
			end
			ELSE
			begin
				SET @frequency = ROUND((@jzCount/@zjCount),4)*100
				INSERT INTO #tmp
			   ( zjCount ,
				 condition ,
				 modelName ,
				 result ,
				 frequency ,
				 pxCount,
				 segment,
				jl,
				sg,
				testroom,
				modelID,
				testroomID
			   )
			   SELECT @zjCount,
			   Frequency*100,
			   ModelName,
			   (CASE WHEN @frequency>=Frequency*100 THEN '合格' ELSE '不合格' END ),
			   @frequency,
			   @jzCount,
			   @segment,
			   @jlcompany,
			   @company,
			   @testroom,
			   @modelID,
			   @testRoomID
			   FROM dbo.sys_biz_reminder_Itemfrequency WHERE ID=@id
			end
		END
		
	   
	   
	   
	   FETCH NEXT FROM cur INTO @modelID,@id
	END

	CLOSE cur
	DEALLOCATE cur

	
	SELECT zjCount ,
			condition ,
			modelName ,
			result ,
			frequency ,
			pxCount,
			segment,
				jl,
				sg,
				testroom,
				modelID,
				testroomID FROM #tmp 
	WHERE zjCount>0
				order by testroom, modelName
			
END
go


create procedure dbo.sp_pxjz_report @testcode varchar(5000), --为空
  	@ftype TINYINT, -- 为空
  	@startdate varchar(30),--为空
  	@enddate varchar(30),--为空
  	@pageSize int=10,
  	@page        int, 
  	@fldSort    varchar(200) = null,    ----排序字段列表或条件
  	@Sort        bit = 0,    
  	@pageCount    int = 1 output,           
  	@Counts    int = 1 output as
BEGIN

	DECLARE 
	@sqls nvarchar(4000)  

 
		CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount INT,
	pxCount INT,
	frequency NVARCHAR(50),
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty   NVARCHAR(50)
	)

	CREATE TABLE #t1
	(
	ModelName NVARCHAR(50),
	ModuleID NVARCHAR(50),
	TestRoomCode NVARCHAR(50),
	zjcount FLOAT,
	condition FLOAT
	)

		CREATE TABLE #t2
	(
	sgcode NVARCHAR(50),
	jlcode NVARCHAR(50),
	ModuleID NVARCHAR(50),
	pxcount FLOAT
	)
	
	IF @testcode!=''
	BEGIN
  
  			
	

	IF @ftype=1
	 BEGIN
     
 SET @sqls='INSERT #t1
	        ( ModelName ,
	          ModuleID ,
	          TestRoomCode ,
	          zjcount,condition
	        )	SELECT DISTINCT a.ModelName,b.ModuleID,a.TestRoomCode,b.zjcount,a.Frequency*100 AS condition    FROM dbo.sys_biz_reminder_Itemfrequency a JOIN	 
	(SELECT ModuleID,TestRoomCode,COUNT(1) AS zjcount FROM dbo.sys_document WHERE  ( TryType=''见证'' OR TryType = ''自检''  ) AND BGRQ>='''+@startdate+''' AND BGRQ<'''+@enddate+'''  GROUP BY TestRoomCode,ModuleID) AS b 
	ON a.TestRoomCode = b.TestRoomCode AND a.IsActive=1 AND a.FrequencyType=1  AND CAST(a.ModelIndex AS UNIQUEIDENTIFIER)=b.ModuleID AND b.TestRoomCode IN ('+@testcode+') '
  EXEC sp_executesql @sqls   
  
SET @sqls='INSERT #t2
	        ( sgcode, jlcode, ModuleID, pxcount ) SELECT DISTINCT a.TestRoomCode AS sgcode,c.TestRoomCode AS jlcode,c.ModuleID,COUNT(1) pxcount  
 FROM dbo.sys_document a JOIN dbo.sys_px_relation b ON a.ID=b.SGDataID JOIN dbo.sys_document c ON 
b.PXDataID=c.ID AND a.ModuleID=c.ModuleID  AND a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''   AND a.TestRoomCode IN ('+@testcode+')
GROUP BY  c.TestRoomCode,a.TestRoomCode,c.ModuleID'--  AND c.BGRQ>='''+@startdate+''' AND c.BGRQ<'''+@enddate+''' 
  EXEC sp_executesql @sqls 
 
			  SELECT ModelName,a.ModuleID ,sgcode,jlcode,zjcount,pxcount,condition,ROUND(CONVERT(FLOAT,ISNULL(pxCount,0)) / CONVERT(FLOAT,zjCount),4)*100 AS frequency,
 dbo.Fweb_ReturnPXQualityCount_New( a.ModuleID ,sgcode,@startdate,@enddate) AS pxqulifty  INTO #t3 FROM #t1 a   JOIN #t2 b ON   a.TestRoomCode=b.sgcode AND  a.ModuleID = b.ModuleID
		
		INSERT #tmp1
		        ( 
		          modelName ,
		          condition ,
		          zjCount ,
		          pxCount ,
		          frequency ,
		          result ,
		          segment ,
		          jl ,
		          sg ,
		          testroom ,
		          modelID ,
		          testroomID ,
		          pxqulifty
		        ) 
		SELECT b.ModelName,b.condition,b.zjcount,b.pxcount,b.frequency, (CASE WHEN ROUND(CONVERT(FLOAT,ISNULL(pxCount,0)) / CONVERT(FLOAT,zjCount),4)*100>=condition THEN '满足' ELSE '不满足' END ),a.标段名称,c.单位名称,a.单位名称,a.试验室名称,b.ModuleID,b.sgcode,b.pxqulifty FROM dbo.v_bs_codeName a JOIN #t3 b ON a.试验室编码=b.sgcode JOIN dbo.v_bs_codeName c ON b.jlcode=c.试验室编码 
		End	    		 
	 END
  
  IF @ftype=2
  BEGIN
 
 SET @sqls='INSERT #t1
	        ( ModelName ,
	          ModuleID ,
	          TestRoomCode ,
	          zjcount,condition
	        )	SELECT DISTINCT a.ModelName,b.ModuleID,a.TestRoomCode,b.zjcount,a.Frequency*100 AS condition    FROM dbo.sys_biz_reminder_Itemfrequency a JOIN	 
	(SELECT ModuleID,TestRoomCode,COUNT(1) AS zjcount FROM dbo.sys_document WHERE  ( TryType=''见证'' OR TryType = ''自检''  ) AND BGRQ>='''+@startdate+''' AND BGRQ<'''+@enddate+'''  GROUP BY TestRoomCode,ModuleID) AS b 
	ON a.TestRoomCode = b.TestRoomCode AND a.IsActive=1 AND a.FrequencyType=2  AND CAST(a.ModelIndex AS UNIQUEIDENTIFIER)=b.ModuleID AND b.TestRoomCode IN ('+@testcode+') '
  EXEC sp_executesql @sqls   



 CREATE TABLE #t0
 (
 ModuleID NVARCHAR(50),
 sgcode NVARCHAR(50),
 jlcode NVARCHAR(50),
 jzcode FLOAT
 )


SET @sqls=' INSERT #t0
         ( ModuleID,jlcode ,sgcode , jzcode )
 SELECT DISTINCT b.ModuleID,b.TryPersonTestRoomCode,b.TestRoomCode,b.zjcount     FROM dbo.sys_biz_reminder_Itemfrequency a JOIN	 
	(SELECT ModuleID,TryPersonTestRoomCode,TestRoomCode,COUNT(1) AS zjcount FROM dbo.sys_document WHERE    TryType=''见证''     AND BGRQ>='''+@startdate+''' AND BGRQ<'''+@enddate+'''  GROUP BY TryPersonTestRoomCode,TestRoomCode,ModuleID) AS b 
	ON a.TestRoomCode = b.TestRoomCode AND a.IsActive=1 AND a.FrequencyType=2  AND CAST(a.ModelIndex AS UNIQUEIDENTIFIER)=b.ModuleID AND b.TestRoomCode IN ('+@testcode+')'
	  EXEC sp_executesql @sqls 


INSERT #tmp1
        (  
          modelName ,
          condition ,
          zjCount ,
          pxCount ,
          frequency ,
          result ,
          segment ,
          jl ,
          sg ,
          testroom ,
          modelID ,
          testroomID ,
          pxqulifty
        )
		SELECT a.ModelName,a.condition,a.zjcount,b.jzcode,ROUND(CONVERT(FLOAT,ISNULL(jzcode,0)) / CONVERT(FLOAT,zjCount),4)*100 AS frequency,(CASE WHEN ROUND(CONVERT(FLOAT,ISNULL(jzcode,0)) / CONVERT(FLOAT,zjCount),4)*100>=condition THEN '满足' ELSE '不满足' END ),c.标段名称,d.单位名称,c.单位名称,c.试验室名称,a.ModuleID,TestRoomCode,''
		 FROM #t1 a JOIN #t0 b ON a.ModuleID = b.ModuleID AND a.TestRoomCode=b.sgcode  JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码  JOIN dbo.v_bs_codeName d ON b.jlcode=d.试验室编码 
 
  END   
  
    

				   --取得分页总数
  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1
	  declare @pageIndex int --总数/页大小
    declare @lastcount int --总数%页大小  

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 


		SET @Counts=@totalcounts
	SELECT * from #tmp1  	
	 where id>(@pageSize*(@page-1)) and id<=(@pageSize*@page)  


			
END
go


create procedure dbo.sp_report @sDate DATETIME,
  	@eDate DATETIME ,
  	@testRoomCode varchar(6000) as
BEGIN	
	DECLARE	@strSQL varchar(8000) -- 主语句
	CREATE TABLE #tmp (testRoomCode NVARCHAR(64) NOT null)
	insert into #tmp select * from dbo.Fweb_SplitExpression(@testRoomCode,',')

	SELECT g.ID, g.TestName, g.UnitName, g.Frequency,SUM(zj) AS zj, SUM(invalid) AS zjinvalid,
	SUM(px) AS px, SUM(jz) AS jz,SUM(sl) AS sl,SUM(pxInvalid) AS pxInvalid INTO #tmp2 FROM (
	SELECT a.ID, a.TestName, a.UnitName, a.Frequency,b.ModuleID, (CASE WHEN c.ID is NULL THEN 0 ELSE 1 END) AS zj,
	 (CASE WHEN d.ID is NULL THEN 0 ELSE 1 END) AS invalid,
	 (CASE WHEN e.SGDataID is NULL THEN 0 ELSE 1 END) AS px,
	 (CASE WHEN e.pxInvalid is NULL THEN 0 ELSE 1 END) AS pxInvalid,
	 (CASE WHEN f.ID is NULL THEN 0 ELSE 1 END) AS jz,
	 (CASE WHEN c.ShuLiang is NULL THEN 0 ELSE c.ShuLiang END) AS sl FROM dbo.sys_report_config a
	LEFT JOIN dbo.sys_report_config_module b ON a.ID = b.ReportConfigID
	LEFT JOIN (SELECT x.ID,x.ModuleID,x.ShuLiang FROM dbo.sys_document x
	JOIN #tmp y ON x.TestRoomCode = y.testRoomCode
	WHERE x.BGRQ>=@sDate AND x.BGRQ<@eDate AND x.Status>0) as c 
	ON b.ModuleID = c.ModuleID
	LEFT JOIN (SELECT ID FROM dbo.sys_invalid_document WHERE AdditionalQualified=0) as d ON c.ID = d.ID
	LEFT JOIN (SELECT SGDataID,p.ID AS pxInvalid FROM dbo.sys_px_relation m JOIN dbo.sys_document n ON n.ID = m.PXDataID 
	LEFT JOIN dbo.sys_invalid_document p ON n.ID=p.ID AND p.AdditionalQualified=0
	WHERE n.BGRQ>=@sDate AND n.BGRQ<@eDate AND n.Status>0) as e ON c.ID = e.SGDataID
	LEFT JOIN (SELECT ID FROM dbo.sys_document WHERE TryType='见证') as f ON f.ID = c.ID) g
	GROUP BY g.ID, g.TestName, g.UnitName, g.Frequency
	
	SELECT g.ID, g.TestName, g.UnitName, g.Frequency,SUM(zj) AS zj, SUM(invalid) AS zjinvalid,
	SUM(px) AS px, SUM(jz) AS jz,SUM(sl) AS sl,SUM(pxInvalid) AS pxInvalid INTO #tmp3 FROM (
	SELECT a.ID, a.TestName, a.UnitName, a.Frequency,b.ModuleID, (CASE WHEN c.ID is NULL THEN 0 ELSE 1 END) AS zj,
	 (CASE WHEN d.ID is NULL THEN 0 ELSE 1 END) AS invalid,
	 (CASE WHEN e.SGDataID is NULL THEN 0 ELSE 1 END) AS px,
	 (CASE WHEN e.pxInvalid is NULL THEN 0 ELSE 1 END) AS pxInvalid,
	 (CASE WHEN f.ID is NULL THEN 0 ELSE 1 END) AS jz,
	 (CASE WHEN c.ShuLiang is NULL THEN 0 ELSE c.ShuLiang END) AS sl FROM dbo.sys_report_config a
	LEFT JOIN dbo.sys_report_config_module b ON a.ID = b.ReportConfigID
	LEFT JOIN (SELECT x.ID,x.ModuleID,x.ShuLiang FROM dbo.sys_document x
	JOIN #tmp y ON x.TestRoomCode = y.testRoomCode
	WHERE x.Status>0) as c 
	ON b.ModuleID = c.ModuleID
	LEFT JOIN (SELECT ID FROM dbo.sys_invalid_document WHERE AdditionalQualified=0) as d ON c.ID = d.ID
	LEFT JOIN (SELECT SGDataID,p.ID AS pxInvalid FROM dbo.sys_px_relation m JOIN dbo.sys_document n ON n.ID = m.PXDataID 
	LEFT JOIN dbo.sys_invalid_document p ON n.ID=p.ID AND p.AdditionalQualified=0
	WHERE n.Status>0) as e ON c.ID = e.SGDataID
	LEFT JOIN (SELECT ID FROM dbo.sys_document WHERE TryType='见证') as f ON f.ID = c.ID) g
	GROUP BY g.ID, g.TestName, g.UnitName, g.Frequency
	
	SELECT a.ID,a.TestName,a.UnitName,a.Frequency,a.zj,a.jz,a.px,CAST(a.sl AS DECIMAL(10,2)) AS sl,
	CASE WHEN a.zj=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,(a.zj-a.zjinvalid)) / CONVERT(FLOAT,a.zj),4)*100 END AS zjF,
	CASE WHEN a.zj=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,a.jz) / CONVERT(FLOAT,a.zj),4)*100 END AS jzF,
	CASE WHEN a.zj=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,a.px) / CONVERT(FLOAT,a.zj),4)*100 END AS pxF,
	CASE WHEN a.px=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,(a.px-a.pxInvalid)) / CONVERT(FLOAT,a.px),4)*100 END AS pxF2,
	b.zj AS zjTotal,b.jz AS jzTotal,b.px AS pxTotal, CAST(b.sl AS DECIMAL(10,2)) AS slTotal,
	CASE WHEN b.zj=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,(b.zj-b.zjinvalid)) / CONVERT(FLOAT,b.zj),4)*100 END AS zjFTotal,
	CASE WHEN b.zj=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,b.jz) / CONVERT(FLOAT,b.zj),4)*100 END AS jzFTotal,
	CASE WHEN b.zj=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,b.px) / CONVERT(FLOAT,b.zj),4)*100 END AS pxFTotal,
	CASE WHEN b.px=0 THEN 0 ELSE ROUND(CONVERT(FLOAT,(b.px-b.pxInvalid)) / CONVERT(FLOAT,b.px),4)*100 END AS pxF2Total
	INTO #tmp4 FROM #tmp2 a
	JOIN #tmp3 b ON a.ID=b.ID ORDER BY a.ID
	
	SELECT ID ,
	        TestName ,
	        UnitName ,
	        Frequency ,
	        CASE WHEN zj=0 THEN '' ELSE CONVERT(NVARCHAR,zj) END AS zj ,
	        CASE WHEN jz=0 THEN '' ELSE CONVERT(NVARCHAR,jz) END AS jz ,
	        CASE WHEN px=0 THEN '' ELSE CONVERT(NVARCHAR,px) END AS px ,
	        CASE WHEN sl=0.00 THEN '' ELSE CONVERT(NVARCHAR,sl) END AS sl ,
	        CASE WHEN zjF=0 THEN '' ELSE CONVERT(NVARCHAR,zjF)+'%' END AS zjF ,
	        CASE WHEN jzF=0 THEN '' ELSE CONVERT(NVARCHAR,jzF)+'%' END AS jzF ,
	        CASE WHEN pxF=0 THEN '' ELSE CONVERT(NVARCHAR,pxF)+'%' END AS pxF ,
	        CASE WHEN pxF2=0 THEN '' ELSE CONVERT(NVARCHAR,pxF2)+'%' END AS pxF2 ,
	        CASE WHEN zjTotal=0 THEN '' ELSE CONVERT(NVARCHAR,zjTotal) END AS zjTotal ,
	        CASE WHEN jzTotal=0 THEN '' ELSE CONVERT(NVARCHAR,jzTotal) END AS jzTotal ,
	        CASE WHEN pxTotal=0 THEN '' ELSE CONVERT(NVARCHAR,pxTotal) END AS pxTotal ,
	        CASE WHEN slTotal=0.00 THEN '' ELSE CONVERT(NVARCHAR,slTotal) END AS slTotal ,
	        CASE WHEN zjFTotal=0 THEN '' ELSE CONVERT(NVARCHAR,zjFTotal)+'%' END AS zjFTotal ,
	        CASE WHEN jzFTotal=0 THEN '' ELSE CONVERT(NVARCHAR,jzFTotal)+'%' END AS jzFTotal ,
	        CASE WHEN pxFTotal=0 THEN '' ELSE CONVERT(NVARCHAR,pxFTotal)+'%' END AS pxFTotal ,
	        CASE WHEN pxF2Total=0 THEN '' ELSE CONVERT(NVARCHAR,pxF2Total)+'%' END AS pxF2Total 	         
	        FROM #tmp4
	
END
go


create procedure dbo.sp_update as
BEGIN
	set xact_abort on
	BEGIN TRAN
	
	DELETE FROM dbo.sys_biz_ModuleCatlog
	DELETE FROM dbo.sys_biz_SheetCatlog
	DELETE FROM dbo.sys_dictionary
	DELETE FROM dbo.sys_formulas WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_formulas WHERE ID IN (SELECT ID FROM dbo.sys_formulas_update)
	DELETE FROM dbo.sys_module_sheet WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_module_sheet WHERE ID IN (SELECT ID FROM dbo.sys_module_sheet_update)
	DELETE FROM dbo.sys_module_config WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_module_config WHERE ID IN (SELECT ID FROM dbo.sys_module_config_update)
	DELETE FROM dbo.sys_module WHERE ID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_sheet WHERE ID IN (SELECT ID FROM dbo.sys_sheet_update)
	DELETE FROM dbo.sys_stadium_config WHERE ID IN (SELECT ID FROM dbo.sys_stadium_config_update)
	
	INSERT INTO dbo.sys_biz_ModuleCatlog SELECT * FROM dbo.sys_biz_ModuleCatlog_update
	INSERT INTO dbo.sys_biz_SheetCatlog SELECT * FROM sys_biz_SheetCatlog_update
	INSERT INTO dbo.sys_dictionary SELECT * FROM sys_dictionary_update      
	INSERT INTO dbo.sys_module SELECT * FROM sys_module_update  
	INSERT INTO dbo.sys_sheet SELECT * FROM sys_sheet_update
	INSERT INTO dbo.sys_module_sheet SELECT * FROM dbo.sys_module_sheet_update
	INSERT INTO dbo.sys_formulas SELECT * FROM sys_formulas_update
	INSERT INTO dbo.sys_stadium_config SELECT * FROM dbo.sys_stadium_config_update
	INSERT INTO dbo.sys_module_config SELECT * FROM dbo.sys_module_config_update
	
	DELETE FROM dbo.sys_biz_ModuleCatlog_update
	DELETE FROM dbo.sys_biz_SheetCatlog_update
	DELETE FROM dbo.sys_dictionary_update
	DELETE FROM dbo.sys_formulas_update
	DELETE FROM dbo.sys_module_sheet_update
	DELETE FROM dbo.sys_module_update
	DELETE FROM dbo.sys_sheet_update
	DELETE FROM dbo.sys_stadium_config_update
	DELETE FROM dbo.sys_module_config_update
	
	IF @@error<>0
	BEGIN
		rollback tran 
		return 0
	END 
	ELSE
	BEGIN 
		commit tran
		return 1
	END
END
go


create procedure dbo.sp_update_module as
BEGIN
	set xact_abort on
	BEGIN TRAN
	--alter table [dbo].[sys_module_sheet]
	--   drop constraint [FK_sys_module_sheet_sys_module]
	--GO
	--alter table [dbo].[sys_formulas]
	--   drop constraint [FK_sys_formulas_sys_module]
	--GO
	
	DELETE FROM dbo.sys_dictionary
	DELETE FROM dbo.sys_formulas WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_formulas WHERE ID IN (SELECT ID FROM dbo.sys_formulas_update)
	DELETE FROM dbo.sys_module_config WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_module_config WHERE ID IN (SELECT ID FROM dbo.sys_module_config_update)
	DELETE FROM dbo.sys_module WHERE ID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_stadium_config WHERE ID IN (SELECT ID FROM dbo.sys_stadium_config_update)
	
	INSERT INTO dbo.sys_dictionary SELECT * FROM sys_dictionary_update      
	INSERT INTO dbo.sys_module SELECT * FROM sys_module_update  
	INSERT INTO dbo.sys_formulas SELECT * FROM sys_formulas_update
	INSERT INTO dbo.sys_stadium_config SELECT * FROM dbo.sys_stadium_config_update
	INSERT INTO dbo.sys_module_config SELECT * FROM dbo.sys_module_config_update
	
	DELETE FROM dbo.sys_dictionary_update
	DELETE FROM dbo.sys_formulas_update
	DELETE FROM dbo.sys_module_update
	DELETE FROM dbo.sys_stadium_config_update
	DELETE FROM dbo.sys_module_config_update
	--删除多余数据
	--DELETE FROM dbo.sys_module_sheet WHERE ModuleID NOT IN(SELECT ID FROM dbo.sys_module)
	--DELETE FROM dbo.sys_formulas WHERE ModuleID NOT IN(SELECT ID FROM dbo.sys_module)
	--添加约束
	--ALTER TABLE [dbo].[sys_module_sheet]  WITH CHECK ADD  CONSTRAINT [FK_sys_module_sheet_sys_module] FOREIGN KEY([ModuleID])
	--REFERENCES [dbo].[sys_module] ([ID])
	--GO
	--ALTER TABLE [dbo].[sys_module_sheet] CHECK CONSTRAINT [FK_sys_module_sheet_sys_module]
	--GO
	--ALTER TABLE [dbo].[sys_formulas]  WITH NOCHECK ADD  CONSTRAINT [FK_sys_formulas_sys_module] FOREIGN KEY([ModuleID])
	--REFERENCES [dbo].[sys_module] ([ID])
	--GO
	--ALTER TABLE [dbo].[sys_formulas] CHECK CONSTRAINT [FK_sys_formulas_sys_module]
	--GO
	
	IF @@error<>0
	BEGIN
		rollback tran 
		return 0
	END 
	ELSE
	BEGIN 
		commit tran
		return 1
	END
END
go


create procedure dbo.sp_update_module_sheet as
BEGIN
	set xact_abort on
	BEGIN TRAN
	--alter table [dbo].[sys_module_sheet]
	--   drop constraint [FK_sys_module_sheet_sys_module]
	--GO
	--alter table [dbo].[sys_formulas]
	--   drop constraint [FK_sys_formulas_sys_module]
	--GO
	DELETE FROM dbo.sys_dictionary
	DELETE FROM dbo.sys_formulas WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_formulas WHERE ID IN (SELECT ID FROM dbo.sys_formulas_update)
	DELETE FROM dbo.sys_module_sheet WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_module_sheet WHERE ID IN (SELECT ID FROM dbo.sys_module_sheet_update)
	DELETE FROM dbo.sys_module_config WHERE ModuleID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_module_config WHERE ID IN (SELECT ID FROM dbo.sys_module_config_update)
	DELETE FROM dbo.sys_module WHERE ID IN (SELECT ID FROM dbo.sys_module_update)
	DELETE FROM dbo.sys_stadium_config WHERE ID IN (SELECT ID FROM dbo.sys_stadium_config_update)
	
	INSERT INTO dbo.sys_dictionary SELECT * FROM sys_dictionary_update      
	INSERT INTO dbo.sys_module SELECT * FROM sys_module_update  
	INSERT INTO dbo.sys_module_sheet SELECT * FROM dbo.sys_module_sheet_update
	INSERT INTO dbo.sys_formulas SELECT * FROM sys_formulas_update
	INSERT INTO dbo.sys_stadium_config SELECT * FROM dbo.sys_stadium_config_update
	INSERT INTO dbo.sys_module_config SELECT * FROM dbo.sys_module_config_update
	
	DELETE FROM dbo.sys_dictionary_update
	DELETE FROM dbo.sys_formulas_update
	DELETE FROM dbo.sys_module_sheet_update
	DELETE FROM dbo.sys_module_update
	DELETE FROM dbo.sys_stadium_config_update
	DELETE FROM dbo.sys_module_config_update
	--删除多余数据
	--DELETE FROM dbo.sys_module_sheet WHERE ModuleID NOT IN(SELECT ID FROM dbo.sys_module)
	--DELETE FROM dbo.sys_formulas WHERE ModuleID NOT IN(SELECT ID FROM dbo.sys_module)
	--添加约束
	--ALTER TABLE [dbo].[sys_module_sheet]  WITH CHECK ADD  CONSTRAINT [FK_sys_module_sheet_sys_module] FOREIGN KEY([ModuleID])
	--REFERENCES [dbo].[sys_module] ([ID])
	--GO
	--ALTER TABLE [dbo].[sys_module_sheet] CHECK CONSTRAINT [FK_sys_module_sheet_sys_module]
	--GO
	--ALTER TABLE [dbo].[sys_formulas]  WITH NOCHECK ADD  CONSTRAINT [FK_sys_formulas_sys_module] FOREIGN KEY([ModuleID])
	--REFERENCES [dbo].[sys_module] ([ID])
	--GO
	--ALTER TABLE [dbo].[sys_formulas] CHECK CONSTRAINT [FK_sys_formulas_sys_module]
	--GO


	
	IF @@error<>0
	BEGIN
		rollback tran 
		return 0
	END 
	ELSE
	BEGIN 
		commit tran
		return 1
	END
END
go


create procedure dbo.sp_update_sheet as
BEGIN
	set xact_abort on
	BEGIN TRAN
	
	DELETE FROM dbo.sys_sheet WHERE ID IN (SELECT ID FROM dbo.sys_sheet_update)
	INSERT INTO dbo.sys_sheet SELECT * FROM sys_sheet_update
	DELETE FROM dbo.sys_sheet_update
	
	IF @@error<>0
	BEGIN
		rollback tran 
		return 0
	END 
	ELSE
	BEGIN 
		commit tran
		return 1
	END
END
go


create procedure dbo.spweb_CreateMatchine as
BEGIN
 
 DELETE  dbo.biz_machinelist

 DECLARE @sqls nvarchar(4000)

 declare @i int
set @i=6
while @i<32
begin

SET @sqls='SELECT 
SCTS,
SCPT,
SCCT,
SCCS,
[col_norm_A'+CAST(@i AS VARCHAR(10)) +'],
[col_norm_B'+CAST(@i AS VARCHAR(10))+'],
[col_norm_C'+CAST(@i AS VARCHAR(10))+'],
[col_norm_D'+CAST(@i AS VARCHAR(10))+'],
[col_norm_E'+CAST(@i AS VARCHAR(10))+'],
[col_norm_F'+CAST(@i AS VARCHAR(10))+'],
[col_norm_G'+CAST(@i AS VARCHAR(10))+'],
[col_norm_H'+CAST(@i AS VARCHAR(10))+'],
[col_norm_I'+CAST(@i AS VARCHAR(10))+'],
[col_norm_J'+CAST(@i AS VARCHAR(10))+'],
[col_norm_K'+CAST(@i AS VARCHAR(10))+'],
[col_norm_L'+CAST(@i AS VARCHAR(10))+'],
[col_norm_M'+CAST(@i AS VARCHAR(10))+'],
[col_norm_N'+CAST(@i AS VARCHAR(10))+'],
[col_norm_O'+CAST(@i AS VARCHAR(10))+'],
[col_norm_P'+CAST(@i AS VARCHAR(10))+'],
[col_norm_Q'+CAST(@i AS VARCHAR(10))+'] 
FROM [biz_norm_试验室仪器设备汇总表]'



INSERT dbo.biz_machinelist
        ( 
          SCTS ,
          SCPT ,
          SCCT ,
          SCCS ,
          col_norm_A6 ,
          col_norm_B6 ,
          col_norm_C6 ,
          col_norm_D6 ,
          col_norm_E6 ,
          col_norm_F6 ,
          col_norm_G6 ,
          col_norm_H6 ,
          col_norm_I6 ,
          col_norm_J6 ,
          col_norm_K6 ,
          col_norm_L6 ,
          col_norm_M6 ,
          col_norm_N6 ,
          col_norm_O6 ,
          col_norm_P6 ,
          col_norm_Q6
        )
		EXEC sp_executesql @sqls 



set @i=@i+1
end

DELETE dbo.biz_machinelist WHERE col_norm_B6 IS NULL AND col_norm_C6 IS NULL AND col_norm_D6 IS NULL
 

 DELETE dbo.biz_machinelist WHERE  col_norm_A6=0

END
go


create procedure dbo.spweb_Createtree as
BEGIN
 
 DELETE dbo.Sys_Tree 



 INSERT dbo.Sys_Tree
         ( NodeCode ,
           DESCRIPTION ,
           DepType ,
           OrderID
         )
	SELECT a.NodeCode,a.Description,b.NodeType,a.NodeCode FROM dbo.v_codeName a JOIN sys_engs_Tree b ON a.RalationID=b.RalationID  AND  Scdel=0
 
END
go


create procedure dbo.spweb_PX_Count_Summary as
BEGIN

 TRUNCATE TABLE dbo.biz_px_relation_web

 INSERT dbo.biz_px_relation_web
         ( 
           SGDataID ,
           PXDataID ,
           SGTestRoomCode ,
           PXTestRoomCode ,
           PXTime 
        
         )
SELECT SGDataID ,
           PXDataID ,
           SGTestRoomCode ,
           PXTestRoomCode ,
           PXTime  FROM dbo.biz_px_relation

 DECLARE 
	@modelID varchar(50),--游标中使用
	@sqls nvarchar(4000)	
 
	DECLARE cur CURSOR FOR
  
  	SELECT DISTINCT ModelIndex FROM  sys_biz_reminder_Itemfrequency WHERE IsActive=1
  

	OPEN cur
	FETCH NEXT FROM cur INTO @modelID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	 

		  
	 
	 SET @sqls='UPDATE dbo.biz_px_relation_web SET PXBGRQ=b.BGRQ,ModelIndex='''+@modelID+''' FROM dbo.biz_px_relation_web a JOIN [biz_norm_extent_'+@modelID+'] b 
	 ON a.PXDataID =b.ID   '
	 EXEC sp_executesql @sqls
   
   SET @sqls='UPDATE dbo.biz_px_relation_web SET SGBGRQ=b.BGRQ,ModelIndex='''+@modelID+''' FROM dbo.biz_px_relation_web a JOIN [biz_norm_extent_'+@modelID+'] b 
	 ON a.SGDataID =b.ID   '
	 EXEC sp_executesql @sqls
   
	   
	   FETCH NEXT FROM cur INTO  @modelID
	END

	CLOSE cur
	DEALLOCATE cur	


			
END
go


create procedure dbo.spweb_PX_ZJ_Summary as
BEGIN

 

	
 
 DECLARE 
	@modelID varchar(50),--游标中使用
	@testroomid VARCHAR(50),
	@sqls nvarchar(4000)	
 


CREATE TABLE #t1
        ( BGRQ DATETIME NULL,
          ModelIndex VARCHAR(50) ,
          TestRoomCode VARCHAR(50),
          ZjCount INT
        )
		 
		 CREATE TABLE #t2
        ( BGRQ DATETIME NULL,
          ModelIndex VARCHAR(50) ,
          TestRoomCode VARCHAR(50),
          JzCount INT
        )


 
	DECLARE cur CURSOR FOR



SELECT DISTINCT ModelIndex FROM  sys_biz_reminder_Itemfrequency WHERE IsActive=1

	OPEN cur
	FETCH NEXT FROM cur INTO @modelID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	 
  
    SET @sqls='INSERT #t1
		        ( BGRQ ,
		          ModelIndex ,
		          TestRoomCode ,
		          ZjCount
		        ) select a.BGRQ,'''+@modelID+''',LEFT(a.SCPT,16),count(1) from [biz_norm_extent_'+@modelID+'] a 
	   where (a.trytype=''自检'' or a.trytype=''见证'') and a.BGRQ is not null GROUP BY a.BGRQ, LEFT(a.SCPT,16) '
	   EXEC	sp_executesql @sqls 


	   SET @sqls='INSERT #t2
		        ( BGRQ ,
		          ModelIndex ,
		          TestRoomCode ,
		          JzCount
		        ) select a.BGRQ,'''+@modelID+''',LEFT(a.SCPT,16),count(1) from [biz_norm_extent_'+@modelID+'] a 
	   where trytype=''见证'' and a.BGRQ is not null  GROUP BY a.BGRQ, LEFT(a.SCPT,16) '
		EXEC sp_executesql @sqls 
		
		
		  
	 
	   
	   FETCH NEXT FROM cur INTO  @modelID
	END

	CLOSE cur
	DEALLOCATE cur	


	--SELECT COUNT(*) FROM #t1

	--SELECT COUNT(*) FROM #t2



SELECT a.*, b.BGRQ AS bgrq1, b.ModelIndex AS modelindex1, b.TestRoomCode AS
testroomcode1, b.JzCount INTO #t3 FROM #t1 a
FULL JOIN #t2 b ON a.BGRQ = b.BGRQ AND 
a.ModelIndex = b.ModelIndex AND 
a.TestRoomCode = b.TestRoomCode

 

UPDATE #t3 SET BGRQ=bgrq1,ModelIndex=modelindex1,TestRoomCode=testroomcode1,ZjCount=0
WHERE BGRQ IS NULL

UPDATE #t3 SET JzCount=0 WHERE JzCount IS NULL

TRUNCATE TABLE dbo.biz_ZJ_JZ_Summary

INSERT dbo.biz_ZJ_JZ_Summary
        ( BGRQ ,
          ModelIndex ,
          TestRoomCode ,
          ZjCount ,
          JzCount,
		  JLCompanyName,
		  JLCompanyCode,
		  ModelName
        )
SELECT 
 a.BGRQ ,
   a.ModelIndex ,
        a.TestRoomCode ,           
		a.ZjCount ,
        a.JzCount,c.Description,d.NodeCode,e.Description FROM #t3 a JOIN dbo.v_codeName b ON LEFT(a.TestRoomCode,12)=b.NodeCode
		JOIN  dbo.sys_engs_CompanyInfo c
		ON CHARINDEX(b.RalationID,c.ConstructionCompany)>0
		JOIN dbo.sys_engs_Tree d
		ON c.ID=d.RalationID
		JOIN dbo.sys_biz_Module e 
		ON a.ModelIndex=e.ID

--DELETE biz_ZJ_JZ_Summary WHERE JLCompanyCode IN ('000100200001','000100090001')


END
go


create procedure dbo.spweb_ageremind @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN
 
 DECLARE @sqls nvarchar(4000)	,
 @modelID VARCHAR(50),
 @searchRange VARCHAR(50) ,
 @n VARCHAR(50)
    

			CREATE TABLE #t2
			(		
            DataID VARCHAR(5000),
            ModelCode VARCHAR(5000),
            ModelIndex VARCHAR(5000),
            DateSpan VARCHAR(5000),  
            F_Name  VARCHAR(5000),
            F_PH  VARCHAR(5000),
            F_ZJRQ  DATETIME,
            F_SJBH  VARCHAR(5000),
            F_SJSize  VARCHAR(5000),
            F_SYXM  VARCHAR(5000),
            F_BGBH  VARCHAR(5000),
            F_WTBH  VARCHAR(5000)
			)
     
	
		DECLARE cur CURSOR FOR
 	SELECT DISTINCT ID,SearchRange FROM dbo.sys_biz_reminder_stadiumInfo WHERE IsActive=1  AND ID!='05d0d71b-def3-42ee-a16a-79b34de97e9b'

		OPEN cur
		FETCH NEXT FROM cur INTO @modelID,@searchRange
		WHILE @@FETCH_STATUS = 0
	BEGIN


 

	 SELECT @n= MAX(Value) FROM Fweb_SplitExpression(@searchRange,',')

			SET @sqls='
				INSERT #t2
	        ( DataID ,
	          ModelCode ,
	          ModelIndex ,
	          DateSpan ,
	          F_Name ,
	          F_PH ,
	          F_ZJRQ ,
	          F_SJBH ,
	          F_SJSize ,
	          F_SYXM ,
	          F_BGBH ,
	          F_WTBH
	        )SELECT 
            a.DataID,
            a.ModelCode,
            a.ModelIndex,
            a.DateSpan,         
            a.F_Name as 名称,
            a.F_PH as 批号,
            a.F_ZJRQ as 制件日期,
            a.F_SJBH as 试件编号,
            a.F_SJSize as 试件尺寸,
            a.F_SYXM as 试验项目,
            a.F_BGBH as 报告编号,
            a.F_WTBH as 委托编号
            FROM dbo.sys_biz_reminder_stadiumData a           
            WHERE a.F_IsDone IS NULL  AND a.ModelIndex='''+@modelID+''' AND DATEDIFF(day, DATEADD(DAY, a.DateSpan, a.F_ZJRQ), GETDATE()) >'''+@n+''' and 
			
			DATEADD(DAY, a.DateSpan, a.F_ZJRQ)>='''+@startdate+'''  and DATEADD(DAY, a.DateSpan, a.F_ZJRQ)<'''+@enddate+'''  '
            

			PRINT @sqls
		  EXEC sp_executesql @sqls
  
  	   FETCH NEXT FROM cur INTO  @modelID,@searchRange
	END

	CLOSE cur
	DEALLOCATE cur	  

	CREATE TABLE #t3
			(	
			id bigint identity(1,1)  primary key,	
            DataID VARCHAR(5000),
            ModelCode VARCHAR(5000),
            ModelIndex VARCHAR(5000),
            DateSpan VARCHAR(5000),  
            F_Name  VARCHAR(5000),
            F_PH  VARCHAR(5000),
            F_ZJRQ  DATETIME,
            F_SJBH  VARCHAR(5000),
            F_SJSize  VARCHAR(5000),
            F_SYXM  VARCHAR(5000),
            F_BGBH  VARCHAR(5000),
            F_WTBH  VARCHAR(5000)
			)	

		IF @testcode!=''
		BEGIN
		
									SET @sqls='INSERT #t3
											( 
											  DataID ,
											  ModelCode ,
											  ModelIndex ,
											  DateSpan ,
											  F_Name ,
											  F_PH ,
											  F_ZJRQ ,
											  F_SJBH ,
											  F_SJSize ,
											  F_SYXM ,
											  F_BGBH ,
											  F_WTBH
											)
								SELECT DataID ,
									  ModelCode ,
									  ModelIndex ,
									  DateSpan ,
									  F_Name ,
									  F_PH ,
									  F_ZJRQ ,
									  F_SJBH ,
									  F_SJSize ,
									  F_SYXM ,
									  F_BGBH ,
									  F_WTBH FROM #t2 WHERE LEFT(ModelCode,16) IN ('+@testcode+') ORDER BY F_ZJRQ DESC'
		 END      

		   EXEC sp_executesql @sqls

		 declare @totalcounts int
  select @totalcounts=count(ID) from #t3

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

	SET @Counts=@totalcounts

	select DataID ,
		   ModelCode ,
		   ModelIndex ,
		   DateSpan ,
		   F_Name ,
		   F_PH ,
		   F_ZJRQ ,
		   F_SJBH ,
		   F_SJSize ,
		   F_SYXM ,
		   F_BGBH ,
		   F_WTBH, 
		   标段名称 AS segment,
		   单位名称 AS company,
		   试验室名称 AS testroom
		    from #t3 a JOIN dbo.v_bs_codeName b ON LEFT(a.ModelCode,16)=b.试验室编码 
			 JOIN dbo.Sys_Tree c ON  LEFT(a.ModelCode,16)=c.NodeCode
		 
			 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  ORDER BY OrderID
 
			 
END
go


create procedure dbo.spweb_check_table as
BEGIN
 
 DECLARE @sqls nvarchar(4000)	,
 @modelID VARCHAR(50),
 @modelname VARCHAR(50)
 
	 CREATE TABLE #t1
	 (
	 modelID VARCHAR(50),
	 modename VARCHAR(50)
	
	 )

 

						SET @sqls='INSERT #t1 ( modelID,modename )  SELECT ID,Description FROM sys_biz_module'
					 EXEC	sp_executesql @sqls
			
  if object_id(N'check_table',N'U') is not null
  BEGIN
	
DELETE dbo.check_table
  END
else 
 BEGIN
	CREATE TABLE [dbo].[check_table](
	[modexindex] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[modelname] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[errornumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[errorseverity] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[errorstate] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[errormessage] [varchar](5000) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]
 END
    
     
	
		DECLARE cur CURSOR FOR
 		SELECT * FROM #t1

		OPEN cur
		FETCH NEXT FROM cur INTO @modelID,@modelname
		WHILE @@FETCH_STATUS = 0
	BEGIN

				SET @sqls='	 select DataName,TryType,IsQualified,BGRQ,DeviceType from [biz_norm_extent_'+@modelID+']  ' 
				
            
			
	 
							  BEGIN TRY
					   EXEC sp_executesql @sqls 
					 END TRY
					 BEGIN CATCH
					   DECLARE @errornumber INT
					   DECLARE @errorseverity INT
					   DECLARE @errorstate INT
					   DECLARE @errormessage NVARCHAR(4000)
					   SELECT  @errornumber = ERROR_NUMBER() ,
					   @errorseverity = ERROR_SEVERITY() ,
					   @errorstate = ERROR_STATE() ,
					   @errormessage = ERROR_MESSAGE()
					  
					  INSERT dbo.check_table
					          ( modexindex ,
					          modelname,
					            errornumber ,
					            errorseverity ,
					            errorstate ,
					            errormessage
					          )
					 SELECT
					   @modelID, 
					   @modelname,
						@errornumber ,
					   @errorseverity ,
					   @errorstate ,
					   @errormessage
					   RAISERROR (
					   @errormessage, -- Message text,
					   @errorseverity,  -- Severity,
					   @errorstate,  -- State,
					   @errornumber
					   )
					  
					 END CATCH




  
  	   FETCH NEXT FROM cur INTO  @modelID,@modelname
	END

	CLOSE cur
	DEALLOCATE cur	  

	
	

			
END
go


create procedure dbo.spweb_evaluatedata @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

DECLARE @sqls nvarchar(4000),
 @modelID VARCHAR(50),
 @testroomid VARCHAR(50)
 
 	 CREATE TABLE #t1
	 (
	 modelID VARCHAR(50),
	 NodeCode VARCHAR(50)
	 )

IF @testcode!='' 
				BEGIN
						SET @sqls='INSERT #t1 ( modelID, NodeCode ) SELECT  DISTINCT b.ID AS modelID,LEFT(a.NodeCode,16) NodeCode   FROM sys_engs_Tree  a JOIN sys_biz_module b ON a.RalationID=b.ID  AND LEFT(a.NodeCode,16) IN ('+@testcode+')  ORDER BY LEFT(a.NodeCode,16)'
					 EXEC	sp_executesql @sqls
				END
  
  
	 CREATE TABLE #tmp
 (
 id bigint identity(1,1)  primary key,
 IndexID  nvarchar(50)				  ,
ModelCode  nvarchar(50)		  ,
ModelIndex  nvarchar(50)		  ,
ReportName  nvarchar(100)		  ,
ReportNumber  nvarchar(100)	  ,
ReportDate DATETIME NULL			  ,
F_InvalidItem  nvarchar(1500)	  ,
SGComment  nvarchar(1000)		  ,
JLComment  nvarchar(1000)		  
 )              
  
  	DECLARE cur CURSOR FOR
 		SELECT * FROM #t1

		OPEN cur
		FETCH NEXT FROM cur INTO @modelID,@testroomid
		WHILE @@FETCH_STATUS = 0
	BEGIN
	

	
	 
			
	

		SET @sqls='  SELECT  a.ID ,	       
	         a.ModelCode ,
	         a.ModelIndex ,
	         a.ReportName ,
	         a.ReportNumber ,
	         a.ReportDate ,
	         a.F_InvalidItem ,
	         a.SGComment ,	         
	         a.JLComment 	  FROM  dbo.sys_biz_reminder_evaluateData a JOIN dbo.[biz_norm_extent_'+@modelID+'] b ON a.ID = b.ID and LEFT(b.SCPT,16)='''+@testroomid+''' AND a.AdditionalQualified=0 and   a.F_InvalidItem NOT LIKE ''%#%'' and       a.ReportDate>='''+@startdate+''' AND a.ReportDate<'''+@enddate+'''' 
					

					INSERT  #tmp
					        (
					          IndexID ,
					          ModelCode ,
					          ModelIndex ,
					          ReportName ,
					          ReportNumber ,
					          ReportDate ,
					          F_InvalidItem ,
					          SGComment ,
					          JLComment
					        )
					EXEC sp_executesql @sqls 

	
	 
  	   FETCH NEXT FROM cur INTO  @modelID,@testroomid
	END

	CLOSE cur
	DEALLOCATE cur	                

			UPDATE #tmp SET ReportDate=NULL WHERE ReportDate<'2013-01-01'	 

  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  

	SET @Counts=@totalcounts

	select  a.id ,
          a.IndexID ,
          a.ModelCode ,
          a.ModelIndex ,
          a.ReportName ,
          a.ReportNumber ,
          a.ReportDate ,
          a.F_InvalidItem ,
          a.SGComment ,
          a.JLComment,
		b.标段名称 AS   SectionName ,
         b.标段编码 AS   SectionCode ,
         b.单位名称 AS   CompanyName ,
         b.单位编码 AS   CompanyCode ,
         b.试验室名称 AS   TestRoomName ,
          b.试验室编码 AS  TestRoomCode
		   from #tmp a JOIN dbo.v_bs_codeName b
		    ON LEFT(a.ModelCode,16)=b.试验室编码 
			
			JOIN dbo.Sys_Tree c ON b.试验室编码=c.NodeCode 
			where a.id>(@pageSize*(@page-1)) and a.id<=(@pageSize*(@page))  ORDER BY c.OrderID
 
 
		 
			
 

END
go


create procedure dbo.spweb_evaluatedata_chart @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN
 
 DECLARE @sqls nvarchar(4000)	 

  CREATE  TABLE #t1
 (
 segment NVARCHAR(50),
  companyname NVARCHAR(50),
   ncount INT,
    companycode NVARCHAR(50)
 )

 SET @sqls='  INSERT #t1
	          ( segment ,
	            companyname ,
	            ncount ,
	            companycode
	          )
	   SELECT MAX( b.标段名称),MAX(b.单位名称), COUNT(1)  ncount ,LEFT(failtestcode,12)     FROM dbo.biz_fail a JOIN   dbo.v_bs_codeName b ON a.failtestcode=b.试验室编码    WHERE  failtestcode IN ('+@testcode+') AND FaliBGRQ>='''+@startdate+'''  AND FaliBGRQ<'''+@enddate+'''    GROUP BY  LEFT(failtestcode,12)  ORDER BY ncount DESC
'

 EXEC sp_executesql @sqls 
 
	 
	    
  SELECT segment,companyname,companycode,ncount FROM #t1  a JOIN dbo.Sys_Tree b ON a.companycode=b.NodeCode AND LEN(b.NodeCode)=12 ORDER BY b.OrderID
 
			
END
go


create procedure dbo.spweb_evaluatedata_chart_pop @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT as
BEGIN

		CREATE TABLE #tmp
		(
		id bigint identity(1,1)  primary key,
		 试验室名称  nvarchar(50),
		 试验室编码 NVARCHAR(50),
		 ncount INT
		 
		) 
		DECLARE 
	@code VARCHAR(50),
	@companyname VARCHAR(50),
	@num INT
 


		DECLARE cur CURSOR FOR	
SELECT 试验室编码,试验室名称 FROM dbo.v_bs_codeName WHERE 单位编码=@testcode

	OPEN cur
	FETCH NEXT FROM cur INTO @code,@companyname
	WHILE @@FETCH_STATUS = 0
	BEGIN		


		SELECT @num=COUNT(*) FROM  dbo.biz_fail WHERE   failtestcode=@code AND FaliBGRQ>@startdate AND FaliBGRQ<@enddate
	
	IF	@num=0
	BEGIN
  INSERT #tmp
        (  试验室名称, 试验室编码, ncount )
VALUES(@companyname,@testcode,0) 
  END  
	ELSE
  BEGIN  
INSERT #tmp
        (  试验室名称, 试验室编码, ncount )
		SELECT  MAX(试验室名称)  ,MAX(failtestcode) , COUNT(*) FROM  dbo.biz_fail  a JOIN dbo.v_bs_codeName b ON a.failtestcode=b.试验室编码 AND 
a.failtestcode=@code AND  FaliBGRQ>@startdate AND FaliBGRQ<@enddate  GROUP BY failtestcode

END
	
	  FETCH NEXT FROM cur INTO @code,@companyname
	END
	CLOSE cur
	DEALLOCATE cur


	SELECT 试验室名称 AS testname, 试验室编码 AS companycode, ncount FROM  #tmp

    

END
go


create procedure dbo.spweb_exec_qxzlhzb_charts as
BEGIN
 
 DECLARE @sqls nvarchar(4000)	,
 @n INT,
 @modelID VARCHAR(50),
 @testroomid VARCHAR(50),
				@m INT
 
	 CREATE TABLE #t1
	 (
	 modelID VARCHAR(50),
	 NodeCode VARCHAR(50)
	 )


						SET @sqls='INSERT #t1 ( modelID, NodeCode ) SELECT  DISTINCT b.ID AS modelID,LEFT(a.NodeCode,16) NodeCode   FROM sys_engs_Tree  a JOIN sys_biz_module b ON a.RalationID=b.ID where b.ID NOT IN (''08899ba2-cc88-403e-9182-3ef73f5fb0ce'',''ba23c25d-7c79-4cb3-a0dc-acfa6c285295'',''e77624e9-5654-4185-9a29-8229aafdd68b'')    ORDER BY LEFT(a.NodeCode,16)'
					 EXEC	sp_executesql @sqls
			
  
   CREATE TABLE #t2
   (
   testcode VARCHAR(50),
   modelindex VARCHAR(50),
   ToatlSCTS NVARCHAR(50),
  ToatlBaRQ NVARCHAR(50)   
   )  
   
      CREATE TABLE #t3
   (
   testcode VARCHAR(50),
   modelindex VARCHAR(50),
   FaliSCTS NVARCHAR(50),
  FaliBGRQ NVARCHAR(50)   
   ) 
    
     
	
		DECLARE cur CURSOR FOR
 		SELECT * FROM #t1

		OPEN cur
		FETCH NEXT FROM cur INTO @modelID,@testroomid
		WHILE @@FETCH_STATUS = 0
	BEGIN

				SET @sqls='   INSERT #t2
           ( testcode ,
             modelindex ,
             ToatlSCTS 
           )select '''+@testroomid+''','''+@modelID+''',CONVERT(varchar, SCTS, 120 ) from [biz_norm_extent_'+@modelID+'] a   where  LEFT(SCPT,16)='''+@testroomid+'''  ' 
				EXEC sp_executesql @sqls 
            
			
			
				SET @sqls='INSERT #t3
	        ( testcode ,
	          modelindex ,
	          FaliSCTS ,
	          FaliBGRQ
	        )  SELECT '''+@testroomid+''','''+@modelID+''',   CONVERT(varchar, a.SCTS, 120 ) ,a.ReportDate FROM  dbo.sys_biz_reminder_evaluateData a JOIN dbo.[biz_norm_extent_'+@modelID+'] b ON a.ID = b.ID and LEFT(b.SCPT,16)='''+@testroomid+''' AND a.AdditionalQualified=0 and   a.F_InvalidItem NOT LIKE ''%#%'' ' 
					EXEC sp_executesql @sqls 
				          
		  

  
  	   FETCH NEXT FROM cur INTO  @modelID,@testroomid
	END

	CLOSE cur
	DEALLOCATE cur	  

	TRUNCATE TABLE  dbo.biz_qxzl
		
	 INSERT dbo.biz_qxzl
	         ( testcode ,
	           modelindex ,
	           ToatlSCTS ,
	           ToatlBaRQ ,	          
	           company ,
	           companycode ,
	           segment
	         )
			  SELECT a.testcode,a.modelindex,a.ToatlSCTS,a.ToatlBaRQ,c.单位名称,c.单位编码,c.标段名称 FROM #t2 a   
			  JOIN dbo.v_bs_codeName c ON a.testcode=c.试验室编码

	

 
		TRUNCATE TABLE  dbo.biz_fail

		INSERT dbo.biz_fail
		        ( failtestcode ,
		          failmodelindex ,
		          FaliSCTS ,
		          FaliBGRQ
		        )
	 SELECT testcode,modelindex,FaliSCTS,FaliBGRQ FROM #t3


	

			
END
go


create procedure dbo.spweb_failreport @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN
 
 
    DECLARE @sqls nvarchar(4000)	
 
  create table #tmp2
 (
id bigint identity(1,1)  primary key,
project varchar(50),
segment varchar(50),
company varchar(50),
testroom varchar(50),
ncount INT,
testcode VARCHAR(50) )
 

   create table #tmp1
 (
id bigint identity(1,1)  primary key,
project varchar(50),
segment varchar(50),
company varchar(50),
testroom varchar(50),
ncount INT,
testcode VARCHAR(50)
 )


 

 		   create table #tmp5
 (
id bigint identity(1,1)  primary key,
project varchar(50),
segment varchar(50),
company varchar(50),
testroom varchar(50),
testcode VARCHAR(50),
totalncount FLOAT,
counts FLOAT,
prenct FLOAT 
 )

  IF @testcode!=''
  BEGIN
  
  SET @sqls='SELECT  MAX(c.工程名称) AS project,MAX(c.标段名称) AS segment,MAX(c.单位名称) AS company,MAX(c.试验室名称) AS testroom ,COUNT(1) AS ncount,a.TestRoomCode AS testcode  FROM dbo.sys_document a JOIN dbo.sys_module b ON a.ModuleID = b.ID AND  b.ModuleType=1  AND  a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''  AND  TestRoomCode IN ('+@testcode+')   and a.Status>0    JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码  GROUP BY a.TestRoomCode '
   INSERT #tmp1
         ( 
           project ,
           segment ,
           company ,
           testroom ,
           ncount,
		   testcode
         )
		   EXEC sp_executesql @sqls 
	
	
	INSERT #tmp2
         ( 
           project ,
           segment ,
           company ,
           testroom ,
           ncount,
		   testcode         )
	SELECT a.工程名称,a.标段名称,a.单位名称,a.试验室名称, b.ncount,a.试验室编码  FROM dbo.v_bs_codeName a LEFT JOIN #tmp1 b ON a.试验室编码=b.testcode  JOIN dbo.Sys_Tree c ON a.试验室编码=c.NodeCode ORDER BY OrderID

	CREATE TABLE #t1
	(
	counts FLOAT,
	testcode NVARCHAR(50)
	)
	
SET @sqls='	INSERT #t1
	        ( counts, testcode )
			 SELECT COUNT(DISTINCT a.IndexID),b.TestRoomCode FROM dbo.v_invalid_document a JOIN dbo.sys_document b ON  a.IndexID = b.ID AND b.BGRQ>='''+@startdate+''' AND b.BGRQ<'''+@enddate+''' AND b.TestRoomCode IN ('+@testcode+') AND b.Status>0  AND  F_InvalidItem NOT LIKE ''%#%''   AND a.AdditionalQualified=0    GROUP BY b.TestRoomCode'
  EXEC sp_executesql @sqls 



   INSERT #tmp5
         ( 
           project ,
           segment ,
           company ,
           testroom ,
           testcode ,
           totalncount ,
           counts ,
           prenct
         )

  SELECT project,segment,company,testroom,a.testcode,ncount,counts,
  (CASE WHEN counts IS NULL THEN 0
  WHEN counts='' THEN 0
  WHEN counts=0 THEN 0
  ELSE  ROUND(counts/ncount*100,2) END
  ) FROM #tmp2 a LEFT JOIN #t1 b ON a.testcode = b.testcode
			 
 
		 		
  END


   declare @totalcounts int
  select @totalcounts=count(ID) from #tmp5

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 

		SET @Counts=@totalcounts

	select * from #tmp5 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page)) 
			
END
go


create procedure dbo.spweb_failreport_chart @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 DECLARE @sqls nvarchar(4000)	

 IF @testcode!=''

 BEGIN

 CREATE  TABLE #t1
 (
 segment NVARCHAR(50),
  company NVARCHAR(50),
   totalncount FLOAT,
    companycode NVARCHAR(50),
 )

  CREATE  TABLE #t2
 (
   counts FLOAT,
    companycode VARCHAR(50)
 )

  CREATE  TABLE #t0
  (
  testcode NVARCHAR(50)
  )
   
   SET @sqls='INSERT #t1
           ( segment ,
             company ,
             totalncount ,
             companycode
           )     
  SELECT  MAX(c.标段名称) segment,MAX(c.单位名称) company, COUNT(1) AS totalncount ,CompanyCode   FROM dbo.sys_document a JOIN dbo.sys_module b ON a.ModuleID = b.ID  AND b.ModuleType=1  AND   a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''  AND  TestRoomCode IN ('+@testcode+')  and a.Status>0  JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码   GROUP BY CompanyCode ' 
  EXEC sp_executesql @sqls 

set @sqls=' INSERT #t0 (testcode) SELECT 试验室编码 FROM dbo.v_bs_codeName WHERE 试验室编码 IN ('+@testcode+')  '

 EXEC sp_executesql @sqls 
 


   INSERT #t2 
           ( counts, companycode )
   SELECT COUNT(1),CompanyCode FROM dbo.v_invalid_document a JOIN #t0 b ON a.TestRoomCode=b.testcode AND ReportDate>=@startdate AND ReportDate<@enddate AND  F_InvalidItem NOT LIKE '%#%' AND AdditionalQualified=0  GROUP BY CompanyCode

		
	 SELECT a.segment,a.company,a.totalncount,a.companycode, counts INTO #t5 FROM #t1 a FULL JOIN #t2 b ON a.companycode = b.companycode  	
	 
	 UPDATE #t5 SET counts=0 WHERE counts IS NULL
  
  
  SELECT a.segment,a.company,a.totalncount,a.companycode, a.counts,(CASE WHEN a.totalncount=0 THEN 0 ELSE ROUND((a.counts/a.totalncount)*100,2) END )  prenct FROM #t5  a  ORDER BY a.counts DESC
  
  

  END
  
END
go


create procedure dbo.spweb_failreport_chart_order @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 DECLARE @sqls nvarchar(4000)	

 IF @testcode!=''

 BEGIN

 CREATE  TABLE #t1
 (
 segment NVARCHAR(50),
  company NVARCHAR(50),
   totalncount FLOAT,
   
    companycode NVARCHAR(50),
 )

  CREATE  TABLE #t2
 (
   counts FLOAT,
    companycode VARCHAR(50)
 )

   CREATE  TABLE #t3
 (
   uncounts FLOAT,
    companycode VARCHAR(50)
 )
  CREATE  TABLE #t0
  (
  testcode NVARCHAR(50)
  )
  
   
   SET @sqls='INSERT #t1
           ( segment ,
             company ,
             totalncount ,
             companycode
           )     
  SELECT  MAX(c.标段名称) segment,MAX(c.单位名称) company, COUNT(1) AS totalncount ,CompanyCode   FROM dbo.sys_document a  JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码  
  where 
  a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''  AND a.Status>0
  AND  a.TestRoomCode IN ('+@testcode+') 
  AND a.ModuleID in (select ID from sys_module where ModuleType=1)
   GROUP BY CompanyCode ' 
  EXEC sp_executesql @sqls 

set @sqls=' INSERT #t0 (testcode) SELECT 试验室编码 FROM dbo.v_bs_codeName WHERE 试验室编码 IN ('+@testcode+')  '

 EXEC sp_executesql @sqls 
 

   --不合格
   INSERT #t2 
           ( counts, companycode )
   SELECT COUNT(1),CompanyCode FROM dbo.v_invalid_document a JOIN #t0 b ON a.TestRoomCode=b.testcode AND ReportDate>=@startdate AND ReportDate<@enddate AND  F_InvalidItem NOT LIKE '%#%' AND AdditionalQualified=0  GROUP BY CompanyCode


   --未处理
   INSERT #t3 
           ( uncounts, companycode )
   SELECT COUNT(1),CompanyCode FROM dbo.v_invalid_document a JOIN #t0 b ON a.TestRoomCode=b.testcode AND ReportDate>=@startdate AND ReportDate<@enddate AND  F_InvalidItem NOT LIKE '%#%' AND AdditionalQualified=0   AND (SGComment IS NULL OR JLComment IS NULL)  GROUP BY CompanyCode --


   select 
   #t1.*,
   #t2.counts,#t3.uncounts 
   INTO #t5
   from #t1 
   left outer join #t2 on #t1.companycode = #t2.companycode
   left outer join #t3 on #t1.companycode = #t3.companycode

   	 
	 UPDATE #t5 SET counts=0 WHERE counts IS NULL
   UPDATE #t5 SET uncounts=0 WHERE uncounts IS NULL   

 

   SELECT a.segment,a.company,a.totalncount,a.companycode, a.counts,(CASE WHEN a.totalncount=0 THEN 0 ELSE ROUND((a.counts/a.totalncount)*100,2) END )  prenct,uncounts FROM #t5  a  JOIN dbo.Sys_Tree b ON a.companycode=b.NodeCode ORDER BY b.OrderID

  END
  
END
go


create procedure dbo.spweb_failreport_chart_pop @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@modelID VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN
 
 DECLARE @sqls nvarchar(4000)	,
 @n INT,
 @modelindex VARCHAR(50),
 @testroomid VARCHAR(50),
				@m INT
 
	 CREATE TABLE #t1
	 (
	 modelID VARCHAR(50),
	 NodeCode VARCHAR(50)
	 )

		IF @testcode!='' 
				BEGIN
						SET @sqls='INSERT #t1 ( modelID, NodeCode ) SELECT  DISTINCT b.ID AS modelID,LEFT(a.NodeCode,16) NodeCode   FROM sys_engs_Tree  a JOIN sys_biz_module b ON a.RalationID=b.ID  AND LEFT(a.NodeCode,16) IN ('+@testcode+')  ORDER BY LEFT(a.NodeCode,16)'
					 EXEC	sp_executesql @sqls
				END
  
   CREATE TABLE #t2
   (
   ModelCode VARCHAR(50),
   ModelIndex VARCHAR(50),   
   ReportName VARCHAR(50),   
   ReportNumber VARCHAR(50),   
   ReportDate DATETIME NULL,
   F_InvalidItem VARCHAR(5000)
   )              
	
		DECLARE cur CURSOR FOR
 		SELECT * FROM #t1

		OPEN cur
		FETCH NEXT FROM cur INTO @modelindex,@testroomid
		WHILE @@FETCH_STATUS = 0
	BEGIN

				SET @sqls='  SELECT ModelCode ,ModelIndex , ReportName , ReportNumber ,ReportDate,F_InvalidItem FROM  dbo.sys_biz_reminder_evaluateData a JOIN dbo.[biz_norm_extent_'+@modelindex+'] b ON a.ID = b.ID and LEFT(b.SCPT,16)='''+@testroomid+''' AND a.AdditionalQualified=0 and    a.F_InvalidItem NOT LIKE ''%#%'' and    a.SCTS>='''+@startdate+''' AND a.SCTS<'''+@enddate+'''' 
					
		      INSERT #t2
			        ( ModelCode ,ModelIndex , ReportName , ReportNumber ,ReportDate,F_InvalidItem
			        )
		   EXEC sp_executesql @sqls 
			

  
  	   FETCH NEXT FROM cur INTO  @modelindex,@testroomid
	END

	CLOSE cur
	DEALLOCATE cur	  

		
		 
		   create table #tmp4
 (
id bigint identity(1,1)  primary key,
project varchar(50),
segment varchar(50),
company varchar(50),
companycode VARCHAR(50),
testroom varchar(50),
testcode VARCHAR(50),
   ModelCode VARCHAR(50),
   ModelIndex VARCHAR(50),   
    ModelName VARCHAR(50),  
   ReportName VARCHAR(50),   
   ReportNumber VARCHAR(50),   
   ReportDate DATETIME,
   F_InvalidItem VARCHAR(5000)
 )
 
 INSERT #tmp4 (
 project,
 segment,
 company,
 companycode,
 testroom,
 testcode,
 ModelCode,
 ModelIndex,
 ModelName,
 ReportName,
 ReportNumber,
 ReportDate,F_InvalidItem)   
 SELECT  b.工程名称,b.标段名称,b.单位名称 ,b.单位编码,b.试验室名称,试验室编码, a.ModelCode,
  a.ModelIndex,
 c.Description,
 a.ReportName,
 a.ReportNumber,
 a.ReportDate,a.F_InvalidItem FROM #t2 a JOIN dbo.v_bs_codeName b ON
 LEFT(ModelCode,16)=b.试验室编码 JOIN dbo.sys_biz_Module c ON a.ModelIndex= c.ID
 

 		   
  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp4

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 

 
	--select @Counts=count(ID) from #tmp2 where  id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  
		SET @Counts=@totalcounts

	select * from #tmp4 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  
 
			
END
go


create procedure dbo.spweb_jzReport @testcode varchar(5000), --有权限的实验室id集合
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 1 output,           
 	@Counts    int = 1 output as
BEGIN

 	CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount FLOAT,
	pxCount FLOAT,
	frequency VARCHAR(50),
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty FLOAT
	)
 
 CREATE TABLE #t1
 (
 ModelIndex VARCHAR(50),
 ModelName VARCHAR(50),
 TestRoomCode VARCHAR(50),
 ZjCount FLOAT,
 JzCount FLOAT,
 JLCompanyName VARCHAR(50)
 )



DECLARE @sqls NVARCHAR(4000)

IF	@testcode!=''
BEGIN
	SET @sqls=' INSERT #t1
			 ( ModelIndex ,
			   ModelName ,
			   TestRoomCode ,
			   ZjCount ,
			   JzCount ,
			   JLCompanyName
			 )
		SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND TestRoomCode IN ('+@testcode+') GROUP BY TestRoomCode,JLCompanyName,ModelIndex'

		EXEC	sp_executesql @sqls
		
		
 
		

			DELETE #t1 WHERE ModelIndex NOT IN 
    (SELECT DISTINCT ModelIndex FROM sys_biz_reminder_Itemfrequency WHERE IsActive=1 AND FrequencyType=2)

	END
    


INSERT #tmp1
        ( 
          modelName ,
          zjCount ,
          pxCount ,
          jl ,
          modelID ,
          testroomID 
        )	
 SELECT ModelName,ZjCount, JzCount,JLCompanyName,ModelIndex,TestRoomCode FROM #t1

 
 
	 SELECT  
           a.modelName ,
             condition ,
           zjCount ,
           pxCount ,
          ROUND((pxCount/zjCount)*100,2) AS frequency ,
           result ,
           b.标段名称 segment ,
           jl ,
           b.单位名称 sg ,
           b.试验室名称 testroom ,
           modelID ,
           a.testroomID ,
           pxqulifty INTO #t3 FROM #tmp1 a JOIN dbo.v_bs_codeName b
		   ON a.testroomID=b.试验室编码
		     JOIN dbo.Sys_Tree c ON b.试验室编码=c.NodeCode
		     ORDER BY pxCount DESC




		   TRUNCATE TABLE #tmp1
		   INSERT #tmp1
		           ( 
		             modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty
		           )SELECT * FROM #t3  


	 UPDATE #tmp1 SET condition=(b.Frequency*100) FROM #tmp1 a JOIN sys_biz_reminder_Itemfrequency b
			 ON a.modelID=b.ModelIndex AND a.testroomID=b.TestRoomCode AND b.IsActive=1  AND b.FrequencyType=2
 
 
 
			 SELECT *  INTO #tmp2 FROM #tmp1
			   TRUNCATE TABLE #tmp1

			 INSERT #tmp1
			         (  
			           modelName ,
			           condition ,
			           zjCount ,
			           pxCount ,
			           frequency ,
			           result ,
			           segment ,
			           jl ,
			           sg ,
			           testroom ,
			           modelID ,
			           testroomID ,
			           pxqulifty
			         )
			SELECT  modelName ,
			           condition ,
			           zjCount ,
			           pxCount ,
			           frequency ,
			           result ,
			           segment ,
			           jl ,
			           sg ,
			           testroom ,
			           modelID ,
			           testroomID ,
			           pxqulifty FROM #tmp2  WHERE condition IS NOT NULL
 

    --取得分页总数
  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1
	  declare @pageIndex int --总数/页大小
    declare @lastcount int --总数%页大小  

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 


		SET @Counts=@totalcounts
	SELECT id, modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		                 (CASE WHEN frequency>=condition THEN '满足' ELSE '不满足' END ) result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty from #tmp1  	
	 where id>(@pageSize*(@page-1)) and id<=(@pageSize*@page)   
 

END
go


create procedure dbo.spweb_login_charts @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 DECLARE @sqls nvarchar(4000)	

 CREATE  TABLE #t1
 (
 TestRoomCode VARCHAR(50),
  nTotals INT,
 CompanyName VARCHAR(50),
 SegmentName VARCHAR(50),
 TestRoomName VARCHAR(50),
 CompanyCode VARCHAR(50)
 )


   
   SET @sqls=' INSERT #t1
         ( TestRoomCode ,
           nTotals ,
           CompanyName ,
           SegmentName ,
           TestRoomName ,
           CompanyCode
         )
SELECT TestRoomCode,COUNT(TestRoomCode) nTotals,MAX(CompanyName) CompanyName,MAX(SegmentName) SegmentName, MAX(TestRoomName) TestRoomName,LEFT(TestRoomCode,12) CompanyCode  FROM dbo.sys_loginlog WHERE LEN(TestRoomCode)>8 AND TestRoomCode IN ('+@testcode+')  AND loginDay>='''+@startdate+'''  AND loginDay<'''+@enddate+'''  GROUP BY TestRoomCode '

  EXEC sp_executesql @sqls 

  CREATE TABLE #t2
  (
  UserName VARCHAR(50),
  TestRoomCode VARCHAR(50)
  )





SET @sqls='    INSERT #t2
          ( UserName, TestRoomCode )
SELECT UserName,MAX(TestRoomCode) TestRoomCode  FROM dbo.sys_loginlog  WHERE LEN(TestRoomCode)>8  AND TestRoomCode IN ('+@testcode+')  AND loginDay>='''+@startdate+'''  AND loginDay<'''+@enddate+'''   GROUP BY UserName
'

 EXEC sp_executesql @sqls 



 SELECT TestRoomCode,COUNT(1) nUserCounts INTO #t3 FROM #t2  GROUP BY TestRoomCode


 SELECT MAX(CompanyName) CompanyName,MAX(SegmentName) SegmentName,SUM(nTotals) nTotals,SUM(nUserCounts) nUserCounts,CompanyCode INTO #t4 FROM #t1 a JOIN #t3 b ON a.TestRoomCode = b.TestRoomCode   GROUP BY  a.CompanyCode
  

  SELECT a.* FROM #t4 a JOIN dbo.Sys_Tree b ON a.CompanyCode=b.NodeCode ORDER BY OrderID
			
END
go


create procedure dbo.spweb_login_charts_pop @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 

 

 SELECT UserName,MAX(SegmentName) SegmentName,MAX(CompanyName) CompanyName,MAX(TestRoomName) TestRoomName,COUNT(1) nTotals FROM dbo.sys_loginlog WHERE LEFT(TestRoomCode,12)=@testcode AND LEN(TestRoomCode)>8   AND loginDay>=@startdate  AND loginDay<@enddate GROUP BY UserName ORDER BY COUNT(1) DESC
   
   
			
END
go


create procedure dbo.spweb_login_one @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 

 
SELECT loginDay,MAX(UserName),COUNT(1) nTotals FROM dbo.sys_loginlog WHERE UserName=@testcode AND  LEN(TestRoomCode)>8 AND loginDay>=@startdate AND loginDay<@enddate  GROUP BY loginDay
			
END
go


create procedure dbo.spweb_loginlogpop @testcode VARCHAR(50), --标准实验室id
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30) as
BEGIN



	 IF	@testcode!=''
	 BEGIN
    SELECT UserName,FirstAccessTime,LastAccessTime FROM dbo.sys_loginlog WHERE UserName=@testcode AND FirstAccessTime>@startdate AND FirstAccessTime<=@enddate
	 END
  
  ELSE
  BEGIN
	  SELECT UserName,FirstAccessTime,LastAccessTime FROM dbo.sys_loginlog WHERE 1=0
  END
     
			
END
go


create procedure dbo.spweb_machineSummary_chart @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT as
BEGIN

		CREATE TABLE #tmp
		(
		id bigint identity(1,1)  primary key,
		试验室名称   nvarchar(50),
		 试验室编码 NVARCHAR(50),
		 ncount INT
		 
		) 
		DECLARE 
	@testid VARCHAR(50),
	@testname VARCHAR(50),
	@num INT,
		@sqls nvarchar(4000)
 


 	CREATE TABLE #t1
	(
	testcode VARCHAR(50),
	testname VARCHAR(50)
	)
	IF @testcode !='' 
  
  BEGIN  
SET @sqls='INSERT #t1 ( testcode,testname ) SELECT   LEFT(NodeCode,16),DESCRIPTION   FROM Sys_Tree where LEFT(NodeCode,16) IN ('+@testcode+')    order BY OrderID'

 	  EXEC	sp_executesql @sqls

	  END

 

		DECLARE cur CURSOR FOR	
--SELECT  试验室编码, 试验室名称 FROM dbo.v_bs_codeName  GROUP BY 试验室编码, 试验室名称

SELECT testcode,testname FROM #t1

	OPEN cur
	FETCH NEXT FROM cur INTO @testid,@testname
	WHILE @@FETCH_STATUS = 0
	BEGIN		
	
	SELECT @num=COUNT(*) FROM  dbo.v_bs_machineSummary WHERE 试验室编码=@testid
	
	IF	@num=0
	BEGIN
  INSERT #tmp
        (  试验室名称, 试验室编码, ncount )
VALUES(@testname,@testid,0) 
  END  
	ELSE
  BEGIN  
INSERT #tmp
        (  试验室名称, 试验室编码, ncount )
SELECT  MAX(试验室名称)  ,MAX(试验室编码) , COUNT(*) FROM  dbo.v_bs_machineSummary WHERE    试验室编码=@testid
END
	
	  FETCH NEXT FROM cur INTO @testid,@testname
	END
	CLOSE cur
	DEALLOCATE cur

	    
IF @ftype=1
		begin
  
		  IF @testcode!=''
		  BEGIN
				SELECT SUM(a.ncount) as ncount,b.单位名称  companyname,b.单位编码 AS companycode, b.标段名称 as segment INTO #t8 FROM  #tmp a JOIN dbo.v_bs_codeName b ON a.试验室编码 = b.试验室编码  GROUP BY b.单位编码,b.单位名称,b.标段名称
				
				SELECT ncount,companyname,companycode,segment FROM #t8 a  JOIN dbo.Sys_Tree b ON a.companycode=b.NodeCode ORDER BY OrderID
					
			END
		  ELSE
		  BEGIN
		  SELECT '' as ncount,'' as  companyname,'' AS companycode, '' as segment FROM  #tmp WHERE 1=0
		  END
    

		END   
IF @ftype=2
		begin

		IF  @testcode!=''
		  BEGIN
			  SELECT SUM(a.ncount) as ncount,b.试验室名称 AS companyname,b.试验室编码 AS companycode,b.标段名称 as segment  FROM  #tmp a JOIN dbo.v_bs_codeName b ON a.试验室编码 = b.试验室编码   GROUP BY b.试验室编码,b.试验室名称,b.标段名称 
				
					 END
		   ELSE
		  BEGIN
		  SELECT '' as ncount,'' as  companyname,'' AS companycode, '' as segment FROM  #tmp WHERE 1=0
		  END           
		END
   ELSE
	  BEGIN
   SELECT '' as ncount,'' as  companyname,'' AS companycode, '' as segment FROM  #tmp WHERE 1=0
	  END   
 


END
go


create procedure dbo.spweb_machineSummary_chart_pop @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT as
BEGIN

		CREATE TABLE #tmp
		(
		id bigint identity(1,1)  primary key,
		
		 试验室编码 NVARCHAR(50),
		 ncount INT
		 
		) 
		DECLARE 
	@code VARCHAR(50),
	@companyname VARCHAR(50),
	@num INT,
	@sqls NVARCHAR(4000)

	SET @sqls=' SELECT LEFT(NodeCode,16) FROM  dbo.sys_engs_Tree WHERE LEFT(NodeCode,16) IN ('+@testcode+') '
 
 CREATE TABLE #t1
 (
 testcode VARCHAR(50)
 )

 INSERT #t1
         ( testcode )
 EXEC	sp_executesql @sqls

		DECLARE cur CURSOR FOR	
SELECT testcode FROM #t1

	OPEN cur
	FETCH NEXT FROM cur INTO @code
	WHILE @@FETCH_STATUS = 0
	BEGIN		
	
	SELECT @num=COUNT(*) FROM  dbo.v_bs_machineSummary WHERE  试验室编码=@code
	
	IF	@num=0
	BEGIN
  INSERT #tmp
        (   试验室编码, ncount )
VALUES(@testcode,0) 
  END  
	ELSE
  BEGIN  
INSERT #tmp
        (   试验室编码, ncount )
SELECT  MAX(试验室编码) , COUNT(*) FROM  dbo.v_bs_machineSummary WHERE    试验室编码=@code
END
	
	  FETCH NEXT FROM cur INTO @code
	END
	CLOSE cur
	DEALLOCATE cur



    SELECT DISTINCT 试验室名称 AS testname,a.试验室编码 AS companycode, ncount FROM #tmp a JOIN dbo.v_bs_codeName b ON a.试验室编码 = b.试验室编码


END
go


create procedure dbo.spweb_pager @tblName     nvarchar(255),        ----要显示的表或多个表的连接
 @fldName     nvarchar(1000) = '*',    ----要显示的字段列表
 @pageSize    int = 1,        ----每页显示的记录个数
 @page        int = 10,        ----要显示那一页的记录
 @pageCount    int = 1 output,            ----查询结果分页后的总页数
 @Counts    int = 1 output,                ----查询到的记录数
 @fldSort    nvarchar(200) = null,    ----排序字段列表或条件
 @Sort        bit = 1,        ----排序方法，0为升序，1为降序(如果是多字段排列Sort指代最后一个排序字段的排列顺序(最后一个排序字段不加排序标记)--程序传参如：' SortA Asc,SortB Desc,SortC ')
 @strCondition    nvarchar(4000) = null,    ----查询条件,不需where
 @ID        nvarchar(150),        ----主表的主键
 @Dist                 bit = 0           ----是否添加查询字段的 DISTINCT 默认0不添加/1添加 as
BEGIN	
declare @strSQL nvarchar(4000) -- 主语句
declare @strOrder varchar(400) -- 排序类型
    set @strSQL = 'select @Counts=count(*) from ' + @tblName + ' where 1=1 '+ @strCondition
    --PRINT @strSQL
    exec sp_executesql @strSQL,N'@Counts int out ',@Counts OUT
    declare @tmpCounts int
if @Counts = 0
set @tmpCounts = 1
else
set @tmpCounts = @Counts
    --取得分页总数
    set @pageCount=(@tmpCounts+@pageSize-1)/@pageSize
    /**//**当前页大于总页数 取最后一页**/
    if @page>@pageCount
        set @page=@pageCount
        
if @Sort = 1--降序
begin
set @strOrder = ' order by ' + @fldSort +' desc'--如果@OrderType不是0，就执行降序，这句很重要！
end
else
begin
set @strOrder = ' order by ' + @fldSort +' asc'
END



set @strSQL = 'select top ' + str(@pageSize*@page) +' IDENTITY(INT,1,1) AS rowNum,' + @fldName + 
   ' into #tmp from ' + @tblName + ' where 1=1 ' + @strCondition + ' ' + @strOrder
+';select '+@fldName +' from #tmp where rowNum>'+str(@pageSize*(@page-1))



--PRINT @strSQL
exec (@strSQL)
END
go


create procedure dbo.spweb_pxjzReport @testcode varchar(5000), --有权限的实验室id集合
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 1 output,           
 	@Counts    int = 1 output as
BEGIN

 	CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount FLOAT,
	pxCount FLOAT,
	frequency VARCHAR(50),
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty FLOAT
	)
 
 CREATE TABLE #t1
 (
 ModelIndex VARCHAR(50),
 ModelName VARCHAR(50),
 TestRoomCode VARCHAR(50),
 ZjCount FLOAT,
 JzCount FLOAT,
 JLCompanyName VARCHAR(50)
 )

DECLARE @sqls NVARCHAR(4000)

IF	@testcode!=''
BEGIN
	SET @sqls=' INSERT #t1
			 ( ModelIndex ,
			   ModelName ,
			   TestRoomCode ,
			   ZjCount ,
			   JzCount ,
			   JLCompanyName
			 )
		SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>='''+@startdate+''' AND BGRQ<'''+@enddate+''' AND TestRoomCode IN ('+@testcode+') GROUP BY TestRoomCode,JLCompanyName,ModelIndex'

		EXEC	sp_executesql @sqls
		
		
 

		
		

		DELETE #t1 WHERE ModelIndex NOT IN 
    (SELECT DISTINCT ModelIndex FROM sys_biz_reminder_Itemfrequency WHERE IsActive=1 AND FrequencyType=1)

	END
    


INSERT #tmp1
        ( 
          modelName ,
          zjCount ,
          pxCount ,
          jl ,
          modelID ,
          testroomID 
        )	
 SELECT ModelName,ZjCount, dbo.Fweb_ReturnPXCount(ModelIndex,TestRoomCode,@startdate,@enddate),JLCompanyName,ModelIndex,TestRoomCode FROM #t1

 
 
	 SELECT  
           a.modelName ,
             condition ,
           zjCount ,
           pxCount ,
          ROUND((pxCount/zjCount)*100,2) AS frequency ,
           result ,
           b.标段名称 segment ,
           jl ,
           b.单位名称 sg ,
           b.试验室名称 testroom ,
           modelID ,
           a.testroomID ,
           pxqulifty INTO #t3 FROM #tmp1 a JOIN dbo.v_bs_codeName b
		   ON a.testroomID=b.试验室编码  JOIN dbo.Sys_Tree c ON b.试验室编码=c.NodeCode
		     ORDER BY pxCount DESC



		   TRUNCATE TABLE #tmp1
		   INSERT #tmp1
		           ( 
		             modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty
		           )SELECT * FROM #t3 


	 UPDATE #tmp1 SET condition=(b.Frequency*100) FROM #tmp1 a JOIN sys_biz_reminder_Itemfrequency b
			 ON a.modelID=b.ModelIndex AND a.testroomID=b.TestRoomCode AND b.IsActive=1  AND b.FrequencyType=1

		 SELECT * INTO #tmp2 FROM #tmp1

			  TRUNCATE TABLE #tmp1

			  INSERT #tmp1
			          ( 
			            modelName ,
			            condition ,
			            zjCount ,
			            pxCount ,
			            frequency ,
			            result ,
			            segment ,
			            jl ,
			            sg ,
			            testroom ,
			            modelID ,
			            testroomID ,
			            pxqulifty
			          )
			  SELECT modelName ,
			            condition ,
			            zjCount ,
			            pxCount ,
			            frequency ,
			            result ,
			            segment ,
			            jl ,
			            sg ,
			            testroom ,
			            modelID ,
			            testroomID ,
			            pxqulifty FROM #tmp2 WHERE condition IS NOT NULL



    --取得分页总数
  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1
	  declare @pageIndex int --总数/页大小
    declare @lastcount int --总数%页大小  

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 


		SET @Counts=@totalcounts
	SELECT id, modelName ,
		             condition ,
		              zjCount,
		             pxCount ,
		             frequency ,
		                 (CASE WHEN frequency>=condition THEN '满足' ELSE '不满足' END ) result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             dbo.Fweb_ReturnPXQualityCount(modelID,testroomID,@startdate,@enddate) pxqulifty from #tmp1  	
	 where id>(@pageSize*(@page-1)) and id<=(@pageSize*@page)   
 

END
go


create procedure dbo.spweb_pxjzReport_ByCode @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT,
 	@modelID VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

 	CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount FLOAT,
	pxCount FLOAT,
	frequency FLOAT,
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty FLOAT
	)
 
 CREATE TABLE #t1
 (
 ModelIndex VARCHAR(50),
 ModelName VARCHAR(50),
 TestRoomCode VARCHAR(50),
 ZjCount FLOAT,
 JzCount FLOAT,
 JLCompanyName VARCHAR(50)
 )

DECLARE @sqls NVARCHAR(4000)

IF	@testcode!=''
BEGIN
	
						IF @ftype=1
						BEGIN
							SET @sqls=' INSERT #t1
								 ( ModelIndex ,
								   ModelName ,
								   TestRoomCode ,
								   ZjCount ,
								   JzCount ,
								   JLCompanyName
								 )
							SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND TestRoomCode IN ('+@testcode+') GROUP BY TestRoomCode,ModelIndex'	  
					   END  
					   IF	@ftype=2
					   BEGIN
							SET @sqls=' INSERT #t1
								 ( ModelIndex ,
								   ModelName ,
								   TestRoomCode ,
								   ZjCount ,
								   JzCount ,
								   JLCompanyName
								 )
							SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND TestRoomCode IN ('+@testcode+') GROUP BY TestRoomCode,ModelIndex'
					  END 

 
		EXEC	sp_executesql @sqls

			DELETE #t1 WHERE ModelIndex NOT IN 
    (SELECT DISTINCT ModelIndex FROM sys_biz_reminder_Itemfrequency WHERE IsActive=1 AND FrequencyType=1)

	END
    


INSERT #tmp1
        ( 
          modelName ,
          zjCount ,
          pxCount ,
          jl ,
          modelID ,
          testroomID 
        )	
 SELECT ModelName,ZjCount, dbo.Fweb_ReturnPXCount(ModelIndex,TestRoomCode,@startdate,@enddate),JLCompanyName,ModelIndex,TestRoomCode FROM #t1

 
 
	 SELECT  
           a.modelName ,
             condition ,
           zjCount ,
           pxCount ,
          ROUND((pxCount/zjCount)*100,2) AS frequency ,
           result ,
           b.标段名称 segment ,
           jl ,
           b.单位名称 sg ,
           b.试验室名称 testroom ,
           modelID ,
           a.testroomID ,
           pxqulifty INTO #t3 FROM #tmp1 a JOIN dbo.v_bs_codeName b
		   ON a.testroomID=b.试验室编码
		     ORDER BY segment,sg,testroom



		   TRUNCATE TABLE #tmp1
		   INSERT #tmp1
		           ( 
		             modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty
		           )SELECT * FROM #t3 


	 UPDATE #tmp1 SET condition=(b.Frequency*100) FROM #tmp1 a JOIN sys_biz_reminder_Itemfrequency b
			 ON a.modelID=b.ModelIndex AND a.testroomID=b.TestRoomCode AND b.IsActive=1  AND b.FrequencyType=1

			UPDATE #tmp1 SET pxqulifty=dbo.Fweb_ReturnPXQualityCount(modelID,testroomID,@startdate,@enddate)

			--SELECT * FROM #tmp1

			SELECT modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty INTO #t4 FROM #tmp1


					 DECLARE @para1 INT
					DECLARE @para2 INT                   

					 IF @modelID=''
					 BEGIN
					  SET	@para1=-10000
					  SET	@para2=10000
					 END

					 IF @modelID='0'
					 BEGIN
					  SET	@para1=-10000
					  SET	@para2=0
					 END
					IF @modelID='60'
					 BEGIN
					  SET	@para1=0
					  SET	@para2=60
					 END  
					 IF @modelID='80'
					 BEGIN
					  SET	@para1=60
					  SET	@para2=80
					 END    
					 
					  IF @modelID='100'
					 BEGIN
					  SET	@para1=80
					  SET	@para2=100
					 END 
					 
					   IF @modelID='101'
					 BEGIN
					  SET	@para1=100
					  SET	@para2=10000
					 END 
				  
			 TRUNCATE TABLE #tmp1

							 INSERT #tmp1
						   ( 
							 modelName ,
							 condition ,
							 zjCount ,
							 pxCount ,
							 frequency ,
							 result ,
							 segment ,
							 jl ,
							 sg ,
							 testroom ,
							 modelID ,
							 testroomID ,
							 pxqulifty
						   )SELECT * FROM #t4 WHERE pxqulifty BETWEEN @para1 AND @para2
				                


    --取得分页总数
  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1
	  declare @pageIndex int --总数/页大小
    declare @lastcount int --总数%页大小  

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 


		SET @Counts=@totalcounts
	SELECT id, modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		                 (CASE WHEN frequency>=condition THEN '满足' ELSE '不满足' END ) result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             dbo.Fweb_ReturnPXQualityCount(modelID,testroomID,@startdate,@enddate) pxqulifty from #tmp1  	
	 where id>(@pageSize*(@page-1)) and id<=(@pageSize*@page)   
 

END
go


create procedure dbo.spweb_pxjzReport_ByCode_fail @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT,
 	@modelID VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

 	CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount FLOAT,
	pxCount FLOAT,
	frequency FLOAT,
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty FLOAT
	)
 
 CREATE TABLE #t1
 (
 ModelIndex VARCHAR(50),
 ModelName VARCHAR(50),
 TestRoomCode VARCHAR(50),
 ZjCount FLOAT,
 JzCount FLOAT,
 JLCompanyName VARCHAR(50)
 )

DECLARE @sqls NVARCHAR(4000)

IF	@testcode!=''
BEGIN
	
						IF @ftype=1
						BEGIN
							SET @sqls=' INSERT #t1
								 ( ModelIndex ,
								   ModelName ,
								   TestRoomCode ,
								   ZjCount ,
								   JzCount ,
								   JLCompanyName
								 )
							SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND TestRoomCode IN ('+@testcode+') GROUP BY TestRoomCode,ModelIndex'	  
					   END  
					   IF	@ftype=2
					   BEGIN
							SET @sqls=' INSERT #t1
								 ( ModelIndex ,
								   ModelName ,
								   TestRoomCode ,
								   ZjCount ,
								   JzCount ,
								   JLCompanyName
								 )
							SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND LEFT(TestRoomCode,12) IN ('+@testcode+') GROUP BY TestRoomCode,ModelIndex'
					  END 

 
		EXEC	sp_executesql @sqls

	END
    


INSERT #tmp1
        ( 
          modelName ,
          zjCount ,
          pxCount ,
          jl ,
          modelID ,
          testroomID 
        )	
 SELECT ModelName,ZjCount, dbo.Fweb_ReturnPXCount(ModelIndex,TestRoomCode,@startdate,@enddate),JLCompanyName,ModelIndex,TestRoomCode FROM #t1

 
 
	 SELECT  
           a.modelName ,
             condition ,
           zjCount ,
           pxCount ,
          ROUND((pxCount/zjCount)*100,2) AS frequency ,
           result ,
           b.标段名称 segment ,
           jl ,
           b.单位名称 sg ,
           b.试验室名称 testroom ,
           modelID ,
           a.testroomID ,
           pxqulifty INTO #t3 FROM #tmp1 a JOIN dbo.v_bs_codeName b
		   ON a.testroomID=b.试验室编码
		     ORDER BY segment,sg,testroom



		   TRUNCATE TABLE #tmp1
		   INSERT #tmp1
		           ( 
		             modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty
		           )SELECT * FROM #t3 WHERE pxCount>0


	 UPDATE #tmp1 SET condition=(b.Frequency*100) FROM #tmp1 a JOIN sys_biz_reminder_Itemfrequency b
			 ON a.modelID=b.ModelIndex AND a.testroomID=b.TestRoomCode AND b.IsActive=1  AND b.FrequencyType=1

			UPDATE #tmp1 SET pxqulifty=dbo.Fweb_ReturnPXQualityCount(modelID,testroomID,@startdate,@enddate)

			--SELECT * FROM #tmp1

			SELECT modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty INTO #t4 FROM #tmp1


					 DECLARE @para1 INT
					DECLARE @para2 INT                   

					 IF @modelID=''
					 BEGIN
					  SET	@para1=-10000
					  SET	@para2=10000
					 END

					 IF @modelID='0'
					 BEGIN
					  SET	@para1=-10000
					  SET	@para2=0
					 END
					IF @modelID='60'
					 BEGIN
					  SET	@para1=0
					  SET	@para2=60
					 END  
					 IF @modelID='80'
					 BEGIN
					  SET	@para1=60
					  SET	@para2=80
					 END    
					 
					  IF @modelID='100'
					 BEGIN
					  SET	@para1=80
					  SET	@para2=100
					 END 
					 
					   IF @modelID='101'
					 BEGIN
					  SET	@para1=100
					  SET	@para2=10000
					 END 
				  

				  DELETE #t4 WHERE frequency>condition


			 TRUNCATE TABLE #tmp1

							 INSERT #tmp1
						   ( 
							 modelName ,
							 condition ,
							 zjCount ,
							 pxCount ,
							 frequency ,
							 result ,
							 segment ,
							 jl ,
							 sg ,
							 testroom ,
							 modelID ,
							 testroomID ,
							 pxqulifty
						   )SELECT * FROM #t4 WHERE pxqulifty BETWEEN @para1 AND @para2
				                


    --取得分页总数
  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp1
	  declare @pageIndex int --总数/页大小
    declare @lastcount int --总数%页大小  

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 


		SET @Counts=@totalcounts
	SELECT id, modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		                 (CASE WHEN frequency>=condition THEN '满足' ELSE '不满足' END ) result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             dbo.Fweb_ReturnPXQualityCount(modelID,testroomID,@startdate,@enddate) pxqulifty from #tmp1  	
	 where id>(@pageSize*(@page-1)) and id<=(@pageSize*@page)   
 

END
go


create procedure dbo.spweb_pxjzReport_Chart @testcode varchar(5000), --有权限的实验室id集合
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30) as
BEGIN

 	CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount FLOAT,
	pxCount FLOAT,
	frequency FLOAT,
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty FLOAT
	)
 
 CREATE TABLE #t1
 (
 ModelIndex VARCHAR(50),
 ModelName VARCHAR(50),
 TestRoomCode VARCHAR(50),
 ZjCount FLOAT,
 JzCount FLOAT,
 JLCompanyName VARCHAR(50)
 )

DECLARE @sqls NVARCHAR(4000)

IF	@testcode!=''
BEGIN
	SET @sqls=' INSERT #t1
			 ( ModelIndex ,
			   ModelName ,
			   TestRoomCode ,
			   ZjCount ,
			   JzCount ,
			   JLCompanyName
			 )
		SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND TestRoomCode IN ('+@testcode+') GROUP BY TestRoomCode,ModelIndex'

		EXEC	sp_executesql @sqls

				DELETE #t1 WHERE ModelIndex NOT IN 
    (SELECT DISTINCT ModelIndex FROM sys_biz_reminder_Itemfrequency WHERE IsActive=1 AND FrequencyType=1)
	END
    


INSERT #tmp1
        ( 
          modelName ,
          zjCount ,
          pxCount ,
          jl ,
          modelID ,
          testroomID 
        )	
 SELECT ModelName,ZjCount, dbo.Fweb_ReturnPXCount(ModelIndex,TestRoomCode,@startdate,@enddate),JLCompanyName,ModelIndex,TestRoomCode FROM #t1

 
 
	 SELECT  
           a.modelName ,
             condition ,
           zjCount ,
           pxCount ,
          ROUND((pxCount/zjCount)*100,2) AS frequency ,
           result ,
           b.标段名称 segment ,
           jl ,
           b.单位名称 sg ,
           b.试验室名称 testroom ,
           modelID ,
           a.testroomID ,
           pxqulifty INTO #t3 FROM #tmp1 a JOIN dbo.v_bs_codeName b
		   ON a.testroomID=b.试验室编码
		     JOIN dbo.Sys_Tree c ON b.试验室编码=c.NodeCode ORDER BY OrderID



		   TRUNCATE TABLE #tmp1
		   INSERT #tmp1
		           ( 
		             modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty
		           )SELECT * FROM #t3 


	 UPDATE #tmp1 SET condition=(b.Frequency*100) FROM #tmp1 a JOIN sys_biz_reminder_Itemfrequency b
			 ON a.modelID=b.ModelIndex AND a.testroomID=b.TestRoomCode AND b.IsActive=1  AND b.FrequencyType=1


			 SELECT modelName ,
			           condition ,
			           zjCount ,
			           pxCount ,
			           frequency ,
			            (CASE WHEN frequency>=condition THEN '满足' ELSE '不满足' END ) result ,
			           segment ,
			           jl ,
			           sg ,
			           testroom ,
			           modelID ,
			           testroomID ,
			           pxqulifty INTO #t5 FROM #tmp1


	 
	 SELECT sg,COUNT(1) AS total,SUM(zjCount) zjCount,SUM(pxCount) pxCount,MAX(jl) jl,MAX(LEFT(testroomID,12)) AS testroomID,MAX(segment) segment INTO #t6 FROM #t5   GROUP BY sg 


	   SELECT sg,COUNT(1) AS failcount INTO #t7 FROM #t5  WHERE result='满足'  GROUP BY sg 
	   
	   
SELECT sg,0 AS failcount  INTO #t8 FROM #t5 WHERE sg NOT IN (SELECT sg FROM #t7)


INSERT #t7 (sg,
failcount) 
SELECT sg,MAX(failcount) FROM #t8  GROUP BY sg
 
 SELECT a.sg,a.jl,a.total,a.zjCount,a.pxCount,(a.total-b.failcount) AS failcount,testroomID,segment FROM #t6 a JOIN #t7 b ON a.sg = b.sg  JOIN dbo.Sys_Tree c ON a.sg=c.DESCRIPTION ORDER BY c.OrderID

END
go


create procedure dbo.spweb_pxjzReport_fail_chart @testcode varchar(5000), --有权限的实验室id集合
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30) as
BEGIN

 	CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount FLOAT,
	pxCount FLOAT,
	frequency FLOAT,
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty FLOAT
	)
 
 CREATE TABLE #t1
 (
 ModelIndex VARCHAR(50),
 ModelName VARCHAR(50),
 TestRoomCode VARCHAR(50),
 ZjCount FLOAT,
 JzCount FLOAT,
 JLCompanyName VARCHAR(50)
 )

DECLARE @sqls NVARCHAR(4000)

IF	@testcode!=''
BEGIN
	SET @sqls=' INSERT #t1
			 ( ModelIndex ,
			   ModelName ,
			   TestRoomCode ,
			   ZjCount ,
			   JzCount ,
			   JLCompanyName
			 )
		SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND LEFT(TestRoomCode,12) IN ('+@testcode+') GROUP BY TestRoomCode,ModelIndex'


		

		EXEC	sp_executesql @sqls
			DELETE #t1 WHERE ModelIndex NOT IN 
    (SELECT DISTINCT ModelIndex FROM sys_biz_reminder_Itemfrequency WHERE IsActive=1 AND FrequencyType=1)
	END
    


INSERT #tmp1
        ( 
          modelName ,
          zjCount ,
          pxCount ,
          jl ,
          modelID ,
          testroomID 
        )	
 SELECT ModelName,ZjCount, dbo.Fweb_ReturnPXCount(ModelIndex,TestRoomCode,@startdate,@enddate),JLCompanyName,ModelIndex,TestRoomCode FROM #t1

 
 
	 SELECT  
           a.modelName ,
             condition ,
           zjCount ,
           pxCount ,
          ROUND((pxCount/zjCount)*100,2) AS frequency ,
           result ,
           b.标段名称 segment ,
           jl ,
           b.单位名称 sg ,
           b.试验室名称 testroom ,
           modelID ,
           a.testroomID ,
           pxqulifty INTO #t3 FROM #tmp1 a JOIN dbo.v_bs_codeName b
		   ON a.testroomID=b.试验室编码
		     ORDER BY segment,sg,testroom



		   TRUNCATE TABLE #tmp1
		   INSERT #tmp1
		           ( 
		             modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty
		           )SELECT * FROM #t3  


	 UPDATE #tmp1 SET condition=(b.Frequency*100) FROM #tmp1 a JOIN sys_biz_reminder_Itemfrequency b
			 ON a.modelID=b.ModelIndex AND a.testroomID=b.TestRoomCode AND b.IsActive=1  AND b.FrequencyType=1



			 DELETE #tmp1 WHERE condition<frequency
			 

			 IF	@ftype=1
			 BEGIN
				SELECT MAX(testroom) AS result,COUNT(1) counts,testroomID,'' AS modelid,'1' AS stype FROM #tmp1 GROUP BY testroomID
			END           
			IF @ftype=2
			BEGIN
			 
				 UPDATE #tmp1 SET pxqulifty=dbo.Fweb_ReturnPXQualityCount(modelID,testroomID,@startdate,@enddate)

				 SELECT testroomID,(CASE WHEN pxqulifty<0 THEN '<0'
							 WHEN pxqulifty BETWEEN 0 AND 60 THEN '0-60'
							 WHEN pxqulifty BETWEEN 60 AND 80 THEN '60-80'
							 WHEN pxqulifty BETWEEN 80 AND 100 THEN '80-100'
							 WHEN pxqulifty>100 THEN '>100'	END			
							) AS result,
							(CASE WHEN pxqulifty<0 THEN '0'
							 WHEN pxqulifty BETWEEN 0 AND 60 THEN '60'
							 WHEN pxqulifty BETWEEN 60 AND 80 THEN '80'
							 WHEN pxqulifty BETWEEN 80 AND 100 THEN '100'
							 WHEN pxqulifty>100 THEN '>101'	END			
							) AS modelid
							 INTO #t5  FROM #tmp1 

						 SELECT MAX(LEFT(testroomID,12)) testroomID,result,COUNT(1) AS counts, MAX(modelid) modelid,'2' AS stype FROM #t5 GROUP BY result



			END          
END
go


create procedure dbo.spweb_pxjzReport_line_chart @testcode varchar(5000), --有权限的实验室id集合
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30) as
BEGIN

 	CREATE TABLE #tmp1
	(
	id bigint identity(1,1)  primary key,
	modelName NVARCHAR(50),
	condition FLOAT,
	zjCount FLOAT,
	pxCount FLOAT,
	frequency FLOAT,
	result VARCHAR(10),
	segment VARCHAR(100),
	jl VARCHAR(100),
	sg VARCHAR(100),
	testroom VARCHAR(100),
	modelID NVARCHAR(50),
	testroomID NVARCHAR(50),
	pxqulifty FLOAT
	)
 
 CREATE TABLE #t1
 (
 ModelIndex VARCHAR(50),
 ModelName VARCHAR(50),
 TestRoomCode VARCHAR(50),
 ZjCount FLOAT,
 JzCount FLOAT,
 JLCompanyName VARCHAR(50)
 )

DECLARE @sqls NVARCHAR(4000)

IF	@testcode!=''
BEGIN
	SET @sqls=' INSERT #t1
			 ( ModelIndex ,
			   ModelName ,
			   TestRoomCode ,
			   ZjCount ,
			   JzCount ,
			   JLCompanyName
			 )
		SELECT ModelIndex,MAX(ModelName) ModelName ,TestRoomCode,SUM(ZjCount) ZjCount,SUM(JzCount) JzCount,MAX(JLCompanyName) JLCompanyName  FROM biz_ZJ_JZ_Summary  WHERE BGRQ>'''+@startdate+''' AND BGRQ<'''+@enddate+''' AND LEFT(TestRoomCode,16) IN ('+@testcode+') GROUP BY TestRoomCode,ModelIndex'


		

		EXEC	sp_executesql @sqls
	DELETE #t1 WHERE ModelIndex NOT IN 
    (SELECT DISTINCT ModelIndex FROM sys_biz_reminder_Itemfrequency WHERE IsActive=1 AND FrequencyType=1)
	END
    


INSERT #tmp1
        ( 
          modelName ,
          zjCount ,
          pxCount ,
          jl ,
          modelID ,
          testroomID 
        )	
 SELECT ModelName,ZjCount, dbo.Fweb_ReturnPXCount(ModelIndex,TestRoomCode,@startdate,@enddate),JLCompanyName,ModelIndex,TestRoomCode FROM #t1

 
 
	 SELECT  
           a.modelName ,
             condition ,
           zjCount ,
           pxCount ,
          ROUND((pxCount/zjCount)*100,2) AS frequency ,
           result ,
           b.标段名称 segment ,
           jl ,
           b.单位名称 sg ,
           b.试验室名称 testroom ,
           modelID ,
           a.testroomID ,
           pxqulifty INTO #t3 FROM #tmp1 a JOIN dbo.v_bs_codeName b
		   ON a.testroomID=b.试验室编码
		     ORDER BY segment,sg,testroom



		   TRUNCATE TABLE #tmp1
		   INSERT #tmp1
		           ( 
		             modelName ,
		             condition ,
		             zjCount ,
		             pxCount ,
		             frequency ,
		             result ,
		             segment ,
		             jl ,
		             sg ,
		             testroom ,
		             modelID ,
		             testroomID ,
		             pxqulifty
		           )SELECT * FROM #t3  


	 UPDATE #tmp1 SET condition=(b.Frequency*100) FROM #tmp1 a JOIN sys_biz_reminder_Itemfrequency b
			 ON a.modelID=b.ModelIndex AND a.testroomID=b.TestRoomCode AND b.IsActive=1  AND b.FrequencyType=1


			 

			 IF	@ftype=1
			 BEGIN
				SELECT MAX(testroom) AS result,COUNT(1) counts,testroomID,'' AS modelid,'1' AS stype FROM #tmp1 GROUP BY testroomID
			END           
			IF @ftype=2
			BEGIN
			 
				 UPDATE #tmp1 SET pxqulifty=dbo.Fweb_ReturnPXQualityCount(modelID,testroomID,@startdate,@enddate)

				 SELECT testroomID,(CASE WHEN pxqulifty<0 THEN '<0'
							 WHEN pxqulifty BETWEEN 0 AND 60 THEN '0-60'
							 WHEN pxqulifty BETWEEN 60 AND 80 THEN '60-80'
							 WHEN pxqulifty BETWEEN 80 AND 100 THEN '80-100'
							 WHEN pxqulifty>100 THEN '>100'	END			
							) AS result,
							(CASE WHEN pxqulifty<0 THEN '0'
							 WHEN pxqulifty BETWEEN 0 AND 60 THEN '60'
							 WHEN pxqulifty BETWEEN 60 AND 80 THEN '80'
							 WHEN pxqulifty BETWEEN 80 AND 100 THEN '100'
							 WHEN pxqulifty>100 THEN '>101'	END			
							) AS modelid
							 INTO #t5  FROM #tmp1 

						 SELECT MAX(LEFT(testroomID,12)) testroomID,result,COUNT(1) AS counts, MAX(modelid) modelid,'2' AS stype FROM #t5 GROUP BY result



			END          
END
go


create procedure dbo.spweb_pxreport_Chart_pop @testcode VARCHAR(50), --标准实验室id
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30),
 	@modelID varchar(50) as
BEGIN


	
		CREATE TABLE #tmp
	(
	chartDate VARCHAR(20),
	zjCount INT,
	pxjzCount INT,	
	)
	
		CREATE TABLE #tmp1
	(
	chartDate VARCHAR(20),
	countnum INT	
	)
	
		CREATE TABLE #tmp2
	(
	chartDate VARCHAR(20),
	countnum INT	
	)
	
	CREATE TABLE #tmp3
	(
	chartDate1 VARCHAR(20),
	chartDate2 VARCHAR(20),
	zjCount INT,
	pxjzCount INT,	
	)
 
 DECLARE 

	@sqls nvarchar(4000)

		
	   SET @sqls='INSERT #tmp1
				   ( chartDate, countnum )   select REPLACE(CONVERT(varchar(12) , BGRQ, 111 ) ,''/'',''-''),count(*) from sys_document a 
	   where a.TryType=''自检'' AND a.TestRoomCode='''+@testcode+''' AND a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+''' and a.status>0 and ModuleID='''+@modelID+''' 
	   GROUP by REPLACE(CONVERT(varchar(12) , a.BGRQ, 111 )  ,''/'',''-'') ' 
	  
	   EXEC(@sqls)   
 
			 SET @sqls='INSERT #tmp2
				   ( chartDate, countnum )SELECT REPLACE(CONVERT(varchar(12) ,c.BGRQ , 111 )  ,''/'',''-''),COUNT(1)  
 FROM dbo.sys_document a JOIN dbo.sys_px_relation b ON a.ID=b.SGDataID JOIN dbo.sys_document c ON 
b.PXDataID=c.ID AND a.ModuleID=c.ModuleID AND a.ModuleID='''+@modelID+'''   AND a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''   AND c.BGRQ>='''+@startdate+''' AND c.BGRQ<'''+@enddate+'''   AND a.TestRoomCode='''+@testcode+''' and a.status>0
GROUP BY  REPLACE(CONVERT(varchar(12) ,c.BGRQ , 111 )  ,''/'',''-'') '
		   EXEC(@sqls)
		
		INSERT #tmp3
		        ( chartDate1 ,
		          chartDate2 ,
		          zjCount ,
		          pxjzCount
		        )
		SELECT a.chartDate,b.chartDate,a.countnum,b.countnum FROM #tmp1 a FULL JOIN #tmp2 b ON a.chartDate = b.chartDate
		
		UPDATE #tmp3 SET chartDate1=chartDate2 WHERE chartDate1 IS NULL
		
		
		INSERT #tmp
		        ( chartDate, zjCount, pxjzCount )
		SELECT chartDate1,zjCount,pxjzCount FROM  #tmp3

		update #tmp set zjCount=0 where zjCount is null
		update #tmp set pxjzCount=0 where pxjzCount is null
		select * from #tmp
			
END
go


create procedure dbo.spweb_qxzlhzb @testcode  VARCHAR(5000),
 	@ftype TINYINT,--没有任何意义为了能和平行见证使用同一方法，特加了一个参数
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN

   DECLARE @sqls nvarchar(4000)	
 
  create table #tmp2
 (
id bigint identity(1,1)  primary key,
project varchar(50),
segment varchar(50),
company varchar(50),
testroom varchar(50),
testname varchar(50),
ncount INT,
wncount INT,
testcode VARCHAR(50),
modelid VARCHAR(50)
 )
 

   create table #tmp1
 (
id bigint identity(1,1)  primary key,
project varchar(50),
segment varchar(50),
company varchar(50),
testroom varchar(50),
testname varchar(50),
ncount INT,
wncount INT,
testcode VARCHAR(50),
modelid VARCHAR(50)
 )


  IF @testcode!=''
  BEGIN
  
  SET @sqls='SELECT  MAX(c.工程名称) AS project,MAX(c.标段名称) AS segment,MAX(c.单位名称) AS company,MAX(c.试验室名称) AS testroom , MAX(b.Name) AS testname,COUNT(1) AS ncount,count(ind.id) as wncount,a.TestRoomCode AS testcode,a.ModuleID AS  modelid  FROM dbo.sys_document a JOIN dbo.sys_module b ON a.ModuleID = b.ID AND  b.ModuleType=1 AND  a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''  AND  TestRoomCode IN ('+@testcode+')   and a.Status>0    JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码  left join
 sys_invalid_document ind on a.id = ind.id and ind.additionalqualified=0 GROUP BY a.TestRoomCode,a.ModuleID'
   INSERT #tmp1
         ( 
           project ,
           segment ,
           company ,
           testroom ,
           testname ,
           ncount,
            wncount,
		   testcode,
		   modelid
         )
		   EXEC sp_executesql @sqls 
	

		 SET @sqls='	SELECT a.工程名称,a.标段名称,a.单位名称,a.试验室名称,b.testname,b.ncount,b.wncount,a.试验室编码,b.modelid FROM dbo.v_bs_codeName a LEFT JOIN #tmp1 b ON a.试验室编码=b.testcode  JOIN dbo.Sys_Tree c ON LEFT(a.试验室编码,12)=c.NodeCode AND a.试验室编码 IN ('+@testcode+')  ORDER BY OrderID'
	
	INSERT #tmp2
         ( 
           project ,
           segment ,
           company ,
           testroom ,
           testname ,
           ncount,
            wncount,
		   testcode,
		   modelid
         )
		    EXEC sp_executesql @sqls 



	
		 		
  END


  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp2

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 
	SET @Counts=@totalcounts

	select * from #tmp2     where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  
			
END
go


create procedure dbo.spweb_qxzlhzb_charts @testcode  VARCHAR(5000),
 	@ftype TINYINT,
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50) as
BEGIN 
 DECLARE @sqls nvarchar(4000)	

 CREATE TABLE #t0
 (
 segment NVARCHAR(50),
  company NVARCHAR(50),
   companycode NVARCHAR(50)
 )

  CREATE TABLE #t1
 (
 CompanyCode NVARCHAR(50),
  totalncount FLOAT
 )


 IF @testcode!=''
 BEGIN
		SET @sqls='  INSERT #t0
         ( segment, company, companycode ) SELECT DISTINCT MAX(标段名称) ,MAX(单位名称) , 单位编码  FROM dbo.v_bs_codeName WHERE 试验室编码 IN ('+@testcode+')  GROUP BY 单位编码 '
		  EXEC sp_executesql @sqls 
		SET @sqls='  INSERT #t1
         ( CompanyCode, totalncount ) SELECT CompanyCode,COUNT(1) totalncount   FROM dbo.sys_document a JOIN dbo.sys_module b ON a.ModuleID = b.ID AND b.ModuleType=1   AND   a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''  AND  TestRoomCode IN ('+@testcode+')  and a.Status>0   GROUP BY CompanyCode'
		  EXEC sp_executesql @sqls 

		  SELECT a.segment AS segment,a.company AS company,(CASE WHEN b.totalncount IS NULL THEN 0
																WHEN b.totalncount='' THEN 0
																ELSE   b.totalncount end ) totalncount ,a.companycode, '0' AS counts,'0' AS prenct FROM #t0 a LEFT JOIN #t1 b ON a.companycode=b.CompanyCode  JOIN  dbo.Sys_Tree c ON a.companycode=c.NodeCode ORDER BY OrderID
 END
 
 ELSE
		BEGIN
		SELECT segment , company,'0' AS totalncount,  companycode, '0' AS counts,'0' AS prenct FROM #t0
		END
        
 END
go


create procedure dbo.spweb_qxzlhzb_charts_pop @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT as
BEGIN

 DECLARE 
 
	@sqls nvarchar(4000)	

	IF (@testcode!='')
	begin
    
    SET @sqls=' SELECT  MAX(c.工程名称) AS project,MAX(c.标段名称) AS segment,MAX(c.单位名称) AS company, MAX(b.Name) AS testname,COUNT(1) AS ncount,ModuleID AS  modelid  FROM dbo.sys_document a JOIN dbo.sys_module b ON a.ModuleID = b.ID AND b.ModuleType=1 AND    a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''  AND  TestRoomCode IN ('+@testcode+')  and a.Status>0   JOIN  (SELECT DISTINCT  工程编码 ,
	          工程名称 ,
	          标段编码 ,
	          标段名称 ,
	          单位编码 ,
	          单位名称  FROM dbo.v_bs_codeName WHERE  试验室编码 IN ('+@testcode+')) c ON a.CompanyCode=c.单位编码  GROUP BY ModuleID  '

			      EXEC	sp_executesql @sqls

			  END
  ELSE
  BEGIN
	   SELECT  MAX(c.工程名称) AS project,MAX(c.标段名称) AS segment,MAX(c.单位名称) AS company, MAX(b.Name) AS testname,COUNT(1) AS ncount,ModuleID AS  modelid  FROM dbo.sys_document a JOIN dbo.sys_module b ON a.ModuleID = b.ID AND b.ModuleType=1 AND       a.Status>0   JOIN  (SELECT DISTINCT  工程编码 ,
	          工程名称 ,
	          标段编码 ,
	          标段名称 ,
	          单位编码 ,
	          单位名称  FROM dbo.v_bs_codeName WHERE 1=2) c ON a.CompanyCode=c.单位编码  GROUP BY ModuleID  
  END            
	 
END
go


create procedure dbo.spweb_qxzlhzb_charts_pop_grid @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT,
 	@modelID VARCHAR(50),
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 0 output,           
 	@Counts    int = 0 output as
BEGIN
 

 create table #tmp2
 (
id bigint identity(1,1)  primary key,
project varchar(50),
segment varchar(50),
company varchar(50),
testroom varchar(50),
testcode varchar(50),
testname VARCHAR(500),
DataName VARCHAR(500),
SCTS DATETIME,
BGRQ DATETIME,
modelid VARCHAR(50),
BGBH VARCHAR(50),
WTBH VARCHAR(50)
 )

 DECLARE 
 
	@sqls nvarchar(4000)	

 IF (@testcode!='')
 BEGIN
	 SET @sqls='	  SELECT  c.工程名称 AS project,c.标段名称 AS segment,c.单位名称 AS company,c.试验室名称 AS testroom,c.试验室编码 AS testcode,b.Name AS testname,a.DataName,a.CreatedTime AS SCTS,a.BGRQ ,ModuleID AS  modelid,a.BGBH,a.WTBH   FROM dbo.sys_document a JOIN dbo.sys_module b ON   a.ModuleID = b.ID AND ModuleID='''+@modelID+''' AND   a.BGRQ>='''+@startdate+''' AND a.BGRQ<'''+@enddate+'''  AND  TestRoomCode IN ('+@testcode+')  JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码   and a.Status>0    ORDER BY a.CreatedTime'
 END
 
 ELSE
 BEGIN
 	 SELECT  c.工程名称 AS project,c.标段名称 AS segment,c.单位名称 AS company,c.试验室名称 AS testroom,c.试验室编码 AS testcode,b.Name AS testname,a.DataName,a.CreatedTime AS SCTS,a.BGRQ ,ModuleID AS  modelid,a.BGBH,a.WTBH  FROM dbo.sys_document a JOIN dbo.sys_module b ON   a.ModuleID = b.ID AND 1=2  JOIN dbo.v_bs_codeName c ON a.TestRoomCode=c.试验室编码   and a.Status>0    ORDER BY a.CreatedTime
 END


 INSERT #tmp2
         (  
           project ,
           segment ,
           company ,
           testroom ,
           testcode ,
           testname ,
           DataName ,
           SCTS ,
		   BGRQ,
           modelid,BGBH,WTBH
         )      EXEC	sp_executesql @sqls
	

		   
		 
  declare @totalcounts int
  select @totalcounts=count(ID) from #tmp2

    --取得分页总数

	  declare @pageIndex INT --总数/页大小
    declare @lastcount INT --总数%页大小  


	SET @pageIndex=0
	SET @lastcount=0

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex  
		SET @Counts=@totalcounts
	select * from #tmp2 where id>(@pageSize*(@page-1)) and id<=(@pageSize*(@page))  ORDER BY SCTS ASC
END
go


create procedure dbo.spweb_qxzlhzb_fail_charts @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT as
BEGIN
 
 DECLARE @sqls nvarchar(4000)	
 
	 CREATE TABLE #t1
	 (
	 modelID VARCHAR(50),
	 NodeCode VARCHAR(50)
	 )

  IF @testcode!='' 
			BEGIN
					SET @sqls='INSERT #t1 ( modelID, NodeCode ) SELECT  DISTINCT b.ID AS modelID,LEFT(a.NodeCode,16) NodeCode   FROM sys_engs_Tree  a JOIN sys_biz_module b ON a.RalationID=b.ID  AND LEFT(a.NodeCode,16) IN ('+@testcode+')  ORDER BY LEFT(a.NodeCode,16)'
				 EXEC	sp_executesql @sqls
			END
	

			
END
go


create procedure dbo.spweb_qxzlhzb_jqgrid_pop @testcode VARCHAR(50), --标准实验室id
 	@ftype TINYINT, -- 频率类型1=平行，2=见证
 	@startdate varchar(30),
 	@enddate varchar(30),
 	@modelID varchar(50) as
BEGIN


	 DECLARE  
	@sqls nvarchar(4000)

	 IF	@testcode!=''
	 BEGIN
	   
	   SELECT count(1) as zjCount,CONVERT(varchar(100), CreatedTime, 23) as chartDate  FROM   dbo.sys_document WHERE ModuleID=@modelID AND TestRoomCode=@testcode AND BGRQ>=@startdate AND BGRQ<@enddate AND Status>0   GROUP BY CONVERT(varchar(100), CreatedTime, 23)  
	   	  EXEC	sp_executesql @sqls   
	END   
  ELSE
	  BEGIN
	select '' as zjCount,'' as chartDate WHERE 1=0
	  END			
END
go


create procedure dbo.spweb_tqdreport as
BEGIN

 DROP TABLE [biz_tongqiangdu]

 CREATE TABLE [dbo].[biz_tongqiangdu](
 ModelID NVARCHAR(50),
  ModelName NVARCHAR(50),
  OrderID INT,
	[ID] [nvarchar](50) NULL,
	[cType] [nvarchar](80) NULL,
	[sDate] DATETIME,
	sZJRQ DATETIME,
	[sPlace] [nvarchar](4000) NULL,
	[sValue1] [nvarchar](80) NULL,
	[sValue2] [nvarchar](80) NULL,
	[sValue3] [nvarchar](80) NULL,
	[sValue4] [nvarchar](80) NULL,
	[sAge] NVARCHAR(50),
	[sTestCode] [varchar](50) NULL,
	[segment] [varchar](50) NULL,
	[company] [varchar](50) NULL,
	[testroom] [varchar](50) NULL
)  

 
    
	--SELECT * FROM dbo.sys_module WHERE ID='05d0d71b-def3-42ee-a16a-79b34de97e9b'---混凝土检查试件抗压强度试验报告
	--SELECT * FROM dbo.sys_module WHERE ID='a974b39b-ec88-4917-a1d5-f8fbbfbb1f7a'---混凝土检查试件抗压强度试验报告（28d喷射）
	--SELECT * FROM dbo.sys_module WHERE ID='f34c2b8b-ddbe-4c04-bd01-f08b0f479ae8'---混凝土检查试件抗压强度试验报告三级配
	--SELECT * FROM dbo.sys_module WHERE ID='4500603a-5be7-4574-bbba-77b4626a3ea1'---混凝土检查试件抗压强度试验报告（梁场）


	CREATE TABLE #t1
	(
	ID NVARCHAR(50),
	ModelID NVARCHAR(50),
	ModelName NVARCHAR(50),
	OrderID INT

	)

 
 

 	INSERT #t1     ( ID, ModelID, ModelName, OrderID ) select ID,'05d0d71b-def3-42ee-a16a-79b34de97e9b','混凝土检查试件抗压强度试验报告',1  from dbo.sys_document WHERE ModuleID='05d0d71b-def3-42ee-a16a-79b34de97e9b'  -----混凝土检查试件抗压强度试验报告
 	INSERT #t1     ( ID, ModelID, ModelName, OrderID ) select ID,'A974B39B-EC88-4917-A1D5-F8FBBFBB1F7A','混凝土检查试件抗压强度试验报告（28d喷射）',3  from  dbo.sys_document WHERE ModuleID='A974B39B-EC88-4917-A1D5-F8FBBFBB1F7A'   -----混凝土检查试件抗压强度试验报告（28d喷射）
 	INSERT #t1     ( ID, ModelID, ModelName, OrderID ) select ID,'F34C2B8B-DDBE-4C04-BD01-F08B0F479AE8','混凝土检查试件抗压强度试验报告三级配',2  from  dbo.sys_document WHERE ModuleID='F34C2B8B-DDBE-4C04-BD01-F08B0F479AE8'  ---混凝土检查试件抗压强度试验报告三级配
 	INSERT #t1     ( ID, ModelID, ModelName, OrderID ) select ID,'4500603A-5BE7-4574-BBBA-77B4626A3EA1','混凝土检查试件抗压强度试验报告（梁场）',4  from  dbo.sys_document WHERE ModuleID='4500603A-5BE7-4574-BBBA-77B4626A3EA1'    -----混凝土检查试件抗压强度试验报告（梁场）


 DELETE dbo.biz_tongqiangdu


	
INSERT dbo.biz_tongqiangdu
        (
		ModelID,ModelName,OrderID, ID ,
          cType ,---强度等级
          sDate ,---报告日期
		  sZJRQ,---制件日期
          sPlace ,---施工部位
          sValue1 ,---组值1
          sValue2 ,---组值2
          sValue3 ,---组值3
          sValue4 ,---组值4
		  sAge,
          sTestCode
        )
SELECT b.ModelID,b.ModelName,b.OrderID,b.ID,a.QDDJ, CASE ISDATE(a.BGRQ) WHEN 1 THEN a.BGRQ WHEN 0 THEN NULL END ,CASE ISDATE(a.Ext7) WHEN 1 THEN a.Ext7 WHEN 0 THEN NULL END,a.Ext1,a.Ext2,a.Ext3,a.Ext4,a.Ext5,a.Ext6,a.TestRoomCode FROM dbo.sys_document a JOIN #t1 b ON a.ID = b.ID 



DELETE biz_tongqiangdu WHERE ISNUMERIC(sValue1)=0    AND ISNUMERIC(sValue2)=0  AND ISNUMERIC(sValue3)=0  AND ISNUMERIC(sValue4)=0  



		
DELETE biz_tongqiangdu WHERE  cType='/' 
DELETE biz_tongqiangdu WHERE   LEN(cType)<1
DELETE biz_tongqiangdu WHERE   cType IS NULL
DELETE biz_tongqiangdu WHERE cType NOT LIKE 'C%'
 





UPDATE dbo.biz_tongqiangdu SET segment=b.标段名称,company=b.单位名称,testroom=b.试验室名称 FROM dbo.biz_tongqiangdu a JOIN dbo.v_bs_codeName b ON a.sTestCode=b.试验室编码 

  
  

			
END
go


create procedure dbo.spweb_userSummary @testcode varchar(5000), --为空
 	@ftype TINYINT, -- 为空
 	@startdate varchar(30),--为空
 	@enddate varchar(30),--为空
 	@pageSize int=10,
 	@page        int, 
 	@fldSort    varchar(200) = null,    ----排序字段列表或条件
 	@Sort        bit = 0,    
 	@pageCount    int = 1 output,           
 	@Counts    int = 1 output as
BEGIN
	 DECLARE  
	@sqls nvarchar(4000)

		CREATE TABLE #tmp
		(
		id bigint identity(1,1)  primary key,
		userid VARCHAR(50),
		 试验室编码  varchar(36),
		 标段名称  nvarchar(50),
		 单位名称  nvarchar(50),
		 试验室名称  nvarchar(50),
		 姓名  nvarchar(80),
		 性别  nvarchar(80),
		 年龄  nvarchar(80),
		 技术职称  nvarchar(80),
		 职务  nvarchar(80),
		 工作年限  nvarchar(80),
		 联系电话  nvarchar(80),
		 学历  nvarchar(80),
		 毕业学校  nvarchar(80),
		 专业  nvarchar(80),
		num  int
		) 

IF	@testcode!=''
		BEGIN

		SET @sqls='SELECT ID,TestRoomCode,b.标段名称,b.单位名称, b.试验室名称  ,Ext1,Ext2,Ext3,Ext4,Ext5,Ext6,Ext7,Ext8,Ext9,Ext10,1  FROM dbo.sys_document a JOIN dbo.v_bs_codeName b ON a.ModuleID=''08899BA2-CC88-403E-9182-3EF73F5FB0CE''   and a.Status>0    AND  TestRoomCode IN ('+@testcode+') AND a.TestRoomCode=b.试验室编码  JOIN dbo.Sys_Tree c ON LEFT(a.TestRoomCode,12)=c.NodeCode ORDER BY OrderID '


			INSERT #tmp
					(  
					  userid ,
					  试验室编码 ,
					  标段名称 ,
					  单位名称 ,
					  试验室名称 ,
					  姓名 ,
					  性别 ,
					  年龄 ,
					  技术职称 ,
					  职务 ,
					  工作年限 ,
					  联系电话 ,
					  学历 ,
					  毕业学校 ,
					  专业 ,
					  num
					)
					  EXEC sp_executesql @sqls 
		
 
		END
  declare @totalcounts int
  select @totalcounts=count(id) from #tmp

    --取得分页总数

	  declare @pageIndex int --总数/页大小
    declare @lastcount int --总数%页大小  

	 set @pageIndex = @totalcounts/@pageSize
    set @lastcount = @totalcounts%@pageSize
    if @lastcount > 0
        set @pageCount = @pageIndex + 1
    else
        set @pageCount = @pageIndex 


		SET @Counts=@totalcounts

	select * from #tmp  where id>(@pageSize*(@page-1)) and id<=(@pageSize*@page)  

END
go


create procedure dbo.spweb_userSummary_chart @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT as
BEGIN

 DECLARE @sqls nvarchar(4000)	


 CREATE TABLE #t1
 (
 companyname NVARCHAR(50),
 companycode NVARCHAR(50),
 ncount NVARCHAR(50),
 segment NVARCHAR(50)
 )

 IF @testcode!=''
 BEGIN
 
	 	 	 	 	 	SET @sqls='
		  INSERT #t1
				 ( companyname ,
				   companycode ,
				   ncount ,
				   segment
				 )
		 SELECT  DISTINCT MAX(b.单位名称) AS companyname, MAX(b.单位编码) AS companycode, COUNT(1) AS ncount, MAX(b.标段名称) AS segment FROM dbo.sys_document a JOIN dbo.v_bs_codeName b ON a.ModuleID=''08899BA2-CC88-403E-9182-3EF73F5FB0CE''   AND  TestRoomCode IN ('+@testcode+') AND a.TestRoomCode=b.试验室编码   and a.Status>0  GROUP BY CompanyCode'
					   EXEC sp_executesql @sqls 

						CREATE TABLE #t2
 (
 companyname NVARCHAR(50),
 companycode NVARCHAR(50),
 segment NVARCHAR(50)
 )

			  	  		 SET @sqls=' INSERT #t2
		           ( companyname, companycode, segment )
		   SELECT DISTINCT  max(单位名称),单位编码,max(标段名称) FROM dbo.v_bs_codeName a JOIN #t1 b ON a.试验室编码 IN ('+@testcode+') GROUP BY 单位编码'

						  EXEC sp_executesql @sqls 

						  SELECT a.companyname,a.companycode,(CASE WHEN b.ncount IS NULL THEN 0
															WHEN b.ncount='' THEN 0
															ELSE b.ncount	END) as ncount
						  ,a.segment FROM #t2 a LEFT JOIN #t1 b ON a.companycode = b.companycode JOIN dbo.Sys_Tree c ON a.companycode=c.NodeCode ORDER BY OrderID
			   
   END
  
  ELSE
  BEGIN
  		SELECT * FROM #t1
  END
 
  
   
END
go


create procedure dbo.spweb_userSummary_chart_pop @testcode  VARCHAR(5000),
 	@startdate VARCHAR(50),
 	@enddate VARCHAR(50),
 	@ftype TINYINT as
BEGIN

DECLARE @sqls NVARCHAR(4000)

		CREATE TABLE #tmp
		(
		id bigint identity(1,1)  primary key,
		 testname  nvarchar(50),
		 Testcode NVARCHAR(50),
		 Companycode NVARCHAR(50),
		 company NVARCHAR(50),
		 ncount INT
		 
		) 
 
 IF	@ftype=1  --按照是实验室分类统计
 BEGIN
 

 SET @sqls='  SELECT MAX(b.试验室名称),b.试验室编码,MAX(b.单位编码),MAX(b.单位名称),COUNT(1) FROM dbo.sys_document a JOIN dbo.v_bs_codeName b ON a.TestRoomCode=b.试验室编码 AND TestRoomCode IN ('+@testcode+') and  a.ModuleID=''08899BA2-CC88-403E-9182-3EF73F5FB0CE''   and a.Status>0   GROUP BY b.试验室编码'
		 INSERT #tmp
				 ( 
				   testname ,
				   Testcode ,
				   Companycode ,
				   company,
				   ncount
				 )		EXEC	sp_executesql @sqls
		 
 END
 
  IF	@ftype=2  --按照是职称分类统计
 BEGIN
 


  
 
 SET @sqls='SELECT a.Ext4, MAX(b.试验室编码),MAX(b.单位编码),MAX(b.单位名称),COUNT(1) FROM dbo.sys_document a JOIN dbo.v_bs_codeName b ON a.TestRoomCode=b.试验室编码 AND TestRoomCode IN ('+@testcode+') and  a.ModuleID=''08899BA2-CC88-403E-9182-3EF73F5FB0CE''   and a.Status>0   GROUP BY a.Ext4'
		 INSERT #tmp
				 ( 
				   testname ,
				   Testcode ,
				   Companycode ,
				   company,
				   ncount
				 )
				 EXEC	sp_executesql @sqls
		 
 END
 
   IF	@ftype=3  --按照是学历分类统计
 BEGIN
 


 SET @sqls='  SELECT a.Ext8, MAX(b.试验室编码),MAX(b.单位编码),MAX(b.单位名称),COUNT(1) FROM dbo.sys_document a JOIN dbo.v_bs_codeName b ON a.TestRoomCode=b.试验室编码 AND TestRoomCode IN ('+@testcode+') and  a.ModuleID=''08899BA2-CC88-403E-9182-3EF73F5FB0CE''   and a.Status>0    GROUP BY a.Ext8'
		 INSERT #tmp
				 ( 
				   testname ,
				   Testcode ,
				   Companycode ,
				   company,
				   ncount
				 ) EXEC	sp_executesql @sqls
		
 END
 
    IF	@ftype=4  --按照是工作年限分类统计
 BEGIN
 
   CREATE TABLE #t5 (testname VARCHAR(50),
		  Testcode VARCHAR(50),
		  Companycode VARCHAR(50),
		  company VARCHAR(50),
		  TYPE INT)  


		SET @sqls='INSERT #t5
		        ( testname ,
		          Testcode ,
		          Companycode ,
		          company ,
		          TYPE
		        )
		 SELECT a.Ext6, b.试验室编码,b.单位编码,b.单位名称, (CASE WHEN Ext6>=1 AND Ext6<5 THEN 1
							 WHEN Ext6>=5 AND Ext6<10 THEN 5
							 WHEN Ext6>=10 AND Ext6<15 THEN 10
							 WHEN Ext6>=15 AND Ext6<20 THEN 15
							 WHEN Ext6>=20 THEN 20
							 ELSE 0	END			
							) 		   
		    FROM dbo.sys_document a JOIN dbo.v_bs_codeName b ON a.TestRoomCode=b.试验室编码 AND TestRoomCode IN ('+@testcode+') and  a.ModuleID=''08899BA2-CC88-403E-9182-3EF73F5FB0CE''   and a.Status>0    '


	 


		  


		EXEC	sp_executesql @sqls

		    INSERT #tmp
				 ( 
				   testname ,
				   Testcode ,
				   Companycode ,
				   company,
				   ncount
				 )
				 SELECT (CASE WHEN type=1 THEN '1-5年'
							  WHEN type=5 THEN '5-10年'
							 WHEN type=10 THEN '10-15年'
							  WHEN type=15 THEN '15-20年'
							 WHEN type=20 THEN '20年以上'
							 ELSE '其他'  END			
							), MAX(Testcode) Testcode,MAX(Companycode) Companycode,MAX(company) company,COUNT(1) ncount FROM #t5 GROUP BY type

				 


 END

	SELECT * FROM #tmp
END
go


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yqun.Bases.ControlBases;
using IS.DNS.WinUI.DataExpress.NavBar;
using Yqun.Interfaces;
using BizCommon.Properties;
using Yqun.Permissions.Runtime;
using YqunMainAppBase;
using Yqun.MainUI.Properties;
using BizCommon;
using Yqun.Services;
using Yqun.Bases;

namespace Yqun.MainUI
{
    public partial class NavBarExplorer : ControlBase
    {
        public object obj;
        private ImageList ImageLt = new ImageList();
        private NavBarControl NavBar_MainNorm;
        private YqunMessage msg = new YqunMessage();
        private string ControlKey;
        private string ControlDescription;

        private bool _IsRunTime = false;
        public bool IsRunTime
        {
            get { return _IsRunTime; }
            set
            {
                _IsRunTime = value;
            }
        }

        public NavBarExplorer()
        {
            InitializeComponent();

            SetImageList();
            LoadTopNodeTab();
        }

        /// <summary>
        /// 设置图标
        /// </summary>
        void SetImageList()
        {
            ImageLt.ColorDepth = ColorDepth.Depth32Bit;
            ImageLt.ImageSize = new Size(16, 16);

            ImageLt.Images.Add(Resources.业务管理);
            ImageLt.Images.Add(Resources.组织结构);
            ImageLt.Images.Add(Resources.权限管理);
        }

        private void LoadTopNodeTab()
        {
            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select code,name,ofsolution From sys_solforldr ");
            Sql_Select.Append("Where datalength(CODE)=4 order by OrderIndex");
            DataTable dt = Agent.CallRemoteService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (dt != null)
            {
                foreach (DataRow Row in dt.Rows)
                {
                    int i = dt.Rows.IndexOf(Row);
                    String Code = Row["code"].ToString();
                    String OfSolution = Row["ofsolution"].ToString();
                    if (AuthManager.GetModuleFolder(Code, OfSolution))
                    {
                        Yqun.UI.Controls.YaTabPage tabPage = new Yqun.UI.Controls.YaTabPage();
                        tabPage.Dock = System.Windows.Forms.DockStyle.Fill;
                        tabPage.ImageIndex = -1;
                        tabPage.Location = new System.Drawing.Point(29, 4);
                        tabPage.Name = "tabPage" + i.ToString();
                        tabPage.Tag = Code + "&" + OfSolution;
                        tabPage.Size = new System.Drawing.Size(181, 454);
                        tabPage.TabIndex = 0;
                        tabPage.Text = Row["name"].ToString();

                        if (!this.Main_TabControl.Controls.ContainsKey(tabPage.Name))
                            this.Main_TabControl.Controls.Add(tabPage);
                    }
                }
            }
        }

        private void LoadNormBar(string Code, string OfSolution)
        {
            NavBar_MainNorm = new NavBarControl();
            NavBar_MainNorm.Dock = DockStyle.Fill;

            this.Main_TabControl.SelectedTab.Controls.Add(NavBar_MainNorm);

            LoadContent(Code, OfSolution);
        }

        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="navBarGroup">群</param>
        /// <param name="FolderCode">所在文件夹编码</param>
        /// <param name="OfSolution">所在解决方案ID</param>
        /// <param name="IsSub">是否子文件夹</param>
        private void LoadContent(string FolderCode, string OfSolution)
        {
            NavBarGroup navBarGroup = new NavBarGroup();
            string Tag = this.Main_TabControl.SelectedTab.Tag.ToString();
            navBarGroup.Name = Tag.Substring(0, Tag.IndexOf("&")).Trim();
            navBarGroup.Hint = OfSolution;
            navBarGroup.Caption = this.Main_TabControl.SelectedTab.Text;

            navBarGroup.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavBar_MainNorm.Groups.Add(navBarGroup);

            StringBuilder Sql_Select = new StringBuilder();
            Sql_Select.Append("Select ID,TYPEFALG,DESCRIPTION,ORDERINDEX From SYS_SOLCONTENT Where OFFOLDER='");
            Sql_Select.Append(FolderCode);
            Sql_Select.Append("' And OFSOLUTION='");
            Sql_Select.Append(OfSolution);
            Sql_Select.Append("' Order By OrderIndex");

            DataTable Data = Agent.CallService("Yqun.BO.LoginBO.dll", "GetDataTable", new object[] { Sql_Select.ToString() }) as DataTable;
            if (Data != null)
            {
                foreach (DataRow r in Data.Rows)
                {
                    string ID = r["ID"].ToString();
                    string TYPEFALG = r["TYPEFALG"].ToString();
                    string DESCRIPTION = r["DESCRIPTION"].ToString();

                    if (AuthManager.GetModuleAuth(ID))
                    {
                        Selection sele = new Selection();
                        sele.ID = ID;
                        sele.Text = DESCRIPTION;
                        sele.Description = DESCRIPTION;
                        sele.Value = TYPEFALG;

                        NavBarItem item = new NavBarItem();

                        item.LinkClicked += new NavBarLinkEventHandler(item_LinkClicked);
                        item.Caption = DESCRIPTION;
                        item.Name = ID;

                        item.ImageAlign = ContentAlignment.MiddleLeft;
                        int Index = string.IsNullOrEmpty(r["ORDERINDEX"].ToString()) ? 0 : int.Parse(r["ORDERINDEX"].ToString());

                        if (this.ImageLt.Images.Count > Index)
                        {
                            item.SmallImage = this.ImageLt.Images[Index];
                        }

                        item.Tag = sele;
                        navBarGroup.ItemLinks.Add(item);
                    }
                }
            }

            navBarGroup.Expanded = true;
        }


        void item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            NavBarItem Item = sender as NavBarItem;
            Selection sele = Item.Tag as Selection;
            if (sele == null)
                return;
            ExecuteOpen(sele);
        }

        /// <summary>
        /// 打开函数
        /// </summary>
        private void ExecuteOpen(Selection sele)
        {
            ControlKey = sele.ID; 
            ControlDescription = sele.Description;
            msg.TypeFlag = "@OPEN";
            msg.BizID = sele.ID;

            ControlBase CtrBase = Yqun.Services.Agent.OpenRuntime(sele.ID) as ControlBase;
            if (obj != null && CtrBase != null)
            {
                MainForm main = obj as MainForm;
                main.AddControl(ControlKey, ControlDescription, null, CtrBase, Yqun.Interfaces.LayoutDockType.Middle, null);
                CtrBase.InvokeMessage(this.msg);
            }
        }

        private void Main_TabControl_TabChanged(object sender, EventArgs e)
        {
            ActionActiveTab();
        }

        private void NavBarExplorer_Load(object sender, EventArgs e)
        {
            ActionActiveTab();
        }

        private void ActionActiveTab()
        {
            if (this.Main_TabControl.SelectedTab != null)
            {
                string Tag = this.Main_TabControl.SelectedTab.Tag.ToString();
                if (!string.IsNullOrEmpty(Tag) && Tag.Contains("&"))
                {
                    string Code = Tag.Substring(0, Tag.IndexOf("&")).Trim();
                    string OfSolution = Tag.Substring(Tag.IndexOf("&") + 1).Trim();
                    LoadNormBar(Code, OfSolution);
                }
            }
        }
    }
}

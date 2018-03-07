using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Yqun.Services;
using Yqun.BO;
using BizCommon;

namespace ShuXianCaiJiComponents
{
    /// <summary>
    /// 
    /// </summary>
    public class CaijiKeyHelper 
    {
        /// <summary>
        /// 初始绑定TreeView
        /// </summary>
        /// <param name="View"></param>
        public static void Init(TreeView View)
        {
            View.Nodes.Clear();

            TreeNode TopNode = new TreeNode();
            TopNode.Name = "";
            TopNode.Text = "组织结构";

            Selection TopSelection = new Selection();
            TopSelection.Value = "true";
            TopNode.Tag = TopSelection;
            TopNode.ImageIndex = 0;
            TopNode.SelectedImageIndex = 0;
            View.Nodes.Add(TopNode);

            DataTable Data = Agent.CallService("Yqun.BO.PermissionManager.dll", "InitOrganization", new object[] { }) as DataTable;
            if (Data != null && Data.Rows.Count > 0)
            {
                foreach (DataRow row in Data.Rows)
                {
                    string Code = row["NodeCode"].ToString();
                    string Name = row["RalationText"].ToString();
                    string Tag = row["NodeType"].ToString();
                    string RalationID = row["RalationID"].ToString();
                    string IsNode = row["IsNode"].ToString();

                    int ImageIndex = 0;
                    int SelectedImageIndex = 0;
                    string ParentCode = "";

                    try
                    {
                        ParentCode = Code.Substring(0, Code.Length - 4);
                    }
                    catch
                    { }

                    TreeNode[] Nodes = View.Nodes.Find(ParentCode, true);
                    if (Nodes.Length > 0)
                    {
                        TreeNode ParentNode = Nodes[0];

                        TreeNode Node = new TreeNode();
                        Node.Name = Code;
                        Node.Text = Name;

                        Selection Selection = new Selection();
                        Selection.ID = RalationID;
                        Selection.Value = IsNode;
                        Selection.Tag = Tag;
                        Node.Tag = Selection;

                        switch (Tag.ToLower())
                        {
                            case "@eng":
                            case "@tenders":
                            case "@unit_施工单位":
                            case "@unit_监理单位":
                            case "@folder":
                                ImageIndex = 1;
                                SelectedImageIndex = ImageIndex;
                                break;
                            case "@user":
                                ImageIndex = 2;
                                SelectedImageIndex = ImageIndex;
                                break;
                        }

                        Node.SelectedImageIndex = SelectedImageIndex;
                        Node.ImageIndex = ImageIndex;
                        ParentNode.Nodes.Add(Node);
                    }
                }
            }

            View.SelectedNode = TopNode;
            View.ExpandAll();
        }

        /// <summary>
        /// 注册指纹
        /// </summary>
        /// <param name="BioKeyInfo"></param>
        /// <returns></returns>
        public static bool SaveRegister(BioKeyInfo BioKeyInfo)
        {
            return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "KeyReginUser", new object[] { BioKeyInfo }).ToString());

        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="_UserCode"></param>
        /// <returns></returns>
        public static bool IsExist(string _UserCode)
        {
            return bool.Parse(Agent.CallService("Yqun.BO.BusinessManager.dll", "IsExist", new object[] { _UserCode }).ToString());
        }


        public static List<BioKeyInfo> GetTemplates(String UserCode)
        {
            List<BioKeyInfo> ListBKInfo = null;
            ListBKInfo = (List<BioKeyInfo>)Agent.CallService("Yqun.BO.BusinessManager.dll", "GetTemplates", new object[] { UserCode });
            return ListBKInfo;
        }

    }

    [Serializable]
    public class Selection : ICloneable
    {
        string _TypeFlag = "";
        public string TypeFlag
        {
            get
            {
                return _TypeFlag;
            }
            set
            {
                _TypeFlag = value;
            }
        }

        string _ID = "";
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        string _Code = "";
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        string _Text = "";
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        string _Description = "";
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        string _Value = "";
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        string _Value1 = "";
        public string Value1
        {
            get
            {
                return _Value1;
            }
            set
            {
                _Value1 = value;
            }
        }

        string _Value2 = "";
        public string Value2
        {
            get
            {
                return _Value2;
            }
            set
            {
                _Value2 = value;
            }
        }

        string _Flag = "";
        public string Flag
        {
            get
            {
                return _Flag;
            }
            set
            {
                _Flag = value;
            }
        }

        object _Tag = "";
        public object Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }

        #region ICloneable 成员

        public object Clone()
        {
            Selection selection = new Selection();
            selection.TypeFlag = this.TypeFlag;
            selection.ID = this.ID;
            selection.Code = this.Code;
            selection.Text = this.Text;
            selection.Description = this.Description;
            selection.Value = this.Value;
            selection.Value1 = this.Value1;
            selection.Value2 = this.Value2;
            selection.Flag = this.Flag;
            selection.Tag = this.Tag;

            return selection;
        }

        #endregion
    }
}

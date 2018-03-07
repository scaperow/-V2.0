using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Yqun.Permissions.Common
{
    public class TreeInfo
    {
        private TreeNode m_rootNode;
        public TreeNode RootNode
        {
            get
            {
                return m_rootNode;
            }
            set
            {
                m_rootNode = value;
            }
        }

        private string m_TreeName;
        public string TreeName
        {
            get
            {
                return m_TreeName;
            }
            set
            {
                m_TreeName = value;
            }
        }

        private string m_TreeText;
        public string TreeText
        {
            get
            {
                return m_TreeText;
            }
            set
            {
                m_TreeText = value;
            }
        }
    }
}

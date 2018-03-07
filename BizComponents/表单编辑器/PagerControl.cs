using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BizComponents
{
    public partial class PagerControl : UserControl
    {
        #region 构造函数

        public PagerControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 分页字段和属性

        private int pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public virtual int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        private int pageSize = 30;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public virtual int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int recordCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public virtual int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        private int pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (pageSize != 0)
                {
                    pageCount = GetPageCount();
                }
                return pageCount;
            }
        }
        private string _lbText = @"共{0}条记录";
        public string LabelText
        {
            get
            {
                return _lbText;
            }
            set
            {
                _lbText = value;
            }
        }


        #endregion

        #region 页码变化触发事件

        public event EventHandler OnPageChanged;

        #endregion

        #region 分页及相关事件功能实现

        private void SetFormCtrEnabled()
        {
            blnkFirst.Enabled = true;
            blnkPrev.Enabled = true;
            blnkNext.Enabled = true;
            blnkLast.Enabled = true;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <returns></returns>
        private int GetPageCount()
        {
            if (PageSize == 0)
            {
                return 0;
            }
            int pageCount = RecordCount / PageSize;
            if (RecordCount % PageSize == 0)
            {
                pageCount = RecordCount / PageSize;
            }
            else
            {
                pageCount = RecordCount / PageSize + 1;
            }
            return pageCount;
        }
        /// <summary>
        /// 外部调用
        /// </summary>
        public void DrawControl(int count, int size)
        {
            recordCount = count;
            pageSize = size;
            pageCount = GetPageCount();
            DrawControl(false);
        }
        /// <summary>
        /// 页面控件呈现
        /// </summary>
        private void DrawControl(bool callEvent)
        {

            lblPageCount.Text = String.Format(@"/{0}",
            pageCount.ToString());

            if (pageCount == 0)
            {
                lblCurrentPage.Text = "0";
            }
            else
            {
                lblCurrentPage.Text = PageIndex.ToString();
            }

            lblTotalCount.Text = String.Format(_lbText, recordCount.ToString());


            //lblPageSize.Text = PageSize.ToString();

            if (callEvent && OnPageChanged != null)
            {
                OnPageChanged(this, null);//当前分页数字改变时，触发委托事件
            }
            SetFormCtrEnabled();
            if (PageCount == 1)//有且仅有一页
            {
                blnkFirst.Enabled = false;
                blnkPrev.Enabled = false;
                blnkNext.Enabled = false;
                blnkLast.Enabled = false;
            }
            else if (PageIndex == 1)//第一页
            {
                blnkFirst.Enabled = false;
                blnkPrev.Enabled = false;
            }
            else if (PageIndex == PageCount)//最后一页
            {
                blnkNext.Enabled = false;
                blnkLast.Enabled = false;
            }
        }

        #endregion




        private void blnkFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            DrawControl(true);
        }

        private void blnkPrev_Click(object sender, EventArgs e)
        {
            PageIndex = Math.Max(1, PageIndex - 1);
            DrawControl(true);
        }

        private void blnkNext_Click(object sender, EventArgs e)
        {
            PageIndex = Math.Min(PageCount, PageIndex + 1);
            DrawControl(true);
        }

        private void blnkLast_Click(object sender, EventArgs e)
        {
            PageIndex = PageCount;
            DrawControl(true);
        }


    }
}

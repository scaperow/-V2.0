using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace BizCommon
{
    public class PictureToExcel
    {
        /// <summary>       
        ///打开没有模板的操作。  
        /// </summary>    
        public void Open()
        {
            this.Open(String.Empty);
        }
        /// <summary>   
        /// 功能：实现Excel应用程序的打开   
        /// </summary>   
        /// <param name="TemplateFilePath">模板文件物理路径</param>   
        public void Open(string TemplateFilePath)
        {
            //打开对象
            m_objExcel = new Excel.Application();
            m_objExcel.Visible = false;
            m_objExcel.DisplayAlerts = false;
            //if (m_objExcel.Version != "11.0")
            //{
            //    //MessageBox.Show("您的Excel 版本不是11.0 （Office 2003），操作可能会出现问题。");
            //    m_objExcel.Quit();
            //    return;
            //}
            m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
            if (TemplateFilePath.Equals(String.Empty))
            {
                m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));
            }
            else
            {
                m_objBook = m_objBooks.Open(TemplateFilePath, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);
            }
            m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
            m_objSheet = (Excel._Worksheet)(m_objSheets.get_Item(1));
            m_objExcel.WorkbookBeforeClose += new Excel.AppEvents_WorkbookBeforeCloseEventHandler(m_objExcel_WorkbookBeforeClose);
        }


        private void m_objExcel_WorkbookBeforeClose(Excel.Workbook m_objBooks, ref bool _Cancel)
        {
            //MessageBox.Show("保存完毕！");
        }



        /// <summary>  
        /// 将图片插入到指定的单元格位置。  
        /// 注意：图片必须是绝对物理路径    /// </summary>  
        /// <param name="RangeName">单元格名称，例如：B4</param>  
        /// <param name="PicturePath">要插入图片的绝对路径。</param> 
        public void InsertPicture(string RangeName, string PicturePath)
        {
            m_objRange = m_objSheet.get_Range(RangeName, m_objOpt);
            m_objRange.Select();
            Excel.Pictures pics = (Excel.Pictures)m_objSheet.Pictures(m_objOpt);
            //pics.Insert(PicturePath, m_objOpt);
            float fL=float.Parse(m_objRange.Left.ToString());
            float fT = float.Parse(m_objRange.Top.ToString());
            float fW = float.Parse(m_objRange.Width.ToString());
            float fH = float.Parse(m_objRange.Height.ToString());
            m_objSheet.Shapes.AddPicture(PicturePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue,fL ,fT ,fW ,fH );
            //Excel.Picture pic = (Excel.Picture)pics.Item(0);//建立图片集合某一图片对象
            //pic.Left = (double)m_objRange.Left;
            //pic.Top = (double)m_objRange.Top;
            //pic.Height = (double)m_objRange.Height;
            //pic.Width = (double)m_objRange.Width;

        }



        /// <summary> 
        /// 将图片插入到指定的单元格位置，并设置图片的宽度和高度。
        /// 注意：图片必须是绝对物理路径    
        /// </summary>   
        /// <param name="RangeName">单元格名称，例如：B4</param>  
        /// <param name="PicturePath">要插入图片的绝对路径。</param>  
        /// <param name="PictuteWidth">插入后，图片在Excel中显示的宽度。</param>    
        /// <param name="PictureHeight">插入后，图片在Excel中显示的高度。</param>    
        public void InsertPicture(string RangeName, string PicturePath, float PictuteWidth, float PictureHeight)
        {
            m_objRange = m_objSheet.get_Range(RangeName, m_objOpt);
            m_objRange.Select();
            float PicLeft, PicTop;
            PicLeft = Convert.ToSingle(m_objRange.Left);
            PicTop = Convert.ToSingle(m_objRange.Top);
            //参数含义： 
            //图片路径  
            //是否链接到文件 
            //图片插入时是否随文档一起保存  
            //图片在文档中的坐标位置（单位：points） 
            //图片显示的宽度和高度（单位：points） 
            //参数详细信息参见：http://msdn2.microsoft.com/zh-cn/library/aa221765(office.11).aspx  
            m_objSheet.Shapes.AddPicture(PicturePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, PicLeft, PicTop, PictuteWidth, PictureHeight);
        }




        /// <summary>  
        /// 将Excel文件保存到指定的目录，目录必须事先存在，文件名称不一定要存在。
        /// </summary>     /// <param name="OutputFilePath">要保存成的文件的全路径。</param>  
        public void SaveFile(string OutputFilePath)
        {
            m_objBook.SaveAs(OutputFilePath, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt, Excel.XlSaveAsAccessMode.xlNoChange, m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);
            this.Close();
        }



        /// <summary>   
        /// 关闭应用程序
        /// </summary>   
        private void Close()
        {
            m_objBook.Close(false, m_objOpt, m_objOpt);
            m_objExcel.Quit();
        }



        /// <summary>  
        /// 释放所引用的COM对象。注意：这个过程一定要执行。
        /// </summary>   
        public void Dispose()
        {
            ReleaseObj(m_objSheets);
            ReleaseObj(m_objBook);
            ReleaseObj(m_objBooks);
            ReleaseObj(m_objExcel);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }



        /// <summary> 
        /// /// 释放对象，内部调用
        /// /// </summary> 
        /// /// <param name="o"></param> 

        private void ReleaseObj(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);

            }
            catch { }

            finally
            {
                o = null;
            }
        }


        private Excel.Application m_objExcel = null;
        private Excel.Workbooks m_objBooks = null;
        private Excel._Workbook m_objBook = null;
        private Excel.Sheets m_objSheets = null;
        private Excel._Worksheet m_objSheet = null;
        private Excel.Range m_objRange = null;
        private object m_objOpt = System.Reflection.Missing.Value; 
    }
}

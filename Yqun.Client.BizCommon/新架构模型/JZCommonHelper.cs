using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using FarPoint.Win.Spread;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.Office.Interop.Excel;
using System.IO.Compression;

namespace BizCommon
{
    public class JZCommonHelper
    {
        public static Boolean ExeCommand(string commandText)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            Boolean strOutput = true;
            try
            {
                p.Start();
                p.StandardInput.WriteLine(commandText);
                p.StandardInput.WriteLine("exit");
                p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            catch
            {
                strOutput = false;
            }
            return strOutput;
        }

        public static Boolean CreateZipFile(List<String> filenames, string zipFilePath)
        {
            Boolean flag = true;
            //try
            //{
            using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
            {

                s.SetLevel(9); // 压缩级别 0-9
                //s.Password = "123"; //Zip压缩文件密码
                byte[] buffer = new byte[4096]; //缓冲区大小
                foreach (string file in filenames)
                {
                    if (!File.Exists(file))
                    {
                        continue;
                    }
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);
                    using (FileStream fs = File.OpenRead(file))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
            }
            //}
            //catch
            //{
            //    flag = false;
            //}
            return flag;
        }

        public static Boolean UnZipFile(string zipFilePath)
        {
            if (!File.Exists(zipFilePath))
            {
                return false;
            }
            Boolean flag = true;
            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(zipFilePath);
                        string fileName = Path.GetFileName(theEntry.Name);

                        if (directoryName.Length > 0 && !Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(Path.Combine(directoryName, fileName)))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static Object GetCellValue(JZDocument doc, Guid sheetID, String cellName)
        {
            if (doc != null)
            {
                foreach (JZSheet sheet in doc.Sheets)
                {
                    if (sheet.ID == sheetID)
                    {
                        foreach (JZCell cell in sheet.Cells)
                        {
                            if (cell.Name == cellName)
                            {
                                return cell.Value;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static Object GetCellValueAndHasCell(JZDocument doc, Guid sheetID, String cellName, out bool HasCell)
        {
            HasCell = false;
            if (doc != null)
            {
                foreach (JZSheet sheet in doc.Sheets)
                {
                    if (sheet.ID == sheetID)
                    {
                        foreach (JZCell cell in sheet.Cells)
                        {
                            if (cell.Name == cellName)
                            {
                                HasCell = true;
                                return cell.Value;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static String BitmapToString(Bitmap image)
        {
            string bitmapString = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Jpeg);
                byte[] bitmapBytes = memoryStream.GetBuffer();
                bitmapString = System.Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);
            }
            return bitmapString;
        }

        public static Image StringToBitmap(String str)
        {
            Image img = null;
            try
            {
                byte[] bitmapBytes = System.Convert.FromBase64String(str);
                using (MemoryStream memoryStream = new MemoryStream(bitmapBytes))
                {
                    img = Image.FromStream(memoryStream);
                }
            }
            catch { }

            return img;
        }

        public static Boolean CreateReportPDFFromExcel(string excelFilePath, String pdfFilePath, Int32 reportIndex)
        {
            Boolean flag = false;

            if (!System.IO.File.Exists(excelFilePath))
            {
                return flag;
            }

            ApplicationClass excelApplication = new ApplicationClass();

            Workbook excelWorkBook = null;

            string paramSourceBookPath = excelFilePath;
            object paramMissing = Type.Missing;

            XlFixedFormatType paramExportFormat = XlFixedFormatType.xlTypePDF;

            XlFixedFormatQuality paramExportQuality = XlFixedFormatQuality.xlQualityMinimum;
            bool paramOpenAfterPublish = false;
            bool paramIncludeDocProps = true;
            bool paramIgnorePrintAreas = true;
            object paramFromPage = 1;
            object paramToPage = 1;

            try
            {
                // Open the source workbook.
                excelWorkBook = excelApplication.Workbooks.Open(paramSourceBookPath, paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing, paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing, paramMissing, paramMissing);

                // Save it in the target format.
                if (excelWorkBook != null)
                {
                    if (!Directory.Exists(Path.GetDirectoryName(pdfFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pdfFilePath));
                    }
                    Worksheet reportSheet = excelWorkBook.Sheets[reportIndex] as Worksheet;
                    if (reportSheet != null)
                    {
                        if (File.Exists(pdfFilePath))
                        {
                            File.Delete(pdfFilePath);
                        }

                        reportSheet.PageSetup.Zoom = 90;
                        reportSheet.PageSetup.LeftMargin = excelApplication.CentimetersToPoints(3);
                        reportSheet.PageSetup.RightMargin = excelApplication.CentimetersToPoints(2);
                        reportSheet.PageSetup.TopMargin = excelApplication.CentimetersToPoints(2.5);
                        reportSheet.PageSetup.BottomMargin = excelApplication.CentimetersToPoints(2.5);
                        reportSheet.ExportAsFixedFormat(paramExportFormat, pdfFilePath, paramExportQuality, paramIncludeDocProps,
                            paramIgnorePrintAreas, paramFromPage, paramToPage, paramOpenAfterPublish, paramMissing);
                    }
                }

                flag = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Close the workbook object.
                if (excelWorkBook != null)
                {
                    excelWorkBook.Close(false, paramMissing, paramMissing);
                    excelWorkBook = null;
                }

                // Close the ApplicationClass object.
                if (excelApplication != null)
                {
                    excelApplication.Quit();
                    excelApplication = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return flag;
        }

        public static Boolean FarpointToImage0(Guid documentID, FpSpread FpSpread, String path, int reportIndex)
        {
            if (FpSpread == null || FpSpread.Sheets.Count == 0)
            {
                return false;
            }
            Boolean flag = true;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //for (int i = 0; i < FpSpread.Sheets.Count; i++)
                //{
                PrintInfo Info = new PrintInfo();
                Info.CopyFrom(FpSpread.Sheets[0].PrintInfo);
                Info.ShowGrid = false;
                Bitmap img = new Bitmap(Info.PaperSize.Width, Info.PaperSize.Height);

                using (Graphics g = Graphics.FromImage(img))
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                    System.Drawing.Rectangle r = new System.Drawing.Rectangle(
                                            Info.Margin.Left,
                                            Info.Margin.Top,
                                            Info.PaperSize.Width - Info.Margin.Left - Info.Margin.Right,
                                            Info.PaperSize.Height - Info.Margin.Top - Info.Margin.Bottom
                                            );

                    int Foot, Heard;
                    Foot = (int)Math.Round(g.MeasureString(Info.Footer, FpSpread.Font).Height);
                    Heard = (int)Math.Round(g.MeasureString(Info.Header, FpSpread.Font).Height);

                    r = new System.Drawing.Rectangle(r.X, r.Y + Heard, r.Width, r.Height - Foot - Heard);
                    FpSpread.OwnerPrintDraw(g, r, 0, 1);
                }
                img.Save(Path.Combine(path, documentID.ToString() + "_" + reportIndex + ".jpg"), ImageFormat.Jpeg);
                //}
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        public static Boolean FarpointToImage(Guid documentID, FpSpread FpSpread, String path)
        {
            if (FpSpread == null || FpSpread.Sheets.Count == 0)
            {
                return false;
            }
            Boolean flag = true;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                for (int i = 0; i < FpSpread.Sheets.Count; i++)
                {
                    PrintInfo Info = new PrintInfo();
                    Info.CopyFrom(FpSpread.Sheets[i].PrintInfo);
                    Info.ShowGrid = false;
                    Bitmap img = new Bitmap(Info.PaperSize.Width, Info.PaperSize.Height);

                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                        System.Drawing.Rectangle r = new System.Drawing.Rectangle(
                                                Info.Margin.Left,
                                                Info.Margin.Top,
                                                Info.PaperSize.Width - Info.Margin.Left - Info.Margin.Right,
                                                Info.PaperSize.Height - Info.Margin.Top - Info.Margin.Bottom
                                                );

                        int Foot, Heard;
                        Foot = (int)Math.Round(g.MeasureString(Info.Footer, FpSpread.Font).Height);
                        Heard = (int)Math.Round(g.MeasureString(Info.Header, FpSpread.Font).Height);

                        r = new System.Drawing.Rectangle(r.X, r.Y + Heard, r.Width, r.Height - Foot - Heard);
                        FpSpread.OwnerPrintDraw(g, r, i, 1);
                    }
                    img.Save(Path.Combine(path, documentID.ToString() + "_" + i + ".jpg"), ImageFormat.Jpeg);
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static Boolean SaveToExcel(Guid documentID, FpSpread fpSpread, String path)
        {
            if (fpSpread == null || fpSpread.Sheets.Count == 0)
            {
                return false;
            }
            Boolean flag = false;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                String filePath = Path.Combine(path, documentID.ToString() + ".xls");
                fpSpread.SaveExcel(filePath, FarPoint.Excel.ExcelSaveFlags.NoFormulas | FarPoint.Excel.ExcelSaveFlags.SaveAsViewed);
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        public static Boolean FarpointToPDF0(FpSpread fpSpread, String pdfFile, Int32 reportIndex)
        {
            Boolean flag = false;
            if (fpSpread == null || fpSpread.Sheets.Count == 0)
            {
                return flag;
            }
            PrintInfo Info = new PrintInfo();
            Info.CopyFrom(fpSpread.Sheets[0].PrintInfo);
            Info.ShowGrid = false;
            Bitmap Panel = new Bitmap(Info.PaperSize.Width, Info.PaperSize.Height);

            using (Graphics g = Graphics.FromImage(Panel))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                System.Drawing.Rectangle r = new System.Drawing.Rectangle(
                                        Info.Margin.Left,
                                        Info.Margin.Top,
                                        Info.PaperSize.Width - Info.Margin.Left - Info.Margin.Right,
                                        Info.PaperSize.Height - Info.Margin.Top - Info.Margin.Bottom
                                        );

                int Foot, Heard;
                Foot = (int)Math.Round(g.MeasureString(Info.Footer, fpSpread.Font).Height);
                Heard = (int)Math.Round(g.MeasureString(Info.Header, fpSpread.Font).Height);

                r = new System.Drawing.Rectangle(r.X, r.Y + Heard, r.Width, r.Height - Foot - Heard);
                fpSpread.OwnerPrintDraw(g, r, 0, 1);
            }
            Info = null;
            PdfDocument doc = new PdfDocument();
            doc.Pages.Add(new PdfPage());
            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
            XImage img = XImage.FromGdiPlusImage(Panel);

            xgr.DrawImage(img, 0, 0);

            doc.Save(pdfFile);
            doc.Close();
            xgr.Dispose();
            img.Dispose();
            doc.Dispose();
            Panel.Dispose();
            GC.Collect();
            flag = true;
            return flag;
        }
        public static Boolean FarpointToPDF(FpSpread fpSpread, String pdfFile, Int32 reportIndex)
        {
            Boolean flag = false;
            if (fpSpread == null || fpSpread.Sheets.Count == 0)
            {
                return flag;
            }
            PrintInfo Info = new PrintInfo();
            Info.CopyFrom(fpSpread.Sheets[reportIndex].PrintInfo);
            Info.ShowGrid = false;
            Bitmap Panel = new Bitmap(Info.PaperSize.Width, Info.PaperSize.Height);

            using (Graphics g = Graphics.FromImage(Panel))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                System.Drawing.Rectangle r = new System.Drawing.Rectangle(
                                        Info.Margin.Left,
                                        Info.Margin.Top,
                                        Info.PaperSize.Width - Info.Margin.Left - Info.Margin.Right,
                                        Info.PaperSize.Height - Info.Margin.Top - Info.Margin.Bottom
                                        );

                int Foot, Heard;
                Foot = (int)Math.Round(g.MeasureString(Info.Footer, fpSpread.Font).Height);
                Heard = (int)Math.Round(g.MeasureString(Info.Header, fpSpread.Font).Height);

                r = new System.Drawing.Rectangle(r.X, r.Y + Heard, r.Width, r.Height - Foot - Heard);
                fpSpread.OwnerPrintDraw(g, r, reportIndex, 1);
            }

            PdfDocument doc = new PdfDocument();
            doc.Pages.Add(new PdfPage());
            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
            XImage img = XImage.FromGdiPlusImage(Panel);

            xgr.DrawImage(img, 0, 0);

            doc.Save(pdfFile);
            doc.Close();

            Panel.Dispose();
            flag = true;
            return flag;
        }

        /// <summary>
        /// 将传入字符串以GZip算法压缩后，返回Base64编码字符
        /// </summary>
        /// <param name="rawString">需要压缩的字符串</param>
        /// <returns>压缩后的Base64编码的字符串</returns>
        public static string GZipCompressString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rawString.ToString());
                byte[] zippedData = Compress(rawData);
                return (string)(Convert.ToBase64String(zippedData));
            }

        }
        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        private static byte[] Compress(byte[] rawData)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(rawData, 0, rawData.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// 将传入的二进制字符串资料以GZip算法解压缩
        /// </summary>
        /// <param name="zippedString">经GZip压缩后的二进制字符串</param>
        /// <returns>原始未压缩字符串</returns>
        public static string GZipDecompressString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] zippedData = Convert.FromBase64String(zippedString.ToString());
                return (string)(System.Text.Encoding.UTF8.GetString(Decompress(zippedData)));
            }
        }
        /// <summary>
        /// ZIP解压
        /// </summary>
        /// <param name="zippedData"></param>
        /// <returns></returns>
        private static byte[] Decompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <param name="Mode">保留着，暂时未用</param>
        /// <returns>处理以后的图片</returns>
        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH, int Mode)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH, PixelFormat.Format16bppRgb555);
                Graphics g = Graphics.FromImage(b);
                //b.SetResolution(newW, newH);
                // 插值算法的质量
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, newW, newH));
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using iText;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using ClosedXML.Excel;

namespace Sunnet_NBFC.App_Code
{
    public class ClsCommon
    {
        public static SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {

                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static void GETClassFromDt(DataTable dt, ref ClsReturnData clsRtn)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                clsRtn.MessageDesc = clsRtn.Message;
                if (clsRtn.ID > 0)
                    clsRtn.MsgType = (int)MessageType.Success;
                else
                    clsRtn.MsgType = (int)MessageType.Fail;
            }
        }

        public static SelectList AnswerDDL()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Text = "Satisfactory",
                Value = "Satisfactory"
            });

            list.Add(new SelectListItem()
            {
                Text = "Not Satisfactory",
                Value = "Not Satisfactory",
                Selected = true
            });

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList StatusDDL(string StatusType)
        {
            DataTable Dt = DataInterface2.GetStatusForDDL(StatusType);
            //return new SelectList(Dt.AsEnumerable(), "Status", "StatusDesc");
            return ToSelectList(Dt, "Status", "StatusDesc");
        }
        public static bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".bmp":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                default:
                    return false;
            }
        }

        public static void AddCelltoHeader(PdfPTable tablelayout, string cellText, bool borderYn = true)
        {
            if (borderYn == true)
            {
                Font fontH1 = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
                tablelayout.AddCell(new PdfPCell(new iTextSharp.text.Phrase(cellText, fontH1))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 5
                });

            }
            else
            {
                Font fontH1 = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
                tablelayout.AddCell(new PdfPCell(new iTextSharp.text.Phrase(cellText, fontH1))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 5,
                    Border = Rectangle.NO_BORDER
                });
            }
        }

        public static void AddCelltoBody(PdfPTable tablelayout, string cellText, bool borderYn = true, bool ImageTblYn = false)
        {
            if (borderYn == true && ImageTblYn == false)
            {
                Font fontH1 = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);
                tablelayout.AddCell(new PdfPCell(new iTextSharp.text.Phrase(cellText, fontH1))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    BackgroundColor = iTextSharp.text.BaseColor.WHITE
                });

            }
            else if (borderYn == false && ImageTblYn == false)
            {
                Font fontH1 = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);
                tablelayout.AddCell(new PdfPCell(new iTextSharp.text.Phrase(cellText, fontH1))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    BackgroundColor = iTextSharp.text.BaseColor.WHITE,
                    Border = Rectangle.NO_BORDER
                });

            }
            else if (borderYn == false && ImageTblYn == true)
            {
                Font fontH1 = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL);
                tablelayout.AddCell(new PdfPCell(new iTextSharp.text.Phrase(cellText, fontH1))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 4,
                    BackgroundColor = iTextSharp.text.BaseColor.WHITE,
                    Border = Rectangle.NO_BORDER
                });

            }
            else
            {
                Font fontH1 = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL);
                tablelayout.AddCell(new PdfPCell(new iTextSharp.text.Phrase(cellText, fontH1))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 5,
                    Border = Rectangle.NO_BORDER
                });
            }
        }

        public static void ExportToExcel(GridView gv, string FileName)
        {

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            HttpContext.Current.Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            HttpContext.Current.Response.Output.Write(objStringWriter.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();


        }

        public static string PreviewImage(string filepath)
        {
            string imgurl = "";
            if (File.Exists(filepath))
            {
                FileStream ds = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(ds);
                Byte[] bytes = br.ReadBytes((Int32)ds.Length);
                br.Close();
                ds.Close();
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgurl = "data:image/png;base64," + base64String;
            }
            return imgurl;
        }
        public static string ReplaceSpecialCharacter(string Words)
        {

            string RepCharacter = "";

            RepCharacter = Words.Trim().Replace("/", "").Replace(">", "").Replace("<", "").Replace("=", "").Replace(".exe", "").Replace("^", "").Replace("*", "").Replace("'", "").Replace("!", "").Replace("|", "");

            return RepCharacter;

        }
        public static void WriteErrorLog_BulkUpload(string strLog, string logFilePath)

        {
            FileInfo logFileInfo = new FileInfo(logFilePath);
            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists)
            {
                logDirInfo.Create();
            }
            using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {

                    log.WriteLine(System.DateTime.Now.ToString("g") + ":" + strLog);
                }
            }
        }
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +

                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +

                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
            {
                return (true);
            }

            else

            {

                return (false);

            }

        }

        public static void WriteLog(string strLog)
        {
            string FP_ErrorLog = ConfigurationManager.AppSettings["ErrorLog"];
            string logFilePath = FP_ErrorLog + "Error_Log-" + System.DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            FileInfo logFileInfo = new FileInfo(logFilePath);
            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists)
            {
                logDirInfo.Create();
            }
            using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    log.WriteLine(System.DateTime.Now.ToString("g") + ":" + strLog);
                }
            }

        }
       

        public static DataTable ConvertJsonToDataTable(string jsonData)
        {
            try
            {
                return JsonConvert.DeserializeObject<DataTable>(jsonData);
            }
            catch
            {
                return null;
            }
        }
    }



}

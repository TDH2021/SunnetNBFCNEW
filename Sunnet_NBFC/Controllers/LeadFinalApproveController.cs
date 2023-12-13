using Sunnet_NBFC.App_Code;
using Sunnet_NBFC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sunnet_NBFC.Controllers
{
    public class LeadFinalApproveController : Controller
    {
        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadView(clsLeadGenerationMaster clss)
        {
            if (Session["UserID"] != null)
            {
                if (String.IsNullOrEmpty(Session["UserID"].ToString()) == true)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();


                    List<clsLeadGenerationMaster> lst = new List<clsLeadGenerationMaster>();
                    try
                    {
                        ViewBag.MainProductList = ClsCommon.ToSelectList(DataInterface1.GetMainProductddl("View"), "MainProdId", "ProductName");

                        using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                        {
                            cls.ReqType = "GetLeadAllData";
                            cls.CompanyId = ClsSession.CompanyID;
                            cls.BranchID = ClsSession.BranchId;
                            cls.MainProductId = clss.MainProductId;
                            cls.ProductId = clss.ProductId;
                            cls.LeadNo = clss.LeadNo;
                            cls.CustomerName = clss.CustomerName;
                            cls.MobileNo1 = clss.MobileNo1;
                            cls.PanNo = clss.PanNo;
                            cls.AadharNo = clss.AadharNo;

                            cls.ShortStage_Name = "FinalApprove";
                            cls.LeadNo = "";
                            cls.LeadId = 0;
                            cls.Empid = int.Parse(Session["EmpId"].ToString());
                            if (Request.QueryString["ShortStage_Name"] != null)
                            {
                                cls.ShortStage_Name = Request.QueryString["ShortStage_Name"].ToString();
                                cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                                cls.Empid = 0;
                            }

                            using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
                            {
                                if (dt != null)
                                {
                                    List<clsLeadGenerationMaster> list = new List<clsLeadGenerationMaster>();
                                    list = (from DataRow row in dt.Rows

                                            select new clsLeadGenerationMaster()
                                            {
                                                LeadId = int.Parse(row["LeadId"].ToString()),
                                                LeadNo = row["LeadNo"].ToString(),
                                                MainProductName = row["MainProductName"].ToString(),
                                                ProductName = row["ProductName"].ToString(),
                                                CustomerName = row["CustomerName"].ToString(),
                                                MobileNo1 = row["MobileNo1"].ToString(),
                                                PanNo = row["PanNo"].ToString(),
                                                AadharNo = row["AadharNo"].ToString(),
                                                ReuestedLoanAmount = row["ReuestedLoanAmount"].ToString(),
                                                ReuestedLoanTenure = row["ReuestedLoanTenure"].ToString(),
                                                ShortStage_Name = row["ShortStage_Name"].ToString(),
                                                StatusDesc = row["StatusDesc"].ToString(),

                                            }).ToList();


                                    ViewBag.lst = list;// DataInterface.ConvertDataTable<clsLeadGenerationMaster>(dt);
                                }
                            }
                        }

                    }
                    catch (Exception e1)
                    {
                        using (clsError clsE = new clsError())
                        {
                            clsE.ReqType = "Insert";
                            clsE.Mode = "WEB";
                            clsE.ErrorDescrption = e1.Message;
                            clsE.FunctionName = "Status View";
                            clsE.Link = "Status/ViewStatus";
                            clsE.PageName = "Status Controller";
                            clsE.UserId = "1";
                            DataInterface.PostError(clsE);
                        }
                        throw e1;
                    }

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult ExportToExcel(clsLeadGenerationMaster clss)
        {

            var gv = new GridView();

            using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
            {
                cls.ReqType = "GetLeadAllData";
                cls.CompanyId = ClsSession.CompanyID;
                cls.BranchID = ClsSession.BranchId;
                cls.MainProductId = clss.MainProductId;
                cls.ProductId = clss.ProductId;
                cls.LeadNo = clss.LeadNo;
                cls.CustomerName = clss.CustomerName;
                cls.MobileNo1 = clss.MobileNo1;
                cls.PanNo = clss.PanNo;
                cls.AadharNo = clss.AadharNo;
                //cls.ReqType = "ViewLead";

                cls.ShortStage_Name = "FinalApprove";
                cls.LeadNo = "";
                cls.LeadId = 0;
                cls.Empid = Session["UserType"].ToString().ToUpper() != "A" ? int.Parse(Session["EmpId"].ToString()) : 0;
                if (Request.QueryString["ShortStage_Name"] != null)
                {
                    cls.ShortStage_Name = Request.QueryString["ShortStage_Name"].ToString();
                    cls.StageEmpId = int.Parse(Session["EmpId"].ToString());
                    cls.Empid = 0;
                }
                using (DataTable dt = DataInterface.GetLeadGenerationData(cls))
                {
                    if (dt != null)
                    {
                        gv.DataSource = dt;

                        gv.DataBind();
                        Response.ClearContent();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment; filename=ReportView.xls");
                        Response.ContentType = "application/ms-excel";
                        Response.Charset = "";

                        StringWriter objStringWriter = new StringWriter();

                        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

                        gv.RenderControl(objHtmlTextWriter);



                        Response.Output.Write(objStringWriter.ToString());

                        Response.Flush();

                        Response.End();

                    }


                    return RedirectToAction("LeadView");

                }

            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadFinalApprove(int leadid, string ComeFrom = "FinalApprove")
        {
            clsLeadFinalApproveMain model = new clsLeadFinalApproveMain();
            try
            {
                clsLeadFinalApproveMain M = new clsLeadFinalApproveMain();
                DataTable dtLeadDetail = new DataTable();
                DataTable dtLeadDoc = new DataTable();
                if (leadid > 0)
                {
                    M.ReqType = "Get";
                    M.ShortStage_Name = "FinalApprove";
                    M.LeadId = leadid;
                    using (DataTable dt = DataInterface2.GetLeadDetail(M))
                    {
                        if (dt != null)
                        {
                            DataRow dr = dt.Rows[0];
                            model.LeadNo = dr["LeadNo"].ToString();
                            model.LeadId = int.Parse(dr["LeadId"].ToString());
                            model.ShortStage_Name = dr["ShortStage_Name"].ToString();
                            model.Status = dr["Status"].ToString();
                        }
                    }

                    using (DataTable dt = DataInterface1.dbLeadFinalApprove(M))
                    {
                        if (dt != null)
                        {
                            DataRow dr = dt.Rows[0];
                            model.FinalApproveId = int.Parse(dr["FinalApproveId"].ToString());
                            model.Particulers = dr["Particulers"].ToString();
                            model.Proccesfees = decimal.Parse(dr["Proccesfees"].ToString());
                            model.AdvanceEMI = decimal.Parse(dr["AdvanceEMI"].ToString());
                            model.GST = decimal.Parse(dr["GST"].ToString());
                            model.NetDisbAmt = decimal.Parse(dr["NetDisbAmt"].ToString());
                            model.TrnchsNo = decimal.Parse(dr["TrnchsNo"].ToString());
                            model.CersaiCharges = decimal.Parse(dr["CersaiCharges"].ToString());
                            model.StamppingCharges = decimal.Parse(dr["StamppingCharges"].ToString());
                            model.Remarks = dr["Remarks"].ToString();
                            model.CreatedBy = int.Parse(dr["CreatedBy"].ToString());
                            model.UpdatedBy = int.Parse(dr["UpdatedBy"].ToString());
                            model.IsDelete = int.Parse(dr["IsDelete"].ToString());
                            model.CompanyId = int.Parse(dr["CompanyId"].ToString());
                            model.BorrowerKyc = dr["BorrowerKyc"].ToString();
                            model.GuarantorKyc = dr["GuarantorKyc"].ToString();
                            model.PDC = dr["PDC"].ToString();
                            model.BorrowerPhoto = dr["BorrowerPhoto"].ToString();
                            model.CoBorrowerPhoto = dr["CoBorrowerPhoto"].ToString();
                            model.GuarantorPhoto = dr["GuarantorPhoto"].ToString();
                            model.SanctionLetter = dr["SanctionLetter"].ToString();
                            model.LoanAgreementkit = dr["LoanAgreementkit"].ToString();
                            model.DisbursementRequestLetter = dr["DisbursementRequestLetter"].ToString();
                            model.NocPreviousFinanced = dr["NocPreviousFinanced"].ToString();
                            model.Rtoslip = dr["Rtoslip"].ToString();
                            //model.DisbursementRequestLetter = dr["DisbursementRequestLetter"].ToString();
                            //model.SignatureVerification = dr["SignatureVerification"].ToString();
                            //model.KycSelfAttested = dr["KycSelfAttested"].ToString();

                        }
                    }

                    //dtLeadDetail = DataInterface2.GetLeadDetail(M);
                    //dtLeadDoc = DataInterface1.dbLeadFinalApprove(M);
                }
                //if (dtLeadDetail != null && dtLeadDetail.Rows.Count > 0)
                //    M = DataInterface.GetItem<clsLeadFinalApproveMain>(dtLeadDetail.Rows[0]);

                //if (dtLeadDoc != null && dtLeadDoc.Rows.Count > 0)
                //    M.clsLeadFinalApprove = DataInterface.ConvertDataTable<clsLeadFinalApprove>(dtLeadDoc);

                model.Status = model.Status ?? "P";
                ViewBag.ComeFrom = ComeFrom;

            }
            catch (Exception ex)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "Get";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = ex.Message;
                    clse.FunctionName = "LeadFinalApprove";
                    clse.Link = "Lead/LeadFinalApprove";
                    clse.PageName = "Lead Final Approve Controller";
                    clse.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clse);
                }
            }
            return View(model);
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        [HttpPost]
        public ActionResult LeadFinalApproveSave(clsLeadFinalApproveMain M)
        {
            ClsReturnData clsRtn = new ClsReturnData();
            clsRtn.MsgType = (int)MessageType.Fail;
            bool IsSave = false;

            try
            {
                TempData.Clear();
                DataTable dt = new DataTable();

                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Invalid Model";
                    return View(M);
                }

                if (M.FinalApproveId <= 0)
                {
                    M.ReqType = "Insert";
                }
                else
                {
                    M.ReqType = "Update";
                }

                dt = DataInterface1.dbLeadFinalApprove(M);

                //ClsCommon.GETClassFromDt(dt, ref clsRtn);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"])) > 0)
                    {
                        IsSave = true;
                    }
                }


                if (IsSave)
                {
                    M.ReqType = "UpdateStatus";
                    M.Status = "A";
                    dt = DataInterface1.UpdateLeadStatusFa(M);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        clsRtn.ID = Convert.ToInt64("0" + Convert.ToString(dt.Rows[0]["ReturnID"]));
                        clsRtn.Message = Convert.ToString(dt.Rows[0]["ReturnMessage"]);
                        clsRtn.MessageDesc = clsRtn.Message;
                        if (clsRtn.ID > 0)
                        {
                            clsRtn.MsgType = (int)MessageType.Success;
                        }
                        else
                        {
                            clsRtn.MsgType = (int)MessageType.Fail;
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "InsertUpdate";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = e1.Message;
                    clse.FunctionName = "LeadFinalApproveSave";
                    clse.Link = "Lead/LeadFinalApprove";
                    clse.PageName = "Lead Final Approve Controller";
                    clse.UserId = ClsSession.UserID.ToString();
                    DataInterface.PostError(clse);
                }
            }

            if (IsSave)
            {
                //DownloadSanctionLetter(M.LeadId, M.LeadNo);
                TempData["Success"] = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Saved/Updated";
                return RedirectToAction("LeadView", "LeadFinalApprove");
                //return View();
            }
            else
            {
                DataTable dtLeadDetail2 = new DataTable();
                DataTable dtLeadDoc2 = new DataTable();
                if (M.LeadId > 0)
                {
                    M.ReqType = "Get";
                    M.ShortStage_Name = "FinalApprove";
                    dtLeadDetail2 = DataInterface2.GetLeadDetail(M);
                    dtLeadDoc2 = DataInterface1.dbLeadFinalApprove(M);
                }
                if (dtLeadDetail2 != null && dtLeadDetail2.Rows.Count > 0)
                    M = DataInterface.GetItem<clsLeadFinalApproveMain>(dtLeadDetail2.Rows[0]);

                if (dtLeadDoc2 != null && dtLeadDoc2.Rows.Count > 0)
                    M.clsLeadFinalApprove = DataInterface.ConvertDataTable<clsLeadFinalApprove>(dtLeadDoc2);

                M.Status = M.Status ?? "P";

                ViewBag.Error = !string.IsNullOrEmpty(clsRtn.Message) ? clsRtn.Message : "Error: Data Not Saved/Updated";
                return View(M);
            }
        }

        [Sunnet_NBFC.App_Code.SessionAttribute]
        public ActionResult LeadFinalApproveView(clsLeadFinalApprove M)
        {
            List<clsLeadFinalApprove> lst = new List<clsLeadFinalApprove>();
            try
            {
                //DataTable dtLeadDetail = new DataTable();
                DataTable dtLeadDoc = new DataTable();
                M.ReqType = "View";
                M.CompanyId = ClsSession.CompanyID;
                //M.StageId = 6;
                //dtLeadDetail = DataInterface2.GetLeadDetail(M);
                dtLeadDoc = DataInterface1.dbLeadFinalApprove(M);
                lst = DataInterface.ConvertDataTable<clsLeadFinalApprove>(dtLeadDoc);
                //if (dtLeadDetail != null && dtLeadDetail.Rows.Count > 0)
                //M = DataInterface.GetItem<clsLeadFinalApprove>(dtLeadDoc.Rows[0]);
                //if (dtLeadDoc != null && dtLeadDoc.Rows.Count > 0)
                //    M.clsLeadFinalApprove = DataInterface.ConvertDataTable<clsLeadFinalApprove>(dtLeadDoc);
                //M.Status = M.Status ?? "P";
                //return View(M);
            }
            catch (Exception ex)
            {
                using (clsError clse = new clsError())
                {
                    clse.ReqType = "View";
                    clse.Mode = "WEB";
                    clse.ErrorDescrption = ex.Message;
                    clse.FunctionName = "ViewLeadFinalApprove";
                    clse.Link = "LeadFinalApprove/LeadFinalApproveView";
                    clse.PageName = "LeadFinalApprove Controller";
                    clse.UserId = "1";
                    DataInterface.PostError(clse);
                }
            }

            return View(lst);
        }

        public string DownloadSanctionLetter(int? leadid, string leadno)
        {
            string FileName = "";
            if (leadid > 0)
            {
                FileName = "SancLetter_" + leadid.ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
                string Filepath = Server.MapPath("~/" + ConfigurationManager.AppSettings["GenLetterPath"].ToString() + "/" + FileName);
                using (clsLeadGenerationMaster cls = new clsLeadGenerationMaster())
                {
                    cls.ReqType = "GenSanction";
                    cls.LeadId = Convert.ToInt32("0" + leadid.ToString());
                    //cls.LeadNo = "Lead032023000001";
                    using (DataSet ds = DataInterface.DBLetter(cls))
                    {
                        if (ds.Tables.Count == 2)
                        {
                            ds.Tables[0].TableName = "Company";
                            ds.Tables[1].TableName = "Lead";
                            using (clsSanctionLetter cls1 = new clsSanctionLetter())
                            {
                                cls1.GenSanctionLetter(Filepath, "SanctionLetter", ds);
                            }
                        }
                    }
                }



                //string path = Filepath;
                //byte[] fileBytes = GetFile(path);
                //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
                //return File(Filepath, "application/force-download", Path.GetFileName(Filepath));

                //#region send email
                //try
                //{
                //    using (clsSendMail cls = new clsSendMail())
                //    {
                //        using (clsMail clsMail = new clsMail())
                //        {
                //            clsMail.ToEmail = "chetan.saini99@gmail.com";
                //            clsMail.Subject = "Sanction Letter -" + leadid.ToString();
                //            clsMail.AttachFile = Filepath;
                //            clsMail.BodyHtml = cls.SanctionLetter("Test", "VchLoan", "10000", Server.MapPath("~/EmailTemplates/SenctionLetter.html"));
                //            int a = cls.SendMail(clsMail);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{ }
                //#endregion

                //#region send sms
                //try
                //{
                //    using (clsTdhSms clsfn = new clsTdhSms())
                //    {
                //        using (clsSMSMaster clssms = new clsSMSMaster())
                //        {
                //            clssms.SMSType = "FinalApprove";
                //            clssms.LeadId = Convert.ToInt32("0" + leadid.ToString());
                //            DataSet ds1 = DataInterface1.dbSMSMaster(clssms);
                //            string URL = ds1.Tables[0].Rows[0]["url"].ToString();
                //            string msg = ds1.Tables[0].Rows[0]["sms"].ToString();
                //            msg = msg.Replace("{#name#}", "Chetan").Replace("{#loanamt#}", "100000").Replace("{#leadno#}", leadno);
                //            URL = URL.Replace("@MobileNo", ds1.Tables[1].Rows[0]["mobileno"].ToString()).Replace("@Message", msg).Replace("@TemplateID", ds1.Tables[0].Rows[0]["TemplateId"].ToString());
                //            string a = clsfn.SendSms(URL);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{ }
                //#endregion

                if (FileName != "")
                {
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Filepath));
                    Response.WriteFile(Filepath);
                    Response.End();
                }

                return "1";
            }
            else
            {
                return "0";
            }

        }


        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(fileName, "application/pdf");
            //Send the File to Download.
            //return File(bytes, "application/octet-stream", fileName);
        }


        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }



        //[HttpPost]
        //public ActionResult SendSMS()
        //{
        //    string message = member.Message;
        //    string cell = member.Cell;

        //    // use the API URL here  
        //    string strUrl = "https://platform.clickatell.com/messages     

        //   / http / send ? apiKey = xxxxxxx & to = xxxxxxx &

        //    content = xxxxxxxx";
        //// Create a request object  
        //WebRequest request = HttpWebRequest.Create(strUrl);
        //    // Get the response back  
        //    HttpWebResponse response =

        //    (HttpWebResponse)request.GetResponse();
        //    Stream s = (Stream)response.GetResponseStream();
        //    StreamReader readStream = new StreamReader(s);
        //    string dataString = readStream.ReadToEnd();
        //    response.Close();
        //    s.Close();
        //    readStream.Close();

        //    return View("Answer", member);
        //}

        void SendTestMail()
        {

            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("tdhitsolution@gmail.com"));  // replace with valid value 
            message.From = new MailAddress(ConfigurationManager.AppSettings["UserName"].ToString());  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = string.Format(body, "Himanshu", "tdhitsolution@gmail.com", "Test Mail");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings["UserName"].ToString(),  // replace with valid value
                    Password = ConfigurationManager.AppSettings["Password"].ToString() // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = ConfigurationManager.AppSettings["Host"].ToString();
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
                smtp.EnableSsl = false;
                smtp.Send(message);

            }
        }


    }
}
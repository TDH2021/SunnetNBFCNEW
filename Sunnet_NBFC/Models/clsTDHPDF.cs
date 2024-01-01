using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using Sunnet_NBFC.App_Code;
using System;
using Microsoft.SqlServer.Server;

namespace Sunnet_NBFC.Models
{

    public static class clsCommonPDF
    {
        public static int TABLEWIDTH = 100;
        public static Document Letter(string FilePath, string letterName)
        {
            System.IO.FileStream fs = new FileStream(FilePath, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddAuthor("TDH");
            document.AddCreator("TDH");
            document.AddKeywords(letterName);
            document.AddSubject(letterName);
            document.AddTitle(letterName);
            return document;
        }

        public static void MainHeading(Document doc, DataTable dt)
        {
            PdfPTable table = new PdfPTable(2);

            float[] headers = { 20, 80 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;
            Image img = Image.GetInstance(HttpContext.Current.Server.MapPath("~/CompanyLOGO/sunitslogo.png"));
            //img.ScaleAbsolute(120f, 50f);
            PdfPCell cell1 = new PdfPCell(img);
            cell1.Rowspan = 5;
            cell1.Border = Rectangle.NO_BORDER;
            cell1.Padding = 0;
            table.AddCell(cell1);
            //PdfPCell cellCompanyName = new PdfPCell(new Phrase(dt.Rows[0]["CompanyName"].ToString()));
            //table.AddCell(cellCompanyName);
            ClsCommon.AddCelltoHeader(table, dt.Rows[0]["CompanyName"].ToString(), false);
            ClsCommon.AddCelltoBody(table, "CorporateOffice:6thMilestone,YuvrajComplex,Delhi-UPBorder", false, true);
            ClsCommon.AddCelltoBody(table, "Chikemberpur,Ghaziabad - 201006 (UP) Phone:0120-4165439", false, true);
            ClsCommon.AddCelltoBody(table, "CIN U67120DL1995PTC072870", false, true);
            ClsCommon.AddCelltoBody(table, "Website www.suneetfinman.com", false, true);
            //ClsCommon.AddCelltoBody(table, dt.Rows[0]["CompanyName"].ToString(), false, true);
            //PdfPCell cell2 = new PdfPCell(new Phrase("CorporateOffice:6thMilestone,YuvrajComplex,Delhi-UPBorder", new Font(Font.FontFamily.TIMES_ROMAN, Font.BOLD, 12)));
            //table.AddCell(cell2);
            //PdfPCell cell21 = new PdfPCell(new Phrase("Chikemberpur,Ghaziabad - 201006 (UP) Phone:0120-4165439", new Font(Font.FontFamily.TIMES_ROMAN, Font.BOLD, 10)));
            //table.AddCell(cell21);
            //PdfPCell cell22 = new PdfPCell(new Phrase("CIN U67120DL1995PTC072870", new Font(Font.FontFamily.TIMES_ROMAN, Font.BOLD, 12)));
            //table.AddCell(cell22);
            //PdfPCell cell23 = new PdfPCell(new Phrase("Website www.suneetfinman.com", new Font(Font.FontFamily.TIMES_ROMAN, Font.BOLD, 12)));
            //table.AddCell(cell23);
            doc.Add(table);
        }
    }
    public class clsSanctionLetter : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenSanctionLetter(string FilePath, string LetterName, DataSet dataSet)
        {
            Document document = new Document();
            document = clsCommonPDF.Letter(FilePath, LetterName);
            document.Open();
            clsCommonPDF.MainHeading(document, dataSet.Tables["Company"]);
            //document.Close();
            PdfPTable table = new PdfPTable(2);

            float[] headers = { 60, 40 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;

            string text = "\nRef No : " + dataSet.Tables["Lead"].Rows[0]["LeadNo"].ToString();
            ClsCommon.AddCelltoBody(table, text, false);
            text = "Date : " + dataSet.Tables["Lead"].Rows[0]["EntryDate"].ToString();
            ClsCommon.AddCelltoBody(table, text, false);
            document.Add(table);

            text = "To,\n" + dataSet.Tables["Lead"].Rows[0]["CustName"].ToString();
            text = text + "\nS/O" + dataSet.Tables["Lead"].Rows[0]["FatherName"].ToString();
            Paragraph p1 = new Paragraph(text);
            p1.Font.Size = 10;
            p1.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p1.Alignment = Element.ALIGN_LEFT;
            document.Add(p1);


            text = "";
            text = "\nSubject: - Sanction of Credit facilities against your loan application dated " + dataSet.Tables["Lead"].Rows[0]["EntryDate"].ToString() + "\n";
            Paragraph p2 = new Paragraph(text);
            p2.Font.Size = 10;
            p2.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p2.Alignment = Element.ALIGN_LEFT;
            document.Add(p2);

            text = "Dear Customer,";
            text = text + "\nWe are glad to inform you that at your request, the following facilities have been sanctioned/renewed as per the details furnished below and, on the terms, & conditions mentioned herein as well those mentioned in the loan documents\n\n";

            Paragraph p3 = new Paragraph(text);
            p3.Font.Size = 10;
            p3.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p3.Alignment = Element.ALIGN_LEFT;
            document.Add(p3);

            PdfPTable table1 = new PdfPTable(2);
            float[] headers1 = { 50, 50 };
            table1.SetWidths(headers1);
            table1.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table1, "Product", true);
            ClsCommon.AddCelltoBody(table1, dataSet.Tables["Lead"].Rows[0]["SubProduct"].ToString(), true);
            ClsCommon.AddCelltoHeader(table1, "Proposed End Use", true);
            ClsCommon.AddCelltoBody(table1, dataSet.Tables["Lead"].Rows[0]["LoanPurpose"].ToString(), true);
            ClsCommon.AddCelltoHeader(table1, "Limit/Loan Sanction Amt", true);
            ClsCommon.AddCelltoBody(table1, dataSet.Tables["Lead"].Rows[0]["NetDisbursementAmount"].ToString().ToString(), true); 
            ClsCommon.AddCelltoHeader(table1, "Interest Type", true);
            ClsCommon.AddCelltoBody(table1, "".ToString(), true);
            ClsCommon.AddCelltoHeader(table1, "Loan rate of Interest", true);
            ClsCommon.AddCelltoBody(table1, dataSet.Tables["Lead"].Rows[0]["ROI"].ToString(), true);
            ClsCommon.AddCelltoHeader(table1, "Tenure", true);
            ClsCommon.AddCelltoBody(table1, dataSet.Tables["Lead"].Rows[0]["Tenure"].ToString(), true);
            ClsCommon.AddCelltoHeader(table1, "Repayment", true);
            ClsCommon.AddCelltoBody(table1, "", true);
            ClsCommon.AddCelltoHeader(table1, "Security", true);
            ClsCommon.AddCelltoBody(table1, "", true);
            ClsCommon.AddCelltoHeader(table1, "Processing Fees", true);
            ClsCommon.AddCelltoBody(table1, dataSet.Tables["Lead"].Rows[0]["Proccesfees"].ToString(), true);
            document.Add(table1);

            Paragraph p4 = new Paragraph("Security furnished/to be furnished\n\n");
            p4.Font.Size = 10;
            p4.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p4.Alignment = Element.ALIGN_LEFT;
            document.Add(p4);
            PdfPTable table2 = new PdfPTable(2);
            float[] headers2 = { 40, 60 };
            table2.SetWidths(headers2);
            table2.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table2, "S.No");
            ClsCommon.AddCelltoHeader(table2, "Description");

            ClsCommon.AddCelltoBody(table2, "1");
            ClsCommon.AddCelltoBody(table2, "");

            ClsCommon.AddCelltoBody(table2, "2");
            ClsCommon.AddCelltoBody(table2, "");
            document.Add(table2);

            Font fontHeading = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD);
            Font fontCell = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.NORMAL);

            // chetan

            text = "Additional Condition to Comply prior to disbursal: -\n";
            text = text + "\nKindly use the Prospect No. as mentioned above in all your further communication with us. Please put your signature as token of your acceptance of the above stated terms & condition and retain a copy with yourself.";
            text = text + "\nIn case of any query or assistance please contact your Branch manager or alternatively you can Email us at info@suneetfinman.comor our corporate office address is 6th Milestone, Yuvraj complex, Delhi UP Border, Chikemberpur, Ghaziabad 201006 or contact us at our landline number 0120-4165439";
            text = text + "\nThe loan sanction letter is valid for a period of 30 from the date of issuance. The borrower must accept the offer within this period by providing all required documentation and signing the loan agreement.If the borrower fails to accept the offer within the validity period, the loan sanction letter will be deemed null and void, and the borrower will be required to submit a new loan application for reconsideration. This validity period clause is subject to the laws and regulations governing loan agreements in the jurisdiction where the loan is being disbursed";
            text = text + "\nThe loan sanction letter is valid for a period of 30 from the date of issuance. The borrower must accept the offer within this period by providing all required documentation and signing the loan agreement.If the borrower fails to accept the offer within the validity period, the loan sanction letter will be deemed null and void, and the borrower will be required to submit a new loan application for reconsideration. This validity period clause is subject to the laws and regulations governing loan agreements in the jurisdiction where the loan is being disbursed";
            text = text + "\nIt is the borrower's responsibility to keep the lender informed of any changes in their financial or personal circumstances that may affect their ability to repay the loan.";
            text = text + "\nAll the terms & conditions mentioned in the sanction letter has conveyed & accepted by the customer.If the borrower accepts the loan sanction offer within the validity period, the loan agreement will be executed, and the loan amount will be disbursed in accordance with the terms and conditions of the loan agreement.";
            text = text + "\nWe value your relationship with us and assure you of our best services always.";
            text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
            Paragraph p5 = new Paragraph(text);
            p5.Font.Size = 10;
            p5.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p5.Alignment = Element.ALIGN_LEFT;
            document.Add(p5);
            document.Close();
            //// Close the document  

            //// Close the writer instance  
            //writer.Close();
            //// Always close open filehandles explicity  
            //fs.Close();           
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }
    public class clsWelcomeLetter : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenWelcomeLetter(string FilePath, string LetterName, DataSet dataSet)
        {
            try
            {
                string newline = "\n";
                Paragraph pnline = new Paragraph(newline);
                Document document = new Document();
                document = clsCommonPDF.Letter(FilePath, LetterName);
                document.Open();
                clsCommonPDF.MainHeading(document, dataSet.Tables["Company"]);
                //document.Close();
                PdfPTable table = new PdfPTable(2);

                float[] headers = { 60, 40 };
                table.SetWidths(headers);
                table.WidthPercentage = TABLEWIDTH;

                string text = " Date  : " + dataSet.Tables["Lead"].Rows[0]["EntryDate"].ToString() + "\n";
                text = text + " Loan account No : " + dataSet.Tables["Lead"].Rows[0]["LeadNo"].ToString() + "\n";
                text = text + " Customer ID : " + dataSet.Tables["Lead"].Rows[0]["CIF"].ToString() + "\n\n\n";

                text = text + "To,\n" + dataSet.Tables["Lead"].Rows[0]["CustName"].ToString();
                text = text + "\nS/O " + dataSet.Tables["Lead"].Rows[0]["FatherName"].ToString();
                Paragraph p1 = new Paragraph(text);
                p1.Font.Size = 10;
                p1.Font.Color = iTextSharp.text.BaseColor.BLACK;
                p1.Alignment = Element.ALIGN_LEFT;
                document.Add(p1);


                text = "";
                text = "\nDear Customer \n Welcome to " + dataSet.Tables["Company"].Rows[0]["CompanyName"].ToString() + " and thank you for choosing us for your " + dataSet.Tables["Lead"].Rows[0]["MainProduct"].ToString() + "\n";
                text = text + " Your installment amount is INR " + dataSet.Tables["Lead"].Rows[0]["EmiAmount"].ToString() + " and will start from " + dataSet.Tables["Lead"].Rows[0]["LoanDate"].ToString() + " First instalment of your repayment mode (PDC/ECS) from your " + dataSet.Tables["Lead"].Rows[0]["BenficaryName"].ToString() + ". Account No: " + dataSet.Tables["Lead"].Rows[0]["BenficaryAccountNo"].ToString() + " will be banked on ………………….& on "+ dataSet.Tables["Lead"].Rows[0]["EmiDay"].ToString() + " of every Month till the entire tenure of the loan. Total Instalments are " + dataSet.Tables["Lead"].Rows[0]["NoofInst"].ToString() + ". from initial repayment start date. The ROI of the captioned loan is " + dataSet.Tables["Lead"].Rows[0]["ROI"].ToString() + "%\n\n " +
                    "DISBURSAL ISSUED TO YOU VIDE-:";
                Paragraph p2 = new Paragraph(text);
                p2.Font.Size = 10;
                p2.Font.Color = iTextSharp.text.BaseColor.BLACK;
                p2.Alignment = Element.ALIGN_LEFT;
                document.Add(p2);
                document.Add(pnline);
                PdfPTable table1 = new PdfPTable(5);
                float[] headers1 = { 20, 20, 20, 20, 20 };
                table1.SetWidths(headers1);
                table1.WidthPercentage = TABLEWIDTH;
                ClsCommon.AddCelltoHeader(table1, "Mode", true);
                ClsCommon.AddCelltoHeader(table1, "UTR", true);
                ClsCommon.AddCelltoHeader(table1, "Date", true);
                ClsCommon.AddCelltoHeader(table1, "Amount", true);
                ClsCommon.AddCelltoHeader(table1, "In favour", true);

                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Disburse"].Rows[0]["paymode"].ToString());
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Disburse"].Rows[0]["UtrNo"].ToString());
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Disburse"].Rows[0]["LoanDate"].ToString().ToString(), true);
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Disburse"].Rows[0]["NetDisbursementAmount"].ToString());
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Disburse"].Rows[0]["BeneficiaryName"].ToString());

                document.Add(table1);

                text = "";
                text = "THE DEDUCTIONS ARE ON ACCOUNT OF :- ";
                
                Paragraph p3 = new Paragraph(text);
                p2.Font.Size = 10;
                p2.Font.Color = iTextSharp.text.BaseColor.BLACK;
                p2.Alignment = Element.ALIGN_LEFT;
                document.Add(p3);
                document.Add(pnline);
                PdfPTable table2 = new PdfPTable(2);
                float[] headers2 = { 60, 40 };
                table2.SetWidths(headers2);
                table2.WidthPercentage = TABLEWIDTH;
                ClsCommon.AddCelltoHeader(table2, "Description", true);
                ClsCommon.AddCelltoHeader(table2, "Amount", true);

                //ClsCommon.AddCelltoHeader(table2, "Processing Fees", true);
                //ClsCommon.AddCelltoBody(table2, "");
                //ClsCommon.AddCelltoHeader(table2, "Insurance", true);
                //ClsCommon.AddCelltoBody(table2, "");
                //ClsCommon.AddCelltoHeader(table2, "Advance EMI", true);
                //ClsCommon.AddCelltoBody(table2, "");
                ClsCommon.AddCelltoBody(table2, "Processing Fees", true);
                ClsCommon.AddCelltoBody(table2, "");
                ClsCommon.AddCelltoBody(table2, "Insurance", true);
                ClsCommon.AddCelltoBody(table2, "");
                ClsCommon.AddCelltoBody(table2, "Advance EMI", true);
                ClsCommon.AddCelltoBody(table2, "");

                document.Add(table2);

                text = "";
                text = text + "\nIf you require any further details on your ………. Account, please contact us at our customer care no 0120-4165439. Our customer service representatives will be glad to assist you\n.";
                text = text + "\nWe value your relationship with us and assure you of our best services always.";
                text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
                Paragraph p5 = new Paragraph(text);
                p5.Font.Size = 10;
                p5.Font.Color = iTextSharp.text.BaseColor.BLACK;
                p5.Alignment = Element.ALIGN_LEFT;
                document.Add(p5);
                document.Close();
                //// Close the document  

                //// Close the writer instance  
                //writer.Close();
                //// Always close open filehandles explicity  
                //fs.Close();
                //
            }
            catch (Exception ex)
            { }
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }

    public class clsRepyament : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenRepyament(string FilePath, string LetterName, DataSet dataSet)
        {
            Document document = new Document();
            document = clsCommonPDF.Letter(FilePath, LetterName);
            document.Open();
            clsCommonPDF.MainHeading(document, dataSet.Tables["Company"]);
            //document.Close();
            string text = "REPAYMENT SCHEDULE\n";
            Paragraph p1 = new Paragraph(text);
            p1.Font.Size = 12;
            p1.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p1.Alignment = Element.ALIGN_CENTER;
            document.Add(p1);
            PdfPTable table = new PdfPTable(4);

            float[] headers = { 25, 25, 25, 25 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;

            ClsCommon.AddCelltoHeader(table, "CONTACT PERSON", false);
            ClsCommon.AddCelltoBody(table, "", false);
            ClsCommon.AddCelltoHeader(table, "SALES PERSON/ BRANCH HEAD", false);
            ClsCommon.AddCelltoHeader(table, "", false);

            ClsCommon.AddCelltoHeader(table, "Account Name", false);
            ClsCommon.AddCelltoBody(table, dataSet.Tables["Lead"].Rows[0]["CustName"].ToString(), false);
            ClsCommon.AddCelltoHeader(table, "Date of Case", false);
            ClsCommon.AddCelltoHeader(table, dataSet.Tables["Lead"].Rows[0]["EntryDate"].ToString(), false);

            ClsCommon.AddCelltoHeader(table, "Loan Account No", false);
            ClsCommon.AddCelltoBody(table, dataSet.Tables["Lead"].Rows[0]["LeadNo"].ToString(), false);
            ClsCommon.AddCelltoHeader(table, "Total Agreed Value", false);
            ClsCommon.AddCelltoHeader(table, dataSet.Tables["Lead"].Rows[0]["EntryDate"].ToString(), false);

            ClsCommon.AddCelltoHeader(table, "Amount Finance", false);
            ClsCommon.AddCelltoBody(table, dataSet.Tables["Lead"].Rows[0]["EntryDate"].ToString(), false);
            ClsCommon.AddCelltoHeader(table, "Instalment Amount", false);
            ClsCommon.AddCelltoHeader(table, dataSet.Tables["Lead"].Rows[0]["EmiAmount"].ToString(), false);

            ClsCommon.AddCelltoHeader(table, "No of Instalments", false);
            ClsCommon.AddCelltoBody(table, dataSet.Tables["Lead"].Rows[0]["NoofInst"].ToString(), false);
            ClsCommon.AddCelltoHeader(table, "Rate of Interest", false);
            ClsCommon.AddCelltoHeader(table, dataSet.Tables["Lead"].Rows[0]["ROI"].ToString(), false);

            ClsCommon.AddCelltoHeader(table, "Repayment Start Date", false);
            ClsCommon.AddCelltoBody(table, dataSet.Tables["Lead"].Rows[0]["LoanStartDate"].ToString(), false);
            ClsCommon.AddCelltoHeader(table, "Repayment End Date", false);
            ClsCommon.AddCelltoHeader(table, dataSet.Tables["Lead"].Rows[0]["LoanEndDate"].ToString(), false);

            document.Add(table);


            PdfPTable table1 = new PdfPTable(7);
            float[] headers2 = { 10, 15, 15, 15, 15, 15, 15 };
            table1.SetWidths(headers2);
            table1.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table1, "Sr.No.", true);
            ClsCommon.AddCelltoHeader(table1, "Due Date", true);
            ClsCommon.AddCelltoHeader(table1, "Opening Principal", true);
            ClsCommon.AddCelltoHeader(table1, "Instalment Amount", true);
            ClsCommon.AddCelltoHeader(table1, "Principal Amt", true);
            ClsCommon.AddCelltoHeader(table1, "Interest Amt", true);
            ClsCommon.AddCelltoHeader(table1, "Closing Principal", true);
            int n = 0;
            for (int i = 0; i <= dataSet.Tables["Repayment"].Rows.Count - 1; i++)
            {
                n++;
                ClsCommon.AddCelltoBody(table1, n.ToString(), true);
                //ClsCommon.AddCelltoBody(table1, dataSet.Tables["Repayment"].Rows[i]["EmiNo"].ToString(), true);
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Repayment"].Rows[i]["EmiDate"].ToString(), true);
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Repayment"].Rows[i]["OpeningPrincipal"].ToString(), true);
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Repayment"].Rows[i]["EmiAmount"].ToString(), true);
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Repayment"].Rows[i]["Principal"].ToString(), true);
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Repayment"].Rows[i]["Interest"].ToString(), true);
                ClsCommon.AddCelltoBody(table1, dataSet.Tables["Repayment"].Rows[i]["CurrentBalance"].ToString(), true);

            }
            document.Add(table1);

            text = "";
            text = text + "\nWe value your relationship with us and assure you of our best services always.";
            text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
            Paragraph p5 = new Paragraph(text);
            p5.Font.Size = 10;
            p5.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p5.Alignment = Element.ALIGN_LEFT;
            document.Add(p5);
            document.Close();
            //// Close the document  

            //// Close the writer instance  
            //writer.Close();
            //// Always close open filehandles explicity  
            //fs.Close();           
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }

    public class clsForeclosure : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenWelcomeLetter(string FilePath, string LetterName, DataSet dataSet)
        {
            Document document = new Document();
            document = clsCommonPDF.Letter(FilePath, LetterName);
            document.Open();
            clsCommonPDF.MainHeading(document, dataSet.Tables["Company"]);
            //document.Close();
            string text = "Date  -: " + dataSet.Tables["Lead"].Rows[0]["LeadNo"].ToString() + "\n";
            text = text + " Loan account No -: " + dataSet.Tables["Lead"].Rows[0]["LeadNo"].ToString() + "\n";
            text = text + " Customer ID -: " + dataSet.Tables["Lead"].Rows[0]["LeadNo"].ToString() + "\n\n\n";
            Paragraph p1 = new Paragraph(text);
            text = text + "To\n" + dataSet.Tables["Lead"].Rows[0]["CustName"].ToString();
            text = text + "\nS/O" + dataSet.Tables["Lead"].Rows[0]["FatherName"].ToString();
            p1.Font.Size = 10;
            p1.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p1.Alignment = Element.ALIGN_LEFT;
            document.Add(p1);

            text = text + "To\n" + dataSet.Tables["Lead"].Rows[0]["CustName"].ToString();
            text = text + "\nS/O" + dataSet.Tables["Lead"].Rows[0]["FatherName"].ToString();
            Paragraph p2 = new Paragraph(text);
            p2.Font.Size = 10;
            p2.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p2.Alignment = Element.ALIGN_LEFT;
            document.Add(p2);

            text = "";
            text = "Subject -: Foreclosure of your ____________ LOAN Account Number……";
            Paragraph p3 = new Paragraph(text);
            p3.Font.Size = 10;
            p3.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p3.Alignment = Element.ALIGN_CENTER;
            document.Add(p3);

            text = "Dear Customer,\n";
            text = text + "We refer to your request for foreclosure of the captioned Loan Account with us. It has been a pleasure to be associated withyou. We request you to reconsider your decision to foreclose the loan as we would like to continue our relationship with you.\n As requested, given below are the foreclosure details:-\n";
            Paragraph p4 = new Paragraph(text);
            p4.Font.Size = 10;
            p4.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p4.Alignment = Element.ALIGN_CENTER;
            document.Add(p4);

            PdfPTable table = new PdfPTable(2);

            float[] headers = { 60, 40 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table, "Particular", true);
            ClsCommon.AddCelltoHeader(table, "Amount", true);
            ClsCommon.AddCelltoHeader(table, "TOTAL POS (Principal O/s)", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Current Month Interest", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Instalment Overdue", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Foreclosure Charges", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "GST on foreclosure Charges", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Excess Amount", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Bouncing Charges", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Penal Charges", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Other Charges", true);
            ClsCommon.AddCelltoBody(table, "");
            ClsCommon.AddCelltoHeader(table, "Final Foreclosure Amount", true);
            ClsCommon.AddCelltoBody(table, "Total ");
            document.Add(table);

            Paragraph p5 = new Paragraph("Kindly Note");
            p5.Font.Size = 10;
            p5.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p5.Alignment = Element.ALIGN_CENTER;
            document.Add(p5);

            List list = new List(List.ORDERED, 20f);
            //list.SetListSymbol("\u2022");
            list.IndentationLeft = 20f;
            list.Add("Foreclosure statement is valid till 15 days from the date of issuance. We request you to make the payment at least 5 working days before the validity date to factor cheque. For each day beyond the validity date an additional interest will be charged per daywhich is to be calculated on Final Foreclosure Amount.");
            list.Add("The cheque / demand draft should be made favouring SUNEET FINMAN PRIVATE LIMITED Loan A/c No………………………. The above-mentioned Foreclosure amount is valid subject to clearance of all the cheques/instalments till date.");
            list.Add("Suneet Finman Private Limited (SFPL) reserves the right to rectify any errors or discrepancies with due intimation to the customers.");
            list.Add("Applicability of Foreclosure charges is subject to the final validation of the foreclosure clause at the time of Sanction/Loan Disbursement.");
            list.Add("In case of any query or assistance please contact your Branch manager or alternatively you can Email us at info@suneetfinman.com or our corporate office address is 6th Milestone, Yuvraj complex, Delhi UP Border, Chikemberpur, Ghaziabad 201006 or contact us at our landline number 0120-4165439.");
            document.Add(list);

            text = "";
            text = text + "\nWe value your relationship with us and assure you of our best services always.";
            text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
            Paragraph p6 = new Paragraph(text);
            p6.Font.Size = 10;
            p6.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p6.Alignment = Element.ALIGN_LEFT;
            document.Add(p6);
            document.Close();
            //// Close the document  

            //// Close the writer instance  
            //writer.Close();
            //// Always close open filehandles explicity  
            //fs.Close();           
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }

    public class clsNocLAP : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenNocLAP(string FilePath, string LetterName, DataSet dataSet)
        {
            Document document = new Document();
            document = clsCommonPDF.Letter(FilePath, LetterName);
            document.Open();
            clsCommonPDF.MainHeading(document, dataSet.Tables["Company"]);
            //document.Close();

            PdfPTable table = new PdfPTable(4);

            float[] headers = { 25, 25, 25, 25 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table, "Ref No", true);
            ClsCommon.AddCelltoBody(table, "............", true);
            ClsCommon.AddCelltoHeader(table, "Date", true);
            ClsCommon.AddCelltoBody(table, "............", true);
            document.Add(table);

            string text = "";
            text = "";
            text = "\n\nNO DUE CERTIFICATE";
            Paragraph p = new Paragraph(text);
            p.Font.Size = 10;
            p.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);
            text = "To, \n";
            text = text + " Mr. " + dataSet.Tables["Lead"].Rows[0]["CustName"].ToString() + ".\n";
            text = text + " S/o -:" + dataSet.Tables["Lead"].Rows[0]["FatherName"].ToString() + "\n";
            Paragraph p1 = new Paragraph(text);
            p1.Font.Size = 10;
            p1.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p1.Alignment = Element.ALIGN_LEFT;
            document.Add(p1);
            text = "Dear Customer,\n";
            text = text + "We Thankfully acknowledge your association with us.We are pleased to inform you that there is no dues in respect to request given by you and we do not have any claim in below mention Property detail .:-\n";
            Paragraph p4 = new Paragraph(text);
            p4.Font.Size = 10;
            p4.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p4.Alignment = Element.ALIGN_LEFT;
            document.Add(p4);


            text = "\n\nIMMOVABLE PROPERTY DETAIL";
            Paragraph p2 = new Paragraph(text);
            p2.Font.Size = 10;
            p2.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p2.Alignment = Element.ALIGN_CENTER;
            document.Add(p2);

            text = "\n\nThe Immovable Residential PlotArea Measuring …………. bounded given as under; -    ";
            Paragraph p3 = new Paragraph(text);
            p3.Font.Size = 10;
            p3.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p3.Alignment = Element.ALIGN_LEFT;
            document.Add(p3);

            text = "\n\nThe Bounded  of  the said  property; -";
            Paragraph p5 = new Paragraph(text);
            p5.Font.Size = 10;
            p5.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p5.Alignment = Element.ALIGN_LEFT;
            document.Add(p5);
            text = "\nEAST :_______________\nWEST_________________\nNORTH______________________\nSOUTH_____________________ -";
            Paragraph p6 = new Paragraph(text);
            p6.Font.Size = 10;
            p6.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p6.Alignment = Element.ALIGN_LEFT;
            document.Add(p6);
            text = "";
            text = text + "\nLooking forward to your continued  patronage and more opportunities to be of service to you ..";
            text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
            Paragraph p7 = new Paragraph(text);
            p7.Font.Size = 10;
            p7.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p7.Alignment = Element.ALIGN_LEFT;
            document.Add(p7);
            document.Close();
            //// Close the document  

            //// Close the writer instance  
            //writer.Close();
            //// Always close open filehandles explicity  
            //fs.Close();           
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }

    public class clsNocViechle : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenNocViechle(string FilePath, string LetterName, DataSet dataSet)
        {
            Document document = new Document();
            document = clsCommonPDF.Letter(FilePath, LetterName);
            document.Open();
            clsCommonPDF.MainHeading(document, dataSet.Tables["Company"]);
            //document.Close();

            PdfPTable table = new PdfPTable(4);

            float[] headers = { 25, 25, 25, 25 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table, "Ref No", false);
            ClsCommon.AddCelltoBody(table, "............", false);
            ClsCommon.AddCelltoHeader(table, "Date", false);
            ClsCommon.AddCelltoBody(table, "............", false);
            document.Add(table);

            string text = "";
            text = "";
            text = "\n\nNO DUE CERTIFICATE";
            Paragraph p = new Paragraph(text);
            p.Font.Size = 10;
            p.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);
            text = "To, \n";
            text = text + " Mr. " + dataSet.Tables["Lead"].Rows[0]["CustName"].ToString() + ".\n";
            text = text + " S/o -:" + dataSet.Tables["Lead"].Rows[0]["FatherName"].ToString() + "\n";
            Paragraph p1 = new Paragraph(text);
            p1.Font.Size = 10;
            p1.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p1.Alignment = Element.ALIGN_LEFT;
            document.Add(p1);


            text = "Dear Customer,\n";
            text = text + "This is to Certify that a Vehicle Loan was financed to ……………………….. S/o, W/o, D/o   Sh.  …………………………….   R/o …………………   dated  ……………………  against  Vehicle No. ………………………., of  Rs. …………………………../-  (  Rupees  ………………………….Only ), there is no outstanding dues  against  this  loan as on date. Particulars of Vehicle No ……….is mentioned below :- .:-\n";
            Paragraph p4 = new Paragraph(text);
            p4.Font.Size = 10;
            p4.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p4.Alignment = Element.ALIGN_LEFT;
            document.Add(p4);

            PdfPTable table1 = new PdfPTable(2);

            float[] headers1 = { 50, 50 };
            table1.SetWidths(headers);
            table1.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table1, "ENGINE NO", true);
            ClsCommon.AddCelltoBody(table1, "............", true);
            ClsCommon.AddCelltoHeader(table1, "CHASIS NO", true);
            ClsCommon.AddCelltoBody(table1, "............", true);
            ClsCommon.AddCelltoHeader(table1, "MODEL", true);
            ClsCommon.AddCelltoBody(table1, "............", true);
            ClsCommon.AddCelltoHeader(table1, "COLOR", true);
            ClsCommon.AddCelltoBody(table1, "............", true);
            ClsCommon.AddCelltoHeader(table1, "MFG YEAR", true);
            ClsCommon.AddCelltoBody(table1, "............", true);
            document.Add(table1);
            text = "";
            text = text + "\nWe have no objection if our name as Hire Purchase Financiers is deleted from its Registration Certificate & we have issued Hire Purchase termination (Form No 35) on dated ……in respect thereof. ..";
            text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
            Paragraph p7 = new Paragraph(text);
            p7.Font.Size = 10;
            p7.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p7.Alignment = Element.ALIGN_LEFT;
            document.Add(p7);
            document.Close();
            //// Close the document  

            //// Close the writer instance  
            //writer.Close();
            //// Always close open filehandles explicity  
            //fs.Close();           
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }

    public class clsNocPL : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenNocPL(string FilePath, string LetterName, DataSet dataSet)
        {
            Document document = new Document();
            document = clsCommonPDF.Letter(FilePath, LetterName);
            document.Open();
            clsCommonPDF.MainHeading(document, dataSet.Tables["Company"]);
            //document.Close();

            PdfPTable table = new PdfPTable(4);

            float[] headers = { 25, 25, 25, 25 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;
            ClsCommon.AddCelltoHeader(table, "Ref No", false);
            ClsCommon.AddCelltoBody(table, "............", false);
            ClsCommon.AddCelltoHeader(table, "Date", false);
            ClsCommon.AddCelltoBody(table, "............", false);
            document.Add(table);

            string text = "";
            text = "";
            text = "\n\nNO DUE CERTIFICATE";
            Paragraph p = new Paragraph(text);
            p.Font.Size = 10;
            p.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);
            text = "To, \n";
            text = text + " Mr. " + dataSet.Tables["Lead"].Rows[0]["CustName"].ToString() + ".\n";
            text = text + " S/o -:" + dataSet.Tables["Lead"].Rows[0]["FatherName"].ToString() + "\n";
            Paragraph p1 = new Paragraph(text);
            p1.Font.Size = 10;
            p1.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p1.Alignment = Element.ALIGN_LEFT;
            document.Add(p1);


            text = "Dear Customer,\n";
            text = text + "This is to certify that a Personal Loan was given to Mr. ……………………..vide Loan account no. ……………..dated …………………….of Rs. ……………………../- (Rupees ………………………), said loan amount has been fully paid by borrower to us & there is no outstanding liability stand against him as on date.:-\n";
            Paragraph p4 = new Paragraph(text);
            p4.Font.Size = 10;
            p4.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p4.Alignment = Element.ALIGN_LEFT;
            document.Add(p4);

            text = "";
            text = text + "\nLooking forward to your continued patronage and more opportunities to be of service to you ..";
            text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
            Paragraph p7 = new Paragraph(text);
            p7.Font.Size = 10;
            p7.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p7.Alignment = Element.ALIGN_LEFT;
            document.Add(p7);
            document.Close();
            //// Close the document  

            //// Close the writer instance  
            //writer.Close();
            //// Always close open filehandles explicity  
            //fs.Close();           
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }

    public class clsTestLetter : IDisposable
    {
        public static int TABLEWIDTH = 100;
        public void GenSanctionLetter(string FilePath, string LetterName)
        {
            Document document = new Document();
            document = clsCommonPDF.Letter(FilePath, LetterName);
            document.Open();
            //document.Close();
            PdfPTable table = new PdfPTable(2);

            float[] headers = { 60, 40 };
            table.SetWidths(headers);
            table.WidthPercentage = TABLEWIDTH;



            string text = "Dear Customer,";
            text = text + "\nWe are glad to inform you that at your request, the following facilities have been sanctioned/renewed as per the details furnished below and, on the terms, & conditions mentioned herein as well those mentioned in the loan documents\n\n";

            Paragraph p3 = new Paragraph(text);
            p3.Font.Size = 10;
            p3.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p3.Alignment = Element.ALIGN_LEFT;
            document.Add(p3);

            text = "Additional Condition to Comply prior to disbursal: -\n";
            text = text + "\nKindly use the Prospect No. as mentioned above in all your further communication with us. Please put your signature as token of your acceptance of the above stated terms & condition and retain a copy with yourself.";
            text = text + "\nIn case of any query or assistance please contact your Branch manager or alternatively you can Email us at info@suneetfinman.comor our corporate office address is 6th Milestone, Yuvraj complex, Delhi UP Border, Chikemberpur, Ghaziabad 201006 or contact us at our landline number 0120-4165439";
            text = text + "\nThe loan sanction letter is valid for a period of 30 from the date of issuance. The borrower must accept the offer within this period by providing all required documentation and signing the loan agreement.If the borrower fails to accept the offer within the validity period, the loan sanction letter will be deemed null and void, and the borrower will be required to submit a new loan application for reconsideration. This validity period clause is subject to the laws and regulations governing loan agreements in the jurisdiction where the loan is being disbursed";
            text = text + "\nThe loan sanction letter is valid for a period of 30 from the date of issuance. The borrower must accept the offer within this period by providing all required documentation and signing the loan agreement.If the borrower fails to accept the offer within the validity period, the loan sanction letter will be deemed null and void, and the borrower will be required to submit a new loan application for reconsideration. This validity period clause is subject to the laws and regulations governing loan agreements in the jurisdiction where the loan is being disbursed";
            text = text + "\nIt is the borrower's responsibility to keep the lender informed of any changes in their financial or personal circumstances that may affect their ability to repay the loan.";
            text = text + "\nAll the terms & conditions mentioned in the sanction letter has conveyed & accepted by the customer.If the borrower accepts the loan sanction offer within the validity period, the loan agreement will be executed, and the loan amount will be disbursed in accordance with the terms and conditions of the loan agreement.";
            text = text + "\nWe value your relationship with us and assure you of our best services always.";
            text = text + "\n\n\n Best Regards\n\n\n For SuneetFinman Private Limited";
            Paragraph p5 = new Paragraph(text);
            p5.Font.Size = 10;
            p5.Font.Color = iTextSharp.text.BaseColor.BLACK;
            p5.Alignment = Element.ALIGN_LEFT;
            document.Add(p5);
            document.Close();
            //// Close the document  

            //// Close the writer instance  
            //writer.Close();
            //// Always close open filehandles explicity  
            //fs.Close();           
        }
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;

        }
    }
}

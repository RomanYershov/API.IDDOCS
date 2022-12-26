using Domain.Entities;
using System;
using Domain.Enums;
using System.IO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Domain.Extentions
{
    public static class DocExtentions
    {
        public  static FileContentResult ToExcel(this IdDoc doc)
        {
            using(XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Report");

                worksheet.Cell("A1").Value = "ID";
                worksheet.Cell("B1").Value = "Name";
                worksheet.Cell("C1").Value = "Type";
                
                worksheet.Row(1).Style.Font.Bold = true;

                SetCells(worksheet, doc, 0);


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Report_{doc.Name}_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

        public static FileContentResult ToExcel(this List<IdDoc> docs)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Report");

                worksheet.Cell("A1").Value = "ID";
                worksheet.Cell("B1").Value = "Name";
                worksheet.Cell("C1").Value = "Type";

                worksheet.Row(1).Style.Font.Bold = true;

                for(int i = 0; i < docs.Count; i++)
                    SetCells(worksheet, docs[i], i);


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Report_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }


        public static String DocTypeDescription(this DocType type)
        {
            String description = String.Empty;

            switch (type)
            {
                case DocType.Accounting:
                    description = "Бухгалтерский";
                    break;
                case DocType.Legal:
                    description = "Юридический";
                    break;
                case DocType.Official:
                    description = "Служебные";
                    break;
                default:
                    description = "Неизвестный тип док-та";
                    break;
            }

            return description;
        }

        private static void SetCells(IXLWorksheet worksheet, IdDoc doc, int index)
        {
            worksheet.Cell(index + 2, 1).Value = doc.ID.ToString();
            worksheet.Cell(index + 2, 2).Value = doc.Name;
            worksheet.Cell(index + 2, 3).Value = doc.Type.DocTypeDescription();
        }
    }
}

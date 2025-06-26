using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;

namespace Warehouse.Models
{
    public static class ReportGenerator
    {
        public static async void GenerateDefaultReport(List<Item> items)
        {
            var wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = true;
            var doc = wordApp.Documents.Add();
            var titleRange = doc.Range();
            titleRange.Text = "Отчёт по товарам на складе";
            titleRange.Font.Size = 16;
            titleRange.Font.Bold = 1; 
            titleRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            titleRange.InsertParagraphAfter();
            var dateRange = doc.Range();
            dateRange.Text = $"Дата: {DateTime.Now.ToShortDateString()}";
            dateRange.Font.Size = 12;
            dateRange.InsertParagraphAfter();
            object missing = Type.Missing; 
            var table = doc.Tables.Add(doc.Range(ref missing, ref missing), items.Count + 1, 6, ref missing, ref missing);
            table.Borders.Enable = 1; 
            table.Cell(1, 1).Range.Text = "Название";
            table.Cell(1, 2).Range.Text = "Описание";
            table.Cell(1, 3).Range.Text = "Цена";
            table.Cell(1, 4).Range.Text = "Тип";
            table.Cell(1, 5).Range.Text = "Производитель";
            table.Cell(1, 6).Range.Text = "Годен до";

            for (int i = 0; i < items.Count; i++)
            {
                var row = i + 2;
                table.Cell(row, 1).Range.Text = items[i].Name;
                table.Cell(row, 2).Range.Text = items[i].Description;
                table.Cell(row, 3).Range.Text = items[i].Price.ToString();
                table.Cell(row, 4).Range.Text = items[i].ItemType.ToString();
                table.Cell(row, 5).Range.Text = items[i].Creator.ToString();
                table.Cell(row, 6).Range.Text = items[i].ToDate?.ToString("dd.MM.yyyy") ?? "-";
            }

            wordApp.Visible=true;
        }
    }
}

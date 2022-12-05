using Lite_Ceep_Store.Assets;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Lite_Ceep_Store.Service
{
    public class CheckService
    {
        private const string PATH = @"Assets\TemplateCheck.docx";
        private static Dictionary<string, string> _replacePatterns;
        public void GetCheck(string NAME, string DESCRIPTION, string PRICE, string KEY)
        {
            _replacePatterns = new()
            {
                { "NAME", Global.CurrentUser.Name},
                { "ORDER_NUMBER", $"{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}"},
                { "ORDER_DATE", $"{DateTime.Now:dddd d MMMM yyy}" },
                { "EMAIL", Global.CurrentUser.Username },
                { "KEY", KEY }
            };
            //Load a document.
            using DocX document = DocX.Load(Path.GetFullPath(PATH).Replace(@"\bin\Debug\net7.0-windows\", @"\"));
            // Check if all the replace patterns are used in the loaded document.
            document.ReplaceText("<(.*?)>", ReplaceFunc);

            var t = document.AddTable(2, 3);
            t.Design = TableDesign.MediumList2;
            t.Alignment = Alignment.center;
            t.Rows[0].Cells[0].Paragraphs[0].Append("Название:").Font("Open Sans SemiBold").FontSize(14);
            t.Rows[0].Cells[1].Paragraphs[0].Append("Описание: ").Font("Open Sans SemiBold").FontSize(14);
            t.Rows[0].Cells[2].Paragraphs[0].Append("Цена: ").Font("Open Sans SemiBold").FontSize(14);

            t.Rows[1].Cells[0].Paragraphs[0].Append(NAME).Font("Open Sans SemiBold").FontSize(14);
            t.Rows[1].Cells[1].Paragraphs[0].Append(DESCRIPTION).Font("Open Sans SemiBold").FontSize(14);
            t.Rows[1].Cells[2].Paragraphs[0].Append(PRICE).Font("Open Sans SemiBold").FontSize(14);

            document.InsertTable(t);
            SaveFileDialog sfd = new()
            {
                FileName = "Чек",
                DefaultExt = ".docx",
                AddExtension = true,
                Filter = "Документ Word (*.docx)|*.docx",
                Title = "Сохранение отчета"
            };
            if (sfd.ShowDialog() == true)
            {
                document.SaveAs(sfd.FileName);
            }
        }
        private static string ReplaceFunc(string findStr)
        {
            if (_replacePatterns.ContainsKey(findStr))
            {
                return _replacePatterns[findStr];
            }
            return findStr;
        }
    }
}

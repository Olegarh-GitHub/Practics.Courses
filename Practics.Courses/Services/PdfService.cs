using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Practics.Courses.Contexts;
using Practics.Courses.Models;

namespace Practics.Courses.Services
{
    public class PdfService
    {
        public void ProcessPdf()
        {
            int yOffset = 30;
            var row = 1;
            var filename = $"Физические_лица_{Guid.NewGuid()}";

            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 14);
            var pen = new XPen(XColors.Black);
            var fontBold = new XFont("Arial", 15, XFontStyle.Bold);

            var textBrush = new XSolidBrush(XColor.FromArgb(0, 0, 0, 0));
            var backgroundBrush = new XSolidBrush(XColor.FromArgb(0, 255, 255, 255));

            gfx.DrawRectangle(backgroundBrush, new XRect(0, 0, page.Width, page.Height));

            gfx.DrawString("Идентификатор", fontBold, textBrush, new XPoint(10, row * yOffset));
            gfx.DrawString("Имя", fontBold, textBrush, new XPoint(160, row * yOffset));
            gfx.DrawString("Фамилия", fontBold, textBrush, new XPoint(260, row * yOffset));
            gfx.DrawString("Отчество", fontBold, textBrush, new XPoint(360, row * yOffset));
            gfx.DrawString("День рождения", fontBold, textBrush, new XPoint(460, row * yOffset));

            gfx.DrawLine(pen, new XPoint(0, 45), new XPoint(page.Width, 45));
            
            gfx.DrawLine(pen, new XPoint(30, 45), new XPoint(30, page.Height));
            gfx.DrawLine(pen, new XPoint(140, 0), new XPoint(140, page.Height));
            gfx.DrawLine(pen, new XPoint(240, 0), new XPoint(240, page.Height));
            gfx.DrawLine(pen, new XPoint(340, 0), new XPoint(340, page.Height));
            gfx.DrawLine(pen, new XPoint(450, 0), new XPoint(450, page.Height));

            row++;

            using (var db = new ApplicationContext())
            {
                List<Person> persons = db.Persons.ToList();
                
                foreach (Person person in persons)
                {
                    gfx.DrawString(person.Id.ToString(), font, textBrush, new XPoint(10, row * yOffset));
                    gfx.DrawString(person.FirstName, font, textBrush, new XPoint(160, row * yOffset));
                    gfx.DrawString(person.LastName, font, textBrush, new XPoint(260, row * yOffset));
                    gfx.DrawString(person.MiddleName, font, textBrush, new XPoint(360, row * yOffset));
                    gfx.DrawString(person.Birthday.ToShortDateString(), font, textBrush, new XPoint(460, row * yOffset));
                    row++;
                }
            }

            var dlg = new SaveFileDialog();
            dlg.FileName = filename;
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF documents (.pdf)|*.pdf";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                document.Save(dlg.FileName);
                Process.Start(dlg.FileName);
            }
        }
    }
}
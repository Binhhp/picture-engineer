
using PictureEngineer.PDFConversion.Models;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureEngineer.PDFConversion
{
    public static class CreateDocx
    {
        public static ResponseFromCreateDocx Run(List<ResultObj> obj)
        {
            ResponseFromCreateDocx response = new ResponseFromCreateDocx();

            DocumentCore document = new DocumentCore();

            CharacterStyle style = new CharacterStyle("Red");
            style.CharacterFormat.FontName = "Times New Roman";
            style.CharacterFormat.Size = 13;

            ParagraphStyle paragraphStyle = new ParagraphStyle("ParagraphStyle1");
            paragraphStyle.CharacterFormat.FontName = "Times New Roman";
            paragraphStyle.CharacterFormat.Size = 14;
            paragraphStyle.ParagraphFormat.Alignment = HorizontalAlignment.Center;

            document.Styles.Add(style);
            document.Styles.Add(paragraphStyle);

            foreach (var item in obj)
            {
                if (!string.IsNullOrWhiteSpace(item.Text))
                {
                    response.TextDetector = response.TextDetector + item.Text;
                    document.Content.End.Insert(item.Text, new CharacterFormat() { Bold = true, FontName = "Times New Roman", Size = 13 });
                }
                else
                {
                    using (Stream memoryStream = new MemoryStream(item.Image))
                    {
                        Picture picture = new Picture(document, memoryStream);

                        document.Content.End.Insert(picture.Content);
                    }
                }
            }
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, new DocxSaveOptions());
                response.Docx = stream.ToArray();
            }

            return response;
        }
    }
}

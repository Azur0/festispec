using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace FestiSpec.SharedCode.SharedControllers
{
    public class FileUpload
    {
        public static async void SaveReportToDrive(PdfDocument report)
        {
            MemoryStream stream = new MemoryStream();
            report.Save(stream, false);
            StreamContent content = new StreamContent(stream);
            HttpClient client = new HttpClient();
            await client.PostAsync("https://www.googleapis.com/upload/drive/v3/files?uploadType=resumable", content);
            client.Dispose();
        }

        public static async void SaveReportToDrive()
        {
            MemoryStream stream = new MemoryStream();
            PdfDocument temp = new PdfDocument();
            temp.InsertPage(0, new PdfPage());
            temp.Save(stream, false);
            StreamContent content = new StreamContent(stream);
            HttpClient client = new HttpClient();
            await client.PostAsync("https://www.googleapis.com/upload/drive/v3/files?uploadType=resumable", content);
            client.Dispose();
        }
    }
}

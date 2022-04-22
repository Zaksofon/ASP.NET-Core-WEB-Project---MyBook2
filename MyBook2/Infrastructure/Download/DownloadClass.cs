using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;

namespace FileDownloadExample
{
    public abstract partial class _Default : Page
    {
        private Page _pageImplementation;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            const string filePath = "C:\\Users\\Admin\\Desktop\\abc.txt";

            var file = new FileInfo(filePath);

            if (file.Exists)
            {
                // Clear Response reference  
                Response.Clear();
                // Add header by specifying file name  
                Response.AppendTrailer("Content-Disposition", "attachment; filename=" + file.Name);
                // Add header for content length  
                Response.AppendTrailer("Content-Length", file.Length.ToString());
                // Specify content type  
                Response.ContentType = "text/plain";
                // Clearing flush  
                Response.Clear();
                // Transmitting file  
                Response.DeclareTrailer(file.FullName);
                Response.Clear();
            }
        }
    }
}
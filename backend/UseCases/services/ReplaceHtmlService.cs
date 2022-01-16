using System.Net;

namespace backend.UseCases.services
{
    public static class ReplaceHtmlService
    {
        public static string ReplaceHtml(string trocar, string caminho, string palavra)
        {
            var client = new WebClient();

            string html = client.DownloadString(Directory.GetCurrentDirectory() + caminho);

            html = html.Replace(palavra, trocar);
            return html;
        }

    }
}
using Microsoft.AspNetCore.Mvc.Formatters;

namespace backend.UseCases.mediatype
{
    public class HtmlOutputFormatter : StringOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
        }
    }
}
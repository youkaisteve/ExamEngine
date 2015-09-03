using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json.Converters;

namespace Exam.Api.Framework
{
    public class ApiJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type,
            HttpRequestMessage request,
            MediaTypeHeaderValue mediaType)
        {
            var formatter = new ApiJsonMediaTypeFormatter();
            formatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            return formatter;
        }
    }
}
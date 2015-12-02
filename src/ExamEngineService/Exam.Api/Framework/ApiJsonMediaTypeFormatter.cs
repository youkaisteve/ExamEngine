using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Exam.Model;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Converters;
using Component.Tools;

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

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            //string typeNameUpper = type.GetTypeInfo().Name.ToUpper();
            //string baseTypeNameUpper = type.GetTypeInfo().BaseType != null ? type.GetTypeInfo().BaseType.Name.ToUpper() : "";
            if (PublicFunc.MatchType(type, "MODELBASE"))
            {
                var result = base.ReadFromStreamAsync(type, readStream, content, formatterLogger).Result as ModelBase;
                if (result != null)
                {
                    var headers = content.Headers;
                    result.User = new UserInfo
                    {
                        UserID = headers.GetValues("UserID").FirstOrDefault(),
                        UserName = headers.GetValues("UserName").FirstOrDefault(),
                        UserSysNo = int.Parse(headers.GetValues("UserSysNo").FirstOrDefault())
                    };
                }
                return Task.Run<object>(() => result);
            }
            return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
        }
    }
}
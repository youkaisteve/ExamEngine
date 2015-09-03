//-------------------------------------------------------------------------------------------------------------
// <copyright file="ApiJsonMediaTypeFormatter.cs" company="Sotao Network Technology Corporation">
//     AUTHOR:      Roye
//     YEAR:            2015
//     Copyright (c) 2015-2020 Sotao Network Technology Co., Ltd. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------------------

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
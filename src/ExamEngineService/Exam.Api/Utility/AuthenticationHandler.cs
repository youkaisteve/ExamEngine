using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Exam.Api.Utility
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                HttpRequestHeaders headers = request.Headers;
                if (headers.Authorization != null)
                {
                    var header = new Header(request.Headers.Authorization.Parameter, request.Method.Method);

                    if (Nonce.IsValid(header.Nonce, header.NounceCounter))
                    {
                        // Just assuming password is same as username for the purpose of illustration
                        string password = header.UserName;

                        string ha1 = String.Format("{0}:{1}:{2}", header.UserName, header.Realm, password).ToMd5Hash();

                        string ha2 = String.Format("{0}:{1}", header.Method, header.Uri).ToMd5Hash();

                        string computedResponse = String.Format("{0}:{1}:{2}:{3}:{4}:{5}",
                            ha1, header.Nonce, header.NounceCounter, header.Cnonce, "auth", ha2).ToMd5Hash();

                        if (String.CompareOrdinal(header.Response, computedResponse) == 0)
                        {
                            // digest computed matches the value sent by client in the response field.
                            // Looks like an authentic client! Create a principal.
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, header.UserName),
                                new Claim(ClaimTypes.AuthenticationMethod, AuthenticationMethods.Password)
                            };

                            var principal = new ClaimsPrincipal(new[] {new ClaimsIdentity(claims, "Digest")});

                            Thread.CurrentPrincipal = principal;

                            if (HttpContext.Current != null)
                                HttpContext.Current.User = principal;
                        }
                    }
                }

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Digest",
                        Header.UnauthorizedResponseHeader.ToString()));
                }

                return response;
            }
            catch (Exception)
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Digest",
                    Header.UnauthorizedResponseHeader.ToString()));

                return response;
            }
        }
    }
}
using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace APITest.OAuth2
{
    public class OpenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //UnitOfWork unitWork = new UnitOfWork();
        /// <summary>
        /// 验证 client 信息
        /// </summary>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //string clientId;
            //string clientSecret;
            //if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            //{
            //    context.TryGetFormCredentials(out clientId, out clientSecret);
            //}

            //if (clientId != "xishuai" || clientSecret != "123")
            //{
            //    context.SetError("invalid_client", "client or clientSecret is not valid");
            //    return;
            //}
            //暂时不验证客户端信息，只提供密码形式的验证
            context.Validated();
        }

        /// <summary>
        /// 生成 access_token（client credentials 授权方式）
        /// </summary>
        public override async Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(
                context.ClientId, OAuthDefaults.AuthenticationType),
                context.Scope.Select(x => new Claim("urn:oauth:scope", x)));

            context.Validated(identity);
        }

        /// <summary>
        /// 生成 access_token（resource owner password credentials 授权方式）
        /// </summary>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (string.IsNullOrEmpty(context.UserName))
            {
                context.SetError("invalid_username", "username is not valid");
                return;
            }
            if (string.IsNullOrEmpty(context.Password))
            {
                context.SetError("invalid_password", "password is not valid");
                return;
            }
            //校验
            //UserInfo userInfo = new UserInfo();
            //userInfo = unitWork.DUserInfo.Get(x => x.UserName == context.UserName && x.Pwd == context.Password).FirstOrDefault();
            //if (userInfo==null)
            //{
            //    context.SetError("invalid_identity", "username or password is not valid");
            //    return;
            //}

            var OAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            OAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            context.Validated(OAuthIdentity);
            context.Request.Context.Authentication.SignIn(OAuthIdentity);
        }

        /// <summary>
        /// 生成 authorization_code（authorization code 授权方式）、生成 access_token （implicit 授权模式）
        /// </summary>
        //public override async Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        //{
        //    if (context.AuthorizeRequest.IsImplicitGrantType)
        //    {
        //        //implicit 授权方式
        //        var identity = new ClaimsIdentity("Bearer");
        //        context.OwinContext.Authentication.SignIn(identity);
        //        context.RequestCompleted();
        //    }
        //    else if (context.AuthorizeRequest.IsAuthorizationCodeGrantType)
        //    {
        //        //authorization code 授权方式
        //        var redirectUri = context.Request.Query["redirect_uri"];
        //        var clientId = context.Request.Query["client_id"];
        //        var identity = new ClaimsIdentity(new GenericIdentity(
        //            clientId, OAuthDefaults.AuthenticationType));

        //        var authorizeCodeContext = new AuthenticationTokenCreateContext(
        //            context.OwinContext,
        //            context.Options.AuthorizationCodeFormat,
        //            new AuthenticationTicket(
        //                identity,
        //                new AuthenticationProperties(new Dictionary<string, string>
        //                {
        //                    {"client_id", clientId},
        //                    {"redirect_uri", redirectUri}
        //                })
        //                {
        //                    IssuedUtc = DateTimeOffset.UtcNow,
        //                    ExpiresUtc = DateTimeOffset.UtcNow.Add(context.Options.AuthorizationCodeExpireTimeSpan)
        //                }));

        //        await context.Options.AuthorizationCodeProvider.CreateAsync(authorizeCodeContext);
        //        context.Response.Redirect(redirectUri + "?code=" + Uri.EscapeDataString(authorizeCodeContext.Token));
        //        context.RequestCompleted();
        //    }
        //}

        /// <summary>
        /// 验证 authorization_code 的请求
        /// </summary>
        public override async Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            if (context.AuthorizeRequest.ClientId == "xishuai" &&
                (context.AuthorizeRequest.IsAuthorizationCodeGrantType || context.AuthorizeRequest.IsImplicitGrantType))
            {
                context.Validated();
            }
            else
            {
                context.Rejected();
            }
        }

        /// <summary>
        /// 验证 redirect_uri
        /// </summary>
        public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            context.Validated(context.RedirectUri);
        }

        /// <summary>
        /// 验证 access_token 的请求
        /// </summary>
        public override async Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            if (context.TokenRequest.IsAuthorizationCodeGrantType || context.TokenRequest.IsRefreshTokenGrantType || context.TokenRequest.IsResourceOwnerPasswordCredentialsGrantType)
            {
                context.Validated();
            }
            else
            {
                context.Rejected();
            }
        }
        public override async  Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            
        }
    }
}

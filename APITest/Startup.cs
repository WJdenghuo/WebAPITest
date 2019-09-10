using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Threading.Tasks;
using APITest.OAuth2;

[assembly: OwinStartup(typeof(APITest.Startup))]

namespace APITest
{
    public class Startup
    {       
        public void Configuration(IAppBuilder app)
        {

            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenEndpointPath = new PathString("/token"), //获取 access_token 认证服务请求地址
                AuthorizeEndpointPath = new PathString("/authorize"), //获取 authorization_code 认证服务请求地址
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(100), //access_token 过期时间

                Provider = new OpenAuthorizationServerProvider(), //access_token 相关认证服务
                //AuthorizationCodeProvider = new OpenAuthorizationCodeProvider(), //authorization_code 认证服务
                RefreshTokenProvider = new OpenRefreshTokenProvider() //refresh_token 认证服务
            };
            app.UseOAuthBearerTokens(OAuthOptions); //表示 token_type 使用 bearer 方式

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                //从url中获取token，兼容hearder方式
                Provider = new QueryStringOAuthBearerProvider("access_token")
            });
        }

        public class QueryStringOAuthBearerProvider : OAuthBearerAuthenticationProvider
        {
            readonly string _name;

            public QueryStringOAuthBearerProvider(string name)
            {
                _name = name;
            }

            public override Task RequestToken(OAuthRequestTokenContext context)
            {
                var value = context.Request.Query.Get(_name);

                if (!string.IsNullOrEmpty(value))
                {
                    context.Token = value;
                }

                return Task.FromResult<object>(null);
            }
        }
    }
}

using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPITest.OAuth2;

[assembly: OwinStartup(typeof(WebAPITest.Startup))]
namespace WebAPITest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"), //获取 access_token 授权服务请求地址
                AuthorizeEndpointPath = new PathString("/authorize"), //获取 authorization_code 授权服务请求地址
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(100), //access_token 过期时间

                Provider = new OpenAuthorizationServerProvider(), //access_token 相关授权服务
            };

            app.UseOAuthBearerTokens(OAuthOptions); //表示 token_type 使用 bearer 方式 不记名令牌验证
        }
    }
}
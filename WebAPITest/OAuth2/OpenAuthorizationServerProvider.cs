using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebAPITest.OAuth2
{
    public class OpenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 验证调用端的clientid与clientSecret已验证调用端的合法性(clientid、clientSecret为约定好的字符串)。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            context.TryGetBasicCredentials(out clientId, out clientSecret);
            //if (clientId == "1234" && clientSecret == "5678")
            //{
            //    context.Validated(clientId);
            //}
            //暂时不验证客户端信息，只提供密码形式的验证
            context.Validated();
            await base.ValidateClientAuthentication(context);
        }

        /// <summary>
        /// 通过重载GrantResourceOwnerCredentials获取用户名和密码进行认证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //调用后台的登录服务验证用户名与密码
            if (context.UserName != "Admin" || context.Password != "123456")
            {
                context.SetError("invalid_grant", "用户名或密码不正确。");
                return;
            }

            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            await base.GrantResourceOwnerCredentials(context);
        }
    }
}
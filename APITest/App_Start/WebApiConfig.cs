using APITest.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APITest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //自定义过滤器
            config.Filters.Add(new WebApiExceptionFilterAttribution());
        }
    }
}

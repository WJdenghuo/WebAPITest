using APITest.Models;
using Edu.Tools;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace APITest.Filters
{
    public class WebApiExceptionFilterAttribution: ExceptionFilterAttribute,IExceptionFilter
    {
        protected log4net.ILog logger = LogHelper.GetInstance().Log;
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            #region 输出错误日志
            string errorMsg = String.Empty;
            if (actionExecutedContext.Response==null)
            {
                errorMsg = string.Format("【错误代码】：{0} <br>【异常类型】：{1} <br>【异常信息】：{2} <br>【堆栈调用】：{3} <br>【用户】：{4} <br>【错误项目】：{5}", new object[] { 500, actionExecutedContext.Exception.GetType().Name, actionExecutedContext.Exception.Message, actionExecutedContext.Exception.StackTrace, "" + "<br>", "WEBAPI<br>" + actionExecutedContext.Request.RequestUri});
            }
            else
            {
                errorMsg = string.Format("【错误代码】：{0} <br>【异常类型】：{1} <br>【异常信息】：{2} <br>【堆栈调用】：{3} <br>【用户】：{4} <br>【错误项目】：{5}", new object[] { (Int32)actionExecutedContext.Response.StatusCode, actionExecutedContext.Exception.GetType().Name, actionExecutedContext.Exception.Message, actionExecutedContext.Exception.StackTrace, "" + "<br>", "WEBAPI<br>" + actionExecutedContext.Response.RequestMessage.RequestUri.AbsoluteUri });
            }
            
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">【位置】</strong>");
            logger.Error(errorMsg);
            #endregion

            #region web项目
            //actionExecutedContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK){ ReasonPhrase = "api error"};
            #endregion

            #region 移动端项目 
            Result<String> result = new Result<string>()
            {
                R = "0",
                Data = null,
                M = "webapi错误" + actionExecutedContext.Exception.Message,
                Total = 0
            };          
            actionExecutedContext.Response=actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, result);
            #endregion

           
        }
    }
}

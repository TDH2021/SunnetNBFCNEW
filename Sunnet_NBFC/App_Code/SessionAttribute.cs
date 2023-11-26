using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace Sunnet_NBFC.App_Code
{
    public class SessionAttribute:ActionFilterAttribute
    {
        public enum DeviceType { Mobile, Desktop }
        public string RedirectController { get; set; } = "Login";

        public string RedirectAction { get; set; } = "Index";

        public DeviceType RedirectOnDevice { get; set; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Log("OnActionExecuted", filterContext.RouteData);

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //if (filterContext.HttpContext.GetOverriddenBrowser().IsMobileDevice == (this.RedirectOnDevice == DeviceType.Mobile))
            //{
            //    this.RedirectToRoute(filterContext, new { controller = this.RedirectController, action = this.RedirectAction });
            //}
            if (HttpContext.Current.Session["UserId"] == null)
            {
                //var descriptor = filterContext.ActionDescriptor;
                //RedirectController = descriptor.ControllerDescriptor.ControllerName;
                //var actionName = descriptor.ActionName;
                
                this.RedirectToRoute(filterContext, new { controller = this.RedirectController, action = this.RedirectAction });

            }
            
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Log("OnResultExecuting", filterContext.RouteData);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
           // Log("OnResultExecuted", filterContext.RouteData);
        }
        
        private void RedirectToRoute(ActionExecutingContext context, object routeValues)
        {
            var rc = new RequestContext(context.HttpContext, context.RouteData);
            var virtualPathData = RouteTable.Routes.GetVirtualPath(rc, new RouteValueDictionary(routeValues));
            if (virtualPathData != null)
            {
                string url = virtualPathData.VirtualPath;
                context.HttpContext.Response.Redirect(url, true);
            }
        }

       
    }

    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            // log the error by using your own method
            LogError(filterContext.Exception.Message, filterContext.RouteData);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
        private void LogError(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            string message = methodName + " Controller:" + controllerName + " Action:" + actionName + " Date: "
                            + DateTime.Now.ToString() + Environment.NewLine;

            //saving the data in a text file called Log.txt
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/ErrorLogs/Log.txt"), message);
        }
    }

}
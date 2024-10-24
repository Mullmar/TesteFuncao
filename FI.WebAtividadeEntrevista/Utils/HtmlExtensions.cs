using System.Web.Mvc;
using System.IO;
using System.Web.Routing;

public static class HtmlExtensions
{
    public static string RenderActionToString(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues = null)
    {
        var controllerContext = htmlHelper.ViewContext.Controller.ControllerContext;
        var routeData = new System.Web.Routing.RouteData();
        routeData.Values.Add("action", actionName);
        routeData.Values.Add("controller", controllerName);

        if (routeValues != null)
        {
            foreach (var key in new System.Web.Routing.RouteValueDictionary(routeValues))
            {
                routeData.Values.Add(key.Key, key.Value);
            }
        }

        var requestContext = new RequestContext(htmlHelper.ViewContext.HttpContext, routeData);
        var controller = ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, controllerName) as Controller;
        controller.ControllerContext = new ControllerContext(requestContext, controller);

        var oldWriter = htmlHelper.ViewContext.Writer;
        using (var sw = new StringWriter())
        {
            htmlHelper.ViewContext.Writer = sw;
            controller.ControllerContext.HttpContext.Response.Output = sw;
            var result = controller.ActionInvoker.InvokeAction(controller.ControllerContext, actionName);
            htmlHelper.ViewContext.Writer = oldWriter;
            var retorno = sw.ToString();
            return retorno;
        }
    }
}

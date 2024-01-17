using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

public class ActionsAttribute : ActionFilterAttribute // used the concept of filter here
{
    Stopwatch? watch;  // used to measure how long things take/time related 

    public override void OnActionExecuted(ActionExecutedContext filterContext) // Does something after the action executes.
    {
        watch?.Stop();

        Action("OnActionExecuted", filterContext.RouteData);
        // filterContext.HttpContext.Response.WriteAsync("Action Time"+
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        Action("OnActionExecuting", filterContext.RouteData); //This method is called before a controller action is executed.
        watch = Stopwatch.StartNew();
    }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        Action("OnResultExecuted", filterContext.RouteData);
    }

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        Action("OnResultExecuting", filterContext.RouteData); //This method is called before a controller action result is executed.
    }

    private void Action(string methodName, RouteData routeData)
    {
        var controllerName = routeData.Values["controller"]; // used for accessing the values inserted by  classes handling routing
        var actionName = routeData.Values["action"]; // the paramteres here are controller,action,id
        var message =
            methodName
            + " -Controller:"
            + controllerName
            + ",Action:"
            + actionName
            + ",Time of Use:"
            + watch?.ElapsedMilliseconds.ToString()
            + "\n";
        Console.WriteLine(message);
    }
}

public class ExceptionFilter : ActionFilterAttribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Exception exception=context.Exception;
   
        Console.WriteLine(exception);
        context.ExceptionHandled = true;
        context.Result = new ViewResult() { ViewName = "Exception" };
    }
}

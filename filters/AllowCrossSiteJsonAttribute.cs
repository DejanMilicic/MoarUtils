﻿using System.Web.Mvc;

namespace MoarUtils.filters {
  public class AllowCrossSiteJsonAttribute : ActionFilterAttribute {
    public override void OnActionExecuting(ActionExecutingContext filterContext) {
      filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
      base.OnActionExecuting(filterContext);
    }
  }
}
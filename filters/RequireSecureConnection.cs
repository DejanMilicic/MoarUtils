﻿using System;
using System.Web.Mvc;

namespace MoarUtils.filters {
  public class RequireSecureConnection : RequireHttpsAttribute {
    public override void OnAuthorization(AuthorizationContext filterContext) {
      if (filterContext == null) {
        throw new ArgumentNullException("filterContext");
      }

      if (filterContext.HttpContext.Request.IsLocal) {
        // when connection to the application is local, don't do any HTTPS stuff
        return;
      }

      base.OnAuthorization(filterContext);
    }
  }
}
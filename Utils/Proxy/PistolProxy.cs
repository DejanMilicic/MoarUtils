﻿using MoarUtils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoarUtils.Utils.Proxy {
  public class PistolProxy {
    public const string PISTOL_PROXY_HOST = "bang.pistolproxy.com";
    public const int PISTOL_PROXY_PORT = 8008;

    public static WebProxy GetWebProxy(
      DesiredProxyType dpt = DesiredProxyType.shared
      //int defaultTimeoutSeconds = 5000
    ) {
      var wp = new WebProxy(PISTOL_PROXY_HOST, PISTOL_PROXY_PORT);
      switch (dpt) {
        case DesiredProxyType.dedicated:
          wp.Credentials = new NetworkCredential("dedicated", "dedicated");
          break;
        case DesiredProxyType.open:
          wp.Credentials = new NetworkCredential("open", "open");
          break;
        case DesiredProxyType.shared:
          wp.Credentials = new NetworkCredential("shared", "shared");
          break;
        case DesiredProxyType.random:
          var index = (new Random()).Next(0, 3);
          switch (index) {
            case 0:
              wp.Credentials = new NetworkCredential("open", "open");
              break;
            case 1:
              wp.Credentials = new NetworkCredential("shared", "shared");
              break;
            case 2:
            default:
              wp.Credentials = new NetworkCredential("dedicated", "dedicated");
              break;
          }
          break;
      }
      return wp;
    }
  }
}

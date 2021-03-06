﻿using MoarUtils.Utils;
using MoarUtils.Utils.Proxy;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace MoarUtils.Utils.AutoRefreshedProxyList {
  public class HighlyAvailable {
    #region Usable Web Proxies
    private static Mutex mLouwp = new Mutex();
    private static DateTime lastAggregatedUsableProxies = DateTime.MinValue;
    private static List<WebProxy> _louwp = new List<WebProxy>();
    public static int minimumUptimePercentage = KittenProxy.defaultMinimumUptimePercentage;
    //this must be populated by calling code
    public static string kittenProxyApiKey = "";
    public static string kittenProxyApiHost = "";

    public static List<WebProxy> louwp {
      get {
        lock (mLouwp) {
          //if we have some and then are <2 min old, then return them
          if (lastAggregatedUsableProxies < DateTime.UtcNow.AddMinutes(-20)) {
            //else, get new
            _louwp = KittenProxy.GetUsableWebProxies(
              apiHost: kittenProxyApiHost,
              apiKey: kittenProxyApiKey,
              anonymous: false,
              maxResults: 25,
              minimumUptimePercentage: minimumUptimePercentage
            );
            lastAggregatedUsableProxies = DateTime.UtcNow;
            LogIt.D("total:" + _louwp.Count + ((_louwp.Count == 0) ? "" : ("|first:" + _louwp[0].Address + "|last:" + _louwp[_louwp.Count - 1].Address)));

            //temp
            foreach (var wp in _louwp) {
              LogIt.D(wp.Address);
            }
          }
          return _louwp;
        }
      }
    }
    public static WebProxy GetUsableWebProxy() {
      var lowp = louwp;
      var index = (new Random()).Next(0, lowp.Count);
      return lowp[index];
    }

    public static void RemoveFromPool(WebProxy wp) {
      lock (mLouwp) {
        if (_louwp.Contains(wp)) {
          _louwp.Remove(wp);
          LogIt.D(wp.Address);
        }
      }
    }
    #endregion
  }
}
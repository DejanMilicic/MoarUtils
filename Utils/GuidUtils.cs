﻿using System;

namespace MoarUtils.Utils {
  public class GuidUtils {
    //GUIDs stored as BINARY(16) in MySQL will require this to string compare

    public static Guid FlipEndian(Guid guid) {
      var newBytes = new byte[16];
      var oldBytes = guid.ToByteArray();

      for (var i = 8; i < 16; i++)
        newBytes[i] = oldBytes[i];

      newBytes[3] = oldBytes[0];
      newBytes[2] = oldBytes[1];
      newBytes[1] = oldBytes[2];
      newBytes[0] = oldBytes[3];
      newBytes[5] = oldBytes[4];
      newBytes[4] = oldBytes[5];
      newBytes[6] = oldBytes[7];
      newBytes[7] = oldBytes[6];

      return new Guid(newBytes);
    }
  }
}


using System;

namespace LightControl.Api.Utils
{
  public static class NumberUtil
  {
    public static int GetBase(this string value)
    {
      if (value == null) return 10;
      string lower = value.ToLower();
      if (lower.ToLower().StartsWith("0x"))
        return 16;
      if (lower.ToLower().StartsWith("0b"))
        return 2;
      return 10;
    }
  }
}
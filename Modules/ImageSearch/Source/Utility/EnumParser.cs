/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using System;

namespace Assessment.ImageSearch.Utility
{
    public static class EnumParser
    {
        public static T ToEnum<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}

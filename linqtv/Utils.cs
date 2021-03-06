﻿using System;
using System.Globalization;

namespace Linqtv
{
    internal static class StringUtilities
    {
        public static string Invariant(FormattableString formattable)
        {
            if (formattable == null)
                throw new ArgumentNullException(nameof(formattable));

            return formattable.ToString(CultureInfo.InvariantCulture);
        }
    }
}
using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Xml.Linq;

namespace Linqtv.Model
{
    internal static class ParserUtils
    {
        public static IImmutableList<string> SplitByPipe(this string str) =>
            str?.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToImmutableList();

        public static TEnum? ParseEnum<TEnum>(string enumString) where TEnum : struct
        {
            if (string.IsNullOrEmpty(enumString))
                return null;

            TEnum outEnum;
            if (!Enum.TryParse(enumString, true, out outEnum))
                throw new ArgumentException(StringUtilities.Invariant($"{enumString} doesn't look like a {typeof(TEnum)}"));

            return outEnum;
        }

        public static XElement ElementOrNull(this XElement element, XName name)
        {
            var retElement = element.Element(name);
            var elementValue = retElement?.Value;

            if (string.IsNullOrEmpty(elementValue) || string.IsNullOrWhiteSpace(elementValue))
                return null;

            return retElement;
        }

        public static DateTimeOffset? ElementAsDateTimeOffset(this XElement element, XName name, string dateTimeFormat)
        {
            try
            {
                var valueToParse = (string)element.ElementOrNull(name);
                if (valueToParse == null)
                    return null;

                return
                    new DateTimeOffset(DateTime.ParseExact(valueToParse, dateTimeFormat, CultureInfo.InvariantCulture),
                        TimeSpan.Zero);
            }
            catch (Exception e) when (e.GetType() == typeof(FormatException))
            {
                return null;
            }
        }

        public static DateTimeOffset? ElementAsDateTimeOffset(this XElement element, XName name)
        {
            try
            {
                var valueToParse = element.ElementOrNull(name);
                if (valueToParse == null)
                    return null;

                return DateTimeOffset.FromUnixTimeSeconds((long)valueToParse);
            }
            catch (Exception e) when (e.GetType() == typeof(FormatException))
            {
                return null;
            }
        }

        public static TimeSpan? ElementAsTimeSpan(this XElement element, XName name, string timeSpanFormat)
        {
            try
            {
                var valueToParse = element.ElementOrNull(name);
                if (valueToParse == null)
                    return null;

                return DateTime.ParseExact((string)valueToParse, timeSpanFormat, CultureInfo.InvariantCulture).TimeOfDay;
            }
            catch (Exception e) when (e.GetType() == typeof(FormatException))
            {
                return null;
            }
        }
    }
}
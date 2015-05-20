using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Xml.Linq;

namespace linqtv.Model
{
    public static class ParserUtils
    {
        public static IImmutableList<string> SplitByPipe(this string str) =>
            str?.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToImmutableList();

        public static TEnum? ParseEnum<TEnum>(string enumString) where TEnum : struct
        {
            if (string.IsNullOrEmpty(enumString))
                return null;

            TEnum outEnum;
            if (!Enum.TryParse(enumString, true, out outEnum))
                throw new ArgumentException($"{enumString} doesn't look like a {typeof(TEnum)}");

            return new TEnum?(outEnum);
        }

        public static XElement ElementOrNull(this XElement element, XName name)
        {
            var retElement = element.Element(name);

            if (string.IsNullOrEmpty(retElement?.Value) || string.IsNullOrWhiteSpace(retElement?.Value))
                return null;

            return retElement;
        }

        public static DateTimeOffset? ElementAsDateTimeOffset(this XElement element, XName name, string dateTimeFormat)
        {
            try
            {
                var valueToParse = (string)element.Element(name);
                if (string.IsNullOrEmpty(valueToParse))
                    return null;

                return new DateTimeOffset(DateTime.ParseExact(valueToParse, dateTimeFormat, CultureInfo.InvariantCulture),
                                            TimeSpan.Zero);
            }
            catch
            {
                return null;
            }
        }

        public static DateTimeOffset? ElementAsDateTimeOffset(this XElement element, XName name)
        {
            try
            {
                return DateTimeOffset.FromUnixTimeSeconds((long)element.Element(name));
            }
            catch
            {
                return null;
            }
        }

        public static TimeSpan? ElementAsTimeSpan(this XElement element, XName name, string timeSpanFormat)
        {
            try
            {
                return DateTime.ParseExact((string)element.Element(name), timeSpanFormat, CultureInfo.InvariantCulture).TimeOfDay;
            }
            catch
            {
                return null;
            }
        }
    }
}
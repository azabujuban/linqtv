using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace linqtv.Model
{
    public static class ParserUtils
    {
        public static IImmutableList<string> SplitByPipe(this string str) => str.Split('|').Select(s => s.Trim()).ToImmutableList();

        public static TEnum? ParseEnum<TEnum>(string enumString) where TEnum: struct
        {
            if (string.IsNullOrEmpty(enumString))
                return null;

            TEnum outEnum;
            if (!Enum.TryParse(enumString, true, out outEnum))
                throw new ArgumentException($"{enumString} doesn't look like a {typeof(TEnum)}");

            return new TEnum?(outEnum);


        }
    }
}

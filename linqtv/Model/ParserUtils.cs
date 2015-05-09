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
        public static StatusEnum ParseStatusEnum(string str)
        {
            StatusEnum outEnum;
            if (!Enum.TryParse(str, true, out outEnum))
                throw new ArgumentException($"{str} doesn't look like an {nameof(StatusEnum)}");

            return outEnum;
        }
    }
}

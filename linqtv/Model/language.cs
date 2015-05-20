namespace linqtv.Model
{
    public static class Language
    {
        public static readonly LangInfo English = new LangInfo { ShortName = "en", Id = 7 };
    }

    public class LangInfo
    {
        public string ShortName;
        public short Id;
    }
}
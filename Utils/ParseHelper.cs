namespace SqlHelper.Utils
{
    public static class ParseHelper
    {
        public static int ToInt(string s)
        {
            int.TryParse(s, out int result);
            return result;
        }

        public static long ToLong(string s)
        {
            long.TryParse(s, out long result);
            return result;
        }
    }
}

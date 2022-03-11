namespace PlaceHolder.Methods
{
    public class JsonReturnStandard
    {
        public Dictionary<string, string> SingleReturnJson(string s)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Message", s);

            return result;
        }

        public Dictionary<string, string> SingleReturnJsonError(string s)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Error", s);

            return result;
        }
    }
}

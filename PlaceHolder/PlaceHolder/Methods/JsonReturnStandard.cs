namespace PlaceHolder.Methods
{
    public class JsonReturnStandard
    {
        public Dictionary<string, object> SingleReturnJson(string s)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("success", true);
            result.Add("Message", s);

            return result;
        }

        public Dictionary<string, object> SingleReturnJsonError(string s)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("success", false);
            result.Add("Error", s);

            return result;
        }

        public string test(string connectionUrl)
        {
            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':');

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
    }
}

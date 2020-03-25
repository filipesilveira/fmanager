namespace FManager.Core.Helpers
{
    using Newtonsoft.Json;

    public static class Converts
    {
        public static string ObjectToJson<T>(T obj)
        {
            var setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, setting);
        }

        public static T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

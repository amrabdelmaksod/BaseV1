using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace Hedaya.Dashboard.Models
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments)) : actualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new StreamReader(stream);
            using JsonTextReader jsonTextReader = new JsonTextReader(streamReader);
            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType != JsonToken.PropertyName) continue;

                var key = jsonTextReader.Value as string;
                jsonTextReader.Read();
                var value = _serializer.Deserialize<string>(jsonTextReader);
                yield return new LocalizedString(key, value);
            }
        }

        private string GetString(string key)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath(filePath);
            if (File.Exists(fullFilePath))
            {
                var result = GetValuesFromJson(key, fullFilePath);
                return result;
            }
            return string.Empty;
        }
        private string GetValuesFromJson(string propertyName, string filePath)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;
            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new StreamReader(stream);
            using JsonTextReader jsonTextReader = new JsonTextReader(streamReader);
            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType == JsonToken.PropertyName && jsonTextReader.Value as string == propertyName)
                {
                    jsonTextReader.Read();
                    return _serializer.Deserialize<string>(jsonTextReader);
                }
            }
            return string.Empty;

        }
    }
}

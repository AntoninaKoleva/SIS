using SIS.HTTP.Common;

namespace SIS.HTTP.Header
{
    public class HttpHeader
    {
        public HttpHeader(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.Key = key;
            this.Value = value;
        }

        public string Key { get; }
        public string Value { get; }
        public override string ToString()
        {
            return $"{this.Key}: {this.Value}";
        }
    }
}

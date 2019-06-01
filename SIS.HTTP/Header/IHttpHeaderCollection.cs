namespace SIS.HTTP.Header
{
    public interface IHttpHeaderCollection
    {
        void AddHeader(HttpHeader header);
        bool CotainsHeader(string key);
        HttpHeader GetHeader(string key);
    }
}

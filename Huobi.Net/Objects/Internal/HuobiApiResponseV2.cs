namespace Huobi.Net.Objects.Internal
{
    internal class HuobiApiResponseV2<T>
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
#pragma warning disable 8618
        public T Data { get; set; }
#pragma warning restore 8618
    }
}

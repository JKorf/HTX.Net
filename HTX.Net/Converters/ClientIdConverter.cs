namespace HTX.Net.Converters
{
    internal class ClientIdConverter : ReplaceConverter
    {
        public ClientIdConverter() : base($"{LibraryHelpers.GetClientReference(() => null, "HTX")}{LibraryHelpers.ClientOrderIdSeparator}->") { }
    }
}

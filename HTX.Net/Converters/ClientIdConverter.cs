using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Converters
{
    internal class ClientIdConverter : ReplaceConverter
    {
        public ClientIdConverter() : base($"{HTXExchange.ClientOrderIdPrefix}->") { }
    }
}

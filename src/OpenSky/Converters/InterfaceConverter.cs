using System;
using Newtonsoft.Json.Converters;

namespace OpenSky.Converters
{
    public class InterfaceConverter<TInterface, TClass> : CustomCreationConverter<TInterface> where TClass : TInterface, new()
    {
        public override TInterface Create(Type objectType) => new TClass();
    }
}

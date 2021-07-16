using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace StronglyTypedIds
{
    internal static class EmbeddedSources
    {
        private static readonly Assembly ThisAssembly = typeof(EmbeddedSources).Assembly;
        internal static readonly string StronglyTypedIdAttributeSource = LoadEmbeddedResource("StronglyTypedIds.Templates.Sources.StronglyTypedIdAttribute.cs");
        internal static readonly string StronglyTypedIdBackingTypeSource = LoadEmbeddedResource("StronglyTypedIds.Templates.Sources.StronglyTypedIdBackingType.cs");
        internal static readonly string StronglyTypedIdJsonConverterSource = LoadEmbeddedResource("StronglyTypedIds.Templates.Sources.StronglyTypedIdJsonConverter.cs");

        internal static readonly string GuidBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Guid.Guid_Base.cs");
        internal static readonly string GuidNewtonsoftBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Guid.Guid_NewtonsoftJsonConverter.cs");
        internal static readonly string GuidSystemTextJsonBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Guid.Guid_SystemTextJsonConverter.cs");

        internal static readonly string IntBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Int.Int_Base.cs");
        internal static readonly string IntNewtonsoftBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Int.Int_NewtonsoftJsonConverter.cs");
        internal static readonly string IntSystemTextJsonBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Int.Int_SystemTextJsonConverter.cs");

        internal static readonly string LongBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Long.Long_Base.cs");
        internal static readonly string LongNewtonsoftBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Long.Long_NewtonsoftJsonConverter.cs");
        internal static readonly string LongSystemTextJsonBase = LoadEmbeddedResource("StronglyTypedIds.Templates.Long.Long_SystemTextJsonConverter.cs");

        internal const string NewtonsoftJsonAttributeSource = "    [Newtonsoft.Json.JsonConverter(typeof(TESTIDNewtonsoftJsonConverter))]";
        internal const string SystemTextJsonAttributeSource = "    [System.Text.Json.Serialization.JsonConverter(typeof(TESTIDSystemTextJsonConverter))]";

        private static string LoadEmbeddedResource(string resourceName)
        {
            var resourceStream = ThisAssembly.GetManifestResourceStream(resourceName);
            if (resourceStream is null)
            {
                var existingResources = ThisAssembly.GetManifestResourceNames();
                throw new ArgumentException($"Could not find embedded resource {resourceName}. Available names: {string.Join(", ", existingResources)}");
            }

            using var reader = new StreamReader(resourceStream, Encoding.UTF8);

            return reader.ReadToEnd();
        }
    }
}
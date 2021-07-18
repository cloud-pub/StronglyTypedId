using System;
using System.Threading.Tasks;
using StronglyTypedIds.Sources;
using VerifyXunit;
using Xunit;

namespace StronglyTypedIds.Tests
{
    [UsesVerify]
    public class StringGeneratorSnapshotTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowsWhenClassNameIsNullOrEmpty(string idName)
        {
            const string idNamespace = "Some.Namespace";
            Assert.Throws<ArgumentException>(() => SourceGenerationHelper.CreateStringId(
                idName: idName,
                idNamespace: idNamespace,
                jsonConverter: null
            ));
        }

        [Theory]
        [MemberData(nameof(Converters))]
        public Task GeneratesStringCorrectly(StronglyTypedIdJsonConverter? converter)
        {
            const string idNamespace = "Some.Namespace";
            const string idName = "MyTestId";
            var result = SourceGenerationHelper.CreateStringId(
                idName: idName,
                idNamespace: idNamespace,
                jsonConverter: converter
            );

            return Verifier.Verify(result)
                .UseDirectory("Snapshots")
                .UseParameters(converter);
        }

        [Theory]
        [MemberData(nameof(Converters))]
        public Task GeneratesStringInGlobalNamespaceCorrectly(StronglyTypedIdJsonConverter? converter)
        {
            const string idName = "MyTestId";
            var result = SourceGenerationHelper.CreateStringId(
                idName: idName,
                idNamespace: string.Empty,
                jsonConverter: converter
            );

            return Verifier.Verify(result)
                .UseDirectory("Snapshots")
                .UseParameters(converter);
        }

        public static TheoryData<StronglyTypedIdJsonConverter?> Converters => new()
        {
            null,
            StronglyTypedIdJsonConverter.NewtonsoftJson,
            StronglyTypedIdJsonConverter.SystemTextJson,
            StronglyTypedIdJsonConverter.NewtonsoftJson | StronglyTypedIdJsonConverter.SystemTextJson,
        };
    }
}
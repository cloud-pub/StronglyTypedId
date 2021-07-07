using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

namespace StronglyTypedIds.Tests
{
    [UsesVerify]
    public class StronglyTypedIdGeneratorTests
    {
        readonly ITestOutputHelper _output;

        public StronglyTypedIdGeneratorTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public Task CanGenerateDefaultIdInGlobalNamespace()
        {
            const string input = @"using StronglyTypedIds;

[StronglyTypedId]
public partial struct MyId {}";
            var (diagnostics, output) = TestHelpers.GetGeneratedOutput<StronglyTypedIdGenerator>(input);

            Assert.Empty(diagnostics);

            return Verifier.Verify(output)
                .UseDirectory("Snapshots");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public Task CanGenerateIdWithNamedParameters(StronglyTypedIdBackingType backingType, StronglyTypedIdJsonConverter? converter)
        {
            var type = $"backingType: {nameof(StronglyTypedIdBackingType)}.{backingType.ToString()}";
            var json = converter.HasValue
                ? $"jsonConverter: {nameof(StronglyTypedIdJsonConverter)}.{converter.ToString()}"
                : "generateJsonConverter: false";
            var attribute = $"[StronglyTypedId({type}, {json})]";

            _output.WriteLine(attribute);

            var input = @"using StronglyTypedIds;

namespace MyTests.TestNameSpace
{
    " + attribute + @"
    public partial struct MyId {}
}";
            var (diagnostics, output) = TestHelpers.GetGeneratedOutput<StronglyTypedIdGenerator>(input);

            return Verifier.Verify(output)
                .UseParameters(backingType, converter)
                .UseDirectory("Snapshots");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public Task CanGenerateIdWithPositionalParameters(StronglyTypedIdBackingType backingType, StronglyTypedIdJsonConverter? converter)
        {
            var type = $"{nameof(StronglyTypedIdBackingType)}.{backingType.ToString()}";
            var args = converter.HasValue
                ? type + $", {nameof(StronglyTypedIdJsonConverter)}.{converter.ToString()}"
                : "false, " + type;
            var attribute = $"[StronglyTypedId({args})]";

            _output.WriteLine(attribute);

            var input = @"using StronglyTypedIds;

namespace MyTests.TestNameSpace
{
    " + attribute + @"
    public partial struct MyId {}
}";
            var (diagnostics, output) = TestHelpers.GetGeneratedOutput<StronglyTypedIdGenerator>(input);

            return Verifier.Verify(output)
                .UseParameters(backingType, converter)
                .UseDirectory("Snapshots");
        }

        public static TheoryData<StronglyTypedIdBackingType, StronglyTypedIdJsonConverter?> GetData => new()
        {
            {StronglyTypedIdBackingType.Guid, null},
            {StronglyTypedIdBackingType.Guid, StronglyTypedIdJsonConverter.NewtonsoftJson},
            {StronglyTypedIdBackingType.Guid, StronglyTypedIdJsonConverter.SystemTextJson},
            {StronglyTypedIdBackingType.Guid, StronglyTypedIdJsonConverter.SystemTextJson | StronglyTypedIdJsonConverter.NewtonsoftJson},
        };
    }
}
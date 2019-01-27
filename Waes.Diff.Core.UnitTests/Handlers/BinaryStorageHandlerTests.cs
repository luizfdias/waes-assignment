using AutoFixture.Idioms;
using NSubstitute;
using System.IO;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Core.UnitTests.Handlers
{
    public class BinaryStorageHandlerTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(BinaryStorageHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Save_WhenStreamIsPassedToSave_ShouldCallBinaryDataStorageSaveWithByteArrayParameters(BinaryStorageHandler sut, Stream stream, string id)
        {
            await sut.Save(id, stream);

            await sut.BinaryDataStorage.Received(1).Save(id, Arg.Any<byte[]>());
        }
    }
}

using AutoFixture.Idioms;
using NSubstitute;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Models;
using Waes.Diff.Core.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Core.UnitTests.Handlers
{
    public class DataStorageHandlerTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(DataStorageHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Save_WhenDataIsPassedToSave_ShouldCallSaveFromDataStorage(DataStorageHandler sut, Data data)
        {
            await sut.Save(data);

            await sut.DataStorage.Received(1).Save(data);
        }
    }
}

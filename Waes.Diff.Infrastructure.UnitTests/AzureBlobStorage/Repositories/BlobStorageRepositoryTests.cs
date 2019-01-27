using AutoFixture.Idioms;
using FluentAssertions;
using NSubstitute;
using System.IO;
using Waes.Diff.Infrastructure.AzureBlobStorage.Repositories;
using Waes.Diff.Infrastructure.UnitTests.AutoData;
using Xunit;

namespace Waes.Diff.Infrastructure.UnitTests.Repositories.AzureBlobStorage
{
    public class BlobStorageRepositoryTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructors_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(BlobStorageRepository).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async void Get_CallingGet_ShouldReturnStreamAsExpected(BlobStorageRepository sut, string id)
        {
            var ms = new MemoryStream();

            sut.CloudBlobContainerWrapper.DownloadToStreamAsync(id).Returns(ms);

            var result = await sut.Get(id);

            result.Should().BeEquivalentTo(ms.ToArray());
        }

        [Theory, AutoNSubstituteData]
        public async void Save_CallingSave_ShouldCallUploadFromByteArrayAsync(BlobStorageRepository sut, string id, byte[] data)
        {
            await sut.Save(id, data);

            await sut.CloudBlobContainerWrapper.Received(1).UploadFromByteArrayAsync(id, data);
        }
    }
}

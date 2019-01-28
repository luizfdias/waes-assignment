using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Api.Mappers;
using Waes.Diff.Api.UnitTests.AutoData;
using Waes.Diff.Core;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Infrastructure.AzureBlobStorage.Factories;
using Waes.Diff.Infrastructure.AzureBlobStorage.Interfaces;
using Waes.Diff.Infrastructure.AzureBlobStorage.Repositories;
using Waes.Diff.Infrastructure.AzureBlobStorage.Wrappers;
using Waes.Diff.Infrastructure.MemoryStorage.Repositories;
using Xunit;

namespace Waes.Diff.Api.UnitTests
{
    public class DependencyInjectionTests
    {
        [Theory]
        [InlineNSubstituteData(typeof(IDiffResponseMapper), typeof(DiffResponseMapper))]
        [InlineNSubstituteData(typeof(IDiffHandler), typeof(DiffHandler))]
        [InlineNSubstituteData(typeof(IBinaryStorageHandler), typeof(BinaryStorageHandler))]
        [InlineNSubstituteData(typeof(IBinaryDataStorage), typeof(MemoryRepository))]
        public void GetService_ShouldResolveAllDependencies(Type typeSource, Type typeExpected, ServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            new Startup(configuration).ConfigureServices(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();

            container.GetService(typeSource).Should().BeOfType(typeExpected);
        }

        [Theory, AutoNSubstituteData]
        public void GetService_WhenGetIDiffChecker_ShouldResolveCompositionAsExpected(ServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            new Startup(configuration).ConfigureServices(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();

            var instance = container.GetService<IDiffChecker>();

            instance.Should().BeOfType<NullabilityChecker>();

            var nullabilityChecker = (NullabilityChecker)instance;
            nullabilityChecker.DiffChecker.Should().BeOfType<SizeChecker>();

            var sizeChecker = (SizeChecker)nullabilityChecker.DiffChecker;
            sizeChecker.DiffChecker.Should().BeOfType<BytesChecker>();
        }

        [Theory, AutoNSubstituteData]
        public void GetService_WhenStorageTypeIsAzureBlob_ShouldResolveRepositoryAsExpected(ServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            configuration["AppSettings:StorageType"] = "AzureBlob";

            new Startup(configuration).ConfigureServices(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();

            container.GetService<IBinaryDataStorage>().Should().BeOfType<BlobStorageRepository>();
            container.GetService<IBlobStorageFactory>().Should().BeOfType<BlobStorageFactory>();
            container.GetService<ICloudBlobContainerWrapper>().Should().BeOfType<CloudBlobContainerWrapper>();
        }
    }
}

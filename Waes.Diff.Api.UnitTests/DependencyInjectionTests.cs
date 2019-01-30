using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Waes.Diff.Api.Contracts;
using Waes.Diff.Api.Interfaces;
using Waes.Diff.Api.Services;
using Waes.Diff.Api.UnitTests.AutoData;
using Waes.Diff.Core;
using Waes.Diff.Core.Handlers;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Infrastructure.MemoryStorage.Repositories;
using Xunit;

namespace Waes.Diff.Api.UnitTests
{
    public class DependencyInjectionTests
    {
        [Theory]
        [InlineNSubstituteData(typeof(IMediator), typeof(Mediator))]
        [InlineNSubstituteData(typeof(IHandleRequest<string, BaseResponse<DiffInfo>>), typeof(DiffService))]
        [InlineNSubstituteData(typeof(IHandleRequest<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>), typeof(DataStoreService))]        
        [InlineNSubstituteData(typeof(IDiffHandler), typeof(DiffHandler))]
        [InlineNSubstituteData(typeof(IDataStorageHandler), typeof(DataStorageHandler))]
        [InlineNSubstituteData(typeof(IDataStorage), typeof(MemoryRepository))]
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
    }
}

using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using Waes.Assignment.Api.Contracts;
using Waes.Assignment.Api.Interfaces;
using Waes.Assignment.Api.Services;
using Waes.Assignment.Api.UnitTests.AutoData;
using Waes.Assignment.Domain;
using Waes.Assignment.Domain.Handlers;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Infrastructure;
using Waes.Assignment.Infrastructure.MongoDBStorage;
using Waes.Assignment.Infrastructure.MongoDBStorage.Interfaces;
using Waes.Assignment.Infrastructure.MongoDBStorage.Repositories;
using Xunit;

namespace Waes.Assignment.Api.UnitTests
{
    public class DependencyInjectionTests
    {
        [Theory]
        [InlineNSubstituteData(typeof(IMediator), typeof(Mediator))]
        [InlineNSubstituteData(typeof(IHandleRequest<string, BaseResponse<DiffInfo>>), typeof(DiffService))]
        [InlineNSubstituteData(typeof(IHandleRequest<BaseRequest<SaveDataModel>, BaseResponse<SaveDataModel>>), typeof(DataStoreService))]        
        [InlineNSubstituteData(typeof(IDiffHandler), typeof(DiffHandler))]
        [InlineNSubstituteData(typeof(IDataStorageHandler), typeof(DataStorageHandler))]
        [InlineNSubstituteData(typeof(IDataStorage), typeof(DataRepository))]
        [InlineNSubstituteData(typeof(IMongoDBContext), typeof(MongoDBContext))]
        public void GetService_ShouldResolveAllDependencies(Type typeSource, Type typeExpected, ServiceCollection serviceCollection,
            IConfiguration configuration, IHostingEnvironment hostingEnvironment, IOptions<StorageSettings> storageSettings)
        {
            hostingEnvironment.EnvironmentName = "Development";

            new Startup(configuration, hostingEnvironment).ConfigureServices(serviceCollection);

            serviceCollection.Replace(new ServiceDescriptor(typeof(IOptions<StorageSettings>),  storageSettings));

            var container = serviceCollection.BuildServiceProvider();

            container.GetService(typeSource).Should().BeOfType(typeExpected);
        }

        [Theory, AutoNSubstituteData]
        public void GetService_WhenGetIDiffChecker_ShouldResolveCompositionAsExpected(ServiceCollection serviceCollection,
            IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            new Startup(configuration, hostingEnvironment).ConfigureServices(serviceCollection);

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

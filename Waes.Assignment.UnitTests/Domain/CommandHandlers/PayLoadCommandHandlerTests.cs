using AutoFixture.Idioms;
using Waes.Assignment.Domain.CommandHandlers;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.UnitTests.AutoData;
using Xunit;

namespace Waes.Assignment.UnitTests.Domain.CommandHandlers
{
    public class PayLoadCommandHandlerTests
    {
        [Theory, AutoNSubstituteData]
        public void Constructor_GuardTests(GuardClauseAssertion guard)
        {
            guard.Verify(typeof(PayLoadCommandHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void T(IMediatorHandler mediatorHandler, IPayLoadRepository payLoadRepository)
        {
            var sut = new PayLoadCommandHandler(mediatorHandler, payLoadRepository);

            //sut.Handle()
        }
    }
}

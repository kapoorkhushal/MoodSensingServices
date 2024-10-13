using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace MoodSensingServices.Test.Base
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new CompositeCustomization(
                new AutoMoqCustomization { ConfigureMembers = true },
                new SupportMutableValueTypesCustomization()));

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(fix => fixture.Behaviors.Remove(fix));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Customizations.Add(new ElementsBuilder<CancellationToken>(CancellationToken.None));

            return fixture;
        })
        { }
    }
}

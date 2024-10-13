using AutoFixture.Xunit2;

namespace MoodSensingServices.Test.Base
{
    public class InlineAutoMoqDataAttribute: InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects): base(new AutoMoqDataAttribute(), objects) { }
    }
}

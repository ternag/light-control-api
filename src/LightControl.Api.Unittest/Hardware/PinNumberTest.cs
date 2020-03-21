using System;
using Xunit;

namespace LightControl.Api.Unittest
{
    public class PinNumberTest
    {
        [Fact]
        public void CanCreate()
        {
            var sut = new PinNumber(3);
        }

        [Fact]
        public void CannotCreateNegativePinNumber()
        {
            //new PinNumber(-3);
        }
    }
}

using Xunit;

namespace LISPValidation.Tests
{
    public class ValidationLogicTests
    {
        [Fact]
        public void EqualParenShouldPass()
        {
            var input = "( + ( * 2 4 ) )";

            var result = ValidationLogic.Parse(input);
            
            Assert.True(result);
        }
        
        [Fact]
        public void MultipleClausesShouldPass()
        {
            var input = "( + ( * ( / 8 4) ( + 2 1 ) ) )";

            var result = ValidationLogic.Parse(input);
            
            Assert.True(result);
        }
        
        [Fact]
        public void UnclosedParenShouldFail()
        {
            var input = "( + ( * 2 ( 4 ) )";

            var result = ValidationLogic.Parse(input);
            
            Assert.False(result);
        }

        [Fact]
        public void ExtraClosingParenShouldFail()
        {
            var input = "( + ( * 2 4 ) ) )";

            var result = ValidationLogic.Parse(input);
            
            Assert.False(result);
        }
    }
}
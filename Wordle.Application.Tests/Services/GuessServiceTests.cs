using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Application.Consts;
using Wordle.Application.Services;
using Xunit;

namespace Wordle.Application.Tests.Services
{
    public class GuessServiceTests
    {
        [Fact]
        public void Validate_ShouldNotThrow_WhenUserGuessIsCorrect()
        {
            Action act = () => GuessService.Validate("horse");

            act.Should().NotThrow();
        }

        [Fact]
        public void Validate_ShouldThrowArgumentException_WhenUserGuessIsTooShort()
        {
            Action act = () => GuessService.Validate("test");

            act.Should().Throw<ArgumentException>().WithMessage(UserGuessException.GUESS_TOO_SHORT + " (Parameter 'userGuess')");
        }

        [Fact]
        public void Validate_ShouldThrowArgumentException_WhenUserGuessIsTooLong()
        {
            Action act = () => GuessService.Validate("test test");

            act.Should().Throw<ArgumentException>(UserGuessException.GUESS_TOO_LONG + " (Parameter 'userGuess')");
        }
    }
}

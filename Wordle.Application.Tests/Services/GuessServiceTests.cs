using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Application.Consts;
using Wordle.Application.Enums;
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

        [Theory]
        [InlineData("apple", "apple")]
        [InlineData("horse", "horse")]
        public void Check_ShouldReturnAllCorrect_WhenTheGuessIsCorrect(string userGuess, string correctAnswer)
        {
            var expected = new Color[]
            {
                Color.Correct,
                Color.Correct,
                Color.Correct,
                Color.Correct,
                Color.Correct
            };

            var result = GuessService.Check(userGuess, correctAnswer);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Check_ShouldReturnAllNeutral_WhenTheGuessIsCompletlyWrong()
        {
            var expected = new Color[]
            {
                Color.Neutral,
                Color.Neutral,
                Color.Neutral,
                Color.Neutral,
                Color.Neutral
            };

            var result = GuessService.Check("night", "sleep");

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Check_ShouldReturnYellowCharacter_WhenLetterIsInAWrongPosition()
        {
            var expected = new Color[]
            {
                Color.Neutral,
                Color.Neutral,
                Color.Misplaced,
                Color.Neutral,
                Color.Neutral
            };

            var result = GuessService.Check("fight", "gecko");

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Check_ShouldReturnOneYellowAndOneGrayDuplicateCharacter_WhenCharacterAppearsOnlyOnceInTheAnswerAtTheWorngPlace()
        {
            var expected = new Color[]
            {
                Color.Neutral,
                Color.Neutral,
                Color.Misplaced,
                Color.Neutral,
                Color.Misplaced
            };

            var result = GuessService.Check("speed", "abide");

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Check_ShouldReutrnOneGreenAndOneGrayDuplicateCharacter_WhenCharacterAppearsOnlyOnceInTheAnswerAtTheRightPlace()
        {
            var expected = new Color[]
            {
                Color.Correct,
                Color.Neutral,
                Color.Correct,
                Color.Neutral,
                Color.Neutral
            };

            var result = GuessService.Check("speed", "steal");

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Check_ShouldReturnOneGreenAndOneYellowDuplicateCharacter_WhenCharacterAppearsTwiceInTheAnswerOnceAtTheRightPlaceAndOnceAtTheWrongPlace()
        {
            var expected = new Color[]
            {
                Color.Neutral,
                Color.Misplaced,
                Color.Correct,
                Color.Misplaced,
                Color.Neutral
            };

            var result = GuessService.Check("speed", "crepe");

            result.Should().BeEquivalentTo(expected);
        }
    }
}

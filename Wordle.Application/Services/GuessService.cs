using Wordle.Application.Consts;

namespace Wordle.Application.Services
{
    public static class GuessService
    {
        public static void Validate(string userGuess)
        {
            if (userGuess.Length < 5)
            {
                throw new ArgumentException(UserGuessException.GUESS_TOO_SHORT, nameof(userGuess));
            }

            if (userGuess.Length > 5)
            {
                throw new ArgumentException(UserGuessException.GUESS_TOO_LONG, nameof(userGuess));
            }
        }
    }
}

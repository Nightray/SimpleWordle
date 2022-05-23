using Wordle.Application.Consts;
using Wordle.Application.Enums;

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

        public static Color[] Check(string userGuess, string correctWord)
        {
            var answer = new Color[] { Color.Neutral, Color.Neutral, Color.Neutral, Color.Neutral, Color.Neutral };
            var duplicateUserGuess = correctWord.ToArray();

            for (int i = 0; i < userGuess.Length; i++)
            {
                var letter = userGuess[i];

                if(letter == correctWord[i])
                {
                    answer[i] = Color.Correct;
                    duplicateUserGuess[i] = '_';
                }
                else if (duplicateUserGuess.Contains(letter))
                {
                    answer[i] = Color.Misplaced;
                    duplicateUserGuess[i] = '_';
                }
            }

            return answer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace YahtzeeGame
{
    public class CalculateScore
    {
        // Upper Section Scores (Ones, Twos, Threes, etc.)
        public static int CalculateCategoryScore(List<int> diceValues, int category)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            return diceValues.Count(d => d == category) * category;
        }

        // Yahtzee Score (All five dice the same)
        public static int CalculateYahtzeeScore(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            return diceValues.Distinct().Count() == 1 ? 50 : 0;
        }

        // Three-of-a-Kind (At least three dice showing the same value)
        public static int CalculateThreeOfAKind(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            return GetGroupedDice(diceValues).Any(g => g.Count() >= 3) ? diceValues.Sum() : 0;
        }

        // Four-of-a-Kind (At least four dice showing the same value)
        public static int CalculateFourOfAKind(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            return GetGroupedDice(diceValues).Any(g => g.Count() >= 4) ? diceValues.Sum() : 0;
        }

        // Full House (Three of one number, two of another)
        public static int CalculateFullHouse(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            var groups = GetGroupedDice(diceValues).Select(g => g.Count()).OrderByDescending(c => c).ToList();
            return groups.SequenceEqual(new List<int> { 3, 2 }) ? 25 : 0;
        }

        // Small Straight (Four consecutive numbers)
        public static int CalculateSmallStraight(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            var uniqueSorted = diceValues.Distinct().OrderBy(d => d).ToList();
            List<List<int>> validStraights = new List<List<int>> {
                new List<int> {1, 2, 3, 4},
                new List<int> {2, 3, 4, 5},
                new List<int> {3, 4, 5, 6}
            };

            return validStraights.Any(straight => straight.All(num => uniqueSorted.Contains(num))) ? 30 : 0;
        }

        // Large Straight (Five consecutive numbers)
        public static int CalculateLargeStraight(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            var uniqueSorted = diceValues.Distinct().OrderBy(d => d).ToList();
            return (uniqueSorted.SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }) || uniqueSorted.SequenceEqual(new List<int> { 2, 3, 4, 5, 6 })) ? 40 : 0;
        }

        // Chance (Sum of all dice)
        public static int CalculateChance(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count == 0)
                throw new ArgumentException("Dice values cannot be empty.");

            return diceValues.Sum();
        }

        // Helper method to group dice values
        private static IEnumerable<IGrouping<int, int>> GetGroupedDice(List<int> diceValues)
        {
            return diceValues.GroupBy(d => d);
        }
    }
}
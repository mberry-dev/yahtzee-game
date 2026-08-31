using System;
using System.Collections.Generic;
using System.Linq;

namespace YahtzeeGame
{
    public class Category
    {
        public int Score { get; private set; }
        public bool IsUsed { get; private set; }
        public string CategoryName { get; set; }

        // Constructor to initialize the category
        public Category(string name)
        {
            CategoryName = name;
            Score = 0;
            IsUsed = false;
        }

        // Set the score for this category
        public void SetScore(int score)
        {
            if (!IsUsed)
            {
                Score = score;
                IsUsed = true;
            }
        }

        // Reset the category for a new game
        public void ResetCategory()
        {
            Score = 0;
            IsUsed = false;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
        }

        // Validate dice values (must be exactly 5 dice)
        private static void ValidateDiceValues(List<int> diceValues)
        {
            if (diceValues == null || diceValues.Count != 5)
                throw new ArgumentException("Dice values must contain exactly 5 dice.");
        }

        // Helper method to group dice values
        private static IEnumerable<IGrouping<int, int>> GetGroupedDice(List<int> diceValues)
        {
            return diceValues.GroupBy(d => d);
        }

        // Calculate score for specific category (Upper Section)
        public int CalculateUpperScore(List<int> diceValues, int number)
        {
            ValidateDiceValues(diceValues);
            return diceValues.Count(die => die == number) * number;
        }

        // Calculate Three-of-a-Kind score (At least three dice showing the same value)
        public int CalculateThreeOfAKind(List<int> diceValues)
        {
            ValidateDiceValues(diceValues);
            return CalculateNOfAKind(diceValues, 3);
        }

        // Calculate Four-of-a-Kind score (At least four dice showing the same value)
        public int CalculateFourOfAKind(List<int> diceValues)
        {
            ValidateDiceValues(diceValues);
            return CalculateNOfAKind(diceValues, 4);
        }

        // Helper method for N-of-a-Kind (3 or 4)
        private static int CalculateNOfAKind(List<int> diceValues, int n)
        {
            return GetGroupedDice(diceValues).Any(g => g.Count() >= n) ? diceValues.Sum() : 0;
        }

        // Calculate Full House score (Three of one number, two of another)
        public int CalculateFullHouse(List<int> diceValues)
        {
            ValidateDiceValues(diceValues);
            var groups = GetGroupedDice(diceValues).Select(g => g.Count()).OrderByDescending(c => c).ToList();
            return groups.SequenceEqual(new List<int> { 3, 2 }) ? 25 : 0;
        }

        // Calculate Yahtzee score (All five dice the same)
        public int CalculateYahtzee(List<int> diceValues)
        {
            ValidateDiceValues(diceValues);
            return diceValues.Distinct().Count() == 1 ? 50 : 0;
        }

        // Calculate Small Straight score (Four consecutive numbers)
        public int CalculateSmallStraight(List<int> diceValues)
        {
            ValidateDiceValues(diceValues);
            var uniqueSorted = diceValues.Distinct().OrderBy(d => d).ToList();
            List<List<int>> validSmallStraights = new List<List<int>> {
                new List<int> {1, 2, 3, 4},
                new List<int> {2, 3, 4, 5},
                new List<int> {3, 4, 5, 6}
            };

            return validSmallStraights.Any(straight => straight.All(num => uniqueSorted.Contains(num))) ? 30 : 0;
        }

        // Calculate Large Straight score (Five consecutive numbers)
        public int CalculateLargeStraight(List<int> diceValues)
        {
            ValidateDiceValues(diceValues);
            var uniqueSorted = diceValues.Distinct().OrderBy(d => d).ToList();
            List<List<int>> validLargeStraights = new List<List<int>> {
                new List<int> {1, 2, 3, 4, 5},
                new List<int> {2, 3, 4, 5, 6}
            };

            return validLargeStraights.Any(straight => straight.All(num => uniqueSorted.Contains(num))) ? 40 : 0;
        }

        // Calculate Chance score (Sum of all dice)
        public int CalculateChance(List<int> diceValues)
        {
            ValidateDiceValues(diceValues);
            return diceValues.Sum();
        }
    }
}
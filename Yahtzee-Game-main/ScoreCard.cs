using System;
using System.Linq;

namespace YahtzeeGame
{
    public class ScoreCard
    {

        public Category[] UpperCategories { get; private set; } = new Category[6]; // 1s to 6s
        public Category[] LowerCategories { get; private set; } = new Category[7]; // 3-of-a-kind, 4-of-a-kind, Full House, etc.
        public int TotalScore { get; private set; }

        public ScoreCard()
        {
            string[] upperNames = { "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes" };
            string[] lowerNames = { "Three-of-a-Kind", "Four-of-a-Kind", "Full House", "Small Straight", "Large Straight", "Chance", "Yahtzee" };

            for (int i = 0; i < 6; i++)
            {
                UpperCategories[i] = new Category(upperNames[i]);
            }

            for (int i = 0; i < 7; i++)
            {
                LowerCategories[i] = new Category(lowerNames[i]);
            }
        }

        // Set the score for a selected category
        public void SetScore(int categoryIndex, int score)
        {
            if (categoryIndex < 6)
            {
                UpperCategories[categoryIndex].SetScore(score);
            }
            else
            {
                LowerCategories[categoryIndex - 6].SetScore(score);
            }
            UpdateTotalScore();
        }

        // Update the total score
        private void UpdateTotalScore()
        {
            TotalScore = UpperCategories.Sum(c => c.Score) + LowerCategories.Sum(c => c.Score);
        }

        // Reset the scorecard for a new game
        public void ResetScorecard()
        {
            foreach (var category in UpperCategories)
            {
                category.ResetCategory(); // Reset each category
            }

            foreach (var category in LowerCategories)
            {
                category.ResetCategory(); // Reset each category
            }

            TotalScore = 0;
        }

        // Get category by index (0-5 for upper, 6-12 for lower)
        public Category GetCategoryByIndex(int index)
        {
            if (index < 6) return UpperCategories[index];
            return LowerCategories[index - 6];
        }

        // Check if a category is used
        public bool IsCategoryUsed(int index)
        {
            return GetCategoryByIndex(index).IsUsed;
        }

        // Mark category as used (prevents reusing it)
        public void MarkCategoryAsUsed(int index)
        {
            Category category = GetCategoryByIndex(index);
            if (category != null)
            {
                category.MarkAsUsed();
            }
        }

        private int ones, twos, threes, fours, fives, sixes, sum;
        private int threeOfAKind, fourOfAKind, fullHouse, smallStraight, largeStraight;
        private int chance, yahtzee, totalScore;

        // Flags for tracking if the categories have been used
        private bool onesUsed, twosUsed, threesUsed, foursUsed, fivesUsed, sixesUsed, sumUsed;
        private bool threeOfAKindUsed, fourOfAKindUsed, fullHouseUsed, smallStraightUsed, largeStraightUsed;
        private bool chanceUsed, yahtzeeUsed, totalScoreUsed;

        // Setters
        public void SetOnes(int ones) 
        {
            this.ones = ones;
        }
        public void SetTwos(int twos)
        {
            this.twos = twos;
        }
        public void SetThrees(int threes)
        {
            this.threes = threes;
        }
        public void SetFours(int fours)
        {
            this.fours = fours;
        }
        public void SetFives(int fives)
        {
            this.fives = fives;
        }
        public void SetSixes(int sixes)
        {
            this.sixes = sixes;
        }
        public void SetSum(int sum)
        {
            this.sum = sum;
        }
        public void SetThreeOfAKind(int threeOfAKind)
        {
            this.threeOfAKind = threeOfAKind;
        }
        public void SetFourOfAKind(int fourOfAKind)
        {
            this.fourOfAKind = fourOfAKind;
        }
        public void FullHouse(int fullHouse)
        {
            this.fullHouse = fullHouse;
        }
        public void SmallStraight(int smallStraight)
        {
            this.smallStraight = smallStraight;
        }
        public void LargeStraight(int largeStraight)
        {
            this.largeStraight = largeStraight;
        }
        public void Chance(int chance)
        {
            this.chance = chance;
        }
        public void SetYahtzee(int yahtzee)
        {
            this.yahtzee = yahtzee;
        }

        // Set total score
        public void CalculateTotalScore()
        {
            totalScore = ones + twos + threes + fours + fives + sixes + threeOfAKind +
                         fourOfAKind + fullHouse + smallStraight + largeStraight + chance + yahtzee;
        }

        // Getters
        public int GetOnes()
        {
            return ones;
        }
        public int GetTwos()
        {
            return twos;
        }
        public int GetThrees()
        {
            return threes;
        }
        public int GetFours()
        {
            return fours;
        }
        public int GetFives()
        {
            return fives;
        }
        public int GetSixes()
        {
            return sixes;
        }
        public int GetSum()
        {
            return sum;
        }
        public int GetThreeOfAKind()
        {
            return threeOfAKind;
        }
        public int GetFourOfAKind()
        {
               return fourOfAKind;
        }
        public int GetSmallStraight()
        {
            return smallStraight;
        }
        public int GetLargeStraight()
        {
            return largeStraight;
        }
        public int GetChance()
        {
            return chance;
        }
        public int GetYahtzee()
        {
            return yahtzee;
        }
        public int GetTotalScore()
        {
            return totalScore;
        }

        // Methods to check if a category has been used (selected)
        public bool IsOnesUsed()
        {
            return onesUsed;
        }
        public bool IsTwosUsed()
        {
            return twosUsed;
        }
        public bool IsThreesUsed()
        {
            return threesUsed;
        }
        public bool IsFourUsed()
        {
            return foursUsed;
        }
        public bool IsFivesUsed()
        {
            return fivesUsed;
        }
        public bool IsSixesUsed()
        {
            return sixesUsed;
        }
        public bool IsThreeofaKindUsed()
        {
            return threeOfAKindUsed;
        }
        public bool IsFourofaKindUsed()
        {
            return fourOfAKindUsed;
        }
        public bool IsFullhouseUsed()
        {
            return fullHouseUsed;
        }
        public bool IsSmallstraightUsed()
        {
            return smallStraightUsed;
        }
        public bool IsLargestraightUsed()
        {
            return largeStraightUsed;
        }
        public bool IsChanceUsed()
        {
            return chanceUsed;
        }
        public bool IsYahtzeeUsed()
        {
            return yahtzeeUsed;
        }
        // Methods to mark a category as used (after selection)
        public void MarkOnesAsUsed()
        {
            onesUsed = true;
        }
        public void MarkTwosAsUsed()
        {
            twosUsed = true;
        }
        public void MarkThreesAsUsed()
        {
            threesUsed = true;
        }
        public void MarkFoursAsUsed()
        {
            foursUsed = true;
        }
        public void MarkFivesAsUsed()
        {
            fivesUsed = true;
        }
        public void MarkSixesAsUsed()
        {
            sixesUsed = true;
        }
        public void MarkThreeofaKindAsUsed()
        {
            threeOfAKindUsed = true;
        }
        public void MarkFourofaKindAsUsed()
        {
            fourOfAKindUsed = true;
        }
        public void MarkFullHouseAsUsed()
        {
            fullHouseUsed = true;
        }
        public void MarkSmallStraightAsUsed()
        {
            smallStraightUsed = true;
        }
        public void MarkLargeStraightAsUsed()
        {
            largeStraightUsed = true;
        }
        public void MarkChanceAsUsed()
        {
            chanceUsed = true;
        }
        public void MarkYahtzeeAsUsed()
        {
            yahtzeeUsed = true;
        }
    }
}

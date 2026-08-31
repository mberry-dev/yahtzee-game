using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YahtzeeGame
{

    public partial class Form1 : Form
    {
        private Random dice = new Random(); // to genrate random numbers
        private int rollCount = 0; // Track number of rolls
        private const int maxRolls = 3; // Maximum allowed rolls per turn
        private List<int> diceValues = new List<int> { 1, 1, 1, 1, 1 }; // Initialize dice values (1-6 range for each die)
        private List<bool> diceHeld = new List<bool> { false, false, false, false, false }; // Tracks whether each die is kept
        private ScoreCard scoreCard;
        private bool canHoldDice = false; // Add a new boolean flag to track if the dice have been rolled

        // Store the file names of dice images
        private List<string> diceImages = new List<string>
        {
            @"C:\Users\Brandon\OneDrive\Documents\Images\Dice 1.JPG",
            @"C:\Users\Brandon\OneDrive\Documents\Images\Dice 2.JPG",
            @"C:\Users\Brandon\OneDrive\Documents\Images\Dice 3.JPG",
            @"C:\Users\Brandon\OneDrive\Documents\Images\Dice 4.JPG",
            @"C:\Users\Brandon\OneDrive\Documents\Images\Dice 5.JPG",
            @"C:\Users\Brandon\OneDrive\Documents\Images\Dice 6.JPG"
        };



        public Form1()
        {
            InitializeComponent();
            scoreCard = new ScoreCard();
        }

        private void RollDice()
        {
            if (rollCount < maxRolls)
            {
                rollCount++;
                
                for(int i = 0; i < 5; i++) 
                {
                    if (!diceHeld[i])
                    {
                        diceValues[i] = dice.Next(1, 7);
                    }
                }
                UpdateDiceVisuals();
                canHoldDice = true;

                // Display possible scores immediately after rolling
                DisplayPossibleScores();

                // Enable scoring only after rolling at least once
                EnableCategoryButtons(true);

                if (rollCount == maxRolls)
                {
                    button1.Enabled = false; // Disable rolling after 3 rolls
                    MessageBox.Show("Select a category to score before the next turn!", "Roll Limit Reached");
                }
            }
        }

        private void DisplayPossibleScores()
        {
            labelPossibleScores.Text = GetPossibleScores();
        }

        private string GetPossibleScores()
        {
            List<string> possibleScores = new List<string>();

            for (int i = 0; i < 13; i++)
            {
                if (!scoreCard.IsCategoryUsed(i))
                {
                    int score = CalculateCategoryScore(i);
                    possibleScores.Add($"Category {i + 1}: {score}");
                }
            }

            return string.Join("\n", possibleScores);
        }

        // Enables or disables the category buttons
        private void EnableCategoryButtons(bool enable)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Tag != null)
                {
                    button.Enabled = enable && !scoreCard.IsCategoryUsed(int.Parse(button.Tag.ToString()));
                }
            }
        }

        // Method to update the dice visuals (images for dice)
        private void UpdateDiceVisuals()
        {
            for (int i = 0; i < 5; i++)
            {
                GetPictureBoxForDie(i).ImageLocation = GetDiceImage(diceValues[i]);
                UpdateDieVisual(i, GetPictureBoxForDie(i));
            }
        }

        // Method to get the image for a given dice value
        private string GetDiceImage(int diceValue)
        {
            // Ensure diceValue is between 1 and 6
            if (diceValue < 1 || diceValue > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(diceValue), "Dice value must be between 1 and 6.");
            }

            // Get the full path to the dice image (combines the path)
            return Path.Combine(Application.StartupPath, "Images", diceImages[diceValue - 1]);
        }

        // Method to update the visual of each dice (border or other indicators)
        private void UpdateDieVisual(int dieIndex, PictureBox pictureBox)
        {
            pictureBox.BorderStyle = diceHeld[dieIndex] ? BorderStyle.Fixed3D : BorderStyle.None;
        }

        // Method to toggle whether the die is kept or not
        private void ToggleDie(int dieIndex)
        {
            if (canHoldDice)
            {
                // Toggle the "held" state of the clicked die
                diceHeld[dieIndex] = !diceHeld[dieIndex];

                // Update the visual representation of the dice
                UpdateDieVisual(dieIndex, GetPictureBoxForDie(dieIndex));
            }
        }

        // Helper method to get the correct PictureBox for each die index
        private PictureBox GetPictureBoxForDie(int dieIndex)
        {
            switch (dieIndex)
            {
                case 0: return pictureBox1;
                case 1: return pictureBox2;
                case 2: return pictureBox3;
                case 3: return pictureBox4;
                case 4: return pictureBox5;
                default: return null; // If dieIndex is out of range
            }
        }

        private void ResetTurn( object sender, EventArgs e)
        {
            rollCount = 0; // Reset roll counter
            button1.Enabled = true; // Re-enable roll button
            canHoldDice = false; // Disable holding dice until new roll
            diceHeld = new List<bool> { false, false, false, false, false };

            // Reset dice images and border styles
            for (int i = 0; i < 5; i++)
            {
                GetPictureBoxForDie(i).BorderStyle = BorderStyle.None;
                diceValues[i] = 1; // Reset to default value (can adjust based on desired default)
            }
        }

        private void buttonRoll_Click(object sender, EventArgs e)
        {
            RollDice();
        }

        private void pictureBox1_Click(object sender, EventArgs e) => ToggleDie(0);
        private void pictureBox2_Click(object sender, EventArgs e) => ToggleDie(1);
        private void pictureBox3_Click(object sender, EventArgs e) => ToggleDie(2);
        private void pictureBox4_Click(object sender, EventArgs e) => ToggleDie(3);
        private void pictureBox5_Click(object sender, EventArgs e) => ToggleDie(4);

        private void categoryButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int categoryIndex = int.Parse(button.Tag.ToString());

            if (!scoreCard.IsCategoryUsed(categoryIndex))
            {
                scoreCard.SetScore(categoryIndex, CalculateCategoryScore(categoryIndex));
                button.Enabled = false; // Disable the category button after use
            }

            
        }

        private int CalculateCategoryScore(int categoryIndex)
        {
            // Implement the logic to calculate the score based on the dice values
            // For example, if the category is "Ones", sum up all the dice that show "1".
            int score = 0;
            switch (categoryIndex)
            {
                case 0: // Ones
                    score = diceValues.Count(d => d == 1) * 1;
                    break;

                 // Add other cases for different categories (Twos, Threes, etc.)
            }
            return score;
        }
    }
}
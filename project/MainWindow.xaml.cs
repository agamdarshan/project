using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int okCount = 0;
        private int failCount = 0;
        private int MinGrade;
        private int MaxGrade;
        private bool IsMultiDivAvailable;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void RandomOperationButton_Click(object sender, RoutedEventArgs e)
        {
            string[] operations = null;
            if (IsMultiDivAvailable == true)
            {
                // Array of available operations for classes C and D
                operations = new[] { "SUM", "SUB", "MULTI", "DIV" };
            }
            else
            {
                // Array of available operations for classes A and B
                operations = new[] { "SUM", "SUB" };
            }


            // Get a random index
            Random random = new Random();
            int randomIndex = random.Next(operations.Length);

            // Get the selected operation based on the random index
            string selectedOperation = operations[randomIndex];

            // Find the corresponding radio button based on the selected operation
            RadioButton selectedRadioButton = null;

            if (selectedOperation == "SUM")
            {
                selectedRadioButton = SumRadioButton;
            }
            else if (selectedOperation == "SUB")
            {
                selectedRadioButton = SubRadioButton;
            }
            else if (selectedOperation == "MULTI")
            {
                selectedRadioButton = MultiRadioButton;
            }
            else if (selectedOperation == "DIV")
            {
                selectedRadioButton = DivRadioButton;
            }

            // Set the IsChecked property of the selected radio button to true
            if (selectedRadioButton != null)
            {
                selectedRadioButton.IsChecked = true;
            }
        }




        private void RandomNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            // Generate two random numbers
            Random random = new Random();
            int num1 = random.Next(MinGrade, MaxGrade);
            int num2 = random.Next(MinGrade, MaxGrade);

            // Set the text of Num1TextBox and Num2TextBox
            Num1TextBox.Text = num1.ToString();
            Num2TextBox.Text = num2.ToString();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are numbers in the text boxes
            if (!int.TryParse(Num1TextBox.Text, out int num1) || !int.TryParse(Num2TextBox.Text, out int num2))
            {
                MessageBox.Show("Please enter valid numbers in NUM1 and NUM2.");
                // Add small red borders to the TextBoxes with invalid input
                Num1TextBox.BorderBrush = Brushes.Red;
                Num2TextBox.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                // Reset the border brushes to default if the input is valid
                Num1TextBox.ClearValue(Border.BorderBrushProperty);
                Num2TextBox.ClearValue(Border.BorderBrushProperty);
            }

            // Check if an operation is selected
            string selectedOperation = null;

            if (SumRadioButton.IsChecked == true)
            {
                selectedOperation = "SUM";
            }
            else if (SubRadioButton.IsChecked == true)
            {
                selectedOperation = "SUB";
            }
            else if (MultiRadioButton.IsChecked == true)
            {
                selectedOperation = "MULTI";
            }
            else if (DivRadioButton.IsChecked == true)
            {
                selectedOperation = "DIV";
            }
            else
            {
                MessageBox.Show("Please choose an operation.");
                return;
            }

            // Perform the calculation based on the selected operation
            int result = 0;

            if (selectedOperation == "SUM")
            {
                result = num1 + num2;
            }
            else if (selectedOperation == "SUB")
            {
                result = num1 - num2;
            }
            else if (selectedOperation == "MULTI")
            {
                result = num1 * num2;
            }
            else if (selectedOperation == "DIV")
            {
                // Handle division by zero
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                else
                {
                    // Handle division by zero scenario
                    MessageBox.Show("Cannot divide by zero.");
                    return;
                }
            }
            else
            {
                // Handle the case if a new operation is added in the future
                MessageBox.Show("Invalid operation.");
                return;
            }

            // Update the ResultTextBlock with the result
            ResultTextBlock.Text = result.ToString();
        }

        private void ResetAllRelvenceElements()
        {
            // Clear the content of text boxes
            Num1TextBox.Clear();
            Num2TextBox.Clear();
            AnswerTextBox.Clear();
            ResultTextBlock.Text = "0";

            // Uncheck all radio buttons
            SumRadioButton.IsChecked = false;
            SubRadioButton.IsChecked = false;
            MultiRadioButton.IsChecked = false;
            DivRadioButton.IsChecked = false;

            // Reset the border brushes to default if the input is valid
            Num1TextBox.ClearValue(Border.BorderBrushProperty);
            Num2TextBox.ClearValue(Border.BorderBrushProperty);
            AnswerTextBox.ClearValue(Border.BorderBrushProperty);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAllRelvenceElements();

        }



        private void SendAnswerButton_Click(object sender, RoutedEventArgs e)
        {

           

            // Get the entered answer from the TextBox
            if (!int.TryParse(AnswerTextBox.Text, out int userAnswer))
            {
                MessageBox.Show("Please enter a valid number in the ANSWER field.");
                AnswerTextBox.BorderBrush = Brushes.Red;  // Add a small red border
                return;
            }
            else
            {
                // Reset the border brush to default if the input is valid
                AnswerTextBox.ClearValue(Border.BorderBrushProperty);
            }



            // Get the entered answer from the TextBox
            string userAnswerText = AnswerTextBox.Text;

            // Get the expected result from the ResultTextBlock
            string expectedResultText = ResultTextBlock.Text;

            // Check if the answer is correct
            if (userAnswerText.Equals(expectedResultText))
            {
                // Increment the OK count
                okCount++;
                OkTextBlock.Text = okCount.ToString();
            }
            else
            {
                // Increment the FAIL count
                failCount++;
                FailTextBlock.Text = failCount.ToString();
            }

            // Calculate and display the grade
            double totalAttempts = okCount + failCount;
            double grade = (okCount / totalAttempts) * 100;

            // Update the GradeTextBlock
            GradeTextBlock.Text = grade.ToString("F2"); // Display grade with two decimal places

            MessageBox.Show("Successfuly send answer !");
            AnswerTextBox.BorderBrush = Brushes.Red;  // Add a small red border
            ResetAllRelvenceElements();
        }

      


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window
            this.Close();
        }

        private void ChangeVisibilities()
        {
            // Hide elements above the buttons
            GradeLabel.Visibility = Visibility.Collapsed;
            GradeButtonsPanel.Visibility = Visibility.Collapsed;

            // Show elements below the buttons
            TitleLabel.Visibility = Visibility.Visible;
           
            Num1Label.Visibility = Visibility.Visible;
            Num1TextBox.Visibility = Visibility.Visible;
            Num2Label.Visibility = Visibility.Visible;
            Num2TextBox.Visibility = Visibility.Visible;
            AnswerLabel.Visibility = Visibility.Visible;
            AnswerTextBox.Visibility = Visibility.Visible;
            RadioButtonsPanel.Visibility = Visibility.Visible;

             // if the student in class A or B he cannot answer multi or div
            if(IsMultiDivAvailable==false)
            {
                MultiRadioButton.Visibility = Visibility.Collapsed;
                DivRadioButton.Visibility = Visibility.Collapsed;
            }


            RandomNumbersButton.Visibility = Visibility.Visible;
            RandomOperationButton.Visibility = Visibility.Visible;
            CalculateButton.Visibility = Visibility.Visible;
            SendAnswerButton.Visibility = Visibility.Visible;
            ResetButton.Visibility = Visibility.Visible;
            CloseButton.Visibility = Visibility.Visible;
            OkLabel.Visibility = Visibility.Visible;
            OkTextBlock.Visibility = Visibility.Visible;
            FailLabel.Visibility = Visibility.Visible;
            FailTextBlock.Visibility = Visibility.Visible;
            GradeTextLabel.Visibility = Visibility.Visible;
            GradeTextBlock.Visibility = Visibility.Visible;
            GuidelinesLabel.Visibility = Visibility.Visible;


        }

        private void GradeAButton_Click(object sender, RoutedEventArgs e)
        {
            MinGrade = 1;
            MaxGrade = 6;
            IsMultiDivAvailable = false;
            ChangeVisibilities();


        }

        private void GradeBButton_Click(object sender, RoutedEventArgs e)
        {
            MinGrade = 1;
            MaxGrade = 11;
            IsMultiDivAvailable = false;
            ChangeVisibilities();

        }

        private void GradeCButton_Click(object sender, RoutedEventArgs e)
        {
            MinGrade = 1;
            MaxGrade = 16;
            IsMultiDivAvailable = true;
            ChangeVisibilities();

        }

        private void GradeDButton_Click(object sender, RoutedEventArgs e)
        {
            MinGrade = 1;
            MaxGrade = 21;
            IsMultiDivAvailable = true;
            ChangeVisibilities();

        }



    }
}

using System;
using System.Windows.Forms;

namespace SimpleCalculatorApp
{
    public partial class Form1 : Form
    {
        // Variables to store calculator state
        double result = 0;
        string operation = "";
        bool isOperationPerformed = false;

        public Form1()
        {
            InitializeComponent();
        }

        // Event handler for digit buttons (0-9) and decimal point
        private void button_Click(object sender, EventArgs e)
        {
            if (displayTextBox.Text == "0" || isOperationPerformed)
            {
                displayTextBox.Clear();
                isOperationPerformed = false;
            }

            Button button = (Button)sender;

            // Handle decimal point logic
            if (button.Text == ".")
            {
                if (!displayTextBox.Text.Contains("."))
                {
                    displayTextBox.Text += button.Text;
                }
            }
            else // For digits
            {
                displayTextBox.Text += button.Text;
            }
        }

        // Event handler for operation buttons (+, -, *, /)
        private void operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (result != 0 && isOperationPerformed == false)
            {
                // If an operation was already pending and a new number was entered
                buttonEquals.PerformClick(); // Calculate previous operation first
            }

            operation = button.Text;
            result = double.Parse(displayTextBox.Text);
            isOperationPerformed = true;
        }

        // Event handler for the Clear (C) button
        private void buttonClear_Click(object sender, EventArgs e)
        {
            displayTextBox.Text = "0";
            result = 0;
            operation = "";
            isOperationPerformed = false;
        }

        // Event handler for the Equals (=) button
        private void buttonEquals_Click(object sender, EventArgs e)
        {
            switch (operation)
            {
                case "+":
                    displayTextBox.Text = (result + double.Parse(displayTextBox.Text)).ToString();
                    break;
                case "-":
                    displayTextBox.Text = (result - double.Parse(displayTextBox.Text)).ToString();
                    break;
                case "*":
                    displayTextBox.Text = (result * double.Parse(displayTextBox.Text)).ToString();
                    break;
                case "/":
                    // Handle division by zero
                    if (double.Parse(displayTextBox.Text) != 0)
                    {
                        displayTextBox.Text = (result / double.Parse(displayTextBox.Text)).ToString();
                    }
                    else
                    {
                        displayTextBox.Text = "Error: Div by 0";
                    }
                    break;
                default:
                    // If no operation selected, just display current number
                    break;
            }
            result = double.Parse(displayTextBox.Text); // Store the current result for chained operations
            operation = ""; // Clear the operation
            isOperationPerformed = false; // Reset for next input
        }

        // --- Important: Link Buttons to Event Handlers ---
        // You need to manually link the digit buttons and operator buttons
        // to their respective generic event handlers in the Properties window.
        // Or, you can set them here if you didn't double-click each individually.

        // If you double-clicked each button, Visual Studio created a separate method for each.
        // It's much cleaner to use shared event handlers.
        // To do this:
        // 1. Select ALL digit buttons (0-9 and decimal point if you have one) in the designer.
        // 2. In the Properties window, go to the "Events" tab (lightning bolt icon).
        // 3. Find the "Click" event. From the dropdown, select button_Click.
        // Do the same for ALL operator buttons (+, -, *, /), linking them to operator_Click.
        // The Clear and Equals buttons already have their unique handlers from double-clicking.
    }
}
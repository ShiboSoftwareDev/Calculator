using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FrmCalculator : Form
    {
        private string currentInput = "";
        private double result = 0;
        private string operation = "";
        private bool isOperationPerformed = false;

        public FrmCalculator()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Text)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (isOperationPerformed)
                    {
                        currentInput = button.Text;
                        isOperationPerformed = false;
                    }
                    else
                    {
                        currentInput += button.Text;
                    }
                    break;
                case "+":
                case "-":
                case "*":
                case "/":
                    if (currentInput != "")
                    {
                        if (operation != "")
                        {
                            PerformCalculation();
                        }
                        else
                        {
                            result = double.Parse(currentInput);
                        }
                        operation = button.Text;
                        isOperationPerformed = true;
                        LblOperation.Text = operation;
                    }
                    break;
                case "=":
                    if (currentInput != "" && operation != "")
                    {
                        PerformCalculation();
                        currentInput = result.ToString();
                        operation = "";
                        LblOperation.Text = "";

                        isOperationPerformed = true;

                    }
                    break;
                case ".":
                    if (!currentInput.Contains("."))
                    {
                        if (currentInput == "" || currentInput == "0")
                        {
                            currentInput = "0.";
                        }
                        else
                        {
                            currentInput += button.Text;
                        }
                    }
                    break;
                case "+/-":
                    if (currentInput != "")
                    {
                        double temp = double.Parse(currentInput);
                        temp *= -1;
                        currentInput = temp.ToString();
                    }
                    LblDisplayPrimary.Text = currentInput;
                    break;
                case "C":
                    currentInput = "";
                    break;
                case "CE":
                    currentInput = "";
                    result = 0;
                    operation = "";
                    LblOperation.Text = "";
                    break;
            }

            LblDisplayPrimary.Text = currentInput == "" ? "0" : currentInput;
            LblDisplaySecondary.Text = result.ToString();
        }

        private void PerformCalculation()
        {
            double inputNumber = double.Parse(currentInput);
                switch (operation)
                {
                    case "+":
                        result += inputNumber;
                        break;
                    case "-":
                        result -= inputNumber;
                        break;
                    case "*":
                        result *= inputNumber;
                        break;
                    case "/":
                        if (inputNumber != 0)
                            result /= inputNumber;
                        else
                            MessageBox.Show("Cannot divide by zero.");
                        break;
                    default:
                        result = inputNumber;
                        break;
                }
            
            currentInput = "";
        }
    }
}

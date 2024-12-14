using System;
using System.Runtime.InteropServices;
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

        private void Execute(string key)
        {
            switch (key)
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
                        currentInput = key;
                        isOperationPerformed = false;
                    }
                    else
                    {
                        currentInput += key;
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
                        operation = key;
                        isOperationPerformed = true;
                    }
                    break;

                case "=":
                    if (currentInput != "" && operation != "")
                    {
                        PerformCalculation();
                        currentInput = result.ToString();
                        operation = "";
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
                            currentInput += key;
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
                    break;

                case "C":
                    currentInput = "";
                    break;

                case "CE":
                    currentInput = "";
                    result = 0;
                    operation = "";
                    break;
            }

            // Update the displays
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
                    {
                        result /= inputNumber;
                    }
                    else
                    {
                        MessageBox.Show("Cannot divide by zero.");
                    }
                    break;
            }

            currentInput = "";
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Execute(button.Text);
        }

        private void FrmCalculator_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void FrmCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            string key = e.KeyCode.ToString().Remove(0, 1);
            Execute(key);
        }
    }
}

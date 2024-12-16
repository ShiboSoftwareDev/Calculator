using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FrmCalculator : Form
    {
        private string operationString = "";
        private double result = 0;
        private bool isNewInput = true;

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
                case ".":
                    if (isNewInput)
                    {
                        operationString = "";
                        LblDisplayPrimary.Text = "";
                        isNewInput = false;
                    }

                    LblDisplayPrimary.Text += key;
                    break;

                case "+":
                case "-":
                case "*":
                case "/":
                    if (isNewInput)
                    {
                        operationString = result.ToString() + key;
                        LblDisplayPrimary.Text = "";
                    }
                    else
                    {
                        operationString += LblDisplayPrimary.Text + key;
                    }

                    LblDisplayPrimary.Text = "";
                    isNewInput = false;
                    break;

                case "=":
                    if (!isNewInput && !string.IsNullOrEmpty(LblDisplayPrimary.Text))
                    {
                        operationString += LblDisplayPrimary.Text;
                        result = Evaluate(operationString);
                        LblDisplayPrimary.Text = result.ToString();
                        LblDisplaySecondary.Text = result.ToString();
                        isNewInput = true;
                    }
                    break;

                case "C":
                    operationString = "";
                    LblDisplayPrimary.Text = "0";
                    LblDisplaySecondary.Text = "0";
                    result = 0;
                    isNewInput = true;
                    break;

                case "CE":
                    LblDisplayPrimary.Text = "0";
                    break;

                case "+/-":
                    if (!string.IsNullOrEmpty(LblDisplayPrimary.Text))
                    {
                        double temp = double.Parse(LblDisplayPrimary.Text);
                        temp *= -1;
                        LblDisplayPrimary.Text = temp.ToString();
                    }
                    break;
            }

            LblDisplaySecondary.Text = operationString;
        }

        private double Evaluate(string expression)
        {
            try
            {
                DataTable table = new DataTable();
                table.CaseSensitive = false;
                return Convert.ToDouble(table.Compute(expression, string.Empty));
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid expression.");
                return 0;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Execute(button.Text);
        }

        private void FrmCalculator_Load(object sender, EventArgs e)
        {
            LblDisplayPrimary.Text = "0";
            LblDisplaySecondary.Text = "0";
        }

        private void FrmCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            string key = e.KeyCode.ToString();

            if (key.Length == 1 && char.IsDigit(key[0]))
            {
                Execute(key);
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Add:
                        Execute("+");
                        break;
                    case Keys.Subtract:
                        Execute("-");
                        break;
                    case Keys.Multiply:
                        Execute("*");
                        break;
                    case Keys.Divide:
                        Execute("/");
                        break;
                    case Keys.Enter:
                        Execute("=");
                        break;
                    case Keys.Back:
                        Execute("CE");
                        break;
                }
            }
        }
    }
}

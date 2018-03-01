using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;


namespace calculator
{
    public partial class Form1 : Form
    {

        double a, b, result = 0;
        String operation = "";
        bool divideByZeroException = false;
        bool equalClicked = false;
        bool zeroClicked = false;
        bool dotClicked = false;
        bool operatorClicked = false;

        public Form1()
        {
            InitializeComponent();
            CultureInfo en = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = en;
            CultureInfo.CurrentUICulture = en;
           
        }


        #region MyMethods

        private void Clear_Result()
        {
            if (equalClicked == true) Clear_All(); equalClicked = false;
        }

        private void Check_Division()
        {
            if (divideByZeroException == false) textBox.Text += result.ToString();
            else
            {
                textBox.Clear();
                label.Text = "Nie mozna dzielic przez 0.";
            }
        }

        private void Clear_All()
        {
            a = 0; b = 0; result = 0;
            operation = "";
            textBox.Text = "0";
            label.Text = "";
            zeroClicked = false;
            dotClicked = false;
        }

        private void ClearZero()
        {
            if (textBox.Text == "0") textBox.Clear();
        }

        void Calculate(string operation)
        {
            switch (operation)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    if (b != 0) result = a / b;
                    else
                    {
                        divideByZeroException = true;
                        Clear_All();
                    }
                    break;
            }
        }

        private bool Is_It_Operator(Button button)
        {
            if (button.Text == "+" || button.Text == "-" || button.Text == "*" || button.Text == "/") return true;
            return false;
        }

        #endregion

        #region Events

        private void button_Click(object sender, EventArgs e)
        {
            Clear_Result();
            ClearZero();
            Button button = (Button)sender;
            if (textBox.Text.Length <= 15) textBox.Text += button.Text;
        }

        private void equals_Click(object sender, EventArgs e)
        {
            if (equalClicked == true) textBox.Clear();
            if (a == 0 & b == 0 & result == 0 && textBox.Text == "0") return;

            label.Text += textBox.Text;

            if (textBox.TextLength > 0)
            {
                if (a == 0 && b == 0) { a = Double.Parse(textBox.Text); textBox.Text = a.ToString(); result = a; } //jesli wpisze sie jedna liczbe i =
                else b = Double.Parse(textBox.Text);
            }
            else
            {
                if (result != 0) a = result;
                label.Text = a.ToString() + " " + operation + " " + b.ToString();
            }

            textBox.Clear();
            Calculate(operation);
            Check_Division();
            equalClicked = true;
            divideByZeroException = false;
        }

        private void c_Click(object sender, EventArgs e)
        {
            Clear_All();
        }

        private void ce_Click(object sender, EventArgs e)
        {
            textBox.Text = "0";
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            string str = textBox.Text;

            if (str != "0" && str.Length != 0 && equalClicked == false) str = str.Substring(0, str.Length - 1);   

            textBox.Text = str;
        }

        private void changeSign_Click(object sender, EventArgs e)
        {
            if (result != 0)
            {
                result *= (-1);
                textBox.Text = result.ToString();
            }
        }

        private void dot_Click(object sender, EventArgs e)
        {
            Clear_Result();
            Button button = (Button)sender;
            dotClicked = true;

            if (textBox.Text.Length < 1) textBox.Text += "0" + button.Text;
            if (!textBox.Text.Contains(".")) textBox.Text += button.Text;
        }

        private void zero_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if ((dotClicked == true && zeroClicked == false) || textBox.Text != "0")
            {
                if (textBox.Text.Length <= 15) textBox.Text += button.Text;
            }

            zeroClicked = true;
            Clear_Result();
        }

        private void operator_Click(object sender, EventArgs e)
        {
            if (operatorClicked == true) equals_Click(sender, e);

            equalClicked = false;
            operatorClicked = true;

            Button button = (Button)sender;
            if (textBox.Text.Length < 1) textBox.Text += "0";

            label.Text += textBox.Text;

            if (result != 0) a = result;
            else a = Double.Parse(textBox.Text);

            label.Text = a.ToString();
            textBox.Clear();

            if (Is_It_Operator(button))
            {
                operation = button.Text;
                label.Text += " " + button.Text + " ";
            }
        }
    }
}

#endregion




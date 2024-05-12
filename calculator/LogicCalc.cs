using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
namespace calculator
{
    public class LogicCalc : Form
    {
        public TextBox inputField;
        private Form1 form1;

        public LogicCalc(TextBox inputField1, Form1 form)
        {
            this.form1 = form;
            this.inputField = inputField1;
        }
        public void keys()
        {
            // Create the buttons
            Button button1 = new Button();
            button1.Text = "1";
            button1.Location = new System.Drawing.Point(10, 40);
            button1.Size = new System.Drawing.Size(40, 40);
            button1.Click += Button_Click;
            form1.Controls.Add(button1);

            Button button2 = new Button();
            button2.Text = "2";
            button2.Location = new System.Drawing.Point(60, 40);
            button2.Size = new System.Drawing.Size(40, 40);
            button2.Click += Button_Click;
            form1.Controls.Add(button2);

            Button button3 = new Button();
            button3.Text = "3";
            button3.Location = new System.Drawing.Point(110, 40);
            button3.Size = new System.Drawing.Size(40, 40);
            button3.Click += Button_Click;
            form1.Controls.Add(button3);

            Button button4 = new Button();
            button4.Text = "4";
            button4.Location = new System.Drawing.Point(10, 90);
            button4.Size = new System.Drawing.Size(40, 40);
            button4.Click += Button_Click;
            form1.Controls.Add(button4);

            Button button5 = new Button();
            button5.Text = "5";
            button5.Location = new System.Drawing.Point(60, 90);
            button5.Size = new System.Drawing.Size(40, 40);
            button5.Click += Button_Click;
            form1.Controls.Add(button5);

            Button button6 = new Button();
            button6.Text = "6";
            button6.Location = new System.Drawing.Point(110, 90);
            button6.Size = new System.Drawing.Size(40, 40);
            button6.Click += Button_Click;
            form1.Controls.Add(button6);

            Button button7 = new Button();
            button7.Text = "7";
            button7.Location = new System.Drawing.Point(10, 140);
            button7.Size = new System.Drawing.Size(40, 40);
            button7.Click += Button_Click;
            form1.Controls.Add(button7);

            Button button8 = new Button();
            button8.Text = "8";
            button8.Location = new System.Drawing.Point(60, 140);
            button8.Size = new System.Drawing.Size(40, 40);
            button8.Click += Button_Click;
            form1.Controls.Add(button8);

            Button button9 = new Button();
            button9.Text = "9";
            button9.Location = new System.Drawing.Point(110, 140);
            button9.Size = new System.Drawing.Size(40, 40);
            button9.Click += Button_Click;
            form1.Controls.Add(button9);

            Button button10 = new Button();
            button10.Text = "+";
            button10.Location = new System.Drawing.Point(170, 90);
            button10.Size = new System.Drawing.Size(40, 40);
            button10.Click += Button_Click;
            form1.Controls.Add(button10);

            Button button11 = new Button();
            button11.Text = "-";
            button11.Location = new System.Drawing.Point(170, 140);
            button11.Size = new System.Drawing.Size(40, 40);
            button11.Click += Button_Click;
            form1.Controls.Add(button11);

            Button button12 = new Button();
            button12.Text = "*";
            button12.Location = new System.Drawing.Point(170, 190);
            button12.Size = new System.Drawing.Size(40, 40);
            button12.Click += Button_Click;
            form1.Controls.Add(button12);

            Button button13 = new Button();
            button13.Text = "/";
            button13.Location = new System.Drawing.Point(110, 190);
            button13.Size = new System.Drawing.Size(40, 40);
            button13.Click += Button_Click;
            form1.Controls.Add(button13);

            Button button14 = new Button();
            button14.Text = "0";
            button14.Location = new System.Drawing.Point(60, 190);
            button14.Size = new System.Drawing.Size(40, 40);
            button14.Click += Button_Click;
            form1.Controls.Add(button14);
        }
        public void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            inputField.Text += button.Text;
        }

        public void EqualsButton_Click(object sender, EventArgs e)
        {
            int result = EvaluateExpression(inputField.Text);
            MessageBox.Show(string.Format("the result is: " + result));
        }

        static int GetPriority(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
            }
            return 0;
        }

        static int ApplyOperation(int a, int b, char op)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                case '^':
                    int result = 1;
                    for (int i = 0; i < b; i++)
                    {
                        result *= a;
                    }
                    return result;
            }
            return 0;
        }

        static int EvaluateExpression(string expression)
        {
            Stack<int> values = new Stack<int>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ' ')
                    continue;

                if (expression[i] == '(')
                {
                    operators.Push(expression[i]);
                }
                else if (char.IsDigit(expression[i]))
                {
                    int value = 0;
                    while (i < expression.Length && char.IsDigit(expression[i]))
                    {
                        value = (value * 10) + (expression[i] - '0');
                        i++;
                    }
                    values.Push(value);
                    i--;
                }
                else if (expression[i] == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        int val2 = values.Pop();
                        int val1 = values.Pop();
                        char op = operators.Pop();
                        values.Push(ApplyOperation(val1, val2, op));
                    }
                    if (operators.Count > 0 && operators.Peek() == '(')
                        operators.Pop();
                }
                else
                {
                    while (operators.Count > 0 && GetPriority(operators.Peek()) >= GetPriority(expression[i]))
                    {
                        int val2 = values.Pop();
                        int val1 = values.Pop();
                        char op = operators.Pop();
                        values.Push(ApplyOperation(val1, val2, op));
                    }
                    operators.Push(expression[i]);
                }
            }

            while (operators.Count > 0)
            {
                int val2 = values.Pop();
                int val1 = values.Pop();
                char op = operators.Pop();
                values.Push(ApplyOperation(val1, val2, op));
            }
            return values.Pop();
        }
    }
}

using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
namespace calculator
{
    public partial class Form1 : Form
    {
        private TextBox inputField;
        LogicCalc logicCalc;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create the input field
           
            inputField = new TextBox();
            inputField.Location = new System.Drawing.Point(10, 10);
            inputField.Size = new System.Drawing.Size(200, 20);
            Controls.Add(inputField);

            logicCalc = new LogicCalc(inputField, this);
            
            logicCalc.keys();
            // Create the equals button
            Button equalsButton = new Button();
    
            equalsButton.Text = "=";
            equalsButton.Location = new System.Drawing.Point(170, 40);
            equalsButton.Size = new System.Drawing.Size(40, 40);
            equalsButton.Click += logicCalc.EqualsButton_Click;
            Controls.Add(equalsButton);

        }
    }
}
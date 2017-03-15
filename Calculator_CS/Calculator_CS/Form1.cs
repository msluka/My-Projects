using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
            private void click_numbers(object sender, EventArgs e)
            {
                var inputNumber = ((Button) sender).Text;
                if (textBox1.Text == "0")
                {
                    textBox1.Text = inputNumber;
                }
                else
                {
                    textBox1.Text += inputNumber;
                }

            }

            private void click_action(object sender, EventArgs e)
            {
                textBox3.Text = ((Button) sender).Text;
                textBox2.Text = textBox1.Text;
                textBox1.Text = null;
            }

        private bool on = false;

        private void click_on_off(object sender, EventArgs e)
        {
            if (on == false)
            {
                textBox1.Text = "0";
                textBox2.Text = String.Empty;
                textBox3.Text = null;
                on = true;
            }

            else
            {
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = null;
                on = false;
            }

            textBox1.SelectionLength = 0;
        }

        private void click_clear(object sender, EventArgs e)
            {
                textBox1.Text = "0";
                textBox2.Text = String.Empty;
                textBox3.Text = null;
            }

            private void click_backspace(object sender, EventArgs e)
            {
                if (textBox1.TextLength > 0)
                {
                    textBox1.Text = textBox1.Text.Substring(0, (textBox1.TextLength - 1));
                }
                if (textBox1.TextLength < 1)
                {
                    textBox1.Text = "0";
                }
            }

            private void click_calculate(object sender, EventArgs e)
            {

                double firstNumber = Double.Parse(textBox2.Text);
                double secondNumber = Double.Parse(textBox1.Text);
                string action = textBox3.Text;
                double addition = firstNumber + secondNumber;
                double subtraction = firstNumber - secondNumber;
                double multiplication = firstNumber * secondNumber;
                double division = firstNumber / secondNumber;
                double module = Convert.ToDouble(firstNumber) % Convert.ToDouble(secondNumber);

                switch (textBox3.Text)
                {
                    case "+":

                    {
                        textBox1.Text = addition.ToString();
                        break;
                    }

                    case "-":

                    {
                        textBox1.Text = subtraction.ToString();
                        break;
                    }

                    case "*":

                    {
                        textBox1.Text = multiplication.ToString();
                        break;
                    }

                    case "/":

                    {
                        if (firstNumber == 0 || secondNumber == 0)
                        {
                            textBox1.Text = "Error";
                        }
                        else
                        {
                            textBox1.Text = division.ToString();
                        }
                        break;

                    }

                    case "%":

                    {
                        textBox1.Text = module.ToString();
                        break;
                    }

                    default:
                    {
                        textBox1.Text = textBox1.Text;
                        break;
                    }

                }

            }

        }
    }


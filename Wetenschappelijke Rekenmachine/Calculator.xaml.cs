﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;
using System.Xml.XPath;

namespace Wetenschappelijke_Rekenmachine
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
            this.Textbox.Text = "0";
        }

        int OperatorCount = 0;
        int ClearEntry = 0;
        int MemoryCount = 0;
        int EqualsClickedCount = 0;
        //int HaakjeSluitedClickedCount = 0;

        double Number1 = 0.0;
        double Number2 = 0.0;
        double HistoryNumber = 0.0;
        double MemoryStoreNumber = 0.0;
        double MemoryRecall = 0.0;
        double Result = 0.0;

        string Input = string.Empty;
        string NextInput = string.Empty;
        string Operators = string.Empty;
        string OperatorDetected = string.Empty;
        string Detected = string.Empty;
        string RealOperator = string.Empty;
        string FirstOperator = string.Empty;
        string PreviousOperator = string.Empty;
        string CalculationString = string.Empty;

        List<string> OperatorsArray = new List<string>();
        List<double> MemoryArray = new List<double>();
        List<double> MemoryStoreArray = new List<double>();
        List<string> MemoryStoreShowArray = new List<string>();
        
        bool UpArrowClicked = false;
        bool HypClicked = false;

        string EquallityInTheMeantime()
        {
            if (OperatorCount > 2)
            {
                switch (PreviousOperator)
                {
                    case "+":
                        Result += Number2;
                        break;
                    case "-":
                        Result -= Number2;
                        break;
                    case "x":
                        Result *= Number2;
                        break;
                    case "÷":
                        Result /= Number2;
                        break;
                    case "Mod":
                        Result = Number1 % Number2;
                        break;
                    case "xY":
                        Result = Math.Pow(Number1, Number2);
                        break;
                }
            }

            if (OperatorCount == 2)
            {
                switch (FirstOperator)
                {
                    case "+":
                        Result = Number1 + Number2;
                        break;
                    case "-":
                        Result = Number1 - Number2;
                        break;
                    case "x":
                        Result = Number1 * Number2;
                        break;
                    case "÷":
                        Result = Number1 / Number2;
                        break;
                    case "Mod":
                        Result = Number1 % Number2;
                        break;
                    case "xY":
                        Result = Math.Pow(Number1, Number2);
                        break;
                }
            }

            this.Textbox.Text = Result.ToString();
            return this.Textbox.Text;
        }

        private void Clear_Entry(object sender, RoutedEventArgs e)
        {
            if (this.Textbox.Text.Length == 0 || this.Textbox.Text.Length == 1)
            {
                this.Textbox.Text = "0";
                NextInput = string.Empty;
            }
            else
            {
                this.Textbox.Text = this.Textbox.Text.Remove(0, ClearEntry);
                this.Textbox.Text = "0";
                NextInput = string.Empty;
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            this.Textbox.Text = "0";
            this.LabelTextBox.Text = "";
            OperatorCount = 0;
            ClearEntry = 0;
            Number1 = 0.0;
            Number2 = 0.0;
            Result = 0.0;
            NextInput = string.Empty;
            Operators = string.Empty;
            Input = string.Empty;
            OperatorDetected = string.Empty;
            Detected = string.Empty;
            RealOperator = string.Empty;
            FirstOperator = string.Empty;
            PreviousOperator = string.Empty;
            CalculationString = string.Empty;
        }

        private void Backspace(object sender, RoutedEventArgs e)
        {
            this.Textbox.Text = this.Textbox.Text.Remove(this.Textbox.Text.Length - 1);
            if (this.Textbox.Text.Length == 0)
            {
                this.Textbox.Text = "0";
            }

            Input = string.Empty;
            Operators = string.Empty;
        }

        public void ClickedNumber(object sender, RoutedEventArgs e)
        {
            if (OperatorCount == 0)
            {
                Input = "";
                this.Textbox.Text = "";
                Button Detected = (Button)sender;
                Input += Detected.Content.ToString();

                if (Input == "π")
                {
                    Input = 3.1415926535897932384626433832795.ToString();
                }

                if (Input == "(" || Input == ")")
                {
                    if (Textbox.Text == "0" || Textbox.Text == "")
                    {
                        Textbox.Text = "0";
                    }

                    else
                    {
                        this.Textbox.Text = Textbox.Text;
                    }
                }

                if (Input == "1" || Input == "2" || Input == "3" || Input == "4" || Input == "5" || Input == "6" || Input == "7" || Input == "8" || Input == "9" || Input == "0")
                {
                    Number1 = Convert.ToDouble(Input);
                    this.Textbox.Text = Input;
                }

                this.LabelTextBox.Text += " " + Input;
            }

            if (OperatorCount >= 1)
            {
                NextInput = "";
                Button Detected = (Button)sender;
                NextInput += Detected.Content.ToString();
                ClearEntry = NextInput.Length;

                if (NextInput == "π")
                {
                    NextInput = 3.1415926535897932384626433832795.ToString();
                }

                if (Input == "(" || Input == ")")
                {
                    if (Textbox.Text == "0" || Textbox.Text == "")
                    {
                        Textbox.Text = "0";
                    }

                    else
                    {
                        this.Textbox.Text = Number1.ToString();
                    }                    
                }

                if (Input == "1" || Input == "2" || Input == "3" || Input == "4" || Input == "5" || Input == "6" || Input == "7" || Input == "8" || Input == "9" || Input == "0")
                {
                    Number2 = Convert.ToDouble(NextInput);
                    this.Textbox.Text = NextInput;
                }

                this.LabelTextBox.Text += NextInput;
            }
        }

        public void Operator_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.LabelTextBox.Text.Contains("(") || this.LabelTextBox.Text.Contains(")"))
            {
                this.LabelTextBox.Text += CalculationString;
                Button Operators = (Button)sender;
                string OperatorDetected = Operators.Content.ToString();
                this.LabelTextBox.Text += " " + OperatorDetected;
            }

            else
            {
                this.LabelTextBox.Text = CalculationString;
                OperatorCount++;
                this.LabelTextBox.Text += " " + NextInput;
                CalculationString = this.LabelTextBox.Text;
                Button Operators = (Button)sender;
                string OperatorDetected = Operators.Content.ToString();
            }
            

            if (OperatorDetected == "xY")
            {
                OperatorDetected = "^";
            }

            RealOperator = OperatorDetected;
            OperatorsArray.Add(RealOperator);

            if (OperatorCount == 1)
            {
                if (this.LabelTextBox.Text.Contains("(") || this.LabelTextBox.Text.Contains(")"))
                {
                    FirstOperator = RealOperator;
                    CalculationString = this.LabelTextBox.Text;
                }

                else
                {
                    this.LabelTextBox.Text = this.Textbox.Text;
                    FirstOperator = RealOperator;
                    this.LabelTextBox.Text = this.Textbox.Text + " " + OperatorDetected;
                    CalculationString = this.LabelTextBox.Text;
                }                    
            }

            if (OperatorCount > 1)
            {
                if (OperatorDetected == "^")
                {
                    LabelTextBox.Text = Result.ToString() + " " + "^";
                    Textbox.Text = Result.ToString();
                }

                else
                {
                    for (int i = OperatorsArray.Count - 1; i < OperatorsArray.Count; i++)
                    {
                        PreviousOperator = OperatorsArray[i - 1];
                    }

                    this.LabelTextBox.Text = CalculationString + " " + OperatorDetected;
                    EquallityInTheMeantime();
                }                
            }

            NextInput = "";
        }

        public void Equals_Clicked(object sender, RoutedEventArgs e)
        {
            EqualsClickedCount++;

            switch (RealOperator)
            {
                case "+":
                    if (OperatorCount > 1 || EqualsClickedCount > 1)
                    {
                        Result += Number2;

                    }
                    else
                    {
                        Result = Number1 + Number2;
                    }
                    break;

                case "-":
                    if (OperatorCount > 1 || EqualsClickedCount > 1)
                    {
                        Result -= Number2;

                    }
                    else
                    {
                        Result = Number1 - Number2;
                    }
                    break;

                case "÷":
                    if (OperatorCount > 1 || EqualsClickedCount > 1)
                    {
                        Result /= Number2;

                    }
                    else
                    {
                        Result = Number1 / Number2;
                    }
                    break;

                case "x":
                    if (OperatorCount > 1 || EqualsClickedCount > 1)
                    {
                        Result *= Number2;
                    }
                    else
                    {
                        Result = Number1 * Number2;
                    }
                    break;

                case "Mod":
                    if (OperatorCount > 1 || EqualsClickedCount > 1)
                    {
                        Result %= Number2;
                    }
                    else
                    {
                        Result = Number1 % Number2;
                    }
                    break;

                case "^":
                    if (OperatorCount > 1 || EqualsClickedCount > 1)
                    {
                        Result = Math.Pow(Result, Number2);
                    }
                    else
                    {
                        Result = Math.Pow(Number1, Number2);
                    }
                    break;

                default:
                    this.Textbox.Text = "Invalid Input";
                    break;
            }            

            this.LabelTextBox.Text = "";
            this.Textbox.Text = Result.ToString();
        }        

        private void MemoryClear_Click(object sender, RoutedEventArgs e)
        {
            MemoryArray.Clear();
            MemoryCount = 0;
            Input = string.Empty;
            MC.IsEnabled = false;
            MT.IsEnabled = false;
            MR.IsEnabled = false;
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            this.Textbox.Text = MemoryRecall.ToString();
        }

        private void AddWithNumberInMemory_Click(object sender, RoutedEventArgs e)
        {
            MC.IsEnabled = true;
            MT.IsEnabled = true;
            MR.IsEnabled = true;
            HistoryNumber = Convert.ToDouble(this.Textbox.Text);
            MemoryCount++;

            if (MemoryCount == 1)
            {
                MemoryArray.Add(HistoryNumber);
            }

            MemoryCount = 0;
            Input = string.Empty;
        }

        private void SubtractWithNumberInMemory_Click(object sender, RoutedEventArgs e)
        {
            MC.IsEnabled = true;
            MT.IsEnabled = true;
            MR.IsEnabled = true;
            HistoryNumber = Convert.ToDouble(this.Textbox.Text);
            MemoryCount++;

            if (MemoryCount == 1)
            {
                MemoryArray.Add(HistoryNumber);
            }

            MemoryCount = 0;
            Input = string.Empty;
        }

        private void MemoryStore_Click(object sender, RoutedEventArgs e)
        {
            //Zorgen voor juiste weergave in de textbox.
            MC.IsEnabled = true;
            MT.IsEnabled = true;
            MR.IsEnabled = true;
            MemoryStoreNumber = Convert.ToDouble(this.Textbox.Text);
            Input = string.Empty;
        }

        private void GRAD_Click(object sender, RoutedEventArgs e)
        {
            GRAD.Visibility = Visibility.Hidden;
            DEG.Visibility = Visibility.Visible;
        }

        private void DEG_Click(object sender, RoutedEventArgs e)
        {
            DEG.Visibility = Visibility.Hidden;
            RAD.Visibility = Visibility.Visible;
        }

        private void RAD_Click(object sender, RoutedEventArgs e)
        {
            RAD.Visibility = Visibility.Hidden;
            GRAD.Visibility = Visibility.Visible;
        }

        //private void ToRadian()
        //{
        //    Textbox.Text = (Math.PI * double.Parse(Textbox.Text) / 180.0).ToString();
        //}

        //private void ToDegrees()
        //{
        //    Textbox.Text = (double.Parse(Textbox.Text) * (180.0 / Math.PI)).ToString();
        //}

        //private void ToGrad()
        //{
        //    //Unknown
        //}
        
        //Formule voor Degree to Radian
        //Math.PI * Input / 180.0;

        //Formule voor Radian to Degree
        //Input * (180.0 / Math.PI);

        //Formule voor Degree to Grad
        //...               

        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Sin(double.Parse(Textbox.Text))).ToString();
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Cos(double.Parse(Textbox.Text))).ToString();
        }

        private void Tan_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Tan(double.Parse(Textbox.Text))).ToString();            
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Log(double.Parse(Textbox.Text))).ToString();
        }

        private void Exp_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Exp(double.Parse(Textbox.Text))).ToString();
        }        

        private void xY_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TenthPower_Click(object sender, RoutedEventArgs e)
        {
            Result = Math.Pow(10, Number1);
            CalculationString = Result.ToString();
            Textbox.Text = CalculationString.Remove(CalculationString.Length - 1);            
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            OperatorCount++;

            if (OperatorCount > 1)
            {
                Result = Math.Sqrt(Result);
                Textbox.Text = Result.ToString();
                CalculationString = LabelTextBox.Text;

                for (int i = 1; i < OperatorCount; i++)
                {
                    LabelTextBox.Text = "✔(" + CalculationString + ")";
                }
            }
            else
            {
                Result = Math.Sqrt(Number1);
                Textbox.Text = Result.ToString();
                LabelTextBox.Text = "✔(" + Number1.ToString() + ")";
            }
        }

        private void Exponents_Click(object sender, RoutedEventArgs e)
        {
            OperatorCount++;

            if (OperatorCount > 1)
            {
                Result = Result * Result;
                Textbox.Text = Result.ToString();
                CalculationString = LabelTextBox.Text;

                for (int i = 1; i < OperatorCount; i++)
                {
                    LabelTextBox.Text = "sqr(" + CalculationString + ")";
                }
            }
            else
            {
                Result = Number1 * Number1;
                Textbox.Text = Result.ToString();
                LabelTextBox.Text = "sqr(" + Number1.ToString() + ")";
            }
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Textbox.Text.StartsWith("-"))
            {
                Textbox.Text = Textbox.Text.Substring(1);
            }
            else if (!string.IsNullOrEmpty(Textbox.Text))
            {
                Textbox.Text = "-" + Textbox.Text;
            }
        }

        private void ExtraCalculations_Click(object sender, RoutedEventArgs e)
        {
            UpArrowClicked = false;

            UpArrow.Visibility = Visibility.Hidden;
            ClickedAgain.Visibility = Visibility.Visible;

            x2.Visibility = Visibility.Visible;
            xy.Visibility = Visibility.Visible;
            sin.Visibility = Visibility.Visible;
            cos.Visibility = Visibility.Visible;
            tan.Visibility = Visibility.Visible;
            Nike.Visibility = Visibility.Visible;
            TenthPower.Visibility = Visibility.Visible;
            log.Visibility = Visibility.Visible;
            Exp.Visibility = Visibility.Visible;
            Mod.Visibility = Visibility.Visible;

            powerthree.Visibility = Visibility.Hidden;
            yrootx.Visibility = Visibility.Hidden;
            sin1.Visibility = Visibility.Hidden;
            cos1.Visibility = Visibility.Hidden;
            tan1.Visibility = Visibility.Hidden;
            dividex.Visibility = Visibility.Hidden;
            ex.Visibility = Visibility.Hidden;
            ln.Visibility = Visibility.Hidden;
            dms.Visibility = Visibility.Hidden;
            deg.Visibility = Visibility.Hidden;

            if (HypClicked == true && UpArrowClicked == true)
            {
                SinH1.Visibility = Visibility.Visible;
                CosH1.Visibility = Visibility.Visible;
                TanH1.Visibility = Visibility.Visible;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
            }

            if (HypClicked == true && UpArrowClicked == false)
            {
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Visible;
                CosH.Visibility = Visibility.Visible;
                TanH.Visibility = Visibility.Visible;
            }

            if (HypClicked == false && UpArrowClicked == true)
            {
                sin1.Visibility = Visibility.Visible;
                cos1.Visibility = Visibility.Visible;
                tan1.Visibility = Visibility.Visible;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
            }

            if (HypClicked == false && UpArrowClicked == false)
            {
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Visible;
                cos.Visibility = Visibility.Visible;
                tan.Visibility = Visibility.Visible;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
            }
        }

        private void ClickedAgain_Click(object sender, RoutedEventArgs e)
        {
            UpArrowClicked = true;

            ClickedAgain.Visibility = Visibility.Hidden;
            UpArrow.Visibility = Visibility.Visible;

            x2.Visibility = Visibility.Hidden;
            xy.Visibility = Visibility.Hidden;
            sin.Visibility = Visibility.Hidden;
            cos.Visibility = Visibility.Hidden;
            tan.Visibility = Visibility.Hidden;
            Nike.Visibility = Visibility.Hidden;
            TenthPower.Visibility = Visibility.Hidden;
            log.Visibility = Visibility.Hidden;
            Exp.Visibility = Visibility.Hidden;
            Mod.Visibility = Visibility.Hidden;

            powerthree.Visibility = Visibility.Visible;
            yrootx.Visibility = Visibility.Visible;
            sin1.Visibility = Visibility.Visible;
            cos1.Visibility = Visibility.Visible;
            tan1.Visibility = Visibility.Visible;
            dividex.Visibility = Visibility.Visible;
            ex.Visibility = Visibility.Visible;
            ln.Visibility = Visibility.Visible;
            dms.Visibility = Visibility.Visible;
            deg.Visibility = Visibility.Visible;

            if (HypClicked == true && UpArrowClicked == true)
            {
                SinH1.Visibility = Visibility.Visible;
                CosH1.Visibility = Visibility.Visible;
                TanH1.Visibility = Visibility.Visible;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
            }

            if (HypClicked == true && UpArrowClicked == false)
            {
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Visible;
                CosH.Visibility = Visibility.Visible;
                TanH.Visibility = Visibility.Visible;
            }

            if (HypClicked == false && UpArrowClicked == true)
            {
                sin1.Visibility = Visibility.Visible;
                cos1.Visibility = Visibility.Visible;
                tan1.Visibility = Visibility.Visible;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
            }

            if (HypClicked == false && UpArrowClicked == false)
            {
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Visible;
                cos.Visibility = Visibility.Visible;
                tan.Visibility = Visibility.Visible;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
            }
        }

        private void HYP_Click(object sender, RoutedEventArgs e)
        {
            HypClicked = true;
            Hyp_Copy.Visibility = Visibility.Visible;
            Hyp.Visibility = Visibility.Hidden;

            if (UpArrowClicked == true && HypClicked == true)
            {
                SinH1.Visibility = Visibility.Visible;
                CosH1.Visibility = Visibility.Visible;
                TanH1.Visibility = Visibility.Visible;
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
            }

            if (UpArrowClicked == true && HypClicked == false)
            {
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                sin1.Visibility = Visibility.Visible;
                cos1.Visibility = Visibility.Visible;
                tan1.Visibility = Visibility.Visible;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
            }

            if (UpArrowClicked == false && HypClicked == true)
            {
                SinH.Visibility = Visibility.Visible;
                CosH.Visibility = Visibility.Visible;
                TanH.Visibility = Visibility.Visible;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
            }

            if (UpArrowClicked == false && HypClicked == false)
            {
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Visible;
                cos.Visibility = Visibility.Visible;
                tan.Visibility = Visibility.Visible;
            }
        }

        private void HYPCopy_Click(object sender, RoutedEventArgs e)
        {
            HypClicked = false;
            Hyp_Copy.Visibility = Visibility.Hidden;
            Hyp.Visibility = Visibility.Visible;

            if (UpArrowClicked == true && HypClicked == true)
            {                
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Visible;
                CosH1.Visibility = Visibility.Visible;
                TanH1.Visibility = Visibility.Visible;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
            }

            if (UpArrowClicked == true && HypClicked == false)
            {
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                sin1.Visibility = Visibility.Visible;
                cos1.Visibility = Visibility.Visible;
                tan1.Visibility = Visibility.Visible;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
            }

            if (UpArrowClicked == false && HypClicked == true)
            {
                sin.Visibility = Visibility.Hidden;
                cos.Visibility = Visibility.Hidden;
                tan.Visibility = Visibility.Hidden;
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Visible;
                CosH.Visibility = Visibility.Visible;
                TanH.Visibility = Visibility.Visible;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
            }

            if (UpArrowClicked == false && HypClicked == false)
            {
                sin.Visibility = Visibility.Visible;
                cos.Visibility = Visibility.Visible;
                tan.Visibility = Visibility.Visible;
                sin1.Visibility = Visibility.Hidden;
                cos1.Visibility = Visibility.Hidden;
                tan1.Visibility = Visibility.Hidden;
                SinH.Visibility = Visibility.Hidden;
                CosH.Visibility = Visibility.Hidden;
                TanH.Visibility = Visibility.Hidden;
                SinH1.Visibility = Visibility.Hidden;
                CosH1.Visibility = Visibility.Hidden;
                TanH1.Visibility = Visibility.Hidden;
            }
        }

        private void FE_Click(object sender, RoutedEventArgs e)
        {
            FE.Visibility = Visibility.Hidden;
            FE_Copy.Visibility = Visibility.Visible;
        }              

        private void FECopy_Click(object sender, RoutedEventArgs e)
        {
            FE.Visibility = Visibility.Visible;
            FE_Copy.Visibility = Visibility.Hidden;
        }

        private void PI_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = Math.PI.ToString();
        }

        private void Factorial_Click(object sender, RoutedEventArgs e)
        {
            if (Number1 <= 1)
            {
                Textbox.Text = "1";
            }

            else
            {
                Result = Number1;

                for (int i = 1; i < Number1; i++)
                {
                    Result = Result * i;
                }

                Textbox.Text = Result.ToString();
            }

            OperatorCount = 0;
            Input = "";
        }
    }
}
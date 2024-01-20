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

namespace WpfCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string trig_checker { get; set; } = "DEG";
        private double valueNow { get; set; }
        private double valueMemoried { get; set; }
        private IOperation operationMemoried { get; set; } = null;
        private bool isComma { get; set; } = false;
        private bool isOperationGo { get; set; } = false;
        private bool isOperationsBlocked { get; set; } = false;
        private double preReversedValue { get; set; }
        private int preReversedLength { get; set; }
        private bool isReversing { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCifrus_Click(object sender, RoutedEventArgs e)
        {
            var taked_object = e.Source as Button;
            if(!isOperationsBlocked)
            {
                if ((valueNow == 0 || isOperationGo) && !isComma)
                {
                    OutputFieldBlock.Text = taked_object.Content.ToString();
                    valueNow = Convert.ToDouble(taked_object.Content);
                    isOperationGo = false;
                }
                else
                {
                    OutputFieldBlock.Text += taked_object.Content.ToString();
                    valueNow = Convert.ToDouble(OutputFieldBlock.Text);
                }
            }
            else
            {
                OutputFieldBlock.Text = taked_object.Content.ToString();
                valueNow = Convert.ToDouble(taked_object.Content);
                valueMemoried = new double();
                OldFieldBlock.Text = valueMemoried.ToString();
                isOperationsBlocked = false;
            }
            isReversing = false;
        }

        private void DoOperate() //Прибывшие сюда данные уже отформатированы. Главное проведение операций
        {
            ExtendedAnsClass answer = operationMemoried.Operate(valueMemoried, valueNow);
            if(answer._Desc_id == -1)
            {
                double con_answer = Math.Round(Convert.ToDouble(answer._Answer), 10);
                OutputFieldBlock.Text = con_answer.ToString();
                valueMemoried = 0;
                valueNow = con_answer;
                operationMemoried = null;
            }
            else if(answer._Desc_id == 1)
            {
                OutputFieldBlock.Text = answer._Desc;
                isOperationsBlocked = true;
                isOperationGo = false;
                operationMemoried = null;
            }
            else if (answer._Desc_id == 2)
            {
                OutputFieldBlock.Text = answer._Desc;
                isOperationsBlocked = true;
                isOperationGo = false;
                operationMemoried = null;
            }
            isReversing = false;
        }

        private bool DoOperate(IOperation temp_operation) //Прибывшие сюда данные уже отформатированы. Проведение второстепенных операций
        {
            bool isNotNormalOperate = true;
            ExtendedAnsClass answer = temp_operation.Operate(valueMemoried, valueNow);
            if (answer._Desc_id == -1)
            {
                double con_answer = Math.Round(Convert.ToDouble(answer._Answer), 10);
                valueNow = con_answer;
                OutputFieldBlock.Text = valueNow.ToString();
                isNotNormalOperate = false;
            }
            else if (answer._Desc_id == 1)
            {
                OutputFieldBlock.Text = answer._Desc;
                isOperationsBlocked = true;
                isOperationGo = false;
            }
            else if (answer._Desc_id == 2)
            {
                OutputFieldBlock.Text = answer._Desc;
                isOperationsBlocked = true;
                isOperationGo = false;
            }

            isReversing = false;
            return isNotNormalOperate;
        }

        private void ButtonComma_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                if (!isComma)
                {
                    OutputFieldBlock.Text += ",";
                    isComma = true;
                }
            }
        }

        private void PlusBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                OldFieldBlock.Text = valueNow.ToString() + " +";
                operationMemoried = new Sum();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }

        private void MinusBut_Click(object sender, RoutedEventArgs e)
        {
            if(!isOperationsBlocked)
            {
                OldFieldBlock.Text = valueNow.ToString() + " -";
                operationMemoried = new Subtraction();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }

        private void MultiplicateBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                OldFieldBlock.Text = valueNow.ToString() + " *";
                operationMemoried = new Multiplication();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }

        private void DivisionBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                OldFieldBlock.Text = valueNow.ToString() + " /";
                operationMemoried = new Division();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }

        private void PercentageOfTwoBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked && valueNow != 0)
            {
                valueNow = (valueMemoried / 100) * valueNow;

                OutputFieldBlock.Text = valueNow.ToString();
            }
        }

        private void EqualBut_Click(object sender, RoutedEventArgs e)
        {
            if(!isOperationsBlocked)
            {
                if (operationMemoried == null)
                {
                    OldFieldBlock.Text = valueNow.ToString();

                    OldFieldBlock.Text += " =";
                    operationMemoried = null;
                }
                else
                {
                    var arr = OldFieldBlock.Text.ToArray();
                    if (arr[arr.Length - 1].ToString() != "=")
                    {
                        OldFieldBlock.Text += " " + valueNow.ToString();
                        DoOperate();

                        OldFieldBlock.Text += " =";
                        operationMemoried = null;
                    }
                }
            }
            else
            {
                DeleteAllBut_Click(DeleteAllBut, e);
            }
           /* OldFieldBlock.Text = "";*/
            
        }

        private void DeleteAllBut_Click(object sender, RoutedEventArgs e)
        {
            valueNow = new double();
            valueMemoried = new double();
            OutputFieldBlock.Text = valueNow.ToString();
            OldFieldBlock.Text = valueMemoried.ToString();
            operationMemoried = null;
            isOperationGo = false;
            isOperationsBlocked = false;
            isComma = false;
            isReversing = false;
        }

        private void DeleteNowBut_Click(object sender, RoutedEventArgs e)
        {
            valueNow = new double();
            OutputFieldBlock.Text = valueNow.ToString();
            isReversing = false;
        }

        private void ReverseSignBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                valueNow = -valueNow;
                OutputFieldBlock.Text = valueNow.ToString();
            }
        }

        private void RootBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                if (valueNow != 0 && operationMemoried == null)
                {
                    OldFieldBlock.Text = ($"√{valueNow} =");
                    valueNow = Convert.ToDouble(Math.Sqrt(valueNow));
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else if (valueNow != 0 && operationMemoried != null)
                {
                    OldFieldBlock.Text += ($"√{valueNow} ");
                    valueNow = Convert.ToDouble(Math.Sqrt(valueNow));
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void DeleteSymbolBut_Click(object sender, RoutedEventArgs e)
        {
            if(!isOperationsBlocked)
            {
                var arr = OutputFieldBlock.Text.ToArray();
                if (arr[arr.Length/* - 1*/].ToString() == ",")
                {
                    isComma = false;
                }

                if (OutputFieldBlock.Text != "0" && arr.Length > 1)
                {
                    string value = "";
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        value += arr[i];
                    }
                    OutputFieldBlock.Text = value;
                    valueNow = Convert.ToDouble(OutputFieldBlock.Text);
                }
                else if (arr.Length == 1)
                {
                    OutputFieldBlock.Text = "0";
                    valueNow = 0;
                }
            }
            else
            {
                DeleteAllBut_Click(DeleteAllBut, e);
            }
            isReversing = false;
        }

        private void ReverseValueBut_Click(object sender, RoutedEventArgs e)
        {
            if(!isReversing)
            {
                ReverseCleanUp();
            }

            if(valueNow != 0 )
            {
                if ((preReversedValue == Math.Round((1 / valueNow), preReversedLength)) && preReversedValue != 0)
                {
                    valueNow = preReversedValue;
                }
                else
                {
                    preReversedValue = valueNow;
                    preReversedLength = valueNow.ToString().Count();
                    valueNow = Math.Round((1 / valueNow), 10);
                }
                OutputFieldBlock.Text = valueNow.ToString();
                isReversing = true;
            }
        }

        private void ReverseCleanUp()
        {
            preReversedValue = 0;
            preReversedLength = 0;

        }

        private void DegreeOfTwoBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                if (valueMemoried == 0)
                {
                    OldFieldBlock.Text = $"{valueNow}² =";
                    valueNow = Math.Pow(valueNow, 2);
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    OldFieldBlock.Text += $"{valueNow}² ";
                    valueNow = Math.Pow(valueNow, 2);
                    OldFieldBlock.Text += $"({valueNow}) =";
                    DoOperate();
                }
            }
        }

        private void DegreeOfValueBut_Click(object sender, RoutedEventArgs e)
        {
            if(!isOperationsBlocked)
            {
                OldFieldBlock.Text = valueNow.ToString() + " ^";
                operationMemoried = new DegreeOfValue();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }

        private void DegreeOfThreeBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                if (valueMemoried == 0)
                {
                    OldFieldBlock.Text = $"{valueNow}³ =";
                    valueNow = Math.Pow(valueNow, 3);
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    OldFieldBlock.Text += $"{valueNow}³ ";
                    valueNow = Math.Pow(valueNow, 3);
                    OldFieldBlock.Text += $"({valueNow}) =";
                    DoOperate();
                }
            }

            
        }

        private void RootOfValueBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                OldFieldBlock.Text =  $"{valueNow} ^ 1/";
                operationMemoried = new RootOfValue();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }

        private void FactorialBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                if (operationMemoried == null)
                {
                    OldFieldBlock.Text = ($"fact({valueNow}) =");
                    double factorial = 1;
                    for(int i = 1; i < valueNow + 1; i++)
                    {
                        factorial *= i;
                    }
                    valueNow = factorial;
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    OldFieldBlock.Text += ($"fact({valueNow}) "); 
                    double factorial = 1;
                    for (int i = 1; i < valueNow + 1; i++)
                    {
                        factorial *= i;
                    }
                    valueNow = factorial;
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void DegreeForTenBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                if (operationMemoried == null)
                {
                    OldFieldBlock.Text = ($"10 ^ {valueNow} =");
                    valueNow = Math.Pow(10, valueNow);
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    OldFieldBlock.Text += ($"10 ^ {valueNow} ");
                    valueNow = Math.Pow(10, valueNow);
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void Log10But_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                IOperation temp_operation = new Log10Value();
                if (operationMemoried == null)
                {
                    OldFieldBlock.Text = ($"log({valueNow}) =");
                    /*
                    OldFieldBlock.Text = ($"log({valueNow}) =");*/

                    DoOperate(temp_operation);/*
                    if(!isNotNormalOperate)
                    {
                        DoOperate();
                    }*/
                }
                else
                {
                    OldFieldBlock.Text += ($" log({valueNow}) ");
                    bool isNotNormalOperate = DoOperate(temp_operation);
                    OldFieldBlock.Text += ($"[{valueNow}] ");
                    if (!isNotNormalOperate)
                    {
                        DoOperate();
                    }
                }
            }
        }

        private void LogEBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                IOperation temp_operation = new LogEValue();
                if (operationMemoried == null)
                {
                    OldFieldBlock.Text = ($"ln({valueNow}) =");
                    /*
                    OldFieldBlock.Text = ($"log({valueNow}) =");*/

                    DoOperate(temp_operation);/*
                    if(!isNotNormalOperate)
                    {
                        DoOperate();
                    }*/
                }
                else
                {
                    OldFieldBlock.Text += ($" ln[{valueNow}] ");
                    bool isNotNormalOperate = DoOperate(temp_operation);
                    if (!isNotNormalOperate)
                    {
                        DoOperate();
                    }
                }
            }
        }

        private void PiBut_Click(object sender, RoutedEventArgs e)
        {
            if(!isOperationsBlocked)
            {
                valueNow = Math.PI;
                OutputFieldBlock.Text = valueNow.ToString();
            }
        }

        private void RootOfThreeBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                OldFieldBlock.Text = $"{valueNow} ^ 1/";
                operationMemoried = new RootOfValue();
                valueMemoried = valueNow;
                valueNow = 3;

                isOperationGo = true;
            }
        }

        private void ExpBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                OldFieldBlock.Text = $"{valueNow},e + ";
                operationMemoried = new Exp();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }

        private void SinusBut_Click(object sender, RoutedEventArgs e)
        {
            if(!isOperationsBlocked && valueNow < 720)
            {
                if (operationMemoried == null)
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text = ($"Sin({valueNow}) =");
                        valueNow = Math.Sin(valueNow * (Math.PI / 180));
                    }
                    else if(trig_checker == "RAD")
                    {
                        OldFieldBlock.Text = ($"Sin({valueNow}) =");
                        valueNow = Math.Sin(valueNow);
                    }
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text += ($"Sin({valueNow}) ");
                        valueNow = Math.Sin(valueNow * (Math.PI / 180));
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text += ($"Sin({valueNow}) ");
                        valueNow = Math.Sin(valueNow);
                    }
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void RadioDEGButton_Checked(object sender, RoutedEventArgs e)
        {
            trig_checker = "DEG";
        }

        private void RadioRADButton_Checked(object sender, RoutedEventArgs e)
        {
            trig_checker = "RAD";
        }

        private void CosinusBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked && valueNow < 720)
            {
                if (operationMemoried == null)
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text = ($"Cos({valueNow}) =");
                        valueNow = Math.Cos(valueNow * (Math.PI / 180));
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text = ($"Cos({valueNow}) =");
                        valueNow = Math.Cos(valueNow);
                    }
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text += ($"Cos({valueNow}) ");
                        valueNow = Math.Cos(valueNow * (Math.PI / 180));
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text += ($"Cos({valueNow}) ");
                        valueNow = Math.Cos(valueNow);
                    }
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void TangentBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked && valueNow < 720)
            {
                if (operationMemoried == null)
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text = ($"Tan({valueNow}) =");
                        valueNow = Math.Tan(valueNow * (Math.PI / 180));
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text = ($"Tan({valueNow}) =");
                        valueNow = Math.Tan(valueNow);
                    }
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text += ($"Tan({valueNow}) ");
                        valueNow = Math.Tan(valueNow * (Math.PI / 180));
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text += ($"Tan({valueNow}) ");
                        valueNow = Math.Tan(valueNow);
                    }
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void ArcSinusBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked && valueNow < 720)
            {
                if (operationMemoried == null)
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text = ($"Arcsin({valueNow}) =");
                        valueNow = Math.Asin(valueNow) * (180 / Math.PI);
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text = ($"Arcsin({valueNow}) =");
                        valueNow = Math.Asin(valueNow);
                    }
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text += ($"Arcsin({valueNow}) ");
                        valueNow = Math.Asin(valueNow) * (180 / Math.PI);
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text += ($"Arcsin({valueNow}) ");
                        valueNow = Math.Asin(valueNow);
                    }
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void ArcCosinusBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked && valueNow < 720)
            {
                if (operationMemoried == null)
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text = ($"Arccos({valueNow}) =");
                        valueNow = Math.Acos(valueNow) * (180 / Math.PI);
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text = ($"Arccos({valueNow}) =");
                        valueNow = Math.Acos(valueNow);
                    }
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text += ($"Arccos({valueNow}) ");
                        valueNow = Math.Acos(valueNow) * (180 / Math.PI);
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text += ($"Arccos({valueNow}) ");
                        valueNow = Math.Acos(valueNow);
                    }
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void ArcTangentBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked && valueNow < 720)
            {
                if (operationMemoried == null)
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text = ($"Arctan({valueNow}) =");
                        valueNow = Math.Atan(valueNow) * (180 / Math.PI);
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text = ($"Arctan({valueNow}) =");
                        valueNow = Math.Atan(valueNow);
                    }
                    OutputFieldBlock.Text = valueNow.ToString();
                }
                else
                {
                    if (trig_checker == "DEG")
                    {
                        OldFieldBlock.Text += ($"Arctan({valueNow}) ");
                        valueNow = Math.Atan(valueNow) * (180 / Math.PI);
                    }
                    else if (trig_checker == "RAD")
                    {
                        OldFieldBlock.Text += ($"Arctan({valueNow}) ");
                        valueNow = Math.Atan(valueNow);
                    }
                    OldFieldBlock.Text += ($"({valueNow}) =");
                    DoOperate();
                }
            }
        }

        private void ModDivisionBut_Click(object sender, RoutedEventArgs e)
        {
            if (!isOperationsBlocked)
            {
                OldFieldBlock.Text = valueNow.ToString() + " Mod";
                operationMemoried = new ModDivision();
                valueMemoried = valueNow;

                isOperationGo = true;
            }
        }
    }
}

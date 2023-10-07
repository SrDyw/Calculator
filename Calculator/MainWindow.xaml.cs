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
using System.Linq;
using System.Data;
using System.Data.OleDb;


namespace Calculator
{
    enum Operation
    {
        ADD = '+',
        SUBTRACT = '-',
        MULTIPLY = '*',
        DIVIDE = '/',
        UNDEFINDED = '|'
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string SYNTAX_ERROR = "Syntax Error";

        public MainWindow()
        {
            InitializeComponent();
            number_shown_txt.Focus();
            CursorToEnd();
        }

        void CursorToEnd()
        {
            number_shown_txt.SelectionStart = number_shown_txt.Text.Length;
        }

        void AddOperation(Operation operation)
        {
            switch(operation)
            {
                case Operation.ADD:
                    if (CurrentCaracter() != '+')
                        AddOperatorCharacter('+');
                    break;
                case Operation.SUBTRACT:
                    if (CurrentCaracter() != '-')
                        AddOperatorCharacter('-');
                    break;
                case Operation.MULTIPLY:
                    if (CurrentCaracter() != '*')
                        AddOperatorCharacter('*');
                    break;
                case Operation.DIVIDE:
                    if (CurrentCaracter() != '/')
                        AddOperatorCharacter('/');
                    break;
            }
        }

        char CurrentCaracter()
        {
            return number_shown_txt.Text.Length > 0 ? number_shown_txt.Text[number_shown_txt.Text.Length - 1] : ' ';
        }
        void AddOperatorCharacter(char value)
        {
            AddValueToOperation(value);
        }

        void RemoveElementLast()
        {
            if (number_shown_txt.Text.Length > 0)
            {
                ChangeTextbox(number_shown_txt.Text.Remove(number_shown_txt.Text.Length - 1));
            }
        }

        void AddValueToOperation(object value)
        {
            var number = 0f;
            var canInput = float.TryParse(value.ToString(), out number) || value.ToString() == ".";

            if (canInput)
            {
                ChangeTextbox((number_shown_txt.Text != "0" || value.ToString() == "." ? number_shown_txt.Text : "") + value.ToString());
            }
        }


        private void btn_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void bnt_number_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(1);
        }

        private void bnt_number_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(2);
        }

        private void bnt_number_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(3);
        }

        private void bnt_number_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(4);
        }

        private void bnt_number_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(5);
        }

        private void bnt_number_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(6);
        }

        private void bnt_number_7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(7);

        }

        private void bnt_number_8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(8);

        }

        private void bnt_number_9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(9);

        }

        private void bnt_number_dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation('.');
        }

        private void bnt_number_0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddValueToOperation(0);

        }

        private void bnt_number_sign_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void bnt_number_del_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RemoveElementLast();
        }

        private void bnt_number_clean_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeTextbox("");
        }

        private void bnt_number_mul_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation(Operation.MULTIPLY);
        }

        private void bnt_number_div_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation(Operation.DIVIDE);
        }

        private void btn_operation_sub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation(Operation.SUBTRACT);

        }

        private void btn_operation_add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation(Operation.ADD);
        } 

        void ChangeTextbox(string expresion)
        {
            number_shown_txt.Text = expresion;
            CursorToEnd();
        }

        enum OperationOrder { Normal, FirstOrder, SecondOrder }
        private void btn_operation_res_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var text = number_shown_txt.Text;
            InterpreteMathExpresion(text);
        }

        void InterpreteMathExpresion(string expresion = "")
        {
            string formula = expresion;

            // Crear una tabla de datos para evaluar la expresión
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("expression", typeof(string), formula);

                // Calcular el resultado de la expresión
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                float resultado = float.Parse((string)row["expression"]);
                ChangeTextbox(resultado.ToString());
                operation_historial_shown_txt.Text = formula;
            } 
            catch (SyntaxErrorException ex)
            {
                ChangeTextbox(SYNTAX_ERROR);
            }
        }

        void Debug(object value)
        {
            MessageBox.Show(value.ToString());
        }

        void ShowListValues<T>(T[] values)
        {
            values.ToList().ForEach(v => MessageBox.Show(v.ToString()));
            
        }
        bool IsNumber(object value)
        {
            return float.TryParse(value.ToString(), out _);
        }

        private void number_shown_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumber(e.Text) && !IsBasicOperation(e.Text) && e.Text != ".")
            {
                e.Handled = true;
            } 
            else if (number_shown_txt.Text == "0" && e.Text != ".")
            {
                ChangeTextbox("");
            }

            if (e.Text == "=")
            {
                InterpreteMathExpresion();
            }
        }

        private bool IsBasicOperation(object operation)
        {
            return "+-*/".Contains(operation.ToString());
        }

        private void handlerBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void number_shown_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (number_shown_txt.Text == SYNTAX_ERROR)
            {
                ChangeTextbox("");
            }
            else
            {
                if (e.Key == Key.Enter)
                {
                    InterpreteMathExpresion(number_shown_txt.Text);
                }
                else if (e.Key == Key.Escape)
                {
                    Close();
                }
            }
        }

        private void operation_historial_shown_txt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeTextbox(operation_historial_shown_txt.Text);
        }

        private void number_shown_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (number_shown_txt.Text == "")
              //  ChangeTextbox("0");
        }
    }

    
}

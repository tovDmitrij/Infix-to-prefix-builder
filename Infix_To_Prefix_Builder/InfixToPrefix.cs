using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace Infix_To_Prefix_Builder
{
    /// <summary>
    /// Преобразование инфиксной нотации в префиксную
    /// </summary>
    internal class InfixToPrefix
    {
        private readonly string infixNotation;

        public InfixToPrefix(string infixNotation)
        {
            this.infixNotation = infixNotation;
        }

        /// <summary>
        /// Преобразование инфиксной нотации в префиксную
        /// </summary>
        public string Run()
        {
            switch (CheckInfix(infixNotation))
            {
                case true:
                    break;
                case false:
                    MessageBox.Show("Инфиксная запись введена неправильно!");
                    return "";
            }
            try
            {
                Stack<char> operators = new Stack<char>();
                Stack<string> operands = new Stack<string>();
                for (int i = 0; i < infixNotation.Length; i++)
                {
                    switch (infixNotation[i])
                    {
                        case '(':
                            operators.Push(infixNotation[i]);
                            break;
                        case ')':
                            while (operators.Count != 0 && operators.Peek() != '(')
                            {
                                string operand1 = operands.Peek();
                                operands.Pop();
                                char @operator = operators.Peek();
                                operators.Pop();
                                if (@operator == '¬')
                                {
                                    string tmp = @operator + operand1;
                                    operands.Push(tmp);
                                }
                                else
                                {
                                    string operand2 = operands.Peek();
                                    operands.Pop();
                                    string tmp = @operator + operand2 + operand1;
                                    operands.Push(tmp);
                                }
                            }
                            operators.Pop();
                            break;
                        default:
                            switch (char.IsLetter(infixNotation[i]))
                            {
                                case true:
                                    operands.Push(infixNotation[i].ToString());
                                    break;
                                case false:
                                    while (operators.Count != 0 && Priority(infixNotation[i]) < Priority(operators.Peek()))
                                    {
                                        string operand1 = operands.Peek();
                                        operands.Pop();
                                        try
                                        {
                                            string operand2 = operands.Peek();
                                            operands.Pop();
                                            char @operator = operators.Peek();
                                            operators.Pop();
                                            string tmp = @operator + operand2 + operand1;
                                            operands.Push(tmp);
                                        }
                                        catch
                                        {
                                            char @operator = operators.Peek();
                                            operators.Pop();
                                            string tmp = @operator + operand1;
                                            operands.Push(tmp);
                                        }
                                    }
                                    operators.Push(infixNotation[i]);
                                    break;
                            }
                            break;
                    }
                }
                while (operators.Count != 0)
                {
                    string operand1 = operands.Peek();
                    operands.Pop();
                    char @operator = operators.Peek();
                    operators.Pop();
                    if (@operator == '¬')
                    {
                        string tmp = @operator + operand1;
                        operands.Push(tmp);
                    }
                    else
                    {
                        string operand2 = operands.Peek();
                        operands.Pop();
                        string tmp = @operator + operand2 + operand1;
                        operands.Push(tmp);
                    }
                }
                return operands.Peek();
            }
            catch
            {
                MessageBox.Show("Некорректно введены данные!");
                return "";
            }
        }

        /// <summary>
        /// Приоритет операции
        /// </summary>
        /// <param name="operation">Операция</param>
        private static int Priority(char operation)
        {
            switch (operation)
            {
                case '¬':
                    return 3;
                case '⋀':
                    return 2;
                case '⋁':
                    return 1;
                case '⊕':
                    return 1;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Проверка на правильность написания инфиксной нотации
        /// </summary>
        /// <param name="infixNotation">Инфиксная запись</param>
        public static bool CheckInfix(string infixNotation)
        {
            char[] operators = { '(', ')', '⋁', '⋀', '→', '↔', '¬', '⊕' };
            try
            {
                if (operators.Contains(infixNotation[^1]) && (infixNotation[^1] != ')') || operators.Contains(infixNotation[0]) && (infixNotation[0] != '¬') && (infixNotation[0] != '('))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            Stack<char> stack = new Stack<char>();
            foreach (var symbol in infixNotation)
            {
                switch (symbol)
                {
                    case '(':
                        stack.Push('(');
                        break;
                    case ')':
                        if (stack.Count == 0)
                        {
                            return false;
                        }
                        else
                        {
                            stack.Pop();
                        }
                        break;
                    default:
                        break;
                }
            }
            if (stack.Count != 0)
            {
                return false;
            }
            for (int i = 0; i < infixNotation.Length - 1; i++)
            {
                char currentSymbol = infixNotation[i];
                char nextSymbol = infixNotation[i + 1];
                if (char.IsLetter(currentSymbol) && (!(operators.Contains(nextSymbol)) || (nextSymbol == '(') || (nextSymbol == '¬')))
                {
                    return false;
                }
                if (operators.Contains(currentSymbol) && (operators.Contains(nextSymbol)) && (nextSymbol != '(') && (nextSymbol != '¬') && (currentSymbol != ')'))
                {
                    return false;
                }
                if ((currentSymbol == ')') && ((char.IsLetter(nextSymbol)) || (nextSymbol == '¬')))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
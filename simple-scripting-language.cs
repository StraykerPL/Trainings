using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SimpleScriptLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: SimpleScript <filename>");
                return;
            }

            string script = File.ReadAllText(args[0]);
            var interpreter = new Interpreter();
            interpreter.Execute(script);
        }
    }

    class Interpreter
    {
        private Dictionary<string, int> variables = new Dictionary<string, int>();
        private Dictionary<string, List<string>> functions = new Dictionary<string, List<string>>();
        private Stack<int> loopStack = new Stack<int>();
        private List<string> lines;
        private int currentLine = 0;

        public void Execute(string script)
        {
            // Split the script into lines and clean them
            lines = new List<string>();
            foreach (var line in script.Split('\n'))
            {
                string trimmedLine = line.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedLine) && !trimmedLine.StartsWith("#"))
                {
                    lines.Add(trimmedLine);
                }
            }

            // First pass: identify functions
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("FUNC "))
                {
                    string funcName = lines[i].Substring(5).Trim();
                    List<string> funcLines = new List<string>();
                    i++;
                    
                    while (i < lines.Count && lines[i] != "ENDFUNC")
                    {
                        funcLines.Add(lines[i]);
                        i++;
                    }
                    
                    functions[funcName] = funcLines;
                }
            }

            // Execute the script
            while (currentLine < lines.Count)
            {
                string line = lines[currentLine];
                ExecuteLine(line);
                currentLine++;
            }
        }

        private void ExecuteLine(string line)
        {
            // Check for comments and empty lines
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                return;

            // Handle variable assignment
            if (line.Contains("="))
            {
                string[] parts = line.Split('=');
                string varName = parts[0].Trim();
                string expression = parts[1].Trim();
                variables[varName] = EvaluateExpression(expression);
                return;
            }

            // Handle print statement
            if (line.StartsWith("PRINT "))
            {
                string value = line.Substring(6).Trim();
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    Console.WriteLine(value.Substring(1, value.Length - 2));
                }
                else
                {
                    Console.WriteLine(EvaluateExpression(value));
                }
                return;
            }

            // Handle input statement
            if (line.StartsWith("INPUT "))
            {
                string varName = line.Substring(6).Trim();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    variables[varName] = result;
                }
                else
                {
                    Console.WriteLine("Error: Input must be a number");
                }
                return;
            }

            // Handle if statement
            if (line.StartsWith("IF "))
            {
                string condition = line.Substring(3).Trim();
                bool result = EvaluateCondition(condition);
                if (!result)
                {
                    // Skip to matching ENDIF or ELSE
                    int nestedIfs = 0;
                    while (currentLine < lines.Count - 1)
                    {
                        currentLine++;
                        if (lines[currentLine].StartsWith("IF "))
                            nestedIfs++;
                        else if (lines[currentLine] == "ENDIF")
                        {
                            if (nestedIfs == 0)
                                break;
                            nestedIfs--;
                        }
                        else if (lines[currentLine] == "ELSE" && nestedIfs == 0)
                            break;
                    }
                }
                return;
            }

            // Handle else statement
            if (line == "ELSE")
            {
                // Skip to matching ENDIF
                int nestedIfs = 0;
                while (currentLine < lines.Count - 1)
                {
                    currentLine++;
                    if (lines[currentLine].StartsWith("IF "))
                        nestedIfs++;
                    else if (lines[currentLine] == "ENDIF")
                    {
                        if (nestedIfs == 0)
                            break;
                        nestedIfs--;
                    }
                }
                return;
            }

            // Handle while loop
            if (line.StartsWith("WHILE "))
            {
                string condition = line.Substring(6).Trim();
                bool result = EvaluateCondition(condition);
                if (result)
                {
                    loopStack.Push(currentLine);
                }
                else
                {
                    // Skip to matching ENDWHILE
                    int nestedWhiles = 0;
                    while (currentLine < lines.Count - 1)
                    {
                        currentLine++;
                        if (lines[currentLine].StartsWith("WHILE "))
                            nestedWhiles++;
                        else if (lines[currentLine] == "ENDWHILE")
                        {
                            if (nestedWhiles == 0)
                                break;
                            nestedWhiles--;
                        }
                    }
                }
                return;
            }

            // Handle end of while loop
            if (line == "ENDWHILE")
            {
                if (loopStack.Count > 0)
                {
                    int whileStart = loopStack.Peek();
                    string whileLine = lines[whileStart];
                    string condition = whileLine.Substring(6).Trim();
                    bool result = EvaluateCondition(condition);
                    if (result)
                    {
                        currentLine = whileStart;
                    }
                    else
                    {
                        loopStack.Pop();
                    }
                }
                return;
            }

            // Handle function call
            if (line.StartsWith("CALL "))
            {
                string funcName = line.Substring(5).Trim();
                if (functions.ContainsKey(funcName))
                {
                    // Save current position
                    int returnLine = currentLine;
                    
                    // Execute function
                    foreach (string funcLine in functions[funcName])
                    {
                        ExecuteLine(funcLine);
                    }
                    
                    // Return to caller
                    currentLine = returnLine;
                }
                else
                {
                    Console.WriteLine($"Error: Function {funcName} not found");
                }
                return;
            }
        }

        private int EvaluateExpression(string expression)
        {
            // Check if it's a variable
            if (variables.ContainsKey(expression))
                return variables[expression];

            // Check if it's a number
            if (int.TryParse(expression, out int result))
                return result;

            // Handle addition
            if (expression.Contains("+"))
            {
                string[] parts = expression.Split('+');
                return EvaluateExpression(parts[0].Trim()) + EvaluateExpression(parts[1].Trim());
            }

            // Handle subtraction
            if (expression.Contains("-"))
            {
                string[] parts = expression.Split('-');
                return EvaluateExpression(parts[0].Trim()) - EvaluateExpression(parts[1].Trim());
            }

            // Handle multiplication
            if (expression.Contains("*"))
            {
                string[] parts = expression.Split('*');
                return EvaluateExpression(parts[0].Trim()) * EvaluateExpression(parts[1].Trim());
            }

            // Handle division
            if (expression.Contains("/"))
            {
                string[] parts = expression.Split('/');
                int divisor = EvaluateExpression(parts[1].Trim());
                if (divisor == 0)
                {
                    Console.WriteLine("Error: Division by zero");
                    return 0;
                }
                return EvaluateExpression(parts[0].Trim()) / divisor;
            }

            // If we get here, the expression is not valid
            Console.WriteLine($"Error: Invalid expression: {expression}");
            return 0;
        }

        private bool EvaluateCondition(string condition)
        {
            // Handle equality
            if (condition.Contains("=="))
            {
                string[] parts = condition.Split(new[] { "==" }, StringSplitOptions.None);
                return EvaluateExpression(parts[0].Trim()) == EvaluateExpression(parts[1].Trim());
            }

            // Handle inequality
            if (condition.Contains("!="))
            {
                string[] parts = condition.Split(new[] { "!=" }, StringSplitOptions.None);
                return EvaluateExpression(parts[0].Trim()) != EvaluateExpression(parts[1].Trim());
            }

            // Handle greater than
            if (condition.Contains(">"))
            {
                string[] parts = condition.Split('>');
                return EvaluateExpression(parts[0].Trim()) > EvaluateExpression(parts[1].Trim());
            }

            // Handle less than
            if (condition.Contains("<"))
            {
                string[] parts = condition.Split('<');
                return EvaluateExpression(parts[0].Trim()) < EvaluateExpression(parts[1].Trim());
            }

            // Handle greater than or equal
            if (condition.Contains(">="))
            {
                string[] parts = condition.Split(new[] { ">=" }, StringSplitOptions.None);
                return EvaluateExpression(parts[0].Trim()) >= EvaluateExpression(parts[1].Trim());
            }

            // Handle less than or equal
            if (condition.Contains("<="))
            {
                string[] parts = condition.Split(new[] { "<=" }, StringSplitOptions.None);
                return EvaluateExpression(parts[0].Trim()) <= EvaluateExpression(parts[1].Trim());
            }

            // If we get here, the condition is not valid
            Console.WriteLine($"Error: Invalid condition: {condition}");
            return false;
        }
    }
}

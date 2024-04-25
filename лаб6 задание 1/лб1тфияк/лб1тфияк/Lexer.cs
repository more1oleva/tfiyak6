using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace лб1тфияк
{
    public class Lexer
    {
        
        private readonly Dictionary<string, int> keywords = new Dictionary<string, int>
    {
        {"public", 1}, 
        {"private", 2},
        {"protected", 3},
        {"struct", 4},
        {"int", 5},
        {"bool", 6},
        {"char", 7},
        {"string", 8},
        {"true", 9},
        {"false", 10},
    };
        private readonly Dictionary<string, int> operators = new Dictionary<string, int>
    {
        {"{", 13}, 
        {"}", 14},
        {" ", 11},
        {";", 15},

    };
        
        private readonly Regex numberRegex = new Regex(@"^\d+$");

        public List<Token> Tokenize(string input)
        {
            var tokens = new List<Token>();
            var lines = input.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                int lineNumber = i + 1;
                int startPos = 1;

                foreach (Match match in Regex.Matches(line, @"\s|\b\d+\b|\b\w+\b|\S"))
                {
                    string value = match.Value;
                    int type;
                   /*if (numberRegex.IsMatch(value))
                    {
                        type = 1; // Целое число
                    }*/
                     if (keywords.ContainsKey(value))
                    {
                        type = keywords[value];
                    }
                    else if (operators.ContainsKey(value))
                    {
                        type = operators[value];
                    }
                  
                    else if (IsIdentifier(value))
                    {
                        type = 12; // Идентификатор
                    }
                    else
                    {
                        type = -1; // Недопустимый символ
                    }

                    tokens.Add(new Token(type, value, lineNumber, startPos));
                    startPos += value.Length;
                }
            }

            return tokens;
        }
        private bool IsIdentifier(string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z_][a-zA-Z\d_]*$");
        }
        
    }
}


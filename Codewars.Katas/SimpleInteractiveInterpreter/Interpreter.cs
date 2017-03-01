using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InterpreterKata
{
    public class Interpreter
    {
        public double? input(string input)
        {
            List<string> tokens = tokenize(input);
            return null;
        }

        private List<string> tokenize(string input)
        {
            input = input + ")";
            List<string> tokens = new List<string>();
            Regex rgxMain = new Regex("=>|[-+*/%=\\(\\)]|[A-Za-z_][A-Za-z0-9_]*|[0-9]*(\\.?[0-9]+)");
            MatchCollection matches = rgxMain.Matches(input);
            foreach (Match m in matches) tokens.Add(m.Groups[0].Value);
            return tokens;
        }
    }
}
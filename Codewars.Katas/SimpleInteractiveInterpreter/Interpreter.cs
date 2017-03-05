using Codewars.Katas.SimpleInteractiveInterpreter;
using System;

namespace InterpreterKata
{
    public class Interpreter
    {
        private Evaluator _evaluator;
        private Lexer _lexer;
        private Parser _parser;

        public Interpreter()
        {
            _lexer = new Lexer();

            var symbolTable = new SymbolTable();

            _parser = new Parser(_lexer, symbolTable);

            _evaluator = new Evaluator(symbolTable);
        }

        public double? input(string input)
        {
            _lexer.SetText(input);

            var node = _parser.Parse();

            return _evaluator.Evaluate(node);
        }
    }
}
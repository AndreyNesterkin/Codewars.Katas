using System;
using System.Globalization;

namespace Codewars.Katas.SimpleInteractiveInterpreter
{
    public class Lexer : ILexer
    {
        private const char NumberDecimalSeparator = '.';
        private string FunctionDefinitionKeyWord = "fn";
        private string _text;
        private int? _pos;
        private char? _currentChar;

        public void SetText(string text)
        {
            _text = text;
            _pos = GetStartPosition(_text);
            _currentChar = GetStartChar(_text, _pos);
        }

        private static int? GetStartPosition(string text)
        {
            return string.IsNullOrEmpty(text) ? new int?() : new int?(0);
        }

        private static char? GetStartChar(string text, int? pos)
        {
            return pos == null ? new char?() : new char?(text[pos.Value]);
        }

        public Token ReadNextToken()
        {
            while (!IsEof(_currentChar))
            {
                if (IsSpace(_currentChar))
                {
                    IgnoreSpaces();
                    continue;
                }
               
                return ReadToken();
            }

            return new Token(TokenType.Eof);
        }

        private Token ReadToken()
        {
            if (_currentChar.Value == '%')
                return ReadDivisionRemainderToken();

            if (_currentChar.Value == '/')
                return ReadDivisionToken();

            if (_currentChar.Value == '*')
                return ReadMultiplicationToken();

            if (_currentChar.Value == '+')
                return ReadPlusToken();

            if (_currentChar.Value == '-')
                return ReadMinusToken();

            if (_currentChar.Value == '(')
                return ReadLeftParenthesisToken();

            if (_currentChar.Value == ')')
                return ReadRightParenthesisToken();

            if (IsDigit(_currentChar))
                return ReadDoubleConstToken();

            if (IsFirstIdentifierChar(_currentChar))
            {
                var token = ReadIdentifierToken();

                if (IsFuncitionHeader(token))
                    return new Token(TokenType.FunctionHeader, token.Value);

                return token;
            }

            if (_currentChar.Value == '=')
            {
                if (IsFunctionOperator(_currentChar))
                    return ReadFunctionOperatorToken();

                return ReadAssignmentToken();
            }

            throw new InvalidOperationException("Invalid character");
        }

        private bool IsFirstIdentifierChar(char? currentChar)
        {
            return IsLetter(currentChar) || IsUnderscore(currentChar);
        }

        private bool IsFuncitionHeader(Token token)
        {
            return string.Equals((string)token.Value, FunctionDefinitionKeyWord, StringComparison.CurrentCultureIgnoreCase);
        }

        private bool IsSpace(char? currentChar)
        {
            return !IsEof(currentChar) && currentChar.Value == ' ';
        }

        private void IgnoreSpaces()
        {
            while (IsSpace(_currentChar))
                MoveNext();
        }

        private Token ReadLeftParenthesisToken()
        {
            return ReadSingleCharToken(TokenType.LeftParenthesis);
        }

        private Token ReadRightParenthesisToken()
        {
            return ReadSingleCharToken(TokenType.RightParenthesis);
        }

        private Token ReadDivisionRemainderToken()
        {
            return ReadSingleCharToken(TokenType.DivisionRemainder);
        }

        private Token ReadDivisionToken()
        {
            return ReadSingleCharToken(TokenType.Division);
        }

        private Token ReadMultiplicationToken()
        {
            return ReadSingleCharToken(TokenType.Multiplication);
        }

        private Token ReadSingleCharToken(TokenType type)
        {
            var token = new Token(type, _currentChar.Value.ToString());
            MoveNext();

            return token;
        }

        private Token ReadPlusToken()
        {
            return ReadSingleCharToken(TokenType.Plus);
        }

        private Token ReadMinusToken()
        {
            return ReadSingleCharToken(TokenType.Minus);
        }

        private void ThrowException()
        {
            throw new InvalidOperationException("Invalid character");
        }

        private static bool IsEof(char? currentChar)
        {
            return currentChar == null;
        }

        private static bool IsDigit(char? currentChar)
        {
            return !IsEof(currentChar) && char.IsDigit(currentChar.Value);
        }

        private Token ReadDoubleConstToken()
        {
            var doubleConst = ReadDigits();

            if (IsNumberDecimalSeparator(_currentChar))
                doubleConst += ReadNumberDecimalSeparator() + ReadDigits();
 
            return new Token(TokenType.DoubleConst, Convert.ToDouble(doubleConst, CultureInfo.InvariantCulture));
        }

        private string ReadDigits()
        {
            var digits = string.Empty;

            while (IsDigit(_currentChar))
            {
                digits += _currentChar.Value;
                MoveNext();
            }

            return digits;
        }

        private char ReadNumberDecimalSeparator()
        {
            MoveNext();
            return NumberDecimalSeparator;
        }

        private static bool IsNumberDecimalSeparator(char? currentChar)
        {
            return !IsEof(currentChar) && currentChar.Value == NumberDecimalSeparator;
        }

        private void MoveNext()
        {
            _pos++;

            if (_pos < _text.Length)
                _currentChar = _text[_pos.Value];
            else
                _currentChar = null;
        }

        private bool IsLetter(char? currentChar)
        {
            return !IsEof(currentChar) && char.IsLetter(currentChar.Value);
        }

        private bool IsUnderscore(char? currentChar)
        {
            return !IsEof(currentChar) && currentChar.Value == '_';
        }

        private Token ReadIdentifierToken()
        {
            return new Token(TokenType.Identifier, ReadIdentifier());
        }

        private string ReadIdentifier()
        {
            var identifier = string.Empty;

            while (IsLetter(_currentChar) || IsDigit(_currentChar) || IsUnderscore(_currentChar))
            {
                identifier += _currentChar.Value;
                MoveNext();
            }

            return identifier;
        }

        private Token ReadAssignmentToken()
        {
            return ReadSingleCharToken(TokenType.Assignment);
        }

        private bool IsFunctionOperator(char? currentChar)
        {
            var nextChar = LookAhead();

            return !IsEof(nextChar) && currentChar.Value == '=' &&  nextChar.Value == '>';
        }

        private char? LookAhead()
        {
            var lookAheadPos = _pos + 1;

            if (lookAheadPos < _text.Length)
               return _text[lookAheadPos.Value];
            else
                return null;
        }

        private Token ReadFunctionOperatorToken()
        {
            MoveNext();
            MoveNext();
            return new Token(TokenType.FunctionOperator, "=>");
        }
    }
}

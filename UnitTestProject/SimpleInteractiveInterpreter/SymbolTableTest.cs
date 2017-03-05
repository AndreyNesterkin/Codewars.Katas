using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codewars.Katas.SimpleInteractiveInterpreter;
using Codewars.Katas.SimpleInteractiveInterpreter.Ast;

namespace UnitTestProject.SimpleInteractiveInterpreter
{
    [TestClass]
    public class SymbolTableTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SystemIdentifierDefineShouldThrowException()
        {
            var table = new SymbolTable();

            var token = new Token(TokenType.Identifier);

            table.DefineVariable("fn", new IdentifierAstNode(token, "fn"));
        }

        [TestMethod]
        public void IdentifierDefineShouldDefineIdentifier()
        {
            var table = new SymbolTable();

            var token = new Token(TokenType.Identifier);

            var node = new IdentifierAstNode(token, "a");

            table.DefineVariable("a", node);

            Assert.AreSame(node, table.Lookup("a"));
        }

        [TestMethod]
        public void FunctionDefineShouldDefineFunction()
        {
            var table = new SymbolTable();

            var token = new Token(TokenType.Identifier, "echo");

            var node = new FunctionDefinitionAstNode(token, new AstNode[0], new IdentifierAstNode(new Token(TokenType.Identifier), "a"));

            table.DefineFunction("echo", node);

            Assert.AreSame(node, table.Lookup("echo"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FunctionDefinedAfterVariableDefineShouldThrowException()
        {
            var table = new SymbolTable();

            var token = new Token(TokenType.Identifier);
            var variable = new IdentifierAstNode(token, "a");
            table.DefineVariable("a", variable);

            token = new Token(TokenType.Identifier, "a");
            var function = new FunctionDefinitionAstNode(token, new AstNode[0], new IdentifierAstNode(new Token(TokenType.Identifier), "a"));
            table.DefineFunction("a", function);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VariableDefinedAfterFunctionDefineShouldThrowException()
        {
            var table = new SymbolTable();
            
            var token = new Token(TokenType.Identifier, "a");
            var function = new FunctionDefinitionAstNode(token, new AstNode[0], new IdentifierAstNode(new Token(TokenType.Identifier), "a"));
            table.DefineFunction("a", function);

            token = new Token(TokenType.Identifier);
            var variable = new IdentifierAstNode(token, "a");
            table.DefineVariable("a", variable);
        }

        [TestMethod]
        public void RedefineFunctionDefineShouldDefine()
        {
            var table = new SymbolTable();

            var token = new Token(TokenType.Identifier, "a");
            var function = new FunctionDefinitionAstNode(token, new AstNode[0], new IdentifierAstNode(new Token(TokenType.Identifier), "b"));
            table.DefineFunction("a", function);

            function = new FunctionDefinitionAstNode(token, new AstNode[0], new IdentifierAstNode(new Token(TokenType.Identifier), "b"));
            table.DefineFunction("a", function);
            Assert.AreSame(function, table.Lookup("a"));
        }
    }
}

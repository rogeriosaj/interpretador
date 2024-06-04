using System;

namespace compi
{
    // Classe responsável por analisar e interpretar tokens gerados pelo Lexer
    public class Parser
    {
        private Lexer _lexer; // Instância do Lexer para gerar tokens
        private Token _currentToken; // Token atual sendo processado
        private SymbolTable _symbolTable; // Tabela de símbolos para armazenar variáveis e seus valores

        // Construtor da classe Parser
        public Parser(Lexer lexer, SymbolTable symbolTable)
        {
            _lexer = lexer; // Inicializa o Lexer
            _symbolTable = symbolTable; // Inicializa a tabela de símbolos
            _currentToken = _lexer.NextToken(); // Pega o primeiro token do Lexer
        }

        // Método para consumir o token atual e avançar para o próximo token
        private void Match(TokenType type)
        {
            // Verifica se o tipo do token atual é o esperado
            if (_currentToken.Type == type)
            {
                // Avança para o próximo token
                _currentToken = _lexer.NextToken();
            }
            else
            {
                // Lança uma exceção se o token atual não for o esperado
                throw new Exception($"Unexpected token: {_currentToken.Type}, expected: {type}");
            }
        }

        // Método principal para iniciar o parsing dos tokens
        public void Parse()
        {
            // Continua o parsing até encontrar o token EOF
            while (_currentToken.Type != TokenType.EOF)
            {
                // Verifica se o token atual é uma atribuição ou uma expressão
                if (_currentToken.Type == TokenType.VAR)
                {
                    var varName = _currentToken.Value; // Armazena o nome da variável
                    Match(TokenType.VAR); // Consome o token VAR
                    Match(TokenType.EQ); // Consome o token de atribuição '='
                    var value = Expr(); // Avalia a expressão seguinte
                    _symbolTable.SetVariable(varName, value); // Armazena o valor da variável na tabela de símbolos
                }
                else if (_currentToken.Type == TokenType.PRINT)
                {
                    Match(TokenType.PRINT); // Consome o token PRINT
                    var value = Expr(); // Avalia a expressão seguinte
                    Console.WriteLine(value); // Imprime o valor da expressão
                }
                else
                {
                    // Lança uma exceção se o token não for reconhecido
                    throw new Exception($"Unexpected token: {_currentToken.Type}");
                }
            }
            Match(TokenType.EOF); // Avança para o próximo token (EOF)
        }

        // Método para avaliar uma expressão
        private int Expr()
        {
            var result = Term(); // Avalia o termo inicial

            // Continua avaliando enquanto houver operadores 
            while (_currentToken.Type == TokenType.SUM || _currentToken.Type == TokenType.SUB)
            {
                var token = _currentToken;
                if (token.Type == TokenType.SUM)
                {
                    Match(TokenType.SUM); // Consome o token '+'
                    result += Term(); // Avalia o próximo termo e soma ao resultado
                }
                else if (token.Type == TokenType.SUB)
                {
                    Match(TokenType.SUB); // Consome o token '-'
                    result -= Term(); // Avalia o próximo termo e subtrai do resultado
                }
            }

            return result; // Retorna o valor da expressão
        }

        // Método para avaliar um termo
        private int Term()
        {
            var result = Factor(); // Avalia o fator inicial

            // Continua avaliando enquanto houver operadores 
            while (_currentToken.Type == TokenType.MUL || _currentToken.Type == TokenType.DIV)
            {
                var token = _currentToken;
                if (token.Type == TokenType.MUL)
                {
                    Match(TokenType.MUL); // Consome o token '*'
                    result *= Factor(); // Avalia o próximo fator e multiplica ao resultado
                }
                else if (token.Type == TokenType.DIV)
                {
                    Match(TokenType.DIV); // Consome o token '/'
                    result /= Factor(); // Avalia o próximo fator e divide do resultado
                }
            }

            return result; // Retorna o valor do termo
        }

        // Método para avaliar um fator
        private int Factor()
        {
            var token = _currentToken;

            // Verifica se o token é um número
            if (token.Type == TokenType.ENUM)
            {
                Match(TokenType.ENUM); // Consome o token do número
                return int.Parse(token.Value); // Retorna o valor do número
            }
            // Verifica se o token é uma variável
            else if (token.Type == TokenType.VAR)
            {
                var varName = token.Value;
                Match(TokenType.VAR); // Consome o token da variável
                return _symbolTable.GetVariable(varName); // Retorna o valor da variável
            }
            // Verifica se o token é uma expressão entre parênteses
            else if (token.Type == TokenType.OPEN)
            {
                Match(TokenType.OPEN); // Consome o token '('
                var result = Expr(); // Avalia a expressão dentro dos parênteses
                Match(TokenType.CLOSE); // Consome o token ')'
                return result; // Retorna o valor da expressão
            }
            else
            {
                // Lança uma exceção se o token não for reconhecido
                throw new Exception($"Unexpected token: {token.Type}");
            }
        }
    }
}

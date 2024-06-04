using System;

namespace compi
{
    // Enumeração dos tipos de tokens que podem ser gerados pelo Lexer
    public enum TokenType
    {
        VAR,    // Variável
        ENUM,   // Número inteiro
        EQ,     // Sinal de igual '='
        SUM,    // Sinal de adição '+'
        SUB,    // Sinal de subtração '-'
        MUL,    // Sinal de multiplicação '*'
        DIV,    // Sinal de divisão '/'
        OPEN,   // Parêntese de abertura '('
        CLOSE,  // Parêntese de fechamento ')'
        PRINT,  // Comando PRINT
        EOF,    // Fim do arquivo
        UNK     // Token desconhecido/erro
    }

    // Estrutura para representar um token
    public struct Token
    {
        public TokenType Type { get; set; } // Tipo do token
        public string Value { get; set; }   // Valor do token

        // Construtor para inicializar um token com tipo e valor
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        // Método para converter o token em string para fins de depuração
        public override string ToString() => $"{Type}({Value})";
    }

    // Classe Lexer responsável por transformar a entrada de texto em tokens
    public class Lexer
    {
        public string Input { get; set; } // Entrada de texto a ser analisada
        private int _peek; // Posição atual do cursor na entrada de texto

        // Construtor que inicializa a entrada de texto e a posição do cursor
        public Lexer(string input)
        {
            Input = input;
            _peek = 0;
        }

        // Método para avançar um caractere na entrada de texto
        private char Scan()
        {
            if (_peek >= Input.Length)
            {
                return '\0'; // Retorna o caractere nulo se alcançar o final da entrada
            }
            return Input[_peek++]; // Retorna o próximo caractere e avança o cursor
        }

        // Método para gerar o próximo token da entrada de texto
        public Token NextToken()
        {
            char c = Scan();
            if (c == '\0')
            {
                return new Token { Type = TokenType.EOF, Value = "null" }; // Retorna token EOF se alcançar o final da entrada
            }

            // Ignora espaços em branco
            while (char.IsWhiteSpace(c))
            {
                c = Scan();
                if (c == '\0')
                {
                    return new Token { Type = TokenType.EOF, Value = "null" }; // Retorna token EOF se alcançar o final da entrada
                }
            }

            // Gera tokens para caracteres específicos
            switch (c)
            {
                case '+': return new Token { Type = TokenType.SUM, Value = c.ToString() };
                case '-': return new Token { Type = TokenType.SUB, Value = c.ToString() };
                case '=': return new Token { Type = TokenType.EQ, Value = c.ToString() };
                case '*': return new Token { Type = TokenType.MUL, Value = c.ToString() };
                case '/': return new Token { Type = TokenType.DIV, Value = c.ToString() };
                case '(': return new Token { Type = TokenType.OPEN, Value = c.ToString() };
                case ')': return new Token { Type = TokenType.CLOSE, Value = c.ToString() };
            }

            // Gera tokens para palavras reservadas e variáveis
            if (char.IsLetter(c))
            {
                var value = string.Empty;
                // Constrói a string da palavra
                while (char.IsLetter(c))
                {
                    value += c;
                    c = Scan();
                }
                _peek--; // Retrocede o cursor um caractere
                if (value == "PRINT")
                {
                    return new Token { Type = TokenType.PRINT, Value = value }; // Retorna token PRINT se a palavra for "PRINT"
                }
                return new Token { Type = TokenType.VAR, Value = value }; // Retorna token VAR para qualquer outra palavra
            }

            // Gera tokens para números
            if (char.IsDigit(c))
            {
                var value = string.Empty;
                // Constrói a string do número
                while (char.IsDigit(c))
                {
                    value += c;
                    c = Scan();
                }
                _peek--; // Retrocede o cursor um caractere
                return new Token { Type = TokenType.ENUM, Value = value }; // Retorna token ENUM
            }

            // Retorna token desconhecido para qualquer outro caractere
            return new Token { Type = TokenType.UNK, Value = c.ToString() };
        }
    }
}

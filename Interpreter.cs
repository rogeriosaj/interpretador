using System;

namespace compi
{
    // Classe responsável por interpretar e executar comandos
    public class Interpreter
    {
        // Tabela de símbolos para armazenar variáveis e seus valores
        private SymbolTable _symbolTable;

        // Construtor da classe Interpreter
        public Interpreter()
        {
            // Inicializa a tabela de símbolos
            _symbolTable = new SymbolTable();
        }

        // Método para executar um comando fornecido como string
        public string Exec(string command)
        {
            // Se o comando for nulo, retorna uma string vazia
            if (command == null) return "";

            // Cria uma nova instância do analisador léxico (lexer) para analisar o comando
            var lexer = new Lexer(command);
            
            // Cria uma nova instância do parser para analisar a estrutura do comando
            var parser = new Parser(lexer, _symbolTable);
            
            // Executa a análise sintática e a execução do comando
            parser.Parse();

            // Retorna uma mensagem indicando que nenhum resultado foi produzido
            return "Nenhum resultado produzido";
        }
    }
}
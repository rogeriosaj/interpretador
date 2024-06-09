using System;
using System.Collections.Generic;

namespace interpretador
{
    // Classe SymbolTable para armazenar variáveis e seus valores
    public class SymbolTable
    {
        // Dicionário privado para armazenar pares de nome de variável e seu valor
        private Dictionary<string, int> _variables;

        // Construtor que inicializa o dicionário
        public SymbolTable()
        {
            _variables = new Dictionary<string, int>();
        }

        // Método para definir uma variável com um nome e um valor
        public void SetVariable(string name, int value)
        {
            _variables[name] = value; // Adiciona ou atualiza a variável no dicionário
        }

        // Método para obter o valor de uma variável pelo seu nome
        public int GetVariable(string name)
        {
            // Verifica se a variável está definida no dicionário
            if (_variables.ContainsKey(name))
            {
                return _variables[name]; // Retorna o valor da variável
            }
            // Lança uma exceção se a variável não estiver definida
            throw new Exception($"Variable not defined: {name}");
        }

        // Método para verificar se uma variável está definida
        public bool IsVariableDefined(string name)
        {
            return _variables.ContainsKey(name); // Retorna true se a variável estiver no dicionário, caso contrário false
        }
    }
}

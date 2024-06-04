﻿namespace compi
{
    // Classe principal do programa
    class Program
    {
        // Método principal (entry point) do programa
        static void Main(string[] args)
        {
            // Cria uma nova instância do interpretador
            var interpreter = new Interpreter();
            
            // Imprime uma mensagem inicial para o usuário
            Console.WriteLine("Calc Interpreter");
            
            // Declaração de uma variável para armazenar o comando do usuário
            string? command;
            
            // Loop principal do programa que continua executando até que o usuário forneça um comando vazio
            do
            {
                // Exibe o prompt para o usuário
                Console.Write(">");
                
                // Lê o comando do usuário a partir da entrada padrão
                command = Console.ReadLine();
                
                // Verifica se o comando não é nulo ou vazio
                if (!string.IsNullOrEmpty(command))
                {
                    // Executa o comando usando o interpretador
                    interpreter.Exec(command);
                }
            // Continua o loop enquanto o comando não for nulo ou vazio
            } while (!string.IsNullOrEmpty(command));
        }
    }
}

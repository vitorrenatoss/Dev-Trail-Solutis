using System;
using System.Runtime.Intrinsics.X86;

//See https://aka.ms/new-console-template for more information

class Program
{
    static void Main()
    {
        Console.Write("Digite N e A1: ");
        string[] entrada0 = Console.ReadLine().Split();
        int n = int.Parse(entrada0[0]);
        int a1 = int.Parse(entrada0[1]);

        int resultado = CalcularSomatorio(n, a1);
        Console.WriteLine(resultado);
        AdivinheONumero();
    }
    /*entrada: 10 2 | saída: 2046
    entrada: 5 15 | saída: 465*/
    static int CalcularSomatorio(int n, int a1)
    {
        int somatorio = a1 * 2;
        int total = 0;

        for (int i = 1; i < n; i++)
        {
            total = a1 + somatorio;
            a1 = total;
            somatorio *= 2;
        }

        return total;
    }

    static void AdivinheONumero()
    {
        Random random = new();
        int numeroCerto = random.Next(1, 11);
        int tentativas = 0;
        int palpite = 0;
        Console.WriteLine("Bem-vindo ao jogo de adivinhação!");
        Console.WriteLine("Tente adivinhar o número entre 1 e 10.");
        Console.WriteLine("Digite -1 para sair do jogo:");
        while (palpite != numeroCerto)
        {
            Console.Write("Digite seu palpite: ");
            palpite = int.Parse(Console.ReadLine());
            tentativas++;
            if (palpite < numeroCerto)
            {
                Console.WriteLine("Chutou baixo! Tente novamente.");
            }
            else if (palpite > numeroCerto)
            {
                Console.WriteLine("Chutou alto! Tente novamente.");
            }
            else if (palpite == -1)
            {
                Console.WriteLine("Jogo encerrado. Obrigado por jogar!");
                break;
            }
            else
            {
                Console.WriteLine($"Parabéns! Você adivinhou o número {numeroCerto} em {tentativas} tentativas.");
                break;
            }
        }
    }

}
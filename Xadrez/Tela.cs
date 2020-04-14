using System;
using Xadrez.Tabuleiro;
namespace Xadrez
{
    class Tela
    {

        public static void imprimirTabuleiro(tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                        Console.Write(" _ ");
                    else
                        Console.Write(" " + tab.peca(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

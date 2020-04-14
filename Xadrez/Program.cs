using System;
using Xadrez.Tabuleiro;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao P;

            tabuleiro tab = new tabuleiro(8, 8);

            Tela.imprimirTabuleiro(tab);
        }
    }
}

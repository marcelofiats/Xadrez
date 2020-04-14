using System;
using Xadrez.Tabuleiro;
using Xadrez.XadrezPecas;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Posicao P;

                tabuleiro tab = new tabuleiro(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(3, 2));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 0));

                Tela.imprimirTabuleiro(tab);
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine("Erro: " + e.Message);
            }
        }
    }
}

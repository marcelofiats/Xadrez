﻿using System;
using Xadrez.Tabuleiro;
using Xadrez.JogoXadrez;

namespace Xadrez
{
    class Tela
    {

        public static void imprimirTabuleiro(tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                        Console.Write(" - ");
                    else
                    {
                        Console.Write(" ");
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("    A  B  C  D  E  F  J  H");
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            String origem = Console.ReadLine();
            char coluna = origem[0];
            int linha = int.Parse(origem[1] + "");
            return new PosicaoXadrez(coluna, linha);
            
        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}

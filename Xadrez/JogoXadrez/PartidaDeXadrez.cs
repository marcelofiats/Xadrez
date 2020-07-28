using System;
using Xadrez.Tabuleiro;
using Xadrez.XadrezPecas;
using Xadrez;
using System.Collections.Generic;

namespace Xadrez.JogoXadrez
{
    class PartidaDeXadrez
    {
        public tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnpassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new tabuleiro(8, 8);
            turno = 1;
            JogadorAtual = Cor.Branca;
            terminada = false;
            vulneravelEnpassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;
            colocarPecas();
        }

        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementaQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }

            // JOGADA ESPECIAL ROQUE PEQUENO
            if(p is Rei && destino.coluna == origem.coluna + 2){
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementaQteMovimento();
                tab.colocarPeca(T, destinoT);
            }

            // JOGADA ESPECIAL ROQUE GRANDE
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna -4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementaQteMovimento();
                tab.colocarPeca(T, destinoT);
            }

            // JOGADA ESPECIAL EN PASSANT

            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                            posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementaQteMovimento();
            if(pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            // desfazendo ESPECIAL ROQUE PEQUENO
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementaQteMovimento();
                tab.colocarPeca(T, origemT);
            }

            // JOGADA ESPECIAL ROQUE GRANDE
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementaQteMovimento();
                tab.colocarPeca(T, origemT);
            }

            // JOGADA ESPECIAL EN PASSANT
            if (p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEnpassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                }
            }
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {

            Peca pecaCapturada = executarMovimento(origem, destino);

            if(estaEmXeque(JogadorAtual))
            {
                desfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("você não pode se colocar em xeque!");
            }

            Peca p = tab.peca(destino);

            // # JOGADA ESPECIAL PROMOCAO

            if(p is Peao)
            {
                if((p.cor == Cor.Branca && destino.linha == 0) || p.cor == Cor.Preta && destino.linha == 7)
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(JogadorAtual)))
            {
                xeque = true;
            }else
            {
                xeque = false;
            }
            if (testeXequeMate(adversaria(JogadorAtual)))
            {
                Console.WriteLine("VENCEDOR PEÇA " + JogadorAtual);
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
            
            //#jogadaespecial en passant
            if(p is Peao && (destino.linha == origem.linha -2 || destino.linha == origem.linha +2))
            {
                vulneravelEnpassant = p;
            }
            else
            {
                vulneravelEnpassant = null;
            }

        }



        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("não existe peça na posição de origem escolhida!!!");
            }
            if(JogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("a peca seecionada não é sua!!!");
            }
            if (!tab.peca(pos).existeMovimentoPossiveis())
            {
                throw new TabuleiroException("não existe movimento possivel para essa peça!!!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Possicao de destino invalida!!!");
            }
        }

        private void mudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Peca rei (Cor cor)
        {
            foreach(Peca x in pecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);

            foreach(Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i<tab.linhas; i++)
                {
                    for(int j = 0; j<tab.colunas; j++)
                    {
                        if(mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazerMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            
            for(char i = 'a'; i <= 'h'; i++)
            {
                colocarNovaPeca(i, 2, new Peao(tab, Cor.Branca, this));
            }

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('e', 8, new Dama(tab, Cor.Preta));

            
            for (char i = 'a'; i <= 'h'; i++)
            {
                colocarNovaPeca(i, 7, new Peao(tab, Cor.Preta, this));
            }

        }
    }
}

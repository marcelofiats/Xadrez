using Xadrez.Tabuleiro;

namespace Xadrez.XadrezPecas
{
    class Peao : Peca
    {
        public Peao(tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return " P ";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);
            //Acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValores(posicao.linha - 2, posicao.coluna);
            if (tab.posicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //Direita
            pos.definirValores(posicao.linha-1, posicao.coluna + 1);
            if(tab.peca(pos) == null)
            {
                mat[pos.linha, pos.coluna] = false;
            }
            else if (tab.peca(pos).cor!= this.cor)
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //Esquerda
            pos.definirValores(posicao.linha -1, posicao.coluna - 1);
            if (tab.peca(pos) == null)
            {
                mat[pos.linha, pos.coluna] = false;
            }
            else if (tab.peca(pos).cor != this.cor)
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}

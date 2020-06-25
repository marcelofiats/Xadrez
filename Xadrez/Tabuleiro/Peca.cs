namespace Xadrez.Tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao{ get; set; }
        public Cor cor{ get; set; }
        public int qteMovimento{ get; set; }
        public tabuleiro tab{ get; set; }

        public Peca(tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            qteMovimento = 0;            
        }

        public bool existeMovimentoPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for(int i = 0; i< tab.linhas; i++)
            {
                for(int j =0; j< tab.colunas; j++)
                {
                   if(mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();

        public void incrementaQteMovimento()
        {
            qteMovimento++;
        }
    }
}

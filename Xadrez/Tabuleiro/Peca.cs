namespace Xadrez.Tabuleiro
{
    class Peca
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

        public void incrementaQteMovimento()
        {
            qteMovimento++;
        }
    }
}

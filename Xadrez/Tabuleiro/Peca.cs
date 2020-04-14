namespace Xadrez.Tabuleiro
{
    class Peca
    {
        public Posicao posicao{ get; set; }
        public Cor cor{ get; set; }
        public int qteMovimento{ get; set; }
        public Tabuleiro tab{ get; set; }
    }
}

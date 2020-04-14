using Xadrez.Tabuleiro;

namespace Xadrez.XadrezPecas
{
    class Bispo : Peca
    {
        public Bispo(tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "B";
        }
    }
}

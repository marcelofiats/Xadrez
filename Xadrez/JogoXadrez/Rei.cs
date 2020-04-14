using Xadrez.Tabuleiro;

namespace Xadrez.XadrezPecas
{
    class Rei : Peca
    {
        public Rei(tabuleiro tab, Cor cor) : base (tab, cor)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}

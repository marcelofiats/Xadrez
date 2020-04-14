using Xadrez.Tabuleiro;

namespace Xadrez.XadrezPecas
{
    class Torre : Peca
    {
        public Torre(tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "T";
        }
    }
}

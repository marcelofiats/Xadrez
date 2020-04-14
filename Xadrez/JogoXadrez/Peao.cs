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
            return "P";
        }
    }
}

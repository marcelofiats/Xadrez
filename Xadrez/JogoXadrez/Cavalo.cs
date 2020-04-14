using Xadrez.Tabuleiro;

namespace Xadrez.XadrezPecas
{
    class Cavalo : Peca
    {
        public Cavalo(tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "C";
        }
    }
}

using Xadrez.Tabuleiro;

namespace Xadrez.XadrezPecas
{
    class Dama : Peca
    {
        public Dama(tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "D";
        }
    }
}

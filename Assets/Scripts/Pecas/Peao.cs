using System.Collections.Generic;

public class Peao : Peca {
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        var x = GetX();
        var z = GetZ();

        return isBranca ? MovimentosBranco(x, z) : MovimentosPreto(x, z);
    }

    private List<Movimento> MovimentosBranco(int x, int z) {
        var lista = new List<Movimento>();
        if (!GetPecaJogador(x, z + 1) && !GetPecaAdversario(x, z + 1)) {
            // Só vai pra frente se não tiver nenhuma peca no caminho
            lista.Add(new Movimento(x, z + 1));
            if (!isMovimentou) lista.Add(new Movimento(x, z + 2));
        }

        var pecaDiagonalCimaDireita = GetPecaAdversario(x + 1, z + 1);
        if (pecaDiagonalCimaDireita)
            lista.Add(new Movimento(x + 1, z + 1)); // Comer peça diagonal cima direita            

        var pecaDiagonalCimaEsquerda = GetPecaAdversario(x - 1, z + 1);
        if (pecaDiagonalCimaEsquerda)
            lista.Add(new Movimento(x - 1, z + 1)); // Comer peça diagonal cima esquerda            

        return lista;
    }

    private List<Movimento> MovimentosPreto(int x, int z) {
        var lista = new List<Movimento>();
        if (!GetPecaJogador(x, z - 1) && !GetPecaAdversario(x, z - 1)) {
            // Só vai pra frente se não tiver nenhuma peca no caminho
            lista.Add(new Movimento(x, z - 1));
            if (!isMovimentou) lista.Add(new Movimento(x, z - 2));
        }

        var pecaDiagonalCimaDireita = GetPecaAdversario(x + 1, z - 1);
        if (pecaDiagonalCimaDireita)
            lista.Add(new Movimento(x + 1, z - 1)); // Comer peça diagonal baixo direita            

        var pecaDiagonalCimaEsquerda = GetPecaAdversario(x - 1, z - 1);
        if (pecaDiagonalCimaEsquerda)
            lista.Add(new Movimento(x - 1, z - 1)); // Comer peça diagonal baixo esquerda            

        return lista;
    }

    public bool PodeMatarRei(int x, int z) {
        if (isBranca) {
            var diagonalDireita = x == x + 1 && z == z + 1;
            var diagonalEsquerda = x == x - 1 && z == z + 1;
            return diagonalDireita || diagonalEsquerda;
        } else {
            var diagonalDireita = x == x + 1 && z == z - 1;
            var diagonalEsquerda = x == x - 1 && z == z - 1;
            return diagonalDireita || diagonalEsquerda;
        }
    }
}
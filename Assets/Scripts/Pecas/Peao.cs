using System.Collections.Generic;
using UnityEngine;

/**
 * Script de peça tipo Peão
 */
public class Peao : Peca {
    /**
     * Retorna todos os movimentos do Peão
     *     1 casa à frente
     *     2 casas à frente (se primeira movimentação)
     *     1 casa à diagonal a sua volta caso possua uma peça inimiga na posição (comer peça)
     */
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        var x = GetX();
        var z = GetZ();

        return isBranca ? MovimentosBranco(x, z) : MovimentosPreto(x, z);
    }

    private List<Movimento> MovimentosBranco(int x, int z) {
        var lista = new List<Movimento>();
        if (!GetPecaJogador(x, z + 1) && !GetPecaAdversario(x, z + 1)) { // 1 casa a frente
            lista.Add(new Movimento(x, z + 1));
        
            if (!isMovimentou && !GetPecaJogador(x, z + 2) && !GetPecaAdversario(x, z + 2)) { // 2 casas a frente caso nao movimentou ainda
                lista.Add(new Movimento(x, z + 2));
            }
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
        if (!GetPecaJogador(x, z - 1) && !GetPecaAdversario(x, z - 1)) { // 1 casa a frente
            lista.Add(new Movimento(x, z - 1));
        
            if (!isMovimentou && !GetPecaJogador(x, z - 2) && !GetPecaAdversario(x, z - 2)) { // 2 casas a frente caso nao movimentou ainda
                lista.Add(new Movimento(x, z - 2));
            }
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
        var xPeca = GetX();
        var zPeca = GetZ();
        if (isBranca) {
            var diagonalDireita = x == xPeca + 1 && z == zPeca + 1;
            var diagonalEsquerda = x == xPeca - 1 && z == zPeca + 1;
            return diagonalDireita || diagonalEsquerda;
        } else {
            var diagonalDireita = x == xPeca + 1 && z == zPeca - 1;
            var diagonalEsquerda = x == xPeca - 1 && z == zPeca - 1;
            return diagonalDireita || diagonalEsquerda;
        }
    }
}
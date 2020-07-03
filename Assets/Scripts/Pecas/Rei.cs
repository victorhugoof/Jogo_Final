using System.Collections.Generic;
using UnityEngine;

/**
 * Script de peça tipo Rei
 */
public class Rei : Peca {
    private readonly int[][] _posicoes = {
        new[] {-1, -1}, new[] {-1, 0}, new[] {-1, 1}, // Esquerda
        new[] {0, -1}, new[] {0, 1}, // Centro
        new[] {1, -1}, new[] {1, 0}, new[] {1, 1} // Direita
    };

    /**
     * Retorna todos os movimentos do Rei
     *     Todas as casas à sua volta
     */
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        var x = GetX();
        var z = GetZ();

        var lista = new List<Movimento>();
        foreach (var posicao in _posicoes) {
            var posicaoX = x + posicao[0];
            var posicaoZ = z + posicao[1];

            if (IsSemPecasAdversariasVizinhas(posicaoX, posicaoZ)) lista.Add(new Movimento(posicaoX, posicaoZ));
        }

        return lista;
    }

    /**
     * Retorna se a movimento que o Rei não irá colocá-lo em cheque
     */
    private bool IsSemPecasAdversariasVizinhas(int x, int z) {
        foreach (var posicao in _posicoes) {
            var posicaoX = posicao[0];
            var posicaoZ = posicao[1];

            var pecaAdversario = GetPecaAdversario(x + posicaoX, z + posicaoZ);
            if (pecaAdversario) {
                if (pecaAdversario.IsPeao()) {
                    if (((Peao) pecaAdversario).PodeMatarRei(x, z)) {
                        return false;
                    }
                } else if (pecaAdversario.GetMovimentos().Exists(movimento => movimento.X == x && movimento.Z == z)) {
                    Debug.Log($"Pode matar rei! X: {x + posicaoX}, Z: {z + posicaoZ}");
                    return false;
                }
            }
        }

        return true;
    }
}
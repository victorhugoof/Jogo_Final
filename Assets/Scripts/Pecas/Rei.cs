using System.Collections.Generic;

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
                    var podeMatar = ((Peao) pecaAdversario).PodeMatarRei(x, z);
                    return podeMatar;
                }

                if (pecaAdversario.GetMovimentos().Exists(movimento => movimento.X == x && movimento.Z == z))
                    return false;
            }
        }

        return true;
    }
}
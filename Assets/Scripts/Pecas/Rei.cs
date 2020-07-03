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
        var pecasAdversario = GetPecasAdversarioNotRei();
        foreach (var posicao in _posicoes) {
            var posicaoX = x + posicao[0];
            var posicaoZ = z + posicao[1];

            if (!IsAdversarioPodeComer(posicaoX, posicaoZ, pecasAdversario))
                lista.Add(new Movimento(posicaoX, posicaoZ));
        }

        return lista;
    }

    /**
     * Retorna se a movimento que o Rei não irá colocá-lo em cheque
     */
    private bool IsAdversarioPodeComer(int x, int z, IEnumerable<Peca> pecasAdversario) {
        foreach (var peca in pecasAdversario) {
            if (peca.IsPeao()) {
                if (((Peao) peca).PodeMatarRei(x, z)) {
                    return true;
                }
            } else if (peca.GetMovimentos().Exists(movimento => movimento.X == x && movimento.Z == z)) {
                return true;
            }
        }

        foreach (var posicao in _posicoes) {
            var posicaoX = posicao[0];
            var posicaoZ = posicao[1];

            var pecaAdversario = GetPecaAdversario(x + posicaoX, z + posicaoZ);
            if (pecaAdversario
                && pecaAdversario.IsRei()
                && pecaAdversario.GetMovimentos().Exists(movimento => movimento.X == x && movimento.Z == z)) {
                return true;
            }
        }

        return false;
    }

    /**
     * Retorna todas as pecas adversárias
     */
    private IEnumerable<Peca> GetPecasAdversarioNotRei() {
        var pecas = GetPecas();
        var pecasAdversarias = new List<Peca>();
        foreach (var peca in pecas) {
            if (peca && peca.isBranca != isBranca && !peca.IsRei()) {
                pecasAdversarias.Add(peca);
            }
        }

        return pecasAdversarias;
    }
}
using System.Collections.Generic;

/**
 * Script de peça tipo Torre
 */
public class Torre : Peca {

    /**
     * Retorna todos os movimentos da Torre
     *     Todas as casas à direita
     *     Todas as casas à esquerda
     *     Todas as casas à baixo
     *     Todas as casas à cima
     */
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        return GetMovimentos(GetX(), GetZ(), this);
    }

    public static IEnumerable<Movimento> GetMovimentos(int X, int Z, Peca peca) {
        var lista = new List<Movimento>();

        var x = X;
        var z = Z;
        while (true) { // Direita
            x++;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        x = X;
        z = Z;
        while (true) { // Esquerda
            x--;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        x = X;
        z = Z;
        while (true) { // Baixo
            z--;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        x = X;
        z = Z;
        while (true) { // Cima
            z++;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        return lista;
    }
}
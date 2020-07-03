using System.Collections.Generic;

public class Bispo : Peca {
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        return GetMovimentos(GetX(), GetZ(), this);
    }

    public static IEnumerable<Movimento> GetMovimentos(int X, int Z, Peca peca) {
        var lista = new List<Movimento>();

        var x = X;
        var z = Z;
        while (true) { // Diagonal direita cima
            x++;
            z++;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        x = X;
        z = Z;
        while (true) { // Diagonal esquerda cima
            x--;
            z++;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        x = X;
        z = Z;
        while (true) { // Diagonal esquerda baixo
            x--;
            z--;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        x = X;
        z = Z;
        while (true) { // Diagonal direita baixo
            x++;
            z--;
            if (!Utils.IsValidPosition(x, z)) break;
            if (GetPecaJogador(x, z, peca)) break;
            lista.Add(new Movimento(x, z));
            if (GetPecaAdversario(x, z, peca)) break;
        }

        return lista;
    }
}
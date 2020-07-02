using System.Collections.Generic;

public class Rei : Peca {

    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        var x = GetX();
        var z = GetZ();

        return new List<Movimento> {
            new Movimento(x - 1, z - 1), // Diagonal esquerda baixo
            new Movimento(x - 1, z), // Esquerda
            new Movimento(x - 1, z + 1), // Diagonal esquerda cima
            new Movimento(x, z - 1), // Baixo
            new Movimento(x, z + 1), // Cima
            new Movimento(x + 1, z - 1), // Diagonal direita baixo
            new Movimento(x + 1, z), // Direita
            new Movimento(x + 1, z + 1) // Diagonal direita cima
        };
    }
}
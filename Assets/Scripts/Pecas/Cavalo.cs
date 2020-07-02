using System.Collections.Generic;

public class Cavalo : Peca {
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        var x = GetX();
        var z = GetZ();

        return new List<Movimento> {
            new Movimento(x - 1, z - 2),
            new Movimento(x - 1, z + 2),
            new Movimento(x + 1, z + 2),
            new Movimento(x + 1, z - 2),
            new Movimento(x - 2, z - 1),
            new Movimento(x + 2, z - 1),
            new Movimento(x - 2, z + 1),
            new Movimento(x + 2, z + 1),
        };
    }
}
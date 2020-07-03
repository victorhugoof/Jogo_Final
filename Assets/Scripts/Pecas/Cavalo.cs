using System.Collections.Generic;

/**
 * Script de peça tipo Cavalo
 */
public class Cavalo : Peca {
    
    /**
     * Retorna todos os movimentos do Cavalo
     *     Pulo em L (não precisa validar se tem peça, pois o cavalo pula as peças no caminho)
     */
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
            new Movimento(x + 2, z + 1)
        };
    }
}
using System.Collections.Generic;

/**
 * Script de peça tipo Rainha
 */
public class Rainha : Peca {
    
    /**
     * Retorna todos os movimentos da Rainha
     *     Todos os movimentos do Bispo
     *     Todos os movimentos da Torre
     */
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        var x = GetX();
        var z = GetZ();

        var lista = new List<Movimento>();
        lista.AddRange(Bispo.GetMovimentos(x, z, this)); // Movimentos do Bispo
        lista.AddRange(Torre.GetMovimentos(x, z, this)); // Movimentos da Torre
        return lista;
    }
}
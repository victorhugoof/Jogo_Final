using System.Collections.Generic;

public class Rainha : Peca {
    protected override IEnumerable<Movimento> GetMovimentosPossiveis() {
        var x = GetX();
        var z = GetZ();

        var lista = new List<Movimento>();
        lista.AddRange(Bispo.GetMovimentos(x, z, this)); // Movimentos do Bispo
        lista.AddRange(Torre.GetMovimentos(x, z, this)); // Movimentos da Torre
        return lista;
    }
}
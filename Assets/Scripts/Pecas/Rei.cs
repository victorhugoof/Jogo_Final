public class Rei : PecaXadrez {
    public override bool[,] Movimentos() {
        var movimentos = new bool[8, 8];

        PermiteMover(GetX() + 1, GetZ(), ref movimentos); // up
        PermiteMover(GetX() - 1, GetZ(), ref movimentos); // down
        PermiteMover(GetX(), GetZ() - 1, ref movimentos); // left
        PermiteMover(GetX(), GetZ() + 1, ref movimentos); // right
        PermiteMover(GetX() + 1, GetZ() - 1, ref movimentos); // up left
        PermiteMover(GetX() - 1, GetZ() - 1, ref movimentos); // down left
        PermiteMover(GetX() + 1, GetZ() + 1, ref movimentos); // up right
        PermiteMover(GetX() - 1, GetZ() + 1, ref movimentos); // down right

        return movimentos;
    }
}
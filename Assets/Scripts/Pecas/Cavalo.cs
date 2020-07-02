public class Cavalo : PecaXadrez {
    public override bool[,] Movimentos() {
        var r = new bool[8, 8];

        // Up left
        PermiteMover(GetX() - 1, GetZ() + 2, ref r);

        // Up right
        PermiteMover(GetX() + 1, GetZ() + 2, ref r);

        // Down left
        PermiteMover(GetX() - 1, GetZ() - 2, ref r);

        // Down right
        PermiteMover(GetX() + 1, GetZ() - 2, ref r);


        // Left Down
        PermiteMover(GetX() - 2, GetZ() - 1, ref r);

        // Right Down
        PermiteMover(GetX() + 2, GetZ() - 1, ref r);

        // Left Up
        PermiteMover(GetX() - 2, GetZ() + 1, ref r);

        // Right Up
        PermiteMover(GetX() + 2, GetZ() + 1, ref r);

        return r;
    }
}
public class Torre : PecaXadrez {
    public override bool[,] Movimentos() {
        var r = new bool[8, 8];

        int i;

        // Right
        i = GetX();
        while (true) {
            i++;
            if (i >= 8) break;

            if (PermiteMover(i, GetZ(), ref r)) break;
        }

        // Left
        i = GetX();
        while (true) {
            i--;
            if (i < 0) break;

            if (PermiteMover(i, GetZ(), ref r)) break;
        }

        // Up
        i = GetZ();
        while (true) {
            i++;
            if (i >= 8) break;

            if (PermiteMover(GetX(), i, ref r)) break;
        }

        // Down
        i = GetZ();
        while (true) {
            i--;
            if (i < 0) break;

            if (PermiteMover(GetX(), i, ref r)) break;
        }

        return r;
    }
}
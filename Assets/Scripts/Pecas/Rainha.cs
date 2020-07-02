public class Rainha : PecaXadrez {
    public override bool[,] Movimentos() {
        var r = new bool[8, 8];

        int i, j;

        // Top left
        i = GetX();
        j = GetZ();
        while (true) {
            i--;
            j++;
            if (i < 0 || j >= 8) break;

            if (PermiteMover(i, j, ref r)) break;
        }

        // Top right
        i = GetX();
        j = GetZ();
        while (true) {
            i++;
            j++;
            if (i >= 8 || j >= 8) break;

            if (PermiteMover(i, j, ref r)) break;
        }

        // Down left
        i = GetX();
        j = GetZ();
        while (true) {
            i--;
            j--;
            if (i < 0 || j < 0) break;

            if (PermiteMover(i, j, ref r)) break;
        }

        // Down right
        i = GetX();
        j = GetZ();
        while (true) {
            i++;
            j--;
            if (i >= 8 || j < 0) break;

            if (PermiteMover(i, j, ref r)) break;
        }

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
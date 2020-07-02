using UnityEngine;

public class Peao : PecaXadrez {
    public override bool[,] Movimentos() {
        var r = new bool[8, 8];

        PecaXadrez c, c2;

        var e = GetTabuleiro().enPassantMove;

        if (branca) {
            ////// White team move //////

            // Diagonal left
            if (GetX() != 0 && GetZ() != 7) {
                if (e[0] == GetX() - 1 && e[1] == GetZ() + 1)
                    r[GetX() - 1, GetZ() + 1] = true;

                c = GetTabuleiro().pecas[GetX() - 1, GetZ() + 1];
                if (c != null && !c.branca)
                    r[GetX() - 1, GetZ() + 1] = true;
            }

            // Diagonal right
            if (GetX() != 7 && GetZ() != 7) {
                Debug.Log("X Peao: " + GetX());
                if (e[0] == GetX() + 1 && e[1] == GetZ() + 1)
                    r[GetX() + 1, GetZ() + 1] = true;

                c = GetTabuleiro().pecas[GetX() + 1, GetZ() + 1];
                if (c != null && !c.branca)
                    r[GetX() + 1, GetZ() + 1] = true;
            }

            // Middle
            if (GetZ() != 7) {
                c = GetTabuleiro().pecas[GetX(), GetZ() + 1];
                if (c == null)
                    r[GetX(), GetZ() + 1] = true;
            }

            // Middle on first move
            if (GetZ() == 1) {
                c = GetTabuleiro().pecas[GetX(), GetZ() + 1];
                c2 = GetTabuleiro().pecas[GetX(), GetZ() + 2];
                if (c == null && c2 == null)
                    r[GetX(), GetZ() + 2] = true;
            }
        } else {
            ////// Black team move //////

            // Diagonal left
            if (GetX() != 0 && GetZ() != 0) {
                if (e[0] == GetX() - 1 && e[1] == GetZ() - 1)
                    r[GetX() - 1, GetZ() - 1] = true;

                c = GetTabuleiro().pecas[GetX() - 1, GetZ() - 1];
                if (c != null && c.branca)
                    r[GetX() - 1, GetZ() - 1] = true;
            }

            // Diagonal right
            if (GetX() != 7 && GetZ() != 0) {
                if (e[0] == GetX() + 1 && e[1] == GetZ() - 1)
                    r[GetX() + 1, GetZ() - 1] = true;

                c = GetTabuleiro().pecas[GetX() + 1, GetZ() - 1];
                if (c != null && c.branca)
                    r[GetX() + 1, GetZ() - 1] = true;
            }

            // Middle
            if (GetZ() != 0) {
                c = GetTabuleiro().pecas[GetX(), GetZ() - 1];
                if (c == null)
                    r[GetX(), GetZ() - 1] = true;
            }

            // Middle on first move
            if (GetZ() == 6) {
                c = GetTabuleiro().pecas[GetX(), GetZ() - 1];
                c2 = GetTabuleiro().pecas[GetX(), GetZ() - 2];
                if (c == null && c2 == null)
                    r[GetX(), GetZ() - 2] = true;
            }
        }

        return r;
    }
}
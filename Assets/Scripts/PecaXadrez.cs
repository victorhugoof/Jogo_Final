using UnityEngine;

public abstract class PecaXadrez : MonoBehaviour {
    public bool branca;
    public abstract bool[,] Movimentos();

    protected bool PermiteMover(int x, int z, ref bool[,] r) {
        if (x >= 0 && x < 8 && z >= 0 && z < 8) {
            var peca = GetTabuleiro().pecas[x, z];
            if (peca == null) {
                r[x, z] = true;
            } else {
                r[x, z] = branca != peca.branca;
                return true;
            }
        }

        return false;
    }

    public int GetX() {
        return (int) transform.position.x;
    }

    public int GetZ() {
        return (int) transform.position.z;
    }

    protected TabuleiroXadrez GetTabuleiro() {
        return FindObjectOfType<TabuleiroXadrez>();
    }
}
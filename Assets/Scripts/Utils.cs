/**
 * Classe de utilitários
 */
public abstract class Utils {
    /**
     * Retorna se a posição informada está dentro do
     * intervalo válido de posiçoes 0-7
     */
    public static bool IsValidPosition(int x, int z) {
        return x >= 0 && x < 8 && z >= 0 && z < 8;
    }
}
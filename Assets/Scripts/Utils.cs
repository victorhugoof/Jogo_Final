public abstract class Utils {
    public static bool IsValidPosition(int x, int z) {
        return x >= 0 && x < 8 && z >= 0 && z < 8;
    }
}
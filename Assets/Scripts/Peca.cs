using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Peca : MonoBehaviour {
    private Tabuleiro _tabuleiro;
    public bool isBranca;
    public bool isMovimentou;
    public MeshRenderer meshRenderer;

    private void Start() {
        _tabuleiro = FindObjectOfType<Tabuleiro>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public List<Movimento> GetMovimentos() {
        var lista = GetMovimentosPossiveis();
        var pecas = _tabuleiro.pecas;

        return lista.Where(movimento => PermiteMover(movimento, pecas)).ToList();
    }

    public bool IsRei() {
        return GetType() == typeof(Rei);
    }

    public bool IsPeao() {
        return GetType() == typeof(Peao);
    }

    protected abstract IEnumerable<Movimento> GetMovimentosPossiveis();

    private bool PermiteMover(Movimento movimento, Peca[,] pecas) {
        var x = movimento.X;
        var z = movimento.Z;

        if (!Utils.IsValidPosition(x, z)) return false;

        var peca = pecas[x, z];
        if (peca) return isBranca != peca.isBranca;

        return true;
    }

    protected Peca GetPecaJogador(int x, int z) {
        return GetPecaJogador(x, z, this);
    }

    protected Peca GetPecaAdversario(int x, int z) {
        return GetPecaAdversario(x, z, this);
    }

    protected static Peca GetPecaAdversario(int x, int z, Peca escopo) {
        var peca = !Utils.IsValidPosition(x, z) ? null : escopo._tabuleiro.pecas[x, z];
        return peca && peca.isBranca != escopo.isBranca ? peca : null;
    }

    protected static Peca GetPecaJogador(int x, int z, Peca escopo) {
        var peca = !Utils.IsValidPosition(x, z) ? null : escopo._tabuleiro.pecas[x, z];
        return peca && peca.isBranca == escopo.isBranca ? peca : null;
    }

    public int GetX() {
        return (int) transform.position.x;
    }

    public int GetZ() {
        return (int) transform.position.z;
    }
}
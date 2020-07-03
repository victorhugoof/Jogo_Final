using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Script principal de uma peca, deve ser implementado
 * o método GetMovimentosPossiveis
 */
public abstract class Peca : MonoBehaviour {
    private Tabuleiro _tabuleiro;
    public bool isBranca;
    public bool isMovimentou;
    public MeshRenderer meshRenderer;

    /**
     * Inicia o script com o tabuleiro e o meshRenderer da peça (para marcá-la)
     */
    private void Start() {
        _tabuleiro = FindObjectOfType<Tabuleiro>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    /**
     * Retorna os movimentos que podem ser feitos, valida os movimentos possíveis
     * removendo movimentos onde a nova posição já possui uma peça do jogador
     */
    public List<Movimento> GetMovimentos() {
        var lista = GetMovimentosPossiveis();

        return lista.Where(movimento => {
            var x = movimento.X;
            var z = movimento.Z;

            return Utils.IsValidPosition(x, z) && !GetPecaJogador(x, z);
        }).ToList();
    }

    /**
     * Retorna se a peça é um Rei
     */
    public bool IsRei() {
        return GetType() == typeof(Rei);
    }

    /**
     * Retorna se a peça é um Peão
     */
    public bool IsPeao() {
        return GetType() == typeof(Peao);
    }

    /**
     * Deve retornar todos os movimentos que a peça pode dar
     */
    protected abstract IEnumerable<Movimento> GetMovimentosPossiveis();

    /**
     * Retorna todas as pecas do tabuleiro
     */
    protected IEnumerable<Peca> GetPecas() {
        return _tabuleiro.pecas.Cast<Peca>().ToList();
    }

    /**
     * Busca uma peca do jogador na posição informada, se
     * não encontrada retorna null
     */
    protected Peca GetPecaJogador(int x, int z) {
        return GetPecaJogador(x, z, this);
    }

    /**
     * Busca uma peca do jogador na posição informada, se
     * não encontrada retorna null
     * método statico
     */
    protected static Peca GetPecaJogador(int x, int z, Peca escopo) {
        var peca = !Utils.IsValidPosition(x, z) ? null : escopo._tabuleiro.pecas[x, z];
        return peca && peca.isBranca == escopo.isBranca ? peca : null;
    }

    /**
     * Busca uma peca do adversário na posição informada, se
     * não encontrada retorna null
     */
    protected Peca GetPecaAdversario(int x, int z) {
        return GetPecaAdversario(x, z, this);
    }

    /**
     * Busca uma peca do adversário na posição informada, se
     * não encontrada retorna null
     * método statico
     */
    protected static Peca GetPecaAdversario(int x, int z, Peca escopo) {
        var peca = !Utils.IsValidPosition(x, z) ? null : escopo._tabuleiro.pecas[x, z];
        return peca && peca.isBranca != escopo.isBranca ? peca : null;
    }

    /**
     * Retorna a posição X atual
     */
    public int GetX() {
        return (int) transform.position.x;
    }

    /**
     * Retorna a posição Z atual
     */
    public int GetZ() {
        return (int) transform.position.z;
    }
}
using System.Collections.Generic;
using UnityEngine;

/**
 * Script responsável por marcar as peças e suas possíveis posições
 */
public class Marcador : MonoBehaviour {
    private readonly List<GameObject> _marcadores = new List<GameObject>();
    private readonly Dictionary<Peca, Material> _materials = new Dictionary<Peca, Material>();
    public float espacamentoMarcadorTabuleiro;
    public GameObject marcadorTabuleiro;
    public Material materialPecaSelecionada;

    public void MarcarPeca(Peca peca) {
        DesmarcarPecas();

        var meshRenderer = peca.meshRenderer;
        _materials.Add(peca, meshRenderer.material);

        materialPecaSelecionada.mainTexture = meshRenderer.material.mainTexture;
        meshRenderer.material = materialPecaSelecionada;

        MarcarPosicoes(peca.GetMovimentos());
    }

    public void DesmarcarPecas() {
        DesmarcarPosicoes();
        foreach (var entry in _materials) {
            var meshRenderer = entry.Key.meshRenderer;
            meshRenderer.material = entry.Value;
        }

        _materials.Clear();
    }

    private void MarcarPosicoes(List<Movimento> movimentos) {
        movimentos.ForEach(movimento => {
            var marcadorObject = GetMarcardor();
            marcadorObject.SetActive(true);
            marcadorObject.transform.position = new Vector3(movimento.X + espacamentoMarcadorTabuleiro, 0.0001f,
                movimento.Z + espacamentoMarcadorTabuleiro);
        });
    }

    private void DesmarcarPosicoes() {
        _marcadores.ForEach(mr => mr.SetActive(false));
    }

    private GameObject GetMarcardor() {
        var marcadorObject = _marcadores.Find(g => !g.activeSelf);

        if (!marcadorObject) {
            marcadorObject = Instantiate(marcadorTabuleiro);
            _marcadores.Add(marcadorObject);
        }

        return marcadorObject;
    }
}
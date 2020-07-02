using System.Collections.Generic;
using UnityEngine;

public class Marcador : MonoBehaviour {
    public GameObject marcadorTabuleiro;
    public float espacamentoMarcadorTabuleiro;
    public Material materialPecaSelecionada;

    private readonly List<GameObject> _marcadores = new List<GameObject>();
    private readonly Dictionary<Peca, Material> _materials = new Dictionary<Peca, Material>();

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
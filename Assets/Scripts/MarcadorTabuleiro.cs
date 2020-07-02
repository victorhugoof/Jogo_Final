using System.Collections.Generic;
using UnityEngine;

public class MarcadorTabuleiro : MonoBehaviour {

    public GameObject marcador;
    public float espacamento;
    private readonly List<GameObject> _marcadores = new List<GameObject>();

    public void Marcar(bool[,] moves) {
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                if (moves[i, j]) {
                    var marcadorObject = GetMarcardor();
                    marcadorObject.SetActive(true);
                    marcadorObject.transform.position = new Vector3(i + espacamento, 0.0001f, j + espacamento);
                }
            }
        }
    }

    public void Desmarcar() {
        _marcadores.ForEach(mr => mr.SetActive(false));
    }

    private GameObject GetMarcardor() {
        var marcadorObject = _marcadores.Find(g => !g.activeSelf);

        if (marcadorObject == null) {
            marcadorObject = Instantiate(marcador);
            _marcadores.Add(marcadorObject);
        }

        return marcadorObject;
    }
}
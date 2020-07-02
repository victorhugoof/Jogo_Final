using System.Linq;
using UnityEngine;

public class Tabuleiro : MonoBehaviour {
    private static readonly Quaternion RotacaoBranco = Quaternion.Euler(0, -90, 0);
    private static readonly Quaternion RotacaoPreto = Quaternion.Euler(0, 90, 0);

    public Peca[,] pecas;

    public GameObject rainhaBranco;
    public GameObject rainhaPreto;
    public GameObject reiBranco;
    public GameObject reiPreto;
    public GameObject bispoBranco;
    public GameObject bispoPreto;
    public GameObject cavaloBranco;
    public GameObject cavaloPreto;
    public GameObject peaoBranco;
    public GameObject peaoPreto;
    public GameObject torreBranco;
    public GameObject torrePreto;
    public float tamanhoCasa;

    private bool _vezBranco = true;
    private Peca _pecaSelecionada;
    private Marcador _marcador;
    private Camera _camera;

    public void Start() {
        _marcador = gameObject.GetComponent<Marcador>();
        _camera = Camera.main;

        CriarPecas();
    }

    public void Update() {
        var pontos = UpdateSelecao();
        var x = pontos[0];
        var z = pontos[1];

        if (!Utils.IsValidPosition(x, z)) {
            if (!_pecaSelecionada) {
                _marcador.DesmarcarPecas();
            }
            return;
        }
        
        if (Input.GetMouseButtonDown(0)) {
            if (!_pecaSelecionada) {
                SelecionarPeca(x, z);
            } else {
                MoverPeca(x, z);
            }
        } else if (!_pecaSelecionada) {
            MarcarPeca(x, z);
        }
    }

    private int[] UpdateSelecao() {
        var x = -1;
        var z = -1;

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, 150f, LayerMask.GetMask("Tabuleiro"))) {
            x = (int) (hit.point.x + 0.4f);
            z = (int) (hit.point.z + 0.4f);
        }

        return new[] {x, z};
    }

    private void SelecionarPeca(int x, int y) {
        _pecaSelecionada = pecas[x, y];

        if (!_pecaSelecionada) {
            return;
        }

        if (_pecaSelecionada.isBranca != _vezBranco || _pecaSelecionada.GetMovimentos().Count <= 0) {
            _pecaSelecionada = null;
            return;
        }

        _marcador.MarcarPeca(_pecaSelecionada);
    }

    private void MoverPeca(int x, int z) {
        var movimento = _pecaSelecionada.GetMovimentos().FirstOrDefault(mv => mv.X == x && mv.Z == z);
        if (movimento != null) {
            var pecaAdversaria = pecas[x, z];
            if (pecaAdversaria && pecaAdversaria.isBranca != _vezBranco) { // Comeu peca
                if (pecaAdversaria.GetType() == typeof(Rei)) { // Fim de jogo
                    Application.Quit();
                }

                Destroy(pecaAdversaria.gameObject);
            }


            var oldX = _pecaSelecionada.GetX();
            var oldZ = _pecaSelecionada.GetZ();
            _pecaSelecionada.transform.position = GetPosicao(x, z);
            _pecaSelecionada.isMovimentou = true;

            pecas[x, z] = _pecaSelecionada;
            pecas[oldX, oldZ] = null;
            SwitchPlayer();
        }

        _pecaSelecionada = null;
        _marcador.DesmarcarPecas();
    }

    private void MarcarPeca(int x, int y) {
        var peca = pecas[x, y];

        if (!peca || peca.isBranca != _vezBranco || peca.GetMovimentos().Count <= 0) {
            _marcador.DesmarcarPecas();
            return;
        }

        _marcador.MarcarPeca(peca);
    }

    private void SwitchPlayer() {
        _vezBranco = !_vezBranco;
    }

    private void CriarPecas() {
        pecas = new Peca[8, 8];

        /*
         * Pecas brancas
         */
        CriaPeca(0, 0, torreBranco, RotacaoBranco);
        CriaPeca(1, 0, cavaloBranco, RotacaoBranco);
        CriaPeca(2, 0, bispoBranco, RotacaoBranco);
        CriaPeca(3, 0, rainhaBranco, RotacaoBranco);
        CriaPeca(4, 0, reiBranco, RotacaoBranco);
        CriaPeca(5, 0, bispoBranco, RotacaoBranco);
        CriaPeca(6, 0, cavaloBranco, RotacaoBranco);
        CriaPeca(7, 0, torreBranco, RotacaoBranco);

        for (var i = 0; i < 8; i++) CriaPeca(i, 1, peaoBranco, RotacaoBranco);

        /*
         * Pecas pretas
         */
        CriaPeca(0, 7, torrePreto, RotacaoPreto);
        CriaPeca(1, 7, cavaloPreto, RotacaoPreto);
        CriaPeca(2, 7, bispoPreto, RotacaoPreto);
        CriaPeca(3, 7, reiPreto, RotacaoPreto);
        CriaPeca(4, 7, rainhaPreto, RotacaoPreto);
        CriaPeca(5, 7, bispoPreto, RotacaoPreto);
        CriaPeca(6, 7, cavaloPreto, RotacaoPreto);
        CriaPeca(7, 7, torrePreto, RotacaoPreto);

        for (var i = 0; i < 8; i++) CriaPeca(i, 6, peaoPreto, RotacaoPreto);
    }

    private void CriaPeca(int x, int z, GameObject peca, Quaternion rotacao) {
        var instantiate = Instantiate(peca, GetPosicao(x, z), rotacao);
        instantiate.transform.SetParent(transform);

        pecas[x, z] = instantiate.GetComponent<Peca>();
    }

    private Vector3 GetPosicao(int x, int z) {
        var posicao = Vector3.zero;
        posicao.x += tamanhoCasa * x;
        posicao.z += tamanhoCasa * z;
        return posicao;
    }
}
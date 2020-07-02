using UnityEngine;

public class TabuleiroXadrez : MonoBehaviour {
    private static readonly Quaternion RotacaoBranco = Quaternion.Euler(0, -90, 0);
    private static readonly Quaternion RotacaoPreto = Quaternion.Euler(0, 90, 0);

    public PecaXadrez[,] pecas;
    public int[] enPassantMove;

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
    public Material materialPecaSelecionada;

    private int _selectionX = -1;
    private int _selectionZ = -1;
    private bool _vezBranco = true;
    private PecaXadrez _pecaSelecionada;
    private bool[,] _movimentosPermitidos;
    private Material _materialOriginal;
    private MarcadorTabuleiro _marcadorPosicoes;

    public void Start() {
        _marcadorPosicoes = gameObject.GetComponent<MarcadorTabuleiro>();
        
        CriarPecas();
        enPassantMove = new[] {-1, 1};
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            UpdateSelection();
            if (_selectionX >= 0 && _selectionZ >= 0) {
                if (_pecaSelecionada == null) {
                    SelectChessman(_selectionX, _selectionZ);
                } else {
                    MoveChessman(_selectionX, _selectionZ);
                }
            }
        }
    }

    private void UpdateSelection() {
        if (!Camera.main) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 150f,
            LayerMask.GetMask("Tabuleiro"))) {
            _selectionX = (int) (hit.point.x + 0.4f);
            _selectionZ = (int) (hit.point.z + 0.4f);
        } else {
            _selectionX = -1;
            _selectionZ = -1;
        }
    }

    private void SelectChessman(int x, int y) {
        Debug.Log("X: " + x + ", Y: " + y + ", peca: " + pecas[x, y] + ", vez branco: " + _vezBranco);
        if (pecas[x, y] == null || pecas[x, y].branca != _vezBranco) {
            return;
        }

        bool hasAtLeastOneMove = false;
        _movimentosPermitidos = pecas[x, y].Movimentos();
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (_movimentosPermitidos[i, j]) {
                    hasAtLeastOneMove = true;
                    i = 8;
                    break;
                }
            }
        }

        if (!hasAtLeastOneMove) {
            return;
        }

        _pecaSelecionada = pecas[x, y];
        _materialOriginal = _pecaSelecionada.GetComponent<MeshRenderer>().material;
        materialPecaSelecionada.mainTexture = _materialOriginal.mainTexture;
        _pecaSelecionada.GetComponent<MeshRenderer>().material = materialPecaSelecionada;

        _marcadorPosicoes.Marcar(_movimentosPermitidos);
    }

    private void MoveChessman(int x, int z) {
        if (_movimentosPermitidos[x, z]) {
            PecaXadrez c = pecas[x, z];

            if (c != null && c.branca != _vezBranco) { // Comeu peca
                if (c.GetType() == typeof(Rei)) {
                    Debug.Log("Fim de Jogo! " + (_vezBranco ? "Branco Venceu" : "Preto Venceu"));
                    Application.Quit();
                }

                Destroy(c.gameObject);
            }

            if (x == enPassantMove[0] && z == enPassantMove[1]) {
                c = _vezBranco ? pecas[x, z - 1] : pecas[x, z + 1];
                Destroy(c.gameObject);
            }

            enPassantMove[0] = -1;
            enPassantMove[1] = -1;
            if (_pecaSelecionada.GetType() == typeof(Peao)) {
                enPassantMove[0] = x;
                if (_pecaSelecionada.GetZ() == 1 && z == 3)
                    enPassantMove[1] = z - 1;
                else if (_pecaSelecionada.GetZ() == 6 && z == 4)
                    enPassantMove[1] = z + 1;
            }

            pecas[_pecaSelecionada.GetX(), _pecaSelecionada.GetZ()] = null;

            var posicao = Vector3.zero;
            posicao.x += tamanhoCasa * x;
            posicao.z += tamanhoCasa * z;

            _pecaSelecionada.transform.position = posicao;
            pecas[x, z] = _pecaSelecionada;
            SwitchPlayer();
        }

        _pecaSelecionada.GetComponent<MeshRenderer>().material = _materialOriginal;

        _marcadorPosicoes.Desmarcar();
        _pecaSelecionada = null;
    }

    private void SwitchPlayer() {
        _vezBranco = !_vezBranco;
    }

    private void CriarPecas() {
        pecas = new PecaXadrez[8, 8];

        /*
         * Pecas brancas
         */
        CriaPeca(0, 0, torreBranco, RotacaoBranco);
        CriaPeca(1, 0, cavaloBranco, RotacaoBranco);
        CriaPeca(2, 0, bispoBranco, RotacaoBranco);
        CriaPeca(3, 0, reiBranco, RotacaoBranco);
        CriaPeca(4, 0, rainhaBranco, RotacaoBranco);
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
        CriaPeca(3, 7, rainhaPreto, RotacaoPreto);
        CriaPeca(4, 7, reiPreto, RotacaoPreto);
        CriaPeca(5, 7, bispoPreto, RotacaoPreto);
        CriaPeca(6, 7, cavaloPreto, RotacaoPreto);
        CriaPeca(7, 7, torrePreto, RotacaoPreto);

        for (var i = 0; i < 8; i++) CriaPeca(i, 6, peaoPreto, RotacaoPreto);
    }

    private void CriaPeca(int x, int z, GameObject peca, Quaternion rotacao) {
        var posicao = Vector3.zero;
        posicao.x += tamanhoCasa * x;
        posicao.z += tamanhoCasa * z;

        var instantiate = Instantiate(peca, posicao, rotacao);
        instantiate.transform.SetParent(transform);

        pecas[x, z] = instantiate.GetComponent<PecaXadrez>();
    }
}
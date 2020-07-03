using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/**
 * Script responsável pelo jogo (tabuleiro)
 */
public class Tabuleiro : MonoBehaviour {
    private static readonly Quaternion RotacaoBranco = Quaternion.Euler(0, -90, 0);
    private static readonly Quaternion RotacaoPreto = Quaternion.Euler(0, 90, 0);
    private Camera _camera;

    private bool _respondendo;
    private bool _chequeBranco;
    private bool _chequePreto;
    private Marcador _marcador;
    private Peca _pecaSelecionada;
    private Peca _pecaComer;
    private bool _vezBranco = true;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private PerguntaManager _perguntaManager;

    public Peca[,] pecas;
    public GameObject bispoBranco;
    public GameObject bispoPreto;
    public GameObject cavaloBranco;
    public GameObject cavaloPreto;
    public GameObject peaoBranco;
    public GameObject peaoPreto;
    public GameObject rainhaBranco;
    public GameObject rainhaPreto;
    public GameObject reiBranco;
    public GameObject reiPreto;
    public GameObject torreBranco;
    public GameObject torrePreto;
    public float tamanhoCasa;

    public Text textPlayer;
    public Text textChequeMate;
    public Text textPontuacao;

    /**
     * Inicia o marcador e a camera (para buscar o ponto clicado em tela)
     * Cria as peças no tabuleiro
     */
    public void Start() {
        _marcador = gameObject.GetComponent<Marcador>();
        _camera = Camera.main;
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _perguntaManager = FindObjectOfType<PerguntaManager>();

        CriarPecas();
        AtualizaTextPontuacao();
        textChequeMate.text = "";
    }

    /**
     * Busca a posição do mouse e marca a peça se existir
     * Se tiver clicado seleciona ou move a peça
     */
    public void Update() {
        
        if (_gameManager.IsPausado()) return;
        if (_perguntaManager.IsRespondendo()) return;

        if (_respondendo) {
            var acertou = _perguntaManager.IsRespondeuCerto();
            ComerPeca(acertou);
            _respondendo = false;
        }

        var pontos = GetSelecaoMouse();
        var x = pontos[0];
        var z = pontos[1];

        if (!_pecaSelecionada) _marcador.DesmarcarPecas();
        if (!Utils.IsValidPosition(x, z)) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (!_pecaSelecionada)
                SelecionarPeca(x, z);
            else
                MoverPeca(x, z);
        } else if (!_pecaSelecionada) {
            MarcarPeca(x, z);
        }
    }

    /**
     * Retorna a posição do mouse em relação ao tabuleiro
     */
    private int[] GetSelecaoMouse() {
        var x = -1;
        var z = -1;

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, 150f,
            LayerMask.GetMask("Tabuleiro"))) {
            x = (int) (hit.point.x + 0.4f);
            z = (int) (hit.point.z + 0.4f);
        }

        return new[] {x, z};
    }

    /**
     * Busca e seleciona uma peca na posicao informada,
     * somente seleciona se a peça for da equipe do jogador e se tiver movimentos
     */
    private void SelecionarPeca(int x, int y) {
        _pecaSelecionada = pecas[x, y];

        if (!_pecaSelecionada) return;

        if (_pecaSelecionada.isBranca != _vezBranco || !PermiteMoverPeca(_pecaSelecionada)) {
            _pecaSelecionada = null;
            return;
        }

        _marcador.MarcarPeca(_pecaSelecionada);
    }

    /**
     * Move a peça selecionada para a posição informada se existir movimento
     */
    private void MoverPeca(int x, int z) {
        var movimento = _pecaSelecionada.GetMovimentos().FirstOrDefault(mv => mv.X == x && mv.Z == z);
        if (movimento != null) {
            var pecaAdversaria = pecas[x, z];
            if (pecaAdversaria && pecaAdversaria.isBranca != _vezBranco) { // Comeu peca
                _pecaComer = pecaAdversaria;
                _respondendo = true;
                _perguntaManager.Perguntar();
                return;
            } 
                

            var oldX = _pecaSelecionada.GetX();
            var oldZ = _pecaSelecionada.GetZ();
            _pecaSelecionada.transform.position = GetPosicao(x, z);
            _pecaSelecionada.isMovimentou = true;

            pecas[x, z] = _pecaSelecionada;
            pecas[oldX, oldZ] = null;
            ProcessaCheque(x, z);
            TrocarJogador();
        }

        _pecaSelecionada = null;
        _marcador.DesmarcarPecas();
    }

    private void ComerPeca(bool acerto) {
        var peca = acerto ? _pecaSelecionada : _pecaComer;
        var pecaAdversaria = acerto ? _pecaComer : _pecaSelecionada;
        
        Destroy(pecaAdversaria.gameObject);
        if (acerto) {
            _audioManager.PlayComeuPeca();
        } else if (!pecaAdversaria.IsRei()) {
            _audioManager.PlayGameOver();
        }
        if (pecaAdversaria.IsRei()) {
            _gameManager.GameOver(acerto ? _vezBranco : !_vezBranco);
        }

        var newX = pecaAdversaria.GetX();
        var newZ = pecaAdversaria.GetZ();
        var oldX = peca.GetX();
        var oldZ = peca.GetZ();
        peca.transform.position = GetPosicao(newX, newZ);
        peca.isMovimentou = true;

        pecas[newX, newZ] = peca;
        pecas[oldX, oldZ] = null;
        ProcessaCheque(newX, newZ);
        TrocarJogador();

        _pecaComer = null;
        _pecaSelecionada = null;
        _marcador.DesmarcarPecas();
    }

    /**
     * Marca peça na posição informada se existir peça e se possuir movimentos
     */
    private void MarcarPeca(int x, int y) {
        var peca = pecas[x, y];

        if (!peca || peca.isBranca != _vezBranco || !PermiteMoverPeca(peca)) {
            return;
        }

        _marcador.MarcarPeca(peca);
    }

    /**
     * Troca o time do jogador e valida se entrou em cheque ou GameOver
     */
    private void TrocarJogador() {
        _vezBranco = !_vezBranco;
        textPlayer.text = $"Jogador: {(_vezBranco ? "Branco" : "Preto")}";
        AtualizaTextPontuacao();
        foreach (var peca in pecas)
            if (peca)
                PermiteMoverPeca(peca); // Verifica GameOver
    }

    /**
     * Processa cheque, verifica se o rei ficou na mira de alguma peça inimiga
     */
    private void ProcessaCheque(int x, int z) {
        textChequeMate.text = "";
        _chequeBranco = false;
        _chequePreto = false;

        var pecaMovida = pecas[x, z];
        pecaMovida.GetMovimentos().ForEach(movimento => {
            var peca = pecas[movimento.X, movimento.Z];
            if (peca && peca.IsRei() && peca.isBranca != _vezBranco) Cheque();
        });
    }

    /**
     * Verifica se a peça informada tem movimentos,
     * se estiver em cheque só permite mover se a peça for rei,
     * caso seja rei e não possua movimentos válidos -> GameOver
     */
    private bool PermiteMoverPeca(Peca peca) {
        if (_chequeBranco && peca.isBranca) {
            if (peca.IsRei()) {
                if (peca.GetMovimentos().Count > 0) return true;
                _gameManager.GameOver(!_vezBranco);
                return false;
            }

            return false;
        }

        if (_chequePreto && !peca.isBranca) {
            if (peca.IsRei()) {
                if (peca.GetMovimentos().Count > 0) return true;
                _gameManager.GameOver(!_vezBranco);
                return false;
            }

            return false;
        }

        return peca.GetMovimentos().Count > 0;
    }

    /**
     * Cria as peças no tabuleiro
     */
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

    /**
     * Cria uma peça na posição informada
     */
    private void CriaPeca(int x, int z, GameObject peca, Quaternion rotacao) {
        var instantiate = Instantiate(peca, GetPosicao(x, z), rotacao);
        instantiate.transform.SetParent(transform);

        pecas[x, z] = instantiate.GetComponent<Peca>();
    }

    /**
     * Retorna Vector3 para uma posição informada
     */
    private Vector3 GetPosicao(int x, int z) {
        var posicao = Vector3.zero;
        posicao.x += tamanhoCasa * x;
        posicao.z += tamanhoCasa * z;
        return posicao;
    }

    /**
     * Método que marca como Cheque
     */
    private void Cheque() {
        if (_vezBranco)
            _chequePreto = true;
        else
            _chequeBranco = true;

        textChequeMate.text = $"Jogador em cheque!";
    }

    /**
     * Método que atualiza o texto de pontuação
     */
    private void AtualizaTextPontuacao() {
        var brancas = 0;
        var pretas = 0;
        foreach (var peca in pecas) {
            if (peca) {
                if (peca.isBranca) {
                    brancas++;
                } else {
                    pretas++;
                }
            }
        }

        textPontuacao.text = $"Branco:\t{brancas}\r\nPreto:\t{pretas}";
    }
}
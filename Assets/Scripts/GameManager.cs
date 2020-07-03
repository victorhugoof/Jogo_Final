using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject panel;
    public Text panelTitle;
    public Text panelDescription;

    private bool _isGameOver;
    private AudioManager _audioManager;

    void Start() {
        this._audioManager = FindObjectOfType<AudioManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isGameOver) return;

            panelTitle.text = "Jogo Pausado";
            panelDescription.text = "";
            panel.SetActive(!panel.activeInHierarchy);
        }
    }

    public void GameOver(bool isBrancoVenceu) {
        Time.timeScale = 0f;
        _isGameOver = true;

        panelTitle.text = "Game-over";
        panelDescription.text = $"Jogador {(isBrancoVenceu ? "branco" : "preto")} venceu!";
        panel.SetActive(true);
        
        _audioManager.PlayGameOver();
    }

    public bool IsPausado() {
        return panel.activeInHierarchy;
    }

    public void ReiniciarJogo() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SairJogo() {
        Application.Quit();
    }
}
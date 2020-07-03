using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PerguntaManager : MonoBehaviour {

    public GameObject perguntaPanel;
    public Text contadorTempo;
    public Text perguntaTitle;
    public List<Text> opcoes;
    public int timeOutPergunta;
    
    private Question _current;
    private bool _acertou;
    private float _timeout;

    private readonly List<Question> _perguntas = new List<Question> {
        new Question("Quanto é a raiz quadrada de 9?", "3", "9", "81", "71"),
        new Question("Quanto é 8^2?", "64", "8", "16", "63"),
        new Question("Quanto é 4^2?", "16", "4", "12", "2"),
        new Question("Quanto é 2^3?", "2 * 2 * 2", "6", "2 * 3", "28"),
        new Question("Quantos minutos têm em 3 horas?", "180 minutos", "160 minutos", "120 minutos", "300 minutos"),
        new Question("Em que ano ocorreu o desastre do 11 de setembro?", "2001", "2000", "2002", "2003"),
        new Question("Quem foi o responsável pelo desenvolvimento deste jogo?", "Victor Hugo M. Fernandes", "Beltrano", "Cicrano", "Fulano"),
    };

    void Update() {
        if (IsRespondendo()) {
            _timeout -= Time.deltaTime;
            contadorTempo.text = ((int) _timeout) + "";
            if (_timeout < 0) {
                ResponderTimeOut();
            }
        }
    }

    public void Perguntar() {
        _timeout = timeOutPergunta;
        var random = new Random();
        _current = _perguntas[random.Next(_perguntas.Count)];

        var processadas = new List<int>();
        for (var i = 0; i < 4; i++) {
            var index = GetRandomIndex(processadas, 4);
            processadas.Add(index);
            opcoes[i].text = GetResposta(index);
        }

        perguntaTitle.text = _current.Pergunta;
        contadorTempo.text = _timeout + "";
        perguntaPanel.SetActive(true);
    }

    public void Responder(Text opcao) {
        _acertou = opcao.text == _current.Resposta;
        _current = null;
        perguntaPanel.SetActive(false);
    }

    private void ResponderTimeOut() {
        _acertou = false;
        _current = null;
        perguntaPanel.SetActive(false);
    }

    public bool IsRespondendo() {
        return perguntaPanel.activeInHierarchy;
    }

    public bool IsRespondeuCerto() {
        return _acertou;
    }

    private static int GetRandomIndex(List<int> processados, int size) {
        var index = new Random().Next(size);
        return processados.Contains(index) ? GetRandomIndex(processados, size) : index;
    }

    private string GetResposta(int index) {
        switch (index) {
            case 0: return _current.Resposta;
            case 1: return _current.Errada1;
            case 2: return _current.Errada2;
            default: return _current.Errada3;
        }
    }
}
public class Question {
    public readonly string Pergunta;
    public readonly string Resposta;
    public readonly string Errada1;
    public readonly string Errada2;
    public readonly string Errada3;

    public Question(string pergunta, string resposta, string errada1, string errada2, string errada3) {
        Pergunta = pergunta;
        Resposta = resposta;
        Errada1 = errada1;
        Errada2 = errada2;
        Errada3 = errada3;
    }
}
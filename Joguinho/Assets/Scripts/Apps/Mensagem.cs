using UnityEngine;

[System.Serializable]
public class Mensagem
{
    public enum MomentoDia { Manha, Tarde, Noite }

    public MomentoDia momento;
    public string textoPergunta;
    public string[] opcoesResposta = new string[2];
    public string[] respostasPersonagem = new string[2];
    public bool jaFoiRespondida = false;
}

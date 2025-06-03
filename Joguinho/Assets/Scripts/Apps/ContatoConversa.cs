using UnityEngine;

[CreateAssetMenu(fileName = "NovoContato", menuName = "Mensagens/Contato")]
public class ContatoConversa : ScriptableObject
{
    public string nomeContato;
    public Mensagem[] mensagens;
}

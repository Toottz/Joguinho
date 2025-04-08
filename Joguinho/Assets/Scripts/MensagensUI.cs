using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MensagensUI : MonoBehaviour
{
    [System.Serializable]
    public class Conversa
    {
        public string nome;
        public List<string> mensagens;
        public List<string> respostas;
    }

    public List<Conversa> conversas;
    public TextMeshProUGUI nomeContatoText;
    public TextMeshProUGUI mensagensText;
    public TMP_Dropdown respostasDropdown;
    public GameObject listaContatos;
    public GameObject painelConversa;

    private int conversaAtual = -1;

    public void AbrirConversa(int index)
    {
        if (index < 0 || index >= conversas.Count) return;

        conversaAtual = index;

        listaContatos.SetActive(false);
        painelConversa.SetActive(true);

        AtualizarConversa();
    }

    void AtualizarConversa()
    {
        var conversa = conversas[conversaAtual];
        nomeContatoText.text = conversa.nome;
        mensagensText.text = string.Join("\n", conversa.mensagens);

        respostasDropdown.ClearOptions();
        respostasDropdown.AddOptions(conversa.respostas);
    }

    public void EnviarResposta()
    {
        var conversa = conversas[conversaAtual];
        string respostaSelecionada = respostasDropdown.options[respostasDropdown.value].text;

        // Simula envio de resposta
        conversa.mensagens.Add("Você: " + respostaSelecionada);
        AtualizarConversa();
    }

    public void VoltarParaLista()
    {
        painelConversa.SetActive(false);
        listaContatos.SetActive(true);
    }
}

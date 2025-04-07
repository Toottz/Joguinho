using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversaSimples : MonoBehaviour
{
    public TextMeshProUGUI mensagemText;
    public List<string> mensagens;
    public float delayEntreMensagens = 1.5f;

    private void OnEnable()
    {
        StartCoroutine(MostrarMensagens());
    }

    IEnumerator MostrarMensagens()
    {
        mensagemText.text = "";
        foreach (string msg in mensagens)
        {
            mensagemText.text += msg + "\n";
            yield return new WaitForSeconds(delayEntreMensagens);
        }
    }
}

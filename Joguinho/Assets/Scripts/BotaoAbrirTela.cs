using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoAbrirTela : MonoBehaviour
{
    [Header("Tela que será desativada (opcional)")]
    public GameObject telaAtual;
    [Header("Tela que será ativada")]
    public GameObject telaNova;

    public void AbrirTela()
    {
        if (telaAtual != null)
            telaAtual.SetActive(false);

        if (telaNova != null)
            telaNova.SetActive(true);
    }
}
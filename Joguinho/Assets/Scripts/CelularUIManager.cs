using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelularUIManager : MonoBehaviour
{
    public GameObject menuApps;
    public GameObject telaMensagens;
    public GameObject celularImagem; // <- A imagem do celular (bordas, etc)

    public void AbrirMensagens()
    {
        menuApps.SetActive(false);         // Esconde menu de apps
        celularImagem.SetActive(false);    // Esconde a borda do celular (se tiver)
        telaMensagens.SetActive(true);     // Mostra só a tela do app
    }

    public void VoltarAoMenu()
    {
        telaMensagens.SetActive(false);    // Esconde o app
        celularImagem.SetActive(true);     // Mostra a carcaça do celular de novo
        menuApps.SetActive(true);          // Volta para o menu de apps
    }
}
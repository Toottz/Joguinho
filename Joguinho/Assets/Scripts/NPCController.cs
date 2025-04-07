using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public string nomeNPC;

    [TextArea]
    public string mensagemErrada = "Essa carta não é pra mim!";

    [Header("Diálogo quando recebe a carta correta")]
    public List<Fala> dialogoCompleto = new List<Fala>();
}
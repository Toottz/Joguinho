using UnityEngine;
using TMPro;

public class TutorialMensagemManager : MonoBehaviour
{
public TextMeshProUGUI textoTutorial;
public void OcultarMensagem()
{
    if (textoTutorial != null)
        textoTutorial.gameObject.SetActive(false);
}
}
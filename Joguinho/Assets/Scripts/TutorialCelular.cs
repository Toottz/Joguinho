using UnityEngine;
using UnityEngine.UI;

public class TutorialCelular : MonoBehaviour
{
    public GameObject textoTutorial;

    private bool mostrado = true;

    void Update()
    {
        if (mostrado && Input.GetKeyDown(KeyCode.Tab))
        {
            if (textoTutorial != null)
                textoTutorial.SetActive(false);

            mostrado = false;
        }
    }
}

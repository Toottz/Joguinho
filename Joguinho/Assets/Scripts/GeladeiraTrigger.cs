using UnityEngine;
using UnityEngine.UI;

public class GeladeiraTrigger : MonoBehaviour
{
    public GameObject mensagemUI;
    public GameObject telaPiscar;

    public float duracaoPiscar = 0.2f;
    private bool jogadorPerto = false;
    private bool comprasGuardadas = false;

    void Start()
    {
        if (mensagemUI != null)
            mensagemUI.SetActive(false);

        if (telaPiscar != null)
            telaPiscar.SetActive(false);
    }

    void Update()
    {
        if (jogadorPerto && !comprasGuardadas && Input.GetKeyDown(KeyCode.E))
        {
            GuardarCompras();
        }
    }

    void GuardarCompras()
    {
        comprasGuardadas = true;
        if (mensagemUI != null)
            mensagemUI.SetActive(false);

        if (telaPiscar != null)
            StartCoroutine(PiscarTela());

        Debug.Log("🍽️ Compras guardadas na geladeira!");
    }

    System.Collections.IEnumerator PiscarTela()
    {
        telaPiscar.SetActive(true);
        yield return new WaitForSeconds(duracaoPiscar);
        telaPiscar.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !comprasGuardadas)
        {
            jogadorPerto = true;
            if (mensagemUI != null)
                mensagemUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            if (mensagemUI != null)
                mensagemUI.SetActive(false);
        }
    }
}

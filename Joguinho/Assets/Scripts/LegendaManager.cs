using UnityEngine;
using TMPro;

public class LegendaManager : MonoBehaviour
{
    public static LegendaManager Instance;

    public GameObject painelLegenda; // Objeto com fundo transparente
    public TextMeshProUGUI legendaTexto;
    public float tempoPadrao = 3f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (painelLegenda != null)
            painelLegenda.SetActive(false);
    }

    public void MostrarLegenda(string texto, float tempo = -1f)
    {
        if (painelLegenda == null || legendaTexto == null) return;

        legendaTexto.text = texto;
        painelLegenda.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(EsconderDepoisDeSegundos(tempo > 0 ? tempo : tempoPadrao));
    }

    private System.Collections.IEnumerator EsconderDepoisDeSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        if (painelLegenda != null)
            painelLegenda.SetActive(false);
    }
}

using UnityEngine;
using TMPro;

public class LocalLegendaTrigger : MonoBehaviour
{
    [TextArea]
    public string textoLegenda;
    public float tempoLegenda = 3f;
    private bool jaMostrou = false;

    private void OnTriggerEnter(Collider other)
    {
        if (jaMostrou) return;

        if (other.CompareTag("Player"))
        {
            LegendaManager.Instance?.MostrarLegenda(textoLegenda, tempoLegenda);
            jaMostrou = true;
        }
    }
}

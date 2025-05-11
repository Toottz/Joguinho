using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuveiroTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("Player"))
            {
            ChuveiroManager.Instance.MostrarOpcoesChuveiro();
            }
        }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChuveiroManager.Instance.FecharOpcoes();
        }
    }
}
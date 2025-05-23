using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigandoCollider : MonoBehaviour
{
    public Collider myCollider;
    private bool hasExited = false; // Controla se j� saiu uma vez

    void Start()
    {
        myCollider = GetComponent<Collider>(); // Pega o Collider do objeto
        myCollider.isTrigger = true; // Garante que come�a como trigger
    }

    void OnTriggerExit(Collider other)
    {
        if (!hasExited && other.CompareTag("Player")) // Verifica se � o Player (ajuste a tag conforme necess�rio)
        {
            hasExited = true; // Marca que j� saiu
            myCollider.isTrigger = false; // Transforma em collider normal
            Debug.Log("Collider agora � s�lido (n�o-trigger)");
        }
    }
}

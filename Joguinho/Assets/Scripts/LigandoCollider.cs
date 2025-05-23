using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigandoCollider : MonoBehaviour
{
    public Collider myCollider;
    private bool hasExited = false; // Controla se já saiu uma vez

    void Start()
    {
        myCollider = GetComponent<Collider>(); // Pega o Collider do objeto
        myCollider.isTrigger = true; // Garante que começa como trigger
    }

    void OnTriggerExit(Collider other)
    {
        if (!hasExited && other.CompareTag("Player")) // Verifica se é o Player (ajuste a tag conforme necessário)
        {
            hasExited = true; // Marca que já saiu
            myCollider.isTrigger = false; // Transforma em collider normal
            Debug.Log("Collider agora é sólido (não-trigger)");
        }
    }
}

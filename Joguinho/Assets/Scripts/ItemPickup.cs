using UnityEngine;

public class ItemPickup : MonoBehaviour
{
public string tipoItem = "Racao";
public bool foiColetado = false;
public void Pegar()
{
    if (foiColetado) return;

    foiColetado = true;
    Debug.Log("Item coletado: " + tipoItem);
    gameObject.SetActive(false);
}
}
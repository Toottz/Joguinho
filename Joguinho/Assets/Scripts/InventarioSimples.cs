using System.Collections.Generic;
using UnityEngine;

public class InventarioSimples : MonoBehaviour
{
public static InventarioSimples Instance;
private List<string> itens = new List<string>();

void Awake()
{
    if (Instance == null)
        Instance = this;
    else
        Destroy(gameObject);
}

public void AdicionarItem(string id)
{
    itens.Add(id);
    Debug.Log("Item adicionado ao inventário: " + id);
}

public bool TemItem(string id)
{
    return itens.Contains(id);
}

public void RemoverItem(string id)
{
    if (itens.Contains(id))
    {
        itens.Remove(id);
        Debug.Log("Item removido do inventário: " + id);
    }
}
}
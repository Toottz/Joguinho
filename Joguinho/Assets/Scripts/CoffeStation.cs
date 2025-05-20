using UnityEngine;

public class CoffeeStation : MonoBehaviour
{
    public GameObject objetoFinalCafe;
    private int ingredientesEntregues = 0;
    private int totalIngredientes = 3;

    public void TentarReceberIngrediente(ItemPickup item)
    {
        if (item == null) return;

        ingredientesEntregues++;
        Destroy(item.gameObject);

        if (ingredientesEntregues >= totalIngredientes && objetoFinalCafe != null)
        {
            objetoFinalCafe.SetActive(true);
            Debug.Log("Café pronto!");
        }
    }
}

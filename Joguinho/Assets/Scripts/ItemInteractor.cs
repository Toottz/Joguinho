using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
public float distanciaInteracao = 3f;
public KeyCode teclaInteracao = KeyCode.E;
public Camera cameraPrincipal;
private void Start()
{
    if (cameraPrincipal == null)
        cameraPrincipal = Camera.main;
}

void Update()
{
    if (Input.GetKeyDown(teclaInteracao))
    {
        Ray ray = new Ray(cameraPrincipal.transform.position, cameraPrincipal.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteracao))
        {
            ItemPickup item = hit.collider.GetComponent<ItemPickup>();
            if (item != null)
            {
                item.Pegar();
            }
        }
    }
}
}
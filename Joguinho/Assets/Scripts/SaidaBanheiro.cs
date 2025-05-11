using UnityEngine;

public class SaidaBanheiro : MonoBehaviour
{
public AudioSource somBarriga;
private bool jaTocou = false;
void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player") && !jaTocou)
    {
        if (ChuveiroManager.Instance != null && ChuveiroManager.Instance.tomouBanho)
        {
            if (somBarriga != null)
                somBarriga.Play();

            jaTocou = true;
        }
    }
}
}
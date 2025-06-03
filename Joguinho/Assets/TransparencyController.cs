using UnityEngine;
using UnityEngine.UI;

public class TransparencyController : MonoBehaviour
{
    public Image targetImage; // Arraste a Image do Inspector para cá

    public void OnSliderChanged(float value)
    {
        // Altera a transparência (value vai de 0 a 1)
        Color cor = targetImage.color;
        cor.a = value; // 'a' = alpha (transparência)
        targetImage.color = cor;
    }
}

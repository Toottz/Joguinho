using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Vibracao : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duração de cada shake
    public Vector3 shakeStrength;  // Intensidade (em graus)
    public int vibrato = 5;           // Quantidade de oscilações
    public float randomness = 30f;     // Aleatoriedade
    public float intervalos = 2f;

    void Start()
    {
        StartShakeCycle();
    }

    private void StartShakeCycle()
    {
        // Shake infinito (repete a cada shakeDuration + delay)
        transform.DOShakeRotation(shakeDuration, shakeStrength, vibrato, randomness)
            .OnComplete(() => {
                DOVirtual.DelayedCall(intervalos, StartShakeCycle); // Repete após 1s
            });
    }

    void OnDestroy()
    {
        // IMPORTANTE: Mata o tweener quando o objeto é destruído
        transform.DOKill();
    }
}

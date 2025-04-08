using UnityEngine;

[System.Serializable]
public class Fala
{
    public enum TipoDeFala
    {
        NPC,
        Jogador
    }

    public TipoDeFala quemFala;

    [TextArea(2, 5)]
    public string texto;
}

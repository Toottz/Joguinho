using UnityEngine;
using TMPro;

public class BlocoDeNotasApp : MonoBehaviour
{
        [TextArea(5, 20)]
        public string objetivosTexto = 
    @"- Tomar banho
    - Escovar os dentes
    - Alimentar o passarinho
    - Fazer café
    - Ir até o mercado";

        public TextMeshProUGUI campoTexto;

        void OnEnable()
        {
            if (campoTexto != null)
                campoTexto.text = objetivosTexto;
        }
}

using UnityEngine;

public class VisionEffectController : MonoBehaviour
{
    public CelularController celular; 
    public Material visaoMaterial;    
    public string intensidadeProp = "_Opacidade"; 

    public float tempoParaAtivar = 5f;
    public float tempoParaSumir = 3f;
    public float intensidadeMax = 1f;

    private float tempo = 0f;
    private float intensidadeAtual = 0f;

    void Start()
    {
        intensidadeAtual = 0f;
        visaoMaterial.SetFloat(intensidadeProp, 0f); 
    }

    void Update()
    {
        if (celular.CelularEstaAberto())
        {
            tempo += Time.deltaTime;
            if (tempo >= tempoParaAtivar)
            {
                intensidadeAtual = Mathf.MoveTowards(intensidadeAtual, intensidadeMax, Time.deltaTime);
            }
        }
        else
        {
            tempo = 0f;
            intensidadeAtual = Mathf.MoveTowards(intensidadeAtual, 0f, Time.deltaTime / tempoParaSumir);
        }

        visaoMaterial.SetFloat(intensidadeProp, intensidadeAtual);
    }
}

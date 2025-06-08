using UnityEngine;

public class Setas : MonoBehaviour
{
    [Header("Objetos da Sequência")]
    public GameObject objeto1;
    public GameObject objeto2;
    public GameObject objeto3;
    public GameObject objeto4;
    public GameObject objeto5;

    [Header("Ativadores (GameObjects)")]
    public GameObject ativador2;
    public GameObject ativador3;
    public GameObject ativador4;
    public GameObject desativador5;

    [Header("Configuração do Item")]
    public string itemAtivar5 = "id_item_especial";

    private bool objeto5JaFoiAtivado = false;
    [SerializeField]
    private bool desativador5JaFoiUsado = false; // Nova flag

    private bool objeto2jaligado = false;
    private bool objeto3jaligado = false;
    private bool objeto4jaligado = false;
    private bool objeto5jaligado = false;   

    void Update()
    {
        if (ativador2.activeSelf && ativador3.activeSelf && ativador4.activeSelf && desativador5.activeSelf)
        {
            objeto5.SetActive(false);
            desativador5JaFoiUsado=true;
            objeto4.SetActive(false);
            Debug.Log("OBJETO 5 DESLIGANDO");
        }

        if (ativador2.activeSelf && !ativador3.activeSelf && !objeto2jaligado)
        {
            AvancarPara(objeto2);
            Debug.Log("OBJETO 2 LIGANDO");
        }
        if (ativador2.activeSelf && ativador3.activeSelf && !ativador4.activeSelf && !objeto3jaligado)
        {
            AvancarPara(objeto3);
            Debug.Log("OBJETO 3 LIGANDO");
        }
        if (ativador2.activeSelf && ativador3.activeSelf && ativador4.activeSelf && !InventarioSimples.Instance.TemItem(itemAtivar5) && !objeto4jaligado)
        {
            AvancarPara(objeto4);
            Debug.Log("OBJETO 4 LIGANDO");
        }
        if (ativador2.activeSelf && ativador3.activeSelf && ativador4.activeSelf && InventarioSimples.Instance.TemItem(itemAtivar5) && !desativador5.activeSelf && !objeto5jaligado)
        {
            AvancarPara(objeto5);
            Debug.Log("OBJETO 5 LIGANDO");
        }
        if (objeto2.activeSelf)
        {
            objeto2jaligado = true;
        }
        if (objeto3.activeSelf)
        {
            objeto3jaligado = true;
        }
        if (objeto4.activeSelf)
        {
            objeto4jaligado = true;
        }
        if (objeto5.activeSelf)
        {
            objeto5jaligado = true;
        }
    }

    void AvancarPara(GameObject proximoObjeto)
    {
        objeto1.SetActive(false);
        objeto2.SetActive(false);
        objeto3.SetActive(false);
        objeto4.SetActive(false);
        objeto5.SetActive(false);

        proximoObjeto.SetActive(true);

    }

}
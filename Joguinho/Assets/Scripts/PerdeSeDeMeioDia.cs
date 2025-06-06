using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PerdeSeDeMeioDia : MonoBehaviour
{
    public float hora;
    public float minuto;
    public string nomeCenaDestino;
    public Image fade;
    public TextMeshProUGUI texto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        float currentHour = Estatico.Hora; 
        float currentMinuto = Estatico.Minutos;

        if (Mathf.FloorToInt(currentMinuto) == Mathf.FloorToInt(minuto))
            {
                if (Mathf.FloorToInt(currentHour) == Mathf.FloorToInt(hora))
                {
                      
                      SceneManager.LoadScene(nomeCenaDestino);
                }
            }
        }
    }


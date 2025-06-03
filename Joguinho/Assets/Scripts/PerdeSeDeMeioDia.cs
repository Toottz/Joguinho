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
    public float velocidadefade = 0.2f;
    public bool escureca;
    public float alpha = 0f;

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
                escureca = true;

            }

        }

        if(escureca)
        {
            alpha += Time.deltaTime * velocidadefade;
            var cor = fade.color;
            cor.a = alpha;
            fade.color = cor;
            if (alpha >= 1f)
            {
                escureca=false;
                SceneManager.LoadScene(nomeCenaDestino);
            }
        }
    }
    }


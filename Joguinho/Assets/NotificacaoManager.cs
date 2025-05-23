using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificacaoManager : MonoBehaviour
{
    public NotificacaoForaCelular notificacao;
    [TextArea]
    public string[] dailyMessages; // Mensagens para cada horário
    public float[] triggerHours;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentHour = TimeManager.HoraAtual;

        for (int i = 0; i < triggerHours.Length; i++)
        {
            if (Mathf.FloorToInt(currentHour) == Mathf.FloorToInt(triggerHours[i]))
            {
                notificacao.ShowNotification(dailyMessages[i]);
            }
        }
    }
}

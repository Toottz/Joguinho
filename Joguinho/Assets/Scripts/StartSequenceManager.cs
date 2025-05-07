using UnityEngine;

public class StartSequenceManager : MonoBehaviour
{
    public GameObject player; // Referência ao jogador
    public FirstPersonMovement movementScript;
    public FirstPersonLook lookScript;
    public GameObject phoneUI;
    public AlarmManager alarmManager; // Gerencia o alarme
    
    private bool isStartSequence = true;
    private bool phoneOpen = false;

    public Transform cameraTransform;
    public Vector3 initialRotation = new Vector3(60, 0, 0); // Olhar para baixo
    public Vector3 targetRotation = new Vector3(0, 0, 0);   // Olhar normal
    public float rotationDuration = 2f;

    private float rotationTimer = 0f;
    private bool rotating = true;


    void Start()
    {
        // No início, o player não pode se mover
        movementScript.enabled = false;
        lookScript.SetLookEnabled(true); // Apenas olhar com o mouse
        cameraTransform.localEulerAngles = initialRotation;
        phoneUI.SetActive(false);
        

        alarmManager.PlayAlarm(); // Começa a tocar o alarme
    }

    void Update()
    {
        if (rotating)
    {
        rotationTimer += Time.deltaTime;
        float t = Mathf.Clamp01(rotationTimer / rotationDuration);
        cameraTransform.localEulerAngles = Vector3.Lerp(initialRotation, targetRotation, t);

        if (t >= 1f)
            rotating = false;
    }

        if (!isStartSequence) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            phoneOpen = !phoneOpen;
            phoneUI.SetActive(phoneOpen);

            if (phoneOpen)
            {
                alarmManager.StopAlarm(); // Alarme desliga ao abrir o celular
                lookScript.SetLookEnabled(true); // Continua podendo olhar
            }
            else
            {
                // Guarda o celular e ativa movimentação
                movementScript.enabled = true;
                isStartSequence = false; // Finaliza sequência inicial
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    public GameObject Cell;

    private Rigidbody rigidbody;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    // Referência opcional ao FirstPersonLook, para bloquear rotação junto com movimento
    public FirstPersonLook cameraLookScript;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        // Se não estiver atribuído no Inspector, tenta encontrar
        if (cameraLookScript == null)
            cameraLookScript = FindObjectOfType<FirstPersonLook>();
    }

    void FixedUpdate()
    {
        // Verifica se o celular está aberto
        bool celularAberto = Cell != null && Cell.activeSelf;

        // Atualiza bloqueio da rotação da câmera
        if (cameraLookScript != null)
            cameraLookScript.bloquearCamera = celularAberto;

        // Interrompe o movimento se o celular estiver aberto
        if (celularAberto)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
            return;
        }

        IsRunning = canRun && Input.GetKey(runningKey);
        float targetMovingSpeed = IsRunning ? runSpeed : speed;

        if (speedOverrides.Count > 0)
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}
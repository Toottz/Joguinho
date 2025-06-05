using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform character;
    public float sensitivity = 2f;
    public float smoothing = 1.5f;
    public bool bloquearCamera = false; // Se verdadeiro, bloqueia a rotação

    private Vector2 velocity;
    private Vector2 frameVelocity;

    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        if (!bloquearCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        if (bloquearCamera)
        {
            return; // Não gira a câmera se estiver bloqueado
        }
        // Se chegou aqui, pode girar e bloquear o cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1f / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public void ResetLook()
    {
        velocity = Vector2.zero;
        frameVelocity = Vector2.zero;
    }

    public void SetLookEnabled(bool state)
    {
        enabled = state;
    }
}
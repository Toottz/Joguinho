using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform character;
    public float sensitivity = 2f;
    public float smoothing = 1.5f;

    public bool bloquearCamera = false; // Permite bloquear rotação da câmera

    private Vector2 velocity;
    private Vector2 frameVelocity;

    void Reset()
    {
        // Pega o objeto de movimento do personagem automaticamente
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (bloquearCamera) return; // Bloqueia rotação da câmera se ativado

        // Coleta movimento do mouse e suaviza
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1f / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90f, 90f);

        // Rotaciona câmera (vertical) e personagem (horizontal)
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public void ResetLook()
    {
        velocity = Vector2.zero;
        frameVelocity = Vector2.zero;
    }
}

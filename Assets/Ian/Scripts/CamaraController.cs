using UnityEngine;
using UnityEngine.InputSystem; // <- Asegúrate de que esta línea esté presente

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    void Update()
    {
        Vector2 moveInput = Vector2.zero;

        // Comprobamos si el teclado actual existe y leemos las teclas WASD o Flechas
        if (Keyboard.current != null)
        {
            float horizontal = 0f;
            float vertical = 0f;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal = 1f;

            moveInput = new Vector2(horizontal, vertical).normalized;
        }

        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}

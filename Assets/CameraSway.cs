using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float swayAmount = 1f;      // Maximum sway amount
    public float swaySpeed = 1f;       // Speed of the sway
    public float resetSpeed = 2f;      // How quickly the sway resets when stopping

    private Vector3 originalRotation;

    void Start()
    {
        originalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Only sway when the player is moving
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            float swayX = Mathf.Lerp(0, horizontal * swayAmount, Time.deltaTime * swaySpeed);
            float swayY = Mathf.Lerp(0, vertical * swayAmount, Time.deltaTime * swaySpeed);

            // Apply the sway
            transform.localEulerAngles = new Vector3(originalRotation.x + swayY, originalRotation.y + swayX, originalRotation.z);
        }
        else
        {
            // Reset camera rotation smoothly
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, originalRotation, Time.deltaTime * resetSpeed);
        }
    }
}

using UnityEngine;

public class LanderLegsRight : MonoBehaviour
{
    public bool isGrounded { get; private set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Pad pad))
        {
            isGrounded = true;
            Debug.Log("Right " + isGrounded);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Pad pad))
        {
            isGrounded = false;
            Debug.Log("Right " + isGrounded);
        }
    }
}

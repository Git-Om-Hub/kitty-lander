using UnityEngine;
using UnityEngine.InputSystem;

public class Kitty : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private StateMachine state;

    private float upSpeed = 700f;
    private float rotationForce = 1.5f;
    private enum StateMachine
    {
        normal,
        gameOver,
    }

    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;
    [SerializeField] private Sprite happy;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            default:
                sr.sprite = idle;

                if (Keyboard.current.wKey.isPressed)
                {

                    rb.AddForce(upSpeed * transform.up * Time.deltaTime);
                }
                if (Keyboard.current.aKey.isPressed)
                {

                    sr.sprite = left;
                    rb.AddForce(500f * (-transform.right) * Time.deltaTime);
                    rb.AddTorque(rotationForce);

                }
                if (Keyboard.current.dKey.isPressed)
                {
                    sr.sprite = right;
                    rb.AddForce(500f * transform.right * Time.deltaTime);
                    rb.AddTorque(-rotationForce);

                }

                float currentRotation = rb.rotation;

                currentRotation = Mathf.Clamp(currentRotation, -5f, 5f);

                rb.rotation = currentRotation;
                break;
            case (StateMachine.gameOver):
                break;

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Landing only on landing Pad

        if (!collision.gameObject.TryGetComponent(out Pad pad))
        {
            Debug.Log("Crash");
            return;
        }

        float landingSpeed = collision.relativeVelocity.magnitude;
        float safeLanding = 3;
        if(landingSpeed > safeLanding)
        {
            Debug.Log("Crash");
            return;
        }

        sr.sprite = happy;
        state = StateMachine.gameOver;
        


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out CatFood fishFood))
        {
            fishFood.DestroySelf();
        }
        if (other.gameObject.TryGetComponent(out Fish fish)
)
        {
            fish.DestroySelf();
        }

    }

}

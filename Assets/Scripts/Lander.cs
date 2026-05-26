using UnityEngine;
using UnityEngine.InputSystem;

public class Kitty : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private StateMachine state;

    private float upSpeed = 700f;
    private float rotationForce = 1.5f;

    [SerializeField] private LanderLegs leftLeg;
    [SerializeField] private LanderLegsRight rightLeg;
    private bool landingCheck;
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

                currentRotation = Mathf.Clamp(currentRotation, -2f, 2f);

                rb.rotation = currentRotation;
                break;
            case (StateMachine.gameOver):
                break;

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == StateMachine.gameOver || landingCheck) return;

        if (collision.gameObject.TryGetComponent(out Pad pad))
        {
            // Wait exactly 0.05 seconds for BOTH legs to register contact before checking results
            Invoke(nameof(EvaluateLandingStatus), 0.15f);
        }
        else
        {
            Debug.Log("Hit an obstacle! Crash!");
            TriggerCrash();
        }
    }

    private void EvaluateLandingStatus()
    {
        // Safety check in case we crashed via obstacle during the delay window
        if (state == StateMachine.gameOver) return;

        float maxLandingSpeed = 1f;

        // Check if BOTH legs successfully stabilized on the pad during the buffer window
        if (leftLeg.isGrounded && rightLeg.isGrounded)
        {
            if (Mathf.Abs(rb.linearVelocity.y) <= maxLandingSpeed)
            {
                Debug.Log("Successful Landing!");
                landingCheck = true;
                state = StateMachine.gameOver;
                sr.sprite = happy;

                // Freeze the ship beautifully on the pad
                //rb.linearVelocity = Vector2.zero;
                //rb.angularVelocity = 0f;
                //rb.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                Debug.Log("Too fast! Crash!");
                TriggerCrash();
            }
        }
        else
        {
            // One leg missed or the main body slammed down single-sided
            Debug.Log("Body hit the pad before legs stabilized! Crash!");
            TriggerCrash();
        }
    }

    private void TriggerCrash()
    {
        state = StateMachine.gameOver;
        // Set your crash sprite or trigger explosion logic here
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

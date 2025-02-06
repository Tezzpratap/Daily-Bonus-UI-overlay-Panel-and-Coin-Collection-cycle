using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public Animator animator; // Reference to the Animator
    public float waveDuration = 3f; // Time to wave
    public float runDuration = 10f; // Time to run
    public float rotationSpeed = 5f; // Speed of character rotation

    private float timer = 0f;
    private bool isWaving = true; // Start with wave

    private Vector2 startTouchPosition; // Start position of the swipe
    private Vector2 currentTouchPosition; // Current touch position
    private bool isSwiping = false; // Is the user swiping?

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        Wave(); // Start with waving
    }

    private void Update()
    {
        HandleSwipeControls(); // Detect swipe gestures
        HandleAnimations();    // Switch between animations
    }

    private void HandleSwipeControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Start of the swipe
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    // Update current touch position
                    if (isSwiping)
                    {
                        currentTouchPosition = touch.position;

                        // Calculate swipe delta (difference)
                        float deltaX = currentTouchPosition.x - startTouchPosition.x;

                        // Rotate character horizontally
                        transform.Rotate(Vector3.up, -deltaX * rotationSpeed * Time.deltaTime);

                        // Update start position for smooth rotation
                        startTouchPosition = currentTouchPosition;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    // End of the swipe
                    isSwiping = false;
                    break;
            }
        }
    }

    private void HandleAnimations()
    {
        // Timer for animation switching
        timer += Time.deltaTime;

        if (isWaving && timer > waveDuration)
        {
            Run();
        }
        else if (!isWaving && timer > runDuration)
        {
            Wave();
        }
    }

    private void Wave()
    {
        isWaving = true;
        timer = 0f;
        animator.SetTrigger("Wave");
    }

    private void Run()
    {
        isWaving = false;
        timer = 0f;
        animator.SetTrigger("Run");
    }
}

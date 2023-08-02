using UnityEngine;

public class TransparentOnEnter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isPlayerInside = false;

    // Set this value in the Inspector to control the transparency amount
    public float transparencyAmount = 0.5f;
    
    // Set the duration of the transparency transition
    public float transitionDuration = 1.0f;
    
    // Store the target transparency color
    private Color targetTransparentColor;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Store the original color (without transparency) for restoring later
        originalColor = spriteRenderer.color;
        // Set the target transparency color
        targetTransparentColor = originalColor;
        targetTransparentColor.a = transparencyAmount;
    }

    void Update()
    {
        // Check if the player is inside the area
        if (isPlayerInside)
        {
            // Gradually transition to the target transparency color
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetTransparentColor, Time.deltaTime / transitionDuration);
        }
        else
        {
            // Gradually transition back to the original color (opaque)
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, Time.deltaTime / transitionDuration);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider has a Player component attached to it
        if (other.GetComponent<Player>() != null)
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting collider has a Player component attached to it
        if (other.GetComponent<Player>() != null)
        {
            isPlayerInside = false;
        }
    }
}

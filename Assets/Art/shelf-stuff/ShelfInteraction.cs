using UnityEngine;

public class ShelfInteraction : MonoBehaviour
{
    [SerializeField] private Animator shelfAnimator;

    private bool isPlayerInRange = false;
    // Track whether the door is currently open
    private bool isOpen = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.X))
        {
            if (isOpen)
            {
                // If door is open, trigger close animation
                shelfAnimator.SetTrigger("Close");
                isOpen = false;
            }
            else
            {
                // If door is closed, trigger open animation
                shelfAnimator.SetTrigger("Open");
                isOpen = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Show your "Press X" prompt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Hide your "Press X" prompt
        }
    }
}

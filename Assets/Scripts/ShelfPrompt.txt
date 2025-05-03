using System.Collections;
using UnityEngine;

public class ShelfPrompt : MonoBehaviour
{
    [SerializeField] private GameObject xPromptUI; // Your "Press X" UI
    [SerializeField] private float reappearDelay = 3f; // Time before UI reappears
    private bool isPlayerInRange = false;
    private bool isWaitingToShow = false;

    void Start()
    {
        if (xPromptUI != null)
        {
            xPromptUI.SetActive(false); // Hide by default
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWaitingToShow)
        {
            isPlayerInRange = true;
            if (xPromptUI != null)
                xPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (xPromptUI != null)
                xPromptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isWaitingToShow && Input.GetKeyDown(KeyCode.X))
        {
            if (xPromptUI != null)
                xPromptUI.SetActive(false);

            StartCoroutine(ReappearAfterDelay());
        }
    }

    private IEnumerator ReappearAfterDelay()
    {
        isWaitingToShow = true;
        yield return new WaitForSeconds(reappearDelay);

        if (isPlayerInRange && xPromptUI != null)
            xPromptUI.SetActive(true);

        isWaitingToShow = false;
    }
}

using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private Animator mAnimator;

    private bool isPlayerInrange = false;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(mAnimator != null)
        {
            if (isPlayerInrange && Input.GetKeyDown(KeyCode.X))
                mAnimator.SetTrigger("TrOpen");

            // if (Input.GetKeyDown(KeyCode.P))
            //     mAnimator.SetTrigger("TrClose");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player is in range");
            isPlayerInrange = true;
        }
            
        // show your "Press X" prompt
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInrange = false;
        // hide your "Press X" prompt
    }


    // [SerializeField] private Animator boxAnimator;
    // 
    // private bool isPlayerInrange = false;
    // 
    // // Track whether the object is currently open or not
    // private bool isOpen = false;
    // 
    // private void Update()
    // {
    //     if (isPlayerInrange && Input.GetKeyDown(KeyCode.X))
    //         if(isOpen)
    //         {
    //             // if object is open, trigger close animation
    //             boxAnimator.SetTrigger("TrClose");
    //             isOpen = false;
    //         }
    //         else
    //         {
    //             // if object is closed, trigger open animation
    //             boxAnimator.SetTrigger("TrOpen");
    //             isOpen = true;
    //         }
    // }
    // 

}

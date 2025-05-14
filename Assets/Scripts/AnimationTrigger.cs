using UnityEngine;
//CHATGPT
public class AnimationTrigger : MonoBehaviour
{
    public Animator animator; // Assign in Inspector or get it dynamically

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            animator.SetBool("isInside", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isInside", false);
        }
    }
}

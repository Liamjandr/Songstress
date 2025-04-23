using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Animator animator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("AttackState", true);
            Debug.Log("attacking");
        }
        else
        {
            animator.SetBool("AttackState", false);
        }
    }
}
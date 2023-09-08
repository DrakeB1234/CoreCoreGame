using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int numRandomAnimations;
    [SerializeField]
    private float randomAnimationTime;
    
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine("RandomAnimation");
    }

    IEnumerator RandomAnimation()
    {
        while (true)
        {
            int randInt = Random.Range(1, numRandomAnimations + 1);
            animator.SetInteger("randomAnimation", randInt);

             yield return new WaitForSeconds(1f);
            // Zero resets back to idle animation
            animator.SetInteger("randomAnimation", 0);

            yield return new WaitForSeconds(randomAnimationTime);
        }
    }

    public void DeathAnimation()
    {
        StopAllCoroutines();
        animator.SetInteger("randomAnimation", 0);

        animator.SetTrigger("foreverLaughTrigger");
    }
}

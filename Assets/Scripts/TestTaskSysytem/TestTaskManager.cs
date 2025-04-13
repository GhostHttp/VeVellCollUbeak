using UnityEngine;
using UnityEngine.UI;

public class TestTaskManager : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;

    //public Animation StartAnimator;
    // public Animation EndAnimator;
    public Button Button;

    private void Awake()
    {
        //StartAnimator.Stop();
        Button.onClick.AddListener(SecoundAnim);
        animator.Play("Intro");
    }

    private void SecoundAnim()
    {
        animator2.gameObject.SetActive(true);
        animator.gameObject.SetActive(false);
    }
}

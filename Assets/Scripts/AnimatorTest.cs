using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;
    public KeyCode keyToTrigger = KeyCode.A;
    public KeyCode keyToReset = KeyCode.S;
    public string triggerToPlay = "Fly";
   
    private void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(keyToTrigger))
    //    {
    //        animator.SetTrigger(triggerToPlay);
    //    }
    //}

    void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetBool(triggerToPlay,true);
        }
        else if (Input.GetKeyDown(keyToReset))
        {
            animator.SetBool(triggerToPlay, false);
        }
    }

}

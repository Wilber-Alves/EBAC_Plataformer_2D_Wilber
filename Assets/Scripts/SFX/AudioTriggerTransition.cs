using UnityEngine;
using UnityEngine.Audio;

public class AudioTriggerTransition : MonoBehaviour
{
    public AudioMixerSnapshot snapshotEXIT;
    public AudioMixerSnapshot snapshotENTER;

    public string tagToCompare = "Player";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagToCompare))
        { 
            snapshotENTER.TransitionTo(0.1f);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagToCompare))
        {
            snapshotEXIT.TransitionTo(0.1f);
        }
    }


}

using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public KeyCode keycode = KeyCode.P;
    public AudioSource audioSource;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public void Play()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

    }




}

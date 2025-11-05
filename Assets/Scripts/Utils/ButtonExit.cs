using UnityEngine;

public class ButtonExit : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

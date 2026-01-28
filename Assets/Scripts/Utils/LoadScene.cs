using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(int i)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(i);

    }

    public void Load(string s)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(s);
    }
}

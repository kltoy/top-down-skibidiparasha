using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLOadMAnagER : MonoBehaviour
{
    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadGameWithMode(string mode)
    {
        SceneManager.LoadScene("Game");
    }

}

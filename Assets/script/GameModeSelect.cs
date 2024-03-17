using UnityEngine;

public class GameModeSelect : MonoBehaviour
{
    public string gameMode;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameMode(string gameModeSelect)
    {
        gameMode = gameModeSelect;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLOadMAnagER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
    }
     public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }
    void Update()
    {
        
    }
}

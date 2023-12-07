using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string sceneName;

    public void DelayFunction(string functionName)
    {
        Invoke(functionName, 1);
    }

    public void GoNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitLevel()
    {
        Application.Quit();
    }
}

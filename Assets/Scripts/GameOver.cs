using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Canvas current;

    public void Start()
    {
        current.enabled = false;
    }

    public void Activate()
    {
        Cursor.visible = true;
        current.enabled = true;
    }
    
    public void MainMenu()
    {
        StartCoroutine(LoadAsync("MenuScene"));
    }
  
    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        this.gameObject.SetActive(false);
        yield return null;
    }
}

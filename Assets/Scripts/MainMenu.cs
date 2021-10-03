using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Text highscoreText;
    public void Start()
    {
        Debug.Log("start");
        float highscore;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "time.save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "time.save", FileMode.Open);
            highscore= (float)bf.Deserialize(file);
            file.Close();
        } else
        {
            highscore = 0;
        }

        highscoreText.text = "Try to bake and eat potates before you starve - careful, the potates are unstable\n" +
           "press e to interact, q to drop, esc to exit back to this menu\n" +
            "Highscore - " + highscore;
    }

    
    
    public void PlayGame(string sceneName)
    {
        Cursor.visible = false;
        StartCoroutine(LoadAsync(sceneName));
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        this.gameObject.SetActive(false);
        yield return null;
    }

}

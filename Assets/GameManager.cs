using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public potato potatoPrefab;
    private potato potatoInstance;

    public float hunger;
    public HungerBar hungerBar;

    public float difficulty;

    public float highscore;

    public GameOver go;

    private float start;

    private bool paused = false;
    public MouseLook ml;
    // Start is called before the first frame update
    void Awake()
    {
        start = Time.time;
        BeginGame();
        hunger = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rnd = Random.value;
        if(rnd > difficulty && !paused)
        {
            hunger -= .01f;
        }
        //Debug.Log("hunger is: " + hunger);
        hungerBar.setSlider(hunger);
        if(hunger <= 0 && !paused)
        {
            DestroyGame();
        }
        
    }

    private void eatPotato(float bakedAmt)
    {
        float rnd = Random.value;
        if(bakedAmt > 90)
        {
            hunger += (rnd);
        } else
        {
            hunger -= (rnd / 10);
        }

        hungerBar.setSlider(hunger);
    }

    private void BeginGame() {
        //kitchenInstance = Instantiate(kitchenPrefab) as Kitchen;
        makePotato();
    }

    public void makePotato()
    {
        potatoInstance = Instantiate(potatoPrefab) as potato;
        potatoInstance.potatoEat.AddListener(eatPotato);
        potatoInstance.potatoExplode.AddListener(DestroyGame);
    }

    private void DestroyGame() {
        Debug.Log("game over");
        ml.enabled = false;
        paused = true;
        highscore = Time.time - start;
        StartCoroutine(doSave());
        go.Activate();
    }

    public IEnumerator doSave()
    {
        Debug.Log("highscore" + highscore);
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "time.save"))
        {
            FileStream filef = File.Open(Application.persistentDataPath + "time.save", FileMode.Open);
            float hs = (float)bf.Deserialize(filef);
            if (hs > highscore)
            {
                highscore = hs;
            }
            filef.Close();
        }
        File.Delete(Application.persistentDataPath + "time.save");
        FileStream file = File.Create(Application.persistentDataPath + "time.save");
        bf.Serialize(file, highscore);
        file.Close();
        yield return null;
    }
}

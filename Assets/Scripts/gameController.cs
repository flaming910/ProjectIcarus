using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour {
    //Allows access to game components without using GetComponent
    public GameObject platform;
    public GameObject horiEnem;
    public GameObject zigZag;
    public GameObject star;
    public GameObject gameOver;
    public Transform platController;
    public Text scoreText;
    public GameObject jumpText;
    float speed;
    float platX;
    float platY;
    float obsX;
    float obsY;
    int score;
    int scoreMultiplier;
    int multiplierIncrement;
    int side;
    bool startMoving;
    bool scoreIncrementing;
    public static bool gameLost;
    public static float difficultyScale;

    // Use this for initialization
    void Start()
    {
        gameLost = false;
        scoreIncrementing = false;
        platY = -4;
        obsY = 1;
        difficultyScale = 1;
        scoreMultiplier = 1;
        //Create initial platforms
        for (var i = 0; i < 10; i++)
        {
            //Alternate sides
            if (side == 0)
            {
                platX = Random.Range(-3f, 0f);
                side = 1;
            }
            else
            {
                platX = Random.Range(0f, 3f);
                side = 0;
            }
            platY += 1.3f;
            Instantiate(platform, new Vector2(platX, platY), Quaternion.identity);
        }
        //Create inital obstacles
        for (var i = 0; i < 5; i++)
        {
            //Alternate sides
            if (side == 0)
            {
                obsX = Random.Range(-3f, 0f);
            }
            else
            {
                obsX = Random.Range(0f, 3f);
            }
            obsY += 5f;
            Instantiate(horiEnem, new Vector2(obsX, obsY), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!scoreIncrementing && startMoving)
        {
            StartCoroutine(scoreIncrement());
            scoreIncrementing = true;
        }

        if (!startMoving)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(StartTimer());
                jumpText.SetActive(false);
            }
        }

        //Move platformController up
        if (!gameLost && startMoving)
        {
            difficultyScale += 0.0001f;
            platController.Translate(0, ((cameraScript.speed * difficultyScale) + cameraScript.catchUpSpeed), 0);
            
        }
        //Turns on Game Over text
        if (gameLost)
        {
            gameOver.SetActive(true);
            gameLost = true;
        }

        //Game reset function after losing
        if(gameLost && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        scoreText.text = "" + score;
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        //Detects platforms as they leave the screen, destroys them and creates new ones at the top
        if (other.gameObject.tag == "platform")
        {
            Destroy(other.gameObject);
            if (side == 0)
            {
                platX = Random.Range(-3f, 0);
                side = 1;
            }
            else
            {
                platX = Random.Range(0f, 3f);
                side = 0;
            }
            platY += 1.2f;
            Instantiate(platform, new Vector2(platX, platY), Quaternion.identity);
        }

        //Detects obstacles as they leave the screen, destroys them and creates new ones at the top
        if (other.gameObject.tag == "obstacle")
        {
            Destroy(other.gameObject);
            if (side == 0)
            {                
                obsX = Random.Range(-3f, 0);
            }
            else
            {
                obsX = Random.Range(0f, 3f);
            }
            //Makes obstacles more frequent as game goes on
            obsY += 5f / difficultyScale;
            float enemySpawned = Random.Range(0f, 1f);
            if (enemySpawned > 0.2f)
            {
                Instantiate(horiEnem, new Vector2(obsX, obsY), Quaternion.identity);
            } else if (enemySpawned > 0.05f)
            {
                Instantiate(zigZag, new Vector2(obsX, obsY), Quaternion.identity);
            } else {
                Instantiate(star, new Vector2(obsX, obsY), Quaternion.identity);
            }
            
        }

        //Kills player if they fall
        if (other.gameObject.tag == "Player" || gameLost)
        {
            gameOver.SetActive(true);
            gameLost = true;
            other.gameObject.SetActive(false);
            playerScript.isAlive = false;
        }

    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        startMoving = true;
    }

    IEnumerator scoreIncrement()
    {
        if(playerScript.isAlive)
        {
            yield return new WaitForSeconds(0.5f);
            score += scoreMultiplier;
            multiplierIncrement++;
            if (multiplierIncrement == 5)
            {
                multiplierIncrement = 0;
                scoreMultiplier++;
            }
            StartCoroutine(scoreIncrement());
        }
        
    }


}

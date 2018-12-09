using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public static float speed;
    public static float catchUpSpeed;
    public Camera cam;
    bool startMoving;
    bool shift;
	public Transform cameraTransform;
    public Transform playerTransform;
    float duration = 50f;
    float t;
    Color32 startColor;
    Color32 endColor;

	// Use this for initialization
	void Start () {
        //Delay before camera starts
        startMoving = false;
        shift = false;
        speed = 0.015f;
        startColor = new Color32(64, 244, 239, 0);
        endColor = new Color32(0, 0, 0, 0);       
    }

    // Update is called once per frame
    void Update () {
        
        if(cameraTransform.position.y <= playerTransform.position.y && cameraTransform.position.y != 0)
        {
            catchUpSpeed = (speed * (playerTransform.position.y / cameraTransform.position.y))/2;
        } else
        {
            catchUpSpeed = 0f;
        }

        if(!startMoving)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(StartTimer());
                StartCoroutine(colorShift());
            }
        }

        //Move camera up
        if(!gameController.gameLost && startMoving)
        {
            cameraTransform.Translate(0f, ((speed * gameController.difficultyScale) + catchUpSpeed), 0f);
        }

        //Darken the sky as the player goes up, gives a "climbing to space" effect
        if(shift)
        {
            if (t < 1)
            {
                t += Time.deltaTime / duration;
            }

            cam.backgroundColor = Color.Lerp(startColor, endColor, t);
        }

        

    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1);
        startMoving = true;
    }

    IEnumerator colorShift()
    {
        yield return new WaitForSeconds(6);
        shift = true;
    }
}

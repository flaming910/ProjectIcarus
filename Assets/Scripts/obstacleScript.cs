using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleScript : MonoBehaviour {

    public float speed;
    public Transform obsTrans;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        obsTrans.position = new Vector3(obsTrans.position.x + speed, obsTrans.position.y, 0);
        if(obsTrans.position.x >= 3 || obsTrans.position.x <= -3)
        {
            speed *= -1;
            obsTrans.rotation = new Quaternion(0, 180, 0, 0);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

    public Rigidbody2D playerRigid;
    public float xSpeed;
    public float ySpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			Jump();
		}

		float movement = Input.GetAxis("Horizontal") * xSpeed;
		playerRigid.AddForce(Vector2.right * movement, ForceMode2D.Force);
	}

	void Jump () {
		playerRigid.AddForce(Vector2.up * ySpeed, ForceMode2D.Impulse);
	}
}

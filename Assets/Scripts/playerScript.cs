using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

    public Rigidbody2D playerRigid;
	public Transform playerTransform;
    public Animator playerAnim;
    public SpriteRenderer playerSprite;
    public static bool isAlive;
    public float xSpeed;
    public float ySpeed;
    bool isTouchingFloor;
    bool vulnerable;

	// Use this for initialization
	void Start () {
        vulnerable = true;
        isTouchingFloor = true;
        isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
        //Jump function only when player is touching floor
		if(Input.GetKeyDown(KeyCode.Space) && isTouchingFloor) {
			Jump();
            isTouchingFloor = false;
		}

		if(Input.GetAxis("Horizontal") > 0.05) {
            playerTransform.rotation = new Quaternion(0, 0, 0, 0);
            playerTransform.Translate(xSpeed, 0f, 0f);
            playerAnim.SetBool("run", true);
            
		}
		else if(Input.GetAxis("Horizontal") < -0.05) {
            playerTransform.rotation = new Quaternion(0, 180, 0, 0);
            playerTransform.Translate(xSpeed, 0f, 0f);
            playerAnim.SetBool("run", true);

        } else //Prevents player from sliding around due to some weird issue where the player suddenly got a burst of velocity
        {
            playerRigid.velocity = new Vector3(0, playerRigid.velocity.y, 0);
            playerAnim.SetBool("run", false);
        }


	}

    void Jump()
    {
        playerRigid.AddForce(Vector2.up * ySpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Sets isTouchingFloor to true when the player is standing on the platform, and the velocity check is to make sure they aren't just passing through
        if (other.gameObject.tag == "platform" && playerRigid.velocity.y <= 0.2f)
        {
            isTouchingFloor = true;
        }

        if(other.gameObject.tag == "obstacle" && vulnerable)
        {
            this.gameObject.SetActive(false);
            gameController.gameLost = true;
            isAlive = false;
        }

        if(other.gameObject.tag == "star")
        {
            Destroy(other.gameObject);
            vulnerable = false;
            playerSprite.color = new Color32(255, 255, 0, 255);
            StartCoroutine(starTimer());
        }


    }

    IEnumerator starTimer()
    {
        yield return new WaitForSeconds(5f);
        vulnerable = true;
        playerSprite.color = new Color32(255, 255, 255, 255);
    }

}

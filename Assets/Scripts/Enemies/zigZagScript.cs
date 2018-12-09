using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zigZagScript : MonoBehaviour {

    public Transform enemTrans;
    public float xSpeed;
    public float ySpeed;
    float originY;

    // Use this for initialization
    void Start()
    {
        originY = enemTrans.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //obsTrans.Translate(new Vector3(speed, 0, 0));
        enemTrans.position = new Vector3(enemTrans.position.x + xSpeed, enemTrans.position.y + ySpeed, 0);
        if (enemTrans.position.x >= 3 || enemTrans.position.x <= -3)
        {
            xSpeed *= -1;
            enemTrans.rotation = new Quaternion(0, 180, 0, 0);
        }

        if (enemTrans.position.y >= originY + 0.8f)
        {
            ySpeed *= -1;
        } else if (enemTrans.position.y <= originY - 0.8f)
        {
            ySpeed *= -1;
        }
    }
}

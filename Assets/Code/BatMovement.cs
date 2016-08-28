using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour {
    Vector2 upMovement;
    Vector2 downMovement;
    bool movingUp;

    // Use this for initialization
    void Start () {
        var xMovement = Random.Range(0.02f, 0.04f);
        var positive = Random.Range(0, 1);
        if (positive == 0)
        {
            xMovement *= -1;
            transform.position = new Vector3(3.55f, 1.2f, 7.0f);
        }
        upMovement = new Vector2(xMovement, 0.01f);
        downMovement = new Vector2(xMovement, -0.01f);
        movingUp = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (movingUp)
        {
            transform.Translate(upMovement);

            if (transform.position.y >= 1.68f)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(downMovement);

            if (transform.position.y <= 1.15f)
            {
                movingUp = true;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class BreakableRock : MonoBehaviour {
    int state = 0;
    public Sprite secondState;
    public Sprite thirdState;
    GameObject key;

    bool keyDropped;
    float keyDropTimer;
    bool keyCollectable;

	// Use this for initialization
	void Start () {
        keyDropped = false;
        keyDropTimer = 1.2f;
        keyCollectable = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (keyDropped)
        {
            keyDropTimer -= Time.deltaTime;

            if (keyDropTimer <= 0.0f)
            {
                keyCollectable = true;
            }
        }
	}

    public void BreakMore()
    {
        state++;

        switch(state)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = secondState;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = thirdState;
                break;
            case 3:
                BreakRock();
                break;
        }
    }

    public bool Broken()
    {
        return state >= 3;
    }

    void BreakRock()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        key = GameObject.Instantiate(Resources.Load("Prefabs/Key") as GameObject);
        keyDropped = true;
    }

    public bool CanCollectKey()
    {
        return keyCollectable;
    }

    public void PickUpKey()
    {
        keyCollectable = false;
        GameObject.Destroy(key);
    }
}

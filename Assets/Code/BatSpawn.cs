using UnityEngine;
using System.Collections;

public class BatSpawn : MonoBehaviour {
    float currentTimer;
    float minTimer;
    float maxTimer;

    // Use this for initialization
    void Start () {
        minTimer = 10.0f;
        maxTimer = 25.0f;
        currentTimer = 8.0f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentTimer > 0.0f)
        {
            currentTimer -= Time.deltaTime;
        } else
        {
            GameObject.Instantiate(Resources.Load("Prefabs/Bat") as GameObject);
            currentTimer = Random.Range(minTimer, maxTimer);
        }
    }
}

using UnityEngine;
using System.Collections;

public class BatCollection : MonoBehaviour {
    float collectionTimer;
    public int resourceCount;
    TextMesh resourceText;

	// Use this for initialization
	void Start () {
        collectionTimer = 1.0f;
        resourceCount = 0;
        resourceText = transform.Find("ResourceText").GetComponent<TextMesh>();
        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {
        collectionTimer -= Time.deltaTime;

        if (collectionTimer <= 0.0f)
        {
            if (resourceCount < 99)
            {
                resourceCount++;
                collectionTimer = 1.0f;
                UpdateText();
            }
        }
    }

    void UpdateText()
    {
        resourceText.text = resourceCount.ToString();
    }
}

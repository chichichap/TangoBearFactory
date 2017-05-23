using UnityEngine;
using System.Collections;

public class Placeholder : MonoBehaviour {
    /*public GameObject head;
    public GameObject body;*/

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () { 

    }

    void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Head") || c.CompareTag("Body")) {
            c.GetComponent<Float>().inPlaceholderArea = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Head") || c.CompareTag("Body"))
        {
            c.GetComponent<Float>().inPlaceholderArea = false;
        }
    }
}

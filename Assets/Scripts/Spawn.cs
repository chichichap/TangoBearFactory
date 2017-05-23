using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject item;
    public GameObject head;
    public GameObject body;
    public Transform spawnLocation;

    public bool IsSpawning;
    
	void Start () {
        StartCoroutine(spawnObjects());
	}

    IEnumerator spawnObjects() { 
        for (int i = 0; true; i++) {

            if (IsSpawning)
            {
                if (i % 2 == 0)
                {
                    Instantiate(head, spawnLocation.position, Quaternion.identity, this.transform);
                }
                else
                {
                    Instantiate(body, spawnLocation.position, Quaternion.identity, this.transform);
                }
            }
                
            yield return new WaitForSecondsRealtime(0.5f);
        } 
    }
}

using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {
    public float FloatStrenght = 0;
    public float RandomRotationStrenght = 0;

    public int lifeTime = 20;
    public bool locked = false;
    public bool inPlaceholderArea = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(expire());
	}
	
	// Update is called once per frame
	void Update () {
        //random = Random.Range(-1.0f, 1.0f);
        if (locked) {
            transform.GetComponent<Rigidbody>().isKinematic = true;
        } else {
            transform.GetComponent<Rigidbody>().isKinematic = false;

            Vector3 direction = Vector3.up;

            if (transform.position.y > 3f)
                direction = Vector3.down;
            else if (transform.position.y < 1f)
                direction = Vector3.up;

            transform.GetComponent<Rigidbody>().AddForce(direction * FloatStrenght);
            transform.Rotate(RandomRotationStrenght, RandomRotationStrenght, RandomRotationStrenght);
        } 
    }

    IEnumerator resetForce() {
        yield return null;
    }

    IEnumerator expire() {
        while (lifeTime > 0) { 
            yield return new WaitForSeconds(1);

            if (!locked)
                lifeTime--;
        } 
         
        Destroy(gameObject);
    }
}

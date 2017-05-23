using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraRayController : MonoBehaviour {
    GameObject goPicking;
    public Transform boxParent;
    public Transform grabbedTfm;
    public Transform placeholder;

    public float smoothTime;
    //Vector4 relatedPos;
    Vector3 _tempCurrVel;

    void Start()
    {
        var btn = GameObject.FindGameObjectWithTag("MainCanvas").transform.Find("ButtonPickup").GetComponent<Button>();
        btn.onClick.AddListener(() => {
            PickUpItem();
        });
    } 

    void Update()
    {
        if (Input.GetKey(KeyCode.J)) {
            PickUpItem();
        }
        if (Input.GetKey(KeyCode.K))
        {
            GameplayController.Singleton.Score++;
        }

        if (goPicking != null)
        {
            goPicking.transform.position = Vector3.SmoothDamp(goPicking.transform.position, grabbedTfm.position, ref _tempCurrVel, smoothTime);
        }
    }

    void PickUpItem() {
        if (goPicking == null)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 100, LayerMask.GetMask("Grabbable")))
            {
                Debug.Log("ray" + LayerMask.GetMask("Grabbable") + " " + LayerMask.NameToLayer("Grabbable"));
                goPicking = hitInfo.collider.gameObject;
                //relatedPos = transform.worldToLocalMatrix * goPicking.transform.position;
                goPicking.transform.SetParent(transform);
                goPicking.GetComponent<Float>().locked = true; //goPicking.GetComponent<Rigidbody>().isKinematic = true; 
            }
            Debug.Log("ray2");
        }
        else //releasing
        {
            //case 1: releasing in placeholder
            if (goPicking.GetComponent<Float>().inPlaceholderArea)
            {
                if (goPicking.CompareTag("Head"))
                {
                    goPicking.transform.position = placeholder.Find("HeadHolder").position;
                }
                else if (goPicking.CompareTag("Body"))
                {
                    goPicking.transform.position = placeholder.Find("BodyHolder").position;
                }

                goPicking.transform.SetParent(placeholder);
                goPicking.transform.rotation = placeholder.rotation;
                
                goPicking.GetComponent<Float>().locked = true;
            }
            //case 2: releasing outside placeholder
            else {
                goPicking.transform.SetParent(boxParent);
                goPicking.GetComponent<Float>().locked = false; //goPicking.GetComponent<Rigidbody>().isKinematic = false;
            }

            goPicking = null;
        }
    } 
}

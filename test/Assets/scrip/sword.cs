using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public GameObject tip;
    public GameObject player;
    public GameObject myself;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(0,0, Camera.main.transform.position.z-180);
        this.transform.LookAt(Camera.main.transform);
        if (tip.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.SetActive(true);
                Destroy(myself);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tip.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            tip.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynight : MonoBehaviour
{
    public float duration = 1.0F;
    public float speed= 1.0F;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = new Color(0.75f, 0.66f, 0.48f);
    }

    // Update is called once per frame
    void Update()
    {   
        float lerp = Mathf.PingPong(Time.time, duration) /duration;
        transform.RotateAround(Vector3.zero, Vector3.right, lerp*Time.deltaTime*speed);
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(new Color(0.65f, 0.63f, 0.73f), new Color(0.09f, 0.1f, 0.24f), lerp));
        RenderSettings.ambientLight = Color.Lerp(new Color(0.75f, 0.66f, 0.48f), new Color(0.09f, 0.1f, 0.24f), lerp);

    }
}

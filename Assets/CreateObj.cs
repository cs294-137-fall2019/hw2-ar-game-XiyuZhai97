using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObj : MonoBehaviour
{
    private float xMinimum, xMaximum, zMinimum, zMaximum;
    float times = 1f;
    public GameObject obj,plane;
    public float Tmax;
    void Start()
    {
        xMinimum = -0.477f;//transform.position.x - 0.5f * plane.GetComponent<Renderer>().bounds.size.x;
        zMinimum = 0.431f;//transform.position.z - 0.5f * plane.GetComponent<Renderer>().bounds.size.z;
        xMaximum = 0.477f;//xMinimum + plane.GetComponent<Renderer>().bounds.size.x;
        zMaximum = -0.431f;// zMinimum + plane.GetComponent<Renderer>().bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        times -= Time.deltaTime;
        if (times < 0)  
        {
            GameObject items = (GameObject)Instantiate(obj);
            items.transform.parent = plane.transform;
            items.transform.localScale = new Vector3(items.transform.localScale.x*1.5f,
            items.transform.localScale.y*0.05f, items.transform.localScale.z*1.5f);
            items.transform.localPosition = new Vector3(Random.Range(xMinimum, xMaximum), 1f,
                Random.Range(zMinimum, zMaximum));
            times = Random.Range(0, Tmax);
        }

    }
}

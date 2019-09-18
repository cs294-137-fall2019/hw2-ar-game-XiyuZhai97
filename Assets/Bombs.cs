using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class Bombs : MonoBehaviour
{
    private float westPosX = -2.15f, eastPosX = -0.814f;
    Vector3 velocity;
    [Range(0, 1)]
    public float speed;

    void Start()
    {
        speed = 0.005f;
        float z = Random.Range(0, 2) * 2f - 1f;
        float x = Random.Range(0, 2) * 2f - 1f * Random.Range(0.2f, 1f);
        // this.gameObject.transform.localPosition = initPos;
        velocity = new Vector3(x, 0, z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = velocity.normalized * speed;
        transform.position += velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.name)
        {
            case "WallEast":
                //transform.localPosition.x = westPos.x;
                transform.localPosition = new Vector3(westPosX, transform.localPosition.y, transform.localPosition.z);
                return;
            case "WallWest":
                transform.localPosition = new Vector3(eastPosX, transform.localPosition.y, transform.localPosition.z);
                return;
            case "WallNorth":
            case "WallSouth":
                velocity.x *= -1f;
                return;
            default:
                //velocity.x *= -1f;
                return;
        }
    }
}

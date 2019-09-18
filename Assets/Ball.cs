using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{

    public Text messageText;
    public Vector3 initPos;
    private float westPosX=-0.427f, eastPosX=0.427f;
    Vector3 velocity;
    [Range(0, 1)]
    public float speedball;
    // Stores a counter for the current remaining wait time.
    public Text countText, winText, failText;
    private int count;
    private bool startPosFlag;
    private Vector2 touchStart, touchEnd;
    public GameObject plane;
    void Start()
    {
        transform.parent = plane.transform;
        messageText.gameObject.SetActive(true);
        initPos = transform.localPosition;
        ResetBall();
    }
    public void ResetBall ()
    {
        transform.localPosition = initPos;
        speedball = 0f;
        float z = Random.Range(0, 2) * 2f - 1f;
        float x = Random.Range(0, 2) * 2f - 1f * Random.Range(0.2f, 1f);
        // this.gameObject.transform.localPosition = initPos;
        velocity = new Vector3(x, 0, z);
        count = 0;
        SetCountText();
        winText.text = "Let's Roll!";
        failText.text = "";

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = velocity.normalized * speedball;
        transform.position += velocity;
        messageText.text = "Level: " + (count/10 + 1) + "\n" + "Speed: " + speedball * 500f;
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && startPosFlag == true)
        {
            touchStart = Input.GetTouch(0).position;

            startPosFlag = false;
        }
        // Swipe end
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            startPosFlag = true;
            touchEnd = Input.GetTouch(0).position;

            float cameraFacing = Camera.main.transform.eulerAngles.y;
            Vector2 swipeVector = touchEnd - touchStart;
            Vector3 inputVector = new Vector3(swipeVector.x, 0.0f, swipeVector.y);
            Vector3 movement = Quaternion.Euler(0.0f, cameraFacing, 0.0f) * Vector3.Normalize(inputVector);
            velocity = movement;

        }
       
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Bottles":
                speedball += 0.0005f;
                return;
            case "Bombs":
                collision.gameObject.SetActive(false);
                failText.text = "Bomb, You Failed!" + "\n" + "Your score: " + count.ToString();
                //System.Threading.Thread.Sleep(2000);
                //ResetBall();
                return;
        }
        switch (collision.transform.name)
        {
            case "WallEast":
                //transform.localPosition.x = westPos.x;
                //transform.localPosition = new Vector3(westPosX, transform.localPosition.y, transform.localPosition.z);
                velocity.z *= -1f;
                return;
            case "WallWest":
                //transform.localPosition = new Vector3(eastPosX, transform.localPosition.y, transform.localPosition.z);
                velocity.z *= -1f;
                return;
            case "WallNorth":
            case "WallSouth":
                velocity.x *= -1f;
                return;
            default:
            
                return;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("Coins"))
        {
            // Make the other game object (the pick up) inactive, to make it disappear
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }

    }
    void SetCountText()
    {
        // Update the text field of our 'countText' variable
        countText.text = "Count: " + count.ToString();

        // Check if our 'count' is equal to or exceeded 12
        if (count % 10 == 0)
        {
            winText.text = "You Win! Next Level!";
            Throttle();
        }
    }
    public void Throttle()
    {
        speedball += 0.002f;
        return;
    }
    public void Brake()
    {
        speedball -= 0.002f;
        return;
    }
}

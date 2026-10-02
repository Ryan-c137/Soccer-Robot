using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{

    // player score and goalie score
    public uint pScore = 0, gScore;
    public float Velocity = 0f;
    private InputAction mBall, mKick;
    private Rigidbody rb;

    public bool isKickable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mBall = InputSystem.actions.FindAction("Ball");
        mKick = InputSystem.actions.FindAction("Kick");
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Velocity = rb.linearVelocity.magnitude;
        // If the ball gets stuck(because of players bad playing, not something wrong with the game system)
        if(mBall.IsPressed() && rb.linearVelocity.magnitude <= 0.1f)
        {
            transform.position = new Vector3(0f, 2f, 0f);
            rb.linearVelocity = new Vector3(0f, 0f, 0f);
        }

        // Kicking
        if(isKickable && mKick.IsPressed())
        {
            rb.AddTorque(GameObject.Find("Player").transform.forward * -1000f, ForceMode.Acceleration);
            rb.AddForce(GameObject.Find("Player").transform.right * 200f, ForceMode.Force);
            isKickable = false;
        }

        

    }

    void OnTriggerEnter(Collider collider)
    {

        // Indicate player
        if(collider.name.Equals("goal_goalie_side"))
        {
            // Put in on the centre of court
            transform.position = new Vector3(0f, 2f, 0f);
            rb.linearVelocity = new Vector3(0f, 0f, 0f);
            pScore++;
            Debug.Log("player score!");
        }else if(collider.name.Equals("goal_player_side"))
        {
            // Put in on the centre of court
            transform.position = new Vector3(0f, 2f, 0f);
            rb.linearVelocity = new Vector3(0f, 0f, 0f);
            gScore++;
            Debug.Log("goalie score!");
        }else if(collider.name.Equals("Player"))
        {
            isKickable = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name.Equals("Player"))
        {
            isKickable = false;
        }
    }
}

using UnityEngine;

public class GoalieScript : MonoBehaviour
{
    private Rigidbody rb;

    public float Velocity = 0f;

    public bool isGrounded = true;

    // Serving for rolling wheels
    public float leftWheelVelocity, rightWheelVelocity;

    // private float ballVelocity = 0f;
    private Vector3 ballPosition = Vector3.zero;
    private Vector3 centerOfGoal = new Vector3(0f, 0f, 20.9f); // Centre of Goalie side of goal
    private Vector3 target = Vector3.zero; // place that goalie tries to be in
    private float angle = 0f; // Rotating agnle, so goalie would get to the target
    private float angleBall = 0f; // Rotating agnle, so goalie would face the ball

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();

        leftWheelVelocity = rightWheelVelocity = 0;


    }

    void FixedUpdate()
    {
    
        isGrounded = transform.position.y < 0.8f? true: false;

        // Get Goalie to its place
        if(isGrounded)
        {
            // Get velovcity from ball
            // ballVelocity = GameObject.Find("Soccer Ball").GetComponent<BallController>().Velocity;
            ballPosition = GameObject.Find("Soccer Ball").GetComponent<BallController>().transform.position;
            ballPosition.y = 0f;
            centerOfGoal.y = 0f;
            target = Vector3.Lerp(centerOfGoal, ballPosition, 0.4f); // The target is between the ball and goal
            target.z = target.z < 20f ? target.z : 20f;
            target.z = target.z > 0f ? target.z : 0f;
            angle = Vector3.Angle(transform.right, Vector3.Normalize(target - new Vector3(transform.position.x, 0f, transform.position.z)));
            // Rotate
            Debug.Log("angle: " + angle);
            
            if((target - transform.position).magnitude > 3f && angle > 2f)
            {
                if (rb.linearVelocity.magnitude > 10f)
                {
                    rb.linearVelocity = Vector3.zero;
                }
                transform.Rotate(Vector3.up * Time.fixedDeltaTime * 50f);
                WheelsRoller(1);

            }else
            {
                if((target - transform.position).magnitude > 3f)
                {
                    // Driving to the target place
                    rb.AddForce(transform.right * 0.1f, ForceMode.VelocityChange);
                    // Roll wheels
                    WheelsRoller(0);
                }else
                {
                    // Rotate and face the ball
                    ballPosition.y = 0;
                    angleBall = Vector3.Angle(transform.right, Vector3.Normalize(ballPosition - centerOfGoal));
                    Debug.Log("angleb: " + angleBall);
                    if(angleBall > 2f && rb.linearVelocity.magnitude < 0.01f)
                    {
                        transform.Rotate(Vector3.up * Time.fixedDeltaTime * -50f);
                        WheelsRoller(-1);
                    }else
                    {
                        WheelsRoller(0);
                    }
                }
                
            }
            
        }


    }
    
    // Rolling Wheels
    void WheelsRoller(float clockwise)
    {
        Velocity = Vector3.Dot(rb.linearVelocity, transform.right) >= 0 ? rb.linearVelocity.magnitude : -rb.linearVelocity.magnitude;
        leftWheelVelocity = Velocity + clockwise * 1f; 
        rightWheelVelocity = -Velocity + (clockwise * 1f);
    }

}

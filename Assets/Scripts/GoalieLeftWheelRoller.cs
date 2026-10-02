using UnityEngine;

public class GoalieLeftWheelRoller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float velocity;
    
    void Start()
    {
        velocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = GameObject.Find("Goalie").GetComponent<GoalieScript>().leftWheelVelocity;
        transform.Rotate(Vector3.up * velocity * 0.7f, Space.Self);
        
    }
}

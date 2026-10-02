using UnityEngine;

public class RightWheelRoller : MonoBehaviour
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
        velocity = GameObject.Find("Player").GetComponent<PlayerController>().rightWheelVelocity;
        transform.Rotate(Vector3.up * velocity * 0.7f, Space.Self);
        
    }
}


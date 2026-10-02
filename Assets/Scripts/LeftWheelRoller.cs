using UnityEngine;

public class LeftWheelRoller : MonoBehaviour
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
        velocity = GameObject.Find("Player").GetComponent<PlayerController>().leftWheelVelocity;
        // velocity = Vector3.Dot(rb.linearVelocity, Vector3.right) >= 0 ? rb.linearVelocity.magnitude : -rb.linearVelocity.magnitude;
        // transform.Rotate(transform.up * velocity * 1f);
        // Debug.Log("Transform.up: " + transform.forward);
        // Debug.Log("Vector3.up(Space.self): " + Vector3.up);

        // Through debugging, we can see that transform vectors are following global positions(self but in world space), which are different than Vectors in the Space of self.
        transform.Rotate(Vector3.up * velocity * 0.7f, Space.Self);
        
    }
}

using UnityEngine;
using UnityEngine.UI;

public class DashUIScript : MonoBehaviour
{
    private float percentage;
    void Start()
    {
        percentage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        percentage = GameObject.Find("Player").GetComponent<PlayerController>().dashCoolDown / 3f;
        transform.GetComponent<Slider>().value = percentage;
    }
}

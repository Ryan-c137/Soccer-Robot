using UnityEngine;
using UnityEngine.UI;

public class KickUIScript : MonoBehaviour
{
    private bool isKickable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isKickable = GameObject.Find("Soccer Ball").GetComponent<BallController>().isKickable;
        transform.GetComponent<Slider>().value = isKickable ? 0 : 1;
    }
}

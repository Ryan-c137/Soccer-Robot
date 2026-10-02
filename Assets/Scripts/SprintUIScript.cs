using UnityEngine;
using UnityEngine.UI;

public class SprintUIScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerController>().isSprinting)
        {
            transform.GetComponent<Slider>().value = 1;
        }else
        {
            transform.GetComponent<Slider>().value = 0;
        }
    }
}

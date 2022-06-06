using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide : MonoBehaviour
{
    public GameObject Player;
    public GameObject TpBall;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Cylinder");
        TpBall = GameObject.Find("TpBall");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TpBall")
        {
            Debug.Log("hit");
            GameObject.Destroy(TpBall);

            Player.transform.position = transform.position + new Vector3(0, 1.5f, 0f);
            
        }
    }
}

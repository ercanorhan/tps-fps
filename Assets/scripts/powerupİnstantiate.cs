using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupİnstantiate : MonoBehaviour
{
    public float instantiateCount = 1f;
    public GameObject TPPowerUp;
    public GameObject SFPowerUp;
    public GameObject DJPowerUp;
    private Vector3 tppoweruplocation = new Vector3(-28.97f, 19.5f, 12.41f);
    private Vector3 sfpoweruplocation = new Vector3(-28.97f, 19.5f ,16.17f);
    private Vector3 djpoweruplocation = new Vector3(-28.97f, 19.5f, 14.29f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag=="Player" && instantiateCount == 1)
        {
            Instantiate(TPPowerUp, tppoweruplocation, transform.rotation);
            Instantiate(SFPowerUp, sfpoweruplocation, transform.rotation);
            Instantiate(DJPowerUp, djpoweruplocation, transform.rotation);
            instantiateCount--;
        }
        
    }
}


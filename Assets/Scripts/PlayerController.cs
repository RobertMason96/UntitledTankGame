using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rgbd;
    public float force = 10f;
    public float jumpMag = 50f;
    public float maxSpeed = 20f;
    public bool floored = false;
    public GameObject turret;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputx = 0;
        float inputy = 0;
        Vector3 jumpforce = new Vector3(0, 0, 0);
        
        if(Input.GetKey(KeyCode.W))
        {
            inputy++;           
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputy--;           
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputx++;            
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputx--;            
        }

        
        if(Input.GetKeyDown(KeyCode.Space)&&floored)
        {
            jumpforce = jumpMag * Vector3.up;
            floored = false;
        }



        Vector2 input=new Vector2(inputx,inputy);
        input = input.normalized;
        rgbd.AddForce(-force *input.x* turret.transform.right+ force * input.y * turret.transform.forward + jumpforce);

        if(rgbd.velocity.magnitude>maxSpeed)
        {
            rgbd.velocity = rgbd.velocity.normalized * maxSpeed;
        }

        if(rgbd.velocity.magnitude>0.1)
        {
            this.transform.forward = new Vector3(rgbd.velocity.x, 0, rgbd.velocity.z);
        }

        


    }

    void OnCollisionStay(Collision collision)
    {

        if(collision.transform.tag=="Floor")
        {
            if(rgbd.velocity.y<0)
            {
                Debug.Log("Floored");
                floored = true;
            }
            
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Floor")
        {
            floored = false;
        }
    }
}

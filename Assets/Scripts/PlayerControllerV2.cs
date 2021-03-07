using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{

    CharacterController cc;
    float velocity = 10f;
    // Start is called before the first frame update
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputx = 0;
        float inputy = 0;
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
        Vector3 input=new Vector3(inputx,0,inputy);
        input = input.normalized*velocity;
        cc.Move(input*Time.deltaTime);



    }
}

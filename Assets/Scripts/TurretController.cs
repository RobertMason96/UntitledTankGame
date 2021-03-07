using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject player;
    public GameObject Gun;
    public GameObject bulletSpawner;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    public float mouseSpeed = 10f;
    public float mouseSpeedY = 20f;
    bool lockstate = false;
    float maxAngle = 20;
    float minAngle = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            lockstate = !lockstate;
            if(lockstate)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        transform.Rotate(Vector3.up * Time.deltaTime * mouseSpeed * Input.GetAxis("Mouse X"), Space.World);
        Debug.Log(WrapAngle(Gun.transform.localEulerAngles.x));
        if(((Input.GetAxis("Mouse Y")<0)&&(WrapAngle(Gun.transform.localEulerAngles.x) < maxAngle))||((Input.GetAxis("Mouse Y") > 0) &&(WrapAngle(Gun.transform.localEulerAngles.x) > minAngle)))
        {
            Gun.transform.Rotate(-this.transform.right * Time.deltaTime * mouseSpeedY * Input.GetAxis("Mouse Y"), Space.World);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject tmpBullet = Instantiate(bullet, bulletSpawner.transform.position, Gun.transform.rotation);
            tmpBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * Gun.transform.forward;
            Physics.IgnoreCollision(tmpBullet.GetComponent<Collider>(), player.GetComponent<Collider>());
        }
    }


    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }
}

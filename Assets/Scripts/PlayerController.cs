using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject playerWeapon;
    public float thrustSpeed = 10.0f;
    public float rotateSpeed = 0.5f;
    public float brakeSpeed = 5.0f;

    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // manage mouselook in separate function, also helps with testing
        //MouseLook();

        // manage keyboard movement
        KeyboardMovement();
        
        // mouse click fires a weapon
        if (Input.GetMouseButtonDown(0))
		{
            Instantiate(playerWeapon, transform.position, transform.rotation);
		}
    }

    void MouseLook()
	{
        // Manage mouselook. 
        // TODO : Properly work out how these work rather than playing assign random junk
        float horizontal = Input.GetAxis("Mouse Y") * -1 * thrustSpeed;
        float vertical = Input.GetAxis("Mouse X") * thrustSpeed;
        transform.Rotate(horizontal, vertical, 0);
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        playerCamera.transform.rotation = transform.rotation;
    }

    void KeyboardMovement()
	{
        //manage movement with RigidBody and 
        //forward with W
        if (Input.GetKey(KeyCode.W))
        {
            playerRb.AddRelativeForce(Vector3.forward * thrustSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //backward with S
        if (Input.GetKey(KeyCode.S))
        {
            playerRb.AddRelativeForce(Vector3.back * thrustSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //strafe left with A
        if (Input.GetKey(KeyCode.A))
        {
            playerRb.AddRelativeForce(Vector3.left * thrustSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //strafe right with D
        if (Input.GetKey(KeyCode.D))
        {
            playerRb.AddRelativeForce(Vector3.right * thrustSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //Up movement with Space
        if (Input.GetKey(KeyCode.Space))
        {
            playerRb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //Down with CTRL
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerRb.AddRelativeForce(Vector3.down * thrustSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //Spin counterclockwise with Q. Uses 
        if (Input.GetKey(KeyCode.Q))
        {
            playerRb.AddRelativeTorque(Vector3.forward * rotateSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //Spin clockwise with E
        if (Input.GetKey(KeyCode.E))
        {
            playerRb.AddRelativeTorque(Vector3.back * rotateSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        //air brake to remove all forces
        if (Input.GetKey(KeyCode.F))
        {
            playerRb.drag = brakeSpeed;
            playerRb.angularDrag = brakeSpeed;
        }
        // reset drag on not using air brake
        if (Input.GetKeyUp(KeyCode.F))
        {
            playerRb.drag = 0;
            playerRb.angularDrag = 0;
        }
    }
}

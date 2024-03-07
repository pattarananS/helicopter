using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // -- global var -- //
    private Transform trans;            
   
 
    [SerializeField] private float speed = 100f;
    [SerializeField] private float powerupspeed = 15f;
    [SerializeField] private float sidespeed = 40f;
    [SerializeField] private float backspeed = 20f;

    private float maxspeed = 60f;
    private float z = 0f;
    private float r = 0f;
    private float y = 0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject propellerback;
    [SerializeField] private GameObject playercamoffset;

    private float power = 0f;
    private float prospeed = 100f, prospeedmax = 500f;
    private float strafespeed = 60f;
    public float RotationSpeed = 100;

    bool starting = false;
    private AudioSource proengine;
    private float proenginespeed = 1f;
    private float proenginevolume = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        
        proengine = this.GetComponent<AudioSource>();
        trans = this.GetComponent<Transform>();

        propeller = GameObject.Find("Propeller_Top");
        propellerback = GameObject.Find("Propeller_Back");
        playercamoffset = GameObject.Find("camoffset");
        proengine.volume = 0f;

    }//end of Start()

    // Update is called once per frame
    void Update()
    {
        playercamoffset.transform.position = this.transform.position;
        playercamoffset.transform.Rotate(0, (Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), 0, Space.World);


        if (power >= maxspeed)
        {
            this.MoveDirection();
        }


        if (prospeed >= prospeedmax)
        {
            prospeed += prospeed * Time.deltaTime;
        }

        r += z * Time.deltaTime;
        propeller.transform.rotation = Quaternion.Euler(-90f, 0, r);
        propellerback.transform.rotation = Quaternion.Euler(r, 0, 0);
        

            

            
        // this.Hovering();
        this.PoweringUp();
        this.Strafe();
    }
    
    //end of Update()

    private void MoveDirection()
    {
        if (Input.GetKey(KeyCode.D)){
			transform.position += transform.right * sidespeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A)){
			transform.position += -transform.right* sidespeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W)){
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)){
			transform.position -= transform.forward * backspeed * Time.deltaTime;
		}
        if (Input.GetKey(KeyCode.LeftControl)){
			transform.position += Vector3.down * speed * Time.deltaTime;
		}


    }
    private void Strafe()
    {
        if (Input.GetKey(KeyCode.Q)){
            y -= strafespeed * Time.deltaTime;
			this.transform.rotation = Quaternion.Euler(0, y, 0);
            propellerback.transform.rotation = Quaternion.Euler(0, y, 0);

		}
        if (Input.GetKey(KeyCode.E)){
            y += strafespeed * Time.deltaTime;
			this.transform.rotation = Quaternion.Euler(0, y, 0);
            propellerback.transform.rotation = Quaternion.Euler(0, y, 0);

		}
    }
    private void PoweringUp()
    {
        if (Input.GetKey(KeyCode.C))
        {
            starting = true;

                if (starting == true)
                {
                    prospeed += 1;
                    z += prospeed * Time.deltaTime;
                    power += powerupspeed * Time.deltaTime;

                        if(proengine.pitch <= 3.1f)
                            {
                                proengine.pitch += proenginespeed * Time.deltaTime;
                            }
                        if(proengine.volume <= 1.0f)
                            {
                                proengine.volume += proenginevolume * Time.deltaTime;
                            }
                }  
            
            
        }
        if (Input.GetKey(KeyCode.B))
        {
            starting = false;

                if (starting == false)
                {
                    power = 0f;
                    z = 0f;
                    proengine.volume = 0f;
                    proengine.pitch = 1f;
                }
        }
        if (power >= maxspeed+5)
        {
            power -= powerupspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && power >= maxspeed)
        {
            transform.position += Vector3.up * powerupspeed * Time.deltaTime;
            
        }
        if(power >= maxspeed)
        {
            transform.position -= Vector3.up * 6 *Time.deltaTime;
        }
        

        Debug.Log("Speed = "+ power);
        //---------sound-----------//

    }

}//end of class

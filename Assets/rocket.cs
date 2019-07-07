using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;

    [SerializeField] float rcsThrust =250f;
    [SerializeField] float mainThrust = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        thurst();
        rotation();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "friendly":
                {
                    //do nothing
                    print("OK");
                    break;
                }
            case "fuel":
                {
                    print("fuel");
                    break;
                }
            default:
                print("dead");
                break;
        }

    }
    

     void thurst()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up*mainThrust);
            if (!audiosource.isPlaying)
            {
                audiosource.Play();

            }
           /* else
            {
                audiosource.Stop();
            }*/
        }
        else
        {
            audiosource.Stop();
        }
       
    }

      void rotation()
     {
         float rotationThisFrame = rcsThrust * Time.deltaTime;
         rigidbody.freezeRotation = true;
         if (Input.GetKey(KeyCode.A))
         {
             transform.Rotate(Vector3.forward*rotationThisFrame);
         }
         else if (Input.GetKey(KeyCode.D))
         {
             transform.Rotate(-Vector3.forward*rotationThisFrame);
         }
         rigidbody.freezeRotation = false;
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audiosource;
    enum State { alive, dying, transcending };
    State state = State.alive;

    [SerializeField] float rcsThrust =250f;
    [SerializeField] float mainThrust = 2.5f;
    [SerializeField] AudioClip enginethrust;
     [SerializeField] AudioClip success;
     [SerializeField] AudioClip death;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            respondToThurstInput();
            respondTorotateInput();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(state != State.alive )
        { return; }
        switch (collision.gameObject.tag)
        {
            case "friendly":
                {
                    
                    break;
                }
            case "Finish":
                {
                    InitializeSuccessSequence();
                    break;
                }
            default:
                InitializeDeathSequence();
                break;
        }

    }

     void InitializeSuccessSequence()
    {
        state = State.transcending;
        audiosource.PlayOneShot(success);
        Invoke("LoadNextScene", 1f);    //it will call loadnext scene after 1 second
    }

     void InitializeDeathSequence()
    {
        state = State.dying;
        audiosource.Stop();
        audiosource.PlayOneShot(death);
        Invoke("LoadFirstLevel", 1f);
    }

    private  void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
       
        SceneManager.LoadScene(1);
    }


    void respondToThurstInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            applyThurst();
           
        }
        else
        {
            audiosource.Stop();
        }
       
    }

     void applyThurst()
     {
         rigidbody.AddRelativeForce(Vector3.up * mainThrust);
         if (!audiosource.isPlaying)
         {
             audiosource.PlayOneShot(enginethrust);

         }
     }

     void respondTorotateInput()
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

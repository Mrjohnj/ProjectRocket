using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{

    // PARAMETERS - for tuning, typically set in the editor
    //CACHE - e.g. References for readability or Speed
    //State - Private instance (Member) Variables
    
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust= 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainParticals;
    [SerializeField] ParticleSystem rightParticals;
    [SerializeField] ParticleSystem leftParticals;
    Rigidbody rb;
    AudioSource auidoSource;
    bool mPlay;
   
    bool m_ToggleChange;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        auidoSource = GetComponent<AudioSource>();
      
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        //Check to see if you just set the toggle to positive
       

    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {   
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            if (!auidoSource.isPlaying)
            {
            //Play the audio you attach to the AudioSource component
            auidoSource.PlayOneShot(mainEngine);
            }
            if(!mainParticals.isPlaying)
            {
              //Play Partical Effect
            mainParticals.Play();  
            }           
                                
            }else{
               auidoSource.Stop();
               mainParticals.Stop();
            }       
                        
     

    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!leftParticals.isPlaying)
            {
                leftParticals.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if(!rightParticals.isPlaying)
            {
               rightParticals.Play(); 
            }
            
        }else{
            rightParticals.Stop();
            leftParticals.Stop();
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so phyics engine kicks in
    }
    
}

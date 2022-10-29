using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHanderler : MonoBehaviour
{
  [SerializeField] float Delay = 1f;
  [SerializeField] AudioClip crash;
  [SerializeField] AudioClip finishScene;
  [SerializeField] ParticleSystem crashParticals;
  [SerializeField] ParticleSystem finishSceneParticals;
  AudioSource auidoSource;
  bool isTransitioning =false;
  bool disableCollisions = true;



 void Start()
    {
      
        auidoSource = GetComponent<AudioSource>();
        
      
        
    }
     void Update() {
      RespondToDebugKeys();
    }
    
    void RespondToDebugKeys()
    {
      if(Input.GetKey(KeyCode.L)){
        NextLevel();
      }
      else if(Input.GetKey(KeyCode.C)){
         //This will toggle collision or a bool in general
        disableCollisions = !disableCollisions;
        
      }      
    }

    void OnCollisionEnter(Collision other) 
    {
      if(!disableCollisions || isTransitioning){return;}
      
        switch (other.gameObject.tag)
        {
          case "Friendly":
          Debug.Log("You hit a friendly");
          break;
          case "Finished":
         StartSuccessSequence();
          break;
          
          default:
          StartCrashSequence();
          break;

        }
      }
       
    

   void StartSuccessSequence()
    {
      
      isTransitioning = true;
      finishSceneParticals.Play();
      auidoSource.Stop();
      auidoSource.PlayOneShot(finishScene);
      GetComponent<Movements>().enabled = false;
      Invoke("NextLevel", Delay);
      
        
    }

    void StartCrashSequence(){
      isTransitioning = true;
      crashParticals.Play();
      auidoSource.Stop();
      auidoSource.PlayOneShot(crash);
    GetComponent<Movements>().enabled = false;
    Invoke("Reload", Delay);

    }
    void Reload(){
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex);      
      
    }
   void NextLevel(){
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex +1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);

    }
}

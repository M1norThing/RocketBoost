using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float resetDelay = 1f;

    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip finishSFX;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticle;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;
      
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                PlayerCrash();
                    break;

            case "Finish":
                NextLevel();
                break;

            case "Start":
                Debug.Log("ff");
                break;

            default:
                PlayerCrash();
                break;
        }
    }

    private void NextLevel()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(finishSFX);
        finishParticle.Play();

        GetComponent<PlayerMovement>().enabled = false;
        Invoke("LoadNextLevel", resetDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void PlayerCrash()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticle.Play();

        GetComponent<PlayerMovement>().enabled = false;
        Invoke("ReloadScene",resetDelay);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    } 
}

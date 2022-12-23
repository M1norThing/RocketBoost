using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustForce = 1f;
    [SerializeField] float rotationForce = 1f;

    [SerializeField] AudioClip rocketEngine;

    [SerializeField] ParticleSystem mainEngine;
    [SerializeField] ParticleSystem rightEngine;
    [SerializeField] ParticleSystem leftEngine;


    Rigidbody playerRigidbody;
    AudioSource rocketEngineSFX;
      
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        rocketEngineSFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        rocketThrusting();
        rocketRotation();
    }

    void rocketThrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Thrusting();
        }
        else
        {
            StopEngine();
        }
    }

    private void Thrusting()
    {
        playerRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime, ForceMode.Impulse);

        if (!rocketEngineSFX.isPlaying)
        {
            rocketEngineSFX.PlayOneShot(rocketEngine);
        }
        if (!mainEngine.isPlaying)
        {
            mainEngine.Play();
        }
    }

    void StopEngine()
    {
        rocketEngineSFX.Stop();
        mainEngine.Stop();
    }


    void rocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopSideEngines();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationForce);
        if (!leftEngine.isPlaying)
        {
            leftEngine.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationForce);
        if (!rightEngine.isPlaying)
        {
            rightEngine.Play();
        }
    }

    void StopSideEngines()
    {
        leftEngine.Stop();
        rightEngine.Stop();
    }


    private void ApplyRotation(float rotationValue)
    {
        playerRigidbody.freezeRotation = true; // freeze unityPhysicsSystem rotation to prevent conflicts when player collides with an obstacle

        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);

        playerRigidbody.freezeRotation = false; // unfreezing unityPhysicsSystem rotation
    }
}

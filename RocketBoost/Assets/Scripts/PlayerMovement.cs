using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float thrustForce = 1f;
    [SerializeField] float rotationForce = 1f;
    [SerializeField] AudioClip rocketEngine;

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
            playerRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime,ForceMode.Impulse);

            if (!rocketEngineSFX.isPlaying)
            {
                rocketEngineSFX.PlayOneShot(rocketEngine);
            }
        }
        else
        {
            rocketEngineSFX.Stop();
        }
    }

    void rocketRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationForce);
        }
    }

    private void ApplyRotation(float rotationValue)
    {
        playerRigidbody.freezeRotation = true; // freeze unityPhysicsSystem rotation to prevent conflicts when player collides with an obstacle

        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);

        playerRigidbody.freezeRotation = false; // unfreezing unityPhysicsSystem rotation
    }
}

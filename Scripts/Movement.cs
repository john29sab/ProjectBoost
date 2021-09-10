using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    // PARAMETERS - for turing, typicallly set in the editor
    // CACHE, e.g. reference for readablity of speed
    //STATE- private instance (member) variables
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

        void ProcessThrust()
        {

            if (Input.GetKey(KeyCode.Space))
            {
                StartThrusting();

            }
            else
            {
                StopThrusting();
            }

        }

        void ProcessRotation()
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
                StopRatating();
            }

        }


        void StartThrusting()
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }

        void StopThrusting()
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }
    private void StopRatating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }





    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing so the physics system can't take over.
    }
}

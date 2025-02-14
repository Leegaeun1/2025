using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float zAngle = 0.1f;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;
    // Update is called once per frame
    private void Start()
    {
        rb=GetComponent<Rigidbody>();
        audioSource=rb.GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
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

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
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
            StopRotate();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
            rightThrusterParticles.Play();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
            leftThrusterParticles.Play();
    }

    private void StopRotate()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //수동제어 할수있도록 회전 고정
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; //회전 고정 해제
    }
}

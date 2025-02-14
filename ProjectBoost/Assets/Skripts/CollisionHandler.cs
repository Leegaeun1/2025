using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay=2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;

    bool isTransitioning=false;
    bool collisionDisable=false;

    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            Invoke("NextLevel", levelLoadDelay);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisable) //�����Ҷ�
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true; //���������ϱ� true
        audioSource.Stop(); //������ �Ҹ��� ��� ����
        audioSource.PlayOneShot(success); //�Ҹ� ��ø�ؼ� ��밡��
        successParticles.Play(); //�����Ҷ� ��ƼŬ ����Ʈ �ֱ�
        GetComponent<Movement>().enabled = false; //�������̰� �ϱ�
        Invoke("NextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true; //���������ϱ� true
        audioSource.Stop(); //������ �Ҹ��� ��� ����
        audioSource.PlayOneShot(crash);//�Ҹ� ��ø�ؼ� ��밡��
        crashParticles.Play(); //�߶��Ҷ� ��ƼŬ ����Ʈ �ֱ�
        GetComponent<Movement>().enabled = false; //�������̰� �ϱ�
        Invoke("RoadLevel", levelLoadDelay);
    }
    void RoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneindex = currentSceneIndex + 1;
        if (nextSceneindex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneindex = 0;
        }
        SceneManager.LoadScene(nextSceneindex);
    }
}



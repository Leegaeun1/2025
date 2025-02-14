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
        if (isTransitioning || collisionDisable) //변형할때
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
        isTransitioning = true; //변형했으니까 true
        audioSource.Stop(); //그전의 소리들 모두 정지
        audioSource.PlayOneShot(success); //소리 중첩해서 사용가능
        successParticles.Play(); //성공할때 파티클 이펙트 넣기
        GetComponent<Movement>().enabled = false; //못움직이게 하기
        Invoke("NextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true; //변형했으니까 true
        audioSource.Stop(); //그전의 소리들 모두 정지
        audioSource.PlayOneShot(crash);//소리 중첩해서 사용가능
        crashParticles.Play(); //추락할때 파티클 이펙트 넣기
        GetComponent<Movement>().enabled = false; //못움직이게 하기
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



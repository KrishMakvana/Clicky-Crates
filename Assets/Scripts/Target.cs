using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    public ParticleSystem targetParticle;
    private GameManager gameManager;
    private AudioSource effectAudio;
    public AudioClip boxClick;
    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float posX = 4f;
    private float spawnPos = -3f;
    private float maxTorque = 10f;
    public int scoreValue;


    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        effectAudio = gameManager.GetComponent<AudioSource>();

        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(randomTorque(), randomTorque(), randomTorque());

        transform.position = spawnTarget();
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    private void OnMouseDown()
    {
            if (gameManager.isStarted)
            {
                if (targetParticle != null)
                {
                    Instantiate(targetParticle, transform.position, targetParticle.transform.rotation);
                }

                gameManager.updateScore(scoreValue);
                if (gameObject.CompareTag("Bad"))
                {
                    gameManager.GameOver();
                }
                if (gameObject.CompareTag("PowerUp"))
                {
                    gameManager.slowMotionEffect();
                }
                if (gameObject.CompareTag("PowerUp2"))
                {
                gameManager.DestroyNearbyObjects();
                }
            }
            if (effectAudio != null)
            {
                effectAudio.PlayOneShot(boxClick);
            }          
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    { 
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();

        }
        Destroy(gameObject);
  
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float randomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 spawnTarget()
    {
        return new Vector3(Random.Range(-posX, posX), spawnPos);
    }

}//Class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float minSpeed = 10f;
    public float maxSpeed = 14f;
    public float maxTorque = 10f;

    public float xRange = 4f;
    public float ySpawnPos = 50;

    public AudioClip boom;
    private AudioSource playerAudio;

    public int pointValue;
    public ParticleSystem explosion;

    private GameManager gameManager;

    private Rigidbody targetRb;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomUpwardForce(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    Vector3 RandomUpwardForce()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        return Vector3.up * randomSpeed;
    }

    Vector3 RandomSpawnPos()
    {
        float randomXPos = Random.Range(-xRange, xRange);
        return new Vector3(randomXPos, ySpawnPos);
    }

    float RandomTorque()
    {
        float randomTorque = Random.Range(0, maxTorque);
        return randomTorque;
    }
    IEnumerator delete()
    {
        yield return new WaitForSeconds(0.07f);
        Destroy(gameObject);

    }

    public void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateScore(pointValue);
            }
            else
            {
                gameManager.UpdateLives();
            }
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            playerAudio = GetComponent<AudioSource>();
            playerAudio.PlayOneShot(boom, 1.0f);
            StartCoroutine(delete());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gameManager.lives > 0)
        { 
            gameManager.UpdateLives();
        }
    }




}

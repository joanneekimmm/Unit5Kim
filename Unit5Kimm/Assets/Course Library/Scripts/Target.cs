using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetrb;
    private GameManager gameManager;

    public ParticleSystem explosionParticle;
    public int pointValue;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 5;
    private float ySpawnPos = -2;




    // Start is called before the first frame update
    void Start()
    {
        targetrb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetrb.AddForce(RandomForce(), ForceMode.Impulse);
        targetrb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //destroys objects when collided with the sensor at the bottom
        Destroy(gameObject);
        //lets the bad objects fall through but when the good ones fall it says game over
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.UpdateLives(-1);
        }


    }

    public void DestroyTarget()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
    Vector3 RandomForce ()
    {
        //random spawning of objects 
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}

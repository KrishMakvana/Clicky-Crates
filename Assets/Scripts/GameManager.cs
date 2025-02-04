using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public List<GameObject> powerUpsTarget;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartBnt;
    public GameObject Title;
    public AudioSource audioSource;
    public AudioClip gameOver;
    public float slowDownFactor = 0.2f;  // How much to slow down time (e.g. 0.2 for 20%)
    public float slowDownDuration = 3f;
    private int score;
    private float spawnRate = 1.0f;
    private float powerUpspawnRate = 10.0f;
    public bool isStarted;
    public bool isFinishedAudio;

    public float destroyRadius = 100f;  // The radius within which nearby objects will be destroyed
    public LayerMask targetLayer;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        isFinishedAudio = false;
    }

    private void Update()
    {
       
    }
    public void slowMotionEffect()
    {
            StartCoroutine(SlowDownTime());
       
    }
    IEnumerator spawnObjects()
    {
        while (isStarted)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    IEnumerator PowerUpSpawnRate()
    {
        while (isStarted)
        {
            yield return new WaitForSeconds(powerUpspawnRate);
            int index1 = Random.Range(0, powerUpsTarget.Count);
            Instantiate(powerUpsTarget[index1]);
        }
    }

    public void updateScore( int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        isStarted = false;
        gameOverAudio();
        gameOverText.gameObject.SetActive(true);
        restartBnt.gameObject.SetActive(true); 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isStarted = true;
        score = 0;
        StartCoroutine("spawnObjects");
        StartCoroutine("PowerUpSpawnRate");
        updateScore(0);
        Title.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    public void gameOverAudio()
    {
       
        if (isFinishedAudio == false)
        {
            AudioSource.PlayClipAtPoint(gameOver, new Vector3(0, -10, 0));
            isFinishedAudio = true;
        }        
    }
   public IEnumerator SlowDownTime()
    {
        // Slow down time
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;  // Adjust physics time step accordingly

        // Wait for the slowDownDuration
        yield return new WaitForSecondsRealtime(slowDownDuration);

        // Restore time to normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;  // Restore default physics time step
    }
    public void DestroyNearbyObjects()
    {
        Debug.Log("Destroying objects in radius: " + destroyRadius);
        // Find all objects within the destruction radius
        Collider[] objectsToDestroy = Physics.OverlapSphere(transform.position, destroyRadius, targetLayer);
        Debug.Log(objectsToDestroy.Length + " objects found in radius.");
        // Loop through all objects in the radius
        foreach (Collider nearbyObject in objectsToDestroy)
        {
            Debug.Log("Destroying object: " + nearbyObject.name);
            // Destroy each object
            Destroy(nearbyObject.gameObject);

        }
    }
}//CLASS

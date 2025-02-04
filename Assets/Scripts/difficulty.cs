using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class difficulty : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int Difficulty;
    
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetDifficulty()
    {
        gameManager.StartGame(Difficulty);
    }
}

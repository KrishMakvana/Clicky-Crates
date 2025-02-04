using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Static instance to ensure only one AudioManager exists
    private static AudioManager instance = null;

    private void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance == null)
        {
            // If not, set this instance and mark it to not be destroyed on scene load
            instance = this;
            DontDestroyOnLoad(gameObject);  // This will keep the AudioManager alive across scenes
        }
        else
        {
            // If an instance already exists, destroy this one to avoid duplicates
            Destroy(gameObject);
        }
    }
}

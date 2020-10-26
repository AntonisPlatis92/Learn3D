using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    [SerializeField]
    AudioClip[] sounds; //Initialize from Unity the sound effects we need
    GameObject AudioSource; //Object in hierarchy that plays the sound
    
    //Method is executed when the app is starting
    void Start()
    {
        AudioSource = GameObject.Find("SoundEffects"); //Locate the object in the hierarchy
    }

    //Method to play a sound
    public void playsound(int i)
    {
        AudioSource.GetComponent<AudioSource>().clip = sounds[i]; //Find the ith song and play it
        AudioSource.GetComponent<AudioSource>().Play();
    }
    
    //Method to stop a sound
    public void stopsound()
    {
        AudioSource.GetComponent<AudioSource>().Stop(); //Stop the audiosource
    }
}

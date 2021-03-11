using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static AudioClip dolphinHit;
    public static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        dolphinHit = Resources.Load<AudioClip>("impact");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {

        if (clip == "impact")
        {
            audioSrc.PlayOneShot(dolphinHit);
        }
       

    }
}

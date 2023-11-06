using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audio;
    private float time;
    private float pastTime;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if (time >= pastTime + 0.25f&& time < 40f)
        {
            audio.pitch += 0.001f;
            pastTime = time;
        }
    }
}

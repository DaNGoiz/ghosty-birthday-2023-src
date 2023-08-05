using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public int index = 0;
    public AudioSource[] audioSources;
    
    public void ChangeBGM(){
        audioSources[index].volume = 0f;

        //生成0-3之间的整数
        int randomIndex = Random.Range(0, audioSources.Length);
        while(randomIndex == index){
            randomIndex = Random.Range(0, audioSources.Length);
        }
        index = randomIndex;

        audioSources[index].volume = 0.7f;
    }

    public void Mute(){
        audioSources[index].volume = 0f;
    }

    public void Unmute(){
        audioSources[index].volume = 0.7f;
    }
}

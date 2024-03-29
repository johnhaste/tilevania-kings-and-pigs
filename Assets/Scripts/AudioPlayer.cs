using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance = null;
    public static AudioPlayer Instance{
        get {return instance;}
    }

    void Awake(){
        if( instance != null && instance != this){
            Destroy(this.gameObject);
            return;
        }
        else{
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

}

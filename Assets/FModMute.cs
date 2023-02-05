using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FModMute : MonoBehaviour
{
    // Start is called before the first frame update
    public void SfxMute(bool state)
    {
        if (state)
        {
            FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA("vca:/SFX"); vca.setVolume(0);
        }

        else
        {
            FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA("vca:/SFX"); vca.setVolume(1);
        }

        
    }

    public void MusicMute(bool state)
    {
        if (state)
        {
            FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA("vca:/Music"); vca.setVolume(0);
        }

        else
        {
            FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA("vca:/Music"); vca.setVolume(1);
        }

    }
}

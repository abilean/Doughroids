using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour {

    public AudioMixer MasterMixer;

    public void SetMasterVol(float vol)
    {
        MasterMixer.SetFloat("MasterVol", vol);
    }

    public void SetMusicVol(float vol)
    {
        MasterMixer.SetFloat("MusicVol", vol);
    }

    public void SetSfxVol(float vol)
    {
        MasterMixer.SetFloat("SfxVol", vol);
    }
}

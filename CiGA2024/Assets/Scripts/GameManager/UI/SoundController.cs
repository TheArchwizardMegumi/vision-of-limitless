using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        float dbVolume = volume > 0 ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat("Master Volume", dbVolume);
    }
    public void SetBGMVolume(float volume)
    {
        float dbVolume = volume > 0 ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat("BGM Volume", dbVolume);
    }
    public void SetBGSVolume(float volume)
    {
        float dbVolume = volume > 0 ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat("BGS Volume", dbVolume);
    }
}

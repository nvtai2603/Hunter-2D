using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Volume")]
    [SerializeField] public AudioSource music;
    [SerializeField] public AudioSource sfx;
    private AudioSource loopingSFXSource;

    [Header("Audio Clip")]
    [SerializeField] public AudioClip background;
    [SerializeField] public AudioClip bowLoading;
    [SerializeField] public AudioClip bowShoot;
    [Header("Audio Clip UI")]
    [SerializeField] public AudioClip click;

    [Header("Volume Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    [Header("Button List")]
    [SerializeField] private List<Button> buttonList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        music.clip = background;
        music.Play();
        music.volume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
        sfx.volume = PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f);
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = music.volume;
        }
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = sfx.volume;
        }
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(() => PlayClickSound(button));
        }
    }
    private void Update()
    {
        if (musicVolumeSlider == null)
        {
            musicVolumeSlider = GameObject.FindWithTag("SliderMusic").GetComponent<Slider>();
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = music.volume;
                musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            }
        }

        if (sfxVolumeSlider == null)
        {
            sfxVolumeSlider = GameObject.FindWithTag("SliderSFX").GetComponent<Slider>();
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = sfx.volume;
                sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void PlayLoopingSFX(AudioClip clip)
    {
        if (loopingSFXSource == null)
        {
            loopingSFXSource = gameObject.AddComponent<AudioSource>();
            loopingSFXSource.volume = sfx.volume;
        }
        loopingSFXSource.clip = clip;
        loopingSFXSource.loop = true;
        loopingSFXSource.Play();
    }

    public void StopLoopingSFX()
    {
        if (loopingSFXSource != null)
        {
            loopingSFXSource.Stop();
            Destroy(loopingSFXSource);
            loopingSFXSource = null;
        }
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfx.volume = volume;
        if (loopingSFXSource != null)
        {
            loopingSFXSource.volume = volume;
        }
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    public void PlayClickSound(Button button)
    {
        PlaySFX(click);
    }
}

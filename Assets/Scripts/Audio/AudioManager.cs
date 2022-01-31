using UnityEngine;
using System;

class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private static AudioManager _instance;
    public static AudioManager i { get { return _instance; } }

    [SerializeField] string debug;


    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        UpdateSources();

    }
    [NaughtyAttributes.Button]
    private void UpdateSources()
    {
        DeleteAllAudioSources();
        _instance = this;
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    private void OnValidate()
    {

        // Start();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip);

    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();

    }
    [NaughtyAttributes.Button]
    public void DebugPlay()
    {
        Play(debug);
    }
    [NaughtyAttributes.Button]
    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            Stop(sound.name);
        }
    }
    [NaughtyAttributes.Button]
    public void DeleteAllAudioSources()
    {
        var audioSources = gameObject.GetComponents<AudioSource>();
        if (!Application.isPlaying)
            Array.ForEach(audioSources, aud => DestroyImmediate(aud));
        else
            Array.ForEach(audioSources, aud => Destroy(aud));
    }



}
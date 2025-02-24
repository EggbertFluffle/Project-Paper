using System;
using System.Collections;
using System.Linq;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* The AudioManager is responsible for storing and playing sounds.
     * It stores music and SFX in separate arrays, each accessible through their own methods.
     * It references and controls the AudioMixers, which control volume. 
     */

    private static AudioManager instance;

    [SerializeField] private Sound[] soundEffects;
    [SerializeField] private Sound[] music;

    public enum SoundType
    { Music, SFX }

    private void Awake()
    {
        /* Initialize the singleton. If one already exists, destroy this object. */
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        UpdateSounds();
    }

    private void Start()
    {
        /* Plays the Idle Theme (Or normal theme in a Menu Scene) of a world. */
        PlayMusicInstance("");
    }

    private void UpdateSounds()
    {
        /* Adds an AudioSource component to this game object for every Sound object in both arrays.
         * Sets the variables of each AudioSource to its respective Sound object.
         */

        foreach (Sound s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public static void PlaySFX(string name) => instance.PlaySFXInstance(name);

    private void PlaySFXInstance(string name)
    {
        /* Finds a sound effect by its name and plays it, if it exists. */
        Sound soundEffect = GetSoundInstance(name, SoundType.SFX);
        if (soundEffect != null)
            soundEffect.source.Play();
    }

    public static void PlayMusic(string name) => instance.PlayMusicInstance(name);

    private void PlayMusicInstance(string name)
    {
        /* Find a music track by its name and plays it, if it exists. */
        Sound music = GetSoundInstance(name, SoundType.Music);
        if (music != null)
        {
            music.source.volume = music.volume;
            music.source.Play();
        }

    }

    public static void StopSFX() => instance.StopSFXInstance(); 

    private void StopSFXInstance()
    {
        /* Stops all sound effects. */
        foreach (Sound s in soundEffects.Where(s => s.source.isPlaying))   
            s.source.Stop();
    }

    public static void StopMusic(float duration = 1) => instance.StopMusicInstance(duration);

    private void StopMusicInstance(float duration)
    {
        /* Fades out all music. Fade out duration can be specified. */
        foreach (var s in music.Where(s => s.source.isPlaying))
        {
            StartCoroutine(Diminuendo(s, duration / 8));
            Invoke(nameof(StopMusicCoroutine), duration);
        }
    }

    private IEnumerator Diminuendo(Sound track, float duration)
    {
        /* Used for fading out music.
         * Decrements the volume by a small amount every frame, until it is 0.
         * When finished, stop the track.
         */

        for (float vol = track.source.volume; vol >= 0; vol -= 0.05f)
        {
            track.source.volume = vol;
            yield return new WaitForSecondsRealtime(duration);
        }
        track.source.Stop();
    }

    private void StopMusicCoroutine() => StopCoroutine(nameof(Diminuendo));

    public static void PauseMusic(string name) => instance.PauseMusicInstance(name);

    private void PauseMusicInstance(string name)
    {
        /* Finds a music track by its name and pauses it, if it exists and is playing. */
        Sound music = GetSoundInstance(name, SoundType.Music);
        if (music != null && music.source.isPlaying)
            music.source.Pause();
    }

    public static void PauseSFX(string name) => instance.PauseSFXInstance(name);

    private void PauseSFXInstance(string name)
    {
        /* Finds a sound effect by its name and pauses it, if it exists and is playing. */
        Sound soundEffect = GetSoundInstance(name, SoundType.SFX);
        if (soundEffect != null && soundEffect.source.isPlaying)
            soundEffect.source.Pause();
    }

    public static bool IsSoundPlaying(string name, SoundType soundType) => instance.IsSoundPlayingInstance(name, soundType);

    private bool IsSoundPlayingInstance(string name, SoundType soundType)
    {
        /* Finds a sound by its name and type and checks if it is playing.
         * This method is mostly called by other classes.
         */

        Sound[] soundArray = soundType == SoundType.Music ? music : soundEffects;
        return soundArray.Any(s => s.name.Equals(name) && s.source.isPlaying);
    }

    public static Sound GetSound(string name, SoundType soundType) => instance.GetSoundInstance(name, soundType);

    private Sound GetSoundInstance(string name, SoundType soundType)
    {
        /* Gets a sound from the Sound Object arrays, by its name and type.
         * If it does not exist, returns null.
         */

        Sound sound = Array.Find(soundType == SoundType.Music ? music : soundEffects, s => s.name.Equals(name));
        if (sound == null) {
            return null;
        }
        return sound;
    }

    public static void SetPitch(string soundName, SoundType soundType, float pitch) {
        instance.SetPitchInstance(soundName, soundType, pitch);
    }  

    public void SetPitchInstance(string soundName, SoundType soundType, float pitch) {
        GetSound(soundName, soundType).pitch = pitch;
        UpdateSounds();
    }

    public static float GetPitch(string soundName, SoundType soundType) {
        return instance.GetPitchInstance(soundName, soundType);
    }

    public float GetPitchInstance(string soundName, SoundType soundType) {
        return GetSound(soundName, soundType).pitch;
    }
}

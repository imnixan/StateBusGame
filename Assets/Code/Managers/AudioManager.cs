using System.Collections;
using UnityEngine;

public class AudioManager : AbstractManager
{
    [SerializeField]
    private AudioClip[] explosions;

    [SerializeField]
    private AudioClip timerEnd,
        newRecord,
        playerShot,
        startGame,
        wind;
    private AudioSource sound;

    private void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
        UpdateSounds();
        PlayerPrefs.SetInt("Record", 0);
    }

    private void UpdateSounds()
    {
        if (IsSoundOn())
        {
            sound.loop = true;
            sound.clip = wind;
            sound.Play();
        }
        else
        {
            sound.Stop();
        }
    }

    private void Vibrate()
    {
        if (IsSoundOn())
        {
            Handheld.Vibrate();
        }
    }

    private bool IsSoundOn()
    {
        return PlayerPrefs.GetInt(
                StaticConstants.SoundSettingsPrefs,
                StaticConstants.TurnedOnSettingsValue
            ) == StaticConstants.TurnedOnSettingsValue;
    }

    private void PlaySound(AudioClip audio)
    {
        if (IsSoundOn())
        {
            sound.PlayOneShot(audio);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (StateBus.Explosion)
        {
            PlaySound(explosions[Random.Range(0, explosions.Length)]);
        }
        if (StateBus.PlayerShot)
        {
            PlaySound(playerShot);
            Vibrate();
        }
        if (StateBus.SettingsChanged)
        {
            UpdateSounds();
        }
    }

    public override void OnGameStarted()
    {
        Vibrate();
        PlaySound(startGame);
    }

    public override void OnGameEnded()
    {
        Vibrate();
        if (StateBus.newRecordReached)
        {
            Debug.Log("newRecordsReached");
            PlaySound(newRecord);
        }
    }

    public override void OnMenu() { }

    public void StartEndTimerSound()
    {
        PlaySound(timerEnd);
    }

    public float GetTimerEndLenght()
    {
        return timerEnd.length;
    }
}

using Godot;
using System;

public enum ESoundType
{
    Click,
    Buy,
    ChangeLevel,
}

public enum EMusicType
{
    Start,
    Main,
    Upgrade
}

public partial class SoundManager : Node
{
    public static SoundManager Instance;

    [Signal] public delegate void PlaySoundEventHandler(ESoundType soundType);

    [Signal] public delegate void StartMusicEventHandler(EMusicType musicType);
    [Signal] public delegate void StopMusicEventHandler();
    [Signal] public delegate void UnpauseMusicEventHandler();

    [Signal] public delegate void SetMasterVolumeEventHandler(float linearValue);

    [Export] private Godot.Collections.Dictionary<ESoundType, AudioStream> _soundDictionary;
    [Export] private Godot.Collections.Dictionary<EMusicType, AudioStream> _musicArray;

    private AudioStreamPlayer _soundPlayer;
    private AudioStreamPlayer _musicPlayer;

    private int _masterBusIndex;

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        _soundPlayer = GetNode<AudioStreamPlayer>("SoundPlayer");
        _musicPlayer = GetNode<AudioStreamPlayer>("MusicPlayer");

        PlaySound += OnPlaySound;

        StartMusic += OnStartMusic;
        StopMusic += OnStopMusic;
        UnpauseMusic += OnUnpauseMusic;

        SetMasterVolume += OnSetMasterVolume;

        _masterBusIndex = AudioServer.GetBusIndex("Master");
    }

    private void OnPlaySound(ESoundType soundType)
    {
        _soundPlayer.Stream = _soundDictionary[soundType];
        _soundPlayer.Play();
    }

    private void OnStartMusic(EMusicType musicType)
    {
        _musicPlayer.StreamPaused = false;
        if (_musicPlayer.Stream != _musicArray[musicType]) _musicPlayer.Stream = _musicArray[musicType];
        _musicPlayer.Play();
    }
    private void OnStopMusic()
    {
        _musicPlayer.StreamPaused = true;
    }
    private void OnUnpauseMusic()
    {
        _musicPlayer.StreamPaused = false;
    }

    public void OnSetMasterVolume(float linearValue)
    {
        float linearFraction = linearValue / 100f;

        float dbValue = Mathf.LinearToDb(linearFraction);

        AudioServer.SetBusVolumeDb(_masterBusIndex, dbValue);
    }
}

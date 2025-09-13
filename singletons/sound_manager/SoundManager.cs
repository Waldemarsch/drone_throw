using Godot;
using System;

public enum ESoundType
{
    Click,
    Buy,
    ChangeLevel,
}

public partial class SoundManager : Node
{
    public static SoundManager Instance;

    [Signal] public delegate void PlaySoundEventHandler(ESoundType soundType);

    [Signal] public delegate void StartMusicEventHandler();
    [Signal] public delegate void StopMusicEventHandler();

    [Export] private Godot.Collections.Dictionary<ESoundType, AudioStream> _soundDictionary;
    [Export] private Godot.Collections.Array<AudioStream> _musicArray;

    private AudioStreamPlayer _soundPlayer;
    private AudioStreamPlayer _musicPlayer;

    public override void _Ready()
    {
        base._Ready();

        Instance = this;

        _soundPlayer = GetNode<AudioStreamPlayer>("SoundPlayer");
        _musicPlayer = GetNode<AudioStreamPlayer>("MusicPlayer");

        PlaySound += OnPlaySound;

        StartMusic += OnStartMusic;
        StopMusic += OnStopMusic;
    }

    private void OnPlaySound(ESoundType soundType)
    {
        _soundPlayer.Stream = _soundDictionary[soundType];
        _soundPlayer.Play();
    }

    private void OnStartMusic()
    {
        _musicPlayer.Stream = _musicArray[(int)GD.Randi() % _musicArray.Count];
        _musicPlayer.Play();
    }
    private void OnStopMusic()
    {
        _musicPlayer.Stop();
    }
}

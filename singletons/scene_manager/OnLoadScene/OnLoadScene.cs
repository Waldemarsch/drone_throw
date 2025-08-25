using Godot;
using Godot.Collections; // Важно для Array
using System.Threading.Tasks;

public partial class OnLoadScene : CanvasLayer
{
    [Signal] public delegate void AllScenesLoadedEventHandler(Array<PackedScene> loadedScenes);

    [Signal] private delegate void LoadingFinishedEventHandler();

    private AnimationPlayer _animationPlayer;
    private TextureProgressBar _progressBar;

    private Array<string> _scenesToLoadPaths;
    private bool _loadingFinished = false;

    public override void _Ready()
    {
        base._Ready();
        SceneManager.Instance.LoadingStarted += OnLoadingStarted;
        AllScenesLoaded += OnAllScenesLoaded;
        ProcessMode = ProcessModeEnum.Disabled;

        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _progressBar = GetNode<TextureProgressBar>("ColorRect/CenterContainer/VBoxContainer/TextureProgressBar");

    }

    public async void OnLoadingStarted(Array<string> scenePaths)
    {
        _scenesToLoadPaths = scenePaths;

        ProcessMode = ProcessModeEnum.Always;
        
        _animationPlayer.Play("Dissolve");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

        foreach (var path in _scenesToLoadPaths)
        {
            ResourceLoader.LoadThreadedRequest(path);
        }
        
        await ToSignal(this, SignalName.LoadingFinished);
        
        var loadedScenes = new Array<PackedScene>();
        foreach (var path in _scenesToLoadPaths)
        {
            loadedScenes.Add((PackedScene)ResourceLoader.LoadThreadedGet(path));
        }

        EmitSignal(SignalName.AllScenesLoaded, loadedScenes);

        _animationPlayer.PlayBackwards("Dissolve");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        
        QueueFree();
    }

    private void OnAllScenesLoaded(Array<PackedScene> loadedScenes)
    {
        foreach (var loadedScene in loadedScenes)
        {
            var loadedSceneInstance = loadedScene.Instantiate();

            if (loadedSceneInstance is Control)
            {
                UIManager.Instance.EmitSignal(UIManager.SignalName.LoadUIElement, loadedSceneInstance);
            }

            else if (loadedSceneInstance is Node2D)
            {
                LevelManager.Instance.EmitSignal(LevelManager.SignalName.LoadLevel, loadedSceneInstance);
            }
        }
        SceneManager.Instance.EmitSignal(SceneManager.SignalName.LoadingCompleted);
    }
    
    public override void _Process(double delta)
    {
        if (_loadingFinished) return;

        float totalProgress = 0f;
        int loadedCount = 0;

        foreach (var path in _scenesToLoadPaths)
        {
            var progressArray = new Array();
            var status = ResourceLoader.LoadThreadedGetStatus(path, progressArray);

            if (status == ResourceLoader.ThreadLoadStatus.Loaded)
            {
                totalProgress += 1.0f;
                loadedCount++;
            }
            else if (status == ResourceLoader.ThreadLoadStatus.InProgress)
            {
                totalProgress += (float)progressArray[0];
            }
        }

        _progressBar.Value = (totalProgress / _scenesToLoadPaths.Count) * 100;

        if (loadedCount == _scenesToLoadPaths.Count)
        {
            _loadingFinished = true;
            EmitSignal(SignalName.LoadingFinished);
        }
    }
}
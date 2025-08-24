using Godot;
using Godot.Collections; // Важно для Array
using System.Threading.Tasks;

public partial class OnLoadScene : CanvasLayer
{
    // Сигнал, который мы отправим SceneManager'у, когда ВСЕ будет готово.
    // Он несет в себе массив уже загруженных сцен.
    [Signal]
    public delegate void AllScenesLoadedEventHandler(Array<PackedScene> loadedScenes);

    [Signal]
    private delegate void LoadingFinishedEventHandler();

    [Export]
    private AnimationPlayer _animationPlayer;
    [Export]
    private TextureProgressBar _progressBar;

    private Array<string> _scenesToLoadPaths;
    private bool _loadingFinished = false;

    public override void _Ready()
    {
        base._Ready();
        SceneManager.Instance.LoadingStarted += StartLoadingScenes;
        AllScenesLoaded += OnAllScenesLoaded;
        ProcessMode = ProcessModeEnum.Disabled;
    }

    public async void StartLoadingScenes(Array<string> scenePaths)
    {
        _scenesToLoadPaths = scenePaths;

        ProcessMode = ProcessModeEnum.Always;

        GD.Print(ProcessMode);
        
        // 1. Показываем анимацию "затемнения"
        _animationPlayer.Play("Dissolve");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

        // 2. Начинаем фоновую загрузку и включаем _Process для обновления ProgressBar
        foreach (var path in _scenesToLoadPaths)
        {
            ResourceLoader.LoadThreadedRequest(path);
        }
        
        // 3. ЖДЕМ, пока _Process не сообщит нам, что загрузка завершена
        await ToSignal(this, SignalName.LoadingFinished);
        
        // 4. Собираем результаты
        var loadedScenes = new Array<PackedScene>();
        foreach (var path in _scenesToLoadPaths)
        {
            loadedScenes.Add((PackedScene)ResourceLoader.LoadThreadedGet(path));
        }

        // 5. Сообщаем SceneManager'у, что все готово и передаем ему сцены
        EmitSignal(SignalName.AllScenesLoaded, loadedScenes);

        // 6. Запускаем анимацию "проявления"
        _animationPlayer.PlayBackwards("Dissolve");
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);
        
        // 7. Самоуничтожаемся
        QueueFree();
    }

    private void OnAllScenesLoaded(Array<PackedScene> loadedScenes)
    {
        foreach (var loadedScene in loadedScenes)
        {
            var loadedSceneInstance = loadedScene.Instantiate();

            if (loadedSceneInstance is Control)
            {
                UIManager.Instance.EmitSignal(UIManager.SignalName.AddUIElement, loadedSceneInstance);
            }

            else if (loadedSceneInstance is Node2D)
            {
                SceneManager.Instance.LevelContainer.AddChild(loadedSceneInstance);
            }
        }
        SceneManager.Instance.EmitSignal(SceneManager.SignalName.LoadingCompleted);
    }
    
    public override void _Process(double delta)
    {
        // Если флаг уже взведен, ничего не делаем
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

        // Правильный подсчет общего прогресса
        _progressBar.Value = (totalProgress / _scenesToLoadPaths.Count) * 100;

        // Если все сцены загружены
        if (loadedCount == _scenesToLoadPaths.Count)
        {
            _loadingFinished = true; // Взводим флаг, чтобы не испускать сигнал много раз
            EmitSignal(SignalName.LoadingFinished);
        }
    }
}
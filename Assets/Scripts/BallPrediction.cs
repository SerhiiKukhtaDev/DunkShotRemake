using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class BallPrediction : MonoBehaviour
{
    [Range(0, 20)] [SerializeField] private int dotsCount = 10;
    [SerializeField] private GameObject dot;
    [SerializeField] private PredictionBallMovement predictionBallPrefab;
    [SerializeField] private Canvas bordersPrefab;

    public PredictionBallMovement PredictionBall { get; private set; }

    private const string PredictionSceneName = "BallPrediction";

    private Scene _predictionScene;
    private PhysicsScene2D _predictionScenePhysics;
    private GameObject _dotsContainer;
    private Scene _levelScene;
    private PhysicsScene2D _levelScenePhysics;
    private GameObject[] _dots;
    private Camera _mainCamera;
    private ScreenScaleNotifier _scaleNotifier;

    [Inject]
    private void Construct(Camera mainCamera, ScreenScaleNotifier scaleNotifier)
    {
        _scaleNotifier = scaleNotifier;
        _mainCamera = mainCamera;
    }
    
    private void Start()
    {
        CreateScenes();
        PredictionBall = Instantiate(predictionBallPrefab);
        
        SceneManager.MoveGameObjectToScene(PredictionBall.gameObject, _predictionScene);

        _dotsContainer = new GameObject("Prediction");
        _dotsContainer.SetActive(false);
        
        _dots = new GameObject[dotsCount];
        
        for (int i = 0; i < 10; i++)
        {
            _dots[i] = Instantiate(dot, _dotsContainer.transform);
            _dots[i].transform.position *= _scaleNotifier.Factor;
        }
    }
    
    public void PredictMovement(float forceDelta)
    {
        _dotsContainer.SetActive(true);

        for (int i = 0; i < dotsCount; i++)
        {
            _predictionScenePhysics.Simulate(Time.fixedDeltaTime * 5);
            
            _dots[i].transform.position = PredictionBall.transform.position;
        }
        
        PredictionBall.ResetPhysicsAndDisable();
    }

    public void EndPrediction()
    {
        _dotsContainer.SetActive(false);
    }

    public void SyncPosition(Vector2 mainBallPosition)
    {
        PredictionBall.transform.position = mainBallPosition;
    }
    
    private void CreateScenes()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
        
        _predictionScene = SceneManager.CreateScene(PredictionSceneName, new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _predictionScenePhysics = _predictionScene.GetPhysicsScene2D();

        Canvas borders = Instantiate(bordersPrefab);
        borders.worldCamera = _mainCamera;
        
        SceneManager.MoveGameObjectToScene(borders.gameObject, _predictionScene);

        _levelScene = SceneManager.GetActiveScene();
        _levelScenePhysics = _levelScene.GetPhysicsScene2D();
    }
    
    private void FixedUpdate()
    {
        if (!_levelScenePhysics.IsValid()) return;

        _levelScenePhysics.Simulate(Time.fixedDeltaTime);
    }
}
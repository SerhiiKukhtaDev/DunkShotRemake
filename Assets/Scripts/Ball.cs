using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Ball : MonoBehaviour
{
    [Range(0, 20)] [SerializeField] private int dotsCount = 10;
    [Range(10, 100)] [SerializeField] private float force = 50;
    [SerializeField] private GameObject dot;
    [SerializeField] private Canvas bordersPrefab;
    
    [SerializeField] private BallMovement ball;
    [SerializeField] private PredictionBallMovement predictionBallPrefab;
    
    private PredictionBallMovement _predictionBall;

    private const string PredictionSceneName = "BallPrediction";
    private Scene _predictionScene;
    private PhysicsScene2D _predictionScenePhysics;

    private Scene _levelScene;
    private PhysicsScene2D _levelScenePhysics;

    private IInputWrapper _inputWrapper;
    private Vector2 _startMousePosition;
    private Vector2 _dragPosition;
    private Camera _mainCamera;
    private GameObject _dotsContainer;

    private GameObject[] _dots;

    [Inject]
    private void Construct(IInputWrapper inputWrapper, Camera mainCamera)
    {
        _mainCamera = mainCamera;
        _inputWrapper = inputWrapper;
    }

    private void Start()
    {
        CreateScenes();
        _predictionBall = Instantiate(predictionBallPrefab);

        SceneManager.MoveGameObjectToScene(_predictionBall.gameObject, _predictionScene);

        _dotsContainer = new GameObject("Prediction");
        _dotsContainer.SetActive(false);
        
        _dots = new GameObject[dotsCount];
        
        for (int i = 0; i < 10; i++)
        {
            _dots[i] = Instantiate(dot, _dotsContainer.transform);
        }
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

    private void Update()
    {
        if (_inputWrapper.IsMouseDown)
        {
            _startMousePosition = _inputWrapper.WorldMousePosition;
        }

        if (_inputWrapper.LeftMousePressed)
        {
            _dragPosition = _inputWrapper.WorldMousePosition;
            _predictionBall.transform.position = ball.transform.position;

            MoveBall(_predictionBall);
            _dotsContainer.SetActive(true);

            for (int i = 0; i < dotsCount; i++)
            {
                _predictionScenePhysics.Simulate(Time.fixedDeltaTime);
                
                _dots[i].transform.position = _predictionBall.transform.position;
            }
            
            _predictionBall.ResetPhysicsAndDisable();
        }

        if (_inputWrapper.IsMouseUp)
        {
            MoveBall(ball);
            _dotsContainer.SetActive(false);
        }
    }

    private void MoveBall(BallMovement ballMovement)
    {
        ballMovement.Move(_startMousePosition, _dragPosition, force);
    }

    private void FixedUpdate()
    {
        if (!_levelScenePhysics.IsValid()) return;

        _levelScenePhysics.Simulate(Time.fixedDeltaTime);
    }
}

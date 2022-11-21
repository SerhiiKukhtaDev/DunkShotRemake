using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Data.SpawnPoints.Base
{
    [CreateAssetMenu(menuName = "Data/SpawnPoints/Ball", fileName = "BallSpawnPoint", order = 49)]
    public class BallSpawnPoint : ScriptableObject
    {
        [SerializeField] private Vector2 spawnPoint;

        public Vector2 SpawnPoint
        {
            get => spawnPoint;
            set => spawnPoint = value;
        }
    }
    
    [CustomEditor(typeof(BallSpawnPoint))]
    public class BallSpawnPointEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (BallSpawnPoint)target;
 
            if(GUILayout.Button("Update position"))
            {
                script.SpawnPoint = GameObject.Find("BallSpawnPoint").transform.position;
            }
        }
    }
}

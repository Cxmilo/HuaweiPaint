using System.Collections.Generic;
using PaintCraft.Tools;
using PaintCraft.Utils;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PaintCraft.Controllers
{
    public class MeshPool : MonoBehaviour
    {
        private const int InitPoolSize = 100; //must be greater than zero
        private const int MeshFrameLifeTime = 1; //Destroy in one frame // must be greater than zero
        private Dictionary<int, List<Mesh>> _fetchedMeshesByFrameId = new Dictionary<int, List<Mesh>>();

        static MeshPool _instance;

        private Queue<Mesh> _meshPool = new Queue<Mesh>();

        static readonly object _lock = new object();

        public static bool Initialized()
        {
            return _instance != null;
        }

        public static MeshPool Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] MeshPool Instance '" +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (MeshPool) FindObjectOfType(typeof(MeshPool));

                        if (FindObjectsOfType(typeof(MeshPool)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                           " - there should never be more than 1 singleton!" +
                                           " Reopening the scene might fix it.");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<MeshPool>();
                            _instance.FillInitialPool();
                            singleton.name = "MeshPool";
                        }
                    }

                    return _instance;
                }
            }
        }

        protected static bool applicationIsQuitting = false;

        void FillInitialPool()
        {
            for (int i = 0; i < InitPoolSize; i++)
            {
                _meshPool.Enqueue(MeshUtil.CreatePlaneMesh(1, 1));
            }
        }
        
        
        public void OnApplicationQuit()
        {
            applicationIsQuitting = true;
        }

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                //_instance.FillInitialPool();
            }
            else
            {
                if (this != _instance)
                    Destroy(this);
            }
        }

        void Update()
        {
            RelaseMeshes();
        }

        List<int> _keysToRemove = new List<int>();
        private void RelaseMeshes()
        {
            _keysToRemove.Clear();
            foreach (int key in _fetchedMeshesByFrameId.Keys)
            {
                if ((Time.frameCount - key) >= MeshFrameLifeTime)
                {
                    _keysToRemove.Add(key);
                }
            }

            foreach (int key in _keysToRemove)
            {
                List<Mesh> usedMeshes = _fetchedMeshesByFrameId[key];
                foreach (Mesh mesh in usedMeshes)
                {
                    _meshPool.Enqueue(mesh);
                }                
                _fetchedMeshesByFrameId.Remove(key);
            }            
        }

        public Mesh GetMesh(BrushContext brushLineContext, Point point, Vector2 firstPointUv)
        {
            float width = point.Size.x * point.Scale;
            float height = point.Size.y * point.Scale;
            width = Mathf.Max(brushLineContext.Brush.MinSize.x, width);
            height = Mathf.Max(brushLineContext.Brush.MinSize.y, height);
            Mesh result;
            if (_meshPool.Count > 0)
            {
                result = _meshPool.Dequeue();
                MeshUtil.ChangeMeshSize(result, width, height);
            }
            else
            {
                result = MeshUtil.CreatePlaneMesh(width, height);
            }
            MeshUtil.ChangeMeshColor(result, point.PointColor.Color);
            MeshUtil.UpdateMeshUV2(result, width, height, point.Position, point.Rotation,
                (float) brushLineContext.Canvas.Width, (float) brushLineContext.Canvas.Height,
                brushLineContext.Canvas.transform.position);
            MeshUtil.UpdateMeshUV3(result, firstPointUv);

            if (!_fetchedMeshesByFrameId.ContainsKey(Time.frameCount))
            {
                _fetchedMeshesByFrameId.Add(Time.frameCount, new List<Mesh>(150));
            }
            _fetchedMeshesByFrameId[Time.frameCount].Add(result);
            return result;
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.BeginGUI();
            GUI.Label(new Rect(0,0,200,30), "PoolSize: "+_meshPool.Count);            
            Handles.EndGUI();
        }
#endif
    }
}
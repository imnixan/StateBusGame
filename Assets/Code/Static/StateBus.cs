using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class StateBus
{
    #region Custom states and events

    public static Dictionary<StateMachine.GameStates, IStateWorker> StateWorkers = new Dictionary<
        StateMachine.GameStates,
        IStateWorker
    >
    {
        { StateMachine.GameStates.Game, new GameStateWorker() },
        { StateMachine.GameStates.Menu, new MenuStateWorker() },
        { StateMachine.GameStates.GameEnd, new GameEndStateWorker() },
    };

    public static Vector2 CrosshairPosition;
    public static bool CanShoot;
    public static StateQueue<StateMachine.GameStates> GameStateChanged;
    public static StateQueue<bool> PlayerShot;
    public static StateQueue<Vector2> Explosion;
    public static StateQueue<bool> EnemyKilled;

    #endregion

    #region Standard events

    public static event Action OnAwake = delegate { };
    public static event Action OnStart = delegate { };
    public static event Action OnUpdate = delegate { };
    public static event Action OnLateUpdate = delegate { };

    #endregion

    #region Utils

    private static readonly List<IStateQueue> stateQueues = new List<IStateQueue>();

    class Updater : MonoBehaviour
    {
        private void Awake()
        {
            StateBus.Awake();
        }

        private void Start()
        {
            StateBus.Start();
        }

        private void Update()
        {
            StateBus.Update();
        }

        private void LateUpdate()
        {
            StateBus.LateUpdate();
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        var updater = new GameObject() { name = "StateBusUpdater" };
        updater.AddComponent<Updater>();
        GameObject.DontDestroyOnLoad(updater);
        GameStateChanged += StateMachine.GameStates.Menu;
    }

    static void Awake()
    {
        stateQueues.Clear();

        foreach (var fi in typeof(StateBus).GetFields())
            if (typeof(IStateQueue).IsAssignableFrom(fi.FieldType))
            {
                IStateQueue stateQueue = fi.GetValue(null) as IStateQueue;
                //create StateQueue object, if not created
                if (stateQueue == null)
                {
                    stateQueue = Activator.CreateInstance(fi.FieldType) as IStateQueue;
                    fi.SetValue(null, stateQueue);
                }
                //save to queues list
                stateQueues.Add(stateQueue);
            }
        OnAwake();
    }

    static void Start()
    {
        OnStart();
    }

    static void Update()
    {
        OnUpdate();
    }

    static void LateUpdate()
    {
        OnLateUpdate();

        foreach (var queue in stateQueues)
            queue.Dequeue();
    }

    struct QueueItem<T>
    {
        public T Value;
        public float TimeToFire;
    }

    interface IStateQueue
    {
        void Dequeue();
    }

    /// <summary>
    /// Queue of states
    /// </summary>
    public class StateQueue<T> : IStateQueue
    {
        // Queue of events
        private Queue<QueueItem<T>> queue = new Queue<QueueItem<T>>();

        /// <summary>
        /// Current value of state (in current frame)
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Put event to queue
        /// </summary>
        public void Enqueue(T value, float deltaTime = 0)
        {
            queue.Enqueue(new QueueItem<T> { Value = value, TimeToFire = Time.time + deltaTime });
        }

        /// <summary>
        /// Implicit conversion to T
        /// </summary>
        public static implicit operator T(StateQueue<T> val)
        {
            return val.Value;
        }

        /// <summary>
        /// Conversion to true/false
        /// </summary>
        public static bool operator true(StateQueue<T> val)
        {
            return !val.Value.Equals(default(T));
        }

        /// <summary>
        /// Conversion to true/false
        /// </summary>
        public static bool operator false(StateQueue<T> val)
        {
            return val.Value.Equals(default(T));
        }

        /// <summary>
        /// Put event to queue via operator +
        /// </summary>
        public static StateQueue<T> operator +(StateQueue<T> vq, T val)
        {
            vq.Enqueue(val);
            return vq;
        }

        /// <summary>
        /// Clear queue, set default value
        /// </summary>
        public void Reset()
        {
            queue.Clear();
            Value = default(T);
        }

        void IStateQueue.Dequeue()
        {
            var count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                //get next event
                var item = queue.Dequeue();

                //time elapsed?
                if (item.TimeToFire <= Time.time)
                {
                    //set event to current value
                    Value = item.Value;
                    return;
                }

                //time is not elapsed => enqueue again
                queue.Enqueue(item);
            }

            //set default value
            Value = default(T);
        }
    }

    #endregion
}

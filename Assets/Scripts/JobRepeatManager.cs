using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
public class JobRepeatManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

    }
}
#endif

public class JobRepeatManager : MonoBehaviour
{
    public static JobRepeatManager Instance = null;

    public delegate void JobTodo();
    public delegate bool JobToDoCondition();
    public delegate bool JobAutoDropCondition();
    public delegate bool JobHoldingCondition();
    public delegate bool JobExceptionCondition();

    public enum JOB_STATE
    {
        NONE,
        JOB_STANBY,
        JOB_WORKING,
        JOB_WAITING,
        JOB_HOLDING,
        JOB_DROP,
    }

    [System.Serializable]
    public class JobBase
    {
        public string key;
        public float repeatDelay;
        public int repeatCount;
        public int excuteCount;
        public JobTodo jobTodo;
        public JobToDoCondition jobToDoCheck;
        public JobHoldingCondition jobHoldingCheck;
        public JobAutoDropCondition jobAutoDropCheck;
        public JobExceptionCondition jobExceptionCheck;
        public JOB_STATE state;
        public IEnumerator worker;
    }

    // Job Information Container - Job Data Struct
    [SerializeField] protected List<JobBase> JobList = new List<JobBase>();

    // Coroutine Worker Container
    protected Dictionary<string, Coroutine> JobWorkers = new Dictionary<string, Coroutine>();

    // Min DelayTime
    protected float m_MinDelayTime = 0.1f;
    public float MinDelayTime { get { return m_MinDelayTime; } set { m_MinDelayTime = value; } }

    public bool AddJob(string key, JobTodo toDo, float delay = 1.0f, int repeatCount = 0,
        JobToDoCondition toDoCondition = null, JobHoldingCondition holdingCondition = null, JobAutoDropCondition autoDropCondition = null, JobExceptionCondition exceptionCondition = null, bool isImmediately = true)
    {
        // Already Registered Job Check
        if (JobList.Find(job => job.key.Equals(key)) != null)
            return false;

        JobBase newJob = new JobBase
        {
            key = key,
            jobTodo = toDo,
            repeatDelay = delay,
            repeatCount = repeatCount,
            excuteCount = 0,
            jobToDoCheck = toDoCondition,
            jobHoldingCheck = holdingCondition,
            jobAutoDropCheck = autoDropCondition,
            jobExceptionCheck = exceptionCondition,
            state = JOB_STATE.JOB_STANBY,
            worker = CoJobHandle(key)
        };

        newJob.repeatDelay = newJob.repeatDelay < m_MinDelayTime ? m_MinDelayTime : newJob.repeatDelay;        
        JobList.Add(newJob);
        
        if (isImmediately)
        {
            StartCoroutine(newJob.worker);
        }

        return true;
    }

    private void Awake() { Instance = this; }

    private void Start()
    {
        StartCoroutine(CoDropWorkers());
    }

    public bool RemoveJob(string key)
    {
        JobBase findJob = JobList.Find(job => job.key.Equals(key));
        if (findJob == null)
            return false;

        return JobList.Remove(findJob);
    }

    public bool JobStart(string key)
    {
        JobBase findJob = JobList.Find(job => job.key.Equals(key));
        if (findJob == null)
            return false;

        StopCoroutine(findJob.worker);
        StartCoroutine(findJob.worker);
        return true;
    }

    public bool ChangeJobDelay(string key, float newDelay)
    {
        JobBase findJob = JobList.Find(job => job.key.Equals(key));
        if (findJob == null)
            return false;

        findJob.repeatDelay = newDelay;
        StopCoroutine(findJob.worker);
        StartCoroutine(findJob.worker);
        return true;
    }

    public bool ChangeRepeatCount(string key, int repeatCount)
    {
        JobBase findJob = JobList.Find(job => job.key.Equals(key));
        if (findJob == null)
            return false;

        findJob.repeatCount = repeatCount;
        StopCoroutine(findJob.worker);
        StartCoroutine(findJob.worker);
        return true;
    }

    public bool ChangeFunctionChain(string key, JobTodo Todo = null, JobToDoCondition toDoCheck = null,
        JobHoldingCondition holdingCondition = null, JobAutoDropCondition autoDropCondition = null, JobExceptionCondition exceptionCondition = null)
    {
        JobBase Job = JobList.Find(job => job.key.Equals(key));
        if (Job == null)
            return false;

        StopCoroutine(Job.worker);

        Job.jobTodo += Todo;
        Job.jobToDoCheck += toDoCheck;
        Job.jobHoldingCheck += holdingCondition;
        Job.jobAutoDropCheck += autoDropCondition;
        Job.jobExceptionCheck += exceptionCondition;
        return true;
    }

    public bool RemoveFunctionChain(string key, JobTodo Todo = null, JobToDoCondition toDoCheck = null,
        JobHoldingCondition holdingCondition = null, JobAutoDropCondition autoDropCondition = null, JobExceptionCondition exceptionCondition = null)
    {
        JobBase Job = JobList.Find(job => job.key.Equals(key));
        if (Job == null)
            return false;

        StopCoroutine(Job.worker);

        Job.jobTodo -= Todo;
        Job.jobToDoCheck -= toDoCheck;
        Job.jobHoldingCheck -= holdingCondition;
        Job.jobAutoDropCheck -= autoDropCondition;
        Job.jobExceptionCheck -= exceptionCondition;
        return true;
    }

    public JobBase GetJobBase(string key)
    {
        return JobList.Find(x => x.key == key);
    }

    private float DropManagingTime = 3.0f;
    private IEnumerator CoDropWorkers()
    {
        WaitForSeconds DropManagingDelay = new WaitForSeconds(DropManagingTime);
        while(gameObject.activeSelf)
        {
            JobList.RemoveAll(Job => Job.state == JOB_STATE.JOB_DROP);
            yield return DropManagingDelay;
        }
    }
    
    private IEnumerator CoJobHandle(string key)
    {
        yield return null;

        JobBase findJob = JobList.Find(x => x.key == key);
        if (findJob == null)
            yield break;
        
        switch (findJob.state)
        {
            case JOB_STATE.JOB_STANBY:
                if (findJob.jobExceptionCheck != null)
                {
                    while (findJob.jobExceptionCheck())
                        yield return null;
                }

                if(findJob.jobHoldingCheck != null)
                {
                    while(findJob.jobHoldingCheck())
                    {
                        findJob.state = JOB_STATE.JOB_HOLDING;
                        yield return null;
                    }
                    findJob.state = JOB_STATE.JOB_STANBY;
                }

                if(findJob.jobAutoDropCheck != null)
                {
                    if(findJob.jobAutoDropCheck())
                    {
                        findJob.state = JOB_STATE.JOB_DROP;
                        break;
                    }
                }

                if (findJob.jobToDoCheck != null)
                {
                    if(findJob.jobToDoCheck())
                    {
                        findJob.state = JOB_STATE.JOB_WORKING;
                        findJob.jobTodo();
                        findJob.excuteCount++;
                        if(findJob.excuteCount > findJob.repeatCount && findJob.repeatCount != 0)
                            findJob.state = JOB_STATE.JOB_DROP;
                        else
                            findJob.state = JOB_STATE.JOB_WAITING;
                    }
                }
                else
                {
                    findJob.state = JOB_STATE.JOB_WORKING;
                    findJob.jobTodo();
                    findJob.excuteCount++;
                    if (findJob.excuteCount > findJob.repeatCount && findJob.repeatCount != 0)
                        findJob.state = JOB_STATE.JOB_DROP;
                    else
                        findJob.state = JOB_STATE.JOB_WAITING;
                }
                break;
            case JOB_STATE.JOB_WAITING:
                WaitForSeconds WaitForDelay = new WaitForSeconds(findJob.repeatDelay);
                yield return WaitForDelay;
                findJob.state = JOB_STATE.JOB_STANBY;
                break;
            case JOB_STATE.JOB_DROP:
                yield break;
        }

        yield return StartCoroutine(CoJobHandle(findJob.key));
    }    
}

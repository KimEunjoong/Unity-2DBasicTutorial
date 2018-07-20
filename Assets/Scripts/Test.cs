using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public bool A_IsTodoCondition = true;
    public bool A_IsHolding = false;
    public bool A_IsAutoDrop = false;
    public bool A_IsException = false;

    public bool B_IsTodoCondition = true;
    public bool B_IsHolding = false;
    public bool B_IsAutoDrop = false;
    public bool B_IsException = false;

    void Start () {

        JobRepeatManager.Instance.AddJob(string.Format("RepeatJob{0}", 1),
            OnExcuteSample_A, 2.0f, 0,
            () => { return A_IsTodoCondition; },
            () => { return A_IsHolding; },
            () => { return A_IsAutoDrop; },
            () => { return A_IsException; });

        JobRepeatManager.Instance.AddJob(string.Format("RepeatJob{0}", 2),
            OnExcuteSample_B, 2.0f, 0,
            () => { return B_IsTodoCondition; },
            () => { return B_IsHolding; },
            () => { return B_IsAutoDrop; },
            () => { return B_IsException; });
    }

    private void OnExcuteSample_A()
    {
        Debug.LogFormat("Repeat_Index : A");
    }

    private void OnExcuteSample_B()
    {
        Debug.LogFormat("Repeat_Index : B");
    }
}

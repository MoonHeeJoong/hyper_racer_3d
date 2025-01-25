
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProvider
{
    float GetAxis(string axisName);
}

public class InputProvider : IInputProvider
{
    public float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName); // 실제 Unity Input API 호출
    }
}

public class MockInputProvider : IInputProvider
{
    private Dictionary<string, float> axisValues = new Dictionary<string, float>();

    public void SetAxisValue(string axisName, float value)
    {
        axisValues[axisName] = value;
    }

    public float GetAxis(string axisName)
    {
        return axisValues.ContainsKey(axisName) ? axisValues[axisName] : 0f;
    }
}

public class InputHandler : Singleton<InputHandler>
{
    IInputProvider inputProvider;
    public Vector2 MovementInput { get; protected set; } 

    [NonSerialized] public Action OnActions; 

    void Start()
    {
        if (inputProvider == null)
            inputProvider = new InputProvider();

        OnActions += DirectControl;
    }

    protected virtual void Update()
    {
        OnActions?.Invoke();
    }

    void DirectControl()
    {
        float horizontal = inputProvider.GetAxis("Horizontal");
        float vertical = inputProvider.GetAxis("Vertical");
        
        MovementInput = new Vector2(horizontal,vertical);
        
    }

    public void SetInputProvider(IInputProvider provider)
    {
        inputProvider = provider;
    }
}

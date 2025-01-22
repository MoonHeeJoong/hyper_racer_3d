
using System;
using System.Collections;
using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{
    public Vector2 MovementInput { get; protected set; } 

    [NonSerialized] public Action OnActions; 
  
    void Start()
    {
        OnActions += DirectControl;
    }

    protected virtual void Update()
    {
        OnActions?.Invoke();
    }

    void DirectControl()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        MovementInput = new Vector2(horizontal,vertical);
    }
}

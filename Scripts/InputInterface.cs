using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInterface : MonoBehaviour
{
    BasicControl control;

    public Vector2 leftAnalog;
    public Dictionary<string, bool> onButtonHold = new Dictionary<string, bool>();
    public Dictionary<string, bool> onButtonDown = new Dictionary<string, bool>();
    public Dictionary<string, bool> onButtonUp = new Dictionary<string, bool>();

    private void Awake()
    {
        control = new BasicControl();
    }

    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in control.Basic.Get().actions)
        {
            onButtonHold.Add(item.name, false);
            onButtonDown.Add(item.name, false);
            onButtonUp.Add(item.name, false);

            item.performed += delegate { onButtonHold[item.name] = true; };
            item.canceled += delegate { onButtonHold[item.name] = false; };

            item.canceled += delegate { onButtonUp[item.name] = true; };
        }
    }

    // Update is called once per frame
    void Update()
    {
        leftAnalog = control.Basic.LeftAnalog.ReadValue<Vector2>();

        foreach (var item in control.Basic.Get().actions)
        {
            onButtonDown[item.name] = item.triggered;
        }
    }

    private void LateUpdate()
    {
        foreach (var item in control.Basic.Get().actions)
        {
            onButtonUp[item.name] = false;
        }
    }
}

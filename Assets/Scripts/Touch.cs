using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Touch : MonoBehaviour
{
    [SerializeField] Camera cam;
    MainInputActions inputActions;
    Touchscreen touch;

    Circle circle;

    bool started = false;
    private void Start()
    {
        touch = Touchscreen.current;
        print("touch is null: " + touch == null);
        inputActions = new MainInputActions();
        inputActions.Player.Look.performed += BeginTouch;
        inputActions.Player.Look.performed += Move;
        
        inputActions.Enable();
    }
    private void Update()
    {
        if(inputActions.Player.Fire.phase != InputActionPhase.Started && inputActions.Player.Fire.phase != InputActionPhase.Performed && started)
            EndTouch();
    }
    void BeginTouch(InputAction.CallbackContext context)
    {
        if (started)
            return;

        print("started");
        started = true;
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y,0)), Vector2.zero, ContactFilter2D.NormalAngleUpperLimit);
        print("hits: " + hits.Length);
        if (hits != null)
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!hits[i].collider.gameObject.CompareTag("Circle"))
                    {
                        continue;
                    }
                    if (circle != null)
                        circle.rb.velocity = Vector2.zero;

                    hits[i].collider.gameObject.TryGetComponent(out circle);

                }
            }
        
    }
    void Move(InputAction.CallbackContext context)
    {
        print("performed");
        if (circle != null)
        {
            if(circle.IsTouchable)
            circle.rb.MovePosition(cam.ScreenToWorldPoint(context.ReadValue<Vector2>()) + Vector3.forward * 10);
            //circle.rb.velocity = cam.ScreenToWorldPoint(Input.GetTouch(0).position) + Vector3.forward * 10 - circle.transform.position;
        }
    }
    void EndTouch()
    {
        print("cancelled");
        started = false;
        circle = null;
    }
}

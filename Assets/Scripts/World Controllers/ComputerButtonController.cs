using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ComputerButtonController : MonoBehaviour
{
    public GameObject ButtonUp;
    public GameObject ButtonDown;
    private bool pressed;
    void Start()
    {
        pressed = false;
        buttonReady(true);
    }

    void buttonReady(bool newBool)
    {
        ButtonUp.gameObject.SetActive(newBool);
        ButtonDown.gameObject.SetActive(!newBool);
    }
    void Update()
    {
        if(Input.GetMouseButton(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                if (hit.collider.gameObject.Equals(ButtonUp))
                {
                    pressed = true;
                    buttonReady(false);
                }
                if (hit.collider.gameObject.Equals(ButtonDown))
                {
                    pressed = true;
                }
            }
        }
        else if (pressed)
        {
            pressed = false;
            buttonReady(true);
            ViewController.Instance.goToRoom(false);
        }
    }
}

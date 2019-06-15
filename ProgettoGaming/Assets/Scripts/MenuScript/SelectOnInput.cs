using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{

    [SerializeField] protected EventSystem eventSystem;
    [SerializeField] protected GameObject selectedObject;

    private bool buttonSelected;

    // Use this for initialization
    void Start()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        buttonSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
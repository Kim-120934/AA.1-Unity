using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLauncher : MonoBehaviour
{
    //We save here the original object that we want to duplicate
    public GameObject prefab;
    //How far the object should appear
    public float positionOffset = 2;
    //We reserve the required memory to use the input manager
    InputManager input;
    private void Start()
    {
        //We create the input manager
        input = new InputManager();
        //We enable the inputs related to the gameplay
        input.Player.Enable();
    }
    void Update()
    {
        //We check if the Interact key was pressed this frame or not. If it does, we duplicate the object into the scene
        if (input.Player.Interact.WasPressedThisFrame())
        {
            //The Instantiate function is used to duplicate an object, we always need to provide it with:
            //The object to duplicate
            //The position where the duplicated object must be put on
            //The rotation on wich the duplicated object will appear
            GameObject instance = Instantiate(prefab, transform.position + transform.forward * positionOffset, transform.rotation);
            //The Instantiate function always returns the duplicated object, and we can save it to access the new object later on
        }
    }
}

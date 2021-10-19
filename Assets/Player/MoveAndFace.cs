using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndFace : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed = 2.0f;
    public Vector3 offset=new Vector3(0,10,2);
    private new Camera camera;
    //直接进行一个抄
    void Start()
    {
        camera = Camera.main;
        controller = gameObject.GetComponent<CharacterController>();
    }
    
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
        }
    }

    void LateUpdate() {
        camera.transform.position = transform.position + offset;
    }
}

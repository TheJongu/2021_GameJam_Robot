using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Robot_Small_Movement : MonoBehaviour
{
    // Keycodes, defines the key for movement
    public KeyCode keyCode_up;
    public KeyCode keyCode_down;
    public KeyCode keyCode_left;
    public KeyCode keyCode_right;

    private bool isMoving;
    private Vector3 orgPos, targetPos;
    private float timePerMove = 0.2f;
    // How many moves are still possible?
    private int charge = 100;

    private IEnumerable coroutine;


    public BoxCollider2D boxedCollider;
    public TilemapCollider2D collisionTileMap;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(keyCode_up) && !isMoving && charge > 0){
            StartCoroutine("MovePlayer",Vector3.up);
        }
        if(Input.GetKey(keyCode_down) && !isMoving && charge > 0){
            StartCoroutine("MovePlayer",Vector3.down);
        }
        if(Input.GetKey(keyCode_left) && !isMoving && charge > 0){
            StartCoroutine("MovePlayer",Vector3.left);
        }
        if(Input.GetKey(keyCode_right) && !isMoving && charge > 0){
            StartCoroutine("MovePlayer",Vector3.right);
        }
    }
    private IEnumerator MovePlayer(Vector3 aDirection){
        
        
        isMoving = true;
        float timePassed = 0;
        orgPos = transform.position;
        targetPos = orgPos + aDirection;

        if(collisionTileMap.IsTouching(boxedCollider)){
            print("Is Touching");
        }

        while(timePassed < timePerMove){
            transform.position = Vector3.Lerp(orgPos, targetPos, (timePassed / timePerMove));
            timePassed += Time.deltaTime;
            yield return null;
        }
        // At the end of the Movement, set Target to be exact
        transform.position = targetPos;
        charge--;

        isMoving = false;
    }
}

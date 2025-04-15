using UnityEngine;

public class Player1Move : PlayerMove
{
    void Start()
    {
        moveUp = KeyCode.W;
        moveDown = KeyCode.S;
        moveLeft = KeyCode.A;
        moveRight = KeyCode.D;
        dashKey = KeyCode.LeftShift;
    }
}



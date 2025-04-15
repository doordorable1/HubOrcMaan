using UnityEngine;

public class Player2Move : PlayerMove
{
    void Start()
    {
        moveUp = KeyCode.UpArrow;
        moveDown = KeyCode.DownArrow;
        moveLeft = KeyCode.LeftArrow;
        moveRight = KeyCode.RightArrow;
        dashKey = KeyCode.Period;
    }
}
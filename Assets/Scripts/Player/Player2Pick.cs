using UnityEngine;

public class Player2Pick : PlayerPick
{
    protected override void Start()
    {
        base.Start();
        pickKey = KeyCode.Slash;
    }
}
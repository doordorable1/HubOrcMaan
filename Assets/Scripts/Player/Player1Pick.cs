using UnityEngine;

public class Player1Pick : PlayerPick
{
    protected override void Start()
    {
        base.Start();
        // Player1만의 추가 설정 (예: 픽업 키를 Space로)
        pickKey = KeyCode.Space;
    }
}



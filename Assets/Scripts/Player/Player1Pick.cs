using UnityEngine;

public class Player1Pick : PlayerPick
{
    protected override void Start()
    {
        base.Start();
        // Player1���� �߰� ���� (��: �Ⱦ� Ű�� Space��)
        pickKey = KeyCode.Space;
    }
}



using UnityEngine;

public class Player1Hp : PlayerHp
{
    protected override void Start()
    {
        base.Start();
        // Player1���� �߰� �ʱ�ȭ�� �ʿ��ϸ� ���⿡ �ۼ�
    }

    protected override void Die()
    {
        base.Die();
        // Player1�� ��� �� �߰� ������ �ִٸ� ���� (��: UI ǥ�� ��)
    }
}

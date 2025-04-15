using UnityEngine;

public class Player1Hp : PlayerHp
{
    protected override void Start()
    {
        base.Start();
        // Player1만의 추가 초기화가 필요하면 여기에 작성
    }

    protected override void Die()
    {
        base.Die();
        // Player1의 사망 시 추가 로직이 있다면 구현 (예: UI 표시 등)
    }
}

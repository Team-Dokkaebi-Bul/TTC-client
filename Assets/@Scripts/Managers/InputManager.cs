using System;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;

    public float MoveInput { get; private set; }
    public bool IsJumpPressed { get; private set; }

    public void OnUpdate()
    {
        // 이동 입력 처리
        MoveInput = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) MoveInput = -1;
        if (Input.GetKey(KeyCode.RightArrow)) MoveInput = 1;

        // 점프 입력 처리
        IsJumpPressed = Input.GetKeyDown(KeyCode.Space);

        if (KeyAction != null)
            KeyAction.Invoke();
    }

    // 스킬 입력 확인 메서드들
    public bool IsAttackPressed() => Input.GetKeyDown(KeyCode.A);
    public bool IsDashPressed() => Input.GetKeyDown(KeyCode.S);
    public bool IsSlidePressed() => Input.GetKeyDown(KeyCode.D);

    // 스탯창 토글
    public bool IsStatWindowTogglePressed()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }
}
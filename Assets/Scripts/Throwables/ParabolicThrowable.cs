using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicThrowable : Throwable
{
    [SerializeField] float gravityEffect = 0.1f;
    protected override void Move() {
        base.Move();

        this.initialDirection += Physics2D.gravity * Time.deltaTime * gravityEffect;
    }
}

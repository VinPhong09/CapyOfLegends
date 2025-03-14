using System;
using UnityEngine;

public class Boar : Enemy
{
    public override void Initialize(Vector3 initPos, Action diedActions, float difficultyRatio)
    {
        base.Initialize(initPos, diedActions, difficultyRatio);
    }
}

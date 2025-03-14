using System;
using UnityEngine;

public class Minotaur : Enemy
{
    public override void Initialize(Vector3 initPos, Action diedActions, float difficultyRatio)
    {
        base.Initialize(initPos, diedActions, difficultyRatio);
    }
}

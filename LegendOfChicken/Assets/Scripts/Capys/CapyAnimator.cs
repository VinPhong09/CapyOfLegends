using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class CapyAnimator : ChickenAnimator
{
   public SkeletonAnimation Head;
   public SkeletonAnimation Body;
   public SkeletonAnimation Legs;
   public SkeletonAnimation Hat_Access;
   public SkeletonAnimation Body_Access;
   public void Start()
   {
      OnPlayIdleAnimation();
   }

   public override void OnPlayAttackAnimation()
   {
      base.OnPlayAttackAnimation();
      ApplyAnimation("hover_ok");
   }

   public override void OnPlayRunAnimation()
   {
      ApplyAnimation("run");
   }

   public override void OnPlayIdleAnimation()
   {
      ApplyAnimation("idle");
   }

   private void ApplyAnimation(string animationName)
   {
      Head.AnimationName = animationName;
      Body.AnimationName = animationName;
      Legs.AnimationName = animationName;
      Hat_Access.AnimationName = animationName;
      Body_Access.AnimationName = animationName;
   }

}

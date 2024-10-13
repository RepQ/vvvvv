using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface Followers
{
    public void FollowTo(Vector3 target);
    public Vector3 CalculateTargetPosition();
}

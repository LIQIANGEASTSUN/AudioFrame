using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect3DFollow : SoundEffect3D
{
    private Transform _target;
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public override void Update()
    {
        base.Update();
        Follow();
    }

    private void Follow()
    {
        if (null == _target)
        {
            return;
        }

        SetPosition(_target.position);
    }
}

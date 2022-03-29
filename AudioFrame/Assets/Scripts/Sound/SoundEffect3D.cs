using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D 音效
/// </summary>
public class SoundEffect3D : SoundBase
{
    protected Vector3 _position;
    protected override void InitAudioSource()
    {
        SetPosition(_position);
        base.InitAudioSource();
    }

    public void PositionProperty(Vector3 position)
    {
        _position = position;
    }

    public override void SetSpatialBlend()
    {
        _audioSource.spatialBlend = 1;
    }

    protected void SetPosition(Vector3 position)
    {
        _audioSource.gameObject.transform.position = _position;
    }
}

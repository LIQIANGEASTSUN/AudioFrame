using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2D 音效
/// </summary>
public class SoundEffect2D : SoundBase
{
    protected override void InitAudioSource()
    {
        base.InitAudioSource();
    }

    public override void SetSpatialBlend()
    {
        _audioSource.spatialBlend = 0;
    }

    public override bool End()
    {
        return base.End();
    }

}

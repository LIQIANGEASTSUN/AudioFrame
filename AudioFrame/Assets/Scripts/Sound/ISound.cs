using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISound
{

    int ID();

    void Play(string audio, bool loop, float volume);

    void Stop();

    void Pause();

    void UnPause();

    void Release();

    void Update();

    bool End();

}

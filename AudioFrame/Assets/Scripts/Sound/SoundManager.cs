using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonObject<SoundManager>
{
    private int _soundBgID = -1;
    private List<ISound> _soundList = new List<ISound>();

    private static int _instanceID = 0;

    public int PlayAudio2D(string audio, bool loop, float volume)
    {
        ISound sound = CreateSound<SoundEffect2D>();
        sound.Play(audio, loop, volume);
        return sound.ID();
    }

    public int PlayAudio3D(Vector3 position, string audio, bool loop, float volume)
    {
        SoundEffect3D sound = CreateSound<SoundEffect3D>();
        sound.PositionProperty(position);
        sound.Play(audio, loop, volume);
        return sound.ID();
    }

    public int PlayAudio3DFollow(string audio, bool loop, float volume)
    {
        ISound sound = CreateSound<SoundEffect3DFollow>();
        sound.Play(audio, loop, volume);
        return sound.ID();
    }

    public int PlayBG(string audio, bool loop, float volume)
    {
        ISound sound = GetSound(_soundBgID);
        if (null == sound)
        {
            sound = CreateSound<SoundBackground>();
            _soundBgID = sound.ID();
        }
        else
        {
            sound.Stop();
        }
        sound.Play(audio, loop, volume);
        return sound.ID();
    }

    public void Stop(int id)
    {
        ISound sound = GetSound(id);
        if (null != sound)
        {
            sound.Stop();
        }
    }

    public void Pause(int id)
    {
        ISound sound = GetSound(id);
        if (null != sound)
        {
            sound.Pause();
        }
    }

    public void UnPause(int id)
    {
        ISound sound = GetSound(id);
        if (null != sound)
        {
            sound.UnPause();
        }
    }

    public void StopAll()
    {
        for (int i = 0; i < _soundList.Count; ++i)
        {
            _soundList[i].Stop();
        }
    }

    public void PauseAll()
    {
        for (int i = 0; i < _soundList.Count; ++i)
        {
            _soundList[i].Pause();
        }
    }

    public void UnPauseAll()
    {
        for (int i = 0; i < _soundList.Count; ++i)
        {
            _soundList[i].UnPause();
        }
    }

    public void Update()
    {
        for (int i = _soundList.Count - 1; i >= 0; --i)
        {
            ISound sound = _soundList[i];
            sound.Update();
            if (sound.End())
            {
                sound.Release();
                _soundList.RemoveAt(i);
            }
        }
    }

    public void Release()
    {
        for (int i = _soundList.Count - 1; i >= 0; --i)
        {
            _soundList[i].Release();
        }
        _soundList.Clear();
    }

    private T CreateSound<T>() where T : SoundBase, new()
    {
        T t = new T();
        int instanceId = _instanceID++;
        t.SetID(instanceId);
        _soundList.Add(t);
        return t;
    }

    private ISound GetSound(int id)
    {
        ISound sound = null;
        int left = 0;
        int right = _soundList.Count - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (_soundList[mid].ID() > id)
            {
                right = mid - 1;
            }
            else if (_soundList[mid].ID() == id)
            {
                sound = _soundList[mid];
                break;
            }
            else
            {
                right = mid + 1;
            }
        }
        return sound;
    }

}

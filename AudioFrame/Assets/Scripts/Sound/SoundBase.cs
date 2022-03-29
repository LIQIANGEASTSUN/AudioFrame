using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundBase : ISound
{
    protected int _id;
    protected SoundHandle _soundState;
    protected AudioClip _audioClip;
    protected AudioSource _audioSource;
    protected string _soundPrefabName = "AudioSound";
    protected float _remainingTime = 0;
    protected bool _loop;
    protected float _volume;

    protected Queue<SoundHandle> _queue = new Queue<SoundHandle>();

    public void SetID(int id)
    {
        _id = id;
    }

    public int ID()
    {
        return _id;
    }

    protected virtual void InitAudioSource()
    {
        _audioSource.clip = _audioClip;
        _audioSource.volume = _volume;
        _remainingTime = _audioClip.length;
        SetSpatialBlend();
        NextHandle();
    }

    public virtual void Play(string audio, bool loop, float volume)
    {
        Debug.LogError("Play");
        _queue.Enqueue(SoundHandle.Play);
        _audioClip = null;
        _loop = loop;
        _volume = volume;
        if (null == _audioClip)
        {
            LoadAudioClip(audio);
        }
        if (null == _audioSource)
        {
            LoadSoundPrefab();
        }
        NextHandle();
    }

    public virtual void Stop()
    {
        Debug.LogError("Stop");
        _queue.Enqueue(SoundHandle.Stop);
        NextHandle();
    }

    public virtual void Pause()
    {
        Debug.LogError("Pause");
        _queue.Enqueue(SoundHandle.Pause);
        NextHandle();
    }

    public virtual void UnPause()
    {
        Debug.LogError("UnPause");
        _queue.Enqueue(SoundHandle.UnPause);
        NextHandle();
    }

    protected void NextHandle()
    {
        if (null == _audioSource || null == _audioClip)
        {
            return;
        }

        while (_queue.Count > 0)
        {
            SoundHandle handle = _queue.Dequeue();
            if (handle == SoundHandle.Play)
            {
                _audioSource.Play();
                _audioSource.loop = _loop;
                _soundState = SoundHandle.Play;
            }
            else if (handle == SoundHandle.Stop)
            {
                _audioSource.Stop();
                _soundState = SoundHandle.Stop;
            }
            else if (handle == SoundHandle.Pause)
            {
                _audioSource.Pause();
                _soundState = SoundHandle.Pause;
            }
            else if (handle == SoundHandle.UnPause)
            {
                _audioSource.UnPause();
                _soundState = SoundHandle.Play;
            }
            //break;
        }
    }

    public abstract void SetSpatialBlend();

    public virtual void Release()
    {
        Stop();
        if (null != _audioSource)
        {
            GameObject.Destroy(_audioSource.gameObject);
        }
        _audioSource = null;
        _audioClip = null;
        Debug.LogError("Release");
    }

    public virtual void Update()
    {
        if (null == _audioSource || null == _audioClip)
        {
            return;
        }
        _remainingTime -= Time.deltaTime;
    }

    public virtual bool End()
    {
        if (_soundState == SoundHandle.Stop)
        {
            return true;
        }
        if (_loop)
        {
            return false;
        }

        return _remainingTime <= 0;
    }

    private void LoadComplete()
    {
        if (null == _audioSource || null == _audioClip)
        {
            return;
        }
        InitAudioSource();
    }

    protected void LoadAudioClip(string audio)
    {
        _remainingTime = float.MaxValue;
        ResourcesManager.GetInstance().LoadAsync<AudioClip>(audio, LoadAudioComplete);
    }

    protected void LoadAudioComplete(AudioClip audioClip)
    {
        if (End())
        {
            return;
        }
        _audioClip = audioClip;
        LoadComplete();
    }

    protected void LoadSoundPrefab()
    {
        ResourcesManager.GetInstance().LoadAsync<GameObject>(_soundPrefabName, LoadPrefabComplete);
    }

    protected void LoadPrefabComplete(GameObject go)
    {
        if (End())
        {
            return;
        }
        go = GameObject.Instantiate(go);
        _audioSource = go.GetComponent<AudioSource>();
        LoadComplete();
    }
}

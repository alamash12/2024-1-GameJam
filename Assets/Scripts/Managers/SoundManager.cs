using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sounds.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    float _bgmvolume = 1.0f;
    float _sfxvolume = 1.0f;

    public float BGMVolume { get { return _bgmvolume; } set { _bgmvolume = value; PlayerPrefs.SetFloat("BGMVolume", value >= 1 ? 1 : value); } }
    public float SFXVolume { get { return _sfxvolume; } set { _sfxvolume = value; PlayerPrefs.SetFloat("SFXVolume", value >= 1 ? 1 : value); } }

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            UnityEngine.Object.DontDestroyOnLoad(root);

            for (Define.Sounds s = Define.Sounds.BGM; s < Define.Sounds.MaxCount; s++)
            {
                GameObject go = new GameObject { name = $"{s}" };
                _audioSources[(int)s] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _bgmvolume = PlayerPrefs.GetFloat("BGMVolume", 0.7f);
            _sfxvolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
            _audioSources[(int)Define.Sounds.BGM].loop = true;
        }
        else
        {
            for (Define.Sounds s = Define.Sounds.BGM; s < Define.Sounds.MaxCount; s++)
            {
                GameObject go = root.transform.Find($"@{s}").gameObject;
                _audioSources[(int)s] = go.GetComponent<AudioSource>();
            }

            _audioSources[(int)Define.Sounds.BGM].loop = true;
        }

        _bgmvolume = PlayerPrefs.GetFloat("BGMVolume", 1f); // ���� ���۽� �÷��̾��������� ���� �ҷ���
        _sfxvolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetVolume(Define.Sounds.BGM, _bgmvolume); // �ҷ��� ������ ������ҽ� ���� ����
        SetVolume(Define.Sounds.SFX, _sfxvolume);

    }

    /// <summary>
    /// SFX�� PlayOneShot���� ���� 
    /// </summary>
    /// <param name="SFXSound"> Define.SFX Enum ���� �������⸦ �ٶ� </param>
    /// <param name="volume"></param>

    public void Play(Define.SFX SFXSound)
    {
        string path = $"{SFXSound}";
        AudioClip audioClip = GetOrAddAudioClip(path, Define.Sounds.SFX);
        Play(audioClip, Define.Sounds.SFX);
    }
    /// <summary>
    /// BGM�� Play�� ����
    /// </summary>
    /// <param name="BGMSound">Define.BGM Enum ���� �������⸦ �ٶ� </param>
    /// <param name="volume"></param>
    public void Play(Define.BGM BGMSound)
    {
        string path = $"{BGMSound}";
        AudioClip audioClip = GetOrAddAudioClip(path, Define.Sounds.BGM);
        Play(audioClip, Define.Sounds.BGM);
    }

    void Play(AudioClip audioClip, Define.Sounds type = Define.Sounds.SFX)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sounds.BGM)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sounds.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sounds.SFX];
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sounds type = Define.Sounds.SFX)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        AudioClip audioClip = null;

        if (type == Define.Sounds.BGM)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");
        return audioClip;
    }

    public void SetVolume(Define.Sounds type, float volume)
    {
        _audioSources[(int)type].volume = volume;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
    public void ButtonClick()
    {
        //Play(Define.SFX.ButtonClick);
    }
}

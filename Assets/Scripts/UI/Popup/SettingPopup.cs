using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingPopup : UI_Popup
{
    enum Sliders
    {
        BgmSlider,
        SfxSlider,
    }

    private void Start()
    {
        Bind<Slider>(typeof(Sliders));

        GetSlider((int)Sliders.BgmSlider).gameObject.AddUIEvent(delegate { VolumeChange(Define.Sounds.BGM); });
        GetSlider((int)Sliders.SfxSlider).gameObject.AddUIEvent(delegate { VolumeChange(Define.Sounds.SFX); });

        GetSlider((int)Sliders.BgmSlider).value = Managers.Sound.BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
        GetSlider((int)Sliders.SfxSlider).value = Managers.Sound.SFXVolume = PlayerPrefs.GetFloat("SFXVolume");

    }

    public override void Init()
    {
        base.Init();
        
    }

    void VolumeChange(Define.Sounds Sound)
    {
        float volume;
        if (Sound == Define.Sounds.BGM)
        {
            volume = Get<Slider>((int)Sliders.BgmSlider).value;
            Managers.Sound.BGMVolume = volume;
        }
        else
        {
            volume = Get<Slider>((int)Sliders.SfxSlider).value;
            Managers.Sound.SFXVolume = volume;
        }

        Managers.Sound.SetVolume(Sound, volume);
    }
}

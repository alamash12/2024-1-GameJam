using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Option : UI_Popup
{
    enum Sliders
    {
        BgmSlider,
        SfxSlider,
    }
    enum Buttons
    {
        Close
    }

    private void Start()
    {
        Init();
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.Close).gameObject.AddUIEvent(CloseButtonClicked);

        GetSlider((int)Sliders.BgmSlider).gameObject.AddUIEvent(delegate { VolumeChange(Define.Sounds.BGM); }, Define.UIEvent.PointerUP);
        GetSlider((int)Sliders.SfxSlider).gameObject.AddUIEvent(delegate { VolumeChange(Define.Sounds.SFX); }, Define.UIEvent.PointerUP);

        GetSlider((int)Sliders.BgmSlider).value = Managers.Sound.BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
        GetSlider((int)Sliders.SfxSlider).value = Managers.Sound.SFXVolume = PlayerPrefs.GetFloat("SFXVolume");

        Managers.Sound.SetVolume(Define.Sounds.BGM, Managers.Sound.BGMVolume);
        Managers.Sound.SetVolume(Define.Sounds.SFX, Managers.Sound.SFXVolume);
    }

    public override void Init()
    {
        base.Init();
        
    }

    void CloseButtonClicked(PointerEventData eventData)
    {
        Managers.UI.ClosePopUpUI();
        Managers.Sound.Play(Define.SFX.Button);
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

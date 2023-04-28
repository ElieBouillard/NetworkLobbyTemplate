using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsPanel : Panel
{
    [Header("References")] 
    [SerializeField] private AudioMixer _audioMixer;
    
    [Header("Text")]
    [SerializeField] private TMP_Text _masterVolumeText;
    [SerializeField] private TMP_Text _musicVolumeText;
    [SerializeField] private TMP_Text _sfxVolumeText;

    [Header("Slider")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    
    [SerializeField] private Button _returnButton;

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    protected override void Awake()
    {
        base.Awake();
        
        if(PlayerPrefs.HasKey(MasterVolumeKey))
            _masterVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(MasterVolumeKey));
        
        if(PlayerPrefs.HasKey(MusicVolumeKey))
            _musicVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(MusicVolumeKey));
        
        if(PlayerPrefs.HasKey(SfxVolumeKey))
            _sfxVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(SfxVolumeKey));
    }

    protected override void AssignButtonsReference()
    {
        _masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        _sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        _returnButton.onClick.AddListener(OnCLickReturn);
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
        _audioMixer.SetFloat(MasterVolumeKey, Mathf.Log10(value) * 20);
        _masterVolumeText.text = (value * 100f).ToString("000");
    }
    
    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        _audioMixer.SetFloat(MusicVolumeKey, Mathf.Log10(value) * 20);
        _musicVolumeText.text = (value * 100f).ToString("000");
    }
    
    public void SetSfxVolume(float value)
    {
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
        _audioMixer.SetFloat(SfxVolumeKey, Mathf.Log10(value) * 20);
        _sfxVolumeText.text = (value * 100f).ToString("000");
    }
    
    protected void OnCLickReturn()
    {
        PanelManager.Instance.EnablePanel(PanelType.MainMenu);
    }
}
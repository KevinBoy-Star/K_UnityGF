using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AVVideoPlay : MonoBehaviour
{
    #region[Field]

    private MediaPlayer _mediaPlayer;
    private DisplayUGUI _mediaDisplay;
    private Text _timeText;                     //时间文本
    private Slider _videoSeekSlider;            //进度条
    private Button _functionButton;             //功能按钮 控制播放或暂停
    private int _currentMinute;                 //分 源
    private int _currentSecond;                 //秒 源
    private int _realMinute;                    //分 实时
    private int _realSecond;                    //秒 实时
    private float _setVideoSeekSliderValue;     //进度条数值
    private bool _wasPlayingOnScrub;
    private readonly string _folder = "Videos/";//文件夹名称
    public string _videoFile;                   //文件名称
    public Sprite[] _buttonSprites;             //功能按钮精灵图片
    private Image _buttonImage;                 //功能按钮图片

    #endregion

    private void Awake()
    {
        _mediaPlayer = GetComponent<MediaPlayer>();
        _mediaDisplay = GetComponentInChildren<DisplayUGUI>();
        _videoSeekSlider = GetComponentInChildren<Slider>();
        _timeText = GetComponentInChildren<Text>();
        _functionButton = GetComponentInChildren<Button>();
        _buttonImage = _functionButton.GetComponent<Image>();
    }

    private void Start()
    {
        _videoSeekSlider.onValueChanged.AddListener(OnVideoSeekSlider);
        _functionButton.onClick.AddListener(VideoPlayingState);
        _mediaPlayer.Events.AddListener(OnVideoEvent);
        _mediaPlayer.m_VideoPath = System.IO.Path.Combine(_folder, _videoFile);
        _mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, _mediaPlayer.m_VideoPath, true);
    }

    private void OnEnable()
    {
        _buttonImage.sprite = _buttonSprites[0];
    }

    private void OnDestroy()
    {
        _mediaPlayer.Events.RemoveListener(OnVideoEvent);
    }

    void Update()
    {
        if (_mediaPlayer && _mediaPlayer.Info != null && _mediaPlayer.Info.GetDurationMs() > 0f)
        {
            //视频现在的时间 毫秒
            float time = _mediaPlayer.Control.GetCurrentTimeMs();

            //视频的总时间 毫秒
            float duration = _mediaPlayer.Info.GetDurationMs();
            float d = Mathf.Clamp(time / duration, 0.0f, 1.0f);

            UpdateVideoTime(duration, time);

            _setVideoSeekSliderValue = d;
            _videoSeekSlider.value = d;
        }
    }

    /// <summary>
    /// 更新视频时间
    /// </summary>
    /// <param name="_currenTime"></param>
    /// <param name="_realTime"></param>
    private void UpdateVideoTime(float _currentTime,float _realTime)
    {
        int m_currentTime = (int)_currentTime / 1000;
        int m_realTime = (int)_realTime / 1000;
        _currentMinute = m_currentTime / 60;
        _currentSecond = m_currentTime % 60;
        _realMinute = m_realTime / 60;
        _realSecond = m_realTime % 60;
        _timeText.text = string.Format("{0:D2}:{1:D2}/{2:D2}:{3:D2}", _currentMinute, _currentSecond, _realMinute, _realSecond);
    }

    /// <summary>
    /// 视频播放状态
    /// </summary>
    private void VideoPlayingState()
    {
        if(_buttonImage.sprite.Equals(_buttonSprites[0]))
        {
            _buttonImage.sprite = _buttonSprites[1];
            _mediaPlayer.Control.Pause();
        }
        else
        {
            _buttonImage.sprite = _buttonSprites[0];
            _mediaPlayer.Control.Play();
        }
    }

    public void OnVideoSeekSlider(float _value)
    {
        if (_mediaPlayer && _videoSeekSlider && _videoSeekSlider.value != _setVideoSeekSliderValue)
        {
            _mediaPlayer.Control.Seek(_videoSeekSlider.value * _mediaPlayer.Info.GetDurationMs());
        }
    }

    public void OnVideoSliderDown()
    {
        _wasPlayingOnScrub = _mediaPlayer.Control.IsPlaying();
        if (_wasPlayingOnScrub)
        {
            _mediaPlayer.Control.Pause();
        }
        OnVideoSeekSlider(_videoSeekSlider.value);
    }

    public void OnVideoSliderUp()
    {
        if (_mediaPlayer && _wasPlayingOnScrub)
        {
            _mediaPlayer.Control.Play();
            _wasPlayingOnScrub = false;
        }
    }

    public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.FirstFrameReady:
                _mediaPlayer.Play();
                _mediaDisplay.CurrentMediaPlayer = _mediaPlayer;
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                SceneManager.LoadScene("Game");
                break;
        }
    }
}

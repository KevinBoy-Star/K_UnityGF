using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerDetection : MonoBehaviour
{
    public string videoName;                     //视频名称
    private RawImage videoImage;                 //rawImage
    private VideoPlayer video;                   //视频播放器
    private Slider videoSlider;                  //slider，控制视频播放进度
    private Text videoTimeText;                  //video time text
    private int clipMinute, clipSecond;          //视频片段时间（分、秒）
    private int currentMinute, currentSecond;    //目前视频片段时间（分、秒）
    private int videoTime, currentVideoTime;     //视频时间
    private bool isStart,isPlayFinish;
    private Button btn_Start;
    private Button btn_Pause;

    private void Awake()
    {
        Init();
        video.url = Application.streamingAssetsPath + "/" + videoName + ".mp4";
    }

    private void Start()
    {
        btn_Start.onClick.AddListener(VideoPlay);
        btn_Pause.onClick.AddListener(VideoPause);
        VideoSliderEvent.Event_SliderDrag += ChangeSlider;
        video.Play();
    }

    private void OnEnable()
    {
        PlayOrPauseBtn(0);
    }

    private void OnDestroy()
    {
        VideoSliderEvent.Event_SliderDrag -= ChangeSlider;
    }

    private void Update()
    {
        if (video.texture == null) { return; }
        videoImage.texture = video.texture;
        ShowVideoTime();

        if((ulong)video.frame>=video.frameCount)
        {
            PlayOrPauseBtn(1);
            video.Stop();
            isPlayFinish = true;
            PlayFinish();
        }
    }

    private void Init()
    {
        videoImage = GetComponentInChildren<RawImage>();
        video = GetComponentInChildren<VideoPlayer>();
        videoSlider = GetComponentInChildren<Slider>();
        videoTimeText = videoSlider.transform.Find("VideoTimeText").GetComponent<Text>();
        btn_Start = transform.Find("Button_Start").GetComponent<Button>();
        btn_Pause = transform.Find("Button_Pause").GetComponent<Button>();
        isStart = true;
    }

    private void ShowVideoTime()
    {
        if(video.isPlaying && isStart)
        {
            videoTime = (int)(video.frameCount / video.frameRate);
            Debug.Log(videoTime);
            clipMinute = videoTime / 60;
            clipSecond = videoTime % 60;
            isStart = !isStart;
        }

        currentVideoTime = (int)video.time;
        currentMinute = currentVideoTime / 60;
        currentSecond = currentVideoTime % 60;

        videoSlider.value = (float)(video.time / (video.frameCount / video.frameRate));
        videoTimeText.text = string.Format("{0:D2}:{1:D2}/{2:D2}:{3:D2}", currentMinute, currentSecond, clipMinute, clipSecond);
    }

    private void ChangeSlider()
    {
        video.time = videoSlider.value * videoTime;
    }

    private void VideoPlay()
    {
        video.Play();
        PlayOrPauseBtn(0);
    }

    private void VideoPause()
    {
        video.Pause();
        PlayOrPauseBtn(1);
    }

    private void PlayOrPauseBtn(int _index)
    {
        //0 代表播放，1 代表暂停
        if(_index==0)
        {
            btn_Start.gameObject.SetActive(false);
            btn_Pause.gameObject.SetActive(true);
        }
        else if(_index==1)
        {
            btn_Start.gameObject.SetActive(true);
            btn_Pause.gameObject.SetActive(false);
        }
        else
        {
            btn_Start.gameObject.SetActive(false);
            btn_Pause.gameObject.SetActive(false);
        }
    }

    private void PlayFinish()
    {
        Debug.Log("视频播放完毕");
    }
}

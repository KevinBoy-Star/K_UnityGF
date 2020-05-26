using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace K_UnityGF
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private Text sliderValueUpdateText;                         //百分比文本
        [SerializeField]
        private Slider slider;                                      //加载条
        private float fsliderValue;                                 //记录Slider的值
        private int isliderValue;                                   //转化后Slider的整型值
        private AsyncOperation asyn;                                //异步加载

        private void Start()
        {
            slider.value = 0;                                       //初始化Slider值
            StartCoroutine(AsynLoading());
        }

        private void Update()
        {
            UpdateSliderValue();
        }

        /// <summary>
        /// 更新Slider值和文本
        /// </summary>
        private void UpdateSliderValue()
        {
            if (asyn.progress < 0.9f)
            {
                slider.value = asyn.progress;
                Debug.Log(slider.value);
            }
            else
            {
                slider.value += 0.08f;
                if (slider.value >= 1)
                {
                    slider.value = 1;
                    asyn.allowSceneActivation = true;
                }
            }

            fsliderValue = slider.value;                                //记录Slider值
            isliderValue = (int)(fsliderValue * 100);                   //Slider值转化成整型值
            sliderValueUpdateText.text = isliderValue.ToString() + "%";  //更新百分比文本
        }

        /// <summary>
        /// 协同  异步加载到不同场景
        /// </summary>
        /// <returns></returns>
        IEnumerator AsynLoading()
        {
            asyn = SceneManager.LoadSceneAsync(CCU.sceneName);
            asyn.allowSceneActivation = false;
            yield return asyn;
        }
    }
}

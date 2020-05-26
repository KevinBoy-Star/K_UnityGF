using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace K_UnityGF
{
    /// <summary>
    /// 自定义工具
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="_sceneName"></param>
        public static void LoadScene(string _sceneName) { SceneManager.LoadScene(_sceneName); }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="_sceneIndex"></param>
        public static void LoadScene(int _sceneIndex) { SceneManager.LoadScene(_sceneIndex); }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public static void ExitGame() { Application.Quit(); }

        /// <summary>
        /// 计算两向量之间的夹角
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <returns></returns>
        public static float DotToAngle(Vector3 _from, Vector3 _to)
        {
            float rad = 0;
            rad = Mathf.Acos(Vector3.Dot(_from.normalized, _from.normalized));
            return rad * Mathf.Rad2Deg;
        }

        /// <summary>
        /// 视角旋转
        /// </summary>
        /// <param name="_upTransform"></param>
        /// <param name="_rightTransform"></param>
        /// <param name="_speed"></param>
        /// <param name="minAngle"></param>
        /// <param name="maxAngle"></param>
        public static void ViewRotate(Transform _upTransform, Transform _rightTransform, float _speed, float minAngle, float maxAngle)
        {
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
            if (Input.GetMouseButton(1))
            {
                float angle = GetAngle(_rightTransform.localEulerAngles.x);
                _upTransform.Rotate(Vector3.up * mouseX * _speed, Space.World);
                if (angle >= minAngle && angle <= maxAngle)
                {
                    _rightTransform.Rotate(Vector3.left * mouseY * _speed, Space.Self);
                }
                else if (angle < minAngle)
                {
                    _rightTransform.localEulerAngles = new Vector3(minAngle, _rightTransform.localEulerAngles.y, _rightTransform.localEulerAngles.z);
                }
                else if (angle > maxAngle)
                {
                    _rightTransform.localEulerAngles = new Vector3(maxAngle, _rightTransform.localEulerAngles.y, _rightTransform.localEulerAngles.z);
                }
            }
        }

        /// <summary>
        /// 获取角度
        /// </summary>
        /// <returns></returns>
        public static float GetAngle(float _angle)
        {
            float angle = _angle - 180;
            if (angle > 0)
            {
                return angle - 180;
            }
            else
            {
                return angle + 180;
            }
        }

        /// <summary>
        /// 视口缩放
        /// </summary>
        /// <param name="_cam">相机</param>
        /// <param name="_minValue">视口最小值</param>
        /// <param name="_maxValue">视口最大值</param>
        /// <param name="_speed">视口缩放速度</param>
        public static void ViewZoom(Camera _cam, float _minValue, float _maxValue, float _speed)
        {
            float viewValue = _cam.fieldOfView;
            float wheelValue = Input.GetAxis("Mouse ScrollWheel");
            if (viewValue >= _minValue && wheelValue > 0)
            {
                _cam.fieldOfView -= _speed;
            }
            else if (viewValue <= _maxValue && wheelValue < 0)
            {
                _cam.fieldOfView += _speed;
            }
        }

        /// <summary>
        /// 查找对象助手 在指定游戏对象，一直向内查找子对象 返回游戏物体
        /// </summary>
        /// <param name="_findParent">所要查找的父对象</param>
        /// <param name="_targetName">所要查找的对象名称</param>
        /// <returns></returns>
        public static GameObject FindChildHelper(Transform _findParent, string _targetName)
        {
            GameObject target = null;
            for (int i = 0; i < _findParent.childCount; i++)
            {
                if (_findParent.GetChild(i).name == _targetName)
                {
                    target = _findParent.GetChild(i).gameObject;
                    break;
                }
                else
                {
                    target = FindChildHelper(_findParent.GetChild(i), _targetName);
                }
            }
            return target;
        }

        /// <summary>
        /// 随机数(随机不同数字)  返回数组
        /// </summary>
        /// <param name="total"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] GetRandomArray(int total, int count)
        {
            int[] sequence = new int[total];
            int[] output = new int[count];

            for (int i = 0; i < total; i++)
            {
                sequence[i] = i;
            }
            int end = total - 1;

            for (int i = 0; i < count; i++)
            {
                int num = UnityEngine.Random.Range(0, end + 1);
                output[i] = sequence[num];
                sequence[num] = sequence[end];
                end--;
            }
            return output;
        }

        /// <summary>
        /// 时间转化为13位时间戳
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
        public static long ConvertDateTimeToUtc(DateTime _time)
        {
            DateTime time = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (_time.Ticks - time.Ticks) / 10000;
            return t;
        }

        /// <summary>
        /// 13位时间戳转化为时间
        /// </summary>
        /// <param name="_utcTime"></param>
        /// <returns></returns>
        public static DateTime ConvertUtcToDateTime(string _utcTime)
        {
            DateTime dt = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
            long lTime = long.Parse(_utcTime + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dt.Add(toNow);
        }

        /// <summary>
        /// 截图(全屏)
        /// </summary>
        /// <param name="_path">保存路径</param>
        /// <returns></returns>
        public static IEnumerator ScreenShot(string _path)
        {
            ScreenCapture.CaptureScreenshot(_path, 0);
            yield return new WaitForEndOfFrame();
        }

        /// <summary>
        /// 截图(指定位置 指定宽高)
        /// </summary>
        /// <param name="_capX">位置 X</param>
        /// <param name="_capY">位置 Y</param>
        /// <param name="_capWidth">图片宽度</param>
        /// <param name="_capHeight">图片高度</param>
        /// <param name="_path">图片保存路径</param>
        /// <returns></returns>
        public static IEnumerator ScreenShot(float _capX, float _capY, int _capWidth, int _capHeight, string _path)
        {
            yield return new WaitForEndOfFrame();
            Texture2D tex = new Texture2D(_capWidth, _capHeight, TextureFormat.RGB24, true);
            tex.ReadPixels(new Rect(_capX, _capY, _capWidth, _capHeight), 0, 0, false);
            tex.Apply();
            byte[] bytes = tex.EncodeToPNG();
            File.WriteAllBytes(_path, bytes);
            yield return new WaitForEndOfFrame();
        }

        /// <summary>
        /// 设置材质球渲染模式
        /// </summary>
        /// <param name="material"></param>
        /// <param name="renderingMode"></param>
        public static void SetMaterialRenderingMode(Material material, RenderingMode renderingMode)
        {
            switch (renderingMode)
            {
                case RenderingMode.Opaque:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;
                case RenderingMode.Cutout:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 2450;
                    break;
                case RenderingMode.Fade:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;
                case RenderingMode.Transparent:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;
            }
        }

        /// <summary>
        /// 转化本地读取的数据 如果url是本地 需要进行json字符串转化(从本地读取的会多三个字节)
        /// </summary>
        /// <param name="_content">需要转化的json字符串</param>
        /// <returns></returns>
        public static string ConvertLocalLoadData(string _content)
        {
            byte[] datas = Encoding.UTF8.GetBytes(_content);
            _content = Encoding.UTF8.GetString(datas, 3, datas.Length - 3);
            return _content;
        }
    }
}


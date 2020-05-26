using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 右手控制器(功能:控制物体的触摸和拾取)
    /// </summary>
    public class VRRightHandler : HandBase
    {
        private bool isGrab;                //是否可以拾取
        private Transform selectTransform;  //选择的对象

        protected override void Update()
        {
            GetDevice();

            //按下握柄键拾取物体 松开扳机键松开物体
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && isGrab)
            {
                Delegation.Broadcast(Delegation.Event_GrabStart, transform, selectTransform);
                isGrab = false;
            }
            //松开握柄键松开物体
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                Delegation.Broadcast(Delegation.Event_GrabEnd, selectTransform);
                selectTransform = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<TouchItem>()!=null)
            {
                selectTransform = other.transform;
                Delegation.Broadcast(Delegation.Event_TouchStart, selectTransform);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<GrabItem>() != null && transform.childCount < 1)
            {
                isGrab = true;
                selectTransform = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            selectTransform = null;
        }
    }
}

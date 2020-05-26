#define HTC
//#define Oculus
using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 右手控制器(控制物体触碰/拾取)
    /// </summary>
    public class OVRRHHandler : MonoBehaviour
    {
        public Animator _HandAnim;          //手部动画控制器
        private bool IsGrab;                //是否可以抓取
        private Transform SelectTrans;      //选择的对象

        private void Start()
        {
            Delegation.Event_TaskFail += TaskFail;
        }

        private void OnDestroy()
        {
            Delegation.Event_TaskFail -= TaskFail;
        }

        private void Update()
        {
#if (Oculus)
            #region

            if (!_HandAnim.gameObject.activeSelf || _HandAnim == null)
            {
                return;
            }
            if (OVRInput.Get(OVRInput.RawButton.B))
            {

            }
            if (OVRInput.GetUp(OVRInput.RawButton.B))
            {

            }
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {

            }
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {

            }
            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
            {

            }
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {

            }

            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
            {

            }

            #endregion
            if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
            {
                if (IsGrab)
                {
                    _HandAnim.SetTrigger("SetUp");
                    Delegation.AddEvent(Delegation.Event_GrabStart, SelectTrans, transform);
                }
                else
                {
                    _HandAnim.SetTrigger("SetUp");
                }
            }

            if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
            {
                _HandAnim.SetTrigger("Idle");
                if (!IsGrab) return;
                Delegation.AddEvent(Delegation.Event_GrabEnd, SelectTrans);
            }
#endif
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<TouchItem>() != null)
            {
                SelectTrans = other.transform;
                Delegation.Broadcast(Delegation.Event_TouchStart, SelectTrans);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<GrabItem>() != null && transform.childCount <= 1)
            {
                IsGrab = true;
                SelectTrans = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            SelectTrans = null;
            IsGrab = false;
        }

        private void TaskFail()
        {
            Destroy(this);
        }
    }
}

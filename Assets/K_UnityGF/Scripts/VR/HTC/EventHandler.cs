using UnityEngine;
using VRTK;

namespace K_UnityGF
{
    /// <summary>
    /// VR 手柄事件(功能:射线(进入/点击/离开))
    /// </summary>
    [RequireComponent(typeof(VRTK_ControllerEvents))]
    [RequireComponent(typeof(VRTK_Pointer))]
    public class EventHandler : MonoBehaviour
    {
        private VRTK_ControllerEvents controllerEvents;
        private VRTK_Pointer vrtkPointer;

        private bool isPressDownTrigger = false;

        private void Awake()
        {
            controllerEvents = GetComponent<VRTK_ControllerEvents>();
            vrtkPointer = GetComponent<VRTK_Pointer>();
        }

        private void Start()
        {
            vrtkPointer.DestinationMarkerEnter += VrtkPointer_DestinationMarkerEnter;
            vrtkPointer.DestinationMarkerHover += VrtkPointer_DestinationMarkerHover;
            vrtkPointer.DestinationMarkerExit += VrtkPointer_DestinationMarkerExit;
        }

        private void OnDestroy()
        {
            vrtkPointer.DestinationMarkerEnter -= VrtkPointer_DestinationMarkerEnter;
            vrtkPointer.DestinationMarkerHover -= VrtkPointer_DestinationMarkerHover;
            vrtkPointer.DestinationMarkerExit -= VrtkPointer_DestinationMarkerExit;
        }

        /// <summary>
        /// 射线进入游戏物体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VrtkPointer_DestinationMarkerEnter(object sender, DestinationMarkerEventArgs e)
        {
            Delegation.Broadcast(Delegation.Event_RayEnter,e.target);
        }

        /// <summary>
        /// 射线悬停在游戏物体上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VrtkPointer_DestinationMarkerHover(object sender, DestinationMarkerEventArgs e)
        {
            if (controllerEvents.triggerPressed)
            {
                if (isPressDownTrigger == false)
                {
                    Delegation.Broadcast(Delegation.Event_ClickItem, e.target);
                    isPressDownTrigger = true;
                }
                return;
            }
            else
            {
                if (isPressDownTrigger == true)
                {
                    isPressDownTrigger = false;
                }
                return;
            }
        }

        /// <summary>
        /// 射线离开游戏物体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VrtkPointer_DestinationMarkerExit(object sender, DestinationMarkerEventArgs e)
        {
            Delegation.Broadcast(Delegation.Event_RayExit, e.target);
        }
    }
}

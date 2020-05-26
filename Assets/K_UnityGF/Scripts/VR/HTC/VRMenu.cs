using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// VR菜单控制器
    /// </summary>
    public class VRMenu : HandBase
    {
        private GameObject menu;            //菜单

        protected override void Update()
        {
            GetDevice();

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                if (menu.activeInHierarchy) { menu.SetActive(false); }
                else { menu.SetActive(true); }
            }

            if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                menu.SetActive(false);
            }
        }

        protected override void InitOperation()
        {
            base.InitOperation();
            menu = transform.Find("Menu").gameObject;
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public delegate void SliderDrag();

public class VideoSliderEvent : MonoBehaviour,IDragHandler
{
    public static event SliderDrag Event_SliderDrag;

    public void OnDrag(PointerEventData eventData)
    {
        if(Event_SliderDrag!=null)
        {
            Event_SliderDrag();
        }
    }
}

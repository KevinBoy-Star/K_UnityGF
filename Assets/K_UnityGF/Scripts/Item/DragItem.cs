using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 物体拖拽
    /// </summary>
    public class DragItem : VictorItem
    {
        private Vector3 originPos;              //源位置
        private Transform originParent;         //源父物体
        private Ray ray;                        //射线
        private RaycastHit hit;                 //射线返回信息
        private Collider m_collider;            //物体碰撞器
        protected bool isDrag;                  //拖拽状态

        /// <summary>
        /// 
        /// </summary>
        protected override void InitOperation()
        {
            base.InitOperation();
            originPos = transform.position;
            originParent = transform.parent;
            m_collider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                StartDrag(ray);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                EndDrag(ray);
            }
        }

        /// <summary>
        /// 开始拖拽
        /// </summary>
        /// <param name="m_ray"></param>
        private void StartDrag(Ray m_ray)
        {
            if (Physics.Raycast(m_ray, out hit))
            {
                if (hit.transform.name.Equals(transform.name))
                {
                    m_collider.enabled = false;
                    isDrag = true;
                }
                StartDragEvent(hit);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_hit"></param>
        protected virtual void StartDragEvent(RaycastHit _hit) { }

        /// <summary>
        /// 结束拖拽
        /// </summary>
        /// <param name="m_ray"></param>
        private void EndDrag(Ray m_ray)
        {
            if (!isDrag) return;
            if (Physics.Raycast(m_ray, out hit))
            {
                EndDragEvent(hit);
            }
            else
            {
                isDrag = false;
                ItemInit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_hit"></param>
        protected virtual void EndDragEvent(RaycastHit _hit) { }

        /// <summary>
        /// 游戏物体初始化
        /// </summary>
        protected void ItemInit()
        {
            m_collider.enabled = true;
            transform.position = originPos;
            transform.parent = originParent;
        }
    }
}


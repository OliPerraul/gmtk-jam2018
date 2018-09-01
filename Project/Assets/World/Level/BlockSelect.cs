using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSLevel
{

    public class BlockSelect : MonoBehaviour
    {
        public BlockHighlight cellHighlight;

        public LayerMask layerMask;


        public Vector2Int offset = new Vector2Int(0, 1);



        private void Update()
        {
            //Block cell = GetSelected();
            //cellHighlight.transform.localPosition = cell.transform.localPosition;
        }

        //public Block GetSelected()
        //{
        //    //Vector2Int idx = GetSelectedIdx();
        //    //Block cell = NSLevel.Level.Instance.blocks[idx.x, idx.y];
        //    //return cell;
        //}


        public void SnapToSelected(Transform transform)
        {
            //Block cell = GetSelected();
            //transform.SetParent(cell.transform);
            //transform.localPosition = Vector3.zero;
            return;

        }


        private Vector2Int GetSelectedIdx()
        {
            return Vector2Int.one;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //RaycastHit hit;
            //Vector2Int idx = new Vector2Int(999, 999);
            //if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            //{
            //    Debug.DrawLine(wp, hit.point, Color.red);

            //    Vector3 pos = hit.point;
            //    idx.x = ((int)pos.x) / NSLevel.Level.Instance.cellSize; //(int) Mathf.Round(pos.x / grid.cellSize);
            //    idx.y = ((int)pos.z) / NSLevel.Level.Instance.cellSize;//(int) Mathf.Round(pos.z / grid.cellSize);

            //    idx += offset;

            //    //Debug.Log(idx);
            //}

            //idx.Clamp(Vector2Int.zero, new Vector2Int(NSLevel.Level.Instance.size.x - 1, NSLevel.Level.Instance.size.y - 1));

            //return idx;
        }
    }
}

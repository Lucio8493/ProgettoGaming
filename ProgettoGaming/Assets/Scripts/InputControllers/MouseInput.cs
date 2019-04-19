using UnityEngine;

namespace InputControllers
{
    public class MouseInput : BaseInputController
    {
        const int NumOfFires = 2;

        public MouseInput()
        {
            Fire = new bool[NumOfFires];
         }
        public override void CheckInput()
        {

            horz = Input.GetAxis("Mouse X");
            vert = Input.GetAxis("Mouse Y");


            Up = (vert > 0);
            Down = (vert < 0);
            Left = (horz < 0);
            Right = (horz > 0);

            Fire[0] = Input.GetMouseButtonDown(0);
            Fire[1] = Input.GetMouseButtonDown(3);
        }
    }
}

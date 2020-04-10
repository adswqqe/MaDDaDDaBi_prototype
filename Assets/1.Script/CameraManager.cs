using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://blog.naver.com/PostView.nhn?blogId=cheeryca&logNo=220799350908&proxyReferer=https%3A%2F%2Fwww.google.com%2F
//https://www.youtube.com/watch?v=qbl38iPitVY
public class CameraManager : MonoBehaviour
{
    Vector2 StartPosition;
    Vector2 DragStartPosition;
    Vector2 DragNewPosition;
    Vector2 Finger0Position;
    float DistanceBetweenFingers;
    bool isZooming = false;
    bool isRotating = false;

    [SerializeField]
    Text debugText;
    [SerializeField]
    Text debugText2;
    [SerializeField]
    Text debugText3;
    [SerializeField]
    Text debugText4;
    [SerializeField]
    Text debugText5;
    [SerializeField]
    Text debugText6;

    const float minPanDistance = 0;
    public float turnAngleDelta;
    public float turnAngle;
    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 2;

    float oldAngle = 0f;

    Vector2 startVector;
    float rotGestureWidth;
    public const float TOUCH_ROTATION_WIDTH = 1; // Always3
    public const float TOUCH_ROTATION_MINIMUM = 1;
    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion desiredRotation = transform.rotation;

        DetectTouchMovement.Calculate();

        if (Input.touchCount == 0 && (isZooming || isRotating))
        {
            isZooming = false;
            isRotating = false;
        }

        if (Input.touchCount == 1)
        {
            if (!isZooming && !isRotating)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 NewPosition = GetWorldPosition();
                    Vector2 PositionDifference = NewPosition - StartPosition;
                    transform.Translate(-PositionDifference);
                }
                StartPosition = GetWorldPosition();
            }
        }
        else if (Input.touchCount == 2)
        {
            debugText.text = "turnAngleDelta : " + Mathf.Abs(DetectTouchMovement.turnAngleDelta);
            debugText2.text = "pinchDistanceDelta : " + Mathf.Abs(DetectTouchMovement.pinchDistanceDelta);
            debugText3.text = "isRotating : " + isRotating;
            debugText4.text = "isZooming  : " + isZooming;

            if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0.5 && !isZooming)  //rotate
            {
                isRotating = true;
                debugText.text = "isRotating  : " + isRotating;
            }
            if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 5 && !isRotating)
            {

                isZooming = true;

                DragNewPosition = GetWorldPositionOfFinger(1);
                Vector2 PositionDifference = DragNewPosition - DragStartPosition;
                float tempMagnitude;
                debugText5.text = "PositionDifference.magnitude? : " + PositionDifference.magnitude;
                    tempMagnitude = Mathf.Clamp(PositionDifference.magnitude, 0.01f, 1);
                debugText6.text = "tempMagnitude? : " + tempMagnitude;
                if (tempMagnitude > 0.5)
                {
                    tempMagnitude = 0.0f;
                    //return;
                }
                if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
                    GetComponent<Camera>().orthographicSize += (tempMagnitude);

                if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
                    GetComponent<Camera>().orthographicSize -= (tempMagnitude);

                DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);

                DragStartPosition = GetWorldPositionOfFinger(1);
                Finger0Position = GetWorldPositionOfFinger(0);
            }

            if (isRotating)
                RotateCamera();
            //else if (isZooming)
                //ZoomCamera();


        }



    }

    void ZoomCamera()
    {
        isZooming = true;

            DragNewPosition = GetWorldPositionOfFinger(1);
        Vector2 PositionDifference = DragNewPosition - DragStartPosition;

        if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
            GetComponent<Camera>().orthographicSize += (PositionDifference.magnitude);

        if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
            GetComponent<Camera>().orthographicSize -= (PositionDifference.magnitude);

        DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);


        DragStartPosition = GetWorldPositionOfFinger(1);
        Finger0Position = GetWorldPositionOfFinger(0);

        debugText.text = "왜 호출이 안되??";
    }

    void RotateCamera()
    {
        //두지점의 좌표값을 가져온다. 
        var _firstPoint = Input.GetTouch(0).position;
        var _secondPoint = Input.GetTouch(1).position;

        //두점 사이의 x의 길이 y의 길이를 구한다.
        var v2 = _firstPoint - _secondPoint;

        //두 지점의 각도를 구한다. -3.14 ~ 3.14
        var newAngle = Mathf.Atan2(v2.y, v2.x);

        //180/Mathf.PI 를 곱해서 -180~180 범위로 변경한다.
        newAngle = newAngle * 180 / Mathf.PI;

        //뒤 각도에서 앞 각도빼기.
        var deltaAngle = Mathf.DeltaAngle(newAngle, oldAngle);

        //비교할 old값 셋팅
        oldAngle = newAngle;

        //첫 터치에는 newAngle 값밖에 없기때문에ㅐ deltaAngle을 구할수가 없다. 
        //그러므로 터치가 처음시작될경우는 제외시켜주고, TouchPhase 가 Moved일 경우에 회전시켜준다. 
        if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began || Mathf.Abs(deltaAngle) > 11)
        {
            debugText.text = "얘는 일 안함??";
            return;
        }
        else if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0 && !isZooming/*Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved && !isZooming*/)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                debugText.text = "해치웠나?";
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit[] hits = Physics.RaycastAll(ray, 30);

            Vector3 position = new Vector3();
            foreach (RaycastHit hit in hits)
            {
                position = hit.transform.position;
            }
            transform.RotateAround(position, Vector3.up, -deltaAngle);
            debugText.text = "deltaAngle?" + deltaAngle;
            isRotating = true;
        }
    }

    Vector2 GetWorldPosition()
    {
        return GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GetWorldPositionOfFinger(int FingerIndex)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
    }
}

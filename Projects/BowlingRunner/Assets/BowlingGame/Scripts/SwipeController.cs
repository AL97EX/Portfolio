﻿using UnityEngine;
public class SwipeController : MonoBehaviour
{
    public bool _isDragging, _isMobilePlatform;
    public Vector2 _tapPoint, _swipeDelta;
    public float _minSwipeDelta = 130;

    public enum SwipeType
    {
        LEFT,
        RIGHT
    }

    public delegate void OnSwipeInput(SwipeType type);
    public static event OnSwipeInput SwipeEvent;

    private void Awake()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
            _isMobilePlatform = false;
        #else
            _isMobilePlatform = true;
        #endif
    }

    void Update()
    {
        if (!_isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _tapPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _isDragging = true;
                    _tapPoint = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
        CalculateSwipe();
    }

    void CalculateSwipe()
    {
        _swipeDelta = Vector2.zero;
        if (_isDragging)
        {
            if (!_isMobilePlatform && Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _tapPoint;
            }
            else if (Input.touchCount > 0)
                _swipeDelta = Input.touches[0].position - _tapPoint;
        }
        if (_swipeDelta.magnitude > _minSwipeDelta)
        {
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    SwipeEvent(_swipeDelta.x < 0 ? SwipeType.LEFT : SwipeType.RIGHT);
            }
            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        _isDragging = false;
        _tapPoint = _swipeDelta = Vector2.zero;
    }
}

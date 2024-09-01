using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private SplineFollower _splineFollower;
    [SerializeField] private float followSpeed = 5f;

    private void Start()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _splineFollower.followSpeed = followSpeed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _splineFollower.follow = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _splineFollower.follow = false;
        }
    }
}

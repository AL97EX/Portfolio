﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    [SerializeField] CountBlocks _count;
    void Awake()
    {
        _count.COUNT_BLOCK_TO_BOWLS = 0;
        _count._new = 0;
        _count._old = 0;
    }
    void Update()
    {
        
    }
}

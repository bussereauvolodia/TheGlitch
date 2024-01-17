using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _spriteOff;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _spriteRenderer.sprite!=_spriteOn) _spriteRenderer.sprite = _spriteOn;
    }
}

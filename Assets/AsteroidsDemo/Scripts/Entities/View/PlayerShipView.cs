﻿using System;
using System.Linq;
using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Infrastructure.Scripts.PortableObject;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;

namespace AsteroidsDemo.SpaceShip.Scripts
{
    public class PlayerShipView : PortableObjectView, IPlayerShipView
    {
        [field: SerializeField] private Sprite commonSprite;
        [field: SerializeField] private Sprite forsageSprite;

        private SpriteRenderer _renderer;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetForsage(bool isActive)
        {
            _renderer.sprite = isActive ? forsageSprite : commonSprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("asteroid") || other.CompareTag("Alien"))
            {
                PlayerWasHit?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler PlayerWasHit;
    }
}
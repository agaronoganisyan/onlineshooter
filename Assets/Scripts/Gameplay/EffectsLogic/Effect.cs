using System;
using PoolLogic;
using UnityEngine;

namespace Gameplay.EffectsLogic
{
    public class Effect  : MonoBehaviour, IPoolable<Effect>
    {
        private Action<Effect> _returnToPool;
        
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _transform;
        
        public void Play(Vector3 position)
        {
            _transform.position = position;
            gameObject.SetActive(true);
            _particleSystem.Play();
        }

        private void OnDisable()
        {
            ReturnToPool();
        }
        
        #region POOL_LOGIC

        public void PoolInitialize(Action<Effect> returnAction)
        {
            _returnToPool = returnAction;
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            _returnToPool?.Invoke(this);
        }
        
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.CameraLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using HelpersLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic
{
    public class SharedGameplayCanvas : MonoBehaviour, ISharedGameplayCanvas
    {
        private CancellationTokenSource _cancellationTokenSource;
        private TimeSpan _updatingRate;

        private ICameraController _cameraController;

        [SerializeField] private LayerMask _obstacleLayer;
        
        [SerializeField] private Transform _transform;

        private List<IPlayerInfoBlock> _playerInfoBlocks = new List<IPlayerInfoBlock>();

        private float _updatingFrequency = 0.25f;
        
        public void Initialize()
        {
            _cameraController = ServiceLocator.Get<ICameraController>();
            
            _updatingRate = TimeSpan.FromSeconds(_updatingFrequency);
        }

        public void StartUpdating()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Updating();
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }
        
        public void AddObject(IPlayerInfoBlock obj)
        {
            obj.SetParent(_transform);
            _playerInfoBlocks.Add(obj);
        }

        private void Update()
        {
            int playerInfoBlocksCount = _playerInfoBlocks.Count;
            for (int i = 0; i < playerInfoBlocksCount; i++)
            {
                _playerInfoBlocks[i].Tick();
            }
        }

        private async UniTaskVoid Updating()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                int playerInfoBlocksCount = _playerInfoBlocks.Count;
                for (int i = 0; i < playerInfoBlocksCount; i++)
                {
                    if (!DetectionOnScreenFunctions.IsWorldTargetInsideScreenBorders(_cameraController.Camera,
                            _playerInfoBlocks[i].TargetHead.position))
                    {
                        _playerInfoBlocks[i].Disable();
                        continue;    
                    }
                    
                    if (DetectionOnScreenFunctions.IsTargetVisible(_playerInfoBlocks[i].TargetHead.position,
                            _cameraController.CameraObjectTransform.position, _obstacleLayer))
                    {
                        _playerInfoBlocks[i].Enable();
                    }
                    else
                    {
                        _playerInfoBlocks[i].Disable();
                    }
                }
                
                await UniTask.Delay(_updatingRate, cancellationToken: _cancellationTokenSource.Token);
            }
        }
    }
}
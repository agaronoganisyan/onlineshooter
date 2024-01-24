using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.CameraLogic;
using Gameplay.CameraLogic.ControllerLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using HelpersLogic;
using Infrastructure.CanvasBaseLogic;
using Infrastructure.ServiceLogic;
using UnityEngine;

namespace Gameplay.UILogic.SharedGameplayCanvasLogic
{
    public class SharedGameplayCanvas : CanvasBase, ISharedGameplayCanvas
    {
        private CancellationTokenSource _cancellationTokenSource;
        private TimeSpan _updatingRate;

        private IGameplayCamera _gameplayCamera;

        [SerializeField] private LayerMask _obstacleLayer;
        
        [SerializeField] private Transform _transform;

        private List<IPlayerInfoBlock> _playerInfoBlocks = new List<IPlayerInfoBlock>();

        private float _updatingFrequency = 0.25f;
        
        private bool _isStopped;
        
        public override void Initialize()
        {
            _gameplayCamera = ServiceLocator.Get<IGameplayCamera>();
            
            _updatingRate = TimeSpan.FromSeconds(_updatingFrequency);
            base.Initialize();
        }

        public void StartUpdating()
        {
            _isStopped = false;
            _cancellationTokenSource = new CancellationTokenSource();
            Updating();
        }

        public void Stop()
        {
            _isStopped = true;
            _cancellationTokenSource?.Cancel();
        }
        
        public void AddObject(IPlayerInfoBlock obj)
        {
            obj.SetParent(_transform);
            _playerInfoBlocks.Add(obj);
        }

        private void Update()
        {
            if (_isStopped) return;
            
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
                    if (!DetectionOnScreenFunctions.IsWorldTargetInsideScreenBorders(_gameplayCamera.Camera,
                            _playerInfoBlocks[i].TargetHead.position))
                    {
                        _playerInfoBlocks[i].Disable();
                        continue;    
                    }
                    
                    if (DetectionOnScreenFunctions.IsTargetVisible(_playerInfoBlocks[i].TargetHead.position,
                            _gameplayCamera.Transform.position, _obstacleLayer))
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
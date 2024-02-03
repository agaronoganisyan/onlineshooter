using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.CameraLogic;
using Gameplay.CameraLogic.ControllerLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic;
using Gameplay.UILogic.SharedGameplayCanvasLogic.SharedGameplayCanvasObjectLogic.PlayerInfoBlock;
using Gameplay.UnitLogic;
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

        private ISharedGameplayCanvasSystem _canvasSystem;
        private ISharedGameplayCanvasObjectFactory _sharedGameplayCanvasObjectFactory;
        private IGameplayCamera _gameplayCamera;

        [SerializeField] private LayerMask _obstacleLayer;
        
        private Transform _transform;

        private List<IPlayerInfoBlock> _playerInfoBlocks = new List<IPlayerInfoBlock>();

        private float _updatingFrequency = 0.25f;
        
        private bool _isStopped;
        
        public override void Initialize()
        {
            _canvasSystem = ServiceLocator.Get<ISharedGameplayCanvasSystem>();
            _sharedGameplayCanvasObjectFactory = ServiceLocator.Get<ISharedGameplayCanvasObjectFactory>();
            _gameplayCamera = ServiceLocator.Get<IGameplayCamera>();
            
            _canvasSystem.OnShown += Show;
            _canvasSystem.OnHidden += Hide;
            _canvasSystem.OnUpdatingStarted += StartUpdating;
            _canvasSystem.OnUpdatingStopped += StopUpdating;
            _canvasSystem.OnUnitInfoAdded += AddUnitInfo;
            _canvasSystem.OnUnitInfoRemoved += RemoveUnitInfo;
            _updatingRate = TimeSpan.FromSeconds(_updatingFrequency);

            _transform = transform;
            
            Disable();
            
            base.Initialize();
        }

       public void StartUpdating()
        {
            Enable();
            
            _isStopped = false;
            _cancellationTokenSource = new CancellationTokenSource();
            Updating();
        }

        public void StopUpdating()
        {
            Disable();
            
            _isStopped = true;
            _cancellationTokenSource?.Cancel();
            Cleanup();
        }

        private void AddUnitInfo(Unit unit)
        {
            IPlayerInfoBlock playerInfoBlock = _sharedGameplayCanvasObjectFactory.GetPlayerBlockInfo(unit);
            playerInfoBlock.SetParent(_transform);
            _playerInfoBlocks.Add(playerInfoBlock);
        }

        private void RemoveUnitInfo(IPlayerInfoBlock infoBlock)
        {
            _playerInfoBlocks.Remove(infoBlock);
            infoBlock.Cleanup();
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
                        _playerInfoBlocks[i].Hide();
                        continue;    
                    }
                    
                    if (DetectionOnScreenFunctions.IsTargetVisible(_playerInfoBlocks[i].TargetHead.position,
                            _gameplayCamera.Transform.position, _obstacleLayer))
                    {
                        _playerInfoBlocks[i].Show();
                    }
                    else
                    {
                        _playerInfoBlocks[i].Hide();
                    }
                }
                
                await UniTask.Delay(_updatingRate, cancellationToken: _cancellationTokenSource.Token);
            }
        }

        private void Cleanup()
        {
            int playerInfoBlocksCount = _playerInfoBlocks.Count;
            for (int i = 0; i < playerInfoBlocksCount; i++)
            {
                _playerInfoBlocks[i].Cleanup();
            }
        }
        
        private void Enable()
        {
            enabled = true;
        }

        private void Disable()
        {
            enabled = false;
        }
    }
}
using System;
using ConfigsLogic;
using Gameplay.MatchLogic.TeamsLogic;
using UnityEngine;

namespace Gameplay.MatchLogic.SpawnLogic.SpawnPointLogic
{
    public class SpawnPointSystem : MonoBehaviour, ISpawnPointSystem
    {
        [SerializeField] private SpawnPointSystemConfig _config;        
        [SerializeField] private SpawnPoint[] _firstTeamSpawnPoints;
        [SerializeField] private SpawnPoint[] _secondTeamSpawnPoints;
        
        private Collider[] _detectedColliders;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _detectedColliders = new Collider[_config.MaxDetectingCollidersAmount];
        }

        public SpawnPointInfo GetSpawnPointInfo(TeamType teamType)
        {
            return GetMostFreePoint(teamType == TeamType.First ? _firstTeamSpawnPoints : _secondTeamSpawnPoints);
        }

        private SpawnPointInfo GetMostFreePoint(SpawnPoint[] teamPoints)
        {
            int minimalTargetsAmount = int.MaxValue;
            SpawnPointInfo point = new SpawnPointInfo();
            
            for (int i = 0; i < _firstTeamSpawnPoints.Length; i++)
            {
                int collidersAmount = Physics.OverlapSphereNonAlloc(teamPoints[i].Transform.position,
                    _config.DetectionZoneRadius, _detectedColliders,_config.TargetHitLayer);

                if (collidersAmount <= minimalTargetsAmount)
                {
                    minimalTargetsAmount = collidersAmount;
                    point.Setup(teamPoints[i].Transform.position, teamPoints[i].Transform.rotation);
                }
            }

            return point;
        }
    }
}
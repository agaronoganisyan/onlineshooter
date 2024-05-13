using System;
using Fusion;

namespace NetworkLogic.TimerLogic
{
    public enum ResultFormat
    {
        None,
        Seconds,
        MinutesAndSeconds
    }
    
    public struct NetworkTimer : INetworkStruct
    {
        private int _target;
        private int _initialTick;

        public bool Expired(NetworkRunner runner) => runner.IsRunning && _target > 0
            && (Tick) _target <= runner.Simulation.Tick;
        public bool IsRunning => _target > 0;

        public static NetworkTimer CreateFromTicks(NetworkRunner runner, int ticks)
        {
            if (runner == false || runner.IsRunning == false)
                return new NetworkTimer();

            NetworkTimer fromTicks = new NetworkTimer();
            fromTicks._target = (int) runner.Simulation.Tick + ticks;
            fromTicks._initialTick = runner.Simulation.Tick;
            return fromTicks;
        }

        public static NetworkTimer CreateFromSeconds(NetworkRunner runner, float delayInSeconds)
        {
            if (runner == false || runner.IsRunning == false)
                return new NetworkTimer();
            
            NetworkTimer fromSeconds = new NetworkTimer();
            fromSeconds._target = (int) runner.Simulation.Tick + (int) Math.Ceiling((double) delayInSeconds / (double) runner.DeltaTime);
            return fromSeconds;
        }

        public string GetRemainingTimeInFormat(NetworkRunner runner, ResultFormat valueFormat)
        {
            int remainingTime = (int)RemainingTime(runner);
            
            if (valueFormat == ResultFormat.MinutesAndSeconds)
            {
                int minutes = (remainingTime / 60);
                int seconds = (remainingTime % 60);
                
                return $"{minutes}:{seconds:00}";
            }
            else 
            {
                int seconds = (remainingTime % 60);
                
                return seconds.ToString();
            }
        }
        
        private float NormalizedValue(NetworkRunner runner)
        {
            if (runner == null || runner.IsRunning == false || IsRunning == false)
                return 0;

            if (Expired(runner))
                return 1;

            return ElapsedTicks(runner) / (_target - (float)_initialTick);
        }

        public float RemainingTime(NetworkRunner runner)
        {
            int remainingTicks = RemainingTicks(runner);
            return remainingTicks * runner.DeltaTime;
        }

        private int RemainingTicks(NetworkRunner runner)
        {
            if (runner == false || runner.IsRunning == false)
                return 0;
            
            if (IsRunning == false || Expired(runner))
                return 0;

            return Math.Max(0, _target - runner.Simulation.Tick);
        }
        
        private int ElapsedTicks(NetworkRunner runner)
        {
            if (runner == false || runner.IsRunning == false)
                return 0;

            if (IsRunning == false || Expired(runner))
                return 0;

            return runner.Simulation.Tick - _initialTick;
        }
    }
}
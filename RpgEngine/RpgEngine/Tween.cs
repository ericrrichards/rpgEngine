namespace RpgEngine {
    public class Tween {
        public delegate float TweenFunc(float timePassed, float start, float distance, float duration);
        private readonly TweenFunc _tweenFunc;
        private readonly float _distance;
        private readonly float _start;
        public float Value { get; private set; }
        private readonly float _totalDuration;
        private float _timePassed;
        public bool IsFinished { get; private set; }

        public Tween(float start, float finish, float totalDuration, TweenFunc tweenFunc = null ) {
            if (tweenFunc == null) {
                _tweenFunc = Linear;
            } else {
                _tweenFunc = tweenFunc;
            }
            _distance = finish - start;
            _start = start;
            Value = start;
            _totalDuration = totalDuration;
            _timePassed = 0;
            IsFinished = false;

        }

        public void Update(float elapsedTime) {
            _timePassed += elapsedTime;
            Value = _tweenFunc(_timePassed, _start, _distance, _totalDuration);
            if (_timePassed > _totalDuration) {
                Value = _start + _distance;
                IsFinished = true;
            }

        }

        public static float Linear(float timepassed, float start, float distance, float duration) {
            return distance * timepassed / duration + start;
        }
    }
}
namespace InputLogic.InputCanvasLogic.GrenadeLaunchingButtonLogic
{
    public class GrenadeLaunchingButton : InputButton
    {
        private void Start()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _inputHandler.OnThrowingInputStatusChanged += SetEnableStatus;
        }

        protected override void Prepare()
        {
            Enable();
        }
    }
}
using System;
using Infrastructure;
using Infrastructure.JoystickApi;
using Infrastructure.JoystickApi.JoyStickEventArgs;

namespace JoystickApi
{
	public class BTZJoyStickController : IBtzJoyStickController
	{
		readonly IJoyStickHandler _joyStickHandler;
        private const int MaxValue = 32767;
        private const int MainValue = -32767;
		const int Low = -32767;
		const int High = Low * (-1);

        #region Implementation

	    public event EventHandler OnShift;
	    public event EventHandler OnSteeringSensitiv;
	    public event EventHandler OnEnabled;
	    public event EventHandler OnMode;
        public event EventHandler OnSteeringChanged;
        public event EventHandler OnPowerChanged;
        public event EventHandler OnSpecialChanged;
	    #endregion

        #region ButtonValues
        static int SteeringPosition;
		static int PowerPosition;
		static int PowerPositionBackWards;
		static bool TempomatState;
		#endregion

		public BTZJoyStickController (IJoyStickHandler _joyStickHandler)
		{
			this._joyStickHandler = _joyStickHandler;
			this._joyStickHandler.OnButtonTriggered += OnJoyStickButtonTriggered;
		}

		void OnJoyStickButtonTriggered (object sender, EventArgs e)
		{
		    JoyStickEventArgs args = (JoyStickEventArgs) e;
            ProcessEvent(args);
		}

		#region IBTZJoyStickController implementation

		public bool Initialize ()
		{
			_joyStickHandler.Initialize ();
			return true;
		}

		public void Start ()
		{
			_joyStickHandler.Start ();
		}

	    public void Stop()
	    {
            _joyStickHandler.OnButtonTriggered -= OnJoyStickButtonTriggered;
            _joyStickHandler.Stop();
	    }

	    #endregion


		void ProcessEvent(JoyStickEventArgs args)
		{
		    switch (args.Button)
		    {
		        case XboxButton.Non:
		            break;
		        case XboxButton.A:
		            break;
		        case XboxButton.B:
		            break;
		        case XboxButton.X:
		            break;
		        case XboxButton.Y:
		            break;
		        case XboxButton.RT: //Speed
		            var valueEventArgRT = new JoyStickValueEventArgs
		            {
                        Value = args.Value
		            };
                    OnPowerChanged(this, valueEventArgRT);
		            break;
		        case XboxButton.RB: //Shift
		            var shiftUp = new JoyStickUpDownEventArgs();
                    shiftUp.JoyEvent = EventUpDown.Up;
		            OnShift(this, shiftUp);
		            break;
		        case XboxButton.LT:
		            break;
		        case XboxButton.LB:
                    var shiftDown = new JoyStickUpDownEventArgs();
                    shiftDown.JoyEvent = EventUpDown.Down;
                    OnShift(this, shiftDown);
		            break;
		        case XboxButton.XLeft:
		            break;
		        case XboxButton.XRight:
		            break;
		        case XboxButton.XDown:
                    var stSensiDown = new JoyStickUpDownEventArgs();
                    stSensiDown.JoyEvent = EventUpDown.Down;
                    OnSteeringSensitiv(this, stSensiDown);
		            break;
		        case XboxButton.XUp:
                    var stSensiUp = new JoyStickUpDownEventArgs();
                    stSensiUp.JoyEvent = EventUpDown.Up;
                    OnSteeringSensitiv(this, stSensiUp);
		            break;
		        case XboxButton.R_JoyLeftRight:
		            break;
		        case XboxButton.R_JoyUpDown:
		            break;
		        case XboxButton.R_JoyButton:
		            break;
		        case XboxButton.L_JoyLeftRight: //Steering
                    var valueEventArgLJ = new JoyStickValueEventArgs
		            {
                        Value = args.Value
		            };
                    OnSteeringChanged(this, valueEventArgLJ);
		            break;
		        case XboxButton.L_JoyUpDown:
		            break;
		        case XboxButton.L_JoyButton:
		            break;
		        case XboxButton.Start:
		            OnEnabled(this, null);
		            break;
		        case XboxButton.Select:
		            break;
		        case XboxButton.XButton:
                    OnMode(this, null);
		            break;
		    }
		}

		void ProcessSpecialButtons(JoyStickEventArgs args)
		{
			//Console.WriteLine(String.Format("ButtonBTZ {0} : {1}",args.Key,args.Triggered));

			OnSpecialChanged (this, new SpecialButtonEventArgs () {
				Triggered = args.Triggered,
				Key = args.Key
			});
		}

		public static int Map (int value, int fromSource, int toSource, int fromTarget, int toTarget)
		{
			return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
		}

		private uint map(int x, int in_min, int in_max, int out_min, int out_max)
		{
			int Puls = (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
			return (uint)Puls;
		}
	}
}


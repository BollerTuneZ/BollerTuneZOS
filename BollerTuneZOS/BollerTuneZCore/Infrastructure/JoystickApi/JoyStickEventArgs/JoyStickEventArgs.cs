using System;

namespace Infrastructure.JoystickApi.JoyStickEventArgs
{
	public class JoyStickEventArgs : EventArgs
	{
		public JoyStickEventArgs ()
		{
		}

        public XboxButton Button { get; set; }

		public byte Key{ get; set;}

		public bool Triggered{get;set;}

		public int Value{get;set;}
	}
}


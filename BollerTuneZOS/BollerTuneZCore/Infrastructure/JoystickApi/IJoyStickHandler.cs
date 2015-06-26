using System;

namespace Infrastructure.JoystickApi
{
	/// <summary>
	/// Empfängt Befehle vom Joystick
	/// </summary>
	public interface IJoyStickHandler
	{
		event EventHandler OnButtonTriggered;

		void Initialize();

		void Start();

	    void Stop();
	}
}


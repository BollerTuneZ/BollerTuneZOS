using System;

namespace Infrastructure.JoystickApi
{
	/// <summary>
	/// Abgewandelte klasse die IJoyStickHandler benötigt,
	/// und nur relevante Daten liefert
	/// </summary>
	public interface IBTZJoyStickController
	{
	    //private const int MaximalValue = 32767;

		event EventHandler OnSteeringChanged;
		event EventHandler OnPowerChanged;
	    event EventHandler OnShift;
        event EventHandler OnSteeringSensitiv;
	    event EventHandler OnEnabled;
	    event EventHandler OnMode;


		bool Initialize();

		void Run();
	}
}


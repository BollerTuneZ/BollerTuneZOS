using Infrastructure.Communication;

namespace Communication.Infrastructure.MessageProcessor
{
	public interface IEngineProcessor
	{
	    void Initialize();
	    void Start();
	    void Stop();
	    void SetSpeed(int speed);
	    void SetDirection(EngineDriveDirection direction);
		void SetEnabled(bool enabled);
	}

    public enum EngineDriveDirection
    {
        Non,
        Forwards,
        Backwards
    }
}


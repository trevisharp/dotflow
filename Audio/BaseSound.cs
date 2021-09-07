namespace Flow.Audio
{
    public abstract class BaseAudio
    {
        public abstract float this[float t, int channel = 0] { get; }
        public abstract void Save();
    }
}
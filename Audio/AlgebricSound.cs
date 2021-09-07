using System;
namespace Flow.Audio
{
    public class AlgebricSound : BaseAudio
    {
        public Func<double, double> Equation { get; set; } = t => 0;
        public double Duration { get; set; } = 1.0;
        public override float this[float t, int channel = 0]
            => (float)Equation((double)t);

        public override void Save()
        {
            Sound s = this;
            s.Save();
        }

        public static implicit operator Sound(AlgebricSound algebric)
        {
            var result = Sound.New((float)algebric.Duration);
            for (int i = 0; i < result.Channels[0].Length; i++)
                result.Channels[0][i] = (float)algebric.Equation(i / 50000.0);
            return result;
        }
    }
}
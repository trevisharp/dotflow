using System;

namespace Flow.Audio.Processing
{
    public static class ForSampleAudioProcessing
    {
        public static Sound ForSample(this Sound sound, Func<double, double> op)
        {
            foreach (var channel in sound.Channels)
            {
                for (int n = 0; n < channel.Length; n++)
                {
                    channel[n] = (float)op(channel[n]);
                }
            }
            return sound;
        }
    }
}
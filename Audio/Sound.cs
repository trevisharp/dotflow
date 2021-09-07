using System.Media;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Flow.Audio
{
    public class Sound : BaseAudio
    {
        public override float this[float t, int channel = 0]
        {
            get => Channels[channel][(int)(t / 50000f)];
            set => Channels[channel][(int)(t / 50000f)] = value;
        }

        public string Path { get; set; } = "output.wav";
        public List<float[]> Channels { get; set; } = new List<float[]>();
        public static Sound New(float duration, int channels = 1)
        {
            Sound sound = new Sound();
            int size = (int)(duration * 50000);
            while (channels-- > 0)
                sound.Channels.Add(new float[size]);
            return sound;
        }

        public override void Save()
            => SaveAsWave(this.Path);

        public void SaveAsWave(string path)
        {
            if (Channels.Count == 0 || Channels.Count(c => c == null || c.Length == 0) > 0)
                throw new Exception("One ou more channels of this Sound is empty");
            WavCodec codec = new WavCodec();
            codec.BitsPerSample = 24;
            codec.FormatCode = 1;
            codec.SampleRate = 50000;
            byte[][] convertedata = new byte[Channels.Count][];
            float[] data;
            int value;
            int size;
            for (int i = 0; i < convertedata.Length; i++)
            {
                data = Channels[i];
                size = (int)(3 * Channels[i].Length);
                convertedata[i] = new byte[size];
                for (int j = 0, k = 0; j < size; j += 3, k++)
                {
                    if (data[k] > 1f)
                        value = 8388608;
                    else if (data[k] < -1f)
                        value = -8388608;
                    else
                        value = (int)(8388608 * data[k]);
                    if (value < 0)
                        value += 256 * 256 * 256;
                    convertedata[i][j] = (byte)(value % 256);
                    value /= 256;
                    convertedata[i][j + 1] = (byte)(value % 256);
                    convertedata[i][j + 2] = (byte)(value / 256);
                }
            }       
            codec.Save(path, convertedata);
        }
    }
}
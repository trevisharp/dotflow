using System;
using System.IO;

namespace Flow.Audio
{
    //TODO: Implementar conversão para FormatCode != 1 (PCM)
    public class WavCodec
    {
        public int SampleRate { get; set; } = 22050;
        public int BitsPerSample { get; set; } = 16;
        public int FormatCode { get; set; } = 1;
        public void Save(string path, byte[] data)
            =>Save(path, new byte[][] { data });
        public void Save(string path, byte[][] channelsdata)
        {
            if (channelsdata == null || 
                channelsdata.Length == 0 || 
                channelsdata[0] == null || 
                channelsdata[0].Length == 0)
                throw new InvalidDataException();
            
            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            int channelscount = channelsdata.Length;
            int bytespersample = BitsPerSample / 8;
            int channelsize = channelsdata[0].Length;
            int datachunksize = channelsize * channelscount;
            int chunksize = 36 + datachunksize;
            foreach (byte[] ch in channelsdata)
            {
                if (ch == null)
                    throw new InvalidDataException();
                if (ch.Length != channelsize)
                    throw new InvalidDataException("Channels have diferent size");
            }

            bw.Write('R');
            bw.Write('I');
            bw.Write('F');
            bw.Write('F');
            bw.Write(ToByte(chunksize, 4));
            bw.Write('W');
            bw.Write('A');
            bw.Write('V');
            bw.Write('E');
            //fmt subchunk
            bw.Write('f');
            bw.Write('m');
            bw.Write('t');
            bw.Write(' ');
            bw.Write(ToByte(16, 4)); //Subckunksize
            bw.Write(ToByte(FormatCode, 2)); //AudioFormat
            bw.Write(ToByte(channelscount, 2));
            bw.Write(ToByte(SampleRate, 4));
            bw.Write(ToByte(channelscount * SampleRate * bytespersample, 4)); //ByteRate
            bw.Write(ToByte(channelscount * bytespersample, 2)); //BlockAlign
            bw.Write(ToByte(BitsPerSample, 2));
            //data subchunk
            bw.Write('d');
            bw.Write('a');
            bw.Write('t');
            bw.Write('a');
            bw.Write(ToByte(datachunksize, 4));
            for (int i = 0; i < channelsize; i += bytespersample)
            {
                for (int k = 0; k < channelscount; k++)
                {
                    for (int j = 0; j < bytespersample; j++)
                    {
                        bw.Write(channelsdata[k][i + j]);
                    }
                }
            }
            bw.Close();
            fs.Close();
        }

        public byte[][] Open(string path)
        {
            if (!File.Exists(path))
                return null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            if (read(1) != 'R' || read(1) != 'I' || read(1) != 'F' || read(1) != 'F')
                throw new FileFormatException();
            int chunksize = read(4);
            if (read(1) != 'W' || read(1) != 'A' || read(1) != 'V' || read(1) != 'E')
                throw new FileFormatException();
            byte[] extra = null;
            int channels = -1;
            byte[][] data = null;
            while (fs.Position < fs.Length)
            {
                string chunckid = readchunckid();
                switch (chunckid)
                {
                    case "fmt ":
                        discard(4); //discard subchunksize
                        int format = read(2);
                        channels = read(2);
                        int rate = read(4);
                        discard(4); //discard ByteRate
                        discard(2); //discard BlockAlign
                        int bitspersample = read(2);
                        if (format != 1 && format != 3)
                        {
                            int extensionsize = read(2);
                            extra = new byte[extensionsize];
                            for (int i = 0; i < extensionsize; i++)
                                extra[i] = (byte)read(1);
                        }
                        this.SampleRate = rate;
                        this.BitsPerSample = bitspersample;
                        this.FormatCode = format;
                        break;
                    case "data":
                        int bytespersample = this.BitsPerSample / 8;
                        int datasize = read(4);

                        int channelsize = datasize / channels;

                        data = new byte[channels][];
                        for (int c = 0; c < channels; c++)
                            data[c] = new byte[channelsize];
            
                        for (int i = 0; i < channelsize; i += bytespersample)
                        {
                            for (int c = 0; c < channels; c++)
                            {
                                for (int j = 0; j < bytespersample; j++)
                                {
                                    data[c][i + j] = (byte)read(1);
                                }
                            }
                        }
                        break;
                    case "fact":
                        discard(4); //discard data size
                        discard(4); //discard data size
                        break;
                    default:
                        int size = read(4);
                        discard(size);
                        break;
                }
            }
            br.Close();
            fs.Close();
            return data;

            string readchunckid()
            {
                string s = "";
                return s + (char)read(1) + (char)read(1) + (char)read(1) + (char)read(1);
            }
            void discard(int count)
            {
                while (count-- > 0)
                {
                    int disbyte = br.ReadByte();
                }
            }
            int read(int count)
            {
                int value = 0;
                int coef = 1;
                while (count-- > 0)
                {
                    value += coef * br.ReadByte();
                    coef *= 256;
                }
                return value;
            }
        }

        private byte[] ToByte(int value, int bytes = -1)
        {
            if (bytes == -1)
                bytes = GetSize(value);
            byte[] data = new byte[bytes];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(value % 256);
                value /= 256;
            }
            return data;
        }
        private int GetSize(int value)
        {
            int size = 1;
            int maxsize = 256;
            while (maxsize <= value)
            {
                maxsize *= 256;
                size++;
            }
            return size;
        }
    }
}
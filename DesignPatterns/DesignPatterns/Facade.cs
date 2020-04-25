using System;

namespace DesignPatterns
{
    /// <summary>
    /// Provide a simplified interface to a library, a framework, or any other complex set of classes.
    /// </summary>
    class Facade : IDesignPattern
    {
        public void DisplayExample()
        {
            VideoConverter converter = new VideoConverter();
            VideoFile file = converter.Convert("funny-cats-video.ogg", "mp4");
            file.Save();
        }

        #region Implementation
        class VideoConverter
        {
            public VideoFile Convert(string filename, string format)
            {
                VideoFile file = new VideoFile(filename);

                Codec sourceCodec = new CodecFactory().Extract(file);
                Codec destinationCodec = new Codec();

                if (format == "mp4")
                    destinationCodec = new MPEG4CompressionCodec();
                else
                    destinationCodec = new OggCompressionCodec();

                string buffer = BitrateReader.Read(filename, sourceCodec);
                byte[] result = BitrateReader.Convert(buffer, destinationCodec);
                result = new AudioMixer().Fix(result);
                return new VideoFile(result);
            }
        }

        // Below, some classes that could come from a video framework

        class VideoFile
        {
            public VideoFile(string filename) { }
            public VideoFile(byte[] bytes) { }

            public void Save()
            {
                Console.WriteLine("Saved VideoFile");
            }
        }

        class Codec { }
        class OggCompressionCodec : Codec { }
        class MPEG4CompressionCodec : Codec { }

        class CodecFactory { public Codec Extract(VideoFile file) => new Codec(); }

        static class BitrateReader
        {
            public static string Read(string filename, Codec codec) => "";
            public static byte[] Convert(string buffer, Codec codec) => null;
        }

        class AudioMixer { public byte[] Fix(byte[] bytes) => null; }
        #endregion
    }
}
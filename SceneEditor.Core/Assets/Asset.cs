using System;
using System.Drawing;
using System.IO;
using SceneEditor.Core.General;

namespace SceneEditor.Core.Assets
{
    public class Asset
    {
        private MemoryStream _stream;

        public Asset(string name, Stream sourceStream)
        {
            if (sourceStream == null)
                throw new ArgumentNullException("sourceStream");

            if (sourceStream.Length == 0)
                throw new ArgumentException("Stream is empty");

            if (name == null)
                throw new ArgumentNullException("name");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is empty");

            Name = name;
            _stream = new MemoryStream();

            if (sourceStream.CanSeek)
                sourceStream.Position = 0;

            sourceStream.CopyTo(_stream);
            _stream.Position = 0;

            // If this is an image, determine the image size
            try
            {
                var image = Image.FromStream(Stream);
                ImageDimensions = new Vector(image.Width, image.Height);
            }
            catch (ArgumentException)
            {
                // Ignore if not a valid image
            }
        }

        public string Name { get; private set; }
        
        public Stream Stream
        {
            get
            {
                if (_stream.Position > 0)
                    _stream.Position = 0;

                var copy = new MemoryStream();
                _stream.CopyTo(copy);
                _stream.Position = 0;
                copy.Position = 0;
                return copy;
            }
        }

        public Vector ImageDimensions { get; private set; }
    }
}
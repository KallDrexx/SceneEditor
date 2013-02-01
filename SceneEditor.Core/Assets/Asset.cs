using System;
using System.IO;

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
    }
}
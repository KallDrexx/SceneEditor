using System;
using System.IO;

namespace SceneEditor.Core.Assets
{
    public class Asset
    {
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

            Stream = new MemoryStream();

            if (sourceStream.CanSeek)
                sourceStream.Position = 0;

            sourceStream.CopyTo(Stream);
            Stream.Position = 0;
            Name = name;
        }

        public string Name { get; private set; }
        public MemoryStream Stream { get; private set; }
    }
}
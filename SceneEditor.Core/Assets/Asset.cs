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

            Stream = new MemoryStream();
            sourceStream.CopyTo(Stream);
            Stream.Position = 0;
            Name = name;
        }

        public string Name { get; private set; }
        public MemoryStream Stream { get; private set; }
    }
}
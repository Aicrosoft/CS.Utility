using System;
using System.IO;

namespace CS.Email
{
    /// <summary>
    ///     ¸½¼þ
    /// </summary>
    public class MessageAttachment
    {
        /// <summary>
        ///     Creates a new attachment
        /// </summary>
        /// <param name="mediaType">Look at System.Net.Mimie.MediaTypeNames for help.</param>
        /// <param name="fileName">Path to the file.</param>
        public MessageAttachment(string mediaType, string fileName)
        {
            MediaType = mediaType;

            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            var info = new FileInfo(fileName);

            if (!info.Exists) throw new ArgumentException("The specified file does not exists", nameof(fileName));

            FileName = fileName;
        }

        /// <summary>
        ///     Creates a new attachment
        /// </summary>
        /// <param name="mediaType">Look at System.Net.Mime.MediaTypeNames for help.</param>
        /// <param name="stream">File stream.</param>
        public MessageAttachment(string mediaType, Stream stream)
        {
            MediaType = mediaType;

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Stream = stream;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageAttachment" /> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="stream">The stream.</param>
        public MessageAttachment(string fileName, string mediaType, Stream stream)
            : this(mediaType, stream)
        {
            FileName = fileName;
        }

        /// <summary>
        ///     Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets the type of the media.
        /// </summary>
        /// <value>The type of the media.</value>
        public string MediaType { get; }

        /// <summary>
        ///     Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public Stream Stream { get; }
    }
}
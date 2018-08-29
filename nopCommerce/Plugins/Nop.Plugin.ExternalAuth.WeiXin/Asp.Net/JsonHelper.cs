using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Nop.Plugin.ExternalAuth.WeiXin.Asp.Net
{
    internal static class JsonHelper
    {
        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <typeparam name="T">The type of the value to deserialize.</typeparam>
        /// <returns>
        /// The deserialized value.
        /// </returns>
        public static T Deserialize<T>(Stream stream) where T : class
        {
            //Requires.NotNull<Stream>(stream, "stream");
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
            return (T)((object)dataContractJsonSerializer.ReadObject(stream));
        }
    }
}

using System;
using System.Text;
using System.Globalization;

namespace XamarinEvolve.Clients.Portable
{
    /// <summary>
    /// Gravatar interaction.
    /// </summary>
    public static class Gravatar
    {
        const string HttpsUrl = "https://secure.gravatar.com/avatar.php?gravatar_id=";

        /// <summary>
        /// Gets the Gravatar URL.
        /// </summary>
        /// <param name="email">The email of user.</param>
        /// <param name="secure">Use HTTPS?</param>
        /// <param name="size">The Gravatar size.</param>
        /// <param name="rating">The Gravatar rating.</param>
        /// <returns>A gravatar URL.</returns>
        public static string GetURL(string email,int size = 150, 
            string rating = "x") => $"{HttpsUrl}{GetMD5(email)}&s={size.ToString(CultureInfo.InvariantCulture)}&r={rating}";


        /// <summary>
        /// Gets the MD5 of the given string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The MD5 hash.</returns>
        static string GetMD5(string input)
        {

            var bytes = Encoding.UTF8.GetBytes(input);
            var data = MD5Core.GetHash(bytes);
            var builder = new StringBuilder();

            for (var i = 0; i < data.Length; i++) 
                builder.Append(data[i].ToString("x2"));
            
            return builder.ToString();
        }
    }
}


using System;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Nancy.Json;

namespace DynatronChallenge.DAC
{
    /// <summary>
    /// Helper class to serialize/deserialize any [Serializable] object in a centralized way.
    /// </summary>
    public static class Serialization
    {

        /// <summary>
        /// Serialize a [Serializable] object into a JSON-formatted string using System.Web.Script.Serialization.JavaScriptSerializer
        /// NOTE: you need to add a reference to System.Web.Extensions to have access to System.Web.Script.Serialization namespace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">any T object</param>
        /// <returns>a JSON-formatted string</returns>
        public static string SerializeToJson<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                return new JavaScriptSerializer().Serialize(value);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("An error occurred", ex);
            }
        }

        /// <summary>
        /// De-serialize a [Serializable] object into a JSON-formatted string using System.Web.Script.Serialization.JavaScriptSerializer
        /// NOTE: you need to add a reference to System.Web.Extensions to have access to System.Web.Script.Serialization namespace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">any T object</param>
        /// <returns>a JSON-formatted string</returns>
        public static T DeserializeFromJson<T>(string json)
        {
            if (String.IsNullOrEmpty(json)) throw new NotSupportedException("ERROR: input string cannot be null.");
            try
            {
                return new JavaScriptSerializer().Deserialize<T>(json);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("An error occurred", ex);
            }
        }
    }
}

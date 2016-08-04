using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace CS.Configuration
{
    public static class ConfigurationHelper
    {
        internal static bool TryGetAndRemove(Dictionary<string, string> dict, string name, out int value, bool required)
        {
            string tmp;
            if (TryGetAndRemove(dict, name, out tmp, required)
                && int.TryParse(tmp, out value))
            {
                return true;
            }

            if (required)
                throw new ConfigurationErrorsException("Missing or invalid parameter: " +
                                                       (string.IsNullOrEmpty(name) ? "element content" : name));

            value = 0;

            return false;
        }

        internal static bool TryGetAndRemove(Dictionary<string, string> dict, string name, out TimeSpan value,
            bool required)
        {
            string tmp;
            if (TryGetAndRemove(dict, name, out tmp, required)
                && TimeSpan.TryParse(tmp, out value))
            {
                return true;
            }

            if (required)
                throw new ConfigurationErrorsException("Missing or invalid parameter: " +
                                                       (string.IsNullOrEmpty(name) ? "element content" : name));

            value = TimeSpan.Zero;

            return false;
        }

        internal static bool TryGetAndRemove(Dictionary<string, string> dict, string name, out string value,
            bool required)
        {
            if (dict.TryGetValue(name, out value))
            {
                dict.Remove(name);

                if (!string.IsNullOrEmpty(value))
                    return true;
            }

            if (required)
                throw new ConfigurationErrorsException("Missing parameter: " +
                                                       (string.IsNullOrEmpty(name) ? "element content" : name));

            return false;
        }

        internal static void CheckForUnknownAttributes(Dictionary<string, string> dict)
        {
            if (dict.Count > 0)
                throw new ConfigurationErrorsException("Unrecognized parameter: " + dict.Keys.First());
        }

        public static void CheckForInterface(Type type, Type interfaceType)
        {
            if (type == null || interfaceType == null) return;

            if (Array.IndexOf(type.GetInterfaces(), interfaceType) == -1)
                throw new ConfigurationErrorsException("The type " + type.AssemblyQualifiedName + " must implement " +
                                                       interfaceType.AssemblyQualifiedName);
        }

        public static IPEndPoint ResolveToEndPoint(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            var parts = value.Split(':');
            if (parts.Length != 2)
                throw new ArgumentException("host:port is expected", "value");

            int port;
            if (!int.TryParse(parts[1], out port))
                throw new ArgumentException("Cannot parse port: " + parts[1], "value");

            return ResolveToEndPoint(parts[0], port);
        }

        public static IPEndPoint ResolveToEndPoint(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException("host");

            IPAddress address;

            // parse as an IP address
            if (!IPAddress.TryParse(host, out address))
            {
                // not an ip, resolve from dns
                // TODO we need to find a way to specify whihc ip should be used when the host has several
                var entry = Dns.GetHostEntry(host);
                address = entry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

                if (address == null)
                    throw new ArgumentException(string.Format("Could not resolve host '{0}'.", host));
            }

            return new IPEndPoint(address, port);
        }
    }
}
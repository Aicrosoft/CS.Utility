using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace CS.Configuration
{

    /// <summary>
    /// 扩展的键值SectionHandler
    /// <remarks>
    /// 当value为空或未设定时直接取默认的节点 CDATA 中的值。优先使用value中的值
    /// </remarks>
    /// </summary>
    public class KeyValueSectionHandler : IConfigurationSectionHandler
    {
        private const string DefaultKeyAttribute = "key";

        private const string DefaultValueAttribute = "value";

        //private const string defaultTextAttribute = "text";

        /// <summary>Gets the XML attribute name to use as the key in a key/value pair.</summary>
        /// <returns>A <see cref="T:System.String" /> value containing the name of the key attribute.</returns>
        protected virtual string KeyAttributeName => DefaultKeyAttribute;

        /// <summary>Gets the XML attribute name to use as the value in a key/value pair.</summary>
        /// <returns>A <see cref="T:System.String" /> value containing the name of the value attribute.</returns>
        protected virtual string ValueAttributeName => DefaultValueAttribute;

        /// <summary>Creates a new configuration handler and adds it to the section-handler collection based on the specified parameters.</summary>
        /// <returns>A configuration object.</returns>
        /// <param name="parent">Parent object.</param>
        /// <param name="context">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <filterpriority>2</filterpriority>
        public object Create(object parent, object context, XmlNode section)
        {
            return CreateStatic(parent, section, this.KeyAttributeName, this.ValueAttributeName);
        }

        internal static object CreateStatic(object parent, XmlNode section)
        {
            return CreateStatic(parent, section, DefaultKeyAttribute, DefaultValueAttribute);
        }

        internal static object CreateStatic(object parent, XmlNode section, string keyAttriuteName, string valueAttributeName)
        {
            var readOnlyNameValueCollection = parent == null ? new ReadOnlyNameValueCollection(StringComparer.OrdinalIgnoreCase) : new ReadOnlyNameValueCollection((ReadOnlyNameValueCollection)parent);
            HandlerBase.CheckForUnrecognizedAttributes(section);
            foreach (XmlNode xmlNode in section.ChildNodes)
            {
                if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
                {
                    if (xmlNode.Name == "add")
                    {
                        string name = HandlerBase.RemoveRequiredAttribute(xmlNode, keyAttriuteName);
                        string value = HandlerBase.RemoveRequiredValue(xmlNode, valueAttributeName);
                        HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
                        readOnlyNameValueCollection[name] = value;
                    }
                    else if (xmlNode.Name == "remove")
                    {
                        string name2 = HandlerBase.RemoveRequiredAttribute(xmlNode, keyAttriuteName);
                        HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
                        readOnlyNameValueCollection.Remove(name2);
                    }
                    else if (xmlNode.Name.Equals("clear"))
                    {
                        HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
                        readOnlyNameValueCollection.Clear();
                    }
                    else
                    {
                        HandlerBase.ThrowUnrecognizedElement(xmlNode);
                    }
                }
            }
            readOnlyNameValueCollection.SetReadOnly();
            return readOnlyNameValueCollection;
        }

        
    }


    internal class ReadOnlyNameValueCollection : NameValueCollection
    {
        internal ReadOnlyNameValueCollection(IEqualityComparer equalityComparer) : base(equalityComparer)
        {
        }

        internal ReadOnlyNameValueCollection(ReadOnlyNameValueCollection value) : base(value)
        {
        }

        internal void SetReadOnly()
        {
            base.IsReadOnly = true;
        }
    }
}
using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace CS.Configuration
{
    internal class HandlerBase
    {
        private HandlerBase()
        {
        }

        private static XmlNode GetAndRemoveAttribute(XmlNode node, string attrib, bool fRequired)
        {
            var xmlNode = node.Attributes.RemoveNamedItem(attrib);
            if (fRequired && xmlNode == null)
            {
                throw new ConfigurationErrorsException(
                    SR.GetString("Config_missing_required_attribute", attrib, node.Name), node);
            }
            return xmlNode;
        }

        private static XmlNode GetAndRemoveStringAttributeInternal(XmlNode node, string attrib, bool fRequired,
            ref string val)
        {
            var andRemoveAttribute = GetAndRemoveAttribute(node, attrib, fRequired);
            if (andRemoveAttribute != null)
            {
                val = andRemoveAttribute.Value;
            }
            return andRemoveAttribute;
        }

        internal static XmlNode GetAndRemoveStringAttribute(XmlNode node, string attrib, ref string val)
        {
            return GetAndRemoveStringAttributeInternal(node, attrib, false, ref val);
        }

        private static XmlNode GetAndRemoveBooleanAttributeInternal(XmlNode node, string attrib, bool fRequired,
            ref bool val)
        {
            var andRemoveAttribute = GetAndRemoveAttribute(node, attrib, fRequired);
            if (andRemoveAttribute != null)
            {
                try
                {
                    val = bool.Parse(andRemoveAttribute.Value);
                }
                catch (Exception inner)
                {
                    throw new ConfigurationErrorsException(
                        SR.GetString(SR.GetString("Config_invalid_boolean_attribute", andRemoveAttribute.Name)), inner,
                        andRemoveAttribute);
                }
            }
            return andRemoveAttribute;
        }

        internal static XmlNode GetAndRemoveBooleanAttribute(XmlNode node, string attrib, ref bool val)
        {
            return GetAndRemoveBooleanAttributeInternal(node, attrib, false, ref val);
        }

        private static XmlNode GetAndRemoveIntegerAttributeInternal(XmlNode node, string attrib, bool fRequired,
            ref int val)
        {
            var andRemoveAttribute = GetAndRemoveAttribute(node, attrib, fRequired);
            if (andRemoveAttribute != null)
            {
                if (andRemoveAttribute.Value.Trim() != andRemoveAttribute.Value)
                {
                    throw new ConfigurationErrorsException(
                        SR.GetString("Config_invalid_integer_attribute", andRemoveAttribute.Name), andRemoveAttribute);
                }
                try
                {
                    val = int.Parse(andRemoveAttribute.Value, CultureInfo.InvariantCulture);
                }
                catch (Exception inner)
                {
                    throw new ConfigurationErrorsException(
                        SR.GetString("Config_invalid_integer_attribute", andRemoveAttribute.Name), inner,
                        andRemoveAttribute);
                }
            }
            return andRemoveAttribute;
        }

        internal static XmlNode GetAndRemoveIntegerAttribute(XmlNode node, string attrib, ref int val)
        {
            return GetAndRemoveIntegerAttributeInternal(node, attrib, false, ref val);
        }

        internal static void CheckForUnrecognizedAttributes(XmlNode node)
        {
            if (node.Attributes.Count != 0)
            {
                throw new ConfigurationErrorsException(
                    SR.GetString("Config_base_unrecognized_attribute", node.Attributes[0].Name), node);
            }
        }

        internal static string RemoveAttribute(XmlNode node, string name)
        {
            var xmlNode = node.Attributes.RemoveNamedItem(name);
            return xmlNode?.Value;
        }

        internal static string RemoveRequiredAttribute(XmlNode node, string name)
        {
            return RemoveRequiredAttribute(node, name, false);
        }

        /// <summary>
        /// 仅针对 value值为空时去节点读取CDATA值时使用
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string RemoveRequiredValue(XmlNode node, string name)
        {
            var xmlNode = node.Attributes.RemoveNamedItem(name);
            string text;
            text = string.IsNullOrWhiteSpace(xmlNode?.Value) ? node.InnerText.Trim() : xmlNode.Value;
            if (text == null) throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_none", name), node);
            return text;
        }

        internal static string RemoveRequiredAttribute(XmlNode node, string name, bool allowEmpty)
        {
            var xmlNode = node.Attributes.RemoveNamedItem(name);
            if (xmlNode == null)
            {
                throw new ConfigurationErrorsException(
                    SR.GetString("Config_base_required_attribute_missing", name), node);
            }
            if (string.IsNullOrEmpty(xmlNode.Value) && !allowEmpty)
            {
                throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_empty", name), node);
            }
            return xmlNode.Value;
        }

        internal static void CheckForNonElement(XmlNode node)
        {
            if (node.NodeType != XmlNodeType.Element)
            {
                throw new ConfigurationErrorsException(SR.GetString("Config_base_elements_only"), node);
            }
        }

        internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
        {
            if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.Whitespace)
            {
                return true;
            }
            CheckForNonElement(node);
            return false;
        }

        internal static void CheckForChildNodes(XmlNode node)
        {
            if (node.HasChildNodes)
            {
                throw new ConfigurationErrorsException(SR.GetString("Config_base_no_child_nodes"), node.FirstChild);
            }
        }

        internal static void ThrowUnrecognizedElement(XmlNode node)
        {
            throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), node);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VOWs.MVVM.Model.Data
{
    public enum ExtensionType
    {
        /// <summary>
        /// The <c>UNKNOWN</c> extension type is the default mode for any extension that doesn't match the
        /// other extension types. Although the default VOWs Launcher should prevent you from opening an
        /// unknown extension, the way I designed this software means that others could create custom
        /// launcher applications that may not check for this condition, or may intend to extend the
        /// functionality in a way that allows VOWs to read an unsupported extension.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {}, 
            IsWorkspaceExtension=false, EnforceReadOnlyMode=true)]
        UNKNOWN,
        /// <summary>
        /// The <c>VDOC</c> extension type is the standard extension for a VOWs document.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {".vdoc"},
            IsWorkspaceExtension=false)]
        VDOC,
        /// <summary>
        /// The <c>VWSP</c> extension type is the standard extension for a VOWs workspace.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {".vwsp"},
            IsWorkspaceExtension=true)]
        VWSP,
        /// <summary>
        /// The <c>RTF</c> extension type is a Microsoft-developed text type supporting text styling and
        /// limited support for images and related media. It will be opened in compatibility mode.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {".rtf"},
            IsWorkspaceExtension=false, EnforceCompatibilityMode=true)]
        RTF,
        /// <summary>
        /// The <c>TXT</c> extension type is the standard text file standard. It does not support styling or
        /// anything besides basic textual content. For this reason, the file will be opened in text-only mode.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {".txt", ".log"},
            IsWorkspaceExtension=false, EnforceTextOnlyMode=true)]
        TXT,
        /// <summary>
        /// The <c>HTML</c> extension type is a unique standard, allowing content to be viewed on the web.
        /// This type of file will, by default, be opened in compatibility mode.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {".html", ".htm"},
            IsWorkspaceExtension=false, EnforceCompatibilityMode=true)]
        HTML,
        /// <summary>
        /// The <c>PDF</c> extension type is a Microsoft-developed display format. It supports all of the
        /// standard functionalities of a Word document, though due to the complexities behind editing PDF
        /// files, it will be opened by default in read-only mode.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {".pdf"},
            IsWorkspaceExtension=false, EnforceReadOnlyMode=true)]
        PDF,
        /// <summary>
        /// The <c>ZIP</c> extension type is the alternate format for VOWs Workspaces. This allows the documents
        /// & content stored within to be easily viewed and edited by other external programs.
        /// </summary>
        [ExtensionMeta(Extensions=new string[] {".zip"},
            IsWorkspaceExtension=true)]
        ZIP
    }

    [AttributeUsage(System.AttributeTargets.Field)]
    public class ExtensionMeta : System.Attribute
    {
        /// <summary>
        /// The <c>Extensions</c> property refers to the string expression of this extention. It is used
        /// to match a file with it's valid extension, and is thus very important. These are case-insensitive.
        /// </summary>
        public string[] Extensions { get; set; }
        /// <summary>
        /// The <c>IsWorkspaceExtension</c> property indicates whether the related <c>ExtensionType</c> is
        /// a valid <c>Workspace</c> extension. If this property is <c>false</c>, it is assumed the related
        /// <c>ExtensionType</c> is a valid <c>Document</c> extension.
        /// </summary>
        public bool IsWorkspaceExtension { get; set; }
        /// <summary>
        /// The <c>EnforceCompatibilityMode</c> property indicates whether the related <c>ExtensionType</c>
        /// should enforce the usage of compatibility mode while open. In Compatibility Mode, all version
        /// control and other custom VOWs features will be disabled. Files opened in this mode may not be
        /// saved correctly or in the same way you intended - it's recommended the file be resaved in a
        /// VOWs file format (".vwsp" or ".vdoc").
        /// </summary>
        public bool EnforceCompatibilityMode { get; set; } = false;
        /// <summary>
        /// The <c>EnforceTextOnlyMode</c> property indicates whether the related <c>ExtensionType</c>
        /// should enforce the usage of text only mode while open. In Text Only Mode, only text-related
        /// features will be enabled - tabs for Media features may still be visible, but won't be accessible.
        /// </summary>
        public bool EnforceTextOnlyMode { get; set; } = false;
        /// <summary>
        /// The <c>EnforceReadOnlyMode</c> property indicates whether the related <c>ExtensionType</c>
        /// should enforce the usage of read onyl mode while ope. In Read Only Mode, all editing features
        /// will be disabled, often because the file format may not be editable or are not entirely
        /// supported.
        /// </summary>
        public bool EnforceReadOnlyMode { get; set; } = false;
    }

    public static class ExtensionUtils
    {
        /// <summary>
        /// The <c>DocumentExtensions</c> property holds a dictionary of all <c>ExtensionType</c> objects and
        /// accompanying <c>ExtensionMeta</c> attributes where <c>ExtensionMeta.IsWorkspaceExtension</c> is <c>false</c>.
        /// </summary>
        public static Dictionary<ExtensionType, ExtensionMeta> DocumentExtensions
        {
            get
            {
                Dictionary<ExtensionType, ExtensionMeta> extensions = new();
                // Iterate through all of the enum members.
                foreach (string s in Enum.GetNames(typeof(ExtensionType)))
                {
                    // Convert the string representation of this back to an ExtensionType.
                    ExtensionType? type = Enum.Parse(typeof(ExtensionType), s) as ExtensionType?;
                    if (type == null) continue;
                    // Retrieve the attribute associated with this enum member.
                    ExtensionMeta meta = GetMeta((ExtensionType)type);
                    if (!meta.IsWorkspaceExtension) extensions.Add((ExtensionType)type, meta);
                }
                // Return the dictionary.
                return extensions;
            }
        }

        /// <summary>
        /// The <c>WorkspaceExtensions</c> property holds a dictionary of all <c>ExtensionType</c> objects and
        /// accompanying <c>ExtensionMeta</c> attributes where <c>ExtensionMeta.IsWorkspaceExtension</c> is <c>true</c>.
        /// </summary>
        public static Dictionary<ExtensionType, ExtensionMeta> WorkspaceExtensions
        {
            get
            {
                Dictionary<ExtensionType, ExtensionMeta> extensions = new();
                // Iterate through all of the enum members.
                foreach (string s in Enum.GetNames(typeof(ExtensionType)))
                {
                    // Convert the string representation of this back to an ExtensionType.
                    ExtensionType? type = Enum.Parse(typeof(ExtensionType), s) as ExtensionType?;
                    if (type == null) continue;
                    // Retrieve the attribute associated with this enum member.
                    ExtensionMeta meta = GetMeta((ExtensionType)type);
                    if (meta.IsWorkspaceExtension) extensions.Add((ExtensionType)type, meta);
                }
                // Return the dictionary.
                return extensions;
            }
        }

        /// <summary>
        /// The <c>GetType</c> method will retrieve the associated <c>ExtensionType</c> with a string extension.
        /// If there isn't any matching <c>ExtensionType</c>s, <c>ExtensionType.UNKNOWN</c> will be returned.
        /// </summary>
        /// <param name="extension">The extension, with the leading dot.</param>
        /// <returns>An <c>ExtensionType</c> object.</returns>
        public static ExtensionType GetType(string extension)
        {
            // Let's begin by compiling a combined list of all document and workspace extensions.
            // We can match one if it's there.
            Dictionary<ExtensionType, ExtensionMeta> extensionTypes = DocumentExtensions.Concat(WorkspaceExtensions).ToDictionary(x => x.Key, x => x.Value);
            foreach (KeyValuePair<ExtensionType, ExtensionMeta> ext in extensionTypes)
            {
                if (ext.Value.Extensions.Contains(extension.ToLower()))
                {
                    return ext.Key;
                }
            }
            // None was found, so we'll return ExtensionType.UNKNOWN.
            return ExtensionType.UNKNOWN;
        }

        /// <summary>
        /// The <c>GetMeta</c> method will retrieve the associated <c>ExtensionMeta</c> with a <c>ExtensionType</c>.
        /// As it is a requirement to have meta associated with an <c>ExtensionType</c> dictating it's state, modes, etc.,
        /// it will raise an error when this attribute cannot be found.
        /// </summary>
        /// <param name="extension">The <c>ExtensionType</c> object.</param>
        /// <returns>An <c>ExtensionMeta</c> object.</returns>
        public static ExtensionMeta GetMeta(ExtensionType extension)
        {
            FieldInfo? fi = typeof(ExtensionType).GetField(extension.ToString());
            return fi.GetCustomAttribute(typeof(ExtensionMeta)) as ExtensionMeta;
        }
    }
}

// --------------------------------------------------------------------------------
// Name:     SvgRoot
//
// Author:   Maurizio Bigoloni <big71@fastwebnet.it>
//           See the ReleaseNote.txt file for copyright and license information.
//
// Remarks:
//
// --------------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace SVGLibrary
{
	/// <summary>
	/// It represents the svg SVG element that is the root of the document.
	/// </summary>
	public class SVGRoot : SVGElement
	{
		/// <summary>
		/// Standard XML namespace.
		/// </summary>
		[Category("Svg")]
		[Description("Standard XML namespace.")]
		public string XmlNs
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSvg_XmlNs);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSvg_XmlNs, value);
			}
		}

		/// <summary>
		/// Standard XML version.
		/// </summary>
		[Category("Svg")]
		[Description("Standard XML version.")]
		public string Version
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSvg_Version);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSvg_Version, value);
			}
		}

		/// <summary>
		/// The width of the svg area.
		/// </summary>
		[Category("(Specific)")]
		[Description("The width of the svg area.")]
		public string Width
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_Width);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_Width, value);
			}
		}

		/// <summary>
		/// The height of the svg area.
		/// </summary>
		[Category("(Specific)")]
		[Description("The height of the svg area.")]
		public string Height
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_Height);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_Height, value);
			}
		}

		internal SVGRoot(SVGDocument doc):base(doc)
		{
			m_sElementName = "svg";
			m_ElementType = SvgElementType.typeSvg;

			AddAttr(SVGAttribute._SvgAttribute.attrSvg_XmlNs, "");
			AddAttr(SVGAttribute._SvgAttribute.attrSvg_Version, "");

			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_Width, "");
			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_Height, "");
		}
	}
}

// --------------------------------------------------------------------------------
// Name:     SvgCircle
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
	/// It represents the circle SVG element.
	/// </summary>
	public class Circle : BasicShape
	{
		/// <summary>
		/// The x-axis coordinate of the center of the circle.
		/// </summary>
		[Category("(Specific)")]
		[Description("The x-axis coordinate of the center of the circle.")]
		public string CX
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_CX);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_CX, value);
			}
		}

		/// <summary>
		/// The y-axis coordinate of the center of the circle.
		/// </summary>
		[Category("(Specific)")]
		[Description("The y-axis coordinate of the center of the circle.")]
		public string CY
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_CY);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_CY, value);
			}
		}

		/// <summary>
		/// The radius of the circle.
		/// </summary>
		[Category("(Specific)")]
		[Description("The radius of the circle.")]
		public string R
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_R);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_R, value);
			}
		}

		/// <summary>
		/// It constructs a circle element with no attribute.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		public Circle(Document doc):base(doc)
		{
			Init();
		}

		/// <summary>
		/// It constructs a circle element.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		/// <param name="sCX"></param>
		/// <param name="sCY"></param>
		/// <param name="sRadius"></param>
		public Circle(Document doc, string sCX, string sCY, string sRadius):base(doc)
		{
			Init();

			CX = sCX;
			CY = sCY;
			R = sRadius;
		}

		private void Init()
		{
			m_sElementName = "circle";
			m_ElementType = SvgElementType.typeCircle;

			AddAttr(Attribute._SvgAttribute.attrSpecific_CX, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_CY, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_R, "");
		}
	}
}

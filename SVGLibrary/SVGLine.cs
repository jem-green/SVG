using System;
using System.ComponentModel;
using System.Drawing;

namespace SVGLibrary
{
	/// <summary>
	/// It represents the line SVG element.
	/// </summary>
	public class SVGLine : SVGBasicShape
	{
		/// <summary>
		/// The x-axis coordinate of the start point of the line.
		/// </summary>
		[Category("(Specific)")]
		[Description("The x-axis coordinate of the start point of the line.")]
		public string X1
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_X1);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_X1, value);
			}
		}

		/// <summary>
		/// The y-axis coordinate of the start point of the line.
		/// </summary>
		[Category("(Specific)")]
		[Description("The y-axis coordinate of the start point of the line.")]
		public string Y1
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_Y1);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_Y1, value);
			}
		}

		/// <summary>
		/// The x-axis coordinate of the end point of the line.
		/// </summary>
		[Category("(Specific)")]
		[Description("The x-axis coordinate of the end point of the line.")]
		public string X2
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_X2);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_X2, value);
			}
		}

		/// <summary>
		/// The y-axis coordinate of the end point of the line.
		/// </summary>
		[Category("(Specific)")]
		[Description("The y-axis coordinate of the end point of the line.")]
		public string Y2
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_Y2);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_Y2, value);
			}
		}

		/// <summary>
		/// It constructs an line element with no attribute.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		public SVGLine(SVGDocument doc):base(doc)
		{
			Init();
		}

		/// <summary>
		/// It constructs an line element.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		/// <param name="sX1"></param>
		/// <param name="sY1"></param>
		/// <param name="sX2"></param>
		/// <param name="sY2"></param>
		/// <param name="col"></param>
		public SVGLine(SVGDocument doc, string sX1, string sY1, string sX2, string sY2, Color col):base(doc)
		{
			Init();

			X1 = sX1;
			Y1 = sY1;
			X2 = sX2;
			Y2 = sY2;
			Fill = col;
		}

		private void Init()
		{
			m_sElementName = "line";
			m_ElementType = SvgElementType.typeLine;

			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_X1, "");
			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_Y1, "");
			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_X2, "");
			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_Y2, "");
		}
	}
}

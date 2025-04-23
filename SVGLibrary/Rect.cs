// --------------------------------------------------------------------------------
// Name:     SvgRect
//
// Author:   Maurizio Bigoloni <big71@fastwebnet.it>
//           See the ReleaseNote.txt file for copyright and license information.
//
// Remarks:
//
// --------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;

namespace SVGLibrary
{
	/// <summary>
	/// It represents the rect SVG element.
	/// </summary>
	public class Rect : BasicShape
	{
		/// <summary>
		/// X-axis coordinate of the side of the element which has the smaller x-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.
		/// </summary>
		[Category("(Specific)")]
		[Description("X-axis coordinate of the side of the element which has the smaller x-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.")]
		public string X
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_X);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_X, value);
			}
		}

		/// <summary>
		/// Y-axis coordinate of the side of the element which has the smaller y-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.
		/// </summary>
		[Category("(Specific)")]
		[Description("Y-axis coordinate of the side of the element which has the smaller y-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.")]
		public string Y
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_Y);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_Y, value);
			}
		}

		/// <summary>
		/// Width of the element. A value of zero disables rendering of the element.
		/// </summary>
		[Category("(Specific)")]
		[Description("Width of the element. A value of zero disables rendering of the element.")]
		public string Width
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_Width);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_Width, value);
			}
		}

		/// <summary>
		/// Height of the element. A value of zero disables rendering of the element.
		/// </summary>
		[Category("(Specific)")]
		[Description("Height of the element. A value of zero disables rendering of the element.")]
		public string Height
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_Height);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_Height, value);
			}
		}

		/// <summary>
		/// For rounded rectangles, the x-axis radius of the ellipse used to round off the corners of the rectangle.
		/// </summary>
		[Category("(Specific)")]
		[Description("For rounded rectangles, the x-axis radius of the ellipse used to round off the corners of the rectangle.")]
		public string RX
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_RX);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_RX, value);
			}
		}

		/// <summary>
		/// For rounded rectangles, the y-axis radius of the ellipse used to round off the corners of the rectangle.
		/// </summary>
		[Category("(Specific)")]
		[Description("For rounded rectangles, the y-axis radius of the ellipse used to round off the corners of the rectangle.")]
		public string RY
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_RY);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_RY, value);
			}
		}

		/// <summary>
		/// It constructs a rect element with no attribute.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		public Rect(Document doc):base(doc)
		{
			Init();
		}

		/// <summary>
		/// It constructs a rect element.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		/// <param name="sX"></param>
		/// <param name="sY"></param>
		/// <param name="sWidth"></param>
		/// <param name="sHeight"></param>
		/// <param name="sStrokeWidth"></param>
		/// <param name="colFill"></param>
		/// <param name="colStroke"></param>
		public Rect(Document doc, 
			           string sX, 
			           string sY, 
			           string sWidth, 
			           string sHeight, 
			           string sStrokeWidth,
			           Color colFill, 
			           Color colStroke):base(doc)
		{
			Init();

			X = sX;
			Y = sY;
			Width = sWidth;
			StrokeWidth = sStrokeWidth;
			Height = sHeight;
			Fill = colFill;
			Stroke = colStroke;
		}

		private void Init()
		{
			m_sElementName = "rect";
			m_ElementType = SvgElementType.typeRect;

			AddAttr(Attribute._SvgAttribute.attrSpecific_X, null);
			AddAttr(Attribute._SvgAttribute.attrSpecific_Y, null);
			AddAttr(Attribute._SvgAttribute.attrSpecific_Width, null);
			AddAttr(Attribute._SvgAttribute.attrSpecific_Height, null);
			AddAttr(Attribute._SvgAttribute.attrSpecific_RX, null);
			AddAttr(Attribute._SvgAttribute.attrSpecific_RY, null);
		}
	}
}

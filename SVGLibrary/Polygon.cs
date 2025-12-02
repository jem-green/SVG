// --------------------------------------------------------------------------------
// Name:     SvgPolygon
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
	/// It represents the polygon SVG element.
	/// The 'polygon' element defines a closed shape consisting of a set of connected straight line segments.
	/// </summary>
	public class Polygon : BasicShape
	{
		/// <summary>
		/// The points that make up the polygon. All coordinate values are in the user coordinate system.
		/// </summary>
		[Category("(Specific)")]
		[Description("The points that make up the polygon. All coordinate values are in the user coordinate system.")]
		public string Points
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_Points);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_Points, value);
			}
		}

		/// <summary>
		/// It constructs a polygon element with no attribute.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		public Polygon(SVGDocument doc):base(doc)
		{
			Init();
		}

		/// <summary>
		/// It constructs a polygon element.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		/// <param name="sPoints"></param>
		public Polygon(SVGDocument doc, string sPoints):base(doc)
		{
			Init();

			Points = sPoints;
		}

		private void Init()
		{
			m_sElementName = "polygon";
			m_ElementType = SvgElementType.typePolygon;

			AddAttr(Attribute._SvgAttribute.attrSpecific_Points, "");
		}
	}
}

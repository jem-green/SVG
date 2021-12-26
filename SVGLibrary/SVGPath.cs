// --------------------------------------------------------------------------------
// Name:     SvgPath
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
	/// It represents the path SVG element.
	/// Paths represent the outline of a shape which can be filled, stroked, used as a clipping path, or any combination of the three.
	/// </summary>
	public class SVGPath : SVGBasicShape
	{
		#region Constructor

		/// <summary>
		/// It constructs a path element with no attribute.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		public SVGPath(SVGDocument doc) : base(doc)
		{
			Init();
		}

		/// <summary>
		/// It constructs a path element.
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="sPathData">SVG document.</param>
		public SVGPath(SVGDocument doc, string sPathData) : base(doc)
		{
			Init();
			PathData = sPathData;
		}

		#endregion
		#region Properties

		/// <summary>
		/// The definition of the outline of a shape.
		/// </summary>
		[Category("(Specific)")]
		[Description("The definition of the outline of a shape.")]
		public string PathData
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_PathData);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_PathData, value);
			}
		}

		/// <summary>
		/// The author's computation of the total length of the path, in user units.
		/// </summary>
		[Category("(Specific)")]
		[Description("The author's computation of the total length of the path, in user units.")]
		public string PathLength
		{
			get	
			{
				return GetAttributeStringValue(SVGAttribute._SvgAttribute.attrSpecific_PathLength);	
			}

			set	
			{
				SetAttributeValue(SVGAttribute._SvgAttribute.attrSpecific_PathLength, value);
			}
		}

        #endregion
        #region Private

        private void Init()
		{
			m_sElementName = "path";
			m_ElementType = SvgElementType.typePath;

			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_PathData, "");
			AddAttr(SVGAttribute._SvgAttribute.attrSpecific_PathLength, "");
		}

		


        #endregion
    }
}

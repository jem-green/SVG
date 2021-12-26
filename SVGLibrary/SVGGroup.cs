// --------------------------------------------------------------------------------
// Name:     SvgGroup
//
// Author:   Maurizio Bigoloni <big71@fastwebnet.it>
//           See the ReleaseNote.txt file for copyright and license information.
//
// Remarks:
//
// --------------------------------------------------------------------------------

using System;

namespace SVGLibrary
{
	/// <summary>
	/// It represents the group SVG element.
	/// </summary>
	public class SVGGroup : SVGElement
	{
		/// <summary>
		/// It constructs a group element with no attribute.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		public SVGGroup(SVGDocument doc):base(doc)
		{
			m_sElementName = "g";
			m_ElementType = SvgElementType.typeGroup;
		}
	}
}

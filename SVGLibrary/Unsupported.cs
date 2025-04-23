// --------------------------------------------------------------------------------
// Name:     SvgUnsupported
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
	/// This class does not represent any SVG element. It is used when parsing an SVG file an
	/// unknown element is found.
	/// </summary>
	public class Unsupported : Element
	{
		public Unsupported(Document doc, string sName):base(doc)
		{
			m_sElementName = sName + ":unsupported";
			m_ElementType = SvgElementType.typeUnsupported;
		}
	}
}

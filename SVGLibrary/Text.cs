// --------------------------------------------------------------------------------
// Name:     SvgText
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
	/// It represents the text SVG element.
	/// </summary>
	public class Text : Element
	{
		/// <summary>
		/// Specifies a base URI other than the base URI of the document or external entity.
		/// </summary>
		[Category("(Core)")]
		[Description("Specifies a base URI other than the base URI of the document or external entity.")]
		public string XmlBase
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrCore_XmlBase);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrCore_XmlBase, value);
			}
		}

		/// <summary>
		/// Standard XML attribute to specify the language (e.g., English) used in the contents and attribute values of particular elements.
		/// </summary>
		[Category("(Core)")]
		[Description("Standard XML attribute to specify the language (e.g., English) used in the contents and attribute values of particular elements.")]
		public string XmlLang
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrCore_XmlLang);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrCore_XmlLang, value);
			}
		}

		/// <summary>
		/// Standard XML attribute to specify whether white space is preserved in character data. The only possible values are default and preserve.
		/// </summary>
		[Category("(Core)")]
		[Description("Standard XML attribute to specify whether white space is preserved in character data. The only possible values are default and preserve.")]
		public string XmlSpace
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrCore_XmlSpace);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrCore_XmlSpace, value);
			}
		}

		/// <summary>
		/// This attribute assigns a (CSS) class name or set of class names to an element.
		/// </summary>
		[Category("Style")]
		[Description("This attribute assigns a (CSS) class name or set of class names to an element.")]
		public string Class
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrStyle_Class);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrStyle_Class, value);
			}
		}

		/// <summary>
		/// This attribute specifies style information for the current element. The style attribute specifies style information for a single element.
		/// </summary>
		[Category("Style")]
		[Description("This attribute specifies style information for the current element. The style attribute specifies style information for a single element.")]
		public string Style
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrStyle_Style);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrStyle_Style, value);
			}
		}

		/// <summary>
		/// The value of the element.
		/// </summary>
		[Category("(Specific)")]
		[Description("The value of the element.")]
		public string Value
		{
			get	
			{
				return m_sElementValue;	
			}

			set	
			{
				m_sElementValue =  value;
			}
		}

		/// <summary>
		/// The x-axis coordinate of the side of the element which has the smaller x-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.
		/// </summary>
		[Category("(Specific)")]
		[Description("The x-axis coordinate of the side of the element which has the smaller x-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.")]
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
		/// The y-axis coordinate of the side of the element which has the smaller y-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.
		/// </summary>
		[Category("(Specific)")]
		[Description("The y-axis coordinate of the side of the element which has the smaller y-axis coordinate value in the current user coordinate system. If the attribute is not specified, the effect is as if a value of 0 were specified.")]
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
		/// Shifts in the current text position along the x-axis for the characters within this element or any of its descendants.
		/// </summary>
		[Category("(Specific)")]
		[Description("Shifts in the current text position along the x-axis for the characters within this element or any of its descendants.")]
		public string DX
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_DX);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_DX, value);
			}
		}

		/// <summary>
		/// Shifts in the current text position along the y-axis for the characters within this element or any of its descendants.
		/// </summary>
		[Category("(Specific)")]
		[Description("Shifts in the current text position along the y-axis for the characters within this element or any of its descendants.")]
		public string DY
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_DY);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_DY, value);
			}
		}

		/// <summary>
		/// The supplemental rotation about the current text position that will be applied to all of the glyphs corresponding to each character within this element.
		/// </summary>
		[Category("(Specific)")]
		[Description("The supplemental rotation about the current text position that will be applied to all of the glyphs corresponding to each character within this element.")]
		public string Rotate
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_Rotate);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_Rotate, value);
			}
		}

		/// <summary>
		/// The author's computation of the total sum of all of the advance values that correspond to character data within this element, including the advance value on the glyph (horizontal or vertical), the effect of properties 'kerning', 'letter-spacing' and 'word-spacing' and adjustments due to attributes dx and dy on 'tspan' elements. This value is used to calibrate the user agent's own calculations with that of the author.
		/// </summary>
		[Category("(Specific)")]
		[Description("The author's computation of the total sum of all of the advance values that correspond to character data within this element, including the advance value on the glyph (horizontal or vertical), the effect of properties 'kerning', 'letter-spacing' and 'word-spacing' and adjustments due to attributes dx and dy on 'tspan' elements. This value is used to calibrate the user agent's own calculations with that of the author.")]
		public string TextLength
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrSpecific_TextLength);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_TextLength, value);
			}
		}

		/// <summary>
		/// Indicates the type of adjustments which the user agent shall make to make the rendered length of the text match the value specified on the textLength attribute.
		/// </summary>
		[Category("(Specific)")]
		[Description("Indicates the type of adjustments which the user agent shall make to make the rendered length of the text match the value specified on the textLength attribute.")]
		public Attribute._SvgLengthAdjust LengthAdjust
		{
			get	
			{
				return (Attribute._SvgLengthAdjust) GetAttributeIntValue(Attribute._SvgAttribute.attrSpecific_LengthAdjust);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrSpecific_LengthAdjust, (int) value);
			}
		}

		/// <summary>
		/// Indicates which font family is to be used to render the text, specified as a prioritized list of font family names and/or generic family names.
		/// </summary>
		[Category("Font")]
		[Description("Indicates which font family is to be used to render the text, specified as a prioritized list of font family names and/or generic family names.")]
		public string FontFamily
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrFont_Family);
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrFont_Family, value);
			}
		}

		/// <summary>
		/// This property refers to the size of the font from baseline to baseline when multiple lines of text are set solid in a multiline layout environment.
		/// </summary>
		[Category("Font")]
		[Description("This property refers to the size of the font from baseline to baseline when multiple lines of text are set solid in a multiline layout environment.")]
		public string FontSize
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrFont_Size);
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrFont_Size, value);
			}
		}

		/// <summary>
		/// This property allows authors to specify an aspect value for an element that will preserve the x-height of the first choice font in a substitute font.
		/// </summary>
		[Category("Font")]
		[Description("This property allows authors to specify an aspect value for an element that will preserve the x-height of the first choice font in a substitute font.")]
		public string SizeAdjust
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrFont_SizeAdjust);
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrFont_SizeAdjust, value);
			}
		}

		/// <summary>
		/// This property indicates the desired amount of condensing or expansion in the glyphs used to render the text.
		/// </summary>
		[Category("Font")]
		[Description("This property indicates the desired amount of condensing or expansion in the glyphs used to render the text.")]
		public Attribute._SvgFontStretch Stretch
		{
			get	
			{
				return (Attribute._SvgFontStretch) GetAttributeIntValue(Attribute._SvgAttribute.attrFont_Stretch);
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrFont_Stretch, (int) value);
			}
		}

		/// <summary>
		/// It constructs a text element with no attribute.
		/// </summary>
		/// <param name="doc">SVG document.</param>
		public Text(Document doc):base(doc)
		{
			m_sElementName = "text";
			m_bHasValue = true;
			m_ElementType = SvgElementType.typeText;

			AddAttr(Attribute._SvgAttribute.attrCore_XmlBase, "");
			AddAttr(Attribute._SvgAttribute.attrCore_XmlLang, "");
			AddAttr(Attribute._SvgAttribute.attrCore_XmlSpace, "");

			AddAttr(Attribute._SvgAttribute.attrSpecific_X, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_Y, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_DX, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_DY, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_Rotate, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_TextLength, "");
			AddAttr(Attribute._SvgAttribute.attrSpecific_LengthAdjust, 0);

			AddAttr(Attribute._SvgAttribute.attrStyle_Class, "");
			AddAttr(Attribute._SvgAttribute.attrStyle_Style, "");

			AddAttr(Attribute._SvgAttribute.attrFont_Family, "");
			AddAttr(Attribute._SvgAttribute.attrFont_Size, "");
			AddAttr(Attribute._SvgAttribute.attrFont_SizeAdjust, "");
			AddAttr(Attribute._SvgAttribute.attrFont_Stretch, 0);
			AddAttr(Attribute._SvgAttribute.attrFont_Style, "");
			AddAttr(Attribute._SvgAttribute.attrFont_Variant, "");
			AddAttr(Attribute._SvgAttribute.attrFont_Weight, "");
		}
	}
}

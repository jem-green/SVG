// --------------------------------------------------------------------------------
// Name:     SvgBasicShape
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
	/// Summary description for SvgBasicShape.
	/// </summary>
	public class BasicShape : Element
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
		/// It is the explicit color to be used to paint the current object.
		/// </summary>
		[Category("Paint")]
		[Description("It is the explicit color to be used to paint the current object.")]
		public Color Color
		{
			get	
			{
				return GetAttributeColorValue(Attribute._SvgAttribute.attrPaint_Color);
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_Color, value);
			}
		}

		/// <summary>
		/// The uniform opacity setting to be applied across an entire object. Any values outside the range 0.0 (fully transparent) to 1.0 (fully opaque) will be clamped to this range.
		/// </summary>
		[Category("Opacity")]
		[Description("The uniform opacity setting to be applied across an entire object. Any values outside the range 0.0 (fully transparent) to 1.0 (fully opaque) will be clamped to this range.")]
		public string Opacity
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrOpacity_Opacity);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrOpacity_Opacity, value);
			}
		}

		/// <summary>
		/// Paints the interior of the given graphical element.
		/// </summary>
		[Category("Paint")]
		[Description("Paints the interior of the given graphical element.")]
		public Color Fill
		{
			get	
			{
				return GetAttributeColorValue(Attribute._SvgAttribute.attrPaint_Fill);
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_Fill, value);
			}
		}

		/// <summary>
		/// It indicates the algorithm which is to be used to determine what parts of the canvas are included inside the shape.
		/// </summary>
		[Category("Paint")]
		[Description("It indicates the algorithm which is to be used to determine what parts of the canvas are included inside the shape.")]
		public Attribute._SvgFillRule FillRule
		{
			get	
			{
				return (Attribute._SvgFillRule) GetAttributeIntValue(Attribute._SvgAttribute.attrPaint_FillRule);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_FillRule, (int) value);
			}
		}

		/// <summary>
		/// It specifies the opacity of the painting operation used to paint the interior the current object.
		/// </summary>
		[Category("Opacity")]
		[Description("It specifies the opacity of the painting operation used to paint the interior the current object.")]
		public string FillOpacity
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrOpacity_FillOpacity);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrOpacity_FillOpacity, value);
			}
		}

		/// <summary>
		/// Paints along the outline of the given graphical element.
		/// </summary>
		[Category("Paint")]
		[Description("Paints along the outline of the given graphical element.")]
		public Color Stroke
		{
			get	
			{
				return GetAttributeColorValue(Attribute._SvgAttribute.attrPaint_Stroke);
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_Stroke, value);
			}
		}

		/// <summary>
		/// It specifies the opacity of the painting operation used to stroke the current object.
		/// </summary>
		[Category("Opacity")]
		[Description("It specifies the opacity of the painting operation used to stroke the current object.")]
		public string StrokeOpacity
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrOpacity_StrokeOpacity);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrOpacity_StrokeOpacity, value);
			}
		}

		/// <summary>
		/// The width of the stroke on the current object. If a percentage is used, the value represents a percentage of the current viewport.
		/// </summary>
		[Category("Paint")]
		[Description("The width of the stroke on the current object. If a percentage is used, the value represents a percentage of the current viewport.")]
		public string StrokeWidth
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrPaint_StrokeWidth);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_StrokeWidth, value);
			}
		}

		/// <summary>
		/// It controls the pattern of dashes and gaps used to stroke paths. <dasharray> contains a list of comma-separated (with optional white space) <length>s that specify the lengths of alternating dashes and gaps.
		/// </summary>
		[Category("Paint")]
		[Description("It controls the pattern of dashes and gaps used to stroke paths. <dasharray> contains a list of comma-separated (with optional white space) <length>s that specify the lengths of alternating dashes and gaps.")]
		public string StrokeDashArray
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrPaint_StrokeDashArray);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_StrokeDashArray, value);
			}
		}

		/// <summary>
		/// It specifies the distance into the dash pattern to start the dash.
		/// </summary>
		[Category("Paint")]
		[Description("It specifies the distance into the dash pattern to start the dash.")]
		public string StrokeDashOffSet
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrPaint_StrokeDashOffSet);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_StrokeDashOffSet, value);
			}
		}

		/// <summary>
		/// It specifies the shape to be used at the end of open subpaths when they are stroked.
		/// </summary>
		[Category("Paint")]
		[Description("It specifies the shape to be used at the end of open subpaths when they are stroked.")]
		public Attribute._SvgLineCap StrokeLineCap
		{
			get	
			{
				return (Attribute._SvgLineCap) GetAttributeIntValue(Attribute._SvgAttribute.attrPaint_StrokeLineCap);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_StrokeLineCap, (int) value);
			}
		}

		/// <summary>
		/// It specifies the shape to be used at the corners of paths or basic shapes when they are stroked.
		/// </summary>
		[Category("Paint")]
		[Description("It specifies the shape to be used at the corners of paths or basic shapes when they are stroked.")]
		public Attribute._SvgLineJoin StrokeLineJoin
		{
			get	
			{
				return (Attribute._SvgLineJoin) GetAttributeIntValue(Attribute._SvgAttribute.attrPaint_StrokeLineJoin);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_StrokeLineJoin, (int) value);
			}
		}

		/// <summary>
		/// When two line segments meet at a sharp angle and miter joins have been specified for 'stroke-linejoin', it is possible for the miter to extend far beyond the thickness of the line stroking the path. The 'stroke-miterlimit' imposes a limit on the ratio of the miter length to the 'stroke-width'. When the limit is exceeded, the join is converted from a miter to a bevel.
		/// </summary>
		[Category("Paint")]
		[Description("When two line segments meet at a sharp angle and miter joins have been specified for 'stroke-linejoin', it is possible for the miter to extend far beyond the thickness of the line stroking the path. The 'stroke-miterlimit' imposes a limit on the ratio of the miter length to the 'stroke-width'. When the limit is exceeded, the join is converted from a miter to a bevel.")]
		public string StrokeMiterLimit
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrPaint_StrokeMiterLimit);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_StrokeMiterLimit, value);
			}
		}

		/// <summary>
		/// It specifies the color space for gradient interpolations, color animations and alpha compositing.
		/// </summary>
		[Category("Paint")]
		[Description("It specifies the color space for gradient interpolations, color animations and alpha compositing.")]
		public Attribute._SvgColorInterpolation ColorInterpolation
		{
			get	
			{
				return (Attribute._SvgColorInterpolation) GetAttributeIntValue(Attribute._SvgAttribute.attrPaint_ColorInterpolation);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_ColorInterpolation, (int) value);
			}
		}

		/// <summary>
		/// It specifies the color space for imaging operations performed via filter effects.
		/// </summary>
		[Category("Paint")]
		[Description("It specifies the color space for imaging operations performed via filter effects.")]
		public Attribute._SvgColorInterpolation ColorInterpolationFilters
		{
			get	
			{
				return (Attribute._SvgColorInterpolation) GetAttributeIntValue(Attribute._SvgAttribute.attrPaint_ColorInterpolationFilters);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_ColorInterpolationFilters, (int) value);
			}
		}

		/// <summary>
		/// It provides a hint to the SVG user agent about how to optimize its color interpolation and compositing operations.
		/// </summary>
		[Category("Paint")]
		[Description("It provides a hint to the SVG user agent about how to optimize its color interpolation and compositing operations.")]
		public Attribute._SvgColorRendering ColorRendering
		{
			get	
			{
				return (Attribute._SvgColorRendering) GetAttributeIntValue(Attribute._SvgAttribute.attrPaint_ColorRendering);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrPaint_ColorRendering, (int) value);
			}
		}

		/// <summary>
		/// It control the visibility of the graphical element.
		/// </summary>
		/// <remarks>
		/// When applied to a container element, setting 'display' to none causes the container and all of its children 
		/// to be invisible; thus, it acts on groups of elements as a group. 
		/// 'visibility', however, only applies to individual graphics elements. Setting 'visibility' to hidden on a 'g' 
		/// will make its children invisible as long as the children do not specify their own 'visibility' properties as 
		/// visible. Note that 'visibility' is not an inheritable property. 
		/// When the 'display' property is set to none, then the given element does not become part of the rendering tree. 
		/// With 'visibility' set to hidden, however, processing occurs as if the element were part of the rendering tree 
		/// and still taking up space, but not actually rendered onto the canvas.
		/// </remarks>
		[Category("Graphics")]
		[Description("It control the visibility of the graphical element.")]
		public Attribute._SvgGraphicsDisplay Display
		{
			get	
			{
				return (Attribute._SvgGraphicsDisplay) GetAttributeIntValue(Attribute._SvgAttribute.attrGraphics_Display);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrGraphics_Display, (int) value);
			}
		}

		/// <summary>
		/// It provides a hint how to make speed vs. quality tradeoffs as it performs image processing.
		/// </summary>
		[Category("Graphics")]
		[Description("It provides a hint how to make speed vs. quality tradeoffs as it performs image processing.")]
		public Attribute._SvgImageRendering ImageRendering
		{
			get	
			{
				return (Attribute._SvgImageRendering) GetAttributeIntValue(Attribute._SvgAttribute.attrGraphics_ImageRendering);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrGraphics_ImageRendering, (int) value);
			}
		}

		/// <summary>
		/// It provides a hint about what tradeoffs to make as it renders vector graphics elements such as 'path' elements and basic shapes such as circles and rectangles.
		/// </summary>
		[Category("Graphics")]
		[Description("It provides a hint about what tradeoffs to make as it renders vector graphics elements such as 'path' elements and basic shapes such as circles and rectangles.")]
		public Attribute._SvgShapeRendering ShapeRendering
		{
			get	
			{
				return (Attribute._SvgShapeRendering) GetAttributeIntValue(Attribute._SvgAttribute.attrGraphics_ShapeRendering);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrGraphics_ShapeRendering, (int) value);
			}
		}

		/// <summary>
		/// It provides a hint to the SVG user agent about how to optimize its color interpolation and compositing operations.
		/// </summary>
		[Category("Graphics")]
		[Description("It provides a hint to the SVG user agent about how to optimize its color interpolation and compositing operations.")]
		public Attribute._SvgTextRendering TextRendering
		{
			get	
			{
				return (Attribute._SvgTextRendering) GetAttributeIntValue(Attribute._SvgAttribute.attrGraphics_TextRendering);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrGraphics_TextRendering, (int) value);
			}
		}

		/// <summary>
		/// It control the visibility of the graphical element.
		/// </summary>
		/// <remarks>
		/// When applied to a container element, setting 'display' to none causes the container and all of its children 
		/// to be invisible; thus, it acts on groups of elements as a group. 
		/// 'visibility', however, only applies to individual graphics elements. Setting 'visibility' to hidden on a 'g' 
		/// will make its children invisible as long as the children do not specify their own 'visibility' properties as 
		/// visible. Note that 'visibility' is not an inheritable property. 
		/// When the 'display' property is set to none, then the given element does not become part of the rendering tree. 
		/// With 'visibility' set to hidden, however, processing occurs as if the element were part of the rendering tree 
		/// and still taking up space, but not actually rendered onto the canvas.
		/// </remarks>
		[Category("Graphics")]
		[Description("It control the visibility of the graphical element.")]
		public Attribute._SvgVisibility Visibility
		{
			get	
			{
				return (Attribute._SvgVisibility) GetAttributeIntValue(Attribute._SvgAttribute.attrGraphics_Visiblity);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrGraphics_Visiblity, (int) value);
			}
		}

		protected BasicShape(Document doc):base(doc)
		{
			AddAttr(Attribute._SvgAttribute.attrCore_XmlBase, null);
			AddAttr(Attribute._SvgAttribute.attrCore_XmlLang, null);
			AddAttr(Attribute._SvgAttribute.attrCore_XmlSpace, null);

			AddAttr(Attribute._SvgAttribute.attrStyle_Class, null);
			AddAttr(Attribute._SvgAttribute.attrStyle_Style, null);

			AddAttr(Attribute._SvgAttribute.attrPaint_Color, Color.Transparent);
			AddAttr(Attribute._SvgAttribute.attrPaint_Fill, Color.Transparent);
			AddAttr(Attribute._SvgAttribute.attrPaint_FillRule, 0);
			AddAttr(Attribute._SvgAttribute.attrPaint_Stroke, Color.Transparent);
			AddAttr(Attribute._SvgAttribute.attrPaint_StrokeWidth, null);	
			AddAttr(Attribute._SvgAttribute.attrPaint_StrokeDashArray, null);	
			AddAttr(Attribute._SvgAttribute.attrPaint_StrokeDashOffSet, null);	
			AddAttr(Attribute._SvgAttribute.attrPaint_StrokeLineCap, 0);	
			AddAttr(Attribute._SvgAttribute.attrPaint_StrokeLineJoin, 0);	
			AddAttr(Attribute._SvgAttribute.attrPaint_StrokeMiterLimit, null);	
			AddAttr(Attribute._SvgAttribute.attrPaint_ColorInterpolation, 0);	
			AddAttr(Attribute._SvgAttribute.attrPaint_ColorInterpolationFilters, 0);	
			AddAttr(Attribute._SvgAttribute.attrPaint_ColorRendering, 0);	
			
			AddAttr(Attribute._SvgAttribute.attrGraphics_Display, 0);
			AddAttr(Attribute._SvgAttribute.attrGraphics_PointerEvents, 0);
			AddAttr(Attribute._SvgAttribute.attrGraphics_ImageRendering, 0);	
			AddAttr(Attribute._SvgAttribute.attrGraphics_ShapeRendering, 0);	
			AddAttr(Attribute._SvgAttribute.attrGraphics_TextRendering, 0);	
			AddAttr(Attribute._SvgAttribute.attrGraphics_Visiblity, 0);

			AddAttr(Attribute._SvgAttribute.attrOpacity_Opacity, null);
			AddAttr(Attribute._SvgAttribute.attrOpacity_FillOpacity, null);
			AddAttr(Attribute._SvgAttribute.attrOpacity_StrokeOpacity, null);
			
		}
	}
}

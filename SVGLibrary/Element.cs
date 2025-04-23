// --------------------------------------------------------------------------------
// Name:     SvgElement
//
// Author:   Maurizio Bigoloni <big71@fastwebnet.it>
//           See the ReleaseNote.txt file for copyright and license information.
//
// Remarks:
//
// --------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace SVGLibrary
{
	/// <summary>
	/// This is the base class of any Svg element.
	/// </summary>
	[DefaultProperty("Id")]
	public class Element
	{
		/// <summary>
		/// List all SVG element types. For each element a specific class is defined in the library.
		/// </summary>
		public enum SvgElementType
		{
			typeUnsupported,
			typeSvg,
			typeDesc,
			typeText,
			typeGroup,
			typeRect,
			typeCircle,
			typeEllipse,
			typeLine,
			typePath,
			typePolygon,
			typeImage
		};

        #region Properties

        /// <summary>
        /// Standard XML attribute for assigning a unique name to an element.
        /// </summary>
        /// <remarks></remarks>
        [Category("(Core)")]
		[Description("Standard XML attribute for assigning a unique name to an element.")]
		public string Id
		{
			get	
			{
				return GetAttributeStringValue(Attribute._SvgAttribute.attrCore_Id);	
			}

			set	
			{
				SetAttributeValue(Attribute._SvgAttribute.attrCore_Id, value);
			}
		}

		#endregion
		#region Class

		private class CEleComparer : IComparer  
		{
			int IComparer.Compare( Object x, Object y )  
			{
				Attribute ax = (Attribute) x;
				Attribute ay = (Attribute) y;

				if ( ax.AttributeGroup == ay.AttributeGroup )
				{
					if ( ax.AttributeType < ay.AttributeType )
					{
						return -1;
					}
					else
					{
						return 1;
					}
				}
				else if ( ax.AttributeGroup < ay.AttributeGroup )
				{
					return -1;
				}
				else
				{
					return 1;
				}
			}
		}

        #endregion
        #region Fields

        // navigation
        protected Element m_Parent;
		protected Element m_Child;
		protected Element m_Next; 
		protected Element m_Previous;

		// document
		protected Document m_doc;
		
		// internal stuff
		protected int m_nInternalId;
		protected string m_sElementName;
		protected string m_sElementValue;
		protected bool m_bHasValue;
		protected SvgElementType m_ElementType;

		// attributes
		private ArrayList m_attributes;
		
		#endregion
		#region Methods

		// constructor is protected!

		/// <summary>
		/// It returns the parent element.
		/// </summary>
		/// <returns></returns>
		public Element getParent()
		{
			return m_Parent;
		}

		/// <summary>
		/// It sets the parent element.
		/// </summary>
		/// <param name="ele">New parent element</param>
		public void setParent(Element ele)
		{
			m_Parent = ele;
		}

		/// <summary>
		/// It gest the first child element.
		/// </summary>
		/// <returns>First child element.</returns>
		public Element getChild()
		{
			return m_Child;
		}

		/// <summary>
		/// It sets the first child element.
		/// </summary>
		/// <param name="ele">New child.</param>
		public void setChild(Element ele)
		{
			m_Child = ele;
		}

		/// <summary>
		/// It gets the next sibling element.
		/// </summary>
		/// <returns>Next element.</returns>
		public Element getNext()
		{
			return m_Next;
		}

		/// <summary>
		/// It sets the next sibling element.
		/// </summary>
		/// <param name="ele">New next element.</param>
		public void setNext(Element ele)
		{
			m_Next = ele;
		}

		/// <summary>
		/// It gets the previous sibling element.
		/// </summary>
		/// <returns>Previous element.</returns>
		public Element getPrevious()
		{
			return m_Previous;
		}

		/// <summary>
		/// It sets the previous element.
		/// </summary>
		/// <param name="ele">New previous element.</param>
		public void setPrevious(Element ele)
		{
			m_Previous = ele;
		}

		/// <summary>
		/// It gets the internal Id of the element.
		/// </summary>
		/// <returns>Internal Id.</returns>
		/// <remarks>The internal Id is a unique number inside the SVG document.
		/// It is assigned when the element is added to the document.</remarks>
		public int getInternalId()
		{
			return m_nInternalId;
		}

		/// <summary>
		/// It sets the internal Id of the element.
		/// </summary>
		/// <param name="nId">New internal Id.</param>
		public void setInternalId(int nId)
		{
			m_nInternalId = nId;
		}

		/// <summary>
		/// It returns the SVG element name.
		/// </summary>
		/// <returns>SVG name.</returns>
		public string getElementName()
		{
			return m_sElementName;
		}

		/// <summary>
		/// It returns the current element value.
		/// </summary>
		/// <returns>Element value.</returns>
		/// <remarks>Not all SVG elements are supposed to have a value. For instance a rect element or 
		/// a circle do not usually have a value while a desc or a text they normally have it.</remarks>
		public string getElementValue()
		{
			return m_sElementValue;
		}

		/// <summary>
		/// Sets the element value.
		/// </summary>
		/// <param name="sValue">New element value.</param>
		public void setElementValue(string sValue)
		{
			m_sElementValue = sValue;
		}

		/// <summary>
		/// Flag indicating if a value is expected for the SVG element.
		/// </summary>
		/// <returns>true if the SVG element has normally a value.</returns>
		public bool HasValue()
		{
			return m_bHasValue;
		}

		/// <summary>
		/// It returns the SVG element type.
		/// </summary>
		/// <returns></returns>
		public SvgElementType getElementType()
		{
			return m_ElementType;
		}

		/// <summary>
		/// It returns the XML string of the SVG tree starting from the element.
		/// </summary>
		/// <returns>XML string.</returns>
		/// <remarks>The method is recursive so it creates the SVG string for the current element and for its
		/// sub-tree. If the element is the root of the SVG document the method return the entire SVG XML string.</remarks>
		public string GetXML()
		{
			string sXML;

			sXML = OpenXMLTag();

			if ( m_Child != null )
			{
				sXML += m_Child.GetXML();
			}

			sXML += CloseXMLTag();

			Element ele = m_Next;
			if (ele != null)
			{
				sXML += ele.GetXML();
			}

			// Should not have any logging in a library
			// this should be rased as an exception

			Debug.WriteLine("SvgElement", "GetXML", ElementInfo());
		
			return sXML;
		}

		/// <summary>
		/// It returns the XML string of the SVG element.
		/// </summary>
		/// <returns>XML string.</returns>
		public string GetTagXml()
		{
			string sXML;

			sXML = OpenXMLTag();
			sXML += CloseXMLTag();

			return sXML;
		}

		/// <summary>
		/// It gets all element attributes.
		/// </summary>
		/// <param name="aType">Attribute type array.</param>
		/// <param name="aName">Attribute name array.</param>
		/// <param name="aValue">Attribute value array.</param>
		public void FillAttributeList(ArrayList aType, ArrayList aName, ArrayList aValue)
		{
			IComparer myComparer = new CEleComparer();
			m_attributes.Sort(myComparer);


			for (int i = 0; i < m_attributes.Count; i++ )
			{
				Attribute attr = (Attribute) m_attributes[i];

				aType.Add(attr.AttributeType);
				aName.Add(attr.Name);
				aValue.Add(attr.Value);
			}
		}

		/// <summary>
		/// It copies all the attributes of the element eleToClone to the
		/// current element.
		/// </summary>
		/// <param name="eleToClone">Element that has to be cloned.</param>
		public void CloneAttributeList(Element eleToClone)
		{
			ArrayList aType = new ArrayList();
			ArrayList aName = new ArrayList();
			ArrayList aValue = new ArrayList();

			eleToClone.FillAttributeList(aType, aName, aValue);

			m_attributes.Clear();


			// copy the attributes
			for (int i = 0; i < aType.Count; i++ )
			{
				AddAttr((Attribute._SvgAttribute) aType[i], aValue[i]);
			}

			// copy the value
			if ( m_bHasValue )
			{
				m_sElementValue = eleToClone.m_sElementValue;
			}
		}

		/// <summary>
		/// It returns a string containing current element info for logging purposes.
		/// </summary>
		/// <returns></returns>
		public string ElementInfo()
		{
			string sMsg = "InternalId:" + m_nInternalId.ToString();

			if ( m_Parent != null )
			{
				sMsg += " - Parent:" + m_Parent.getInternalId().ToString();
			}

			if ( m_Previous != null )
			{
				sMsg += " - Previous:" + m_Previous.getInternalId().ToString();
			}

			if ( m_Next != null )
			{
				sMsg += " - Next:" + m_Next.getInternalId().ToString();
			}

			if ( m_Child != null )
			{
				sMsg += " - Child:" + m_Child.getInternalId().ToString();
			}

			return sMsg;
		}

        #endregion
        #region Private

        protected Element(Document doc)
		{
			Debug.WriteLine("SvgElement", "SvgElement", "Element created");

			m_doc = doc;

			m_attributes = new ArrayList();

			AddAttr(Attribute._SvgAttribute.attrCore_Id, null);

			m_Parent = null;
			m_Child = null;
			m_Next = null;
			m_Previous = null;

			m_sElementName = "unsupported";
			m_sElementValue = "";
			m_bHasValue = false;
			m_ElementType = SvgElementType.typeUnsupported;
		}

		~Element()
		{
			Debug.WriteLine("SvgElement", "SvgElement", "Element destroyed, InternalId:" + m_nInternalId.ToString());

			m_Parent = null;
			m_Child = null;
			m_Next = null;
			m_Previous = null;
		}

		protected string OpenXMLTag()
		{
			string sXML;

			sXML = "<" + m_sElementName;

			for (int i = 0; i < m_attributes.Count; i++ )
			{
				Attribute attr = (Attribute) m_attributes[i];
				sXML += attr.GetXML();
			}

			if ( m_sElementValue == "")
			{
				if (m_Child == null)
				{
					sXML += " />\r\n";
				}
				else
				{
					sXML += ">\r\n";
				}
			}
			else
			{
				sXML += ">";
				sXML += m_sElementValue;
			}
			
			return sXML;
		}

		protected string CloseXMLTag()
		{
			if ( (m_sElementValue == "") && (m_Child == null) )
			{
				return "";
			}
			else
			{
				return "</" + m_sElementName + ">\r\n";
			}
		}

		protected void AddAttr(Attribute._SvgAttribute type, object objValue)
		{
			Attribute attrToAdd = new Attribute(type);
			attrToAdd.Value = objValue;

			m_attributes.Add(attrToAdd);
		}

		internal Attribute GetAttribute(string sName)
		{
			for (int i = 0; i < m_attributes.Count; i++ )
			{
				Attribute attr = (Attribute) m_attributes[i];
				if ( attr.Name == sName )
				{
					return attr;
				}
			}
			
			return null;
		}

		internal Attribute GetAttribute(Attribute._SvgAttribute type)
		{
			for (int i = 0; i < m_attributes.Count; i++ )
			{
				Attribute attr = (Attribute) m_attributes[i];
				if ( attr.AttributeType == type )
				{
					return attr;
				}
			}
			
			return null;
		}

		internal bool SetAttributeValue(string sName, string sValue)
		{
			bool bReturn = false;

			for (int i = 0; i < m_attributes.Count; i++ )
			{
				Attribute attr = (Attribute) m_attributes[i];
				if ( attr.Name == sName )
				{
					switch (attr.AttributeDataType)
					{
						case Attribute._SvgAttributeDataType.datatypeString:
						case Attribute._SvgAttributeDataType.datatypeHRef:
							{
								attr.Value = sValue;
								break;
							}
						case Attribute._SvgAttributeDataType.datatypeEnum:
							{
								int nValue = 0;
								try
								{
									nValue = Convert.ToInt32(sValue);
								}
								catch
								{
								}

								attr.Value = nValue;
								break;
							}
						case Attribute._SvgAttributeDataType.datatypeColor:
							{
								if (sValue == "")
								{
									attr.Value = Color.Transparent;
								}
								else
								{
									Color c = attr.String2Color(sValue);
									attr.Value = c;
								}
								break;
							}
						case Attribute._SvgAttributeDataType.datatypePath:
                            {
								// This is where i would like to expand the path
								//attr.Value = Path.Decode(sValue);
								attr.Value = sValue;
								break;
							}
					}

					bReturn = true;

					break;
				}
			}
			
			return bReturn;
		}

		internal bool SetAttributeValue(Attribute._SvgAttribute type, object objValue)
		{
			bool bReturn = false;

			for (int i = 0; i < m_attributes.Count; i++ )
			{
				Attribute attr = (Attribute) m_attributes[i];
				if ( attr.AttributeType == type )
				{
					bReturn = true;
					attr.Value = objValue;

					break;
				}
			}
			
			return bReturn;
		}

		internal bool GetAttributeValue(Attribute._SvgAttribute type, out object objValue)
		{
			bool bReturn = false;
			objValue = null;

			for (int i = 0; i < m_attributes.Count; i++ )
			{
				Attribute attr = (Attribute) m_attributes[i];
				if ( attr.AttributeType == type )
				{
					bReturn = true;
					objValue = attr.Value;

					break;
				}
			}
			
			return bReturn;
		}

		internal object GetAttributeValue(Attribute._SvgAttribute type)
		{
			object objValue;

			if ( GetAttributeValue(type, out objValue) )
			{
				return objValue;
			}
			else
			{
				return null;
			}
		}

		internal string GetAttributeStringValue(Attribute._SvgAttribute type)
		{
			object objValue = GetAttributeValue(type);

			if ( objValue != null )
			{
				return objValue.ToString();
			}
			else
			{
				return "";
			}
		}

		internal int GetAttributeIntValue(Attribute._SvgAttribute type)
		{
			object objValue = GetAttributeValue(type);

			if ( objValue != null )
			{
				int nValue = 0;
				try
				{
					nValue = Convert.ToInt32(objValue.ToString());
				}
				catch
				{
				}

				return nValue;
			}
			else
			{
				return 0;
			}
		}

		internal Color GetAttributeColorValue(Attribute._SvgAttribute type)
		{
			object objValue = GetAttributeValue(type);

			if ( objValue != null )
			{
				Color cValue = Color.Black;
				try
				{
					cValue = (Color) (objValue);
				}
				catch
				{
				}

				return cValue;
			}
			else
			{
				return Color.Black;
			}
		}

#endregion
    }
}

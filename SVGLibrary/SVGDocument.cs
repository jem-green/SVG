using System;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;

namespace SVGLibrary
{
	/// <summary>
	/// It represents the SVG document.
	/// </summary>
	public class SVGDocument : IEnumerable
	{
        #region Fields

        // root of the document 
        private Root _root;
		
		// document elements, hashtable key is the InternalId 
		private Hashtable _elements;

		// store the next InternalId to be assigned to a new element
		private int _nNextId;

		private string _XmlDeclaration;
		private string _XmlDocType;

        public object Current => throw new NotImplementedException();

        #endregion
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public SVGDocument()
		{
			_root = null;
			_nNextId = 1;
			_elements = new Hashtable();
		}

		/// <summary>
		/// Create a SVG document reading from a file.
		/// If a current document exists, it is destroyed.
		/// </summary>
		/// <param name="Filename">The complete path of a valid SVG file.</param>
		/// </summary>
		public SVGDocument(string filename)
		{
			_root = null;
			_nNextId = 1;
			_elements = new Hashtable();
			Load(filename);
		}

#endregion
        #region Methods

        /// <summary>
        /// It creates a new empty SVG document that contains just the root element.
        /// If a current document exists, it is destroyed.
        /// </summary>
        /// <returns>
        /// The root element of the SVG document.
        /// </returns>
        public Root New()
		{
			if ( _root != null )
			{
				_root = null;
				_nNextId = 1;
				_elements.Clear();
			}

			_root = new Root(this);
			_root.setInternalId(_nNextId++);
			
			_elements.Add(_root.getInternalId(), _root);

			_XmlDeclaration = "<?xml version=\"1.0\"?>";
			_XmlDocType = "<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">";

			_root.SetAttributeValue(Attribute._SvgAttribute.attrSvg_XmlNs, "http://www.w3.org/2000/svg");
			_root.SetAttributeValue(Attribute._SvgAttribute.attrSvg_Version, "1.1");

			return _root;
		}

		/// <summary>
		/// It creates a SVG document reading from a file.
		/// If a current document exists, it is destroyed.
		/// </summary>
		/// <param name="sFilename">The complete path of a valid SVG file.</param>
		/// <returns>
		/// true if the file is loaded successfully and it is a valid SVG document, false if the file cannot be open or if it is not
		/// a valid SVG document.
		/// </returns>
		public bool Load(string sFilename)
		{
			Debug.WriteLine("SvgDoc", "LoadFromFile");
			Debug.WriteLine("sFilename", sFilename);

			if ( _root != null )
			{
				_root = null;
				_nNextId = 1;
				_elements.Clear();
			}

			bool bResult = true;

			try
			{
				XmlTextReader reader;
				reader = new XmlTextReader(sFilename);
				reader.WhitespaceHandling = WhitespaceHandling.None;
				reader.Normalization = false;
				reader.XmlResolver = null;
				reader.Namespaces = false;

				string tmp;
				Element eleParent = null;	

				try 
				{
					// parse the file and display each of the nodes.
					while ( reader.Read() && bResult ) 
					{
						switch (reader.NodeType) 
						{
							case XmlNodeType.Attribute:
								{
									tmp = reader.Name;
									tmp = reader.Value;
									break;
								}
							case XmlNodeType.Element:
								{
									Element ele = AddElement(eleParent, reader.Name);

									if (ele == null)
									{
										// Should not have any logging in a library
										// this should be rased as an exception

										Debug.WriteLine("Svg element cannot be added. Name: " + reader.Name);

										bResult = false;
									}
									else
									{
										eleParent = ele;

										if (reader.IsEmptyElement)
										{
											if (eleParent != null)
											{
												eleParent = eleParent.getParent();
											}
										}

										bool bLoop = reader.MoveToFirstAttribute();
										while (bLoop)
										{
											ele.SetAttributeValue(reader.Name, reader.Value);

											bLoop = reader.MoveToNextAttribute();
										}
									}
									break;
								}
							case XmlNodeType.Text:
								{
									if (eleParent != null)
									{
										eleParent.setElementValue(reader.Value);
									}
									break;
								}
							case XmlNodeType.CDATA:
								{
									// Should not have any logging in a library
									// this should be rased as an exception

									Debug.WriteLine("Unexpected item: " + reader.Value);
									break;
								}
							case XmlNodeType.ProcessingInstruction:
								{
									Debug.WriteLine("Unexpected item: " + reader.Value);
									break;
								}
							case XmlNodeType.Comment:

								Debug.WriteLine("Unexpected item: " + reader.Value);
								break;

							case XmlNodeType.XmlDeclaration:
								_XmlDeclaration = "<?xml " + reader.Value + "?>";
								break;

							case XmlNodeType.Document:
								Debug.WriteLine("Unexpected item: " + reader.Value);
								break;

							case XmlNodeType.DocumentType:
							{
								string sDTD1;
								string sDTD2;

								sDTD1 = reader.GetAttribute("PUBLIC");
								sDTD2 = reader.GetAttribute("SYSTEM");

								_XmlDocType = "<!DOCTYPE svg PUBLIC \"" + sDTD1 + "\" \"" + sDTD2 + "\">";
							}
								break;

							case XmlNodeType.EntityReference:
								Debug.WriteLine("Unexpected item: " + reader.Value);
								break;

							case XmlNodeType.EndElement:
								if ( eleParent != null )
								{
									eleParent = eleParent.getParent();
								}
								break;
						}
					}
				}
				catch(XmlException xmle)
				{
					Debug.WriteLine(xmle);
					Debug.WriteLine("Line Number", xmle.LineNumber.ToString());
					Debug.WriteLine("Line Position", xmle.LinePosition.ToString());

					bResult = false;
				}
				catch(Exception e)
				{
					Debug.WriteLine(e);
					bResult = false;
				}
				finally
				{
					reader.Close();
				}
			}
			catch
			{
				Debug.WriteLine("");
				bResult = false;
			}

			Debug.WriteLine(bResult);

			return bResult;
		}

		/// <summary>
		/// It saves the current SVG document to a file.
		/// </summary>
		/// <param name="sFilename">The complete path of the file.</param>
		/// <returns>
		/// true if the file is saved successfully, false otherwise
		/// </returns>
		public bool Save(string sFilename)
		{
			Debug.WriteLine("SvgDoc", "SaveToFile");
			Debug.WriteLine("sFilename", sFilename);

			bool bResult = false;
			StreamWriter sw = null;
			try
			{
				sw = File.CreateText(sFilename);
				bResult = true;
			}
			catch (UnauthorizedAccessException uae)
			{
				Debug.WriteLine(uae);
			}
			catch (DirectoryNotFoundException dnfe)
			{
				Debug.WriteLine(dnfe);
			}
			catch (ArgumentException ae)
			{
				Debug.WriteLine(ae);
			}
			catch
			{
				Debug.WriteLine("");
			}

			if ( !bResult )
			{
				Debug.WriteLine(false);
				
				return false;
			}

			try
			{
				sw.Write(GetXML());
				sw.Close();
			}
			catch
			{
				Debug.WriteLine("");
				Debug.WriteLine(false);

				return false;
			}

			Debug.WriteLine(true);

			return true;
		}

		/// <summary>
		/// It returns the XML string of the entire SVG document.
		/// </summary>
		/// <returns>
		/// The SVG document. An empty string if the document is empty.
		/// </returns>
		public string GetXML()
		{
			if ( _root == null )
			{
				return "";
			}

			string sXML;

			sXML = _XmlDeclaration + "\r\n";
			sXML += _XmlDocType;
			sXML += "\r\n";
			
			sXML += _root.GetXML();

			return sXML;
		}

		/// <summary>
		/// It returns the SvgElement with the given internal (numeric) identifier.
		/// </summary>
		/// <param name="nInternalId">Internal unique identifier of the element.</param>
		/// <returns>
		/// The SvgElement with the given internal Id. Null if no element can be found.
		/// </returns>
		public Element GetElement(int nInternalId)
		{
			if (!_elements.ContainsKey(nInternalId))
			{
				return null;
			}

			return (Element) _elements[nInternalId];
		}

		/// <summary>
		/// It returns the root element of the SVG document.
		/// </summary>
		/// <returns>
		/// Root element.
		/// </returns>
		public Root GetRoot()
		{
			return _root;
		}

		/// <summary>
		/// It returns the SvgElement with the given XML Id.
		/// </summary>
		/// <param name="sId">XML identifier of the element.</param>
		/// <returns>
		/// The SvgElement with the given XML Id. Null if no element can be found.
		/// </returns>
		public Element GetElement(string sId)
		{
			Element eleToReturn = null;

			IDictionaryEnumerator e = _elements.GetEnumerator();
			
			bool bLoop = e.MoveNext();
			while ( bLoop )
			{
				string sValue = "";

				Element ele = (Element) e.Value;
				sValue = ele.GetAttributeStringValue(Attribute._SvgAttribute.attrCore_Id);
				if ( sValue == sId )
				{
					eleToReturn = ele;
					bLoop = false;
				}

				bLoop = e.MoveNext();
			}

			return eleToReturn;
		}

		/// <summary>
		/// It adds the new element eleToAdd as the last children of the given parent element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <param name="eleToAdd">Element to be added.</param>
		public void AddElement(Element parent, Element eleToAdd)
		{
			Debug.WriteLine("SvgDoc", "AddElement");

			if ( eleToAdd == null || _root == null )
			{
				Debug.WriteLine(false);
				return;
			}

			Element parentToAdd = _root;
			if ( parent != null )
			{
				parentToAdd = parent;
			}

			eleToAdd.setInternalId(_nNextId++);
			_elements.Add(eleToAdd.getInternalId(), eleToAdd);

			eleToAdd.setParent(parentToAdd);
			if (parentToAdd.getChild() == null )
			{
				// the element is the first child
				parentToAdd.setChild(eleToAdd);
			}
			else
			{
				// add the element as the last sibling
				Element last = GetLastSibling(parentToAdd.getChild());

				if ( last != null )
				{
					last.setNext(eleToAdd);
					eleToAdd.setPrevious(last);
				}
			}

			Debug.WriteLine(eleToAdd.ElementInfo());
			Debug.WriteLine(true);
		}

		/// <summary>
		/// It creates a new element according to the element name provided
		/// and it adds the new element as the last children of the given parent element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <param name="sName">SVG element name.</param>
		/// <returns>The new created element.</returns>
		public Element AddElement(Element parent, string sName)
		{
			Element eleToReturn = null;

			if ( sName == "svg" )
			{
				_root = new Root(this);
				_root.setInternalId(_nNextId++);
			
				_elements.Add(_root.getInternalId(), _root);
				eleToReturn = _root;
			}
			else if ( sName == "desc" )
			{
				eleToReturn = AddDesc(parent);
			}
			else if ( sName == "text" )
			{
				eleToReturn = AddText(parent);
			}
			else if ( sName == "g" )
			{
				eleToReturn = AddGroup(parent);
			}
			else if ( sName == "rect" )
			{
				eleToReturn = AddRect(parent);
			}
			else if ( sName == "circle" )
			{
				eleToReturn = AddCircle(parent);
			}
			else if ( sName == "ellipse" )
			{
				eleToReturn = AddEllipse(parent);
			}
			else if ( sName == "line" )
			{
				eleToReturn = AddLine(parent);
			}
			else if ( sName == "path" )
			{
				eleToReturn = AddPath(parent);
			}
			else if ( sName == "polygon" )
			{
				eleToReturn = AddPolygon(parent);
			}
			else if ( sName == "image" )
			{
				eleToReturn = AddImage(parent);
			}
			else
			{
				if ( parent != null )
				{
					eleToReturn = AddUnsupported(parent, sName);
				}
			}

			return eleToReturn;
		}

		/// <summary>
		/// It creates a new element copying all attributes from eleToClone; the new
		/// element is inserted under the parent element provided. 
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <param name="eleToClone">Element to be cloned</param>
		/// <returns></returns>
		public Element CloneElement(Element parent, Element eleToClone)
		{
			// calculate unique id
			string sOldId = eleToClone.GetAttributeStringValue(Attribute._SvgAttribute.attrCore_Id);
			string sNewId = sOldId;
			
			if ( sOldId != "" )
			{
				int i = 1;
				
				// check if it is unique
				while ( GetElement(sNewId) != null )
				{
					sNewId = sOldId + "_" + i.ToString();
					i++;
				} 
			}

			// clone operation
			Element eleNew = AddElement(parent, eleToClone.getElementName());
			eleNew.CloneAttributeList(eleToClone);

			if ( sNewId != "" )
			{
				eleNew.SetAttributeValue(Attribute._SvgAttribute.attrCore_Id, sNewId);
			}

			if ( eleToClone.getChild() != null )
			{
				eleNew.setChild(CloneElement(eleNew, eleToClone.getChild()));

				if ( eleToClone.getChild().getNext() != null )
				{
					eleNew.getChild().setNext(CloneElement(eleNew, eleToClone.getChild().getNext()));
				}
			}

			return eleNew;
		}

		/// <summary>
		/// It creates a new SVG Unsupported element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <param name="sName">Name</param>
		/// <returns>
		/// New element created.
		/// </returns>
		/// <remarks>
		/// The unsupported element is used when during the parsing of a file an unknown
		/// element tag is found.
		/// </remarks>
		public Unsupported AddUnsupported(Element parent, string sName)
		{
			Unsupported uns = new Unsupported(this, sName);
			
			AddElement(parent, uns);
			
			return uns;
		}

		/// <summary>
		/// It creates a new SVG Desc element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Desc AddDesc(Element parent)
		{
			Desc desc = new Desc(this);
			
			AddElement(parent, desc);
			
			return desc;
		}

		/// <summary>
		/// It creates a new SVG Group element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Group AddGroup(Element parent)
		{
			Group grp = new Group(this);
			
			AddElement(parent, grp);
			
			return grp;
		}
		
		/// <summary>
		/// It creates a new SVG Text element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Text AddText(Element parent)
		{
			Text txt = new Text(this);
			
			AddElement(parent, txt);
			
			return txt;
		}

		/// <summary>
		/// It creates a new SVG Rect element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Rect AddRect(Element parent)
		{
			Rect rect = new Rect(this);
			
			AddElement(parent, rect);
			
			return rect;
		}

		/// <summary>
		/// It creates a new SVG Circle element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Circle AddCircle(Element parent)
		{
			Circle circle = new Circle(this);
			
			AddElement(parent, circle);
			
			return circle;
		}

		/// <summary>
		/// It creates a new SVG Ellipse element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Ellipse AddEllipse(Element parent)
		{
			Ellipse ellipse = new Ellipse(this);
			
			AddElement(parent, ellipse);
			
			return ellipse;
		}

		/// <summary>
		/// It creates a new SVG Line element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Line AddLine(Element parent)
		{
			Line line = new Line(this);
			
			AddElement(parent, line);
			
			return line;
		}

		/// <summary>
		/// It creates a new SVG Path element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Path AddPath(Element parent)
		{
			Path path = new Path(this);
			
			AddElement(parent, path);
			
			return path;
		}

		/// <summary>
		/// It creates a new SVG Polygon element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Polygon AddPolygon(Element parent)
		{
			Polygon poly = new Polygon(this);
			
			AddElement(parent, poly);
			
			return poly;
		}

		/// <summary>
		/// It creates a new SVG Image element.
		/// </summary>
		/// <param name="parent">Parent element. If null the element is added under the root.</param>
		/// <returns>New element created.</returns>
		public Image AddImage(Element parent)
		{
			Image img = new Image(this);
			
			AddElement(parent, img);
			
			return img;
		}

		/// <summary>
		/// It deletes an element from the document.
		/// </summary>
		/// <param name="ele">Element to be deleted.</param>
		/// <returns>
		/// true if the element has been successfully deleted; false otherwise.
		/// </returns>
		public bool DeleteElement(Element ele)
		{
			return DeleteElement(ele, true);
		}

		/// <summary>
		/// It deletes an element from the document.
		/// </summary>
		/// <param name="nInternalId">Internal Id of the element to be deleted.</param>
		/// <returns>
		/// true if the element has been successfully deleted; false otherwise.
		/// </returns>
		public bool DeleteElement(int nInternalId)
		{
			return DeleteElement(GetElement(nInternalId), true);
		}

		/// <summary>
		/// It deletes an element from the document.
		/// </summary>
		/// <param name="sId">XML Id of the element to be deleted.</param>
		/// <returns>
		/// true if the element has been successfully deleted; false otherwise.
		/// </returns>
		public bool DeleteElement(string sId)
		{
			return DeleteElement(GetElement(sId), true);
		}

		/// <summary>
		/// It moves the element before its current previous sibling.
		/// </summary>
		/// <param name="ele">Element to be moved.</param>
		/// <returns>
		/// true if the operation succeeded.
		/// </returns>
		public bool ElementPositionUp(Element ele)
		{
			Debug.WriteLine("SvgDoc", "ElementPositionUp");
			Debug.WriteLine("Element to move " + ele.ElementInfo());

			Element parent = ele.getParent();
			if ( parent == null )
			{
				Debug.WriteLine("Root node cannot be moved");
				Debug.WriteLine(false);

				return false;
			}

			if ( IsFirstChild(ele) )
			{
				Debug.WriteLine("Element is already at the first position");
				Debug.WriteLine(false);

				return false;
			}

			Element nxt = ele.getNext();
			Element prv = ele.getPrevious();
			Element prv2 = null;

			ele.setNext(null);
			ele.setPrevious(null);

			// fix Next
			if ( nxt != null )
			{
				nxt.setPrevious(prv);
			}

			// fix Previous
			if ( prv != null )
			{
				prv.setNext(nxt);
				prv2 = prv.getPrevious();
				prv.setPrevious(ele);

				// check if the Previous is the first child
				if ( IsFirstChild(prv) )
				{
					// if yes the moved element has to became the new first child
					if ( prv.getParent() != null )
					{
						prv.getParent().setChild(ele);
					}
				}
			}

			// fix Previous/Previous
			if ( prv2 != null )
			{
				prv2.setNext(ele);
			}

			// fix Element
			ele.setNext(prv);
			ele.setPrevious(prv2);

			Debug.WriteLine("Element moved " + ele.ElementInfo());
			Debug.WriteLine(true);

			return true;
		}

		/// <summary>
		/// It moves the element one level up in the tree hierarchy.
		/// </summary>
		/// <param name="ele">Element to be moved.</param>
		/// <returns>
		/// true if the operation succeeded.
		/// </returns>
		public bool ElementLevelUp(Element ele)
		{
			Debug.WriteLine("SvgDoc", "ElementLevelUp");
			Debug.WriteLine("Element to move " + ele.ElementInfo());

			Element parent = ele.getParent();
			if ( parent == null )
			{
				Debug.WriteLine("Root node cannot be moved");
				Debug.WriteLine(false);

				return false;
			}

			if ( parent.getParent() == null )
			{
				Debug.WriteLine("An element cannot be moved up to the root");
				Debug.WriteLine(false);

				return false;
			}
				
			Element nxt = ele.getNext();
				
			// the first child of the parent became the next
			parent.setChild(nxt);

			if ( nxt != null )
			{
				nxt.setPrevious(null);
			}

			// get the last sibling of the parent
			Element last = GetLastSibling(parent);
			if ( last != null )
			{
				last.setNext(ele);
			}

			ele.setParent(parent.getParent());
			ele.setPrevious(last);
			ele.setNext(null);

			return true;
		}

		/// <summary>
		/// It moves the element after its current next sibling.
		/// </summary>
		/// <param name="ele">Element to be moved.</param>
		/// <returns>
		/// true if the operation succeeded.
		/// </returns>
		public bool ElementPositionDown(Element ele)
		{
			Debug.WriteLine("SvgDoc", "ElementPositionDown");
			Debug.WriteLine("Element to move " + ele.ElementInfo());

			Element parent = ele.getParent();
			if ( parent == null )
			{
				Debug.WriteLine("Root node cannot be moved");
				Debug.WriteLine(false);

				return false;
			}

			if ( IsLastSibling(ele) )
			{
				Debug.WriteLine("Element is already at the last sibling position");
				Debug.WriteLine(false);

				return false;
			}
			
			Element nxt = ele.getNext();
			Element nxt2 = null;
			Element prv = ele.getPrevious();
			
			// fix Next
			if ( nxt != null )
			{
				nxt.setPrevious(ele.getPrevious());
				nxt2 = nxt.getNext();
				nxt.setNext(ele);
			}

			// fix Previous
			if ( prv != null )
			{
				prv.setNext(nxt);
			}

			// fix Element
			if ( IsFirstChild(ele) )
			{
				parent.setChild(nxt);
			}

			ele.setPrevious(nxt);
			ele.setNext(nxt2);

			if ( nxt2 != null )
			{
				nxt2.setPrevious(ele);
			}
				
			Debug.WriteLine("Element moved " + ele.ElementInfo());
			Debug.WriteLine(true);

			return true;
		}

		//public IEnumerator GetEnumerator()
		//{
		//	return ((IEnumerable)_elements).GetEnumerator();
		//}

#endregion
		#region Private

        private bool DeleteElement(Element ele, bool bDeleteFromParent)
		{
			Debug.WriteLine("SvgDoc", "DeleteElement");

			if ( ele == null )
			{
				Debug.WriteLine(false);

				return false;
			}

			Element parent = ele.getParent();
			if ( parent == null )
			{
				// root node cannot be delete!
				Debug.WriteLine("root node cannot be delete!");
				Debug.WriteLine(false);

				return false;
			}

			// set the Next reference of the previous
			if ( ele.getPrevious() != null )
			{
				ele.getPrevious().setNext(ele.getNext());
			}

			// set the Previous reference of the next
			if ( ele.getNext() != null )
			{
				ele.getNext().setPrevious(ele.getPrevious());
			}

			// check if the element is the first child
			// the bDeleteFromParent flag is used to avoid deleting
			// all parent-child relationship. This is used in the Cut
			// operation where the subtree can be pasted 
			if ( bDeleteFromParent )
			{
				if ( IsFirstChild(ele) )
				{
					// set the Child reference of the parent to the next
					ele.getParent().setChild(ele.getNext());
				}
			}

			// delete its children
			Element child = ele.getChild();

			while ( child != null )
			{
				DeleteElement(child, false);
				child = child.getNext();
			}

			// delete the element from the colloection
			_elements.Remove(ele.getInternalId());

			Debug.WriteLine(ele.ElementInfo());
			Debug.WriteLine(true);

			return true;
		}

		// returns true if the given element is the first child 
		private bool IsFirstChild(Element ele)
		{
			if ( ele.getParent() == null )
			{
				return false;
			}

			if ( ele.getParent().getChild() == null )
			{
				return false;
			}

			return (ele.getInternalId() == ele.getParent().getChild().getInternalId());
		}

		private bool IsLastSibling(Element ele)
		{
			Element last = GetLastSibling(ele);

			if ( last == null )
			{
				return false;
			}

			return (ele.getInternalId() == last.getInternalId());
		}

		private Element GetLastSibling(Element ele)
		{
			if ( ele == null )
			{
				return null;
			}

			Element last = ele;
			while (last.getNext() != null)
			{
				last = last.getNext();
			}
			
			return last;
		}

		public IDictionaryEnumerator GetEnumerator()
		{
			return (_elements.GetEnumerator());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (_elements.GetEnumerator());
		}


		#endregion
	}
}

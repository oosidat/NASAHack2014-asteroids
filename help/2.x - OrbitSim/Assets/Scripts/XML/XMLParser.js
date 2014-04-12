#pragma strict
#pragma implicit
#pragma downcast

// -----
//
// XMLParser.js
//
// -----

class XMLParser {
	
	// -----
	// Creator Function
	// -----
	
	function XMLParser () { }
	
	
	// -----
	// Public Functions
	// -----
	
	function ParseString (aXMLString : String) : Hashtable[] {
		
		// Initialize the system data array
		var tSystemData : Array = new Array();
		
		// Look for and remove the XML header tag
		aXMLString = RemoveXMLHeaderTag(aXMLString);
		
		// Pull the configuration data
		var tConfigData : Hashtable = GetElementAttributes(aXMLString, "system");
		tSystemData.Push(tConfigData);
		
		// Look for and remove the wrapping system tag
		aXMLString = RemoveSystemTag(aXMLString);
		
		// Process all body tags
		var tNextBody : Hashtable = GetElementAttributes(aXMLString, "body"); //GetNextBody(aXMLString);
		while (tNextBody != null) {
			
			// Add the body to the system data array
			tSystemData.Push(tNextBody);
			
			// Strip the current body tag
			aXMLString = RemoveBodyTag(aXMLString);
			
			// Look for the next body tag
			tNextBody = GetElementAttributes(aXMLString, "body");
			
		}
		
		// Return the system data array as a builtin array
		return tSystemData.ToBuiltin(Hashtable);
		
	}
	
	
	// -----
	// Private Functions
	// -----
	
	private function GetElementAttributes (aXMLString : String, aElementName : String) : Hashtable {
		
		// Look for the next element with the provided name
		var tElementStart : int = aXMLString.IndexOf("<" + aElementName + " ");
		var tElementEnd : int = aXMLString.IndexOf(">");
		if ((tElementStart > -1) && (tElementEnd > tElementStart)) {
			
			// Initialize the attributes hashtable
			var tAttributes : Hashtable = new Hashtable();
			
			// Pull the attribute string of the element tag
			var tStripStart : int = tElementStart + 2 + aElementName.length;
			var tStripLength : int = tElementEnd - tStripStart;
			var tAttributeString : String = aXMLString.Substring(tStripStart, tStripLength);
			
			// Strip any leading/trailing space characters
			tAttributeString = tAttributeString.Trim();
			
			// Split into name/value chunks, and walk each chunk
			var tChunks : String[] = tAttributeString.Split(" "[0]);
			for (var i : int = 0; i < tChunks.length; i++) {
				
				// Split the chunk into separate name and value strings
				var tChunkString : String = tChunks[i].ToString();
				if (tChunkString != "/") {
					var tSubStrings : Array = tChunkString.Split("="[0]);
					var tNameString : String = tSubStrings[0].ToString();
					var tValueString : String = tSubStrings[1].ToString();
				
					// Store the name/value (stripping the leading/trailing "s) pair information in the attributes hashtable
					tAttributes[tNameString] = tValueString.Substring(1, (tValueString.length - 2));
				}
				
			}
			
			// Return the attributes hashtable
			return tAttributes;
			
		} else {
			
			// Return null
			return null;
			
		}
		
	}
	
	private function RemoveBodyTag (aXMLString : String) : String {
		
		// Look for a body tag
		var tBodyStart : int = aXMLString.IndexOf("<body ");
		var tBodyEnd : int = aXMLString.IndexOf("/>");
		if ((tBodyStart > -1) && (tBodyEnd > tBodyStart)) {
			
			// Strip the body tag from the string
			var tStripStart : int = tBodyEnd + 2;
			aXMLString = aXMLString.Substring(tStripStart);
			
		}
		
		// Return the trimmed XML string
		return aXMLString;
		
	}
	
	private function RemoveSystemTag (aXMLString : String) : String {
		
		// Look for the system tag
		var tSystemStart : int = aXMLString.IndexOf(">");
		var tSystemEnd : int = aXMLString.IndexOf("</system>");
		if ((tSystemStart > -1) && (tSystemEnd > tSystemStart)) {
			
			// Strip the system start/end elements
			var tStripStart : int = tSystemStart + 1;
			var tStripLength : int = tSystemEnd - tStripStart;
			aXMLString = aXMLString.Substring(tStripStart, tStripLength);
			
		}
		
		// Return the trimmed XML string
		return aXMLString;
		
	}
	
	private function RemoveXMLHeaderTag (aXMLString : String) : String {
		
		// Look for the XML header tag
		var tHeaderStart : int = aXMLString.IndexOf("<?xml");
		var tHeaderEnd : int = aXMLString.IndexOf("?>");
		if ((tHeaderStart > -1) && (tHeaderEnd > tHeaderStart)) {
			
			// Strip the XML header tag from the string
			var tStripStart : int = tHeaderEnd + 2;
			aXMLString = aXMLString.Substring(tStripStart);
			
		}
		
		// Return the trimmed XML string
		return aXMLString;
		
	}
		
}

// TH
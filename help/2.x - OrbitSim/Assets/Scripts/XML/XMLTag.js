#pragma strict
#pragma implicit
#pragma downcast

////////////////////////////////////////////////////////////////////////////////
//
//
//  XMLTag.js
//
//  This script defines a simple class in which XML tag data can be stored. It
//  exposes three public properties:
//
//  - Name: string found within starting tag element.
//  - Value: string found between the starting and ending tag element.
//  - Children: if the Value itself defineds tag elements this is an array of
//    those child tag objects.
//
////////////////////////////////////////////////////////////////////////////////

class XMLTag {
	
   var Children : Array = new Array();
   var Name : String;
   var Value : Object;
   
   function XMLTag (aName : String, aValue : Object) {
      Name = aName;
      Value = aValue;
   }

} 

//  TH
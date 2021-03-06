// mWaves2D for Unity - mgear - http://unitycoder.com/blog

// ORIGINAL CODE:
/* Processing Water Simulation
* adapted by: Rodrigo Amaya
*
* Based on "Java Water Simulation", by: Neil Wallis
* For more information visit the original article here:
* http://neilwallis.com/projects/java/water/index.php
*
* How does it work? "2D Water"
* http://freespace.virgin.net/hugo.elias/graphics/x_water.htm
*
*/

public var sourceimage:Texture2D;
private var targettexture:Texture2D;
 
private var size:int;
private var hwidth:int;
private var hheight:int;
private var riprad:int;

private var ripplemap:int[];
private var data:int;

private var ripple : Color[];
private var texture : Color[];
private var pixels:int[];

private var oldind:int;
private var newind:int;
private var mapind:int;
 
private var i:int;
private var a:int;
private var b:int;

private var width:int;
private var height:int;

public var disturbsize:int=128;
 
// init 
function Awake ()
{

  width =sourceimage.width;
  height = sourceimage.height;
  
	targettexture = new Texture2D(width,height);
	GetComponent.<Renderer>().material.mainTexture = targettexture;
	GetComponent.<Renderer>().material.SetTexture("_ParallaxMap", targettexture);
   
  hwidth = width>>5;
  hheight = height>>5;
  riprad=8; //test with 3
   
  size = width * (height+2) * 2;
   
  ripplemap = new int[size];
  ripple = new Color[width*height];
  texture = new Color[width*height];
  pixels = new int[width*height];
  
  oldind = width;
  newind = width * (height+3);

	var counter:int = 0;
	for (var y:int=0;y<height;y++) 
	{
		for (var x:int=0;x<width;x++) 
		{
			texture[counter] = sourceimage.GetPixel (x,y);
			counter++;
		}
	}
	
	//  smooth();

}
 
function Update()
{
//  image(img, 0, 0); //Displays images to the screen
//  loadPixels(); // Loads the pixel data for the display window into the pixels[] array
//  texture = pixels;
   
  newframe();
  var px:int = 0;
  var py:int = 0;
  
  for (var i:int = 0; i < pixels.length; i++) 
  {
//	todo: use Texture2D.SetPixels instead..
	targettexture.SetPixel (px, py, ripple[i]);
	px++;
	if (px>=width) {px=0; py++;}
  }
   
  //updatePixels(); //Updates the display window with the data in the pixels[] array
  	targettexture.Apply();
	
	// left mouse button is pressed down
	/*if(Input.GetMouseButton(0))
	{
		// raycast to mousecursor location
		var hit : RaycastHit;
		if (!Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), hit))	return;
		
		// get real coordinates
		var pixelUV = hit.textureCoord;
		pixelUV.x *= width;
		pixelUV.y *= height;
		
		// then apply waves on that position
		disturb(pixelUV.x,pixelUV.y);
	}*/

}

	// Destroy everything that enters the trigger
	function OnTriggerEnter (other : Collider) {
	    
	    disturb(width/2,height/2);
	    /*var hit = new RaycastHit();
	    var ray = new Ray( other.contacts[ 0 ].point -other.contacts[ 0 ].normal, other.contacts[ 0 ].normal );

	    // get real coordinates
	    var pixelUV = hit.textureCoord;
	    pixelUV.x *= width;
	    pixelUV.y *= height;
		
	    // then apply waves on that position
	    disturb(pixelUV.x,pixelUV.y);*/
	}

	
 
// ripples 
function disturb(dx:int,dy:int)
{
  for (var j:int=dy-riprad;j<dy+riprad;j++) {
    for (var k:int=dx-riprad;k<dx+riprad;k++) {
      if (j>=0 && j<height && k>=0 && k<width) {
        ripplemap[oldind+(j*width)+k] += disturbsize;
      }
    }
  }
}


// processing
function newframe() 
{
  //Toggle maps each frame
  i=oldind;
  oldind=newind;
  newind=i;
 
  i=0;
  mapind=oldind;
  for (var y:int=0;y<height;y++) {
    for (var x:int=0;x<width;x++) {
      data = (ripplemap[mapind-width]+ripplemap[mapind+width]+ripplemap[mapind-1]+ripplemap[mapind+1])>>1;
      data -= ripplemap[newind+i];
      data -= data >> 5;
      ripplemap[newind+i]=data;
 
      //where data=0 then still, where data>0 then wave
      data = (1024-data);
 
      //offsets
      a=((x-hwidth)*data/1024)+hwidth;
      b=((y-hheight)*data/1024)+hheight;
 
      //bounds check
      if (a>=width) a=width-1;
      if (a<0) a=0;
      if (b>=height) b=height-1;
      if (b<0) b=0;
 
      ripple[i]=texture[a+(b*width)];
      mapind++;
      i++;
    }
  }
}

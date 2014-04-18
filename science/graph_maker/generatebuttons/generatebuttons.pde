color text_color1 = color(138,119,237);
color text_color2 = color(194,120,121);
float button_width = 120;
float button_height = 100;
float curve_rad = 10;
float spacing = 110;
float edge = 50;
void setup(){
  background(0);
  //size(600,600);
  size((int)(button_width+2*edge),(int)(button_height/1.5+2*edge));
}

void draw(){
  background(0);
  textSize(32);
  fill(text_color1);
  text("Filters",10,30);
  stroke(text_color1);
  strokeWeight(3);
  fill(text_color1,100);
  rect(width/2-button_width/2,edge,button_width,button_height,curve_rad,curve_rad);
  rect(width/2-button_width/2,edge+spacing,button_width,button_height,curve_rad,curve_rad);
  rect(width/2-button_width/2,edge+2*spacing,button_width,button_height,curve_rad,curve_rad);
  save("filterbutton0.png");

  background(0);
  textSize(32);
  fill(text_color1);
  text("Filters",10,30);
  stroke(text_color1);
  strokeWeight(3);
  fill(text_color1,200);
  rect(width/2-button_width/2,edge,button_width,button_height,curve_rad,curve_rad);
  fill(text_color1,100);
  rect(width/2-button_width/2,edge+spacing,button_width,button_height,curve_rad,curve_rad);
  rect(width/2-button_width/2,edge+2*spacing,button_width,button_height,curve_rad,curve_rad);
  save("filterbutton1.png");
  
  background(0);
  textSize(32);
  fill(text_color1);
  text("Filters",10,30);
  stroke(text_color1);
  strokeWeight(3);
  fill(text_color1,100);
  rect(width/2-button_width/2,edge,button_width,button_height,curve_rad,curve_rad);
  fill(text_color1,200);
   rect(width/2-button_width/2,edge+spacing,button_width,button_height,curve_rad,curve_rad);
   fill(text_color1,100);
  rect(width/2-button_width/2,edge+2*spacing,button_width,button_height,curve_rad,curve_rad);
  save("filterbutton2.png");
    background(0);
  textSize(32);
  fill(text_color1);
  text("Filters",10,30);
  stroke(text_color1);
  strokeWeight(3);
  fill(text_color1,100);
  rect(width/2-button_width/2,edge,button_width,button_height,curve_rad,curve_rad);
  rect(width/2-button_width/2,edge+spacing,button_width,button_height,curve_rad,curve_rad);
   fill(text_color1,200);
  rect(width/2-button_width/2,edge+2*spacing,button_width,button_height,curve_rad,curve_rad);
  save("filterbutton3.png");
  background(0);
  
  rect(width/2-button_width/2,edge,button_width,button_height/1.5,curve_rad,curve_rad);
  smooth();
  save("smallminenotext.png");
  smooth();
  textSize(52);
  fill(0);
  stroke(0);
  text("Mine",width/2-55,edge+52);
  save("smallminetext.png");
  //text("Ultra-Violet"-button_width/2,width/2,width/2);
  stop();
}

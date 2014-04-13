color text_color1 = color(138,119,237);
float graph_height = 500;
float graph_width = 700;
float graph_margin = 20;
float line_width = 3;
void setup()
{
  size(600,600);
  background(0,0,0,0);
}

void draw(){

  background(0,0,0,0);
  textSize(32);
  fill(text_color1);
  text("Radar",10,30);
  line(width/2,graph_margin,width/2,height-graph_margin);
  line(graph_margin,width/2,height-graph_margin,width/2);
  fill(0,0,0,0);
  stroke(text_color1);
  strokeWeight(line_width);
  float circle_margin = graph_margin*2;
  ellipse(width/2,width/2,circle_margin,circle_margin);
  ellipse(width/2,width/2,2*circle_margin,2*circle_margin);
  ellipse(width/2,width/2,3*circle_margin,3*circle_margin);
  ellipse(width/2,width/2,4*circle_margin,4*circle_margin);
  ellipse(width/2,width/2,width-2*graph_margin,width-2*graph_margin);
  save("radar.png");
}

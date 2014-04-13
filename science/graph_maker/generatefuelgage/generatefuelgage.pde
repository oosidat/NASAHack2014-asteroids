color text_color1 = color(138,119,237);
float margin = 20;
float roundedness = 10;
float num_bars = 10;
float spacing = 8;
int start_bar = 0;
void setup(){
  size(300,600);
  background(0);
}
void draw(){
  background(0);
  fill(0);
  strokeWeight(3);
  stroke(text_color1);
  rect(width/2-width/4-spacing/4,margin,width/2+spacing/2,height-2*margin,roundedness,roundedness);
  float bar_height = (height-2*margin)/num_bars;
  for(int i=start_bar;i<int(num_bars);i++)
  {
    fill(text_color1);
    rect(width/2-width/4+spacing/2,margin+bar_height*i+spacing/2,width/2-spacing,bar_height-spacing,roundedness,roundedness);
  }
  String filename = "fuelgage"+nf(start_bar,2)+".png";
  save(filename);
  start_bar++;
  if(start_bar>10)
  {
    stop();
  }
}

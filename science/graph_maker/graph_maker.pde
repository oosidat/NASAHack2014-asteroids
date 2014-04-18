color background_color = color(0,0,0);
color text_color1 = color(138,119,237);
color text_color2 = color(194,120,121);
String lines[]; 
String cats[]; 
float vals[][];
int cat_i;
float graph_height = 370;//500;
int text_height = 142;
int text_height3 = 87;
int text_height4 = 54;
int text_height2 = 88;
float text_scaling_1 =0.85;
float graph_width = 700;
float graph_margin = 20;
float line_width = 3;
int alpha_grad[] = new int[int(graph_width)];
int red_grad[] = new int[int(graph_width)];
int blue_grad[] = new int[int(graph_width)];
int green_grad[] = new int[int(graph_width)];
color rainbow[] = new color[6];
String ingredients_lines[];
int ingredients[][];
String ingredients_names[] = {"feldspar","olivine","pyroxene","enstatite","carbon","silicates","water","metal"};
String pos_lines[];
float pos_bins[][];
void setup()
{
  rainbow[0] = color(145,57,250);
  rainbow[1] = color(57,70,250);
  rainbow[2] = color(29, 204,31);
  rainbow[3] = color(250,235,32);
  rainbow[4] = color(250,141,32);
  rainbow[5] = color(250,47,32);
  cat_i = 0;
  lines = loadStrings("vis_data.txt");
  cats = new String[lines.length];
  vals =  new float[lines.length][3];
  for (int i = 1 ; i < lines.length; i++) {
    String[] columns = split(lines[i],",");
    cats[i-1] = columns[0];
    vals[i-1][0] =  Float.parseFloat(columns[1]);
    vals[i-1][1] =  Float.parseFloat(columns[2]);
    vals[i-1][2] =  Float.parseFloat(columns[3]);
  }
  ingredients = new int[lines.length-1][3];
  ingredients_lines = loadStrings("compo_data.txt");
  for(int i = 1;i<ingredients_lines.length;i++)
  {
    String[] columns = split(ingredients_lines[i],",");
    ingredients[i-1][0] = Integer.parseInt(columns[1]);
     if(!columns[3].isEmpty())
    {
      ingredients[i-1][1] = Integer.parseInt(columns[2]);
    }
    else
    {
      ingredients[i-1][1] = -1;
    }
    if(!columns[3].isEmpty())
    {
      ingredients[i-1][2] = Integer.parseInt(columns[3]); 
    }
    else
    {
      ingredients[i-1][2] = -1;
    }
  }
  for(int i=0; i<graph_width;i++)
  {
    red_grad[i] = 255;
    blue_grad[i] = 255;
    green_grad[i] = 255;
    if(i<graph_width/3)
    {
      alpha_grad[i] = int(255*i*3/graph_width);
      red_grad[i] = int(red(rainbow[0]));
      blue_grad[i] = int(blue(rainbow[0]));
      green_grad[i] = int(green(rainbow[0]));
    }
    else if(i>2*graph_width/3)
    {
      alpha_grad[i] = int(255*(graph_width-i)*3/graph_width);
      red_grad[i] = int(red(rainbow[5]));
      blue_grad[i] = int(blue(rainbow[5]));
      green_grad[i] = int(green(rainbow[5]));
    }
    else
    {
      
      float tmp_pos = (i-graph_width/3.0)*3.0/graph_width;
      float current_box =  tmp_pos*5-floor(5*tmp_pos);
     // print(5*tmp_pos+ " "+current_box+"\n");
      alpha_grad[i] = 255;
      int current_index = int(5*tmp_pos);
      //print(current_index+ " "+current_box+"\n");
      red_grad[i] = int(red(rainbow[current_index])*(1-current_box))+int(red(rainbow[current_index+1])*(current_box));
      print(red_grad[i]+" "+int(red(rainbow[current_index]))+" "+int(red(rainbow[current_index+1]))+"\n");
      blue_grad[i] = int(blue(rainbow[current_index])*(1-current_box))+int(blue(rainbow[current_index+1])*(current_box));
      green_grad[i] = int(green(rainbow[current_index])*(1-current_box))+int(green(rainbow[current_index+1])*(current_box));
      
      //red_grad[i] = 138*()
    }
  }
  pos_bins = new float[ingredients_lines.length-1][7];
  pos_lines = loadStrings("pos_data.txt");
  for(int i = 1;i<ingredients_lines.length;i++)
  {
    String[] columns = split(pos_lines[i],",");
    pos_bins[i-1][0] = Float.parseFloat(columns[1]);
    pos_bins[i-1][1] = Float.parseFloat(columns[2]);
    pos_bins[i-1][2] = Float.parseFloat(columns[3]);
    pos_bins[i-1][3] = Float.parseFloat(columns[4]);
    pos_bins[i-1][4] = Float.parseFloat(columns[5]);
    pos_bins[i-1][5] = Float.parseFloat(columns[6]);
    pos_bins[i-1][6] = Float.parseFloat(columns[7]);
  }
  background(background_color);
  size(800,600);
  //size(600,600);
}
void draw(){
  background(background_color);
  textSize((int)(text_height3*text_scaling_1));
  fill(text_color1);
  String graph_title = cats[cat_i].toUpperCase()+"-type";
  
  
  String ingred_list =ingredients_names[ingredients[cat_i][0]];
  if(ingredients[cat_i][1]!=-1)
  {
    ingred_list = ingred_list+", "+ingredients_names[ingredients[cat_i][1]];
  }
  if(ingredients[cat_i][2]!=-1)
  {
    ingred_list = ingred_list+", "+ingredients_names[ingredients[cat_i][2]];
  }
  text(ingred_list,10,text_height3);
  textSize(text_height4);
  fill(text_color2);
  
  text("Spectrum",10,text_height);

  stroke(text_color1);
  
  float yprev = vals[cat_i][0]*graph_height;
  float graph_max = -pow(graph_width,2)/4.0;
  for(float x = 0;x<graph_width;x++)
  {
    float parab = (graph_height/graph_max)*(pow(x,2)-x*graph_width); 
    
    float ynext= yprev;
    if(x<graph_width/2.0)
    {
      
      ynext = vals[cat_i][0]*(graph_height);
      ynext = ynext+(vals[cat_i][1]-vals[cat_i][0])*parab;
      
    }
    else
    {
       ynext = vals[cat_i][2]*(graph_height);
       ynext = ynext +(vals[cat_i][1]-vals[cat_i][2])*parab;
       
    }
    //print(x+" "+yprev+" "+ynext+"\n");
    stroke(red_grad[(int)x],green_grad[(int)x],blue_grad[(int)x],alpha_grad[(int)x]);
    line(x+graph_margin,graph_height+text_height-yprev,x+graph_margin,graph_height+text_height);
    stroke(text_color1);
    line(x+graph_margin,graph_height+text_height-yprev,x+1+graph_margin,graph_height+text_height-ynext);
    
    yprev= ynext;
  }
    strokeWeight(line_width);
  line(20,text_height,20,text_height+graph_height);
  line(20,text_height+graph_height,20+graph_width,text_height+graph_height);
  textSize((int)(text_height2*text_scaling_1));
  fill(text_color2);
  text("UV",graph_margin,graph_height+text_height2+text_height-graph_margin);
  text("Vis",graph_margin+graph_width/2.0,graph_height+text_height2+text_height-graph_margin);
  text("IR",-graph_margin+graph_width,graph_height+text_height2+text_height-graph_margin);
  smooth();
  String filename = graph_title+".png";
  save(filename);
  
  
  
  background(background_color);
  textSize((int)(text_height3*text_scaling_1));
  fill(text_color1);
  text(graph_title,10,text_height3);
  text( "May contain: ",400,text_height3);
  textSize(text_height4);
  fill(text_color2);
  
  text("Distance",graph_margin,text_height);

  stroke(text_color1);
  
  strokeWeight(line_width);
  float bin_width = graph_width/7.0;
  for(int i = 0;i<7;i++)
  {
    stroke(text_color1);
    fill(text_color2);
    float bin_height = graph_height*(pos_bins[cat_i][i])/0.7;
    rect(i*bin_width+graph_margin,graph_height+text_height-bin_height,bin_width,bin_height);
  }
  line(20,text_height,20,text_height+graph_height);
  line(20,text_height+graph_height,20+graph_width,text_height+graph_height);
  textSize((int)(text_height2*text_scaling_1));
  fill(text_color2);
  text("2",graph_margin,graph_height+text_height+text_height2-graph_margin);
  text("2.75",-5.5*graph_margin+graph_width/2.0,graph_height+text_height+text_height2-graph_margin);
  text("3.5 AU",-7*graph_margin+graph_width,graph_height+text_height+text_height2-graph_margin);
  smooth();
  
  String filename2 = "pos_"+graph_title+".png";
  save(filename2);
  
  cat_i++;
  /*
  background(0);
  textSize(32);
  fill(text_color1);
  text("Radar",10,30);
  line(width/2,graph_margin,width/2,height-graph_margin);
  line(graph_margin,width/2,height-graph_margin,width/2);
  fill(0,0,0,0);
  float circle_margin = graph_margin*2;
  ellipse(width/2,width/2,circle_margin,circle_margin);
  ellipse(width/2,width/2,2*circle_margin,2*circle_margin);
  ellipse(width/2,width/2,3*circle_margin,3*circle_margin);
  ellipse(width/2,width/2,4*circle_margin,4*circle_margin);
  ellipse(width/2,width/2,width-2*graph_margin,width-2*graph_margin);
  save("radar.png");*/
  if(cat_i>12)
  {
    stop();
  }
}



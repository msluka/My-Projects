var n = 0;
var disp = document.getElementById('imgDis');

var mainImg = "http://images.all-free-download.com/images/graphiclarge/hd_picture_of_the_beautiful_natural_scenery_03_166249.jpg";
var array = [
  "",
  "http://images.all-free-download.com/images/graphiclarge/autumn_background_208731.jpg",
  "http://images.all-free-download.com/images/graphiclarge/beautiful_natural_scenery_553758.jpg",
  "http://images.all-free-download.com/images/graphiclarge/wooden_houses_lakes_mountains_238912.jpg",
  "http://images.all-free-download.com/images/graphiclarge/white_beach_and_blue_sky_204719.jpg"
];

if (n === 0) disp.src = mainImg;

function next() {
  n++;
  if (n > array.length - 1 && n !== 0) n = (0);

  imgDis.src = array[n];
  if (n === 0) disp.src = mainImg;

}

function prev() {
  n--;
  if (n < 0) n = (array.length - 1);

  imgDis.src = array[n];
  if (n === 0) disp.src = mainImg;
}

document.getElementById("imgContainer").onmouseover = function(event) {
  showButtons(event);
};

function showButtons(e) {

  document.getElementById("prev").style.display = "block";
  document.getElementById("next").style.display = "block";

}

document.getElementById("imgContainer").onmouseout = function(event) {
  hideButtons(event);
};

function hideButtons(e) {

  document.getElementById("prev").style.display = "none";
  document.getElementById("next").style.display = "none";

}
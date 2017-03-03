window.onload = bonIt();

function insert1() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 1;
  } else {
    a = a;
  }

}

function insert2() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 2;
  } else {
    a = a;
  }

}

function insert3() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 3;
  } else {
    a = a;
  }

}

function insert4() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 4;
  } else {
    a = a;
  }

}

function insert5() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 5;
  } else {
    a = a;
  }

}

function insert6() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 6;
  } else {
    a = a;
  }

}

function insert7() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 7;
  } else {
    a = a;
  }

}

function insert8() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 8;
  } else {
    a = a;
  }

}

function insert9() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 9;
  } else {
    a = a;
  }

}

function insert0() {

  var a = document.getElementById("display1").value;
  if (a.length < 13) {
    document.getElementById("display1").value = a + 0;
  } else {
    a = a;
  }

}

function divi() {

  document.getElementById("display3").innerHTML = "/  ";

  document.getElementById("display2").innerHTML = document.getElementById("display1").value;
  document.getElementById("display1").value = "";

}

function mult() {

  document.getElementById("display3").innerHTML = "*  ";


  document.getElementById("display2").innerHTML =
    document.getElementById("display1").value;
  document.getElementById("display1").value = "";
}

function bm() {

  document.getElementById("display3").innerHTML = "-  ";


  document.getElementById("display2").innerHTML =
    document.getElementById("display1").value;
  document.getElementById("display1").value = "";

}

function bp() {

  document.getElementById("display3").innerHTML = "+  ";


  document.getElementById("display2").innerHTML =
    document.getElementById("display1").value;
  document.getElementById("display1").value = "";

}


function bmd() {

  document.getElementById("display3").innerHTML = "%  ";
  document.getElementById("display2").innerHTML =
    document.getElementById("display1").value;
  document.getElementById("display1").value = "";

}

function msq() {
  var n = document.getElementById("display1").value;
  document.getElementById("display2").innerHTML = n;
  document.getElementById("display1").value = Math.sqrt(n).toFixed(2);
}

function clr() {

  document.getElementById("display1").value = "";
  document.getElementById("display2").innerHTML = "";
  document.getElementById("display3").innerHTML = "";

}


function backclr() {
  var str = document.getElementById("display1").value;

  document.getElementById("display1").value = str.slice(0, -1);

}

function myResult() {

  var dis1 = parseInt(document.getElementById("display1").value);
  var dis2 = parseInt(document.getElementById("display2").innerHTML);
  var dis3 = document.getElementById("display3").innerText;
  var resu = "";
  var a = "+",
    b = "*",
    c = "-";
  e = "/", f = "%";
  if (dis3 == a) resu = dis1 + dis2;
  if (dis3 == b) resu = dis1 * dis2;
  if (dis3 == c) resu = dis2 - dis1;
  if (dis3 == e) resu = (dis2 / dis1).toFixed(2);
  if (dis3 == f) resu = dis2 % dis1;
  if (dis3 == e && dis1 == "0") resu = "Error";

  document.getElementById("display1").value = resu;

}

var on;


function bonIt() {
  on = !on;

  if (on === false) {
    document.getElementById("display").style.background = "#ff3700";
    document.getElementById("display1").style.background = "#ff3700";
    document.getElementById("author").style.color = "white";

    var buto = document.getElementsByTagName('button');

    for (var z = 0; z < buto.length; z++) {
      buto[z].style.color = "white";
      document.getElementById("bclr").style.color = "red";
      document.getElementById("backclr").style.color = "red";
    }

  } else {
    document.getElementById("author").style.color = "gray";
    document.getElementById("bclr").style.color = "gray";
    document.getElementById("display").style.background = "gray";
    document.getElementById("display1").style.background = "gray";
    var buto2 = document.getElementsByTagName('button');
    for (var l = 0; l < buto2.length; l++) {
      buto2[l].style.color = "gray";
    }

  }

}
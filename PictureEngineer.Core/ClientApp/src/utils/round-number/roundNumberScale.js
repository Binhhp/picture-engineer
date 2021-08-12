const roundNumberScale = (num, scale) => {
    if(!("" + num).includes("e")) {
      return +(Math.round(num + "e+" + scale)  + "e-" + scale);  
    } else {
      var arr = ("" + num).split("e");
      var sig = ""
      if(+arr[1] + scale > 0) {
        sig = "+";
      }
      var i = +arr[0] + "e" + sig + (+arr[1] + scale);
      var j = Math.round(i);
      var k = +(j + "e-" + scale);
      return k;  
    }
}

export {roundNumberScale};
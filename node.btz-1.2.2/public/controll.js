$(document).ready(function(){

	var itemw = document.getElementById("w");
	var itema = document.getElementById("a");
	var items = document.getElementById("s");
	var itemd = document.getElementById("d");
	

	document.body.onkeydown = function(e){
    //String.fromCharCode(w.keyCode)
    	if (e.keyCode==87) {
    	
    	
    		itemw.setAttribute("style", "background-color:gray; width: 100px ; height: 100px")
			console.log("Press W") 
		}
		
		if (e.keyCode==65) {
			itema.setAttribute("style", "background-color:gray; width: 100px ; height: 100px")
	    	console.log("Press A") 
		}
		
		if (e.keyCode==83) {
			items.setAttribute("style", "background-color:gray; width: 100px ; height: 100px")
	    	console.log("Press S") 
		}
		
		if (e.keyCode==68) {
			itemd.setAttribute("style", "background-color:gray; width: 100px ; height: 100px")
	    	console.log("Press D") 
		}
		
		
	};

	document.body.onkeyup = function(r){
    //String.fromCharCode(w.keyCode)
    	if (r.keyCode==87) {
    		itemw.setAttribute("style", " width: 100px ; height: 100px")
			console.log("keyup W") 
		
		}
		
		if (r.keyCode==65) {
			itema.setAttribute("style", " width: 100px ; height: 100px")
	    	console.log("keyup A") 
		}
		
		if (r.keyCode==83) {
			items.setAttribute("style", " width: 100px ; height: 100px")
	    	console.log("keyup S") 
		}
		
		if (r.keyCode==68) {
			itemd.setAttribute("style", " width: 100px ; height: 100px")
	    	console.log("keyup D") 
		}
		
		
	};
	







});
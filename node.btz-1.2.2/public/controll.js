$(document).ready(function(){

	$('#searchbox input').bind('keypress', function(e) {
	if(e.keyCode==13){
		// Enter pressed... do anything here...
	}
	});
	
	$(document).keydown(function(event){ 
    $("LenkmotorSpeedMax").html("Key: " + event.which);
});
	

});
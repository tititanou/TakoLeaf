$( document ).ready(function() { /* POInt cross pour marqué le début. Jquery est pour fixé des élements quand la page ce charger avec ready.*/ 

    $( ".cross" ).hide();
    $( ".menu" ).hide();
    $( ".hamburger" ).click(function() {
    $( ".menu" ).slideToggle( "slow", function() {
    $( ".hamburger" ).hide();
    $( ".cross" ).show();
    });
    });
    
    $( ".cross" ).click(function() {
    $( ".menu" ).slideToggle( "slow", function() {
    $( ".cross" ).hide();
    $( ".hamburger" ).show();
    });
    });
    
    $( "#testjs" ).click(function(){
        alert("J'ai cliqué houhou");
    })
    });



/* $ permet de recupérer des éléments. 
Le point cross est pour récupérer des élementsd avec la classe cross
#cross est pour récupérer les élements */


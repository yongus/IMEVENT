/* Style Switcher */

window.console = window.console || (function(){
	var c = {}; c.log = c.warn = c.debug = c.info = c.error = c.time = c.dir = c.profile = c.clear = c.exception = c.trace = c.assert = function(){};
	return c;
})();

jQuery(document).ready(function(){ 
						   
var styleswitcherstr = ' \
	<div class="switcher-inner"> \
	<h2>Style Switcher <a href="#"></a></h2> \
    <div class="content"> \
    <h3>Color Style</h3> \
    <div class="switcher-box"> \
	<ul class="color_schemes"> \
	<li><a id="default" class="styleswitch color"> Default </a></li> \
    <li><a id="brown" class="styleswitch color">Brown </a></li> \
    <li><a id="blue" class="styleswitch color"> Blue </a></li> \
    <li><a id="green" class="styleswitch color">Green</a></li> \
    <li><a id="purple" class="styleswitch color">Purple</a></li> \
    </div><!-- End switcher-box --> \
    </div><!-- End content --> \
	';
	
jQuery(".switcher").prepend( styleswitcherstr );

});

/* boxed & wide syle */
jQuery(document).ready(function(){ 

var cookieName = 'wide';

function changeLayout(layout) {
jQuery.cookie(cookieName, layout);
jQuery('head link[name=layout]').attr('href', 'css/layout/' + layout + '.css');
}

if( jQuery.cookie(cookieName)) {
changeLayout(jQuery.cookie(cookieName));
}

jQuery("#wide").click( function(){ jQuery
changeLayout('wide');
});

jQuery("#boxed").click( function(){ jQuery
changeLayout('boxed');
});

});


/* background images */
jQuery(document).ready(function(){ 
  
  var startClass = jQuery.cookie('mycookie');
  jQuery("body").addClass("wood");

/* crossed */
jQuery("#crossed").click( function(){ 
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('crossed');
  jQuery.cookie('mycookie','crossed');
});

/* fabric */
jQuery("#fabric").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('fabric');
  jQuery.cookie('mycookie','fabric');
});

/* linen */
jQuery("#linen").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('linen');
  jQuery.cookie('mycookie','linen');
});

/* wood */
jQuery("#wood").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('wood');
  jQuery.cookie('mycookie','wood');
});

/* diagmonds */
jQuery("#diagmonds").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('diagmonds');
  jQuery.cookie('mycookie','diagmonds');
});

/* triangles */
jQuery("#triangles").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('triangles');
  jQuery.cookie('mycookie','triangles');
});

/* triangles */
jQuery("#black_mamba").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('black_mamba');
  jQuery.cookie('mycookie','black_mamba');
});

/* vichy */
jQuery("#vichy").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('vichy');
  jQuery.cookie('mycookie','vichy');
});

/* back_pattern */
jQuery("#back_pattern").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('back_pattern');
  jQuery.cookie('mycookie','back_pattern');
});

/* checkered_pattern */
jQuery("#checkered_pattern").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('checkered_pattern');
  jQuery.cookie('mycookie','checkered_pattern');
});

/* diamond_upholstery */
jQuery("#diamond_upholstery").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('diamond_upholstery');
  jQuery.cookie('mycookie','diamond_upholstery');
});

/* lyonnette */
jQuery("#lyonnette").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('lyonnette');
  jQuery.cookie('mycookie','lyonnette');
});

/* graphy */
jQuery("#graphy").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('black_thread');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('graphy');
  jQuery.cookie('mycookie','graphy');
});

/* black_thread */
jQuery("#black_thread").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('subtlenet2');
  jQuery("body").addClass('black_thread');
  jQuery.cookie('mycookie','black_thread');
});

/* subtlenet2 */
jQuery("#subtlenet2").click( function(){ 
  jQuery("body").removeClass('crossed');
  jQuery("body").removeClass('fabric');
  jQuery("body").removeClass('linen');
  jQuery("body").removeClass('wood');
  jQuery("body").removeClass('diagmonds');
  jQuery("body").removeClass('triangles');
  jQuery("body").removeClass('black_mamba');
  jQuery("body").removeClass('vichy');
  jQuery("body").removeClass('back_pattern');
  jQuery("body").removeClass('checkered_pattern');
  jQuery("body").removeClass('diamond_upholstery');
  jQuery("body").removeClass('lyonnette');
  jQuery("body").removeClass('graphy');
  jQuery("body").removeClass('black_thread');
  jQuery("body").addClass('subtlenet2');
  jQuery.cookie('mycookie','subtlenet2');
});



if (jQuery.cookie('mycookie')) {
  jQuery('body').addClass(jQuery.cookie('mycookie'));
}

});

/* Skins Style */
jQuery(document).ready(function(){ 

var cookieName = 'default';

function changeLayout(layout) {
jQuery.cookie(cookieName, layout);
jQuery('head link[name=skins]').attr('href', 'css/skins/' + layout + '.css');
}

if( jQuery.cookie(cookieName)) {
changeLayout(jQuery.cookie(cookieName));
}

jQuery("#default").click( function(){ jQuery
changeLayout('default');
});

jQuery("#brown").click( function(){ jQuery
changeLayout('golden');
});

jQuery("#blue").click( function(){ jQuery
changeLayout('blue');
});

jQuery("#green").click( function(){ jQuery
changeLayout('red');
});

jQuery("#purple").click( function(){ jQuery
changeLayout('purple');
});


});


/* Reset Switcher */
jQuery(document).ready(function(){ 

// Style Switcher	
jQuery('.switcher').animate({
left: '-160px'
});

jQuery('.switcher h2 a').click(function(e){
e.preventDefault();
var div = jQuery('.switcher');
console.log(div.css('left'));
if (div.css('left') === '-160px') {
jQuery('.switcher').animate({
  left: '0px'
}); 
} else {
jQuery('.switcher').animate({
  left: '-160px'
});
}
})


/*jQuery('a.color').click(function(e){
e.preventDefault();
jQuery(this).parent().parent().find('a').removeClass('selected');
jQuery(this).addClass('selected');
})

jQuery('a.pattern').click(function(e){
e.preventDefault();
jQuery(this).parent().parent().find('a').removeClass('selected2');
jQuery(this).addClass('selected2');
})

jQuery('a.layout').click(function(e){
e.preventDefault();
jQuery(this).parent().parent().find('a').removeClass('selected3');
jQuery(this).addClass('selected3');
})*/

		 
});						   


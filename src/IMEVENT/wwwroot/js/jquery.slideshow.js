// page init

jQuery(function(){

	initSlideShow();

});



// slideshow init

function initSlideShow() {

	jQuery('div.slideshow').fadeGallery({

		slides: '.gmask li',

		currentNumber: 'span.cur-num',

		totalNumber: 'span.all-num',

		switchSimultaneously: true,

		disableWhileAnimating: false,

		generatePagination: 'div.pagination',

		autoRotation: true,

		autoHeight: true,

		switchTime:3000,

		animSpeed:800

	});

}



/*

 * jQuery SlideShow plugin

 */

;(function($){

	function FadeGallery(options) {

		this.options = $.extend({

			slides: 'ul.slideset > li',

			activeClass:'active',

			disabledClass:'disabled',

			btnPrev: 'a.btn-prev',

			btnNext: 'a.btn-next',

			generatePagination: false,

			pagerList: '<ul>',

			pagerListItem: '<li><a href="#"></a></li>',

			pagerListItemText: 'a',

			pagerLinks: '.pagination li',

			currentNumber: 'span.current-num',

			totalNumber: 'span.total-num',

			btnPlay: '.btn-play',

			btnPause: '.btn-pause',

			btnPlayPause: '.btn-play-pause',

			autorotationActiveClass: 'autorotation-active',

			autorotationDisabledClass: 'autorotation-disabled',

			circularRotation: true,

			switchSimultaneously: true,

			disableWhileAnimating: false,

			disableFadeIE: false,

			autoRotation: false,

			pauseOnHover: true,

			autoHeight: false,

			switchTime: 4000,

			animSpeed: 600,

			event:'click'

		}, options);

		this.init();

	}

	FadeGallery.prototype = {

		init: function() {

			if(this.options.holder) {

				this.findElements();

				this.initStructure();

				this.attachEvents();

				this.refreshState(true);

				this.autoRotate();

			}

		},

		findElements: function() {

			// control elements

			this.gallery = $(this.options.holder);

			this.slides = this.gallery.find(this.options.slides);

			this.slidesHolder = this.slides.eq(0).parent();

			this.stepsCount = this.slides.length;

			this.btnPrev = this.gallery.find(this.options.btnPrev);

			this.btnNext = this.gallery.find(this.options.btnNext);

			this.currentIndex = 0;

			

			// disable fade effect in old IE

			if(this.options.disableFadeIE && $.browser.msie && $.browser.version < 9) {

				this.options.animSpeed = 0;

			}

			

			// create gallery pagination

			if(typeof this.options.generatePagination === 'string') {

				this.pagerHolder = this.gallery.find(this.options.generatePagination).empty();

				this.pagerList = $(this.options.pagerList).appendTo(this.pagerHolder);

				for(var i = 0; i < this.stepsCount; i++) {

					$(this.options.pagerListItem).appendTo(this.pagerList).find(this.options.pagerListItemText).text(i+1);

				}

				this.pagerLinks = this.pagerList.children();

			} else {

				this.pagerLinks = this.gallery.find(this.options.pagerLinks);

			}

			

			// get start index

			var activeSlide = this.slides.filter('.'+this.options.activeClass);

			if(activeSlide.length) {

				this.currentIndex = this.slides.index(activeSlide);

			}

			this.prevIndex = this.currentIndex;

			

			// autorotation control buttons

			this.btnPlay = this.gallery.find(this.options.btnPlay);

			this.btnPause = this.gallery.find(this.options.btnPause);

			this.btnPlayPause = this.gallery.find(this.options.btnPlayPause);

			

			// misc elements

			this.curNum = this.gallery.find(this.options.currentNumber);

			this.allNum = this.gallery.find(this.options.totalNumber);

		},

		initStructure: function() {

			this.slides.css({display:'block',opacity:0}).eq(this.currentIndex).css({

				opacity:''

			});

		},

		attachEvents: function() {

			this.btnPrev.bind(this.options.event, this.bindScope(function(e){

				this.prevSlide();

				e.preventDefault();

			}));

			this.btnNext.bind(this.options.event, this.bindScope(function(e){

				this.nextSlide();

				e.preventDefault();

			}));

			this.pagerLinks.each(this.bindScope(function(ind, obj){

				$(obj).bind(this.options.event, this.bindScope(function(e){

					this.numSlide(ind);

					e.preventDefault();

				}));

			}));

			

			// autorotation buttons handler

			this.btnPlay.bind(this.options.event, this.bindScope(function(e){

				this.startRotation();

				e.preventDefault();

			}));

			this.btnPause.bind(this.options.event, this.bindScope(function(e){

				this.stopRotation();

				e.preventDefault();

			}));

			this.btnPlayPause.bind(this.options.event, this.bindScope(function(e){

				if(!this.gallery.hasClass(this.options.autorotationActiveClass)) {

					this.startRotation();

				} else {

					this.stopRotation();

				}

				e.preventDefault();

			}));

			

			// pause on hover handling

			if(this.options.pauseOnHover) {

				this.gallery.hover(this.bindScope(function(){

					if(this.options.autoRotation) {

						this.galleryHover = true;

						this.pauseRotation();

					}

				}), this.bindScope(function(){

					if(this.options.autoRotation) {

						this.galleryHover = false;

						this.resumeRotation();

					}

				}));

			}

		},

		prevSlide: function() {

			if(!(this.options.disableWhileAnimating && this.galleryAnimating)) {

				this.prevIndex = this.currentIndex;

				if(this.currentIndex > 0) {

					this.currentIndex--;

					this.switchSlide();

				} else if(this.options.circularRotation) {

					this.currentIndex = this.stepsCount - 1;

					this.switchSlide();

				}

			}

		},

		nextSlide: function(fromAutoRotation) {

			if(!(this.options.disableWhileAnimating && this.galleryAnimating)) {

				this.prevIndex = this.currentIndex;

				if(this.currentIndex < this.stepsCount - 1) {

					this.currentIndex++;

					this.switchSlide();

				} else if(this.options.circularRotation || fromAutoRotation === true) {

					this.currentIndex = 0;

					this.switchSlide();

				}

			}

		},

		numSlide: function(c) {

			if(this.currentIndex != c) {

				this.prevIndex = this.currentIndex;

				this.currentIndex = c;

				this.switchSlide();

			}

		},

		switchSlide: function() {

			if(this.slides.length > 1) {

				this.galleryAnimating = true;

				this.slides.eq(this.prevIndex).stop().animate({opacity:0},{duration: this.options.animSpeed, complete: this.bindScope(function(){

					this.slides.eq(this.prevIndex).css({opacity:0});

				})});

				clearTimeout(this.switchTimer);

				this.switchTimer = setTimeout(this.bindScope(function(){

					this.slides.eq(this.currentIndex).stop().animate({opacity:1},{duration: this.options.animSpeed, complete: this.bindScope(function(){

						this.slides.eq(this.currentIndex).css({opacity:''});

						this.galleryAnimating = false;

						this.autoRotate();

					})});

					

				}), this.options.switchSimultaneously ? 1 : this.options.animSpeed);

				this.refreshState();

			}

		},

		refreshState: function(initial) {

			this.slides.removeClass(this.options.activeClass).eq(this.currentIndex).addClass(this.options.activeClass);

			this.pagerLinks.removeClass(this.options.activeClass).eq(this.currentIndex).addClass(this.options.activeClass);

			this.curNum.html(this.currentIndex+1);

			this.allNum.html(this.stepsCount);

			

			// initial refresh

			if(initial) {

				if(this.options.autoHeight) {

					this.slidesHolder.css({height: this.slides.eq(this.currentIndex).outerHeight(true) });

				}

			} else {

				if(this.options.autoHeight) {

					this.slidesHolder.stop().animate({height: this.slides.eq(this.currentIndex).outerHeight(true) });

				}

			}

			

			// disabled state

			if(!this.options.circularRotation) {

				this.btnPrev.add(this.btnNext).removeClass(this.options.disabledClass);

				if(this.currentIndex === 0) this.btnPrev.addClass(this.options.disabledClass);

				if(this.currentIndex === this.stepsCount - 1) this.btnNext.addClass(this.options.disabledClass);

			}

		},

		startRotation: function() {

			this.options.autoRotation = true;

			this.galleryHover = false;

			this.autoRotationStopped = false;

			this.resumeRotation();

		},

		stopRotation: function() {

			this.galleryHover = true;

			this.autoRotationStopped = true;

			this.pauseRotation();

		},

		pauseRotation: function() {

			this.gallery.addClass(this.options.autorotationDisabledClass);

			this.gallery.removeClass(this.options.autorotationActiveClass);

			clearTimeout(this.timer);

		},

		resumeRotation: function() {

			if(!this.autoRotationStopped) {

				this.gallery.addClass(this.options.autorotationActiveClass);

				this.gallery.removeClass(this.options.autorotationDisabledClass);

				this.autoRotate();

			}

		},

		autoRotate: function() {

			clearTimeout(this.timer);

			if(this.options.autoRotation && !this.galleryHover && !this.autoRotationStopped) {

				this.gallery.addClass(this.options.autorotationActiveClass);

				this.timer = setTimeout(this.bindScope(function(){

					this.nextSlide(true);

				}), this.options.switchTime);

			} else {

				this.pauseRotation();

			}

		},

		bindScope: function(func, scope) {

			return $.proxy(func, scope || this);

		}

	}



	// jquery plugin

	$.fn.fadeGallery = function(opt){

		return this.each(function(){

			$(this).data('FadeGallery', new FadeGallery($.extend(opt,{holder:this})));

		});

	}

}(jQuery));
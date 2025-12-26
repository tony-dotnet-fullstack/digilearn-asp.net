/*=========================================================================================
    File Name: ext-component-media-player.js
    Description:extra components media player using Plyr plugin
==========================================================================================*/
$(document).ready(function () {

	var i18n =  {
		restart: 'شروع مجدد',
		rewind: 'Rewind {seektime}s',
		play: 'پخش',
		pause: 'مکث',
		fastForward: 'جلو کشیدن {seektime} ثانیه',
		seek: 'Seek',
		seekLabel: '{currentTime} از {duration}',
		played: 'پخش شده',
		buffered: 'بافر شده',
		currentTime: 'Time کنونی',
		duration: 'مدت',
		volume: 'بلندی صدا',
		mute: 'بی صدا',
		unmute: 'با صدا',
		enableCaptions: 'Active کردن زیرنویس',
		disableCaptions: 'disable Active کردن زیرنویس',
		download: 'دریافت',
		enterFullscreen: 'ورود به تمام صفحه',
		exitFullscreen: 'exit از تمام صفحه',
		frameTitle: 'پخش کننده برای {title}',
		captions: 'زیرنویس ها',
		settings: 'تنظیمات',
		pip: 'تصویر در تصویر',
		menuBack: 'بازگشت به فهرست previous',
		speed: 'سرعت',
		normal: 'طبیعی',
		quality: 'کیفیت',
		loop: 'چرخه',
		start: 'شروع',
		end: 'پایان',
		all: 'all',
		reset: 'بازنشانی',
		disabled: 'disableActive',
		enabled: 'Active',
		advertisement: 'تبلیغات',
		qualityBadge: {
		  2160: '4K',
		  1440: 'HD',
		  1080: 'HD',
		  720: 'HD',
		  576: 'SD',
		  480: 'SD',
		},
	  }

	// video player  define
	var player = new Plyr(".video-player", {
		i18n: i18n
	});
	// audio player define
	var player1 = new Plyr(".audio-player", {
		i18n: i18n
	});
});
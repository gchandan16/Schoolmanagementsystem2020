!function(){"use strict";var u="undefined"!=typeof window&&void 0!==window.document?window.document:{},e="undefined"!=typeof module&&module.exports,l="undefined"!=typeof Element&&"ALLOW_KEYBOARD_INPUT"in Element,r=function(){for(var e,n=[["requestFullscreen","exitFullscreen","fullscreenElement","fullscreenEnabled","fullscreenchange","fullscreenerror"],["webkitRequestFullscreen","webkitExitFullscreen","webkitFullscreenElement","webkitFullscreenEnabled","webkitfullscreenchange","webkitfullscreenerror"],["webkitRequestFullScreen","webkitCancelFullScreen","webkitCurrentFullScreenElement","webkitCancelFullScreen","webkitfullscreenchange","webkitfullscreenerror"],["mozRequestFullScreen","mozCancelFullScreen","mozFullScreenElement","mozFullScreenEnabled","mozfullscreenchange","mozfullscreenerror"],["msRequestFullscreen","msExitFullscreen","msFullscreenElement","msFullscreenEnabled","MSFullscreenChange","MSFullscreenError"]],l=0,r=n.length,t={};l<r;l++)if((e=n[l])&&e[1]in u){for(l=0;l<e.length;l++)t[n[0][l]]=e[l];return t}return!1}(),t={change:r.fullscreenchange,error:r.fullscreenerror},n={request:function(e){var n=r.requestFullscreen;e=e||u.documentElement,/ Version\/5\.1(?:\.\d+)? Safari\//.test(navigator.userAgent)?e[n]():e[n](l&&Element.ALLOW_KEYBOARD_INPUT)},exit:function(){u[r.exitFullscreen]()},toggle:function(e){this.isFullscreen?this.exit():this.request(e)},onchange:function(e){this.on("change",e)},onerror:function(e){this.on("error",e)},on:function(e,n){var l=t[e];l&&u.addEventListener(l,n,!1)},off:function(e,n){var l=t[e];l&&u.removeEventListener(l,n,!1)},raw:r};r?(Object.defineProperties(n,{isFullscreen:{get:function(){return Boolean(u[r.fullscreenElement])}},element:{enumerable:!0,get:function(){return u[r.fullscreenElement]}},enabled:{enumerable:!0,get:function(){return Boolean(u[r.fullscreenEnabled])}}}),e?module.exports=n:window.screenfull=n):e?module.exports=!1:window.screenfull=!1}();
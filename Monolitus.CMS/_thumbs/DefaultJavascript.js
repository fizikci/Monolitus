var ajaxLog = {};
function callAPIMethod(method, data, successCallback, failCallback) {
    ajaxLog[method] = new Date(); 
    $.ajax({
        url: "APIBridgeHandler.ashx?method="+method,
        data: "data="+JSON.stringify(data),
        type: 'POST',
        dataType: "json",
        success: function (res) {
            //res = eval('('+TOB64.decode(res)+')');
            console.log(method + ': ' + (new Date() - ajaxLog[method]) + ' ms');
            if (res.IsError)
                (failCallback || defaultFailCallback)(res);
            else
                (successCallback || defaultSuccessCallback)(res.Data);
                
            console.log(method + '+callback: ' + (new Date() - ajaxLog[method]) + ' ms');
        },
        error: function (err) {
            alert(JSON.stringify(err, null, '\t'));
        }
    });
}

function defaultFailCallback(res) {
    alertNice(res.ErrorMessage);
}

function defaultSuccessCallback(res) {
    alertNice(JSON.stringify(res, null, '\t'));
}

function alertNice(msg, title){
    if(!title) title = 'Monolitus';
    if(!$('#alertNice').length){
        var html = '';
        html += '<div class="modal fade" id="alertNice" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">';
        html += '  <div class="modal-dialog">';
        html += '    <div class="modal-content">';
        html += '      <div class="modal-header">';
        html += '        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
        html += '        <h4 class="modal-title" id="myModalLabel"></h4>';
        html += '      </div>';
        html += '      <div class="modal-body">';
        html += '      </div>';
        html += '      <div class="modal-footer">';
        html += '        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>';
        html += '      </div>';
        html += '    </div>';
        html += '  </div>';
        html += '</div>';
        $('body').append(html);
    }
    $('#alertNice .modal-body').html(msg);
    $('#alertNice .modal-title').html(title);
    $('#alertNice').modal('show');
}


// Create Base64 Object
var TOB64={_keyStr:"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",encode:function(e){var t="";var n,r,i,s,o,u,a;var f=0;e=Base64._utf8_encode(e);while(f<e.length){n=e.charCodeAt(f++);r=e.charCodeAt(f++);i=e.charCodeAt(f++);s=n>>2;o=(n&3)<<4|r>>4;u=(r&15)<<2|i>>6;a=i&63;if(isNaN(r)){u=a=64}else if(isNaN(i)){a=64}t=t+this._keyStr.charAt(s)+this._keyStr.charAt(o)+this._keyStr.charAt(u)+this._keyStr.charAt(a)}return t},decode:function(e){var t="";var n,r,i;var s,o,u,a;var f=0;e=e.replace(/[^A-Za-z0-9\+\/\=]/g,"");while(f<e.length){s=this._keyStr.indexOf(e.charAt(f++));o=this._keyStr.indexOf(e.charAt(f++));u=this._keyStr.indexOf(e.charAt(f++));a=this._keyStr.indexOf(e.charAt(f++));n=s<<2|o>>4;r=(o&15)<<4|u>>2;i=(u&3)<<6|a;t=t+String.fromCharCode(n);if(u!=64){t=t+String.fromCharCode(r)}if(a!=64){t=t+String.fromCharCode(i)}}t=Base64._utf8_decode(t);return t},_utf8_encode:function(e){e=e.replace(/\r\n/g,"\n");var t="";for(var n=0;n<e.length;n++){var r=e.charCodeAt(n);if(r<128){t+=String.fromCharCode(r)}else if(r>127&&r<2048){t+=String.fromCharCode(r>>6|192);t+=String.fromCharCode(r&63|128)}else{t+=String.fromCharCode(r>>12|224);t+=String.fromCharCode(r>>6&63|128);t+=String.fromCharCode(r&63|128)}}return t},_utf8_decode:function(e){var t="";var n=0;var r=c1=c2=0;while(n<e.length){r=e.charCodeAt(n);if(r<128){t+=String.fromCharCode(r);n++}else if(r>191&&r<224){c2=e.charCodeAt(n+1);t+=String.fromCharCode((r&31)<<6|c2&63);n+=2}else{c2=e.charCodeAt(n+1);c3=e.charCodeAt(n+2);t+=String.fromCharCode((r&15)<<12|(c2&63)<<6|c3&63);n+=3}}return t}}
// form to json
$.fn.serializeObject = function()
{
    var o = {};
    var a = this.serializeArray();
    $.each(a, function() {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

function shuffle(o){ //v1.0
    for(var j, x, i = o.length; i; j = Math.floor(Math.random() * i), x = o[--i], o[i] = o[j], o[j] = x);
    return o;
};

$(function(){
    $(".theme-link").click(function(){setTheme($(this));});
    markTheme();
});
function markTheme(){
    var currTheme = getCookie('theme') || 'United';
    $(".theme-link").parent().removeClass("active");
    $(".theme-link:contains('"+currTheme+"')").parent().addClass("active");    
}
function setTheme(elm){
    setCookie('theme', $(elm).text());
    $('#tema').attr('href','http://maxcdn.bootstrapcdn.com/bootswatch/3.2.0/'+$(elm).text().toLowerCase()+'/bootstrap.min.css');
    markTheme();
}

function panelHoverIn(elm) {
   $(elm).addClass("panel-primary");
}
function panelHoverOut(elm) {
   $(elm).removeClass("panel-primary");
}


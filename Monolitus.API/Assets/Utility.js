var ajaxLoadingTimeoutHandle = 0;
$(document).on({
    ajaxStart: function() {
        //ajaxLoadingTimeoutHandle = setTimeout(function() { $("body").addClass("loading"); }, 2000);
    },
    ajaxStop: function () {
        //clearTimeout(ajaxLoadingTimeoutHandle);
        //$("body").removeClass("loading");
    }
});

function bindTable(options) {
    var oTable1 = $(options.tableId).dataTable({
        "aaData": options.list,
        "bDestroy": true,
        "bRetrieve": true,
        "bProcessing": true,
        "oTableTools": {
            "sRowSelect": "single"
        },
        "iDisplayLength": options.pageSize || 15,

        "aoColumns": options.columns,
        
        "fnRowCallback": options.rowCallback || function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {
            var rowClass = aData.IsDeleted ? "deleted" : "";
            $(nRow).addClass(rowClass);
            return nRow;
        },

        "sAjaxDataProp": "data",
        "oLanguage": {
            //"sLengthMenu": "Her sayfada  _MENU_ kayıt görülsün",
            //"sZeroRecords": "Kayıt bulunamadı",
            //"sInfo": " Görüntülenen: _START_ - _END_ |  Toplam: _TOTAL_  kayıt",
            //"sInfoEmpty": "Görüntülenecek kayıt yok.",
            //"sInfoFiltered": "  Toplam kayıt sayısı: _MAX_ ",
            //"sSearch": "Ara"
        }
    });

    $('table th input:checkbox').on('click', function () {
        var that = this;
        $(this).closest('table').find('tr td input:checkbox')
        .each(function () {
            this.checked = that.checked;
            $(this).closest('tr').toggleClass('checked');
        });
    });

    $('table td input:checkbox').on('click', function () {
        $(this).closest('tr').toggleClass('checked');
    });

    $("#table-entity tbody tr").on('dblclick', function (e) {
        if ($(this).hasClass('row_selected')) {
            $(this).removeClass('row_selected');
        }
        else {
            oTable1.$('tr.row_selected').removeClass('row_selected');
            $(this).addClass('row_selected');
        }
    });

    $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });
    function tooltip_placement(context, source) {
        var $source = $(source);
        var $parent = $source.closest('table')
        var off1 = $parent.offset();
        var w1 = $parent.width();

        var off2 = $source.offset();
        var w2 = $source.width();

        if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
        return 'left';
    }

    $(options.tableId).prev().find('.col-sm-6').hide();
}

function loadForm(theForm, entity) {
    theForm = $(theForm);
    theForm.data('entity', entity);

    for (var key in entity) {
        try {
            var ctrl = $('input[name=' + key + '],select[name=' + key + '],textarea[name=' + key + '],img[name=' + key + ']');
            if (ctrl.length > 0) {
                if (ctrl[0].tagName.toLowerCase() == 'select' && ctrl.attr("bool"))
                    ctrl.val(entity[key]?1:0);
                else if (ctrl[0].tagName.toLowerCase() == 'select' && ctrl.attr("multiple"))
                    setChosenValue(ctrl, entity[key]);
                else if (ctrl[0].tagName.toLowerCase() == 'img' && entity[key])
                    ctrl.attr('src', entity[key]);
                else if (ctrl.attr('type') == 'checkbox')
                    ctrl.prop('checked', entity[key] ? true : false);
                else if (ctrl.hasClass('date-picker')) {
                    var d = entity[key].substr(0, 10);
                    if (d) {
                        var parts = d.split('-');
                        ctrl.val(parts[2] + '-' + parts[1] + '-' + parts[0]);
                    }
                }
                else
                    ctrl.val(entity[key]);
            }
        } catch (e) {
            console.log(e);
        }
    }
}

function setChosenValue(chosen, val) {
    $(chosen).val(val.split(','));
    $(chosen).trigger("chosen:updated");
}

function makeChosen(select, entityName, filter) {
    var select = $(select);
    select.chosen();
    select.next().find(".chosen-select-deselect").chosen({ allow_single_deselect: true });
    select.next().find('.chosen-search input').autocomplete({
        source: function (request, response) {
            readEntityList(entityName, filter, function (entityList) {
                select.next().find('ul.chosen-results').empty(); select.empty();
                response($.map(entityList, function (item) {
                    select.next().find('ul.chosen-results').append('<li class="active-result">' + item.Name + '</li>');
                    select.append('<option value="' + item.Id + '">' + item.Name + '</option>');
                }));
                select.trigger("chosen:updated");
            });
        }
    });
}

function fillSelectOptions(select, entityName, filter) {
    var select = $(select);
    var entity = select.closest('form').data('entity');
    readEntityList(entityName, filter, function (entityList) {
        var html = '';
        entityList.each(function (item) {
            html += '<option value="' + item.Id + '">' + item.Name + '</option>';
        });
        select.append(html);
        if (entity) {
            var key = select.attr('id');
            select.val(entity[key]);
        }
    }, 'Name');
}

//function jsonToFormData(obj) {
//    var str = '';
//    for (var key in obj) {
//        if(typeof obj[key] != 'object')
//            str += '&' + key + '=' + encodeURIComponent((obj[key] || '').toString());
//    }
//    return str;
//}

function alertNice(msg, title, btn) {
    if (!title) title = 'Monolitus';
    if (msg[0] == '#') msg = $(msg).html();
    if (!$('#alertNice').length) {
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
        if (btn)
            html += btn;
        html += '        <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>';
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


function openFileManager(selectedPath, onSelectFile) {
    //var win = new Window({ className: 'alphacube', title: '<span class="fff folder_picture"></span> ' + lang('File Manager'), minWidth: 913, width: 965, minHeight: 350, height: 600, wiredDrag: true, destroyOnClose: true });
    if (!$('#theFileBrowser').length)
        $('body').prepend('<div id="theFileBrowser"></div>');

    var winContent = $('#theFileBrowser');
    var fm = new FileManager({
        container: winContent,
        folder: selectedPath ? selectedPath.substring(0, selectedPath.lastIndexOf("/")) : undefined,
        onSelectFile: onSelectFile
    });

    return fm;
}


var fileBrowserCurrInput, list, footer, currFolder, currPicEdit;
var FileManager = Class.create(); FileManager.prototype = {
    width: 600,
    height: 500,
    title: 'Select file',
    container: null,
    folder: '/Medya',
    listMode: 'pics',
    onSelectFile: null,
    canDelete: true,
    initialize: function (options) {
        Object.extend(this, options);
        if (!this.folder) this.folder = '/Medya';
        this.container = $(this.container ? this.container : document.body);
        var html = '<div><div id="fileBrowserList" onselectstart="return false;"></div>' +
            '<div id="fileBrowserFooter">' +
            '<form action="/Staff/Handlers/SystemInfo.ashx?method=uploadFile" method="post" enctype="multipart/form-data" target="fakeUplFrm" class="ui-widget-content ui-corner-all">' +
            '<input type="hidden" name="folder"/>' +
            'Dosya: <input type="file" name="upload" multiple="multiple" style="display:inline"/><input type="submit" value="Yükle"/><div id="fileBrowserLoading">&nbsp;</div>' +
            '<iframe name="fakeUplFrm"></iframe>' +
            '</form>' +
            '<form action="/Staff/Handlers/SystemInfo.ashx?method=createFolder" method="post" target="fakeUplFrm" class="ui-widget-content ui-corner-all">' +
            '<input type="hidden" name="folder"/>' +
            'Klasör: <input type="text" name="name" style="width:80px"/><input type="submit" value="Oluştur"/>' +
            '</form>' +
            '<form id="fileManagerRenameForm" action="/Staff/Handlers/SystemInfo.ashx?method=renameFile" method="post" target="fakeUplFrm" class="ui-widget-content ui-corner-all delForm">' +
            '<input type="hidden" name="folder"/><input type="hidden" name="name"/>' +
            'Adını: <input type="text" name="newName" style="width:80px"/><input type="submit" value="Değiştir"/>' +
            '</form>' +
        (this.canDelete ? ('<form action="/Staff/Handlers/SystemInfo.ashx?method=deleteFile" method="post" target="fakeUplFrm" class="ui-widget-content ui-corner-all delForm">' +
            '<input type="hidden" name="folder"/>' +
            '<input type="hidden" name="name"/><input type="submit" value="Sil"/>' +
            '</form>') : '') +
            '<input type="button" onclick="$(\'#theFileBrowser\').hide(\'fast\', function(){$(this).remove();})" value="Kapat"/>' +
            '</div></div>';
        this.container.append(html);
        this.container.find('form').each(function (eix, frm) {
            $(frm).on('submit', function () {
                $('#fileBrowserLoading').show();
                $(frm).find('input[name=folder]').val(currFolder);
                if ($(frm).hasClass('delForm'))
                    $(frm).find('input[name=name]').val($A($('#fileBrowserList .fileSelected')).collect(function (elm) { return $(elm).attr('name'); }).join('#NL#'));
            });
        });
        currFolder = this.folder;
        currPicEdit = this;
        this.getFileList();

        var ths = this;

        $('#fb_btnImgEdit').on('click', function () {
            var arr = ths.getSelectedFiles();
            if (arr.length > 0) {
                var name = arr[0];
                if (name.endsWith('.png') || name.endsWith('.jpg') || name.endsWith('.jpeg') || name.endsWith('.gif') || name.endsWith('.jpe'))
                    editImage(name);
                else if (name.endsWith('.txt') || name.endsWith('.js') || name.endsWith('.css') || name.endsWith('.html') || name.endsWith('.htm'))
                    editTextFile(name.substring(name.lastIndexOf('/') + 1));
                else
                    niceAlert(lang('Plese select a picture file to edit'));
            }
        });

    },
    getFileList: function () {
        var list = $('#fileBrowserList');

        if ($('#fileBrowserLoading').length) $('#fileBrowserLoading').show();
        var ths = this;
        new Ajax.Request('/Staff/Handlers/SystemInfo.ashx?method=getFileList&folder=' + currFolder, {
            onComplete: function (resp) {
                resp = eval("(" + resp.responseText + ")");
                if (resp.success) {
                    var folders = currFolder.substring(1).split('/');
                    var folderLinks = '';
                    for (var i = 0; i < folders.length; i++) {
                        var str = '';
                        for (var k = 0; k <= i; k++)
                            str += '/' + folders[k];
                        folderLinks += '<span onclick="currFolder = \'' + str + '\'; currPicEdit.getFileList();">' + folders[i] + '</span>' + ' / ';
                    }
                    var str = '<div class="nav ui-widget-content ui-corner-all">' + folderLinks + '</div>';
                    if (ths.listMode == 'details') {
                        str = '<table class="fileList" cellspacing="0" border="0">'; //<tr><th>Ad</th><th>Boyut</th><th>Tarih</th></tr>
                        for (var i = 0; i < resp.root.length; i++) {
                            var item = resp.root[i];
                            str += '<tr><td class="fileName ' + ths.getFileClassName(item) + (item.size < 0 ? "" : " fileItem") + '" name="' + item.name + '">' + item.name + '</td><td class="size">' + ths.getSize(item) + '</td><td class="date">' + ths.formatDate(item.date) + '</td></tr>';
                        }
                        str += '</table>';
                    } else {
                        for (var i = 0; i < resp.root.length; i++) {
                            var item = resp.root[i];
                            var fileClass = ths.getFileClassName(item);
                            if (fileClass == 'picture') {
                                var src = (currFolder + '/' + item.name);
                                str += '<div class="fileNameBox ' + ths.getFileClassName(item) + (item.size < 0 ? "" : " fileItem") + ' ui-widget-content ui-corner-all" name="' + item.name + '"><img src="' + src + '"/><br/>' + item.name + '</div>';
                            } else {
                                var src = ('/Assets/images/icons/' + fileClass + '.png');
                                str += '<div class="fileNameBox ' + ths.getFileClassName(item) + (item.size < 0 ? "" : " fileItem") + ' ui-widget-content ui-corner-all" name="' + item.name + '"><img src="' + src + '"/><br/>' + item.name + '</div>';
                            }

                        }
                    }
                    list.html(str);
                    list.find('.folder').each(function (eix, elm) {
                        $(elm).on('click', function (event) {
                            var path = currFolder + '/' + $(elm).attr('name');
                            if (!(event.ctrlKey || event.shiftKey || event.metaKey))
                                list.find('.fileNameBox').each(function (eix, fnm) { $(fnm).removeClass('fileSelected'); });
                            $(elm).toggleClass('fileSelected');
                            $('#fileManagerRenameForm input[name=newName]').val($(elm).attr('name'));
                        });
                        $(elm).on('dblclick', function () {
                            var f = $(elm).attr('name');
                            currFolder = currFolder + '/' + f;
                            ths.getFileList();
                        });
                    });
                    list.find('.fileItem').each(function (eix, elm) {
                        $(elm).on('dblclick', function () {
                            var path = currFolder + '/' + $(elm).attr('name');
                            if (ths.onSelectFile) {
                                ths.onSelectFile(path);
                                $('#theFileBrowser').hide('fast', function(){$(this).remove();})
                            }
                        });
                        $(elm).on('click', function (event) {
                            var path = currFolder + '/' + $(elm).attr('name');
                            if (!(event.ctrlKey || event.shiftKey || event.metaKey))
                                list.find('.fileNameBox').each(function (eix, fnm) { $(fnm).removeClass('fileSelected'); });
                            $(elm).toggleClass('fileSelected');
                            $('#fileManagerRenameForm input[name=newName]').val($(elm).attr('name'));
                        });
                    });
                } else
                    alert(resp.errorMessage);

                $('#fileBrowserLoading').hide();
            }
        });

    },
    getSelectedFiles: function () {
        return $A($('#fileBrowserList .fileSelected')).collect(function (elm) { return currFolder + '/' + $(elm).attr('name'); });
    },
    getSize: function (item) {
        if (item.size < 0) return ''; //***

        if (item.size >= 1024 * 1024) return Math.round(item.size / 1024 / 1024) + ' MB';
        if (item.size >= 1024) return Math.round(item.size / 1024) + ' KB';
        return item.size + ' B';
    },
    getFileClassName: function (item) {
        if (item.size == -1)
            return 'folder';
        var name = item.name.toLowerCase();
        if (name.endsWith('.png') || name.endsWith('.jpg') || name.endsWith('.jpeg') || name.endsWith('.gif') || name.endsWith('.jpe'))
            return 'picture';
        if (name.endsWith('.wmv') || name.endsWith('.mpg') || name.endsWith('.mpeg') || name.endsWith('.flv') || name.endsWith('.avi') || name.endsWith('.3gp') || name.endsWith('.rm') || name.endsWith('.mov'))
            return 'video';
        if (name.endsWith('.wav') || name.endsWith('.mp3') || name.endsWith('.wma') || name.endsWith('.mid'))
            return 'audio';
        return 'file';
    },
    formatDate: function (d) {
        var date = d.getDate(), month = d.getMonth() + 1, hour = d.getHours(), minute = d.getMinutes();
        if (date.toString().length == 1) date = '0' + date;
        if (month.toString().length == 1) month = '0' + month;
        if (hour.toString().length == 1) hour = '0' + hour;
        if (minute.toString().length == 1) minute = '0' + minute;
        return date + '/' + month + '/' + d.getFullYear() + ' ' + hour + ':' + minute;
    }
};
fileBrowserUploadFeedback = function (msg, url) {
    currPicEdit.getFileList();
    currPicEdit.container.find('form').trigger('reset');
}

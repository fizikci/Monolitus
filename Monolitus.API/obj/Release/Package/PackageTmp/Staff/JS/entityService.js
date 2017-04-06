app.service('entityService', function () {

    var url = "/Staff/Handlers/CRUDHandler.ashx?entityName=_ENTITY_NAME_";
    
    this.get = function (entityName, id, callback) {
        doAjaxCall(url.replace('_ENTITY_NAME_', entityName), callback, { method: "GetById", id: id });
    };

    this.getList = function (entityName, pageSize, pageNo, callback, where, orderBy) {
        var postData = { method: "GetList", pageSize: pageSize, pageNo: pageNo };
        if (where) postData["Where"] = where;
        if (orderBy) postData["OrderBy"] = orderBy;
        
        doAjaxCall(url.replace('_ENTITY_NAME_', entityName), function (res) {
            if(callback) callback(res);
        }, postData);
    };


    this.getIdNameList = function (entityName, pageSize, pageNo, callback, where, orderBy) {
        var postData = { method: "GetIdNameList", pageSize: pageSize, pageNo: pageNo };
        if (where) postData["Where"] = where;
        if (orderBy) postData["OrderBy"] = orderBy;

        doAjaxCall(url.replace('_ENTITY_NAME_', entityName), function (list) {
            if (callback) callback(list);
        }, postData);
    };

    this.delete = function (entityName, entity, callback) {
        doAjaxCall(url.replace('_ENTITY_NAME_', entityName), function () {
            if (callback) callback();
        }, { method: "DeleteById", id: entity.Id });
    };

    this.undelete = function (entityName, entity, callback) {
        doAjaxCall(url.replace('_ENTITY_NAME_', entityName), function () {
            if (callback) callback();
        }, { method: "UndeleteById", id: entity.Id });
    };

    this.save = function (entityName, data, callback) {
        doAjaxCall(url.replace('_ENTITY_NAME_', entityName) + '&method=SaveWithAjax', function (entity) {
            if (callback) callback(entity);
        }, data);
    };


    this.getEnumList = function (enumName, callback) {
        doAjaxCall(url.replace('_ENTITY_NAME_', 'User'), function(list) {
            if (callback) callback(list);
        }, { method: "GetEnumList", enumName: enumName });
    };

});

function doAjaxCall(url, callback, postData) {
    $.ajax({
        type: "POST",
        url: url,
        data: postData,
        cache: false,
        timeout: 20 * 1000,
        beforeSend: function () {
            //todo: show loading
        },
        complete: function () {
            //todo: hide loading
        },
        success: function (res) {
            if (res.isError) {
                alert(res.errorMessage);
                return;
            }

            if(callback) callback(res.data);
        },
        error: function (msg, t) {
            if (msg && !msg.isError) {
                var d = eval('(' + msg.responseText + ')');
                callback(d.data);
                return;
            }
            alert("HATA: " + msg.statusText);
        }
    });
}